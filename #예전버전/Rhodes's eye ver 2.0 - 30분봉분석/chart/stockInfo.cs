﻿using System;
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

        public long v양봉고가 = 0;
        public long 양봉고가 = 0;

        public long v음봉저가 = 0;
        public long 음봉저가 = 0;

        public int lLine;
        public int HLine;

        public int 고가_120;
        public int 저가_120;
 
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
            if (this.location != 5)
            {
                if (this.종가[몇봉전] > this.lLine * 1 && this.종가[몇봉전] < this.lLine * 1.03)
                    this.location = 3;

                if (this.종가[몇봉전] > this.HLine * 1 && this.종가[몇봉전] < this.HLine * 1.03)
                    this.location = 4;


                int 고저_75 = (this.고가_120 * 3 + this.저가_120) / 4;
                int 고저_50 = (this.고가_120 + this.저가_120) / 2;
                int 고저_25 = (this.고가_120 + this.저가_120 * 3) / 4;


                if (this.종가[몇봉전] < 고저_75 * 1.03 && this.종가[몇봉전] > 고저_75 * 1)
                    this.location = 2;

                //      else if (this.종가[몇봉전] < 고저_50 * 1.03 && this.종가[몇봉전] > 고저_50 * 1)
                //          this.location = 2;

                else if (this.종가[몇봉전] < 고저_25 * 1.03 && this.종가[몇봉전] > 고저_25 * 1)
                    this.location = 1;

            }
            
        }

        public void lineReset ()
        {
            this.location = 0;
            this.lLine = 0;
            this.HLine = 0;

            this.v양봉고가 = 0;
            this.양봉고가 = 0;

            this.v음봉저가 = 0;
            this.음봉저가 = 0;

            this.고가_120 = 0;
            this.저가_120 = 9999999;
        }

        public void lineCalc(int 몇봉전shift1)
        {
            int 몇봉전 = 몇봉전shift1 + 1;

            if(this.종가.Count < 390)
            {
                this.location = 5;
            }
            else
            {
                for (int i = 몇봉전; i < 390 + 몇봉전; i++)
                {
                    if (this.종가[i] > this.시가[i] && this.거래량[i] > 0)
                    {
                        this.양봉고가 += (long)(this.고가[i] * (this.거래량[i]));
                        this.v양봉고가 += (long)(this.거래량[i]);
                    }
                    else if (this.종가[i] < this.시가[i] && this.거래량[i] > 0)
                    {
                        this.음봉저가 += (long)(this.저가[i] * (this.거래량[i]));
                        this.v음봉저가 += (long)(this.거래량[i]);
                    }
                }
                this.HLine = Convert.ToInt32(this.양봉고가 / this.v양봉고가);
                this.lLine = Convert.ToInt32(this.음봉저가 / this.v음봉저가);


                for (int i = 몇봉전shift1; i < 390 + 몇봉전shift1; i++)
                {
                    if (this.저가_120 > this.저가[i])
                        this.저가_120 = this.저가[i];

                    if (this.고가_120 < this.고가[i])
                        this.고가_120 = this.고가[i];
                }
            }

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

