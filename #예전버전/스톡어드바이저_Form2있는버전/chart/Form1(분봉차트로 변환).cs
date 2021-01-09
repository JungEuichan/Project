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
using System.Drawing;
using System.Runtime.CompilerServices;

namespace chart
{
    public partial class Form1 : Form
    {
        //실시간 + TR 데이터베이스
        List<stockInfo> searchStock = new List<stockInfo>(); //stockInfo값을 가지는 리스트를 선언함. 리스트 선언은 이렇게 알고 있으면 될것.
        public List<realInfo> realData = new List<realInfo>();

        //메서드 여러개를 거치면서 임시적으로 쓸 변수
        string searchName;
        string[] searchList;
        string selectedStockName;

        //서버와 송수신할 때 쓰일 화면번호 변수
        public static int scrNum1 = 0; // 5000대 : 조건검색 결과 반환요청에 쓰임.
        public static int scrNum2 = 0; // 5100대 : Tr데이터 요청에 쓰임.
        public static int scrNum3 = 0; // 6000대 : Real데이터 요청에 쓰임.

        //Form 새로띄울때 쓸 변수
        Form2 Form2;
        int formNum = 0;

        //타이머 변수
        int tick = 0;
        int renewalTick = 0;

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
            LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + ": Tr데이터가 요청되었습니다. 종목명:" + e.sRQName);
           
