using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace formtest
{
    public partial class Form1 : Form
    {

        Form2 showForm2;

        public static Form1 form3;

        public Form1()
        {

            InitializeComponent();

            form3 = this; 
            //   this.BackgroundImage = imageList1.Images[1];
        }


        public string A = "333";
        //변수를 public으로 하려면 클래스 바깥에다가 선언해야함.

        private void button1_Click(object sender, EventArgs e)
        {
            showForm2 = new Form2(this);
            showForm2.Show();

            showForm2.BackColor = Color.Blue;
            //   showForm2.BackgroundImage("");
         // this.BackColor(Color.Blue) 이런건안된다.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showForm2.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            A = textBox1.Text;
        }
    }
}
