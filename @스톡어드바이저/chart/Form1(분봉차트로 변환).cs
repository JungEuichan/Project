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
using System.IO;

namespace chart
{
    public partial class Form1 : Form
    {
        List<stockInfo> stockDB = new List<stockInfo>();                    //실시간 + TR 데이터베이스 저장리스트
        List<stockInfo> tempDB = new List<stockInfo>();
        List<requestConditonInfo> requestConditonList = new List<requestConditonInfo>();          //사용자가 송신한 조건검색 리스트. 나중에 조건검색결과 해지할때쓰인다.
        List<accountInfo> stockBalanceList = new List<accountInfo>();       //사용자의 보유종목을 포함하는 리스트

        //서버와 송수신할 때 쓰일 화면번호 변수
        public static int scrNum_조건검색 = 0;       // 5000대 : 조건검색 결과 반환요청에 쓰임.
        public static int scrNum_TR = 0;             // 5100대 : Tr데이터 요청에 쓰임.
        public static int scrNum_Real = 0;           // 6000대 : Real데이터 요청에 쓰임.

        //타이머 변수
        int tick_종목수 = 0;
        int sec = 0;
        int msec = 0;

        //기타 UI에 쓰일 변수
        int[] checkCnt = { 0, 0, 0 };   //listBox에 있는 종목들 일괄체크에 쓰이는 변수이다.

        //메서드 간 통신을 위한 변수
        string[] searchList;           //조건검색 결과, 나온 종목들을 저장하는 리스트
        string selectedStockName;       //사용자가 ListBox에서 마우스 클릭을 통해 선택한 종목명
        string searchName;              //메소드끼리 공유하는 종목명
        Boolean isMarketOpen = false;               //장시작전 0, 장중 1
        Boolean isTradeFake = true;
        Boolean isMoneyAvail = false;

        public Form1()
        {
            InitializeComponent();

            axKHOpenAPI1.CommConnect(); //로그인

            axKHOpenAPI1.OnEventConnect += onEventConnect;       //로그인 이벤트함수
            axKHOpenAPI1.OnReceiveTrData += onReceiveTrData;     //TR데이터요청 이벤트함수
            axKHOpenAPI1.OnReceiveRealData += onReceiveRealData; //Real데이터요청 이벤트함수

            axKHOpenAPI1.OnReceiveConditionVer += onReceiveConditonVer; //조건검색요청 이벤트함수
            axKHOpenAPI1.OnReceiveRealCondition += onReceiveRealCondition; //조건검색 실시간 이벤트함수
            axKHOpenAPI1.OnReceiveTrCondition += onReceiveTrCondition; //조건검색결과반환 이벤트함수
            axKHOpenAPI1.OnReceiveChejanData += onReceiveChejanData; //사용자 매매주문 결과반환 이벤트함수 (매매일시, 수량, 시장가등의 정보)
            axKHOpenAPI1.OnReceiveMsg += onReceiveMsg; //사용자 매매주문 결과반환 이벤트함수 (매수매도 실패관련 정보)


        }

        public void onEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e) //로그인 이벤트 함수. Form1형성후 한번만 호출된다. 
        {
            if (e.nErrCode == 0)
            {
                addToLogBox(": ## 로그인 성공");

                //계좌번호들을 불러온다.
                string[] accountList = axKHOpenAPI1.GetLoginInfo("ACCLIST").Split(';');
                addToLogBox(": ## 사용자 계좌번호 목록 요청.");

                //계좌번호를 받아와서 콤보박스에 추가해 준다.
                for (int i = 0; i < accountList.Length; i++)
                {
                    accountBox.Items.Add(accountList[i]);
                }

                axKHOpenAPI1.GetConditionLoad(); //조건식 요청
                addToLogBox(": ## 조건식 요청");

                
                잔고요청();
                addToLogBox(": ## 사용자 계좌에 접근합니다. 주기적 잔고요청시작.");
                //잔고 데이터를 실시간으로 업데이트하기 위한 timer2를 켜준다.
                timer2.Enabled = true;


                addToLogBox("지수 요청중");
                stockDB.Add(new stockInfo("226490", "KODEX 코스피"));
                분봉조회("226490");
                int j = stockDB.FindIndex(o => o.stockCode == "226490");
                stockDB[j].location = 4;

                addToLogBox(": ## 005^AB패턴 : 매물선이용한 인트라데이용 : 조건검색 송신요청합니다. 화면번호" + (scrNum_조건검색 + 5000));
                {
                    //화면번호 및 데이터를 리셋한다.
                    {
                        axKHOpenAPI1.SetRealRemove("ALL", "ALL");

                        for (int i = 0; i < requestConditonList.Count; i++)
                        {
                            axKHOpenAPI1.SendConditionStop(requestConditonList[i].화면번호, requestConditonList[i].검색명, requestConditonList[i].검색인덱스);
                        }

                        for (int i = 0; i <= scrNum_조건검색; i++)
                        {
                            axKHOpenAPI1.DisconnectRealData((5000 + scrNum_TR).ToString());
                            scrNum_조건검색 = 0;
                        }

                        for (int i = 0; i <= scrNum_TR; i++)
                        {
                            axKHOpenAPI1.DisconnectRealData((5100 + scrNum_TR).ToString());
                            scrNum_TR = 0;
                        }

                        for (int i = 0; i <= scrNum_Real; i++)
                        {
                            axKHOpenAPI1.DisconnectRealData((6000 + scrNum_Real).ToString());
                            scrNum_Real = 0;
                        }

                        //사용자 UI에 표시된 것들을 지운다.
                        dataGridView.Rows.Clear();
                        listBox0.Items.Clear(); // 참고로 listBox1/2/3는 stockDB를 실시간으로 반영하므로 지우지 않아도 된다.

                        //내부 데이터를 지운다.
                        stockDB.Clear();
                        requestConditonList.Clear();
                    }

                    //나중에 조건검색 해지를 위해 requestConditon
                    requestConditonList.Insert(0, new requestConditonInfo((5000 + scrNum_조건검색).ToString(), searchBox.Text.Split('^')[1], Convert.ToInt32(searchBox.Text.Split('^')[0])));
                    axKHOpenAPI1.SendCondition(requestConditonList[0].화면번호, requestConditonList[0].검색명, requestConditonList[0].검색인덱스, 1);
                    scrNum_조건검색++;
                }

            }
        }

