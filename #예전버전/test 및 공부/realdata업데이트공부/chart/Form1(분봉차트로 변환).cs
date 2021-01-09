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



namespace chart
{
    public partial class Form1 : Form
    {
        List<stockInfo> searchStock = new List<stockInfo>(); //stockInfo값을 가지는 리스트를 선언함. 리스트 선언은 이렇게 알고 있으면 될것.
  
        realInfo realdata = new realInfo("0");

        string searchName;
        string[] searchList;
        string selectedStockName;

        int scrNum=0;
        int scrNum1 = 0;
        int scrNum3 = 0;

        public Form1()
        {
            InitializeComponent();


            axKHOpenAPI1.CommConnect(); //로그인

            axKHOpenAPI1.OnEventConnect += onEventConnect; //로그인 이벤트 함수
            axKHOpenAPI1.OnReceiveTrData += onReceiveTrData; //TR데이터요청 이벤트 함수
            axKHOpenAPI1.OnReceiveRealData += onReceiveRealData; 


        }

        public void onEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e) //로그인 이벤트 함수
        {
            if (e.nErrCode == 0)
            {

            }
        }


        public void onReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e) //TR데이터 요청 이벤트 함수
        {
            LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + ": Tr데이터가 요청되었습니다. 종목명:" + e.sRQName);
           
            int i = searchStock.FindIndex(o => o.stockCode == e.sRQName);
            if (i >= 0)
            {
                    if(true)
                {
                    long v = 0;
                    int h = 0;
                    int l = 0;
                    int close = 0;
                    int open = 0;

                    searchStock[i].priceNow(Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "현재가").Trim())));

                    for (int j = 389; j >= 0; j--)
                    {
                        v = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "거래량").Trim()));
                        h = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "고가").Trim()));
                        l = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "저가").Trim()));
                        close = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "현재가").Trim()));
                        open = Math.Abs(int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, j, "시가").Trim()));

                        searchStock[i].priceAdd(h, l, open, close, v);
                    }

                    searchStock[i].체결시간 = Convert.ToInt32(DateTime.Now.ToString("HHmm"));

                    Present();

                    axKHOpenAPI1.SetRealReg((6000 + scrNum3).ToString(),searchBox.Text,"20;10;15;16;17;18;13", "0");
                    scrNum3++;
                }
            }
        }

        public void onReceiveRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent e) //실시간데이터 요청이벤트 함수 
        {

            /* 1. 체결시간을 받아온다.
            2. 그 종목의 체결시간과 얼마나 차이나는지 구하고, 1분단위로 같다면, 그 종목의 종가를 update한다. 거래량은 더해준다. 고가/저가도 업데이트해준다.
            3. 그 종목의 체결시간과 1분차이난다면, 시가/고가/저가 거래량을 추가해준다. 체결시간을 맞춰준다. 
            4. 라인을 따로 구하기 위해서, sellsum과 vSellsum도 따로 클래스에 저장되어 있는상태여야 한다. 그값에 새로운 sellsum과 vSellsum을 더하고 나눠서 line 업데이트를 해준다. 
            단, line update는 너무 잦으면 시스템에 부하를 줄수 있기때문에, 체결시간과 1분차이날때 전 봉의 line만 체크하도록한다.
            
            5. 또한 setrealreg의 경우 최대 100개 까지 종목들을 동시에 조회할것으로 예상되기 때문에, 종목코드를 잘 대조해서 알아서 searchStock에 업데이트 해주는 구조로 만든다.
            20 체결시간 10 현재가 15거래량 
            */

            // setrealreg 리셋하는 구문도 필요하다.


            if (e.sRealType == "주식체결")
            {
                searchStock[0].minuteData(int.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 20).Trim()), Math.Abs(int.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 10).Trim())), int.Parse(axKHOpenAPI1.GetCommRealData(e.sRealKey, 13).Trim()));
            }

        }



        //  --------------------------------------------------------------------------메소드 및 form 객체 이벤트함수-------------------------------------------------------------------------------//

        private void searchButton_Click(object sender, EventArgs e)
        {
           
            if(searchBox.Text.Length>0)
            {
                string searchName = axKHOpenAPI1.GetMasterCodeName(searchBox.Text);

                if (searchName.Length > 1)
                {
                    searchStock.Add(new stockInfo(searchBox.Text, searchName));

                    분봉조회(searchBox.Text);
                    searchNameLabel.Text = "종목명 : " + searchName;
                }
            }
        }

        public void 분봉조회(string i)
        {
            
            axKHOpenAPI1.SetInputValue("종목코드", i);
            axKHOpenAPI1.SetInputValue("틱범위", "1");
            axKHOpenAPI1.SetInputValue("수정주가구분", "0"); //수정주가구분

            int e = axKHOpenAPI1.CommRqData(i, "opt10080", 0, (5100+scrNum).ToString());
            Delay(300);

            LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + ": 분봉데이터를 요청합니다. 종목명:" + i + "화면번호 : " + (5100+scrNum));
            if (e<0)
            {
                MessageBox.Show("시세과부화로 인한 에러입니다. 가격정보를 받지 못한 종목은 다음과 같습니다." + i);
            }
        }

        private static DateTime Delay(int MS)  // System.Threading.Thread.Sleep(210); 대신에 쓰이는것.
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


        public void Present()
        {
            PriceLogListBox.Items.Clear();

            int i = searchStock.FindIndex(o => o.stockCode == searchBox.Text);

            if (i >= 0)
            {

                PriceLogListBox.Items.Add("종목코드 :" + searchStock[i].stockCode);
                PriceLogListBox.Items.Add("현재가 :" + searchStock[i].현재가);
                PriceLogListBox.Items.Add("체결시간 :" + searchStock[i].체결시간);
                
                for (int j = 0; j <= searchStock[i].종가.Count - 1; j++)
                {
                    PriceLogListBox.Items.Add(j + "번째 전 봉의 종가는 " + searchStock[i].종가[j] + "입니다.");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
