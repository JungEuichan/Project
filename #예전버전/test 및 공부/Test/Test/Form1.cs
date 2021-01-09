using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        string searchCode;

        public Form1()
        {
            
            InitializeComponent();
            axKHOpenAPI1.CommConnect();
            axKHOpenAPI1.OnEventConnect += onEventConnect; //로그인 이벤트 함수
            axKHOpenAPI1.OnReceiveTrData += onReceiveTrData;
        }
        public void onEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e) //로그인 이벤트 함수
        {
            if (e.nErrCode == 0)
            {
                axKHOpenAPI1.GetConditionLoad(); //조건식 요청
                listBox1.Items.Add(DateTime.Now + ": ## 로그인 성공");
                // 033100 033110 033130 033160 033170
                searchCode = "000020";

                axKHOpenAPI1.SetInputValue("종목코드",searchCode);
                axKHOpenAPI1.SetInputValue("틱범위", "1");
                axKHOpenAPI1.SetInputValue("수정주가구분", "0");

                axKHOpenAPI1.CommRqData(searchCode, "opt10080", 0, "5000");

                searchCode = "033100";

                axKHOpenAPI1.SetInputValue("종목코드", searchCode);
                axKHOpenAPI1.SetInputValue("틱범위", "1");
                axKHOpenAPI1.SetInputValue("수정주가구분", "0");

                axKHOpenAPI1.CommRqData(searchCode, "opt10080", 0, "5000");

                searchCode = "033110";

                axKHOpenAPI1.SetInputValue("종목코드", searchCode);
                axKHOpenAPI1.SetInputValue("틱범위", "1");
                axKHOpenAPI1.SetInputValue("수정주가구분", "0");

                axKHOpenAPI1.CommRqData(searchCode, "opt10080", 0, "5000");


            }
        }

        public void onReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            MessageBox.Show("종목명 : " + e.sRQName + searchCode + "고가" + axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "고가"));

        //    if (e.sRQName == searchCode)
            {
                for(int i=0;i<10;i++)
                {
                    listBox1.Items.Add("종목명 : " + e.sRQName + "고가" + axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "고가"));
                }
                
            }
        }

    }
}