        public int isTradeOK()
        {
            //코스피 지수가 30분봉상으로 좋지 않다면 매매하지 않는다.
            int j = stockDB.FindIndex(o => o.stockCode == "226490");
            if(j>-1)
            {
                //장이 열려있는지, 이미 매수한 종목이 없는지도 확인한다.
                if(stockDB[j].location != -1 && isMarketOpen && isMoneyAvail)
                    return 1;
                else
                    return 0;
            }
            else
                return -1;
        }

        public void onReceiveConditonVer(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveConditionVerEvent e) //조건검색결과 요청 이벤트 함수. 로그인 직후에 한번만 호출된다. 
        {
            if (e.lRet == 1)
            {
                addToLogBox(": ## 조건검색 리스트를 불러옵니다.");

                string[] conditionList = axKHOpenAPI1.GetConditionNameList().TrimEnd(':').Split(';');  //조건검색식 목록불러오는 함수. conditionList는 "조건검색index^조건검색"의 값을 가진다.

                //조건검색명 콤보박스에 데이터를 추가한다.
                searchBox.Items.AddRange(conditionList);

                //조건검색명 콤보박스에 자동완성기능을 추가한다.
                AutoCompleteStringCollection searchBox_AC = new AutoCompleteStringCollection();
                searchBox_AC.AddRange(conditionList);
                searchBox.AutoCompleteCustomSource = searchBox_AC;
            }
        }

