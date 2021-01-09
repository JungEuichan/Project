namespace chart
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axKHOpenAPI1 = new AxKHOpenAPILib.AxKHOpenAPI();
            this.listBox0 = new System.Windows.Forms.ListBox();
            this.renewalButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.searchNameLabel = new System.Windows.Forms.Label();
            this.LogListBox = new System.Windows.Forms.ListBox();
            this.resetButton = new System.Windows.Forms.Button();
            this.PriceLogListBox = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.blackList = new System.Windows.Forms.Label();
            this.listBox4 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.CheckedListBox();
            this.listBox1 = new System.Windows.Forms.CheckedListBox();
            this.listBox3 = new System.Windows.Forms.CheckedListBox();
            this.outputButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.searchAddComboBox = new System.Windows.Forms.ComboBox();
            this.candleBeforeBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).BeginInit();
            this.SuspendLayout();
            // 
            // axKHOpenAPI1
            // 
            this.axKHOpenAPI1.Enabled = true;
            this.axKHOpenAPI1.Location = new System.Drawing.Point(328, 143);
            this.axKHOpenAPI1.Name = "axKHOpenAPI1";
            this.axKHOpenAPI1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axKHOpenAPI1.OcxState")));
            this.axKHOpenAPI1.Size = new System.Drawing.Size(74, 31);
            this.axKHOpenAPI1.TabIndex = 0;
            // 
            // listBox0
            // 
            this.listBox0.FormattingEnabled = true;
            this.listBox0.ItemHeight = 12;
            this.listBox0.Location = new System.Drawing.Point(26, 65);
            this.listBox0.Name = "listBox0";
            this.listBox0.Size = new System.Drawing.Size(126, 532);
            this.listBox0.TabIndex = 4;
            this.listBox0.SelectedIndexChanged += new System.EventHandler(this.listBox0_SelectedIndexChanged);
            // 
            // renewalButton
            // 
            this.renewalButton.ForeColor = System.Drawing.Color.Black;
            this.renewalButton.Location = new System.Drawing.Point(461, 9);
            this.renewalButton.Name = "renewalButton";
            this.renewalButton.Size = new System.Drawing.Size(70, 23);
            this.renewalButton.TabIndex = 7;
            this.renewalButton.Text = "선조회";
            this.renewalButton.UseVisualStyleBackColor = true;
            this.renewalButton.Click += new System.EventHandler(this.renewalButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "alpha조건검색식 만족";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "25근접";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(293, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "75근접";
            // 
            // searchNameLabel
            // 
            this.searchNameLabel.AutoSize = true;
            this.searchNameLabel.Location = new System.Drawing.Point(709, 43);
            this.searchNameLabel.Name = "searchNameLabel";
            this.searchNameLabel.Size = new System.Drawing.Size(41, 12);
            this.searchNameLabel.TabIndex = 11;
            this.searchNameLabel.Text = "종목명";
            // 
            // LogListBox
            // 
            this.LogListBox.FormattingEnabled = true;
            this.LogListBox.HorizontalScrollbar = true;
            this.LogListBox.ItemHeight = 12;
            this.LogListBox.Location = new System.Drawing.Point(1027, 66);
            this.LogListBox.Name = "LogListBox";
            this.LogListBox.Size = new System.Drawing.Size(240, 532);
            this.LogListBox.TabIndex = 6;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(281, 10);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(70, 23);
            this.resetButton.TabIndex = 13;
            this.resetButton.Text = "조건리셋";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // PriceLogListBox
            // 
            this.PriceLogListBox.FormattingEnabled = true;
            this.PriceLogListBox.HorizontalScrollbar = true;
            this.PriceLogListBox.ItemHeight = 12;
            this.PriceLogListBox.Location = new System.Drawing.Point(711, 65);
            this.PriceLogListBox.Name = "PriceLogListBox";
            this.PriceLogListBox.Size = new System.Drawing.Size(304, 532);
            this.PriceLogListBox.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1025, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "Log";
            // 
            // blackList
            // 
            this.blackList.AutoSize = true;
            this.blackList.Location = new System.Drawing.Point(435, 43);
            this.blackList.Name = "blackList";
            this.blackList.Size = new System.Drawing.Size(107, 12);
            this.blackList.TabIndex = 11;
            this.blackList.Text = "저가선/고가선근접";
            // 
            // listBox4
            // 
            this.listBox4.FormattingEnabled = true;
            this.listBox4.ItemHeight = 12;
            this.listBox4.Location = new System.Drawing.Point(572, 65);
            this.listBox4.Name = "listBox4";
            this.listBox4.Size = new System.Drawing.Size(123, 532);
            this.listBox4.TabIndex = 6;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(295, 65);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(124, 532);
            this.listBox2.TabIndex = 14;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged_1);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(166, 64);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(109, 532);
            this.listBox1.TabIndex = 14;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(437, 65);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(124, 532);
            this.listBox3.TabIndex = 14;
            this.listBox3.SelectedIndexChanged += new System.EventHandler(this.listBox3_SelectedIndexChanged_1);
            // 
            // outputButton
            // 
            this.outputButton.Location = new System.Drawing.Point(1149, 9);
            this.outputButton.Name = "outputButton";
            this.outputButton.Size = new System.Drawing.Size(118, 23);
            this.outputButton.TabIndex = 17;
            this.outputButton.Text = "텍스트파일 출력";
            this.outputButton.UseVisualStyleBackColor = true;
            this.outputButton.Click += new System.EventHandler(this.outputButton_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // searchAddComboBox
            // 
            this.searchAddComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.searchAddComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.searchAddComboBox.FormattingEnabled = true;
            this.searchAddComboBox.Location = new System.Drawing.Point(30, 10);
            this.searchAddComboBox.Name = "searchAddComboBox";
            this.searchAddComboBox.Size = new System.Drawing.Size(235, 20);
            this.searchAddComboBox.TabIndex = 18;
            // 
            // candleBeforeBox
            // 
            this.candleBeforeBox.Location = new System.Drawing.Point(378, 9);
            this.candleBeforeBox.Name = "candleBeforeBox";
            this.candleBeforeBox.Size = new System.Drawing.Size(34, 21);
            this.candleBeforeBox.TabIndex = 19;
            this.candleBeforeBox.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(418, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "봉 전 ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1277, 609);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.candleBeforeBox);
            this.Controls.Add(this.searchAddComboBox);
            this.Controls.Add(this.outputButton);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.searchNameLabel);
            this.Controls.Add(this.blackList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.renewalButton);
            this.Controls.Add(this.PriceLogListBox);
            this.Controls.Add(this.LogListBox);
            this.Controls.Add(this.listBox4);
            this.Controls.Add(this.listBox0);
            this.Controls.Add(this.axKHOpenAPI1);
            this.Name = "Form1";
            this.Text = "조건만족종목";
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxKHOpenAPILib.AxKHOpenAPI axKHOpenAPI1;
        private System.Windows.Forms.ListBox listBox0;
        private System.Windows.Forms.Button renewalButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label searchNameLabel;
        private System.Windows.Forms.ListBox LogListBox;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.ListBox PriceLogListBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label blackList;
        private System.Windows.Forms.ListBox listBox4;
        private System.Windows.Forms.CheckedListBox listBox2;
        private System.Windows.Forms.CheckedListBox listBox1;
        private System.Windows.Forms.CheckedListBox listBox3;
        private System.Windows.Forms.Button outputButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox searchAddComboBox;
        private System.Windows.Forms.TextBox candleBeforeBox;
        private System.Windows.Forms.Label label5;
    }
}

