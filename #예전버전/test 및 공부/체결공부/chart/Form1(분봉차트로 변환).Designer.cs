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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.searchNameLabel = new System.Windows.Forms.Label();
            this.LogListBox = new System.Windows.Forms.ListBox();
            this.PriceLogListBox = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.testBox = new System.Windows.Forms.ListBox();
            this.대량체결창 = new System.Windows.Forms.Label();
            this.fakeCheckBox = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.대량체결누적 = new System.Windows.Forms.Label();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.axKHOpenAPI1 = new AxKHOpenAPILib.AxKHOpenAPI();
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).BeginInit();
            this.SuspendLayout();
            // 
            // searchNameLabel
            // 
            this.searchNameLabel.AutoSize = true;
            this.searchNameLabel.Location = new System.Drawing.Point(25, 43);
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
            this.LogListBox.Location = new System.Drawing.Point(530, 525);
            this.LogListBox.Name = "LogListBox";
            this.LogListBox.Size = new System.Drawing.Size(234, 76);
            this.LogListBox.TabIndex = 6;
            // 
            // PriceLogListBox
            // 
            this.PriceLogListBox.FormattingEnabled = true;
            this.PriceLogListBox.HorizontalScrollbar = true;
            this.PriceLogListBox.ItemHeight = 12;
            this.PriceLogListBox.Location = new System.Drawing.Point(27, 69);
            this.PriceLogListBox.Name = "PriceLogListBox";
            this.PriceLogListBox.Size = new System.Drawing.Size(204, 532);
            this.PriceLogListBox.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(528, 501);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "Log";
            // 
            // testBox
            // 
            this.testBox.FormattingEnabled = true;
            this.testBox.ItemHeight = 12;
            this.testBox.Location = new System.Drawing.Point(237, 69);
            this.testBox.Name = "testBox";
            this.testBox.Size = new System.Drawing.Size(109, 532);
            this.testBox.TabIndex = 16;
            // 
            // 대량체결창
            // 
            this.대량체결창.AutoSize = true;
            this.대량체결창.Location = new System.Drawing.Point(235, 21);
            this.대량체결창.Name = "대량체결창";
            this.대량체결창.Size = new System.Drawing.Size(65, 12);
            this.대량체결창.TabIndex = 11;
            this.대량체결창.Text = "대량체결창";
            // 
            // fakeCheckBox
            // 
            this.fakeCheckBox.FormattingEnabled = true;
            this.fakeCheckBox.ItemHeight = 12;
            this.fakeCheckBox.Location = new System.Drawing.Point(352, 69);
            this.fakeCheckBox.Name = "fakeCheckBox";
            this.fakeCheckBox.Size = new System.Drawing.Size(160, 532);
            this.fakeCheckBox.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(350, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "허매수/허매도 판독";
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(27, 12);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(100, 21);
            this.searchBox.TabIndex = 17;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(143, 10);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 18;
            this.searchButton.Text = "검색";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(530, 69);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(109, 412);
            this.listBox1.TabIndex = 16;
            // 
            // 대량체결누적
            // 
            this.대량체결누적.AutoSize = true;
            this.대량체결누적.Location = new System.Drawing.Point(235, 43);
            this.대량체결누적.Name = "대량체결누적";
            this.대량체결누적.Size = new System.Drawing.Size(77, 12);
            this.대량체결누적.TabIndex = 11;
            this.대량체결누적.Text = "대량체결누적";
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.ItemHeight = 12;
            this.listBox3.Location = new System.Drawing.Point(655, 69);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(109, 412);
            this.listBox3.TabIndex = 16;
            // 
            // axKHOpenAPI1
            // 
            this.axKHOpenAPI1.Enabled = true;
            this.axKHOpenAPI1.Location = new System.Drawing.Point(690, 223);
            this.axKHOpenAPI1.Name = "axKHOpenAPI1";
            this.axKHOpenAPI1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axKHOpenAPI1.OcxState")));
            this.axKHOpenAPI1.Size = new System.Drawing.Size(74, 40);
            this.axKHOpenAPI1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 613);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.fakeCheckBox);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.testBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.대량체결누적);
            this.Controls.Add(this.대량체결창);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.searchNameLabel);
            this.Controls.Add(this.PriceLogListBox);
            this.Controls.Add(this.LogListBox);
            this.Controls.Add(this.axKHOpenAPI1);
            this.Name = "Form1";
            this.Text = "조건만족종목";
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxKHOpenAPILib.AxKHOpenAPI axKHOpenAPI1;
        private System.Windows.Forms.Label searchNameLabel;
        private System.Windows.Forms.ListBox LogListBox;
        private System.Windows.Forms.ListBox PriceLogListBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox testBox;
        private System.Windows.Forms.Label 대량체결창;
        private System.Windows.Forms.ListBox fakeCheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label 대량체결누적;
        private System.Windows.Forms.ListBox listBox3;
    }
}

