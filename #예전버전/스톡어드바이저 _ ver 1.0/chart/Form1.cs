using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace chart
{
    public partial class Form1 : Form
    {
        List<stockInfo> stockList; //stockInfo값을 가지는 리스트를 선언함. 리스트 선언은 이렇게 알고 있으면 될것.
        List<priceInfoObject> priceInfoList; // 얘네들은 form클래스에 있으면 안되고 그 상위에 있어야 되나봄.
        Series chartSeries;


        public Form1()
        {

            InitializeComponent();
            axKHOpenAPI1.CommConnect(); //로그인
            axKHOpenAPI1.OnEventConnect += onEventConnect;
            stockSearchButton.Click += chartPlot;
            axKHOpenAPI1.OnReceiveTrData += onReceiveTrData;

            chartSeries = stockChart.Series["Series1"]; //
            stockChart.Series["Series1"]["PriceUpColor"] = "Red";//
            stockChart.Series["Series1"]["PriceDownColor"] = "Blue";//
        }

        public void onReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if (e.sRQName=="주식일봉차트조회")
            {
                int count = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName); //이게 뭔소리임?? .GetRepeatCnt 수신 받은 데이터의 반복 개수를 반환한다.

                for (int i=0; i<count; i++)
                {
                    string 일자 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "일자").Trim(); //데이터를 어떻게 불러오길래? i는 몇번째 데이터인지 보여주는듯
                    int 시가 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "시가").Trim());
                    int 고가 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "고가").Trim());
                    int 저가 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "저가").Trim());
                    int 종가 = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "현재가").Trim());

                    priceInfoList.Add(new priceInfoObject(일자, 시가, 고가, 저가, 종가));

                    stockChart.Series["Series1"].Points.AddXY(priceInfoList[i].일자, priceInfoList[i].고가);
                    stockChart.Series["Series1"].Points[i].YValues[1] = priceInfoList[i].저가;
                    stockChart.Series["Series1"].Points[i].YValues[2] = priceInfoList[i].시가;
                    stockChart.Series["Series1"].Points[i].YValues[3] = priceInfoList[i].종가;
                }
            }
        }

        public void onEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            if(e.nErrCode == 0)
            {
                stockList = new List<stockInfo>();

                string stockListofString = axKHOpenAPI1.GetCodeListByMarket(null);
                string[] stockArray = stockListofString.Split(';');
                AutoCompleteStringCollection stockCollection = new AutoCompleteStringCollection();

                for(int i=0; i<stockArray.Length; i++)
                {
                    stockList.Add(new stockInfo(stockArray[i], axKHOpenAPI1.GetMasterCodeName(stockArray[i])));
                    stockCollection.Add(axKHOpenAPI1.GetMasterCodeName(stockArray[i]));
                }
                stockTextBox.AutoCompleteCustomSource = stockCollection;
            }
        }

        public void chartPlot(object sender, EventArgs e)
        {
            if(sender.Equals(stockSearchButton))
            {
                priceInfoList = new List<priceInfoObject>();

                if(stockTextBox.Text.Length>0)
                {
                    int index = stockList.FindIndex(o => o.stockName == stockTextBox.Text); // string과 string비교는 =가 아닌 ==로 해야됨.
                    string stockCode = stockList[index].stockCode;
                    axKHOpenAPI1.SetInputValue("종목코드", stockCode);
                    axKHOpenAPI1.SetInputValue("기준일자", DateTime.Now.ToString("yyyyMMdd")); //날짜데이타를 스트링데이타로 변경해서 보내준다.
                    axKHOpenAPI1.SetInputValue("수정주가구분", "1");
                    axKHOpenAPI1.CommRqData("주식일봉차트조회", "opt10081", 0, "8100"); // TR의 번호는 opt10081 "주식 일봉차트 조회"입니다., 화면번호는 8100

                }
            }
        }


        class stockInfo
        {
            public string stockCode;
            public string stockName;
            public stockInfo(string stockCode, string stockName)
            {
                this.stockCode = stockCode;
                this.stockName = stockName;
            }
        }

        class priceInfoObject
        {
            public string 일자;
            public int 시가;
            public int 고가;
            public int 저가;
            public int 종가;
            public priceInfoObject(string 일자, int 시가, int 고가, int 저가, int 종가)
            {
                this.일자 = 일자;
                this.시가 = 시가;
                this.고가 = 고가;
                this.저가 = 저가;
                this.종가 = 종가;
            }
        }
    }
}
