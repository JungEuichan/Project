using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test2
{


    public partial class Form1 : Form
    {

        stockInfo Samsung = new stockInfo();
        stockInfo Hyundai = new stockInfo();

        public Form1()
        {
            InitializeComponent();

            Samsung.stockPrice.Enqueue(1);
            Samsung.stockPrice.Enqueue(3);
            Samsung.stockPrice.Enqueue(5);
            Samsung.stockPrice.Enqueue(7);

            Hyundai.stockPrice.Enqueue(2);
            Hyundai.stockPrice.Enqueue(4);
            Hyundai.stockPrice.Enqueue(6);
            Hyundai.stockPrice.Enqueue(8);
            Hyundai.stockPrice.Enqueue(6);
            Hyundai.stockPrice.Enqueue(8);
            Hyundai.stockPrice.Enqueue(6);
            Hyundai.stockPrice.Enqueue(8);
            Hyundai.stockPrice.Enqueue(6);
            Hyundai.stockPrice.Enqueue(8);


            Hyundai = (stockInfo)Samsung.Clone();
            
            for (int i = 0; i < Hyundai.stockPrice.Count-1; i++)
            {
                listBox1.Items.Add("Hyundai price : " + Hyundai.stockPrice.Peek());
                Hyundai.stockPrice.Dequeue();
            }

   //         Hyundai.stockPrice.Append(8);
    //        int a = Hyundai.stockPrice.First();

            Hyundai = (stockInfo)Samsung.Clone();

            Hyundai.stockPrice.Enqueue(8);
            Hyundai.stockPrice.Enqueue(8);
            for (int i = 0; i < Hyundai.stockPrice.Count; i++)
            {
                listBox1.Items.Add("Hyundai price : " + Hyundai.stockPrice.Peek());
                Hyundai.stockPrice.Dequeue();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    class stockInfo : ICloneable
    {
        public Queue<int> stockPrice = new Queue<int>();
        
        public object Clone()
        {
            stockInfo newClass = new stockInfo();
            newClass.stockPrice = this.stockPrice;

            return newClass;        
        }
    }
}
/*

            Hyundai.stockPrice.Enqueue(8);
            Hyundai.stockPrice.Enqueue(8);
            Hyundai.stockPrice.Enqueue(8);
            Hyundai.stockPrice.Enqueue(8);
            Hyundai.stockPrice.Enqueue(8);
            Hyundai.stockPrice.Enqueue(8);

            Hyundai.stockPrice.Enqueue(8);
            Hyundai.stockPrice.Enqueue(8);
            Hyundai.stockPrice.Enqueue(8);
            Hyundai.stockPrice.Enqueue(8);

 * */