        public void onReceiveTrCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrConditionEvent e) //Tr데이터 조건검색결과 요청 이벤트함수. searchList를 반환한다. TImer1을 켜준다. 
        {
            if (e.strCodeList.Length > 0)
            {
                //받아온 종목들을 종목코드 데이터로 searchList에 저장한다.
                searchList = e.strCodeList.TrimEnd(';').Split(';');

                tick_종목수 = 0;
                timer1.Enabled = true;

                addToLogBox(": ## 조건검색 종목 송신요청 성공.");
            }
            else
                addToLogBox(": ## 검색된 종목이 없습니다. 조건을 다시 확인해 보세요");
        }

        public void onReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e) //TR데이터 요청 이벤트 함수. 실시간데이터를 요청한다.
        {
            //잔고현황요청을 더 많이 할것이기 때문에 분봉조회 TR요청보다 앞에다가 둠.
            if (e.sRQName == "잔고현황요청")
            {
                int count = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);

                //데이터 그리드 값을 리셋해준다.
                balanceDataGridView.Rows.Clear();


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
                    double profitRate = Math.Round((double.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "손익율")) / 10000), 2);

                    int j = stockBalanceList.FindIndex(o => o.stockName == stockName);

                    if (j == -1) // 보유종목이 추가된 경우이다.
                        stockBalanceList.Add(new accountInfo(stockName, stockCode, moneyBought, stockQty, profitRate));
                    else         // 기존에 있던 종목인 경우이다.
                        stockBalanceList[j].accountUpdate(moneyBought, stockQty, profitRate);
                }

                //보유종목이 업데이트 된 이후에도 열람여부값이 false라면 해당 종목을 삭제한다.
                for (int i = 0; i < stockBalanceList.Count; i++)
                {
                    if (stockBalanceList[i].열람값 == false)
                    {
                        stockBalanceList.RemoveAt(i);
                        stockDB.RemoveAt(stockDB.FindIndex(o => o.stockCode == stockBalanceList[i].stockCode));
                    }

                    else
                    {
                        //stockBalanceList의 종목코드를 기준으로 stockDB와 대조해보고, stockDB에 해당종목이 없다면 stockDB에 추가하고 분봉조회를 요청한다. 분봉조회를 할 때, 너무 자주하면 서버에서 거절될수 있으므로 잠시 쉬었다가 받아준다.
                        if (stockDB.FindIndex(o => o.stockCode == stockBalanceList[i].stockCode) == -1)
                        {
                            addToLogBox(": 보유종목을 stockDB 데이터베이스에 업데이트 합니다. :" + stockBalanceList[i].stockName);

                            stockDB.Add(new stockInfo(stockBalanceList[i].stockCode, stockBalanceList[i].stockName));
                            Delay(300);
                            분봉조회(stockBalanceList[i].stockCode);
                        }

                        //데이터 그리드 값에 내용을 추가해준다.
                        balanceDataGridView.Rows.Add(stockBalanceList[i].stockName, 0, 0, 0, 0, 0, 0);
                    }
                }
            }

            //분봉조회할 때, 종목명에 대한 정보도 넘겨주기 위해서 종목코드를 RQName으로 잡음. 종목코드의 자리수가 6개임을 이용한 if문임.
            //분봉데이터를 stockDB에 저장하고, 선을 계산한다./ 데이터그리드에 행을 추가하고, listBox0에 항목을 추가한다. / 실시간조회를 요청한다.
            else if (e.sRQName.Length == 6)
            {
                int i = stockDB.FindIndex(o => o.stockCode == e.sRQName);
                if (i >= 0)
                {
                    if (long.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "거래량").Trim()) > 0)
                    {
                        long v = 0;
                        int h = 0;
                        int l = 0;
                        int close = 0;
                        int open = 0;

                        stockDB[i].priceNow(Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "현재가").Trim())));

                        for (int j = 399; j >= 1; j--)
                        {
                            v = Math.Abs(long.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "거래량").Trim()));
                            h = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "고가").Trim()));
                            l = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "저가").Trim()));
                            close = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "현재가").Trim()));
                            open = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "시가").Trim()));

                            stockDB[i].priceAdd(h, l, open, close, v);


                            if (close > open)
                            {
                                stockDB[i].buySum += h * v;
                                stockDB[i].vBuySum += v;
                            }
                            else if (close < open)
                            {
                                stockDB[i].sellSum += l * v;
                                stockDB[i].vSellSum += v;
                            }
                        }


                        long lLine = stockDB[i].sellSum / stockDB[i].vSellSum;
                        long hLine = stockDB[i].buySum / stockDB[i].vBuySum;

                        stockDB[i].lineAdd(Convert.ToInt32(lLine), Convert.ToInt32(hLine));
                        stockDB[i].체결시간 = Convert.ToInt32(DateTime.Now.ToString("HHmm"));

                        v = Math.Abs(long.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "거래량").Trim()));
                        h = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "고가").Trim()));
                        l = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "저가").Trim()));
                        close = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "현재가").Trim()));
                        open = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "시가").Trim()));

                        stockDB[i].priceAdd(h, l, open, close, v);

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

                        axKHOpenAPI1.SetRealReg((6000 + scrNum_Real).ToString(), e.sRQName, "20;10;13;41;51;61;71", "1");
                        scrNum_Real++;

                        listBox0.Items.Add(stockDB[i].stockName);
                        dataGridView.Rows.Add(stockDB[i].stockName, 1, 1, 1, 1, 1, 1);
                    }
                }
            }
        }

        public void onReceiveRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent e) //실시간데이터 요청이벤트 함수. 체결데이터 / 호가데이터 / 분봉데이터를 각각 stockDB에 반영한다. 
        {
            if (e.sRealKey == "잔고") //잔고 실시간 데이터는 받지 않는다.
            { }
            else
            {
                int i = stockDB.FindIndex(o => o.stockCode == e.sRealKey);

                if (i > -1)
                {
                    if (e.sRealType == "주식체결")
                    {
                        stockDB[i].minuteData(int.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 20).Trim()), Math.Abs(int.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 10).Trim())), long.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 13).Trim()));
                        stockDB[i].체결update(int.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 15).Trim()));
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

                        stockDB[i].호가update(매도호가가격, 매도호가잔량, 매수호가가격, 매수호가잔량);
                    }
                }
            }
        }

        public void onReceiveRealCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealConditionEvent e) //실시간 조건검색결과 이벤트함수 / 종목추가시 stockDB에 추가, 종목개수 라벨에 update
        {
            if (e.strType == "I") //종목 편입
            {
                string stockName = axKHOpenAPI1.GetMasterCodeName(e.sTrCode);

                int i = tempDB.FindIndex(o => o.stockCode == e.sTrCode);
                if (i == -1)
                {
                    tempDB.Add(new stockInfo(e.sTrCode, stockName));
                    addToLogBox(": 신규종목편입 (" + stockName + ")");
                    
                }
            }
           
            else if (e.strType == "D") //종목 이탈
            {
                string stockName = axKHOpenAPI1.GetMasterCodeName(e.sTrCode);
                addToLogBox(": 기존종목이탈 (" + stockName + ")");

                if (realRemoveCheck.Checked == true)
                {
                    stockDB.RemoveAt(stockDB.FindIndex(o => o.stockName == stockName));
                }
            }
        }

        private void timer1_tick(object sender, EventArgs e) //조건검색 종목을 1초당 하나씩 조회요청한다. / 누적호가,체결데이터를 리셋 / 체결데이터 그리드업데이트 / 선 자동분류
        {
            sec++;

            // 조건검색 종목들을 받아올 때, timer1에 맞춰서 한번에 하나씩 받아온다.
            if (tick_종목수 < searchList.Length)
            {
                searchName = axKHOpenAPI1.GetMasterCodeName(searchList[tick_종목수]); //여기에서의 searchName을 전역변수로 선언한이유는 분봉조회한 후 TR 이벤트함수에서 이 값을 필요로 하기 때문이다.

                //잔고현황요청으로 이미 stockDB가 있는 종목도 있으니 그런 종목은 거른다.
                if (stockDB.FindIndex(o => o.stockCode == searchList[tick_종목수]) < 0)
                {
                    stockDB.Add(new stockInfo(searchList[tick_종목수], searchName));
                    분봉조회(searchList[tick_종목수]);
                }
                else
                    addToLogBox(" : 이미 존재하는 종목입니다. 종목명 : " + searchName);

                tick_종목수++;
            }

            // 조건검색 종목들을 모두 받아왔을때, tick_종목수을 해제하고 모두 받아왔음을 알려준다.
            else if (tick_종목수 == searchList.Length)
            {
                int i = tick_종목수;
                tick_종목수 = 9999;
                // 바로 메세지박스에 tick_종목수를 표시하지 않는이유는, 메시지박스가 표시되는동안, 코드는 멈춰있는데 타이머만 실행되기 때문에 메시지박스가 여러개가 표시된다.
                // 따라서 메시지박스가 표시되기 전에 tick_종목수 값을 바꿔준다.

                MessageBox.Show("검색결과 전부 송신완료했습니다. \n받아온 종목수는 " + (i) + "개 입니다.");
                label1.Text = "stockDB 종목개수 : " + stockDB.Count.ToString() + "개";
            }

            //5분이 지났다면, 누적체결량/ 누적순매수 / 누적순매도 값을 리셋함. + sec값도 0으로 바꿔줌.
            if (sec > 180)
            {
                sec = 0;
                for (int i = 0; i < stockDB.Count; i++)
                {
                    stockDB[i].누적체결량 = 0;
                    stockDB[i].누적음수체결량 = 0;
                    stockDB[i].누적양수체결량 = 0;
                    stockDB[i].누적매도순변동 = 0;
                    stockDB[i].누적매수순변동 = 0;
                }
            }

            // 3초마다 체결 데이터그리드를 업데이트 함.
            if (sec % 3 == 0)
            {
                for (int j = 0; j < dataGridView.Rows.Count; j++)
                {
                    int i = stockDB.FindIndex(o => o.stockName == dataGridView.Rows[j].Cells[0].Value.ToString());
                    if (i > -1)
                    {
                        dataGridView.Rows[j].Cells[1].Value = stockDB[i].누적체결량;
                        dataGridView.Rows[j].Cells[2].Value = stockDB[i].누적매수순변동;
                        dataGridView.Rows[j].Cells[3].Value = stockDB[i].누적매도순변동;
                        dataGridView.Rows[j].Cells[4].Value = stockDB[i].하락방어율;
                        dataGridView.Rows[j].Cells[5].Value = stockDB[i].location;
                    }
                }
            }

            //자동분류에 체크되어있을경우 3초마다 분류함.
            if (checkBox1.Checked == true && sec % 2 == 0)
            {
                renewal();
            }

            //
            if(sec % 10 == 0) //10초에 한번씩 조건검색 종목 편입이 결정된다 / 잦은 편입 이탈으로 인해 키움에서 접근거부하는 것을 막기 위함이다 이탈은 실시간으로 되는 반면에 편입은 10초에 한번으로 결정된다.
            {

                for(int j=0;j<tempDB.Count;j++)
                {
                    if(stockDB.FindIndex(o => o.stockName == tempDB[j].stockName) == -1)
                    {
                        stockDB.Add(new stockInfo(tempDB[j].stockName, tempDB[j].stockCode));
                        tempDB.RemoveAt(j);
                        addToLogBox(": 편입종목반영 (" + tempDB[j].stockName + "), tempDB 종목 남은개수 : "+tempDB.Count);

                        분봉조회(tempDB[j].stockCode); //신규 편입되는 종목의 tr데이터를 받아온다
                        label1.Text = "검색된 종목 개수 : " + stockDB.Count.ToString() + "개";
                    }
                }
             }
        }

        private void timer2_tick(object sender, EventArgs e)
        {
            msec++;

            // 5초마다 잔고 데이터를 받아옴 > 확인이 수월하기 위해서 5초이지만, 나중에는 30초마다 한번으로 줄일것.
            if (msec % 100 == 0)
            {
                잔고요청();

                if (int.Parse(DateTime.Now.ToString("HHmm")) > 0930 && int.Parse(DateTime.Now.ToString("HHmm")) < 1500)
                {
                    if (!isMarketOpen)
                        addToLogBox(": ## 장개시했습니다.");
                    isMarketOpen = true;
                }
                else
                {
                    if (isMarketOpen)
                        addToLogBox(": ## 장마감되었습니다.");
                    isMarketOpen = false;
                }


            }

            // 200ms마다 stockSearch로 부터 잔고데이터를 업데이트함. 매도 조건에 따라 종목 매도함. 
            if (passwordBox.TextLength == 4 && accountBox.Text.Length > 0)
            {

                for (int i = 0; i < balanceDataGridView.Rows.Count; i++)
                {
                    int j = stockDB.FindIndex(o => o.stockName == balanceDataGridView.Rows[i].Cells[0].Value.ToString());
                    if (j > -1)
                    {

                        // 50초마다 분봉상저가최고가를 구함. 이 간격이 너무짧으면 데이터를 받아오기도 전에 계산이 진행되어 에러를 일으킬수있다. **msec를 초기화함.** 
                        if (msec > 500)
                        {
                            int highestLow;

                            if (stockDB[j].종가[1] > stockDB[j].시가[1])
                                highestLow = stockDB[j].저가[1];
                            else
                                highestLow = stockDB[j].종가[1];

                            if (stockBalanceList[i].분봉상저가최고가 < highestLow)
                                stockBalanceList[i].분봉상저가최고가 = highestLow;


                            msec = 0;
                        }

                        //stockBalanceList에 현재가를 업데이트하고, 잔고dataGridView를 업데이트한다.
                        stockBalanceList[i].priceUpdate(stockDB[j].현재가);

                        balanceDataGridView.Rows[i].Cells[1].Value = stockDB[j].하락방어율;
                        balanceDataGridView.Rows[i].Cells[2].Value = stockDB[j].상승확률;
                        balanceDataGridView.Rows[i].Cells[3].Value = stockDB[j].location;
                        balanceDataGridView.Rows[i].Cells[4].Value = stockBalanceList[i].손익율;
                        balanceDataGridView.Rows[i].Cells[5].Value = stockBalanceList[i].최고손익율;
                        balanceDataGridView.Rows[i].Cells[6].Value = stockBalanceList[i].분봉상저가최고가;

                        if (stockDB[j].하락방어율 < 1)
                            balanceDataGridView.Rows[i].Cells[1].Style.BackColor = Color.SkyBlue;
                        else
                            balanceDataGridView.Rows[i].Cells[1].Style.BackColor = Color.Coral;

                        if (stockDB[j].상승확률 < 1)
                            balanceDataGridView.Rows[i].Cells[2].Style.BackColor = Color.SkyBlue;

                        if (stockDB[j].location == -1)
                            balanceDataGridView.Rows[i].Cells[3].Style.BackColor = Color.SkyBlue;

                        if (stockBalanceList[i].손익율 < -2)
                            balanceDataGridView.Rows[i].Cells[4].Style.BackColor = Color.SkyBlue;
                    }
                    else
                    {
                        //MessageBox.Show("에러발생. balanceDataGridView에 표시할 stockDB가 업데이트되지 않았습니다");
                    }



                    // 매도 전략 체크 여부에 따라 매도 감시 시작, 손익율이 -30퍼센트를 넘는경우는 자동매도대상으로 삼지 않는다.
                    if (자동매도Check.Checked == true && stockBalanceList[i].손익율 > -30)
                    {
                        double 손절율 = -2.5;
                        Double.TryParse(손절율TextBox.Text, out 손절율);

                        // stockBalanceList.열람값 은 잔고TR데이터 받아오면서 다 true로 되어있는 상태임. 한번 매도조건 만족했다면 매도창을 띄우고 다음5초가 찾아올때까지 창을 띄우지 않는다. 
                        // 사용자가 그 사이에 매도감시를 해제할수도 있다.

                        if (손절Check.Checked == true && stockBalanceList[i].손익율 < 손절율 && stockBalanceList[i].열람값 == true)
                        {
                            stockBalanceList[i].열람값 = false;
                            sell(stockBalanceList[i]);
                        }


                        double 트레일링기준 = 0.5;
                        Double.TryParse(트레일링기준TextBox.Text, out 트레일링기준);
                        double 트레일링익절율 = 0.5;
                        Double.TryParse(트레일링익절율TextBox.Text, out 트레일링익절율);

                        if (트레일링Check.Checked == true && stockBalanceList[i].손익율 > 트레일링기준 && stockDB[j].현재가 < stockBalanceList[i].분봉상저가최고가 && stockBalanceList[i].열람값 == true)
                        {
                            stockBalanceList[i].열람값 = false;
                            sell(stockBalanceList[i]);
                        }


                        double 이익보전조건 = 0.75;
                        Double.TryParse(이익보전조건TextBox.Text, out 이익보전조건);
                        double 이익보전율 = 0.5;
                        Double.TryParse(이익보전율TextBox.Text, out 이익보전율);

                        if (이익보전Check.Checked == true && stockBalanceList[i].최고손익율 > 이익보전조건 && stockBalanceList[i].손익율 < 이익보전율 && stockBalanceList[i].열람값 == true)
                        {
                            stockBalanceList[i].열람값 = false;
                            sell(stockBalanceList[i]);
                        }
                    }



                }
            }
        }

        public void onReceiveChejanData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEvent e) //이 프로그램을 통하지 않은 주문이라도, 사용자의 모든 주문에 반응하는 이벤트함수
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
            if (e.sRQName == "매수" || e.sRQName == "매도")
            {
                AutoClosingMessageBox.Show(e.sScrNo + ";" + e.sRQName + ";" + e.sTrCode + ";" + e.sMsg, "알림", 2000);
                addToLogBox(": " + e.sTrCode + e.sMsg);
            }
        } //매매발생시 제대로 체결되었는지 알려주는 이벤트함수

        //  --------------------------------------------------------------------------    메소드    -------------------------------------------------------------------------------//


        private void sell(accountInfo account)
        {
            if (passwordBox.TextLength == 4 && accountBox.Text.Length > 0)
            {
                string 종목코드 = account.stockCode;
                int 보유수량 = account.보유수량;

                playSimpleSound();

                if (yesModeCheck.Checked == true)
                    axKHOpenAPI1.SendOrder("매도", "9000", accountBox.Text, 2, 종목코드, 보유수량, 0, "03", "");

                else if (MessageBox.Show(account.stockName + " 종목을 매도하시겠습니까? \n 현재손익율은 " + account.손익율 + "입니다", "매도 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    axKHOpenAPI1.SendOrder("매도", "9000", accountBox.Text, 2, 종목코드, 보유수량, 0, "03", "");

            }
            else
                MessageBox.Show("자동 매도 실패. \n 필요한 정보를 입력해주세요.");
        }
        public void 잔고요청()  //api에 잔고TR데이터를 요청
        {
            if (passwordBox.TextLength == 4 && accountBox.Text.Length > 0)
            {
                string accountNum = accountBox.Text;
                string password = passwordBox.Text;

                axKHOpenAPI1.SetInputValue("계좌번호", accountNum);
                axKHOpenAPI1.SetInputValue("비밀번호", password);
                axKHOpenAPI1.SetInputValue("비밀번호입력매체구분", "00");
                axKHOpenAPI1.SetInputValue("조회구분", "1");

                axKHOpenAPI1.CommRqData("잔고현황요청", "opw00004", 0, "0004");
            }
        }

        public void 분봉조회(string stockCode)
        {
            axKHOpenAPI1.SetInputValue("종목코드", stockCode);
            axKHOpenAPI1.SetInputValue("틱범위", "1");
            axKHOpenAPI1.SetInputValue("수정주가구분", "0"); //수정주가구분

            axKHOpenAPI1.CommRqData(stockCode, "opt10080", 0, (5100 + scrNum_TR).ToString());

            addToLogBox(": 1분봉데이터를 요청합니다. 종목코드:" + stockCode + "화면번호 : " + (5100 + scrNum_TR));
            scrNum_TR++;
        }

        public void 분봉조회_30(string stockCode)
        {
            axKHOpenAPI1.SetInputValue("종목코드", stockCode);
            axKHOpenAPI1.SetInputValue("틱범위", "30");
            axKHOpenAPI1.SetInputValue("수정주가구분", "0"); //수정주가구분

            axKHOpenAPI1.CommRqData(stockCode, "opt10080", 0, (5100 + scrNum_TR).ToString());

            addToLogBox(": 30분봉데이터를 요청합니다. 종목코드:" + stockCode + "화면번호 : " + (5100 + scrNum_TR));
            scrNum_TR++;
        }

        public void 일봉조회(string stockCode)
        {
            axKHOpenAPI1.SetInputValue("종목코드", stockCode);
            axKHOpenAPI1.SetInputValue("기준일자", "1");
            axKHOpenAPI1.SetInputValue("수정주가구분", "0"); //수정주가구분

            axKHOpenAPI1.CommRqData(stockCode, "opt10081", 0, (5100 + scrNum_TR).ToString());

            addToLogBox(": 일봉데이터를 요청합니다. 종목코드:" + stockCode + "화면번호 : " + (5100 + scrNum_TR));
            scrNum_TR++;
        }

        public void renewal()
        {
            for (int i = 0; i < stockDB.Count; i++)
            {
                int h = stockDB[i].HLine;
                int c = stockDB[i].현재가;


                if (h == 9999999)
                {
                    stockDB[i].location = 4;
                }
                else if (h == 0)
                {

                }
                else
                {
                    switch (c)
                    {
                        case int n when (n < h):
                            stockDB[i].location = -1;
                            break;

                        case int n when ((n >= h) && (n <= h * 1.005)):
                            stockDB[i].location = 1;
                            break;

                        case int n when ((n > h * 1.008) && (n <= h * 1.013)):
                            stockDB[i].location = 2;
                            break;

                        case int n when ((n > h * 1.018) && (n <= h * 1.0245)):
                            stockDB[i].location = 3;
                            break;
                    }
                }

            }

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();

            for (int i = 0; i < stockDB.Count; i++)
            {
                if (체결분석율checkBox.Checked == true)
                {
                    if ((stockDB[i].하락방어율 > 1 || stockDB[i].상승확률 > 1))
                    {
                        switch (stockDB[i].location)
                        {
                            case 1:
                                listBox1.Items.Add(stockDB[i].stockName + "; " + stockDB[i].하락방어율.ToString() + " ; " + stockDB[i].상승확률.ToString(), stockDB[i].check);
                                break;

                            case 2:
                                listBox2.Items.Add(stockDB[i].stockName + "; " + stockDB[i].하락방어율.ToString() + " ; " + stockDB[i].상승확률.ToString(), stockDB[i].check);
                                break;

                            case 3:
                                listBox3.Items.Add(stockDB[i].stockName + "; " + stockDB[i].하락방어율.ToString() + " ; " + stockDB[i].상승확률.ToString(), stockDB[i].check);
                                break;
                            case 4:
                                listBox4.Items.Add(stockDB[i].stockName);
                                break;

                        }
                    }
                }
                else
                {
                    switch (stockDB[i].location)
                    {
                        case 1:
                            listBox1.Items.Add(stockDB[i].stockName + "; " + stockDB[i].하락방어율.ToString() + " ; " + stockDB[i].상승확률.ToString(), stockDB[i].check);
                            break;

                        case 2:
                            listBox2.Items.Add(stockDB[i].stockName + "; " + stockDB[i].하락방어율.ToString() + " ; " + stockDB[i].상승확률.ToString(), stockDB[i].check);
                            break;

                        case 3:
                            listBox3.Items.Add(stockDB[i].stockName + "; " + stockDB[i].하락방어율.ToString() + " ; " + stockDB[i].상승확률.ToString(), stockDB[i].check);
                            break;
                        case 4:
                            listBox4.Items.Add(stockDB[i].stockName);
                            break;
                    }
                }

            }
        }

        public void Present()
        {
            PriceLogListBox.Items.Clear();

            int i = stockDB.FindIndex(o => o.stockName == selectedStockName);

            if (i >= 0)
            {
                PriceLogListBox.Items.Add("종목코드 :" + stockDB[i].stockCode);
                PriceLogListBox.Items.Add("현재가 :" + stockDB[i].현재가);
                PriceLogListBox.Items.Add("location :" + stockDB[i].location);
                PriceLogListBox.Items.Add("체결시간 :" + stockDB[i].체결시간);
                PriceLogListBox.Items.Add("hLine :" + stockDB[i].HLine);

                for (int j = 0; j <= stockDB[i].종가.Count - 1; j++)
                {
                    PriceLogListBox.Items.Add(j + "번째 : (종)" + stockDB[i].종가[j] + " (시)" + stockDB[i].시가[j] + " (저)" + stockDB[i].저가[j] + " (고)" + stockDB[i].고가[j] + "(거래량)" + stockDB[i].거래량[j]);
                }
            }
        }

        public void playSimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\Speech On.wav");
            simpleSound.Play();
        }

        private void addToLogBox(string log)
        {
            LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + log);
        }


        private static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }

        //  --------------------------------------------------------------------------   객체 이벤트함수-------------------------------------------------------------------------------//
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if(MessageBox.Show("정말종료합니까?","종료",MessageBoxButtons.YesNo)==DialogResult.No)
            //{
            //   e.Cancel = true;
            //}

            //LogListBox에 있는 내용을 txt파일로 저장하여 올린다.
            StreamWriter writer;
            string filePath = "C:\\Users\\fenton\\Desktop\\@주식\\@스톡어드바이저\\" + DateTime.Now.ToString("MMdd_HHmm_") + "Log.txt";

            //해당 경로에 text화일을 형성후에 내부내용을 작성한다.
            //  writer = File.CreateText(strFilePath);
            System.IO.File.WriteAllText(filePath, "", Encoding.Default);

            Stream stream1 = new FileStream(filePath, FileMode.Truncate);

            writer = new StreamWriter(stream1, Encoding.Default);

            //UTF-8 Encoding의 문자열을 ANSI Encoding으로 변환

            for (int i = 0; i < LogListBox.Items.Count; i++)
            {
                byte[] a = System.Text.Encoding.Default.GetBytes(LogListBox.Items[i].ToString());
                string a1 = Encoding.Default.GetString(a);

                writer.WriteLine(a1);
            }

            writer.Close();

            //확장자명을 바꾼다.
            //string result = Path.ChangeExtension(filePath, ".csv");
            //System.IO.File.Move(filePath, result);

            //MessageBox.Show("Log파일 텍스트 파일 출력완료.");
        }

        private void renewalButton_Click(object sender, EventArgs e)
        {
            renewal();
        }

        //listBox에 있는 종목들을 일괄 체크하기 편하도록 만들어놓은 버튼이다.
        private void checkButtonClicked(object sender, EventArgs e)
        {
            bool chk;
            string searchName;
            if (checkCnt[0] % 2 == 0)
                chk = true;
            else
                chk = false;

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                listBox1.SetItemChecked(i, chk);
                searchName = listBox1.Items[i].ToString().Split(';')[0];
                stockDB.Find(o => o.stockName == searchName).check = chk;
            }
            checkCnt[0]++;
        }
        private void checkButton2_Click(object sender, EventArgs e)
        {
            bool chk;
            string searchName;

            if (checkCnt[1] % 2 == 0)
                chk = true;
            else
                chk = false;

            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                listBox2.SetItemChecked(i, chk);
                searchName = listBox2.Items[i].ToString().Split(';')[0];
                stockDB.Find(o => o.stockName == searchName).check = chk;
            }
            checkCnt[1]++;
        }
        private void checkButton3_Click(object sender, EventArgs e)
        {
            string searchName;
            bool chk;

            if (checkCnt[2] % 2 == 0)
                chk = true;
            else
                chk = false;

            for (int i = 0; i < listBox3.Items.Count; i++)
            {
                listBox3.SetItemChecked(i, chk);
                searchName = listBox3.Items[i].ToString().Split(';')[0];
                stockDB.Find(o => o.stockName == searchName).check = chk;
            }
            checkCnt[2]++;
        }

        //listBox에서 종목을 선택하면, 선택된 종목 label에 반영하고, 종목분석창에 분봉내역을 표시하며, stockDB의 체크여부를 표시해준다.
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
                    stockDB.Find(o => o.stockName == selectedStockName).check = true;
                else
                    stockDB.Find(o => o.stockName == selectedStockName).check = false;

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

                int i = stockDB.FindIndex(o => o.stockName == selectedStockName);

                if (listBox2.GetItemChecked(listBox2.SelectedIndex) == true)
                    stockDB[i].check = true;
                else
                    stockDB[i].check = false;

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
                    stockDB.Find(o => o.stockName == selectedStockName).check = true;
                else
                    stockDB.Find(o => o.stockName == selectedStockName).check = false;

                Present();
            }
        }

        //listBox에서 종목을 지우는 메서드이다.
        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            string searchName;
            int i = listBox1.SelectedIndex;
            if (i >= 0)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    searchName = listBox1.Items[i].ToString().Split(';')[0];

                    listBox1.Items.RemoveAt(i);
                    listBox4.Items.Add(searchName);

                    stockDB.Find(o => o.stockName == searchName).HLine = 9999999;
                }

                if (e.KeyCode == Keys.C)
                {
                    searchName = listBox1.Items[i].ToString().Split(';')[0];
                    stockDB.Find(o => o.stockName == searchName).check = true;
                    listBox1.SetItemChecked(i, true);
                }
            }
        }
        private void listBox2_KeyDown(object sender, KeyEventArgs e)
        {


            string searchName;
            int i = listBox2.SelectedIndex;
            if (i >= 0)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    searchName = listBox2.Items[i].ToString().Split(';')[0];

                    listBox2.Items.RemoveAt(i);
                    listBox4.Items.Add(searchName);

                    stockDB.Find(o => o.stockName == searchName).HLine = 9999999;
                }
                if (e.KeyCode == Keys.C)
                {
                    searchName = listBox2.Items[i].ToString().Split(';')[0];
                    stockDB.Find(o => o.stockName == searchName).check = true;
                }
            }
        }

        private void listBox3_KeyDown(object sender, KeyEventArgs e)
        {
            string searchName;
            int i = listBox3.SelectedIndex;
            if (i >= 0)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    searchName = listBox3.Items[i].ToString().Split(';')[0];

                    listBox3.Items.RemoveAt(i);
                    listBox4.Items.Add(searchName);

                    stockDB.Find(o => o.stockName == searchName).HLine = 9999999;
                }
                if (e.KeyCode == Keys.C)
                {
                    searchName = listBox3.Items[i].ToString().Split(';')[0];
                    stockDB.Find(o => o.stockName == searchName).check = true;
                }
            }
        }

        private void dataGridViewkey(object sender, KeyPressEventArgs e) //체결분석 데이터그리드뷰에서 키보드키를 누르면 색상을 바꿔서 포커스하기 편하게 해준다. 
        {
            int i = dataGridView.CurrentCell.RowIndex;

            if (dataGridView.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
                dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;
            else
                dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
        }

        private void balanceDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) //보유종목 잔고에서 종목선택시 라벨에 종목을 표시해 포커스를 두어 나중에 매도할 준비를 한다..
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
                int i = stockDB.FindIndex(o => o.stockName == searchNameLabel2.Text);
                string 종목코드 = stockDB[i].stockCode;
                int 보유수량 = stockBalanceList.Find(o => o.stockName == searchNameLabel2.Text).보유수량;

                axKHOpenAPI1.SendOrder("매도", "9000", accountBox.Text, 2, 종목코드, 보유수량, 0, "03", "");
            }
            else
                MessageBox.Show("필요한 정보를 입력해주세요.");

        }

        private void buyButton_Click(object sender, EventArgs e)
        {
            if (passwordBox.TextLength == 4 && accountBox.Text.Length > 0 && searchNameLabel1.Text.Length > 0 && stockQtyBox.TextLength > 0)
            {
                int i = stockDB.FindIndex(o => o.stockName == searchNameLabel1.Text);
                string 종목코드 = stockDB[i].stockCode;

                axKHOpenAPI1.SendOrder("매수", "8249", accountBox.Text, 1, 종목코드, int.Parse(stockQtyBox.Text), 0, "03", "");
            }
            else
                MessageBox.Show("필요한 정보를 입력해주세요.");
        }

        private void DBUpdateButton_Click(object sender, EventArgs e)
        {
            listBox0.Items.Clear();
            for (int i = 0; i < stockDB.Count; i++)
            {
                listBox0.Items.Add(stockDB[i].stockName);
            }
        }


        // -------------------------------- 버려진 함수들 ----------------------------------------------

        private void searchButton_Click(object sender, EventArgs e) //사용자가 선택한 조건을 서버에 송신해서 종목들을 받아온다.
        {
            if (searchBox.Text.Length > 0)
            {
                //기존종목유지 체크표시가 해제되어있다면, 화면번호 및 데이터를 리셋한다.
                if (addTogetherCheckBox.Checked == false)
                {
                    axKHOpenAPI1.SetRealRemove("ALL", "ALL");

                    for (int i = 0; i < requestConditonList.Count; i++)
                    {
                        axKHOpenAPI1.SendConditionStop(requestConditonList[i].화면번호, requestConditonList[i].검색명, requestConditonList[i].검색인덱스);
                    }

                    for (int i = 0; i <= scrNum_조건검색; i++)
                    {
                        axKHOpenAPI1.DisconnectRealData((5000 + scrNum_TR).ToString());
                        scrNum_조건검색 = 0;
                    }

                    for (int i = 0; i <= scrNum_TR; i++)
                    {
                        axKHOpenAPI1.DisconnectRealData((5100 + scrNum_TR).ToString());
                        scrNum_TR = 0;
                    }

                    for (int i = 0; i <= scrNum_Real; i++)
                    {
                        axKHOpenAPI1.DisconnectRealData((6000 + scrNum_Real).ToString());
                        scrNum_Real = 0;
                    }

                    //사용자 UI에 표시된 것들을 지운다.
                    dataGridView.Rows.Clear();
                    listBox0.Items.Clear(); // 참고로 listBox1/2/3는 stockDB를 실시간으로 반영하므로 지우지 않아도 된다.

                    //내부 데이터를 지운다.
                    stockDB.Clear();
                    requestConditonList.Clear();
                }

                //나중에 조건검색 해지를 위해 requestConditon
                requestConditonList.Insert(0, new requestConditonInfo((5000 + scrNum_조건검색).ToString(), searchBox.Text.Split('^')[1], Convert.ToInt32(searchBox.Text.Split('^')[0])));
                axKHOpenAPI1.SendCondition(requestConditonList[0].화면번호, requestConditonList[0].검색명, requestConditonList[0].검색인덱스, 1);
                scrNum_조건검색++;

                addToLogBox(": ## 조건검색 송신요청합니다. 화면번호" + (scrNum_조건검색 + 5000));
            }
        }



    }



}
