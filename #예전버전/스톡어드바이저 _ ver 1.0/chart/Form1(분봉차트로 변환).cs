using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Media;
using System.Runtime.CompilerServices;
using AxKHOpenAPILib;

namespace chart
{
    public partial class Form1 : Form
    {
        //실시간 + TR 데이터베이스
        List<stockInfo> searchStock = new List<stockInfo>(); //stockInfo값을 가지는 리스트를 선언함. 리스트 선언은 이렇게 알고 있으면 될것.
        List<searchedList> requestList = new List<searchedList>();
        List<accountInfo> stockBalanceList = new List<accountInfo>();

        //메서드 여러개를 거치면서 임시적으로 쓸 변수
        string searchName;
        string[] searchList;
        string selectedStockName;

        //서버와 송수신할 때 쓰일 화면번호 변수
        public static int scrNum1 = 0; // 5000대 : 조건검색 결과 반환요청에 쓰임.
        public static int scrNum2 = 0; // 5100대 : Tr데이터 요청에 쓰임.
        public static int scrNum3 = 0; // 6000대 : Real데이터 요청에 쓰임.

        //타이머 변수
        int tick = 0;
        int sec = 0;

        //기타 UI에 쓰일 변수
        AutoCompleteStringCollection autoList = new AutoCompleteStringCollection();
        int[] checkCnt = { 0, 0, 0 };


        public Form1()
        {
            InitializeComponent();

            axKHOpenAPI1.CommConnect(); //로그인

            axKHOpenAPI1.OnEventConnect += onEventConnect; //로그인 이벤트 함수
            axKHOpenAPI1.OnReceiveTrData += onReceiveTrData; //TR데이터요청 이벤트 함수
            axKHOpenAPI1.OnReceiveRealData += onReceiveRealData;

            axKHOpenAPI1.OnReceiveConditionVer += onReceiveConditonVer;
            axKHOpenAPI1.OnReceiveRealCondition += onReceiveRealCondition;
            axKHOpenAPI1.OnReceiveTrCondition += onReceiveTrCondition;
            axKHOpenAPI1.OnReceiveChejanData += onReceiveChejanData;
            axKHOpenAPI1.OnReceiveMsg += onReceiveMsg;

            listBox1.KeyDown += listBox1_KeyDown;
            listBox2.KeyDown += listBox2_KeyDown;
            listBox3.KeyDown += listBox3_KeyDown;
            checkButton1.Click += checkButtonClicked;
        }

