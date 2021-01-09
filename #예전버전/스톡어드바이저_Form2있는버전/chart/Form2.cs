using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxKHOpenAPILib;

namespace chart
{
    public partial class Form2 : Form
    {

        Form1 Form1;
        
        int formNumTemp;

        // 화면번호 따로 생성, stockInfo클래스도 따로 생성, 생성후에 지우는것도 일임, 창이 꺼졌을때 해제시키는 메서드필요, realInfo클래스 형성해보기.
        //Form1에서 리셋버튼 눌렀을때 form2에는 영향을 주면안됨. 흠....

        public Form2(Form1 Form, int formNum)
        {
            InitializeComponent();
            this.Form1 = Form;
            this.Text = ("체결분석창 _" + formNum).ToString();
            formNumTemp = formNum;

            FormClosing += FormClose;

        }


        // -------------------------------------------------------------------- 메소드 및 이벤트 호출함수 -----------------------------------------------------------------------------------------------
        private void searchButton_Click(object sender, EventArgs e)
        {
            if(searchBox.TextLength>0)
            {


                Form1.realData[formNumTemp].stockCode = searchBox.Text;
                Form1.realData[formNumTemp].stockName = Form1.GetMasterCodeName(searchBox.Text);

                if(Form1.realData[formNumTemp].stockName.Length>1)
                {
                    
                    stockNameLabel.Text = "종목명 : " + Form1.realData[formNumTemp].stockName;

                    Form1.realData[formNumTemp].reset();
                    Form1.SetRealReg(formNumTemp, searchBox.Text, "21;15;81;91;10", "1");
                    timer1.Enabled = true;
                }
                else
                {
                    stockNameLabel.Text = "해당 종목을 찾을수 없음";
                }
            }
        }

        public int formNumLoad()
        {
            return formNumTemp;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = Form1.realData[formNumTemp].체결량누적.ToString();
            label2.Text = Form1.realData[formNumTemp].매수잔량순변동.ToString();
        }

        public void FormClose(object sender, FormClosingEventArgs e)
        {
             Form1.Disconnect(formNumTemp);
        }
    }
}
