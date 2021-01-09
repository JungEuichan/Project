using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chart
{

    class realInfo
    {
        public string stockCode;

        public string 호가시간;
        public int 매도1변동 = 0;
        public int 매수1변동 = 0;

        public string 체결시간;
        public int 체결량 = 0;

 
        public realInfo(string stockCode)
        {
            this.stockCode = stockCode;
        }

        public void 호가(string 호가시간, int 매도1변동, int 매수1변동)
        {
            this.호가시간 = 호가시간;
            this.매도1변동 = 매도1변동;
            this.매수1변동 = 매수1변동;
        }


        public void 체결(string 체결시간, int 체결량)
        {
            this.체결시간 = 체결시간;
            this.체결량 = 체결량;
        }

    }

    class priceInfo
    {
        public int 고가;
        public int 저가;
        public int 시가;
        public int 종가;
        public long 거래량;

        public priceInfo() {}
    
        public priceInfo(int 고가, int 저가, int 시가, int 종가, long 거래량)
        {
            this.고가 = 고가;
            this.저가 = 저가;
            this.시가 = 시가;
            this.종가 = 종가;
            this.거래량 = 거래량;
        }

        public void priceUpdate(int 고가, int 저가, int 시가, int 종가, long 거래량)
        {
            this.고가 = 고가;
            this.저가 = 저가;
            this.시가 = 시가;
            this.종가 = 종가;
            this.거래량 = 거래량;
        }
    } 


    class stockInfo
    {

        public string stockCode;
        public string stockName;

        public List<int> 고가 = new List<int>();
        public List<int> 저가 = new List<int>();
        public List<int> 종가 = new List<int>();
        public List<int> 시가 = new List<int>();
        public List<long> 거래량 = new List<long>();
        public List<long> 누적거래량 = new List<long>();

        public long vBuySum = 0;
        public long vSellSum = 0;
        public long buySum = 0;
        public long sellSum = 0;

        public int 현재가;
        public int 체결시간;
        public int lLine;
        public int HLine;
        public int location = 0;
        public bool check = false;

        public void priceAdd(int 고가, int 저가, int 시가, int 종가, long 거래량)
        {
            this.고가.Insert(0,고가);
            this.저가.Insert(0, 저가);
            this.종가.Insert(0, 종가);
            this.시가.Insert(0, 시가);
            
            this.거래량.Insert(0, 거래량);

            if (this.종가.Count > 390)
            {
                this.고가.RemoveAt(390);
                this.시가.RemoveAt(390);
                this.저가.RemoveAt(390);
                this.종가.RemoveAt(390);
                this.거래량.RemoveAt(390);
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
    }


    class secondInfo
    {
        public int 체결시간=0;
        public int 종가=0;
        public int 저가=0;
        public int 고가=0;

        public long 누적거래량 = 0;



        public secondInfo(int 체결시간) 
        {
            this.체결시간 = (int)Math.Truncate((double)체결시간 / 100);
        }


        public priceInfo update(int 체결시간, int 종가, int 저가, int 고가, long 누적거래량, long 기존거래량) 
            //1분봉의 데이터를 업데이트한다. 체결시간이 내부에서 분단위로 달라지면, searchStock에 값을 업데이트하고 내부값을 재정비한다. 분단위로 바뀌지 않으면, 이 클래스값을 재정비만 한다. 
            //처음에 secondInfo 생성할때 체결시간은 넣어줘야할듯
            // 참고로 누적거래량은 시초가에 형성되므로, 누적거래량의 값을 추가하면서 거래량을 추가해줘야함.
            //기존거래량은 원래의 searchStock의 누적거래량부분을 집어넣고, 새로 누적거래량을 업데이트 해주면서 그 차이만큼을 거래량으로 반환해준다.
        {
            priceInfo priceData = new priceInfo(0, 0, 0, 0, 0);
            int temp = 0;
            int 시가 = 0;

            temp = (int)Math.Truncate((double)체결시간 / 100);

            if (temp == 체결시간)
            {
                if (종가 > this.고가)
                    this.고가 = 종가;
                else if (종가 < this.저가)
                    this.저가 = 종가;
                this.종가 = 종가;
                this.누적거래량 = 누적거래량;
                
                return priceData;
            }
            else
            {
                long 거래량 = 누적거래량 - 기존거래량;

                priceData.priceUpdate(고가, 저가, 시가, 종가, 거래량);
                this.체결시간 = temp;
                return priceData;
            }
        }
    }
}
