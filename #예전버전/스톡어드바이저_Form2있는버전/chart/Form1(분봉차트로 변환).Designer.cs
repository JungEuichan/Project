
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.searchNameLabel = new System.Windows.Forms.Label();
            this.LogListBox = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.PriceLogListBox = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.blackList = new System.Windows.Forms.Label();
            this.listBox4 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.CheckedListBox();
            this.listBox1 = new System.Windows.Forms.CheckedListBox();
            this.listBox3 = new System.Windows.Forms.CheckedListBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkButton1 = new System.Windows.Forms.Button();
            this.newFormButton = new System.Windows.Forms.Button();
            this.searchAddButton = new System.Windows.Forms.Button();
            this.searchAddCheck = new System.Windows.Forms.CheckBox();
            this.searchAddComboBox = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.addTogetherCheckBox = new System.Windows.Forms.CheckBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.종목명 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.누적체결량 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.위치 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.평가 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkButton2 = new System.Windows.Forms.Button();
            this.checkButton3 = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.listBox5 = new System.Windows.Forms.ListBox();
            this.listBox6 = new System.Windows.Forms.ListBox();
            this.listBox7 = new System.Windows.Forms.ListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.searchNameLabel1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.tabPage4.SuspendLayout();
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
            this.listBox0.Location = new System.Drawing.Point(21, 48);
            this.listBox0.Name = "listBox0";
            this.listBox0.Size = new System.Drawing.Size(126, 532);
            this.listBox0.TabIndex = 4;
            this.listBox0.SelectedIndexChanged += new System.EventHandler(this.listBox0_SelectedIndexChanged);
            // 
            // renewalButton
            // 
            this.renewalButton.BackColor = System.Drawing.Color.LightSkyBlue;
            this.renewalButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.renewalButton.Location = new System.Drawing.Point(9, 20);
            this.renewalButton.Name = "renewalButton";
            this.renewalButton.Size = new System.Drawing.Size(68, 22);
            this.renewalButton.TabIndex = 7;
            this.renewalButton.Text = "분류";
            this.renewalButton.UseVisualStyleBackColor = false;
            this.renewalButton.Click += new System.EventHandler(this.renewalButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "선택한 조건검색 종목";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "hLine";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(292, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "1pLine";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(430, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "2pLine ";
            // 
            // searchNameLabel
            // 
            this.searchNameLabel.AutoSize = true;
            this.searchNameLabel.Location = new System.Drawing.Point(6, 3);
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
            this.LogListBox.Location = new System.Drawing.Point(9, 6);
            this.LogListBox.Name = "LogListBox";
            this.LogListBox.Size = new System.Drawing.Size(469, 172);
            this.LogListBox.TabIndex = 6;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // PriceLogListBox
            // 
            this.PriceLogListBox.FormattingEnabled = true;
            this.PriceLogListBox.HorizontalScrollbar = true;
            this.PriceLogListBox.ItemHeight = 12;
            this.PriceLogListBox.Location = new System.Drawing.Point(8, 18);
            this.PriceLogListBox.Name = "PriceLogListBox";
            this.PriceLogListBox.Size = new System.Drawing.Size(470, 520);
            this.PriceLogListBox.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(566, 497);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "Log";
            // 
            // blackList
            // 
            this.blackList.AutoSize = true;
            this.blackList.Location = new System.Drawing.Point(430, 326);
            this.blackList.Name = "blackList";
            this.blackList.Size = new System.Drawing.Size(55, 12);
            this.blackList.TabIndex = 11;
            this.blackList.Text = "blackList";
            // 
            // listBox4
            // 
            this.listBox4.FormattingEnabled = true;
            this.listBox4.ItemHeight = 12;
            this.listBox4.Location = new System.Drawing.Point(432, 348);
            this.listBox4.Name = "listBox4";
            this.listBox4.Size = new System.Drawing.Size(124, 232);
            this.listBox4.TabIndex = 6;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(297, 48);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(124, 532);
            this.listBox2.TabIndex = 14;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged_1);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(161, 47);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(124, 532);
            this.listBox1.TabIndex = 14;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(432, 47);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(124, 260);
            this.listBox3.TabIndex = 14;
            this.listBox3.SelectedIndexChanged += new System.EventHandler(this.listBox3_SelectedIndexChanged_1);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(97, 22);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(76, 16);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "자동 분류";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkButton1
            // 
            this.checkButton1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.checkButton1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.checkButton1.Location = new System.Drawing.Point(261, 21);
            this.checkButton1.Name = "checkButton1";
            this.checkButton1.Size = new System.Drawing.Size(24, 20);
            this.checkButton1.TabIndex = 7;
            this.checkButton1.Text = "✅";
            this.checkButton1.UseVisualStyleBackColor = false;
            this.checkButton1.Click += new System.EventHandler(this.renewalButton_Click);
            // 
            // newFormButton
            // 
            this.newFormButton.BackColor = System.Drawing.Color.LightSkyBlue;
            this.newFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newFormButton.Location = new System.Drawing.Point(9, 20);
            this.newFormButton.Name = "newFormButton";
            this.newFormButton.Size = new System.Drawing.Size(136, 25);
            this.newFormButton.TabIndex = 16;
            this.newFormButton.Text = "개별감시창 띄우기";
            this.newFormButton.UseVisualStyleBackColor = false;
            this.newFormButton.Click += new System.EventHandler(this.newFormButton_Click);
            // 
            // searchAddButton
            // 
            this.searchAddButton.BackColor = System.Drawing.Color.LightSkyBlue;
            this.searchAddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchAddButton.Location = new System.Drawing.Point(276, 20);
            this.searchAddButton.Name = "searchAddButton";
            this.searchAddButton.Size = new System.Drawing.Size(66, 22);
            this.searchAddButton.TabIndex = 17;
            this.searchAddButton.Text = "조건추가";
            this.searchAddButton.UseVisualStyleBackColor = false;
            this.searchAddButton.Click += new System.EventHandler(this.searchAddButton_Click);
            // 
            // searchAddCheck
            // 
            this.searchAddCheck.AutoSize = true;
            this.searchAddCheck.BackColor = System.Drawing.SystemColors.Window;
            this.searchAddCheck.Checked = true;
            this.searchAddCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchAddCheck.Location = new System.Drawing.Point(9, 52);
            this.searchAddCheck.Name = "searchAddCheck";
            this.searchAddCheck.Size = new System.Drawing.Size(136, 16);
            this.searchAddCheck.TabIndex = 20;
            this.searchAddCheck.Text = "검색결과 실시간표시";
            this.searchAddCheck.UseVisualStyleBackColor = false;
            // 
            // searchAddComboBox
            // 
            this.searchAddComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.searchAddComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.searchAddComboBox.FormattingEnabled = true;
            this.searchAddComboBox.Location = new System.Drawing.Point(9, 20);
            this.searchAddComboBox.Name = "searchAddComboBox";
            this.searchAddComboBox.Size = new System.Drawing.Size(255, 20);
            this.searchAddComboBox.TabIndex = 21;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(568, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(492, 568);
            this.tabControl1.TabIndex = 22;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.LogListBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(484, 542);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log/Control";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.newFormButton);
            this.groupBox2.Location = new System.Drawing.Point(9, 413);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(371, 112);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "체결감시 관련";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox1.Location = new System.Drawing.Point(105, 53);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 25;
            this.textBox1.Text = "500";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label10.Location = new System.Drawing.Point(212, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 24;
            this.label10.Text = "만원";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.Location = new System.Drawing.Point(12, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 12);
            this.label9.TabIndex = 24;
            this.label9.Text = "기준거래대금 : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label6.Location = new System.Drawing.Point(10, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(341, 12);
            this.label6.TabIndex = 24;
            this.label6.Text = "체결정보를 통해 매수세를 가늠할수 있도록 지표를 제시합니다";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.renewalButton);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Location = new System.Drawing.Point(9, 319);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(371, 70);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "사용자전략 적용";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label5.Location = new System.Drawing.Point(10, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(329, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "사용자가 기존 설정해놓은 전략에 맞춰 종목들을 분류합니다";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.searchAddComboBox);
            this.groupBox1.Controls.Add(this.addTogetherCheckBox);
            this.groupBox1.Controls.Add(this.searchAddButton);
            this.groupBox1.Controls.Add(this.searchAddCheck);
            this.groupBox1.Location = new System.Drawing.Point(9, 194);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(371, 102);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "조건검색관련";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label8.Location = new System.Drawing.Point(6, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(333, 12);
            this.label8.TabIndex = 24;
            this.label8.Text = "사용자의 계정 내 조건검색을 통해 필요한 종목을 불러옵니다";
            // 
            // addTogetherCheckBox
            // 
            this.addTogetherCheckBox.AutoSize = true;
            this.addTogetherCheckBox.BackColor = System.Drawing.SystemColors.Window;
            this.addTogetherCheckBox.Checked = true;
            this.addTogetherCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.addTogetherCheckBox.Location = new System.Drawing.Point(211, 52);
            this.addTogetherCheckBox.Name = "addTogetherCheckBox";
            this.addTogetherCheckBox.Size = new System.Drawing.Size(128, 16);
            this.addTogetherCheckBox.TabIndex = 24;
            this.addTogetherCheckBox.Text = "기존 검색종목 유지";
            this.addTogetherCheckBox.UseVisualStyleBackColor = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.searchNameLabel);
            this.tabPage1.Controls.Add(this.PriceLogListBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(484, 542);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "종목상세";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridView);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(484, 542);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "체결분석";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.종목명,
            this.누적체결량,
            this.Column3,
            this.위치,
            this.평가});
            this.dataGridView.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.dataGridView.Location = new System.Drawing.Point(3, 6);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(475, 533);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentDoubleClick);
            // 
            // 종목명
            // 
            this.종목명.Frozen = true;
            this.종목명.HeaderText = "종목명";
            this.종목명.Name = "종목명";
            this.종목명.ReadOnly = true;
            // 
            // 누적체결량
            // 
            this.누적체결량.Frozen = true;
            this.누적체결량.HeaderText = "누적체결량";
            this.누적체결량.Name = "누적체결량";
            this.누적체결량.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.Frozen = true;
            this.Column3.HeaderText = "누적호가";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // 위치
            // 
            this.위치.Frozen = true;
            this.위치.HeaderText = "위치";
            this.위치.Name = "위치";
            this.위치.ReadOnly = true;
            this.위치.Width = 50;
            // 
            // 평가
            // 
            this.평가.Frozen = true;
            this.평가.HeaderText = "평가";
            this.평가.Name = "평가";
            this.평가.ReadOnly = true;
            this.평가.Width = 50;
            // 
            // checkButton2
            // 
            this.checkButton2.Location = new System.Drawing.Point(399, 21);
            this.checkButton2.Name = "checkButton2";
            this.checkButton2.Size = new System.Drawing.Size(22, 21);
            this.checkButton2.TabIndex = 23;
            this.checkButton2.Text = "✅";
            this.checkButton2.UseVisualStyleBackColor = true;
            this.checkButton2.Click += new System.EventHandler(this.checkButton2_Click);
            // 
            // checkButton3
            // 
            this.checkButton3.Location = new System.Drawing.Point(533, 21);
            this.checkButton3.Name = "checkButton3";
            this.checkButton3.Size = new System.Drawing.Size(23, 20);
            this.checkButton3.TabIndex = 24;
            this.checkButton3.Text = "✅";
            this.checkButton3.UseVisualStyleBackColor = true;
            this.checkButton3.Click += new System.EventHandler(this.checkButton3_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.searchNameLabel1);
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Controls.Add(this.label12);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.listBox7);
            this.tabPage4.Controls.Add(this.listBox6);
            this.tabPage4.Controls.Add(this.listBox5);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(484, 542);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "체결상세";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // listBox5
            // 
            this.listBox5.FormattingEnabled = true;
            this.listBox5.ItemHeight = 12;
            this.listBox5.Location = new System.Drawing.Point(6, 55);
            this.listBox5.Name = "listBox5";
            this.listBox5.Size = new System.Drawing.Size(130, 484);
            this.listBox5.TabIndex = 0;
            // 
            // listBox6
            // 
            this.listBox6.FormattingEnabled = true;
            this.listBox6.ItemHeight = 12;
            this.listBox6.Location = new System.Drawing.Point(144, 55);
            this.listBox6.Name = "listBox6";
            this.listBox6.Size = new System.Drawing.Size(150, 484);
            this.listBox6.TabIndex = 1;
            // 
            // listBox7
            // 
            this.listBox7.FormattingEnabled = true;
            this.listBox7.ItemHeight = 12;
            this.listBox7.Location = new System.Drawing.Point(301, 55);
            this.listBox7.Name = "listBox7";
            this.listBox7.Size = new System.Drawing.Size(163, 484);
            this.listBox7.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 12);
            this.label11.TabIndex = 3;
            this.label11.Text = "실시간 체결량";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(142, 33);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 12);
            this.label12.TabIndex = 4;
            this.label12.Text = "우선호가동적분석";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(299, 33);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 12);
            this.label13.TabIndex = 4;
            this.label13.Text = "호가정적분석";
            // 
            // searchNameLabel1
            // 
            this.searchNameLabel1.AutoSize = true;
            this.searchNameLabel1.Location = new System.Drawing.Point(6, 13);
            this.searchNameLabel1.Name = "searchNameLabel1";
            this.searchNameLabel1.Size = new System.Drawing.Size(41, 12);
            this.searchNameLabel1.TabIndex = 11;
            this.searchNameLabel1.Text = "종목명";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 591);
            this.Controls.Add(this.checkButton3);
            this.Controls.Add(this.checkButton2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.blackList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkButton1);
            this.Controls.Add(this.listBox4);
            this.Controls.Add(this.listBox0);
            this.Controls.Add(this.axKHOpenAPI1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "스톡어드바이저";
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxKHOpenAPILib.AxKHOpenAPI axKHOpenAPI1;
        private System.Windows.Forms.ListBox listBox0;
        private System.Windows.Forms.Button renewalButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label searchNameLabel;
        private System.Windows.Forms.ListBox LogListBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListBox PriceLogListBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label blackList;
        private System.Windows.Forms.ListBox listBox4;
        private System.Windows.Forms.CheckedListBox listBox2;
        private System.Windows.Forms.CheckedListBox listBox1;
        private System.Windows.Forms.CheckedListBox listBox3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button checkButton1;
        private System.Windows.Forms.Button newFormButton;
        private System.Windows.Forms.Button searchAddButton;
        private System.Windows.Forms.CheckBox searchAddCheck;
        private System.Windows.Forms.ComboBox searchAddComboBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.CheckBox addTogetherCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button checkButton2;
        private System.Windows.Forms.Button checkButton3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn 종목명;
        private System.Windows.Forms.DataGridViewTextBoxColumn 누적체결량;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn 위치;
        private System.Windows.Forms.DataGridViewTextBoxColumn 평가;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label searchNameLabel1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox listBox7;
        private System.Windows.Forms.ListBox listBox6;
        private System.Windows.Forms.ListBox listBox5;
    }
}

