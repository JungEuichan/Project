using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace chart
{


    public class realInfo
    {
        public string stockCode;
        public string stockName;

        public int 현재가 = 0;

        public int 호가시간 = 0;

        public int 매수잔량1초변동 = 0;
        public int 매수잔량순변동 = 0;

        public int m체결량1초누적 = 0;
        public int 체결량누적 = 0;

        const int threshold = 5000000;

        public void 체결update(int 체결량, int 현재가)
        {
            if (Math.Abs(체결량 * 현재가) > threshold)
            {
                this.현재가 = 현재가;

                this.체결량누적 += 체결량;

                if (체결량<0)
                    this.m체결량1초누적 += 체결량;
                
            }
        }

        public void 호가update(int 호가시간, int 매수잔량변동)
        {
            if (Math.Abs(현재가 * 매수잔량변동) > threshold)
            {
                this.매수잔량1초변동 += 매수잔량변동;

                if (this.호가시간 != 호가시간)
                {
                    this.매수잔량순변동 = this.매수잔량순변동 + this.매수잔량1초변동 - this.m체결량1초누적;

               /*     if (this.매수잔량1초변동 - this.m체결량1초누적 < 0)
                    {
                        SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\Windows Critical Stop.wav");
                        simpleSound.Play();
                    }*/

                    this.m체결량1초누적 = 0;
                    this.매수잔량1초변동 = 0;

                    if (this.매수잔량순변동 > 100000)
                    {
                        this.매수잔량순변동 = 0;
                        this.체결량누적 = 0;
                    }
                }
            }
        }

        public realInfo() { }

        public void reset()
        {
            현재가 = 0;

            호가시간 = 0;

            매수잔량1초변동 = 0;
            매수잔량순변동 = 0;

            m체결량1초누적 = 0;
            체결량누적 = 0;
        }

    } //알고리즘 테스트 용도로만쓸것.

        class stockInfo
        {

            public string stockCode;
            public string stockName;

            public List<int> 고가 = new List<int>();
            public List<int> 저가 = new List<int>();
            public List<int> 종가 = new List<int>();
            public List<int> 시가 = new List<int>();
            public List<long> 거래량 = new List<long>();

            public long vBuySum = 0;
            public long vSellSum = 0;
            public long buySum = 0;
            public long sellSum = 0;

            public int lLine;
            public int HLine;
            public int location = 0;
            public bool check = false;

            public int 현재가;

            //real데이터 부분, Tr데이터 받아올 때 체결시간 받아올것. 누적거래량은 내부메서드 첫 실행시 받아옴.
            public long 누적거래량 = 0;
            public int 체결시간 = 0;

            // 호가데이터 중간부산물
            public int 호가시간 = 0;
            public int 호가잔량1초 = 0;
            public int 체결량1초 = 0;

            // datagirdview에 표시될 호가 데이터 부분.
            public int 누적체결량 = 0;
            public int 누적호가변동 = 0;
            public int 평가점수 = 0;

            public void 호가update()
            {

            }

            public void 체결update()
            {

            }

            public void minuteData(int 체결시간, int 현재가, int 누적거래량)
            {
                int 체결분 = (int)Math.Truncate((double)체결시간 / 100);

                if (this.누적거래량 == 0)
                    this.누적거래량 = 누적거래량;

                if (this.체결시간 == 체결분)
                {
                    this.현재가 = 현재가;
                    this.종가[0] = 현재가;

                    if (현재가 > this.고가[0])
                        this.고가[0] = 현재가;
                    else if (현재가 < this.저가[0])
                        this.저가[0] = 현재가;
                }
                else
                {
                    this.거래량[0] += 누적거래량 - this.누적거래량;
                    //   MessageBox.Show(this.stockName + " : updated before : " + this.체결시간 + " (고)" + this.고가[0] + " (저)" + this.저가[0] + " (종)" + this.현재가 + "(시) " + this.시가[0] + "  (거래량)" +this.거래량[0] + "update to: " + 체결분 + "분, 시가: " + 현재가);

                    this.현재가 = 현재가;


                    if (this.종가[0] > this.시가[0])
                    {
                        this.buySum += this.고가[0] * this.거래량[0];
                        this.vBuySum += this.거래량[0];
                        
                        if(this.종가.Count>389)  ///////////////// 엥간해서는 문제없음.
                        {
                            if (this.종가[389] > this.시가[389])
                            {
                                this.buySum -= this.고가[389] * this.거래량[389];
                                this.vBuySum -= this.거래량[389];
                            }
                        }


                        this.HLine = Convert.ToInt32(this.buySum / this.vBuySum);
                    }
                    else if (this.종가[0] < this.시가[0])
                    {
                        this.sellSum += this.저가[0] * this.거래량[0];
                        this.vSellSum += this.거래량[0];

                        if (this.종가.Count > 389)
                        {
                            if (this.종가[389] < this.시가[389])
                            {
                                this.vSellSum -= this.거래량[389];
                                this.sellSum -= this.저가[389] * this.거래량[389];
                            }
                        }

                        this.lLine = Convert.ToInt32(this.sellSum / this.vSellSum);
                    }

                    priceAdd(현재가, 현재가, 현재가, 현재가, 0);
                    this.누적거래량 = 누적거래량;
                    this.체결시간 = 체결분;

                    // 이렇게 하면서 1번째전의 hLine lLine 계산해야할듯.
                }
            }

            public void priceAdd(int 고가, int 저가, int 시가, int 종가, long 거래량)
            {
                this.고가.Insert(0, 고가);
                this.저가.Insert(0, 저가);
                this.종가.Insert(0, 종가);
                this.시가.Insert(0, 시가);

                this.거래량.Insert(0, 거래량);

                if (this.종가.Count > 410)
                {
                    this.고가.RemoveAt(410);
                    this.시가.RemoveAt(410);
                    this.저가.RemoveAt(410);
                    this.종가.RemoveAt(410);
                    this.거래량.RemoveAt(410);
                }
            }

            public void priceNow(int 현재가)
            {
                this.현재가 = 현재가;
            }

            public void lineAdd(int lLine, int HLine)
            {
                this.lLine = lLine;
                this.HLine = HLine;
            }

            public stockInfo(string stockCode, string stockName)
            {
                this.stockCode = stockCode;
                this.stockName = stockName;
            }

            public stockInfo()
            { }

        }
}

