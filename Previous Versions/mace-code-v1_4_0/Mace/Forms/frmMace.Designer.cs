namespace Mace
{
    partial class frmMace
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMace));
            this.tabOptions = new System.Windows.Forms.TabControl();
            this.tpFeatures = new System.Windows.Forms.TabPage();
            this.tlpFeatures = new System.Windows.Forms.TableLayoutPanel();
            this.chkIncludePaths = new System.Windows.Forms.CheckBox();
            this.chkIncludeBuildings = new System.Windows.Forms.CheckBox();
            this.chkIncludeFarms = new System.Windows.Forms.CheckBox();
            this.chkIncludeMoat = new System.Windows.Forms.CheckBox();
            this.chkIncludeWalls = new System.Windows.Forms.CheckBox();
            this.chkIncludeDrawbridges = new System.Windows.Forms.CheckBox();
            this.chkIncludeNoticeboard = new System.Windows.Forms.CheckBox();
            this.chkIncludeGuardTowers = new System.Windows.Forms.CheckBox();
            this.lblSelectFeatures = new System.Windows.Forms.Label();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.tlpGeneral = new System.Windows.Forms.TableLayoutPanel();
            this.cmbCitySize = new System.Windows.Forms.ComboBox();
            this.lblCitySize = new System.Windows.Forms.Label();
            this.lblMoatType = new System.Windows.Forms.Label();
            this.cmbMoatType = new System.Windows.Forms.ComboBox();
            this.lblCityEmblem = new System.Windows.Forms.Label();
            this.cmbCityEmblem = new System.Windows.Forms.ComboBox();
            this.lblOutsideLights = new System.Windows.Forms.Label();
            this.cmbOutsideLights = new System.Windows.Forms.ComboBox();
            this.lblFireBeacons = new System.Windows.Forms.Label();
            this.cmbFireBeacons = new System.Windows.Forms.ComboBox();
            this.tpLog = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnGenerateCity = new System.Windows.Forms.Button();
            this.picMace = new System.Windows.Forms.PictureBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.lblProgressBack = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.tabOptions.SuspendLayout();
            this.tpFeatures.SuspendLayout();
            this.tlpFeatures.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.tlpGeneral.SuspendLayout();
            this.tpLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMace)).BeginInit();
            this.SuspendLayout();
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.tpFeatures);
            this.tabOptions.Controls.Add(this.tpGeneral);
            this.tabOptions.Controls.Add(this.tpLog);
            this.tabOptions.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabOptions.Location = new System.Drawing.Point(12, 80);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.SelectedIndex = 0;
            this.tabOptions.Size = new System.Drawing.Size(304, 296);
            this.tabOptions.TabIndex = 0;
            this.tabOptions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabOptions_KeyDown);
            // 
            // tpFeatures
            // 
            this.tpFeatures.Controls.Add(this.tlpFeatures);
            this.tpFeatures.Controls.Add(this.lblSelectFeatures);
            this.tpFeatures.Location = new System.Drawing.Point(4, 25);
            this.tpFeatures.Name = "tpFeatures";
            this.tpFeatures.Padding = new System.Windows.Forms.Padding(3);
            this.tpFeatures.Size = new System.Drawing.Size(296, 267);
            this.tpFeatures.TabIndex = 1;
            this.tpFeatures.Text = "Features";
            this.tpFeatures.UseVisualStyleBackColor = true;
            // 
            // tlpFeatures
            // 
            this.tlpFeatures.ColumnCount = 2;
            this.tlpFeatures.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpFeatures.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFeatures.Controls.Add(this.chkIncludePaths, 0, 7);
            this.tlpFeatures.Controls.Add(this.chkIncludeBuildings, 0, 6);
            this.tlpFeatures.Controls.Add(this.chkIncludeFarms, 0, 0);
            this.tlpFeatures.Controls.Add(this.chkIncludeMoat, 0, 1);
            this.tlpFeatures.Controls.Add(this.chkIncludeWalls, 0, 4);
            this.tlpFeatures.Controls.Add(this.chkIncludeDrawbridges, 1, 1);
            this.tlpFeatures.Controls.Add(this.chkIncludeNoticeboard, 1, 5);
            this.tlpFeatures.Controls.Add(this.chkIncludeGuardTowers, 0, 3);
            this.tlpFeatures.Location = new System.Drawing.Point(15, 34);
            this.tlpFeatures.Name = "tlpFeatures";
            this.tlpFeatures.RowCount = 8;
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpFeatures.Size = new System.Drawing.Size(258, 216);
            this.tlpFeatures.TabIndex = 0;
            // 
            // chkIncludePaths
            // 
            this.chkIncludePaths.AutoSize = true;
            this.chkIncludePaths.BackColor = System.Drawing.Color.Transparent;
            this.chkIncludePaths.Checked = true;
            this.chkIncludePaths.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tlpFeatures.SetColumnSpan(this.chkIncludePaths, 2);
            this.chkIncludePaths.Location = new System.Drawing.Point(3, 185);
            this.chkIncludePaths.Name = "chkIncludePaths";
            this.chkIncludePaths.Size = new System.Drawing.Size(61, 20);
            this.chkIncludePaths.TabIndex = 7;
            this.chkIncludePaths.Text = "&Paths";
            this.chkIncludePaths.UseVisualStyleBackColor = false;
            // 
            // chkIncludeBuildings
            // 
            this.chkIncludeBuildings.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkIncludeBuildings.AutoSize = true;
            this.chkIncludeBuildings.BackColor = System.Drawing.Color.Transparent;
            this.chkIncludeBuildings.Checked = true;
            this.chkIncludeBuildings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tlpFeatures.SetColumnSpan(this.chkIncludeBuildings, 2);
            this.chkIncludeBuildings.Location = new System.Drawing.Point(3, 159);
            this.chkIncludeBuildings.Name = "chkIncludeBuildings";
            this.chkIncludeBuildings.Size = new System.Drawing.Size(80, 20);
            this.chkIncludeBuildings.TabIndex = 6;
            this.chkIncludeBuildings.Text = "&Buildings";
            this.chkIncludeBuildings.UseVisualStyleBackColor = false;
            // 
            // chkIncludeFarms
            // 
            this.chkIncludeFarms.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkIncludeFarms.AutoSize = true;
            this.chkIncludeFarms.BackColor = System.Drawing.Color.Transparent;
            this.chkIncludeFarms.Checked = true;
            this.chkIncludeFarms.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tlpFeatures.SetColumnSpan(this.chkIncludeFarms, 2);
            this.chkIncludeFarms.Location = new System.Drawing.Point(3, 3);
            this.chkIncludeFarms.Name = "chkIncludeFarms";
            this.chkIncludeFarms.Size = new System.Drawing.Size(64, 20);
            this.chkIncludeFarms.TabIndex = 0;
            this.chkIncludeFarms.Text = "&Farms";
            this.chkIncludeFarms.UseVisualStyleBackColor = false;
            // 
            // chkIncludeMoat
            // 
            this.chkIncludeMoat.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkIncludeMoat.AutoSize = true;
            this.chkIncludeMoat.BackColor = System.Drawing.Color.Transparent;
            this.chkIncludeMoat.Checked = true;
            this.chkIncludeMoat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tlpFeatures.SetColumnSpan(this.chkIncludeMoat, 2);
            this.chkIncludeMoat.Location = new System.Drawing.Point(3, 29);
            this.chkIncludeMoat.Name = "chkIncludeMoat";
            this.chkIncludeMoat.Size = new System.Drawing.Size(56, 20);
            this.chkIncludeMoat.TabIndex = 1;
            this.chkIncludeMoat.Text = "&Moat";
            this.chkIncludeMoat.UseVisualStyleBackColor = false;
            // 
            // chkIncludeWalls
            // 
            this.chkIncludeWalls.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkIncludeWalls.AutoSize = true;
            this.chkIncludeWalls.BackColor = System.Drawing.Color.Transparent;
            this.chkIncludeWalls.Checked = true;
            this.chkIncludeWalls.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tlpFeatures.SetColumnSpan(this.chkIncludeWalls, 2);
            this.chkIncludeWalls.Location = new System.Drawing.Point(3, 107);
            this.chkIncludeWalls.Name = "chkIncludeWalls";
            this.chkIncludeWalls.Size = new System.Drawing.Size(60, 20);
            this.chkIncludeWalls.TabIndex = 4;
            this.chkIncludeWalls.Text = "&Walls";
            this.chkIncludeWalls.UseVisualStyleBackColor = false;
            this.chkIncludeWalls.CheckedChanged += new System.EventHandler(this.chkIncludeWalls_CheckedChanged);
            // 
            // chkIncludeDrawbridges
            // 
            this.chkIncludeDrawbridges.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkIncludeDrawbridges.BackColor = System.Drawing.Color.Transparent;
            this.chkIncludeDrawbridges.Checked = true;
            this.chkIncludeDrawbridges.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tlpFeatures.SetColumnSpan(this.chkIncludeDrawbridges, 2);
            this.chkIncludeDrawbridges.Location = new System.Drawing.Point(3, 55);
            this.chkIncludeDrawbridges.Name = "chkIncludeDrawbridges";
            this.chkIncludeDrawbridges.Size = new System.Drawing.Size(98, 20);
            this.chkIncludeDrawbridges.TabIndex = 2;
            this.chkIncludeDrawbridges.Text = "&Drawbridges";
            this.chkIncludeDrawbridges.UseVisualStyleBackColor = false;
            // 
            // chkIncludeNoticeboard
            // 
            this.chkIncludeNoticeboard.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkIncludeNoticeboard.AutoSize = true;
            this.chkIncludeNoticeboard.BackColor = System.Drawing.Color.Transparent;
            this.chkIncludeNoticeboard.Checked = true;
            this.chkIncludeNoticeboard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeNoticeboard.Location = new System.Drawing.Point(23, 133);
            this.chkIncludeNoticeboard.Name = "chkIncludeNoticeboard";
            this.chkIncludeNoticeboard.Size = new System.Drawing.Size(96, 20);
            this.chkIncludeNoticeboard.TabIndex = 5;
            this.chkIncludeNoticeboard.Text = "&Noticeboard";
            this.chkIncludeNoticeboard.UseVisualStyleBackColor = false;
            // 
            // chkIncludeGuardTowers
            // 
            this.chkIncludeGuardTowers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkIncludeGuardTowers.AutoSize = true;
            this.chkIncludeGuardTowers.BackColor = System.Drawing.Color.Transparent;
            this.chkIncludeGuardTowers.Checked = true;
            this.chkIncludeGuardTowers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tlpFeatures.SetColumnSpan(this.chkIncludeGuardTowers, 2);
            this.chkIncludeGuardTowers.Location = new System.Drawing.Point(3, 81);
            this.chkIncludeGuardTowers.Name = "chkIncludeGuardTowers";
            this.chkIncludeGuardTowers.Size = new System.Drawing.Size(104, 20);
            this.chkIncludeGuardTowers.TabIndex = 3;
            this.chkIncludeGuardTowers.Text = "G&uard towers";
            this.chkIncludeGuardTowers.UseVisualStyleBackColor = false;
            // 
            // lblSelectFeatures
            // 
            this.lblSelectFeatures.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSelectFeatures.AutoSize = true;
            this.lblSelectFeatures.Location = new System.Drawing.Point(6, 10);
            this.lblSelectFeatures.Name = "lblSelectFeatures";
            this.lblSelectFeatures.Size = new System.Drawing.Size(242, 16);
            this.lblSelectFeatures.TabIndex = 0;
            this.lblSelectFeatures.Text = "Select the features to include in the city:";
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.tlpGeneral);
            this.tpGeneral.Location = new System.Drawing.Point(4, 25);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tpGeneral.Size = new System.Drawing.Size(296, 267);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            // 
            // tlpGeneral
            // 
            this.tlpGeneral.ColumnCount = 2;
            this.tlpGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.82759F));
            this.tlpGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.17242F));
            this.tlpGeneral.Controls.Add(this.cmbCitySize, 1, 0);
            this.tlpGeneral.Controls.Add(this.lblCitySize, 0, 0);
            this.tlpGeneral.Controls.Add(this.lblMoatType, 0, 1);
            this.tlpGeneral.Controls.Add(this.cmbMoatType, 1, 1);
            this.tlpGeneral.Controls.Add(this.lblCityEmblem, 0, 2);
            this.tlpGeneral.Controls.Add(this.cmbCityEmblem, 1, 2);
            this.tlpGeneral.Controls.Add(this.lblOutsideLights, 0, 3);
            this.tlpGeneral.Controls.Add(this.cmbOutsideLights, 1, 3);
            this.tlpGeneral.Controls.Add(this.lblFireBeacons, 0, 4);
            this.tlpGeneral.Controls.Add(this.cmbFireBeacons, 1, 4);
            this.tlpGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGeneral.Location = new System.Drawing.Point(3, 3);
            this.tlpGeneral.Name = "tlpGeneral";
            this.tlpGeneral.RowCount = 7;
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpGeneral.Size = new System.Drawing.Size(290, 261);
            this.tlpGeneral.TabIndex = 1;
            // 
            // cmbCitySize
            // 
            this.cmbCitySize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCitySize.FormattingEnabled = true;
            this.cmbCitySize.Items.AddRange(new object[] {
            "Random",
            "------------------------------------",
            "Very small",
            "Small",
            "Medium",
            "Large",
            "Very large"});
            this.cmbCitySize.Location = new System.Drawing.Point(104, 3);
            this.cmbCitySize.Name = "cmbCitySize";
            this.cmbCitySize.Size = new System.Drawing.Size(183, 24);
            this.cmbCitySize.TabIndex = 0;
            this.cmbCitySize.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
            // 
            // lblCitySize
            // 
            this.lblCitySize.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCitySize.AutoSize = true;
            this.lblCitySize.Location = new System.Drawing.Point(3, 7);
            this.lblCitySize.Name = "lblCitySize";
            this.lblCitySize.Size = new System.Drawing.Size(59, 16);
            this.lblCitySize.TabIndex = 2;
            this.lblCitySize.Text = "&City size";
            this.lblCitySize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMoatType
            // 
            this.lblMoatType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMoatType.AutoSize = true;
            this.lblMoatType.Location = new System.Drawing.Point(3, 37);
            this.lblMoatType.Name = "lblMoatType";
            this.lblMoatType.Size = new System.Drawing.Size(66, 16);
            this.lblMoatType.TabIndex = 2;
            this.lblMoatType.Text = "&Moat type";
            this.lblMoatType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbMoatType
            // 
            this.cmbMoatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMoatType.FormattingEnabled = true;
            this.cmbMoatType.Items.AddRange(new object[] {
            "Random",
            "------------------------------------",
            "Water",
            "Lava",
            "Cactus",
            "Drop to Bedrock"});
            this.cmbMoatType.Location = new System.Drawing.Point(104, 33);
            this.cmbMoatType.Name = "cmbMoatType";
            this.cmbMoatType.Size = new System.Drawing.Size(183, 24);
            this.cmbMoatType.TabIndex = 1;
            this.cmbMoatType.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
            // 
            // lblCityEmblem
            // 
            this.lblCityEmblem.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCityEmblem.AutoSize = true;
            this.lblCityEmblem.Location = new System.Drawing.Point(3, 67);
            this.lblCityEmblem.Name = "lblCityEmblem";
            this.lblCityEmblem.Size = new System.Drawing.Size(81, 16);
            this.lblCityEmblem.TabIndex = 2;
            this.lblCityEmblem.Text = "City &emblem";
            this.lblCityEmblem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCityEmblem
            // 
            this.cmbCityEmblem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCityEmblem.FormattingEnabled = true;
            this.cmbCityEmblem.Items.AddRange(new object[] {
            "Random",
            "------------------------------------",
            "None",
            "------------------------------------",
            "Cross",
            "England Flag",
            "Pride Flag",
            "Shield",
            "Yin and Yang"});
            this.cmbCityEmblem.Location = new System.Drawing.Point(104, 63);
            this.cmbCityEmblem.Name = "cmbCityEmblem";
            this.cmbCityEmblem.Size = new System.Drawing.Size(183, 24);
            this.cmbCityEmblem.TabIndex = 2;
            this.cmbCityEmblem.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
            // 
            // lblOutsideLights
            // 
            this.lblOutsideLights.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblOutsideLights.AutoSize = true;
            this.lblOutsideLights.Location = new System.Drawing.Point(3, 97);
            this.lblOutsideLights.Name = "lblOutsideLights";
            this.lblOutsideLights.Size = new System.Drawing.Size(88, 16);
            this.lblOutsideLights.TabIndex = 2;
            this.lblOutsideLights.Text = "&Outside lights";
            this.lblOutsideLights.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbOutsideLights
            // 
            this.cmbOutsideLights.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOutsideLights.FormattingEnabled = true;
            this.cmbOutsideLights.Items.AddRange(new object[] {
            "Random",
            "------------------------------------",
            "None",
            "------------------------------------",
            "Fire",
            "Torches"});
            this.cmbOutsideLights.Location = new System.Drawing.Point(104, 93);
            this.cmbOutsideLights.Name = "cmbOutsideLights";
            this.cmbOutsideLights.Size = new System.Drawing.Size(183, 24);
            this.cmbOutsideLights.TabIndex = 3;
            this.cmbOutsideLights.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
            // 
            // lblFireBeacons
            // 
            this.lblFireBeacons.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFireBeacons.AutoSize = true;
            this.lblFireBeacons.Location = new System.Drawing.Point(3, 127);
            this.lblFireBeacons.Name = "lblFireBeacons";
            this.lblFireBeacons.Size = new System.Drawing.Size(83, 16);
            this.lblFireBeacons.TabIndex = 2;
            this.lblFireBeacons.Text = "&Fire beacons";
            this.lblFireBeacons.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbFireBeacons
            // 
            this.cmbFireBeacons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFireBeacons.FormattingEnabled = true;
            this.cmbFireBeacons.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cmbFireBeacons.Location = new System.Drawing.Point(104, 123);
            this.cmbFireBeacons.Name = "cmbFireBeacons";
            this.cmbFireBeacons.Size = new System.Drawing.Size(183, 24);
            this.cmbFireBeacons.TabIndex = 3;
            this.cmbFireBeacons.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
            // 
            // tpLog
            // 
            this.tpLog.Controls.Add(this.txtLog);
            this.tpLog.Location = new System.Drawing.Point(4, 25);
            this.tpLog.Name = "tpLog";
            this.tpLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpLog.Size = new System.Drawing.Size(296, 267);
            this.tpLog.TabIndex = 2;
            this.tpLog.Text = "Log";
            this.tpLog.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(6, 6);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(284, 255);
            this.txtLog.TabIndex = 0;
            // 
            // btnGenerateCity
            // 
            this.btnGenerateCity.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGenerateCity.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateCity.Location = new System.Drawing.Point(194, 384);
            this.btnGenerateCity.Name = "btnGenerateCity";
            this.btnGenerateCity.Size = new System.Drawing.Size(122, 27);
            this.btnGenerateCity.TabIndex = 2;
            this.btnGenerateCity.Text = "&Generate City";
            this.btnGenerateCity.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnGenerateCity.UseVisualStyleBackColor = true;
            this.btnGenerateCity.Click += new System.EventHandler(this.btnGenerateCity_Click);
            // 
            // picMace
            // 
            this.picMace.BackColor = System.Drawing.Color.Black;
            this.picMace.Location = new System.Drawing.Point(12, 12);
            this.picMace.Name = "picMace";
            this.picMace.Size = new System.Drawing.Size(301, 55);
            this.picMace.TabIndex = 4;
            this.picMace.TabStop = false;
            this.picMace.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMace_MouseDown);
            // 
            // btnAbout
            // 
            this.btnAbout.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAbout.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbout.Location = new System.Drawing.Point(12, 384);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(27, 27);
            this.btnAbout.TabIndex = 1;
            this.btnAbout.Text = "?";
            this.btnAbout.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // lblProgressBack
            // 
            this.lblProgressBack.BackColor = System.Drawing.Color.Black;
            this.lblProgressBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressBack.Location = new System.Drawing.Point(45, 392);
            this.lblProgressBack.Name = "lblProgressBack";
            this.lblProgressBack.Size = new System.Drawing.Size(142, 12);
            this.lblProgressBack.TabIndex = 5;
            this.lblProgressBack.Visible = false;
            // 
            // lblProgress
            // 
            this.lblProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.Location = new System.Drawing.Point(45, 392);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(142, 12);
            this.lblProgress.TabIndex = 5;
            this.lblProgress.Visible = false;
            // 
            // frmMace
            // 
            this.AcceptButton = this.btnGenerateCity;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 421);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.lblProgressBack);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnGenerateCity);
            this.Controls.Add(this.tabOptions);
            this.Controls.Add(this.picMace);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.tabOptions.ResumeLayout(false);
            this.tpFeatures.ResumeLayout(false);
            this.tpFeatures.PerformLayout();
            this.tlpFeatures.ResumeLayout(false);
            this.tlpFeatures.PerformLayout();
            this.tpGeneral.ResumeLayout(false);
            this.tlpGeneral.ResumeLayout(false);
            this.tlpGeneral.PerformLayout();
            this.tpLog.ResumeLayout(false);
            this.tpLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMace)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabOptions;
        private System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.TabPage tpFeatures;
        private System.Windows.Forms.TabPage tpLog;
        private System.Windows.Forms.TableLayoutPanel tlpGeneral;
        private System.Windows.Forms.ComboBox cmbCitySize;
        private System.Windows.Forms.Label lblCitySize;
        private System.Windows.Forms.TableLayoutPanel tlpFeatures;
        private System.Windows.Forms.CheckBox chkIncludeBuildings;
        private System.Windows.Forms.CheckBox chkIncludeNoticeboard;
        private System.Windows.Forms.CheckBox chkIncludeGuardTowers;
        private System.Windows.Forms.CheckBox chkIncludeWalls;
        private System.Windows.Forms.CheckBox chkIncludeFarms;
        private System.Windows.Forms.CheckBox chkIncludeMoat;
        private System.Windows.Forms.CheckBox chkIncludePaths;
        private System.Windows.Forms.Label lblSelectFeatures;
        private System.Windows.Forms.Label lblMoatType;
        private System.Windows.Forms.ComboBox cmbMoatType;
        private System.Windows.Forms.Button btnGenerateCity;
        private System.Windows.Forms.PictureBox picMace;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Label lblProgressBack;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.CheckBox chkIncludeDrawbridges;
        private System.Windows.Forms.Label lblCityEmblem;
        private System.Windows.Forms.ComboBox cmbCityEmblem;
        private System.Windows.Forms.Label lblOutsideLights;
        private System.Windows.Forms.ComboBox cmbOutsideLights;
        private System.Windows.Forms.Label lblFireBeacons;
        private System.Windows.Forms.ComboBox cmbFireBeacons;

    }
}

