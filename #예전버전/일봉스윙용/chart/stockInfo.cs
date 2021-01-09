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
    class stockInfo
    {


        public string stockCode;
        public string stockName;

        public List<int> 고가 = new List<int>();
        public List<int> 저가 = new List<int>();
        public List<int> 종가 = new List<int>();
        public List<int> 시가 = new List<int>();
        public List<long> 거래량 = new List<long>();

        public long v양봉고가_20 = 0;
        public long 양봉고가_20 = 0;
        public long v양봉고가_50 = 0;
        public long 양봉고가_50 = 0;

        public long v음봉저가_20 = 0;
        public long 음봉저가_20 = 0;
        public long v음봉저가_50 = 0;
        public long 음봉저가_50 = 0;


        public int lLine20;
        public int lLine50;
        public int HLine20;
        public int HLine50;

        public int location = 0;
        public bool check = false;

        public int 현재가;

        public void priceAdd(int 고가, int 저가, int 시가, int 종가, long 거래량)
        {
            this.고가.Insert(0, 고가);
            this.저가.Insert(0, 저가);
            this.종가.Insert(0, 종가);
            this.시가.Insert(0, 시가);
            this.거래량.Insert(0, 거래량);
        }

        public void priceNow(int 현재가)
        {
            this.현재가 = 현재가;
        }

        public stockInfo(string stockCode, string stockName)
        {
            this.stockCode = stockCode;
            this.stockName = stockName;
        }

        public stockInfo()
        { }

        public void lineDistribute(int 몇봉전)
        {
            //20고가선근처에 종가가 형성되고, 시가가 20고가선 위에 형성된경우.
            if (this.종가[몇봉전] < this.HLine20 * 1.02 && this.종가[몇봉전] > this.HLine20 * 0.995 && this.시가[몇봉전] > this.HLine20)
                this.location = 1;

            //50고가선근처에 종가가 형성되고, 시가가 50고가선 위에 형성된경우.
            if (this.종가[몇봉전] < this.HLine50 * 1.02 && this.종가[몇봉전] > this.HLine50 * 0.995 && this.시가[몇봉전] > this.HLine50)
                this.location = 2;

            //20저가선근처에 종가가 형성된 경우
            if (this.종가[몇봉전] < this.lLine20 * 1.02 && this.종가[몇봉전] > this.lLine20 * 0.99)
                this.location = 3;

            //50저가선 근처에 종가가 형성된 경우
            if (this.종가[몇봉전] < this.lLine50 * 1.02 && this.종가[몇봉전] > this.lLine50 * 0.99)
                this.location = 4;
        }

        public void lineReset ()
        {
            this.location = 0;
            this.HLine50 = 0;
            this.lLine20 = 0;
            this.lLine50 = 0;
            this.HLine20 = 0;

            this.v양봉고가_20 = 0;
            this.양봉고가_20 = 0;
            this.v양봉고가_50 = 0;
            this.양봉고가_50 = 0;


            this.v음봉저가_20 = 0;
            this.음봉저가_20 = 0;
            this.v음봉저가_50 = 0;
            this.음봉저가_50 = 0;

        }

        public void lineCalc(int 몇봉전shift1)
        {
            int 몇봉전 = 몇봉전shift1 + 1;

            
            for ( int i = 몇봉전; i< 20+몇봉전; i++)
            {
                if (this.종가[i] > this.시가[i] && this.거래량[i] > 0)
                {
                    this.양봉고가_20 += (long)(this.고가[i] * (this.거래량[i]));
                    this.v양봉고가_20 += (long)(this.거래량[i]);
                }
                else if (this.종가[i] < this.시가[i] && this.거래량[i] > 0)
                {
                    this.음봉저가_20 += (long)(this.저가[i] * (this.거래량[i]));
                    this.v음봉저가_20 += (long)(this.거래량[i]);
                }
            }
            this.HLine20 = Convert.ToInt32(this.양봉고가_20 / this.v양봉고가_20);
            this.lLine20 = Convert.ToInt32(this.음봉저가_20 / this.v음봉저가_20);

            for (int i = 몇봉전; i < 50 + 몇봉전; i++)
            {
                if (this.종가[i] > this.시가[i] && this.거래량[i] > 0)
                {
                    this.양봉고가_50 += (long)(this.고가[i] * (this.거래량[i]));
                    this.v양봉고가_50 += (long)(this.거래량[i]);
                }
                else if (this.종가[i] < this.시가[i] && this.거래량[i] > 0)
                {
                    this.음봉저가_50 += (long)(this.저가[i] * (this.거래량[i]));
                    this.v음봉저가_50 += (long)(this.거래량[i]);
                }
            }
            this.HLine50 = Convert.ToInt32(this.양봉고가_50 / this.v양봉고가_50);
            this.lLine50 = Convert.ToInt32(this.음봉저가_50 / this.v음봉저가_50);

        }
    }


    class searchedList
    {
        public string 화면번호;
        public string 검색명;
        public int 검색인덱스;

        public searchedList(string 화면번호, string 검색명, int 검색인덱스)
        {
            this.화면번호 = 화면번호;
            this.검색명 = 검색명;
            this.검색인덱스 = 검색인덱스;
        }
    }
}

