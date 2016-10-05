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
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.tlpGeneral = new System.Windows.Forms.TableLayoutPanel();
            this.cmbWallMaterial = new System.Windows.Forms.ComboBox();
            this.lblWallMaterial = new System.Windows.Forms.Label();
            this.cmbCitySize = new System.Windows.Forms.ComboBox();
            this.lblCitySize = new System.Windows.Forms.Label();
            this.lblMoatType = new System.Windows.Forms.Label();
            this.cmbMoatType = new System.Windows.Forms.ComboBox();
            this.lblCityEmblem = new System.Windows.Forms.Label();
            this.cmbCityEmblem = new System.Windows.Forms.ComboBox();
            this.lblOutsideLights = new System.Windows.Forms.Label();
            this.cmbOutsideLights = new System.Windows.Forms.ComboBox();
            this.lblTowerAddition = new System.Windows.Forms.Label();
            this.cmbTowerAddition = new System.Windows.Forms.ComboBox();
            this.lblWorldSeed = new System.Windows.Forms.Label();
            this.lblCitySeed = new System.Windows.Forms.Label();
            this.lblCityName = new System.Windows.Forms.Label();
            this.txtWorldSeed = new System.Windows.Forms.TextBox();
            this.txtCitySeed = new System.Windows.Forms.TextBox();
            this.txtCityName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tpFeatures = new System.Windows.Forms.TabPage();
            this.tlpFeatures = new System.Windows.Forms.TableLayoutPanel();
            this.chkExportSchematic = new System.Windows.Forms.CheckBox();
            this.lblBreaker = new System.Windows.Forms.Label();
            this.chkIncludeFarms = new System.Windows.Forms.CheckBox();
            this.chkIncludeMoat = new System.Windows.Forms.CheckBox();
            this.chkIncludeWalls = new System.Windows.Forms.CheckBox();
            this.chkIncludeDrawbridges = new System.Windows.Forms.CheckBox();
            this.chkIncludeGuardTowers = new System.Windows.Forms.CheckBox();
            this.chkIncludeBuildings = new System.Windows.Forms.CheckBox();
            this.chkValuableBlocks = new System.Windows.Forms.CheckBox();
            this.chkItemsInChests = new System.Windows.Forms.CheckBox();
            this.chkIncludeMineshaft = new System.Windows.Forms.CheckBox();
            this.lblSelectFeatures = new System.Windows.Forms.Label();
            this.tpNPCs = new System.Windows.Forms.TabPage();
            this.picNPC = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chkIncludeSpawners = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lnkRisugamiModLoader = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.lnkGhostdancerMobsForMace = new System.Windows.Forms.LinkLabel();
            this.tpLog = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnGenerateCity = new System.Windows.Forms.Button();
            this.picMace = new System.Windows.Forms.PictureBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.lblProgressBack = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.tabOptions.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.tlpGeneral.SuspendLayout();
            this.tpFeatures.SuspendLayout();
            this.tlpFeatures.SuspendLayout();
            this.tpNPCs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNPC)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tpLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMace)).BeginInit();
            this.SuspendLayout();
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.tpGeneral);
            this.tabOptions.Controls.Add(this.tpFeatures);
            this.tabOptions.Controls.Add(this.tpNPCs);
            this.tabOptions.Controls.Add(this.tpLog);
            this.tabOptions.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabOptions.Location = new System.Drawing.Point(12, 79);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.SelectedIndex = 0;
            this.tabOptions.Size = new System.Drawing.Size(304, 377);
            this.tabOptions.TabIndex = 0;
            this.tabOptions.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            this.tabOptions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabOptions_KeyDown);
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.tlpGeneral);
            this.tpGeneral.Location = new System.Drawing.Point(4, 25);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tpGeneral.Size = new System.Drawing.Size(296, 348);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            this.tpGeneral.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // tlpGeneral
            // 
            this.tlpGeneral.ColumnCount = 2;
            this.tlpGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.82759F));
            this.tlpGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.17241F));
            this.tlpGeneral.Controls.Add(this.cmbWallMaterial, 1, 5);
            this.tlpGeneral.Controls.Add(this.lblWallMaterial, 0, 5);
            this.tlpGeneral.Controls.Add(this.cmbCitySize, 1, 0);
            this.tlpGeneral.Controls.Add(this.lblCitySize, 0, 0);
            this.tlpGeneral.Controls.Add(this.lblMoatType, 0, 1);
            this.tlpGeneral.Controls.Add(this.cmbMoatType, 1, 1);
            this.tlpGeneral.Controls.Add(this.lblCityEmblem, 0, 2);
            this.tlpGeneral.Controls.Add(this.cmbCityEmblem, 1, 2);
            this.tlpGeneral.Controls.Add(this.lblOutsideLights, 0, 3);
            this.tlpGeneral.Controls.Add(this.cmbOutsideLights, 1, 3);
            this.tlpGeneral.Controls.Add(this.lblTowerAddition, 0, 4);
            this.tlpGeneral.Controls.Add(this.cmbTowerAddition, 1, 4);
            this.tlpGeneral.Controls.Add(this.lblWorldSeed, 0, 9);
            this.tlpGeneral.Controls.Add(this.lblCitySeed, 0, 8);
            this.tlpGeneral.Controls.Add(this.lblCityName, 0, 7);
            this.tlpGeneral.Controls.Add(this.txtWorldSeed, 1, 9);
            this.tlpGeneral.Controls.Add(this.txtCitySeed, 1, 8);
            this.tlpGeneral.Controls.Add(this.txtCityName, 1, 7);
            this.tlpGeneral.Controls.Add(this.label3, 0, 6);
            this.tlpGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGeneral.Location = new System.Drawing.Point(3, 3);
            this.tlpGeneral.Name = "tlpGeneral";
            this.tlpGeneral.RowCount = 11;
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.Size = new System.Drawing.Size(290, 342);
            this.tlpGeneral.TabIndex = 1;
            this.tlpGeneral.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // cmbWallMaterial
            // 
            this.cmbWallMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbWallMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWallMaterial.FormattingEnabled = true;
            this.cmbWallMaterial.Items.AddRange(new object[] {
            "Random",
            "------------------------------------",
            "Bedrock",
            "Brick",
            "Cobblestone",
            "Diamond",
            "Dirt",
            "Glass",
            "Glowstone",
            "Gold",
            "Ice",
            "Jack-o-Lantern",
            "Mossy Cobblestone",
            "Netherrack",
            "Obsidian",
            "Sandstone",
            "Snow",
            "Soul sand",
            "Stone",
            "Wood Logs",
            "Wood Planks"});
            this.cmbWallMaterial.Location = new System.Drawing.Point(104, 153);
            this.cmbWallMaterial.Name = "cmbWallMaterial";
            this.cmbWallMaterial.Size = new System.Drawing.Size(183, 24);
            this.cmbWallMaterial.TabIndex = 5;
            this.cmbWallMaterial.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
            this.cmbWallMaterial.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // lblWallMaterial
            // 
            this.lblWallMaterial.AutoSize = true;
            this.lblWallMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWallMaterial.Location = new System.Drawing.Point(3, 150);
            this.lblWallMaterial.Name = "lblWallMaterial";
            this.lblWallMaterial.Size = new System.Drawing.Size(95, 30);
            this.lblWallMaterial.TabIndex = 9;
            this.lblWallMaterial.Text = "W&all material";
            this.lblWallMaterial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblWallMaterial.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // cmbCitySize
            // 
            this.cmbCitySize.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.cmbCitySize.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // lblCitySize
            // 
            this.lblCitySize.AutoSize = true;
            this.lblCitySize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCitySize.Location = new System.Drawing.Point(3, 0);
            this.lblCitySize.Name = "lblCitySize";
            this.lblCitySize.Size = new System.Drawing.Size(95, 30);
            this.lblCitySize.TabIndex = 2;
            this.lblCitySize.Text = "&City size";
            this.lblCitySize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCitySize.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // lblMoatType
            // 
            this.lblMoatType.AutoSize = true;
            this.lblMoatType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMoatType.Location = new System.Drawing.Point(3, 30);
            this.lblMoatType.Name = "lblMoatType";
            this.lblMoatType.Size = new System.Drawing.Size(95, 30);
            this.lblMoatType.TabIndex = 2;
            this.lblMoatType.Text = "&Moat type";
            this.lblMoatType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMoatType.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // cmbMoatType
            // 
            this.cmbMoatType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbMoatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMoatType.FormattingEnabled = true;
            this.cmbMoatType.Items.AddRange(new object[] {
            "Random",
            "------------------------------------",
            "Cactus",
            "Drop to Bedrock",
            "Fire",
            "Lava",
            "Water"});
            this.cmbMoatType.Location = new System.Drawing.Point(104, 33);
            this.cmbMoatType.Name = "cmbMoatType";
            this.cmbMoatType.Size = new System.Drawing.Size(183, 24);
            this.cmbMoatType.TabIndex = 1;
            this.cmbMoatType.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
            this.cmbMoatType.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // lblCityEmblem
            // 
            this.lblCityEmblem.AutoSize = true;
            this.lblCityEmblem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCityEmblem.Location = new System.Drawing.Point(3, 60);
            this.lblCityEmblem.Name = "lblCityEmblem";
            this.lblCityEmblem.Size = new System.Drawing.Size(95, 30);
            this.lblCityEmblem.TabIndex = 2;
            this.lblCityEmblem.Text = "City &emblem";
            this.lblCityEmblem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCityEmblem.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // cmbCityEmblem
            // 
            this.cmbCityEmblem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCityEmblem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCityEmblem.FormattingEnabled = true;
            this.cmbCityEmblem.Items.AddRange(new object[] {
            "Random",
            "------------------------------------",
            "None",
            "------------------------------------"});
            this.cmbCityEmblem.Location = new System.Drawing.Point(104, 63);
            this.cmbCityEmblem.Name = "cmbCityEmblem";
            this.cmbCityEmblem.Size = new System.Drawing.Size(183, 24);
            this.cmbCityEmblem.TabIndex = 2;
            this.cmbCityEmblem.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
            this.cmbCityEmblem.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // lblOutsideLights
            // 
            this.lblOutsideLights.AutoSize = true;
            this.lblOutsideLights.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOutsideLights.Location = new System.Drawing.Point(3, 90);
            this.lblOutsideLights.Name = "lblOutsideLights";
            this.lblOutsideLights.Size = new System.Drawing.Size(95, 30);
            this.lblOutsideLights.TabIndex = 2;
            this.lblOutsideLights.Text = "&Outside lights";
            this.lblOutsideLights.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblOutsideLights.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // cmbOutsideLights
            // 
            this.cmbOutsideLights.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.cmbOutsideLights.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // lblTowerAddition
            // 
            this.lblTowerAddition.AutoSize = true;
            this.lblTowerAddition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTowerAddition.Location = new System.Drawing.Point(3, 120);
            this.lblTowerAddition.Name = "lblTowerAddition";
            this.lblTowerAddition.Size = new System.Drawing.Size(95, 30);
            this.lblTowerAddition.TabIndex = 2;
            this.lblTowerAddition.Text = "&Tower addition";
            this.lblTowerAddition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTowerAddition.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // cmbTowerAddition
            // 
            this.cmbTowerAddition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTowerAddition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTowerAddition.FormattingEnabled = true;
            this.cmbTowerAddition.Items.AddRange(new object[] {
            "Random",
            "------------------------------------",
            "None",
            "------------------------------------",
            "Fire beacon",
            "Flag"});
            this.cmbTowerAddition.Location = new System.Drawing.Point(104, 123);
            this.cmbTowerAddition.Name = "cmbTowerAddition";
            this.cmbTowerAddition.Size = new System.Drawing.Size(183, 24);
            this.cmbTowerAddition.TabIndex = 4;
            this.cmbTowerAddition.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
            this.cmbTowerAddition.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // lblWorldSeed
            // 
            this.lblWorldSeed.AutoSize = true;
            this.lblWorldSeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWorldSeed.Location = new System.Drawing.Point(3, 270);
            this.lblWorldSeed.Name = "lblWorldSeed";
            this.lblWorldSeed.Size = new System.Drawing.Size(95, 30);
            this.lblWorldSeed.TabIndex = 5;
            this.lblWorldSeed.Text = "&World seed";
            this.lblWorldSeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblWorldSeed.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // lblCitySeed
            // 
            this.lblCitySeed.AutoSize = true;
            this.lblCitySeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCitySeed.Location = new System.Drawing.Point(3, 240);
            this.lblCitySeed.Name = "lblCitySeed";
            this.lblCitySeed.Size = new System.Drawing.Size(95, 30);
            this.lblCitySeed.TabIndex = 5;
            this.lblCitySeed.Text = "City &seed";
            this.lblCitySeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCitySeed.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // lblCityName
            // 
            this.lblCityName.AutoSize = true;
            this.lblCityName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCityName.Location = new System.Drawing.Point(3, 210);
            this.lblCityName.Name = "lblCityName";
            this.lblCityName.Size = new System.Drawing.Size(95, 30);
            this.lblCityName.TabIndex = 5;
            this.lblCityName.Text = "City &name";
            this.lblCityName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCityName.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // txtWorldSeed
            // 
            this.txtWorldSeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWorldSeed.Location = new System.Drawing.Point(104, 273);
            this.txtWorldSeed.Name = "txtWorldSeed";
            this.txtWorldSeed.Size = new System.Drawing.Size(183, 22);
            this.txtWorldSeed.TabIndex = 8;
            this.txtWorldSeed.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            this.txtWorldSeed.Enter += new System.EventHandler(this.txtCityName_Enter);
            this.txtWorldSeed.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtCityName_Enter);
            // 
            // txtCitySeed
            // 
            this.txtCitySeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCitySeed.Location = new System.Drawing.Point(104, 243);
            this.txtCitySeed.Name = "txtCitySeed";
            this.txtCitySeed.Size = new System.Drawing.Size(183, 22);
            this.txtCitySeed.TabIndex = 7;
            this.txtCitySeed.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            this.txtCitySeed.Enter += new System.EventHandler(this.txtCityName_Enter);
            this.txtCitySeed.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtCityName_Enter);
            // 
            // txtCityName
            // 
            this.txtCityName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCityName.Location = new System.Drawing.Point(104, 213);
            this.txtCityName.Name = "txtCityName";
            this.txtCityName.Size = new System.Drawing.Size(183, 22);
            this.txtCityName.TabIndex = 6;
            this.txtCityName.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            this.txtCityName.Enter += new System.EventHandler(this.txtCityName_Enter);
            this.txtCityName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtCityName_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.tlpGeneral.SetColumnSpan(this.label3, 2);
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(284, 30);
            this.label3.TabIndex = 5;
            this.label3.Text = "Leave these blank for random values:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // tpFeatures
            // 
            this.tpFeatures.Controls.Add(this.tlpFeatures);
            this.tpFeatures.Controls.Add(this.lblSelectFeatures);
            this.tpFeatures.Location = new System.Drawing.Point(4, 25);
            this.tpFeatures.Name = "tpFeatures";
            this.tpFeatures.Padding = new System.Windows.Forms.Padding(3);
            this.tpFeatures.Size = new System.Drawing.Size(296, 348);
            this.tpFeatures.TabIndex = 1;
            this.tpFeatures.Text = "Features";
            this.tpFeatures.UseVisualStyleBackColor = true;
            this.tpFeatures.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // tlpFeatures
            // 
            this.tlpFeatures.ColumnCount = 2;
            this.tlpFeatures.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpFeatures.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFeatures.Controls.Add(this.chkExportSchematic, 0, 10);
            this.tlpFeatures.Controls.Add(this.lblBreaker, 0, 7);
            this.tlpFeatures.Controls.Add(this.chkIncludeFarms, 0, 0);
            this.tlpFeatures.Controls.Add(this.chkIncludeMoat, 0, 1);
            this.tlpFeatures.Controls.Add(this.chkIncludeWalls, 0, 4);
            this.tlpFeatures.Controls.Add(this.chkIncludeDrawbridges, 1, 1);
            this.tlpFeatures.Controls.Add(this.chkIncludeGuardTowers, 0, 3);
            this.tlpFeatures.Controls.Add(this.chkIncludeBuildings, 1, 4);
            this.tlpFeatures.Controls.Add(this.chkValuableBlocks, 0, 8);
            this.tlpFeatures.Controls.Add(this.chkItemsInChests, 1, 8);
            this.tlpFeatures.Controls.Add(this.chkIncludeMineshaft, 1, 5);
            this.tlpFeatures.Location = new System.Drawing.Point(15, 34);
            this.tlpFeatures.Name = "tlpFeatures";
            this.tlpFeatures.RowCount = 11;
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpFeatures.Size = new System.Drawing.Size(266, 297);
            this.tlpFeatures.TabIndex = 0;
            this.tlpFeatures.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // chkExportSchematic
            // 
            this.chkExportSchematic.AutoSize = true;
            this.chkExportSchematic.BackColor = System.Drawing.Color.Transparent;
            this.tlpFeatures.SetColumnSpan(this.chkExportSchematic, 2);
            this.chkExportSchematic.Location = new System.Drawing.Point(3, 263);
            this.chkExportSchematic.Name = "chkExportSchematic";
            this.chkExportSchematic.Size = new System.Drawing.Size(250, 20);
            this.chkExportSchematic.TabIndex = 10;
            this.chkExportSchematic.Text = "&Export schematic to the save directory";
            this.chkExportSchematic.UseVisualStyleBackColor = false;
            // 
            // lblBreaker
            // 
            this.lblBreaker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBreaker.AutoSize = true;
            this.lblBreaker.BackColor = System.Drawing.Color.Black;
            this.tlpFeatures.SetColumnSpan(this.lblBreaker, 2);
            this.lblBreaker.Location = new System.Drawing.Point(14, 194);
            this.lblBreaker.Margin = new System.Windows.Forms.Padding(14, 12, 14, 14);
            this.lblBreaker.Name = "lblBreaker";
            this.lblBreaker.Size = new System.Drawing.Size(238, 1);
            this.lblBreaker.TabIndex = 9;
            this.lblBreaker.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
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
            this.chkIncludeFarms.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
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
            this.chkIncludeMoat.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
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
            this.chkIncludeWalls.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
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
            this.chkIncludeDrawbridges.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
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
            this.chkIncludeGuardTowers.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // chkIncludeBuildings
            // 
            this.chkIncludeBuildings.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkIncludeBuildings.AutoSize = true;
            this.chkIncludeBuildings.BackColor = System.Drawing.Color.Transparent;
            this.chkIncludeBuildings.Checked = true;
            this.chkIncludeBuildings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tlpFeatures.SetColumnSpan(this.chkIncludeBuildings, 2);
            this.chkIncludeBuildings.Location = new System.Drawing.Point(3, 133);
            this.chkIncludeBuildings.Name = "chkIncludeBuildings";
            this.chkIncludeBuildings.Size = new System.Drawing.Size(80, 20);
            this.chkIncludeBuildings.TabIndex = 6;
            this.chkIncludeBuildings.Text = "&Buildings";
            this.chkIncludeBuildings.UseVisualStyleBackColor = false;
            this.chkIncludeBuildings.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // chkValuableBlocks
            // 
            this.chkValuableBlocks.AutoSize = true;
            this.chkValuableBlocks.BackColor = System.Drawing.Color.Transparent;
            this.chkValuableBlocks.Checked = true;
            this.chkValuableBlocks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tlpFeatures.SetColumnSpan(this.chkValuableBlocks, 2);
            this.chkValuableBlocks.Location = new System.Drawing.Point(3, 211);
            this.chkValuableBlocks.Name = "chkValuableBlocks";
            this.chkValuableBlocks.Size = new System.Drawing.Size(204, 20);
            this.chkValuableBlocks.TabIndex = 8;
            this.chkValuableBlocks.Text = "&Valuable blocks in architecture";
            this.chkValuableBlocks.UseVisualStyleBackColor = false;
            this.chkValuableBlocks.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // chkItemsInChests
            // 
            this.chkItemsInChests.AutoSize = true;
            this.chkItemsInChests.BackColor = System.Drawing.Color.Transparent;
            this.chkItemsInChests.Checked = true;
            this.chkItemsInChests.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tlpFeatures.SetColumnSpan(this.chkItemsInChests, 2);
            this.chkItemsInChests.Location = new System.Drawing.Point(3, 237);
            this.chkItemsInChests.Name = "chkItemsInChests";
            this.chkItemsInChests.Size = new System.Drawing.Size(116, 20);
            this.chkItemsInChests.TabIndex = 8;
            this.chkItemsInChests.Text = "&Items in chests";
            this.chkItemsInChests.UseVisualStyleBackColor = false;
            this.chkItemsInChests.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // chkIncludeMineshaft
            // 
            this.chkIncludeMineshaft.AutoSize = true;
            this.chkIncludeMineshaft.BackColor = System.Drawing.Color.Transparent;
            this.chkIncludeMineshaft.Checked = true;
            this.chkIncludeMineshaft.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tlpFeatures.SetColumnSpan(this.chkIncludeMineshaft, 2);
            this.chkIncludeMineshaft.Location = new System.Drawing.Point(3, 159);
            this.chkIncludeMineshaft.Name = "chkIncludeMineshaft";
            this.chkIncludeMineshaft.Size = new System.Drawing.Size(83, 20);
            this.chkIncludeMineshaft.TabIndex = 7;
            this.chkIncludeMineshaft.Text = "Mine&shaft";
            this.chkIncludeMineshaft.UseVisualStyleBackColor = false;
            this.chkIncludeMineshaft.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
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
            this.lblSelectFeatures.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // tpNPCs
            // 
            this.tpNPCs.Controls.Add(this.picNPC);
            this.tpNPCs.Controls.Add(this.tableLayoutPanel1);
            this.tpNPCs.Location = new System.Drawing.Point(4, 25);
            this.tpNPCs.Name = "tpNPCs";
            this.tpNPCs.Padding = new System.Windows.Forms.Padding(3);
            this.tpNPCs.Size = new System.Drawing.Size(296, 348);
            this.tpNPCs.TabIndex = 3;
            this.tpNPCs.Text = "NPCs";
            this.tpNPCs.UseVisualStyleBackColor = true;
            // 
            // picNPC
            // 
            this.picNPC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picNPC.Location = new System.Drawing.Point(3, 5);
            this.picNPC.Margin = new System.Windows.Forms.Padding(12, 22, 12, 12);
            this.picNPC.Name = "picNPC";
            this.picNPC.Padding = new System.Windows.Forms.Padding(2);
            this.picNPC.Size = new System.Drawing.Size(288, 161);
            this.picNPC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picNPC.TabIndex = 10;
            this.picNPC.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.06897F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.93103F));
            this.tableLayoutPanel1.Controls.Add(this.chkIncludeSpawners, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lnkRisugamiModLoader, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lnkGhostdancerMobsForMace, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 167);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(290, 178);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // chkIncludeSpawners
            // 
            this.chkIncludeSpawners.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkIncludeSpawners.AutoSize = true;
            this.chkIncludeSpawners.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan(this.chkIncludeSpawners, 2);
            this.chkIncludeSpawners.Location = new System.Drawing.Point(6, 153);
            this.chkIncludeSpawners.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.chkIncludeSpawners.Name = "chkIncludeSpawners";
            this.chkIncludeSpawners.Size = new System.Drawing.Size(160, 20);
            this.chkIncludeSpawners.TabIndex = 14;
            this.chkIncludeSpawners.Text = "&Include NPC Spawners";
            this.chkIncludeSpawners.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label2, 2);
            this.label2.Location = new System.Drawing.Point(3, 94);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 7, 7, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(277, 48);
            this.label2.TabIndex = 13;
            this.label2.Text = "Once installed the NPCs will randomly appear in your MineCraft worlds. To include" +
    " more of the NPCs in the Mace cities, click this option:";
            // 
            // lnkRisugamiModLoader
            // 
            this.lnkRisugamiModLoader.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lnkRisugamiModLoader.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lnkRisugamiModLoader, 2);
            this.lnkRisugamiModLoader.Location = new System.Drawing.Point(3, 68);
            this.lnkRisugamiModLoader.Name = "lnkRisugamiModLoader";
            this.lnkRisugamiModLoader.Size = new System.Drawing.Size(139, 16);
            this.lnkRisugamiModLoader.TabIndex = 12;
            this.lnkRisugamiModLoader.TabStop = true;
            this.lnkRisugamiModLoader.Text = "Risugami\'s ModLoader";
            this.lnkRisugamiModLoader.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRisugamiModLoader_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 7, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 32);
            this.label1.TabIndex = 10;
            this.label1.Text = "Download and install the following mods to include NPCs in Mace:";
            // 
            // lnkGhostdancerMobsForMace
            // 
            this.lnkGhostdancerMobsForMace.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lnkGhostdancerMobsForMace.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lnkGhostdancerMobsForMace, 2);
            this.lnkGhostdancerMobsForMace.Location = new System.Drawing.Point(3, 45);
            this.lnkGhostdancerMobsForMace.Name = "lnkGhostdancerMobsForMace";
            this.lnkGhostdancerMobsForMace.Size = new System.Drawing.Size(181, 16);
            this.lnkGhostdancerMobsForMace.TabIndex = 11;
            this.lnkGhostdancerMobsForMace.TabStop = true;
            this.lnkGhostdancerMobsForMace.Text = "Ghostdancer\'s Mobs for Mace";
            this.lnkGhostdancerMobsForMace.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkGhostdancerMobsForMace_LinkClicked);
            // 
            // tpLog
            // 
            this.tpLog.Controls.Add(this.txtLog);
            this.tpLog.Location = new System.Drawing.Point(4, 25);
            this.tpLog.Name = "tpLog";
            this.tpLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpLog.Size = new System.Drawing.Size(296, 348);
            this.tpLog.TabIndex = 2;
            this.tpLog.Text = "Log";
            this.tpLog.UseVisualStyleBackColor = true;
            this.tpLog.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(290, 342);
            this.txtLog.TabIndex = 0;
            this.txtLog.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // btnGenerateCity
            // 
            this.btnGenerateCity.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGenerateCity.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateCity.Location = new System.Drawing.Point(196, 462);
            this.btnGenerateCity.Name = "btnGenerateCity";
            this.btnGenerateCity.Size = new System.Drawing.Size(122, 27);
            this.btnGenerateCity.TabIndex = 2;
            this.btnGenerateCity.Text = "&Generate City";
            this.btnGenerateCity.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnGenerateCity.UseVisualStyleBackColor = true;
            this.btnGenerateCity.Click += new System.EventHandler(this.btnGenerateCity_Click);
            this.btnGenerateCity.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // picMace
            // 
            this.picMace.BackColor = System.Drawing.Color.Black;
            this.picMace.Location = new System.Drawing.Point(12, 12);
            this.picMace.Name = "picMace";
            this.picMace.Size = new System.Drawing.Size(301, 55);
            this.picMace.TabIndex = 4;
            this.picMace.TabStop = false;
            this.picMace.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            this.picMace.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMace_MouseDown);
            // 
            // btnAbout
            // 
            this.btnAbout.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAbout.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbout.Location = new System.Drawing.Point(14, 462);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(27, 27);
            this.btnAbout.TabIndex = 1;
            this.btnAbout.Text = "?";
            this.btnAbout.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            this.btnAbout.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // lblProgressBack
            // 
            this.lblProgressBack.BackColor = System.Drawing.Color.Black;
            this.lblProgressBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressBack.Location = new System.Drawing.Point(47, 470);
            this.lblProgressBack.Name = "lblProgressBack";
            this.lblProgressBack.Size = new System.Drawing.Size(142, 12);
            this.lblProgressBack.TabIndex = 5;
            this.lblProgressBack.Visible = false;
            this.lblProgressBack.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // lblProgress
            // 
            this.lblProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.Location = new System.Drawing.Point(47, 470);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(142, 12);
            this.lblProgress.TabIndex = 5;
            this.lblProgress.Visible = false;
            this.lblProgress.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // frmMace
            // 
            this.AcceptButton = this.btnGenerateCity;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(326, 498);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.lblProgressBack);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnGenerateCity);
            this.Controls.Add(this.tabOptions);
            this.Controls.Add(this.picMace);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMace";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.tabOptions.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.tlpGeneral.ResumeLayout(false);
            this.tlpGeneral.PerformLayout();
            this.tpFeatures.ResumeLayout(false);
            this.tpFeatures.PerformLayout();
            this.tlpFeatures.ResumeLayout(false);
            this.tlpFeatures.PerformLayout();
            this.tpNPCs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picNPC)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.CheckBox chkIncludeGuardTowers;
        private System.Windows.Forms.CheckBox chkIncludeWalls;
        private System.Windows.Forms.CheckBox chkIncludeFarms;
        private System.Windows.Forms.CheckBox chkIncludeMoat;
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
        private System.Windows.Forms.Label lblTowerAddition;
        private System.Windows.Forms.ComboBox cmbTowerAddition;
        private System.Windows.Forms.Label lblCityName;
        private System.Windows.Forms.TextBox txtCityName;
        private System.Windows.Forms.Label lblCitySeed;
        private System.Windows.Forms.Label lblWorldSeed;
        private System.Windows.Forms.TextBox txtCitySeed;
        private System.Windows.Forms.TextBox txtWorldSeed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkItemsInChests;
        private System.Windows.Forms.CheckBox chkValuableBlocks;
        private System.Windows.Forms.Label lblBreaker;
        private System.Windows.Forms.ComboBox cmbWallMaterial;
        private System.Windows.Forms.Label lblWallMaterial;
        private System.Windows.Forms.CheckBox chkIncludeMineshaft;
        private System.Windows.Forms.TabPage tpNPCs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picNPC;
        private System.Windows.Forms.LinkLabel lnkRisugamiModLoader;
        private System.Windows.Forms.LinkLabel lnkGhostdancerMobsForMace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkIncludeSpawners;
        private System.Windows.Forms.CheckBox chkExportSchematic;

    }
}

