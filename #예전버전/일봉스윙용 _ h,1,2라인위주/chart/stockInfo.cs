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

            public long vSum20 = 0;
            public long sum20 = 0;
            public long vSum50 = 0;
            public long sum50 = 0;
            
            public int lLine;
            public int HLine;
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

                if (this.종가.Count > 52)
                {
                    this.고가.RemoveAt(51);
                    this.시가.RemoveAt(51);
                    this.저가.RemoveAt(51);
                    this.종가.RemoveAt(51);
                    this.거래량.RemoveAt(51);
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