        public void onEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e) //로그인 이벤트 함수
        {
            if (e.nErrCode == 0)
            {
                axKHOpenAPI1.GetConditionLoad(); //조건식 요청
                LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + ": ## 로그인 성공. 조건식 요청");

                //계좌번호 목록을 불러온다.
                string[] accountList = axKHOpenAPI1.GetLoginInfo("ACCLIST").Split(';');
              
                for(int i=0; i<accountList.Length;i++)
                {
                    accountBox.Items.Add(accountList[i]);
                }
            }
        }

        public void onReceiveConditonVer(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveConditionVerEvent e) //조건검색결과 요청 이벤트 함수
        {
            if (e.lRet == 1)
            {

                LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + ": ## 조건검색 리스트를 불러옵니다.");

                string[] conditionList = axKHOpenAPI1.GetConditionNameList().TrimEnd(':').Split(';');  //조건검색식 목록불러오는 함수.

                searchAddComboBox.Items.AddRange(conditionList);
                autoList.AddRange(conditionList);
                searchAddComboBox.AutoCompleteCustomSource = autoList;
            }
        }

        public void onReceiveTrCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrConditionEvent e) //Tr데이터 조건검색결과 요청 이벤트함수
        {
            if (e.strCodeList.Length > 0)
            {
                searchList = e.strCodeList.TrimEnd(';').Split(';');
                int searchCnt = searchList.Length;

                tick = 0;
                timer1.Enabled = true;

                LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + ": ## 조건검색 종목 송신요청 성공.");  //kwrq데이터를 요청하는애도 있는데 실시간으로 하려면 얘도 필요한가? tr데이터도 있던데 흠... 
            }
            else
            {
                LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + ": ## 검색된 종목이 없습니다. 조건을 다시 확인해 보세요");
            }
        }

        public void onReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e) //TR데이터 요청 이벤트 함수
        {
            
            if(e.sRQName == "계좌평가현황요청")
            {
                int count = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);

                //보유종목을 업데이트하기 전에, stockBalanceList에서 사라진 보유종목이 있는지 확인하기 위해 열람여부값을 false로 해둔다.
                for (int i = 0; i < stockBalanceList.Count; i++)
                    stockBalanceList[i].열람값 = false;

                //stockBalanceList를 업데이트한다.
                for (int i = 0; i < count; i++)
                {
                    string stockName = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim();
                    string stockCode = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목코드").TrimStart('0').TrimStart('A').Trim();
                    int stockQty = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "보유수량"));
                    long moneyBought = long.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "매입금액"));
                    long currentPrice = long.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "현재가").Replace("-", ""));
                    double profitRate = Math.Round((double.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "손익율"))/10000),2);

                    int j = stockBalanceList.FindIndex(o => o.stockName == stockName);
                    if (j == -1)
                    {
                        stockBalanceList.Add(new accountInfo(stockName, stockCode, moneyBought, stockQty, profitRate));
                  //      MessageBox.Show("보유종목 신규추가;" + stockName + ";" + stockCode + ";" + moneyBought + ";" + stockQty + ";" + profitRate);
                    }
                    else
                    {
                        stockBalanceList[j].accountUpdate(moneyBought, stockQty, profitRate);
                  //      MessageBox.Show("보유종목 업데이트;" + stockName + ";" + stockCode + ";" + moneyBought + ";" + stockQty + ";" + profitRate);
                    }
                }

                //보유종목이 업데이트 된 이후에도 열람여부값이 false라면 해당 종목을 삭제한다.
                for (int i = 0; i < stockBalanceList.Count; i++)
                {
                    if (stockBalanceList[i].열람값 == false)
                    {
                        stockBalanceList.RemoveAt(i);
                    }
                    else
                    {
                        //stockBalanceList의 종목명을 기준으로 searchStock와 대조해보고, searchStock에 해당종목이 없다면 searchStock에 추가하고 분봉조회를 요청한다.
                        if (searchStock.FindIndex(o => o.stockName == stockBalanceList[i].stockName) == -1)
                        {
                            LogListBox.Items.Add(DateTime.Now + ": 보유종목을 searchStock 데이터베이스에 업데이트 합니다. :" +stockBalanceList[i].stockName);

                            searchStock.Add(new stockInfo(stockBalanceList[i].stockCode, stockBalanceList[i].stockName));
                            분봉조회(stockBalanceList[i].stockCode);
                        }
                    }
                }
            }
            
            if(e.sRQName.Length == 6)
            {
                LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + ": Tr데이터가 요청되었습니다. 종목명:" + e.sRQName);

                int i = searchStock.FindIndex(o => o.stockCode == e.sRQName);
                if (i >= 0)
                {
                    if (long.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "거래량").Trim()) > 0)
                    {
                        long v = 0;
                        int h = 0;
                        int l = 0;
                        int close = 0;
                        int open = 0;

                        searchStock[i].priceNow(Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "현재가").Trim())));

                        for (int j = 399; j >= 1; j--)
                        {
                            v = Math.Abs(long.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "거래량").Trim()));
                            h = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "고가").Trim()));
                            l = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "저가").Trim()));
                            close = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "현재가").Trim()));
                            open = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "시가").Trim()));

                            searchStock[i].priceAdd(h, l, open, close, v);


                            if (close > open)
                            {
                                searchStock[i].buySum += h * v;
                                searchStock[i].vBuySum += v;
                            }
                            else if (close < open)
                            {
                                searchStock[i].sellSum += l * v;
                                searchStock[i].vSellSum += v;
                            }
                        }


                        long lLine = searchStock[i].sellSum / searchStock[i].vSellSum;
                        long hLine = searchStock[i].buySum / searchStock[i].vBuySum;

                        searchStock[i].lineAdd(Convert.ToInt32(lLine), Convert.ToInt32(hLine));
                        searchStock[i].체결시간 = Convert.ToInt32(DateTime.Now.ToString("HHmm"));

                        v = Math.Abs(long.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "거래량").Trim()));
                        h = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "고가").Trim()));
                        l = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "저가").Trim()));
                        close = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "현재가").Trim()));
                        open = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "시가").Trim()));

                        searchStock[i].priceAdd(h, l, open, close, v);


                        /*
                        - 20 체결시간
                        - 10 현재가
                        - 15 거래량
                        - 13 누적거래량

                        - 21 호가시간
                        - 41 매도호가1 / 61 매도호가수량 / 81 매도호가직전대비1
                        - 42 매도호가2 / 62 매도호가수량
                        ....
                        - 50 매도호가10

                        - 51 매수호가1 / 71 매수호가수량
                        - 52
                        ...
                        - 60 매수호가10
                        */

                        axKHOpenAPI1.SetRealReg((6000 + scrNum3).ToString(), e.sRQName, "20;10;13;41;51;61;71", "1");
                        scrNum3++;

                        listBox0.Items.Add(searchStock[i].stockName);
                        dataGridView.Rows.Add(searchStock[i].stockName, 1, 1, 1, 1, 1, 1);

                    }
                }
            }
        }

        public void onReceiveRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent e) //실시간데이터 요청이벤트 함수 
        {
            if(e.sRealKey == "잔고")
            {}
            else
            {
                int i = searchStock.FindIndex(o => o.stockCode == e.sRealKey);

                if (i > -1)
                {
                    if (e.sRealType == "주식체결")
                    {
                        searchStock[i].minuteData(int.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 20).Trim()), Math.Abs(int.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 10).Trim())), long.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 13).Trim()));
                        searchStock[i].체결update(int.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 15).Trim()));
                    }

                    if (e.sRealType == "주식호가잔량")
                    {
                        int[] 매도호가가격 = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        int[] 매도호가잔량 = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        int[] 매수호가가격 = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                        int[] 매수호가잔량 = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                        for (int k = 0; k <= 0; k++)
                        {
                            매도호가가격[k] = int.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 41 + k).Trim());
                            매도호가잔량[k] = int.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 61 + k).Trim());
                            매수호가가격[k] = int.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 51 + k).Trim());
                            매수호가잔량[k] = int.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 71 + k).Trim());
                        }

                        /*      for (int k = 0; k <= 9; k++)
                              {
                                  MessageBox.Show(매도호가가격[k].ToString() + 매도호가잔량[k].ToString());
                              }*/

                searchStock[i].호가update(매도호가가격, 매도호가잔량, 매수호가가격, 매수호가잔량);
                    }
                }
            }
        }

        public void onReceiveRealCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealConditionEvent e) //실시간 조건검색결과 요청 이벤트함수
        {
            if (e.strType == "I") //종목 편입
            {
                searchName = axKHOpenAPI1.GetMasterCodeName(e.sTrCode);

                int i = searchStock.FindIndex(o => o.stockCode == e.sTrCode);
                if (i == -1)
                {
                    searchStock.Add(new stockInfo(e.sTrCode, searchName));
                    LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + ": 신규종목편입 (" + searchName + ")");
                    분봉조회(e.sTrCode); //신규 편입되는 종목의 tr데이터를 받아온다
                    label1.Text = "검색된 종목 개수 : " + searchStock.Count.ToString() + "개";
                }
            }

            else if (e.strType == "D") //종목 이탈
            {
                searchName = axKHOpenAPI1.GetMasterCodeName(e.sTrCode);
                LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + ": 기존종목이탈 (" + searchName + ")");
            }
        }

        public void onReceiveChejanData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEvent e)
        {
            if (e.sGubun == "0")//주문 접수 , 체결시
            {
                LogListBox.Items.Add("계좌번호 : " + axKHOpenAPI1.GetChejanData(9201) + " | " + " 주문번호 : " + axKHOpenAPI1.GetChejanData(9203));
                LogListBox.Items.Add("주문상태 : " + axKHOpenAPI1.GetChejanData(913) + " | " + " 종목명 : " + axKHOpenAPI1.GetChejanData(302));
                LogListBox.Items.Add("매매구분" + axKHOpenAPI1.GetChejanData(906) + " | " + " 주문수량 : " + axKHOpenAPI1.GetChejanData(900));

                string orderTime = axKHOpenAPI1.GetChejanData(908);
                string orderHour = orderTime[0] + "" + orderTime[1];
                string orderMinute = orderTime[2] + "" + orderTime[3];
                string orderSecond = orderTime[4] + "" + orderTime[5];
                long orderPrice = long.Parse(axKHOpenAPI1.GetChejanData(901));

                LogListBox.Items.Add("주문/체결시간 : " + orderHour + "시 " + orderMinute + "분 " + orderSecond + "초");
                LogListBox.Items.Add("주문구분 : " + axKHOpenAPI1.GetChejanData(905));
                LogListBox.Items.Add("주문가격 : " + String.Format("{0:#,###}", orderPrice));
                LogListBox.Items.Add("----------------------------------------------------------");
            }
        }


        public void onReceiveMsg(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveMsgEvent e)
        {
            if(e.sRQName == "매수" || e.sRQName == "매도")
                AutoClosingMessageBox.Show(e.sScrNo + ";" + e.sRQName + ";" + e.sTrCode + ";" + e.sMsg, "알림", 700);
        }

        //  --------------------------------------------------------------------------    메소드    -------------------------------------------------------------------------------//


        public void 잔고요청()
        {
            if (passwordBox.TextLength == 4 && accountBox.Text.Length > 0)
            {
                string accountNum = accountBox.Text;
                string password = passwordBox.Text;

                axKHOpenAPI1.SetInputValue("계좌번호", accountNum);
                axKHOpenAPI1.SetInputValue("비밀번호", password);
                axKHOpenAPI1.SetInputValue("비밀번호입력매체구분", "00");
                axKHOpenAPI1.SetInputValue("조회구분", "1");

                axKHOpenAPI1.CommRqData("계좌평가현황요청", "opw00004", 0, "0004");
            }
        }

        public void 분봉조회(string i)
        {
            axKHOpenAPI1.SetInputValue("종목코드", i);
            axKHOpenAPI1.SetInputValue("틱범위", "1");
            axKHOpenAPI1.SetInputValue("수정주가구분", "0"); //수정주가구분

            axKHOpenAPI1.CommRqData(i, "opt10080", 0, (5100 + scrNum2).ToString());

            LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + ": 분봉데이터를 요청합니다. 종목명:" + i + "화면번호 : " + (5100 + scrNum2));
            scrNum2++;
        }

        public static DateTime Delay(int MS)  // System.Threading.Thread.Sleep(210); 대신에 쓰이는것.
        {
            DateTime thisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime afterMoment = thisMoment.Add(duration);

            while (afterMoment >= thisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                thisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }

        public void renewal()
        {
            for (int i = 0; i < searchStock.Count; i++)
            {
                int h = searchStock[i].HLine;
                int c = searchStock[i].현재가;


                if (h == 9999999)
                {
                    searchStock[i].location = 4;
                }
                else if (h == 0)
                {

                }
                else
                {
                    switch (c)
                    {
                        case int n when (n < h):
                            searchStock[i].location = -1;
                            break;

                        case int n when ((n >= h) && (n <= h * 1.003)):
                            searchStock[i].location = 1;
                            break;

                        case int n when ((n > h * 1.008) && (n <= h * 1.013)):
                            searchStock[i].location = 2;
                            break;

                        case int n when ((n > h * 1.018) && (n <= h * 1.0245)):
                            searchStock[i].location = 3;
                            break;
                    }
                }

            }

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();


            for (int i = 0; i < searchStock.Count; i++)
            {
                if (searchStock[i].simple < -1.2 || searchStock[i].simple > 0)
                {
                    switch (searchStock[i].location)
                    {
                        case 1:
                            listBox1.Items.Add(searchStock[i].stockName + "; " + searchStock[i].simple.ToString() + " ; " + searchStock[i].종합.ToString(), searchStock[i].check);
                            break;

                        case 2:
                            listBox2.Items.Add(searchStock[i].stockName + "; " + searchStock[i].simple.ToString() + " ; " + searchStock[i].종합.ToString(), searchStock[i].check);
                            break;

                        case 3:
                            listBox3.Items.Add(searchStock[i].stockName + "; " + searchStock[i].simple.ToString() + " ; " + searchStock[i].종합.ToString(), searchStock[i].check);
                            break;
                        case 4:
                            listBox4.Items.Add(searchStock[i].stockName);
                            break;
                    }
                }
            }
        }

        public void Present()
        {
            PriceLogListBox.Items.Clear();


            int i = searchStock.FindIndex(o => o.stockName == selectedStockName);

            if (i >= 0)
            {
                PriceLogListBox.Items.Add("종목코드 :" + searchStock[i].stockCode);
                PriceLogListBox.Items.Add("현재가 :" + searchStock[i].현재가);
                PriceLogListBox.Items.Add("체결시간 :" + searchStock[i].체결시간);
                PriceLogListBox.Items.Add("hLine :" + searchStock[i].HLine);
                //     PriceLogListBox.Items.Add("lLine :" + searchStock[i].lLine);
                //     PriceLogListBox.Items.Add("저가 :" + searchStock[i].저가[0]);
                //      PriceLogListBox.Items.Add("시가 :" + searchStock[i].시가[0]);
                //     PriceLogListBox.Items.Add("고가 :" + searchStock[i].고가[0]);


                //     PriceLogListBox.Items.Add("1봉전 저가 :" + searchStock[i].저가[1]);
                //     PriceLogListBox.Items.Add("1봉전 시가 :" + searchStock[i].시가[1]);
                //     PriceLogListBox.Items.Add("1봉전 고가 :" + searchStock[i].고가[1]);

                for (int j = 0; j <= searchStock[i].종가.Count - 1; j++)
                {
                    PriceLogListBox.Items.Add(j + "번째 : (종)" + searchStock[i].종가[j] + " (시)" + searchStock[i].시가[j] + " (저)" + searchStock[i].저가[j] + " (고)" + searchStock[i].고가[j] + "(거래량)" + searchStock[i].거래량[j]);
                }


                /*for (int j = 0; j <= searchStock[i].거래량.Count - 1; j++)
                {
                    PriceLogListBox.Items.Add(j + "번째 전 봉의 거래량은 " +  + "입니다.");
                }*/
            }

        }

        public void playSimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\Speech On.wav");
            simpleSound.Play();
        }

        //  --------------------------------------------------------------------------   객체 이벤트함수-------------------------------------------------------------------------------//
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            sec++;
            //5분이 지났다면, 누적체결량/ 누적순매수 / 누적순매도 값을 리셋함.
            if (sec > 300)
            {
                sec = 0;
                for (int i = 0; i < searchStock.Count; i++)
                {
                    searchStock[i].누적체결량 = 0;
                    searchStock[i].누적매도순변동 = 0;
                    searchStock[i].누적매수순변동 = 0;
                }
            }

            // 3초마다 체결 데이터그리드를 업데이트 함.
            if (sec % 3 == 0)
            {
                for (int j = 0; j < dataGridView.Rows.Count; j++)
                {
                    int i = searchStock.FindIndex(o => o.stockName == dataGridView.Rows[j].Cells[0].Value.ToString());
                    if (i > -1)
                    {
                        dataGridView.Rows[j].Cells[1].Value = searchStock[i].누적체결량;
                        dataGridView.Rows[j].Cells[2].Value = searchStock[i].누적매수순변동;
                        dataGridView.Rows[j].Cells[3].Value = searchStock[i].누적매도순변동;
                        dataGridView.Rows[j].Cells[4].Value = searchStock[i].종합;
                        dataGridView.Rows[j].Cells[5].Value = searchStock[i].location;
                    }
                }
            }

            // 5초마다 잔고 데이터를 받아옴. 
            if (sec % 5 == 0)
            {
                잔고요청();
            }

            // 1초마다 stockSearch로 부터 잔고데이터를 업데이트함.
            if (passwordBox.TextLength == 4 && accountBox.Text.Length > 0)
            {
                balanceDataGridView.Rows.Clear();

                for (int i = 0; i < stockBalanceList.Count; i++)
                {
                    int j = searchStock.FindIndex(o => o.stockName == stockBalanceList[i].stockName);
                    stockBalanceList[i].priceUpdate(searchStock[j].현재가);

                    balanceDataGridView.Rows.Add(stockBalanceList[i].stockName, searchStock[j].simple, searchStock[j].종합, searchStock[j].location, stockBalanceList[i].손익율, stockBalanceList[i].최고손익율);

                    if (searchStock[j].simple < 0 && searchStock[j].simple > -1)
                        balanceDataGridView.Rows[i].Cells[1].Style.BackColor = Color.SkyBlue;
                    else
                        balanceDataGridView.Rows[i].Cells[1].Style.BackColor = Color.Coral;

                    if (searchStock[j].종합 < -100)
                        balanceDataGridView.Rows[i].Cells[2].Style.BackColor = Color.SkyBlue;
                    

                    if (searchStock[j].location == -1)
                        balanceDataGridView.Rows[i].Cells[3].Style.BackColor = Color.SkyBlue;

                    if (stockBalanceList[i].손익율 < -2)
                        balanceDataGridView.Rows[i].Cells[4].Style.BackColor = Color.SkyBlue;
                }
            }

            //자동분류에 체크되어있을경우 1초마다 실행함.
            if (checkBox1.Checked == true)
            {
                renewal();
            }

            // 조건검색 종목들을 받아올 때, timer1에 맞춰서 한번에 하나씩 받아온다.
            if (tick < searchList.Length)
            {
                string searchName = axKHOpenAPI1.GetMasterCodeName(searchList[tick]);

                if (searchStock.FindIndex(o => o.stockName == searchName) < 0)
                    searchStock.Add(new stockInfo(searchList[tick], searchName));

                분봉조회(searchList[tick]);

                LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + "분봉조회를 요청합니다." + searchList[tick]);

                tick++;
            }

            // 조건검색 종목들을 모두 받아왔을때, tick을 해제하고 모두 받아왔음을 알려준다.
            else if (tick == searchList.Length)
            {
                int i = tick;
                tick = 9999;
                MessageBox.Show("검색결과 전부 송신완료했습니다. \n받아온 종목수는 " + (i) + "개 입니다.");
                label1.Text = "검색된 종목 개수 : " + searchStock.Count.ToString() + "개";
            }
        }


        private void renewalButton_Click(object sender, EventArgs e)
        {
            renewal();
        }

        private void searchAddButton_Click(object sender, EventArgs e)
        {
            if(searchAddComboBox.Text.Length>0)
            {
                //기존종목유지 체크표시가 해제되어있다면, 화면번호를 리셋한다.
                if (addTogetherCheckBox.Checked == false)
                {

                    dataGridView.Rows.Clear();

                    axKHOpenAPI1.SetRealRemove("ALL", "ALL");

                    for (int i = 0; i < requestList.Count; i++)
                    {
                        //   MessageBox.Show(requestList[i].화면번호 +  requestList[i].검색명 + requestList[i].검색인덱스);
                        axKHOpenAPI1.SendConditionStop(requestList[i].화면번호, requestList[i].검색명, requestList[i].검색인덱스);
                    }

                    requestList.Clear();

                    for (int i = 0; i <= scrNum1; i++)
                    {
                        axKHOpenAPI1.DisconnectRealData((5000 + scrNum2).ToString());
                        scrNum1 = 0;
                    }

                    for (int i = 0; i <= scrNum2; i++)
                    {
                        axKHOpenAPI1.DisconnectRealData((5100 + scrNum2).ToString());
                        scrNum2 = 0;
                    }

                    for (int i = 0; i <= scrNum3; i++)
                    {
                        axKHOpenAPI1.DisconnectRealData((6000 + scrNum3).ToString());
                        scrNum3 = 0;
                    }

                    dataGridView.Rows.Clear();
                    listBox0.Items.Clear();
                    searchStock.Clear();
                }

                requestList.Insert(0, new searchedList((5000 + scrNum1).ToString(), searchAddComboBox.Text.Split('^')[1], Convert.ToInt32(searchAddComboBox.Text.Split('^')[0])));

                axKHOpenAPI1.SendCondition(requestList[0].화면번호, requestList[0].검색명, requestList[0].검색인덱스, 1);
                scrNum1++;

                LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + ": ## 조건검색 송신요청합니다. 화면번호" + (scrNum1 + 5000));
            }
        }

        private void checkButtonClicked(object sender, EventArgs e)
        {
            bool chk;

            if (checkCnt[0] % 2 == 0)
                chk = true;
            else
                chk = false;

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                listBox1.SetItemChecked(i, chk);
                searchName = listBox1.Items[i].ToString().Split(';')[0];
                searchStock.Find(o => o.stockName == searchName).check = chk;
            }
            checkCnt[0]++;
        }
        private void checkButton2_Click(object sender, EventArgs e)
        {
            bool chk;

            if (checkCnt[1] % 2 == 0)
                chk = true;
            else
                chk = false;

            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                listBox2.SetItemChecked(i, chk);
                searchName = listBox2.Items[i].ToString().Split(';')[0];
                searchStock.Find(o => o.stockName == searchName).check = chk;
            }
            checkCnt[1]++;
        }
        private void checkButton3_Click(object sender, EventArgs e)
        {
            bool chk;

            if (checkCnt[2] % 2 == 0)
                chk = true;
            else
                chk = false;

            for (int i = 0; i < listBox3.Items.Count; i++)
            {
                listBox3.SetItemChecked(i, chk);
                searchName = listBox3.Items[i].ToString().Split(';')[0];
                searchStock.Find(o => o.stockName == searchName).check = chk;
            }
            checkCnt[2]++;
        }

        private void listBox0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox0.SelectedIndex >= 0)
            {
                selectedStockName = listBox0.SelectedItem.ToString();
                searchNameLabel.Text = "선택된 종목:" + selectedStockName;
                searchNameLabel1.Text = selectedStockName;
                Present();
            }
        }
        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                selectedStockName = listBox1.SelectedItem.ToString().Split(';')[0];
                searchNameLabel.Text = "선택된 종목:" + selectedStockName;
                searchNameLabel1.Text = selectedStockName;

                if (listBox1.GetItemChecked(listBox1.SelectedIndex) == true)
                    searchStock.Find(o => o.stockName == selectedStockName).check = true;
                else
                    searchStock.Find(o => o.stockName == selectedStockName).check = false;

                Present();
            }
        }
        private void listBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex >= 0)
            {
                selectedStockName = listBox2.SelectedItem.ToString().Split(';')[0];
                searchNameLabel.Text = "선택된 종목:" + selectedStockName;
                searchNameLabel1.Text = selectedStockName;

                int i = searchStock.FindIndex(o => o.stockName == selectedStockName);

                if (listBox2.GetItemChecked(listBox2.SelectedIndex) == true)
                    searchStock[i].check = true;
                else
                    searchStock[i].check = false;

                Present();

            }
        }
        private void listBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex >= 0)
            {
                selectedStockName = listBox3.SelectedItem.ToString().Split(';')[0];
                searchNameLabel.Text = "선택된 종목:" + selectedStockName;
                searchNameLabel1.Text = selectedStockName;

                if (listBox3.GetItemChecked(listBox3.SelectedIndex) == true)
                    searchStock.Find(o => o.stockName == selectedStockName).check = true;
                else
                    searchStock.Find(o => o.stockName == selectedStockName).check = false;
                Present();
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            int i = listBox1.SelectedIndex;
            if (i >= 0)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    searchName = listBox1.Items[i].ToString().Split(';')[0];

                    listBox1.Items.RemoveAt(i);
                    listBox4.Items.Add(searchName);

                    searchStock.Find(o => o.stockName == searchName).HLine = 9999999;
                }

                if(e.KeyCode == Keys.C)
                {
                    searchName = listBox1.Items[i].ToString().Split(';')[0];
                    searchStock.Find(o => o.stockName == searchName).check = true;
                }
            }
        }
        private void listBox2_KeyDown(object sender, KeyEventArgs e)
        {
            int i = listBox2.SelectedIndex;
            if (i >= 0)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    searchName = listBox2.Items[i].ToString().Split(';')[0];

                    listBox2.Items.RemoveAt(i);
                    listBox4.Items.Add(searchName);

                    searchStock.Find(o => o.stockName == searchName).HLine = 9999999;
                }
                if (e.KeyCode == Keys.C)
                {
                    searchName = listBox2.Items[i].ToString().Split(';')[0];
                    searchStock.Find(o => o.stockName == searchName).check = true;
                }
            }
        }
        private void listBox3_KeyDown(object sender, KeyEventArgs e)
        {
            int i = listBox3.SelectedIndex;
            if (i >= 0)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    searchName = listBox3.Items[i].ToString().Split(';')[0];

                    listBox3.Items.RemoveAt(i);
                    listBox4.Items.Add(searchName);

                    searchStock.Find(o => o.stockName == searchName).HLine = 9999999;
                }
                if (e.KeyCode == Keys.C)
                {
                    searchName = listBox3.Items[i].ToString().Split(';')[0];
                    searchStock.Find(o => o.stockName == searchName).check = true;
                }
            }
        }

        private void dataGridViewkey(object sender, KeyPressEventArgs e)
        {
            int i = dataGridView.CurrentCell.RowIndex;

            if (dataGridView.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
            {
                dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }

            else
            {
                dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
            }
        }

        private void passwordBox_TextChanged(object sender, EventArgs e)
        {
            잔고요청();
        }

        private void balanceDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = balanceDataGridView.CurrentCell.RowIndex;
            string sellStockName = balanceDataGridView.Rows[i].Cells[0].Value.ToString();
            if (sellStockName.Length > 0)
                searchNameLabel2.Text = sellStockName;
        }

        private void sellButton_Click(object sender, EventArgs e)
        {
            if (passwordBox.TextLength == 4 && accountBox.Text.Length > 0 && searchNameLabel2.Text.Length > 0)
            {
                int i = searchStock.FindIndex(o => o.stockName == searchNameLabel2.Text);
                string 종목코드 = searchStock[i].stockCode;
                int 보유수량 = stockBalanceList.Find(o => o.stockName == searchNameLabel2.Text).보유수량;
                int 현재가 = searchStock[i].현재가;

                axKHOpenAPI1.SendOrder("매도", "9000", accountBox.Text , 2, 종목코드, 보유수량, 0, "03", "");
            }
            else
                MessageBox.Show("필요한 정보를 입력해주세요.");
            
        }

        private void buyButton_Click(object sender, EventArgs e)
        {
            if (passwordBox.TextLength == 4 && accountBox.Text.Length > 0 && searchNameLabel1.Text.Length > 0 && stockQtyBox.TextLength > 0)
            {
                int i = searchStock.FindIndex(o => o.stockName == searchNameLabel1.Text);
                string 종목코드 = searchStock[i].stockCode;
                int 현재가 = searchStock[i].현재가;

                axKHOpenAPI1.SendOrder("매수", "8249", accountBox.Text, 1, 종목코드, int.Parse(stockQtyBox.Text), 0, "03", "");
            }
            else
                MessageBox.Show("필요한 정보를 입력해주세요.");
        }
    }
}
