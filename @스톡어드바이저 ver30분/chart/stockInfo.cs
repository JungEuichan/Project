using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
namespace chart
{ // --------------------------------------------------------------------------
    class stockInfo
    {
        public string stockCode;
        public string stockName;
        public List<int> 고가 = new List<int>();
        public List<int> 저가 = new List<int>();
        public List<int> 종가 = new List<int>();
        public List<int> 시가 = new List<int>();
        public List<long> 거래량 = new List<long>();
        public int ma5 = 0;
        public List<long> 시간 = new List<long>(); // 20200731151700 와 같은형식으로 나가므로
        public long vBuySum = 0;
        public long vMBuySum = 0;
        public long vSellSum = 0;
        public long vMSellSum = 0;
        public long buySum = 0;
        public long MBuySum = 0;
        public long sellSum = 0;
        public long MSellSum = 0;
        public int lLine;
        public int HLine;
        public int MLLine;
        public int MHLine;
        public int location = 0;
        public bool check = false;
        public int 현재가 = 0;
        // real데이터 부분, Tr데이터 받아올 때 체결시간 받아올것. 누적거래량은 내부메서드 첫 실행시 받아옴.
        public long 누적거래량 = 0;
        public int 체결시간 = 0;
        // 호가데이터 중간부산물
        const int thresholdMoney = 0;
        public int 순간체결량 = 0;
        public int 누적음수체결량 = 0;
        public int 누적양수체결량 = 0;
        public int 누적체결량 = 0;
        public float midPrice = 0;
        public int 누적매수순변동 = 0;
        public int 누적매도순변동 = 0;
        public int 매도호가잔량 = 0;
        public int 매수호가잔량 = 0;
        // datagirdview에 표시될 호가 데이터 부분.
        public int test = 0;
        public double 하락방어율 = 0;
        public double 상승확률 = 0;
        public int 금일저가 = 999999;
        public int 금일고가 = 0;
        public int index = 0;
        // 전략 1.sobi
        public void 호가update(int[] 매도호가가격, int[] 매도호가잔량, int[] 매수호가가격, int[] 매수호가잔량)
        {
            float midPriceTemp = (매도호가가격[0] + 매수호가가격[0]) / 2;
            // DOBI Part
            if (midPrice == midPriceTemp)
            {
                this.누적매수순변동 += (매수호가잔량[0] - this.매수호가잔량);
                this.누적매도순변동 += (매도호가잔량[0] - this.매도호가잔량);
            }
            else
            {
                this.midPrice = midPriceTemp;
            }
            this.매도호가잔량 = 매도호가잔량[0];
            this.매수호가잔량 = 매수호가잔량[0];
            /*   if(this.누적매수순변동>10000)
               {
                   this.누적체결량 = 0;
                   this.누적매수순변동 = 0;
                   this.누적매도순변동 = 0;
               }*/
            if (this.누적음수체결량 != 0 && this.누적매도순변동 != 0)
            {
                this.하락방어율 = Math.Round(((double)this.누적매수순변동 / (double)this.누적음수체결량), 1); // 참고로 누적체결량 threshold가 0넘어가면 이 지표도 왜곡이 시작됨.
                this.상승확률 = Math.Round((((double)this.누적양수체결량) / (double)this.누적매도순변동), 1);
            }
        }
        public void 체결update(int 체결량)
        {
            this.순간체결량 = 체결량;
            if (체결량 > 0)
            {
                this.누적매도순변동 += 체결량;
                this.누적양수체결량 += 체결량;
            }
            else
            {
                this.누적매수순변동 -= 체결량;
                this.누적음수체결량 -= 체결량;
            }
            if (Math.Abs(체결량 * midPrice) > thresholdMoney)
                this.누적체결량 += 체결량;

        }
        public void minuteData(int 체결시간, int 현재가, long 누적거래량)
        { // 블랙리스트에 오른 종목들은 minuteData를 수행하지 않음으로서 hline을 99999로 유지시킨다.
            if (this.location != 4)
            {
                int 체결분 = (int)Math.Truncate((double)체결시간 / 1000);
                int cnt_30 = 0;
                if (this.체결시간 == 체결분) { }
                else
                {
                    cnt_30++;
                    this.체결시간 = 체결분;
                }
                if (this.누적거래량 == 0)
                    this.누적거래량 = 누적거래량;

                if (cnt_30 != 30)
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
                    if (this.거래량.Count > 0)
                        this.거래량[0] += 누적거래량 - this.누적거래량;

                    // MessageBox.Show(this.stockName + " : updated before : " + this.체결시간 + " (고)" + this.고가[0] + " (저)" + this.저가[0] + " (종)" + this.현재가 + "(시) " + this.시가[0] + "  (거래량)" +this.거래량[0] + "update to: " + 체결분 + "분, 시가: " + 현재가);
                    this.현재가 = 현재가;
                    if (this.종가[0] > this.시가[0])
                    {
                        this.buySum += this.고가[0] * this.거래량[0];
                        this.vBuySum += this.거래량[0];
                        if (this.종가.Count > 389)
                        { // /////////////// 엥간해서는 문제없음.
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
        }
        public void priceAdd(int 고가, int 저가, int 시가, int 종가, long 거래량)
        {
            this.고가.Insert(0, 고가);
            this.저가.Insert(0, 저가);
            this.종가.Insert(0, 종가);
            this.시가.Insert(0, 시가);
            this.거래량.Insert(0, 거래량);
            this.ma5 = 종가;
            if (this.종가.Count >= 5)
            {
                this.ma5 = (this.종가[0] + this.종가[1] + this.종가[2] + this.종가[3] + this.종가[4]) / 5;
            }
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
        public void lineAdd(int lLine, int HLine, int MLLine, int MHLine)
        {
            this.lLine = lLine;
            this.HLine = HLine;
            this.MLLine = MLLine;
            this.MHLine = MHLine;
        }
        // public void distribute()
        // {
        //    if (this.HLine == 9999999)
        //        this.location = 4;
        //    else if ((this.금일저가 < this.lLine * 0.99 && this.금일고가 > this.lLine) || (this.금일저가 < this.HLine * 0.97 && this.금일고가 > this.HLine))
        //        this.location = 1;
        // }
        // 날짜가 달라지는 금일 분봉의 인덱스값을 구함. 추가적으로, 금일저가/금일고가값을 구한다.
        // public int todayBong()
        // {
        //    string today = DateTime.Now.ToString("MMdd");
        //    for (int i = this.종가.Count - 1; i >= 0; i--)
        //    {
        //        if (this.시간[i].ToString().Substring(4, 4) == today)
        //        {
        //            this.index = i;
        //            break;
        //        }
        //    }
        //    for (int i = 0; i <= index; i++)
        //    {
        //        if (this.고가[i] > this.금일고가)
        //            this.금일고가 = this.고가[i];
        //        if (this.저가[i] < this.금일저가)
        //            this.금일저가 = this.저가[i];
        //    }
        //    if (this.index > 0)
        //    {
        //        return this.index;
        //    }
        //    else
        //    {
        //        this.금일고가 = this.고가[0];
        //        this.금일저가 = this.저가[0];
        //        return 0;
        //    }
        // }
        // public void lineCalc() // 몇봉전 구현
        // {
        //    for (int i = this.index; i < 390 + index; i++)
        //        if (this.종가[i] > this.시가[i])
        //        {
        //            this.buySum += this.고가[i] * this.거래량[i];
        //            this.vBuySum += this.거래량[i];
        //        }
        //        else if (this.종가[i] < this.시가[i])
        //        {
        //            this.sellSum += this.저가[i] * this.거래량[i];
        //            this.vSellSum += this.거래량[i];
        //        }
        //    long lLine = this.sellSum / this.vSellSum;
        //    long hLine = this.buySum / this.vBuySum;
        //    // this.lineAdd(Convert.ToInt32(lLine), Convert.ToInt32(hLine));
        // }
        public stockInfo(string stockCode, string stockName)
        {
            this.stockCode = stockCode;
            this.stockName = stockName;
        }
        public stockInfo() { }
    }
    class requestConditonInfo
    {
        public string 화면번호;
        public string 검색명;
        public int 검색인덱스;
        public requestConditonInfo(string 화면번호, string 검색명, int 검색인덱스)
        {
            this.화면번호 = 화면번호;
            this.검색명 = 검색명;
            this.검색인덱스 = 검색인덱스;
        }
    }
    class accountInfo
    {
        public string stockName;
        public string stockCode;
        public int 보유수량 = 0;
        public double 매입금액 = 0;
        public int 매입평단가 = 0;
        public int 현재가 = 0;
        public int 분봉상저가최고가 = 0;
        public double 손익율 = 0;
        public double 최고손익율 = -100;
        public bool 열람값 = true;
        public int 감시상한가 = 999999;
        public int 감시하한가 = 0;
        public accountInfo(string 종목명, string 종목코드, long 매입금액, int 보유수량, double 손익율)
        {
            this.stockName = 종목명;
            this.stockCode = 종목코드;
            this.매입금액 = 매입금액;
            this.보유수량 = 보유수량;
            this.매입평단가 = Convert.ToInt32(매입금액 / 보유수량);
            this.손익율 = 손익율;
            if (손익율 > this.최고손익율)
            {
                this.최고손익율 = 손익율;
            }
        }
        public void accountUpdate(long 매입금액, int 보유수량, double 손익율)
        {
            손익율 = Math.Round(손익율, 2);
            this.매입금액 = 매입금액;
            this.보유수량 = 보유수량;
            this.매입평단가 = (int)Math.Round((매입금액 / (double)보유수량), 0);
            this.손익율 = Math.Round(손익율, 2);
            this.열람값 = true;
            if (손익율 > this.최고손익율)
            {
                this.최고손익율 = 손익율;
            }
        }
        public void priceUpdate(int 현재가)
        {
            this.현재가 = 현재가;
            this.손익율 = Math.Round((((double)현재가 - 매입평단가) / 매입평단가) * 100 - 0.25, 2);
            if (this.손익율 > this.최고손익율)
            {
                this.최고손익율 = this.손익율;
            }
        }
    }
    class AutoClosingMessageBox
    {
        [System
                .Runtime
                .InteropServices
                .DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System
                .Runtime
                .InteropServices
                .DllImport("user32.dll", CharSet = System
                    .Runtime
                    .InteropServices
                    .CharSet
                    .Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        System.Threading.Timer _timeoutTimer; // 쓰레드 타이머
        string _caption;
        const int WM_CLOSE = 0x0010; // close 명령
        AutoClosingMessageBox(string text, string caption, int timeout)
        {
            _caption = caption;
            _timeoutTimer = new System.Threading.Timer(OnTimerElapsed, null, timeout, System
                .Threading
                .Timeout
                .Infinite);
            MessageBox.Show(text, caption);
        }
        // 생성자 함수
        public static void Show(string text, string caption, int timeout)
        {
            new AutoClosingMessageBox(text, caption, timeout);
        }
        // 시간이 다되면 close 메세지를 보냄
        void OnTimerElapsed(object state)
        {
            IntPtr mbWnd = FindWindow(null, _caption);
            if (mbWnd != IntPtr.Zero)
                SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

            _timeoutTimer.Dispose();
        }
    }
}