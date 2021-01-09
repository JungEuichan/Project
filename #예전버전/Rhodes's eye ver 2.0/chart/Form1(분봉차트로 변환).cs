using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;


namespace chart
{
    public partial class Form1 : Form
    {
        List<stockInfo> searchStock = new List<stockInfo>(); //stockInfo값을 가지는 리스트를 선언함. 리스트 선언은 이렇게 알고 있으면 될것.
        
        string[] searchList;
        string selectedStockName;
        int tick = 0;
        public static int scrNum1 = 0; // 5000대 : 조건검색 결과 반환요청에 쓰임.
        public static int scrNum2 = 0; // 5100대 : Tr데이터 요청에 쓰임.
        public static int scrNum3 = 0; // 6000대 : Real데이터 요청에 쓰임.


        List<searchedList> requestList = new List<searchedList>();
        AutoCompleteStringCollection autoList = new AutoCompleteStringCollection();

        public Form1()
        {
            InitializeComponent();

            axKHOpenAPI1.CommConnect(); //로그인

            axKHOpenAPI1.OnEventConnect += onEventConnect; //로그인 이벤트 함수
            axKHOpenAPI1.OnReceiveTrData += onReceiveTrData; //TR데이터요청 이벤트 함수
            axKHOpenAPI1.OnReceiveRealData += onReceiveRealData; 

            axKHOpenAPI1.OnReceiveConditionVer += onReceiveConditonVer;
            axKHOpenAPI1.OnReceiveTrCondition += onReceiveTrCondition;

            listBox1.KeyDown += listBox1_KeyDown;
            listBox2.KeyDown += listBox2_KeyDown;
            listBox3.KeyDown += listBox3_KeyDown;
        }

        public void onEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e) //로그인 이벤트 함수
        {
            if (e.nErrCode == 0)
            {
                axKHOpenAPI1.GetConditionLoad(); //조건식 요청
                LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + ": ## 로그인 성공. 조건식 요청");

                //계좌번호 목록을 불러온다.
                string[] accountList = axKHOpenAPI1.GetLoginInfo("ACCLIST").Split(';');

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
                int nIndex = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);
                
