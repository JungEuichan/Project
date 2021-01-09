using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace test3
{
    public partial class Form1 : Form
    {
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

            playSimpleSound();

        }


        private void playSimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\Speech On.wav");
            simpleSound.Play();
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
                for (int i = 0; i < conditionList.Length; i++)
                {
                    LogListBox.Items.Add(DateTime.Now.ToString("HH:mm:ss") + ": " + conditionList[i]);
                }
            }
        }

        public void onReceiveTrCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrConditionEvent e) //Tr데이터 조건검색결과 요청 이벤트함수
        {

        }

        public void onReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e) //TR데이터 요청 이벤트 함수
        {

        }

        public void onReceiveRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent e) //실시간데이터 요청이벤트 함수 
        {

        }

        public void onReceiveRealCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealConditionEvent e) //실시간 조건검색결과 요청 이벤트함수
        {

        }







        //  --------------------------------------------------------------------------메소드 및 form 객체 이벤트함수-------------------------------------------------------------------------------//


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




    }
}