            int i = searchStock.FindIndex(o => o.stockCode == e.sRQName);
            if (i >= 0)
            {
                    if(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "거래량").Trim())>0)
                    {
                    long v = 0;
                    int h = 0;
                    int l = 0;
                    int close = 0;
                    int open = 0;

                    searchStock[i].priceNow(Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "현재가").Trim())));


                    for (int j = 399; j >= 1; j--)
                    {
                        v = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "거래량").Trim()));
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

                    v = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "거래량").Trim()));
                    h = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "고가").Trim()));
                    l = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "저가").Trim()));
                    close = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "현재가").Trim()));
                    open = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "시가").Trim()));

                    searchStock[i].priceAdd(h, l, open, close, v);

                    axKHOpenAPI1.SetRealReg((6000 + scrNum3).ToString(), e.sRQName, "20;10;13", "1");
                    scrNum3++;

                }



                    
            }
        }

        public void onReceiveRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent e) //실시간데이터 요청이벤트 함수 
        {


            if (e.sRealType == "주식체결")
            {
                int j = realData.FindIndex(o => o.stockCode == e.sRealKey);
                int i = searchStock.FindIndex(o => o.stockCode == e.sRealKey);

                if (j >= 0)
                {
                     realData[j].체결update(int.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 15).Trim()), Math.Abs(int.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 10).Trim())));
                }

                else if (i >= 0)
                {
                    searchStock[i].minuteData(int.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 20).Trim()), Math.Abs(int.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 10).Trim())), int.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 13).Trim()));
                }
            }
            if (e.sRealType == "주식호가잔량")
            {
                int j = realData.FindIndex(o => o.stockCode == e.sRealKey);
                if (j >= 0)
                {
                    realData[j].호가update(int.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 21).Trim()), int.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 91).Trim()));
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
                }
            }

            else if (e.strType == "D") //종목 이탈
            {
                searchName = axKHOpenAPI1.GetMasterCodeName(e.sTrCode);
                LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + ": 기존종목이탈 (" + searchName + ")");
            }
        }


        //  --------------------------------------------------------------------------메소드 및 form 객체 이벤트함수-------------------------------------------------------------------------------//


        public void 분봉조회(string i)
        {
            axKHOpenAPI1.SetInputValue("종목코드", i);
            axKHOpenAPI1.SetInputValue("틱범위", "1");
            axKHOpenAPI1.SetInputValue("수정주가구분", "0"); //수정주가구분

            axKHOpenAPI1.CommRqData(i, "opt10080", 0, (5100+scrNum2).ToString());

            LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + ": 분봉데이터를 요청합니다. 종목명:" + i + "화면번호 : " + (5100+scrNum2));
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
                else if(h == 0)
                {
                    
                }
                else
                {
                    switch (c)
                    {
                        case int n when ((n>=h) && (n<=h*1.003)):
                            searchStock[i].location = 1;
                            break;

                        case int n when ((n>h*1.008) && (n<=h*1.013)):
                            searchStock[i].location = 2;
                            break;

                        case int n when ((n>h*1.018) && (n<=h*1.0245)):                            
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

                switch (searchStock[i].location)
                {
                    case 1:
                        listBox1.Items.Add(searchStock[i].stockName, searchStock[i].check);
                        break;

                    case 2:
                        listBox2.Items.Add(searchStock[i].stockName, searchStock[i].check);
                        if (searchStock[i].check == false)
                            playSimpleSound();
                        break;

                    case 3:
                        listBox3.Items.Add(searchStock[i].stockName, searchStock[i].check);
                        break;
                    case 4:
                        listBox4.Items.Add(searchStock[i].stockName);
                        break;
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



        private void renewalButton_Click(object sender, EventArgs e)
        {
            renewal();
        }

        private void newFormButton_Click(object sender, EventArgs e)
        {
            Form2 = new Form2(this, formNum);
            Form2.Show();
            formNum++;
            realData.Add(new realInfo());
        }

        private void searchAddButton_Click(object sender, EventArgs e)
        {
            if (addTogetherCheckBox.Checked == false)
            {
                axKHOpenAPI1.SetRealRemove("ALL", "ALL");
                axKHOpenAPI1.DisconnectRealData("6000");

                searchStock.Clear();
            }
            axKHOpenAPI1.SendCondition((5000 + scrNum1).ToString(), searchAddComboBox.Text.Split('^')[1], Convert.ToInt32(searchAddComboBox.Text.Split('^')[0]), 1);
            LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + ": ## 조건검색 송신요청합니다. 화면번호" + (scrNum1 + 5000));

            scrNum1++;
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
                searchName = listBox1.Items[i].ToString();
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
                searchName = listBox2.Items[i].ToString();
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
                searchName = listBox3.Items[i].ToString();
                searchStock.Find(o => o.stockName == searchName).check = chk;
            }
            checkCnt[2]++;
        }
        private void timer1_Tick_1(object sender, EventArgs e) //3초마다 확인할 것으로, 조건검색종목이 바뀜에 따라 조건검색리스트를 업데이트하는 역할을 함.
        {

            if(searchAddCheck.Checked == true)
            {
                if (renewalTick == 4)
                {
                    renewalTick = 0;
                    listBox0.Items.Clear();

                    for (int i = 0; i < searchStock.Count; i++)
                        listBox0.Items.Add(searchStock[i].stockName);

                    if (checkBox1.Checked == true)
                        renewal();
                }
                renewalTick++;
            }

            

            if (tick < searchList.Length)
            {
                string searchName = axKHOpenAPI1.GetMasterCodeName(searchList[tick]);

                if(searchStock.FindIndex(o => o.stockName == searchName) < 0)
                    searchStock.Add(new stockInfo(searchList[tick], searchName));
                
                분봉조회(searchList[tick]);

                LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + "분봉조회를 요청합니다." + searchList[tick]);

                tick++;
            }
            else if (tick == searchList.Length)
            {
                int i = tick;
                tick = 9999;
                MessageBox.Show("검색결과 전부 송신완료했습니다. \n받아온 종목수는 " + (i + 1) + "개 입니다.");
            }
        }

        private void listBox0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox0.SelectedIndex >= 0)
            {
                selectedStockName = listBox0.SelectedItem.ToString();
                searchNameLabel.Text = "선택된 종목:" + selectedStockName;
                searchNameLabel1.Text = "선택된 종목:" + selectedStockName;
                Present();
            }
        }
        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                selectedStockName = listBox1.SelectedItem.ToString();
                searchNameLabel.Text = "선택된 종목:" + selectedStockName;

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
                selectedStockName = listBox2.SelectedItem.ToString();
                searchNameLabel.Text = "선택된 종목:" + selectedStockName;

                if(listBox2.GetItemChecked(listBox2.SelectedIndex) == true)
                    searchStock.Find(o => o.stockName == selectedStockName).check = true;
                else
                    searchStock.Find(o => o.stockName == selectedStockName).check = false;

                    Present();
            }
        }
        private void listBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex >= 0)
            {
                selectedStockName = listBox3.SelectedItem.ToString();
                searchNameLabel.Text = "선택된 종목:" + selectedStockName;

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
            if(i >= 0)
            {
                if(e.KeyCode == Keys.Delete)
                {
                    searchName = listBox1.Items[i].ToString();

                    listBox1.Items.RemoveAt(i);
                    listBox4.Items.Add(searchName);

                    searchStock.Find(o => o.stockName == searchName).HLine = 9999999;
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
                    searchName = listBox2.Items[i].ToString();

                    listBox2.Items.RemoveAt(i);
                    listBox4.Items.Add(searchName);

                    searchStock.Find(o => o.stockName == searchName).HLine = 9999999;
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
                    searchName = listBox3.Items[i].ToString();

                    listBox3.Items.RemoveAt(i);
                    listBox4.Items.Add(searchName);

                    searchStock.Find(o => o.stockName == searchName).HLine = 9999999;
                }
            }
        }


        // ---------------------------------------------------- Form2와 통신하기 위한 메서드 ------------------------------------------------------------------

        public string GetMasterCodeName(string Code)
        {
            return axKHOpenAPI1.GetMasterCodeName(Code);
        }

       public void SetRealReg(int scrNum,string code, string inputs, string exclusive)
        {
            axKHOpenAPI1.SetRealReg((7000 + scrNum).ToString(), code, inputs, exclusive); //20;10;15;
        }

        public void Disconnect(int scrNum)
        {
            axKHOpenAPI1.SetRealRemove((scrNum+7000).ToString(), "All");
            axKHOpenAPI1.DisconnectRealData((scrNum + 7000).ToString());
        }


        private void dataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
