
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.yesModeCheck = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.searchNameLabel2 = new System.Windows.Forms.Label();
            this.buyButton = new System.Windows.Forms.Button();
            this.stockQtyBox = new System.Windows.Forms.TextBox();
            this.searchNameLabel1 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.손절율TextBox = new System.Windows.Forms.TextBox();
            this.트레일링기준TextBox = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.트레일링익절율TextBox = new System.Windows.Forms.TextBox();
            this.이익보전율TextBox = new System.Windows.Forms.TextBox();
            this.이익보전조건TextBox = new System.Windows.Forms.TextBox();
            this.sellButton = new System.Windows.Forms.Button();
            this.balanceDataGridView = new System.Windows.Forms.DataGridView();
            this.계좌_종목명 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.계좌_simple = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.계좌_종합 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.계좌_위치 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.계좌_수익률 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.계좌_최고수익률 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.계좌_저점최고가 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.자동매도Check = new System.Windows.Forms.CheckBox();
            this.이익보전Check = new System.Windows.Forms.CheckBox();
            this.손절Check = new System.Windows.Forms.CheckBox();
            this.트레일링Check = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.accountBox = new System.Windows.Forms.ComboBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.stockNameText = new System.Windows.Forms.TextBox();
            this.stockButton = new System.Windows.Forms.Button();
            this.종목명 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.누적체결 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.누적매도호가변동 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.정적호가분석 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.공격 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.balanceDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // axKHOpenAPI1
            // 
            this.axKHOpenAPI1.Enabled = true;
            this.axKHOpenAPI1.Location = new System.Drawing.Point(155, 185);
            this.axKHOpenAPI1.Name = "axKHOpenAPI1";
            this.axKHOpenAPI1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axKHOpenAPI1.OcxState")));
            this.axKHOpenAPI1.Size = new System.Drawing.Size(74, 31);
            this.axKHOpenAPI1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(3, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(544, 568);
            this.tabControl1.TabIndex = 22;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.stockButton);
            this.tabPage3.Controls.Add(this.stockNameText);
            this.tabPage3.Controls.Add(this.dataGridView);
            this.tabPage3.Controls.Add(this.axKHOpenAPI1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(536, 542);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "체결상세";
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
            this.누적체결,
            this.Column3,
            this.누적매도호가변동,
            this.정적호가분석,
            this.공격});
            this.dataGridView.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.dataGridView.Location = new System.Drawing.Point(6, 64);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(505, 472);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridViewkey);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.yesModeCheck);
            this.tabPage4.Controls.Add(this.label12);
            this.tabPage4.Controls.Add(this.searchNameLabel2);
            this.tabPage4.Controls.Add(this.buyButton);
            this.tabPage4.Controls.Add(this.stockQtyBox);
            this.tabPage4.Controls.Add(this.searchNameLabel1);
            this.tabPage4.Controls.Add(this.textBox7);
            this.tabPage4.Controls.Add(this.손절율TextBox);
            this.tabPage4.Controls.Add(this.트레일링기준TextBox);
            this.tabPage4.Controls.Add(this.textBox4);
            this.tabPage4.Controls.Add(this.트레일링익절율TextBox);
            this.tabPage4.Controls.Add(this.이익보전율TextBox);
            this.tabPage4.Controls.Add(this.이익보전조건TextBox);
            this.tabPage4.Controls.Add(this.sellButton);
            this.tabPage4.Controls.Add(this.balanceDataGridView);
            this.tabPage4.Controls.Add(this.자동매도Check);
            this.tabPage4.Controls.Add(this.이익보전Check);
            this.tabPage4.Controls.Add(this.손절Check);
            this.tabPage4.Controls.Add(this.트레일링Check);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.passwordBox);
            this.tabPage4.Controls.Add(this.accountBox);
            this.tabPage4.Controls.Add(this.checkBox8);
            this.tabPage4.Controls.Add(this.checkBox7);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(536, 542);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "계좌관리";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // yesModeCheck
            // 
            this.yesModeCheck.AutoSize = true;
            this.yesModeCheck.Location = new System.Drawing.Point(441, 394);
            this.yesModeCheck.Name = "yesModeCheck";
            this.yesModeCheck.Size = new System.Drawing.Size(76, 16);
            this.yesModeCheck.TabIndex = 30;
            this.yesModeCheck.Text = "외출 모드";
            this.yesModeCheck.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(183, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 12);
            this.label12.TabIndex = 29;
            this.label12.Text = "주";
            // 
            // searchNameLabel2
            // 
            this.searchNameLabel2.AutoSize = true;
            this.searchNameLabel2.Location = new System.Drawing.Point(210, 19);
            this.searchNameLabel2.Name = "searchNameLabel2";
            this.searchNameLabel2.Size = new System.Drawing.Size(0, 12);
            this.searchNameLabel2.TabIndex = 28;
            // 
            // buyButton
            // 
            this.buyButton.BackColor = System.Drawing.Color.Coral;
            this.buyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buyButton.Location = new System.Drawing.Point(442, 40);
            this.buyButton.Name = "buyButton";
            this.buyButton.Size = new System.Drawing.Size(75, 23);
            this.buyButton.TabIndex = 27;
            this.buyButton.Text = "매수";
            this.buyButton.UseVisualStyleBackColor = false;
            this.buyButton.Click += new System.EventHandler(this.buyButton_Click);
            // 
            // stockQtyBox
            // 
            this.stockQtyBox.Location = new System.Drawing.Point(154, 39);
            this.stockQtyBox.Name = "stockQtyBox";
            this.stockQtyBox.Size = new System.Drawing.Size(48, 21);
            this.stockQtyBox.TabIndex = 26;
            this.stockQtyBox.Text = "1";
            // 
            // searchNameLabel1
            // 
            this.searchNameLabel1.AutoSize = true;
            this.searchNameLabel1.Location = new System.Drawing.Point(210, 45);
            this.searchNameLabel1.Name = "searchNameLabel1";
            this.searchNameLabel1.Size = new System.Drawing.Size(0, 12);
            this.searchNameLabel1.TabIndex = 25;
            // 
            // textBox7
            // 
            this.textBox7.Font = new System.Drawing.Font("굴림", 8F);
            this.textBox7.Location = new System.Drawing.Point(175, 460);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(39, 20);
            this.textBox7.TabIndex = 17;
            this.textBox7.Text = "-100";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // 손절율TextBox
            // 
            this.손절율TextBox.Font = new System.Drawing.Font("굴림", 8F);
            this.손절율TextBox.Location = new System.Drawing.Point(101, 413);
            this.손절율TextBox.Name = "손절율TextBox";
            this.손절율TextBox.Size = new System.Drawing.Size(30, 20);
            this.손절율TextBox.TabIndex = 16;
            this.손절율TextBox.Text = "-2.5";
            this.손절율TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // 트레일링기준TextBox
            // 
            this.트레일링기준TextBox.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.트레일링기준TextBox.Location = new System.Drawing.Point(137, 507);
            this.트레일링기준TextBox.Name = "트레일링기준TextBox";
            this.트레일링기준TextBox.Size = new System.Drawing.Size(31, 20);
            this.트레일링기준TextBox.TabIndex = 15;
            this.트레일링기준TextBox.Text = "2";
            this.트레일링기준TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("굴림", 8F);
            this.textBox4.Location = new System.Drawing.Point(192, 436);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(40, 20);
            this.textBox4.TabIndex = 14;
            this.textBox4.Text = "-0.8";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // 트레일링익절율TextBox
            // 
            this.트레일링익절율TextBox.Font = new System.Drawing.Font("굴림", 8F);
            this.트레일링익절율TextBox.Location = new System.Drawing.Point(282, 508);
            this.트레일링익절율TextBox.Name = "트레일링익절율TextBox";
            this.트레일링익절율TextBox.Size = new System.Drawing.Size(31, 20);
            this.트레일링익절율TextBox.TabIndex = 13;
            this.트레일링익절율TextBox.Text = "0.5";
            this.트레일링익절율TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // 이익보전율TextBox
            // 
            this.이익보전율TextBox.Font = new System.Drawing.Font("굴림", 8F);
            this.이익보전율TextBox.Location = new System.Drawing.Point(238, 484);
            this.이익보전율TextBox.Name = "이익보전율TextBox";
            this.이익보전율TextBox.Size = new System.Drawing.Size(31, 20);
            this.이익보전율TextBox.TabIndex = 12;
            this.이익보전율TextBox.Text = "0.5";
            this.이익보전율TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // 이익보전조건TextBox
            // 
            this.이익보전조건TextBox.Font = new System.Drawing.Font("굴림", 8F);
            this.이익보전조건TextBox.Location = new System.Drawing.Point(137, 484);
            this.이익보전조건TextBox.Name = "이익보전조건TextBox";
            this.이익보전조건TextBox.Size = new System.Drawing.Size(31, 20);
            this.이익보전조건TextBox.TabIndex = 11;
            this.이익보전조건TextBox.Text = "0.75";
            this.이익보전조건TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // sellButton
            // 
            this.sellButton.BackColor = System.Drawing.Color.SkyBlue;
            this.sellButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sellButton.Location = new System.Drawing.Point(442, 11);
            this.sellButton.Name = "sellButton";
            this.sellButton.Size = new System.Drawing.Size(76, 22);
            this.sellButton.TabIndex = 10;
            this.sellButton.Text = "매도";
            this.sellButton.UseVisualStyleBackColor = false;
            this.sellButton.Click += new System.EventHandler(this.sellButton_Click);
            // 
            // balanceDataGridView
            // 
            this.balanceDataGridView.AllowUserToAddRows = false;
            this.balanceDataGridView.AllowUserToDeleteRows = false;
            this.balanceDataGridView.AllowUserToResizeRows = false;
            this.balanceDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.balanceDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.balanceDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.balanceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.balanceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.계좌_종목명,
            this.계좌_simple,
            this.계좌_종합,
            this.계좌_위치,
            this.계좌_수익률,
            this.계좌_최고수익률,
            this.계좌_저점최고가});
            this.balanceDataGridView.Font = new System.Drawing.Font("Tahoma", 7F);
            this.balanceDataGridView.Location = new System.Drawing.Point(19, 70);
            this.balanceDataGridView.Name = "balanceDataGridView";
            this.balanceDataGridView.ReadOnly = true;
            this.balanceDataGridView.RowTemplate.Height = 23;
            this.balanceDataGridView.Size = new System.Drawing.Size(503, 316);
            this.balanceDataGridView.TabIndex = 9;
            this.balanceDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.balanceDataGridView_CellContentClick);
            // 
            // 계좌_종목명
            // 
            this.계좌_종목명.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.계좌_종목명.FillWeight = 77.1924F;
            this.계좌_종목명.HeaderText = "종목명";
            this.계좌_종목명.Name = "계좌_종목명";
            this.계좌_종목명.ReadOnly = true;
            // 
            // 계좌_simple
            // 
            this.계좌_simple.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.계좌_simple.FillWeight = 75.17681F;
            this.계좌_simple.HeaderText = "방어";
            this.계좌_simple.Name = "계좌_simple";
            this.계좌_simple.ReadOnly = true;
            // 
            // 계좌_종합
            // 
            this.계좌_종합.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.계좌_종합.FillWeight = 72.33145F;
            this.계좌_종합.HeaderText = "공격";
            this.계좌_종합.Name = "계좌_종합";
            this.계좌_종합.ReadOnly = true;
            // 
            // 계좌_위치
            // 
            this.계좌_위치.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.계좌_위치.FillWeight = 72.29483F;
            this.계좌_위치.HeaderText = "위치";
            this.계좌_위치.Name = "계좌_위치";
            this.계좌_위치.ReadOnly = true;
            // 
            // 계좌_수익률
            // 
            this.계좌_수익률.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.계좌_수익률.FillWeight = 84.04727F;
            this.계좌_수익률.HeaderText = "수익률";
            this.계좌_수익률.Name = "계좌_수익률";
            this.계좌_수익률.ReadOnly = true;
            // 
            // 계좌_최고수익률
            // 
            this.계좌_최고수익률.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.계좌_최고수익률.FillWeight = 80.88622F;
            this.계좌_최고수익률.HeaderText = "H수익률";
            this.계좌_최고수익률.Name = "계좌_최고수익률";
            this.계좌_최고수익률.ReadOnly = true;
            // 
            // 계좌_저점최고가
            // 
            this.계좌_저점최고가.FillWeight = 238.0711F;
            this.계좌_저점최고가.HeaderText = "저점최고";
            this.계좌_저점최고가.Name = "계좌_저점최고가";
            this.계좌_저점최고가.ReadOnly = true;
            this.계좌_저점최고가.Width = 70;
            // 
            // 자동매도Check
            // 
            this.자동매도Check.AutoSize = true;
            this.자동매도Check.Location = new System.Drawing.Point(19, 392);
            this.자동매도Check.Name = "자동매도Check";
            this.자동매도Check.Size = new System.Drawing.Size(100, 16);
            this.자동매도Check.TabIndex = 8;
            this.자동매도Check.Text = "자동매도 시작";
            this.자동매도Check.UseVisualStyleBackColor = true;
            // 
            // 이익보전Check
            // 
            this.이익보전Check.AutoSize = true;
            this.이익보전Check.Checked = true;
            this.이익보전Check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.이익보전Check.Location = new System.Drawing.Point(19, 488);
            this.이익보전Check.Name = "이익보전Check";
            this.이익보전Check.Size = new System.Drawing.Size(324, 16);
            this.이익보전Check.TabIndex = 7;
            this.이익보전Check.Text = "최소 이익보전 :             %넘겼을시          % 이익보전 ";
            this.이익보전Check.UseVisualStyleBackColor = true;
            // 
            // 손절Check
            // 
            this.손절Check.AutoSize = true;
            this.손절Check.Checked = true;
            this.손절Check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.손절Check.Location = new System.Drawing.Point(19, 416);
            this.손절Check.Name = "손절Check";
            this.손절Check.Size = new System.Drawing.Size(202, 16);
            this.손절Check.TabIndex = 6;
            this.손절Check.Text = "손절라인 :           % 손실시 매도";
            this.손절Check.UseVisualStyleBackColor = true;
            // 
            // 트레일링Check
            // 
            this.트레일링Check.AutoSize = true;
            this.트레일링Check.Checked = true;
            this.트레일링Check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.트레일링Check.Location = new System.Drawing.Point(19, 511);
            this.트레일링Check.Name = "트레일링Check";
            this.트레일링Check.Size = new System.Drawing.Size(416, 16);
            this.트레일링Check.TabIndex = 5;
            this.트레일링Check.Text = "트레일링 스탑 :             %이익후 저점대비           %이상 떨어질시 매도";
            this.트레일링Check.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 3;
            this.label11.Text = "비밀번호";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "계좌번호";
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(76, 39);
            this.passwordBox.MaxLength = 4;
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '*';
            this.passwordBox.Size = new System.Drawing.Size(66, 21);
            this.passwordBox.TabIndex = 1;
            this.passwordBox.Text = "071";
            this.passwordBox.TextChanged += new System.EventHandler(this.passwordBox_TextChanged);
            // 
            // accountBox
            // 
            this.accountBox.FormattingEnabled = true;
            this.accountBox.Location = new System.Drawing.Point(76, 15);
            this.accountBox.Name = "accountBox";
            this.accountBox.Size = new System.Drawing.Size(126, 20);
            this.accountBox.TabIndex = 0;
            this.accountBox.Text = "5436982110";
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Location = new System.Drawing.Point(19, 464);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(280, 16);
            this.checkBox8.TabIndex = 19;
            this.checkBox8.Text = "호가분석매도 : 종합값이            이하일시 매도";
            this.checkBox8.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(19, 440);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(336, 16);
            this.checkBox7.TabIndex = 18;
            this.checkBox7.Text = "호가분석매도 :  simple값이            이상, 0이하일시 매도";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 200;
            this.timer2.Tick += new System.EventHandler(this.timer2_tick);
            // 
            // stockNameText
            // 
            this.stockNameText.Location = new System.Drawing.Point(6, 24);
            this.stockNameText.Name = "stockNameText";
            this.stockNameText.Size = new System.Drawing.Size(154, 21);
            this.stockNameText.TabIndex = 1;
            // 
            // stockButton
            // 
            this.stockButton.Location = new System.Drawing.Point(166, 22);
            this.stockButton.Name = "stockButton";
            this.stockButton.Size = new System.Drawing.Size(75, 23);
            this.stockButton.TabIndex = 2;
            this.stockButton.Text = "추가";
            this.stockButton.UseVisualStyleBackColor = true;
            // 
            // 종목명
            // 
            this.종목명.Frozen = true;
            this.종목명.HeaderText = "종목명";
            this.종목명.Name = "종목명";
            this.종목명.ReadOnly = true;
            this.종목명.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.종목명.Width = 70;
            // 
            // 누적체결
            // 
            this.누적체결.Frozen = true;
            this.누적체결.HeaderText = "누적체결";
            this.누적체결.Name = "누적체결";
            this.누적체결.ReadOnly = true;
            this.누적체결.Width = 76;
            // 
            // Column3
            // 
            this.Column3.Frozen = true;
            this.Column3.HeaderText = "매수변동";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 76;
            // 
            // 누적매도호가변동
            // 
            this.누적매도호가변동.Frozen = true;
            this.누적매도호가변동.HeaderText = "매도변동";
            this.누적매도호가변동.Name = "누적매도호가변동";
            this.누적매도호가변동.ReadOnly = true;
            this.누적매도호가변동.Width = 76;
            // 
            // 정적호가분석
            // 
            this.정적호가분석.Frozen = true;
            this.정적호가분석.HeaderText = "방어";
            this.정적호가분석.Name = "정적호가분석";
            this.정적호가분석.ReadOnly = true;
            this.정적호가분석.Width = 60;
            // 
            // 공격
            // 
            this.공격.HeaderText = "공격";
            this.공격.Name = "공격";
            this.공격.ReadOnly = true;
            this.공격.Width = 60;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 591);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Rhodes ver1.1";
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.balanceDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxKHOpenAPILib.AxKHOpenAPI axKHOpenAPI1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.ComboBox accountBox;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox 손절율TextBox;
        private System.Windows.Forms.TextBox 트레일링기준TextBox;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox 트레일링익절율TextBox;
        private System.Windows.Forms.TextBox 이익보전율TextBox;
        private System.Windows.Forms.TextBox 이익보전조건TextBox;
        private System.Windows.Forms.Button sellButton;
        private System.Windows.Forms.DataGridView balanceDataGridView;
        private System.Windows.Forms.CheckBox 이익보전Check;
        private System.Windows.Forms.CheckBox 손절Check;
        private System.Windows.Forms.CheckBox 트레일링Check;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label searchNameLabel2;
        private System.Windows.Forms.Button buyButton;
        private System.Windows.Forms.TextBox stockQtyBox;
        private System.Windows.Forms.Label searchNameLabel1;
        private System.Windows.Forms.CheckBox 자동매도Check;
        private System.Windows.Forms.DataGridViewTextBoxColumn 계좌_종목명;
        private System.Windows.Forms.DataGridViewTextBoxColumn 계좌_simple;
        private System.Windows.Forms.DataGridViewTextBoxColumn 계좌_종합;
        private System.Windows.Forms.DataGridViewTextBoxColumn 계좌_위치;
        private System.Windows.Forms.DataGridViewTextBoxColumn 계좌_수익률;
        private System.Windows.Forms.DataGridViewTextBoxColumn 계좌_최고수익률;
        private System.Windows.Forms.DataGridViewTextBoxColumn 계좌_저점최고가;
        private System.Windows.Forms.CheckBox yesModeCheck;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button stockButton;
        private System.Windows.Forms.TextBox stockNameText;
        private System.Windows.Forms.DataGridViewTextBoxColumn 종목명;
        private System.Windows.Forms.DataGridViewTextBoxColumn 누적체결;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn 누적매도호가변동;
        private System.Windows.Forms.DataGridViewTextBoxColumn 정적호가분석;
        private System.Windows.Forms.DataGridViewTextBoxColumn 공격;
    }
}