                long v = 0;
                int h = 0;
                int l = 0;
                int close = 0;
                int open = 0;
                searchStock[i].priceNow(Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "현재가").Trim())));
                
                for (int j = nIndex-1; j >= 0; j--)
                {
                    
                    v = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "거래량").Trim()));
                        h = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "고가").Trim()));
                        l = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "저가").Trim()));
                        close = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "현재가").Trim()));
                        open = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "시가").Trim()));

                        searchStock[i].priceAdd(h, l, open, close, v);

                }
            }
        }

        public void onReceiveRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent e) //실시간데이터 요청이벤트 함수 
        {

        }


        //  --------------------------------------------------------------------------메소드 및 form 객체 이벤트함수-------------------------------------------------------------------------------//


        public void 일봉조회(string i)
        {
            
            axKHOpenAPI1.SetInputValue("종목코드", i);
            axKHOpenAPI1.SetInputValue("기준일자", DateTime.Now.ToString("yyyyMMdd"));
            axKHOpenAPI1.SetInputValue("수정주가구분", "1"); //수정주가구분

            axKHOpenAPI1.CommRqData(i, "opt10081", 0, (5100+scrNum2).ToString());

            LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + ": 일봉데이터를 요청합니다. 종목명:" + i + "화면번호 : " + (8300+scrNum2));

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

        private void resetButton_Click(object sender, EventArgs e) //searchStock 내용을 모두 밀어버리고 다시 갖고온다. + 예전 실시간검색을 끊어버리고 다시 조건검색을 요청한다.
        {
            reset();
        }

        private void renewalButton_Click(object sender, EventArgs e)
        {
            renewal();
        }

        public void renewal()
        {
            
            for (int i = 0; i < searchStock.Count; i++)
            {
                searchStock[i].lineReset();
                searchStock[i].lineCalc(int.Parse(candleBeforeBox.Text));
                searchStock[i].lineDistribute(int.Parse(candleBeforeBox.Text));
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

        public void reset()
        {
            axKHOpenAPI1.SetRealRemove("ALL", "ALL");
            axKHOpenAPI1.DisconnectRealData("ALL");

            searchStock.Clear();
            listBox0.Items.Clear();

            requestList.Insert(0, new searchedList((5000 + scrNum1).ToString(), searchAddComboBox.Text.Split('^')[1], Convert.ToInt32(searchAddComboBox.Text.Split('^')[0])));

            axKHOpenAPI1.SendCondition(requestList[0].화면번호, requestList[0].검색명, requestList[0].검색인덱스, 1);

            scrNum1++;
            LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + ": ## 조건검색 송신요청합니다. 화면번호" + (scrNum1 + 5000));
            timer1.Enabled = true;

        }

        public void Present()
        {
            PriceLogListBox.Items.Clear();

            int i = searchStock.FindIndex(o => o.stockName == selectedStockName);

            if (i >= 0)
            {
                PriceLogListBox.Items.Add("종목코드 :" + searchStock[i].stockCode);
                PriceLogListBox.Items.Add("hLine20 :" + searchStock[i].HLine20);
                PriceLogListBox.Items.Add("lLine20 :" + searchStock[i].lLine20);

                PriceLogListBox.Items.Add("hLine50 :" + searchStock[i].HLine50);
                PriceLogListBox.Items.Add("lLine50 :" + searchStock[i].lLine50);


                PriceLogListBox.Items.Add("고가_120 :" + searchStock[i].고가_120);
                PriceLogListBox.Items.Add("저가_120 :" + searchStock[i].저가_120);

                PriceLogListBox.Items.Add("고가라인 :" + searchStock[i].고가라인);
                PriceLogListBox.Items.Add("저가라인 :" + searchStock[i].저가라인);

                for (int j = 0; j < searchStock[i].종가.Count - 1; j++)
                {
                    PriceLogListBox.Items.Add(j + "번째 : (종)" + searchStock[i].종가[j] + " (시)" + searchStock[i].시가[j] + " (저)" + searchStock[i].저가[j] + " (고)" + searchStock[i].고가[j] + "(거래량)" + searchStock[i].거래량[j]);
                }
            }
        }


        private void listBox0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox0.SelectedIndex >= 0)
            {
                selectedStockName = listBox0.SelectedItem.ToString();
                searchNameLabel.Text = "선택된 종목:" + selectedStockName;

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

        }
        private void listBox2_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void listBox3_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void outputButton_Click(object sender, EventArgs e)
        {
            StreamWriter writer;
            string filePath = "C:\\KiwoomHero4\\temp\\" + DateTime.Now.ToString("MMddHHmmss.") + "탈출봉.txt" ;

            //해당 경로에 text화일을 형성후에 내부내용을 작성한다.
            //  writer = File.CreateText(strFilePath);
            System.IO.File.WriteAllText(filePath, "", Encoding.Default);

            Stream stream1 = new FileStream(filePath, FileMode.Truncate);

            writer = new StreamWriter(stream1, Encoding.Default);
            writer.WriteLine("종목명,종목코드", Encoding.Default);


            //UTF-8 Encoding의 문자열을 ANSI Encoding으로 변환
            
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string stockName = listBox1.Items[i].ToString();
                string stockCode = searchStock.Find(o => o.stockName == stockName).stockCode;

                byte[] a = System.Text.Encoding.Default.GetBytes(stockName);
                string a1 = Encoding.Default.GetString(a);

                byte[] b = System.Text.Encoding.Default.GetBytes(stockCode);
                string b1 = Encoding.Default.GetString(b);
                writer.WriteLine(a1 + "," + b1);
           
            }

            writer.WriteLine("", Encoding.Default);

            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                string stockName = listBox2.Items[i].ToString();
                string stockCode = searchStock.Find(o => o.stockName == stockName).stockCode;

                byte[] a = System.Text.Encoding.Default.GetBytes(stockName);
                string a1 = Encoding.Default.GetString(a);

                byte[] b = System.Text.Encoding.Default.GetBytes(stockCode);
                string b1 = Encoding.Default.GetString(b);
                writer.WriteLine(a1 + "," + b1);
            }

            writer.WriteLine("", Encoding.Default);

            for (int i = 0; i < listBox3.Items.Count; i++)
            {
                string stockName = listBox3.Items[i].ToString();
                string stockCode = searchStock.Find(o => o.stockName == stockName).stockCode;

                byte[] a = System.Text.Encoding.Default.GetBytes(stockName);
                string a1 = Encoding.Default.GetString(a);

                byte[] b = System.Text.Encoding.Default.GetBytes(stockCode);
                string b1 = Encoding.Default.GetString(b);
                writer.WriteLine(a1 + "," + b1);
            }

            writer.WriteLine("", Encoding.Default);

            for (int i = 0; i < listBox4.Items.Count; i++)
            {
                string stockName = listBox4.Items[i].ToString();
                string stockCode = searchStock.Find(o => o.stockName == stockName).stockCode;

                byte[] a = System.Text.Encoding.Default.GetBytes(stockName);
                string a1 = Encoding.Default.GetString(a);

                byte[] b = System.Text.Encoding.Default.GetBytes(stockCode);
                string b1 = Encoding.Default.GetString(b);
                writer.WriteLine(a1 + "," + b1);
            }


            writer.Close();

            //확장자명을 바꾼다.
            string result = Path.ChangeExtension(filePath, ".csv");
            System.IO.File.Move(filePath, result);

            MessageBox.Show("텍스트 파일 출력완료.");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tick < searchList.Length)
            {
                string searchName = axKHOpenAPI1.GetMasterCodeName(searchList[tick]);
                searchStock.Add(new stockInfo(searchList[tick], searchName));
                listBox0.Items.Add(searchName);

                일봉조회(searchList[tick]);

                LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + "일봉조회를 요청합니다." + searchList[tick]);

                tick++;
            }
            else if (tick >= searchList.Length)
            {
                timer1.Enabled = false;
                playSimpleSound();
                MessageBox.Show("검색결과 전부 송신완료했습니다. \n받아온 종목수는 " + tick + "개 입니다.");
                tick = 0;
            }
        }

        public void playSimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\Speech On.wav");
            simpleSound.Play();
        }
    }
}
