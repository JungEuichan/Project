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
    public partial class Form2 : Form
    {
        Form1 form;

        private void Form2_Load(object sender, System.EventArgs e)
        {
            BackColor = Color.Blue;
        }

        public Form2(Form1 form)
        {
            InitializeComponent();
            this.form = form;

            
            //form1 인스턴스를 형성후 form1객체를 받아오는 form2의 생성자를 만들면, form1인스턴스로 부터 메서드/변수를 받아올수있다.
        }



        public Form2()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = Form1.form3.A;


           //  textBox2.Text = form.A;
        }
    }
}
