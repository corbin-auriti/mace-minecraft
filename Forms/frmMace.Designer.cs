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
            this.tpCities = new System.Windows.Forms.TabPage();
            this.tlpGeneral = new System.Windows.Forms.TableLayoutPanel();
            this.lnkMedievalBuildingBundle = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkExportCities = new System.Windows.Forms.CheckBox();
            this.numMinimumChunksBetweenCities = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.lblAmountOfCities = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.clbCityThemesToUse = new System.Windows.Forms.CheckedListBox();
            this.numAmountOfCities = new System.Windows.Forms.NumericUpDown();
            this.chkItemsInChests = new System.Windows.Forms.CheckBox();
            this.chkValuableBlocks = new System.Windows.Forms.CheckBox();
            this.btnNewTheme = new System.Windows.Forms.Button();
            this.cbUndergroundOres = new System.Windows.Forms.ComboBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtWorldSeed = new System.Windows.Forms.TextBox();
            this.txtWorldName = new System.Windows.Forms.TextBox();
            this.btnGenerateRandomSeedIdea = new System.Windows.Forms.Button();
            this.btnGenerateRandomWorldName = new System.Windows.Forms.Button();
            this.cmbSpawnPoint = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkMapFeatures = new System.Windows.Forms.CheckBox();
            this.lblWorldType = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblWorldName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbWorldType = new System.Windows.Forms.ComboBox();
            this.tpNPCs = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbNPCs = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lnkRisugamiModLoader = new System.Windows.Forms.LinkLabel();
            this.lnkGhostdancerMobsForMace = new System.Windows.Forms.LinkLabel();
            this.lblGhostdancerHelp = new System.Windows.Forms.Label();
            this.picGhostdancerNPC = new System.Windows.Forms.PictureBox();
            this.tpLog = new System.Windows.Forms.TabPage();
            this.tabLog = new System.Windows.Forms.TabControl();
            this.tpNormal = new System.Windows.Forms.TabPage();
            this.btnSaveLogNormal = new System.Windows.Forms.Button();
            this.txtLogNormal = new System.Windows.Forms.TextBox();
            this.tpVerbose = new System.Windows.Forms.TabPage();
            this.btnSaveLogVerbose = new System.Windows.Forms.Button();
            this.txtLogVerbose = new System.Windows.Forms.TextBox();
            this.tpAbout = new System.Windows.Forms.TabPage();
            this.tlpAbout = new System.Windows.Forms.TableLayoutPanel();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblApplication = new System.Windows.Forms.Label();
            this.lnkForumTopic = new System.Windows.Forms.LinkLabel();
            this.lnkProjectSite = new System.Windows.Forms.LinkLabel();
            this.lnkCredits = new System.Windows.Forms.LinkLabel();
            this.flpRobson = new System.Windows.Forms.FlowLayoutPanel();
            this.lblBy = new System.Windows.Forms.Label();
            this.lnkRobson = new System.Windows.Forms.LinkLabel();
            this.picAbout = new System.Windows.Forms.PictureBox();
            this.btnGenerateWorld = new System.Windows.Forms.Button();
            this.picMace = new System.Windows.Forms.PictureBox();
            this.lblProgressBack = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblSplash = new System.Windows.Forms.Label();
            this.tabOptions.SuspendLayout();
            this.tpCities.SuspendLayout();
            this.tlpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinimumChunksBetweenCities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmountOfCities)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tpNPCs.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGhostdancerNPC)).BeginInit();
            this.tpLog.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.tpNormal.SuspendLayout();
            this.tpVerbose.SuspendLayout();
            this.tpAbout.SuspendLayout();
            this.tlpAbout.SuspendLayout();
            this.flpRobson.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAbout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMace)).BeginInit();
            this.SuspendLayout();
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.tpCities);
            this.tabOptions.Controls.Add(this.tabPage1);
            this.tabOptions.Controls.Add(this.tpNPCs);
            this.tabOptions.Controls.Add(this.tpLog);
            this.tabOptions.Controls.Add(this.tpAbout);
            this.tabOptions.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabOptions.Location = new System.Drawing.Point(11, 89);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.SelectedIndex = 0;
            this.tabOptions.Size = new System.Drawing.Size(304, 399);
            this.tabOptions.TabIndex = 0;
            this.tabOptions.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            this.tabOptions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabOptions_KeyDown);
            // 
            // tpCities
            // 
            this.tpCities.Controls.Add(this.tlpGeneral);
            this.tpCities.Location = new System.Drawing.Point(4, 25);
            this.tpCities.Name = "tpCities";
            this.tpCities.Padding = new System.Windows.Forms.Padding(3);
            this.tpCities.Size = new System.Drawing.Size(296, 370);
            this.tpCities.TabIndex = 0;
            this.tpCities.Text = "Cities";
            this.tpCities.UseVisualStyleBackColor = true;
            this.tpCities.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // tlpGeneral
            // 
            this.tlpGeneral.ColumnCount = 2;
            this.tlpGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.68966F));
            this.tlpGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.31034F));
            this.tlpGeneral.Controls.Add(this.lnkMedievalBuildingBundle, 0, 3);
            this.tlpGeneral.Controls.Add(this.label7, 0, 2);
            this.tlpGeneral.Controls.Add(this.label1, 0, 9);
            this.tlpGeneral.Controls.Add(this.chkExportCities, 0, 8);
            this.tlpGeneral.Controls.Add(this.numMinimumChunksBetweenCities, 1, 5);
            this.tlpGeneral.Controls.Add(this.label6, 0, 5);
            this.tlpGeneral.Controls.Add(this.lblAmountOfCities, 0, 4);
            this.tlpGeneral.Controls.Add(this.label4, 0, 0);
            this.tlpGeneral.Controls.Add(this.clbCityThemesToUse, 0, 1);
            this.tlpGeneral.Controls.Add(this.numAmountOfCities, 1, 4);
            this.tlpGeneral.Controls.Add(this.chkItemsInChests, 0, 6);
            this.tlpGeneral.Controls.Add(this.chkValuableBlocks, 0, 7);
            this.tlpGeneral.Controls.Add(this.btnNewTheme, 1, 0);
            this.tlpGeneral.Controls.Add(this.cbUndergroundOres, 1, 9);
            this.tlpGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGeneral.Location = new System.Drawing.Point(3, 3);
            this.tlpGeneral.Name = "tlpGeneral";
            this.tlpGeneral.RowCount = 11;
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpGeneral.Size = new System.Drawing.Size(290, 364);
            this.tlpGeneral.TabIndex = 1;
            this.tlpGeneral.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            this.tlpGeneral.Paint += new System.Windows.Forms.PaintEventHandler(this.tlpGeneral_Paint);
            // 
            // lnkMedievalBuildingBundle
            // 
            this.lnkMedievalBuildingBundle.AutoSize = true;
            this.lnkMedievalBuildingBundle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lnkMedievalBuildingBundle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkMedievalBuildingBundle.Location = new System.Drawing.Point(0, 158);
            this.lnkMedievalBuildingBundle.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lnkMedievalBuildingBundle.Name = "lnkMedievalBuildingBundle";
            this.lnkMedievalBuildingBundle.Size = new System.Drawing.Size(205, 16);
            this.lnkMedievalBuildingBundle.TabIndex = 32;
            this.lnkMedievalBuildingBundle.TabStop = true;
            this.lnkMedievalBuildingBundle.Text = "medieval building bundle";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.tlpGeneral.SetColumnSpan(this.label7, 2);
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(0, 139);
            this.label7.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(290, 16);
            this.label7.TabIndex = 31;
            this.label7.Text = "Many thanks to Novv for allowing us to use his";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 335);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 20);
            this.label1.TabIndex = 21;
            this.label1.Text = "&Underground ores:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkExportCities
            // 
            this.chkExportCities.AutoSize = true;
            this.chkExportCities.BackColor = System.Drawing.Color.Transparent;
            this.tlpGeneral.SetColumnSpan(this.chkExportCities, 2);
            this.chkExportCities.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkExportCities.Location = new System.Drawing.Point(6, 308);
            this.chkExportCities.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.chkExportCities.Name = "chkExportCities";
            this.chkExportCities.Size = new System.Drawing.Size(281, 20);
            this.chkExportCities.TabIndex = 19;
            this.chkExportCities.Text = "&Export schematics of each city";
            this.chkExportCities.UseVisualStyleBackColor = false;
            // 
            // numMinimumChunksBetweenCities
            // 
            this.numMinimumChunksBetweenCities.BackColor = System.Drawing.SystemColors.Control;
            this.numMinimumChunksBetweenCities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numMinimumChunksBetweenCities.Location = new System.Drawing.Point(208, 218);
            this.numMinimumChunksBetweenCities.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMinimumChunksBetweenCities.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMinimumChunksBetweenCities.Name = "numMinimumChunksBetweenCities";
            this.numMinimumChunksBetweenCities.Size = new System.Drawing.Size(79, 22);
            this.numMinimumChunksBetweenCities.TabIndex = 3;
            this.numMinimumChunksBetweenCities.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 215);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(199, 30);
            this.label6.TabIndex = 18;
            this.label6.Text = "&Minimum chunks between cities:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAmountOfCities
            // 
            this.lblAmountOfCities.AutoSize = true;
            this.lblAmountOfCities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAmountOfCities.Location = new System.Drawing.Point(3, 185);
            this.lblAmountOfCities.Name = "lblAmountOfCities";
            this.lblAmountOfCities.Size = new System.Drawing.Size(199, 30);
            this.lblAmountOfCities.TabIndex = 15;
            this.lblAmountOfCities.Text = "&Amount of cities:";
            this.lblAmountOfCities.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 30);
            this.label4.TabIndex = 14;
            this.label4.Text = "&Themes to use:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // clbCityThemesToUse
            // 
            this.clbCityThemesToUse.BackColor = System.Drawing.SystemColors.Control;
            this.clbCityThemesToUse.CheckOnClick = true;
            this.tlpGeneral.SetColumnSpan(this.clbCityThemesToUse, 2);
            this.clbCityThemesToUse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbCityThemesToUse.FormattingEnabled = true;
            this.clbCityThemesToUse.Location = new System.Drawing.Point(6, 33);
            this.clbCityThemesToUse.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.clbCityThemesToUse.Name = "clbCityThemesToUse";
            this.clbCityThemesToUse.Size = new System.Drawing.Size(281, 89);
            this.clbCityThemesToUse.Sorted = true;
            this.clbCityThemesToUse.TabIndex = 0;
            // 
            // numAmountOfCities
            // 
            this.numAmountOfCities.BackColor = System.Drawing.SystemColors.Control;
            this.numAmountOfCities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numAmountOfCities.Location = new System.Drawing.Point(208, 188);
            this.numAmountOfCities.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAmountOfCities.Name = "numAmountOfCities";
            this.numAmountOfCities.Size = new System.Drawing.Size(79, 22);
            this.numAmountOfCities.TabIndex = 2;
            this.numAmountOfCities.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // chkItemsInChests
            // 
            this.chkItemsInChests.AutoSize = true;
            this.chkItemsInChests.BackColor = System.Drawing.Color.Transparent;
            this.chkItemsInChests.Checked = true;
            this.chkItemsInChests.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tlpGeneral.SetColumnSpan(this.chkItemsInChests, 2);
            this.chkItemsInChests.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkItemsInChests.Location = new System.Drawing.Point(6, 248);
            this.chkItemsInChests.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.chkItemsInChests.Name = "chkItemsInChests";
            this.chkItemsInChests.Size = new System.Drawing.Size(281, 20);
            this.chkItemsInChests.TabIndex = 4;
            this.chkItemsInChests.Text = "&Items in chests";
            this.chkItemsInChests.UseVisualStyleBackColor = false;
            // 
            // chkValuableBlocks
            // 
            this.chkValuableBlocks.AutoSize = true;
            this.chkValuableBlocks.BackColor = System.Drawing.Color.Transparent;
            this.chkValuableBlocks.Checked = true;
            this.chkValuableBlocks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tlpGeneral.SetColumnSpan(this.chkValuableBlocks, 2);
            this.chkValuableBlocks.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkValuableBlocks.Location = new System.Drawing.Point(6, 278);
            this.chkValuableBlocks.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.chkValuableBlocks.Name = "chkValuableBlocks";
            this.chkValuableBlocks.Size = new System.Drawing.Size(281, 20);
            this.chkValuableBlocks.TabIndex = 5;
            this.chkValuableBlocks.Text = "&Valuable blocks in architecture";
            this.chkValuableBlocks.UseVisualStyleBackColor = false;
            // 
            // btnNewTheme
            // 
            this.btnNewTheme.AutoSize = true;
            this.btnNewTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNewTheme.Location = new System.Drawing.Point(208, 3);
            this.btnNewTheme.Name = "btnNewTheme";
            this.btnNewTheme.Size = new System.Drawing.Size(79, 24);
            this.btnNewTheme.TabIndex = 20;
            this.btnNewTheme.Text = "New...";
            this.btnNewTheme.UseVisualStyleBackColor = true;
            this.btnNewTheme.Click += new System.EventHandler(this.btnNewTheme_Click);
            // 
            // cbUndergroundOres
            // 
            this.cbUndergroundOres.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbUndergroundOres.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUndergroundOres.FormattingEnabled = true;
            this.cbUndergroundOres.Items.AddRange(new object[] {
            "Sparse",
            "Uncommon",
            "Normal",
            "Common",
            "Dense"});
            this.cbUndergroundOres.Location = new System.Drawing.Point(208, 338);
            this.cbUndergroundOres.Name = "cbUndergroundOres";
            this.cbUndergroundOres.Size = new System.Drawing.Size(79, 24);
            this.cbUndergroundOres.TabIndex = 22;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(296, 370);
            this.tabPage1.TabIndex = 5;
            this.tabPage1.Text = "World";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.09302F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.90697F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel2.Controls.Add(this.txtWorldSeed, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.txtWorldName, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.btnGenerateRandomSeedIdea, 2, 5);
            this.tableLayoutPanel2.Controls.Add(this.btnGenerateRandomWorldName, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this.cmbSpawnPoint, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.chkMapFeatures, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblWorldType, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.lblWorldName, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.cmbWorldType, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 11;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(290, 364);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // txtWorldSeed
            // 
            this.txtWorldSeed.BackColor = System.Drawing.SystemColors.Control;
            this.txtWorldSeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWorldSeed.Location = new System.Drawing.Point(60, 153);
            this.txtWorldSeed.Name = "txtWorldSeed";
            this.txtWorldSeed.Size = new System.Drawing.Size(195, 22);
            this.txtWorldSeed.TabIndex = 23;
            // 
            // txtWorldName
            // 
            this.txtWorldName.BackColor = System.Drawing.SystemColors.Control;
            this.txtWorldName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWorldName.Location = new System.Drawing.Point(60, 123);
            this.txtWorldName.Name = "txtWorldName";
            this.txtWorldName.Size = new System.Drawing.Size(195, 22);
            this.txtWorldName.TabIndex = 21;
            // 
            // btnGenerateRandomSeedIdea
            // 
            this.btnGenerateRandomSeedIdea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGenerateRandomSeedIdea.Location = new System.Drawing.Point(261, 153);
            this.btnGenerateRandomSeedIdea.Name = "btnGenerateRandomSeedIdea";
            this.btnGenerateRandomSeedIdea.Size = new System.Drawing.Size(26, 24);
            this.btnGenerateRandomSeedIdea.TabIndex = 20;
            this.btnGenerateRandomSeedIdea.Text = "!";
            this.btnGenerateRandomSeedIdea.UseVisualStyleBackColor = true;
            this.btnGenerateRandomSeedIdea.Click += new System.EventHandler(this.btnGenerateRandomSeedIdea_Click);
            // 
            // btnGenerateRandomWorldName
            // 
            this.btnGenerateRandomWorldName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGenerateRandomWorldName.Location = new System.Drawing.Point(261, 123);
            this.btnGenerateRandomWorldName.Name = "btnGenerateRandomWorldName";
            this.btnGenerateRandomWorldName.Size = new System.Drawing.Size(26, 24);
            this.btnGenerateRandomWorldName.TabIndex = 19;
            this.btnGenerateRandomWorldName.Text = "!";
            this.btnGenerateRandomWorldName.UseVisualStyleBackColor = true;
            this.btnGenerateRandomWorldName.Click += new System.EventHandler(this.btnGenerateRandomWorldName_Click);
            // 
            // cmbSpawnPoint
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.cmbSpawnPoint, 2);
            this.cmbSpawnPoint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSpawnPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpawnPoint.FormattingEnabled = true;
            this.cmbSpawnPoint.Items.AddRange(new object[] {
            "Away from the cities",
            "Inside a random city",
            "Outside a random city"});
            this.cmbSpawnPoint.Location = new System.Drawing.Point(60, 3);
            this.cmbSpawnPoint.Name = "cmbSpawnPoint";
            this.cmbSpawnPoint.Size = new System.Drawing.Size(227, 24);
            this.cmbSpawnPoint.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 30);
            this.label8.TabIndex = 18;
            this.label8.Text = "&Spawn:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkMapFeatures
            // 
            this.chkMapFeatures.AutoSize = true;
            this.chkMapFeatures.Checked = true;
            this.chkMapFeatures.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableLayoutPanel2.SetColumnSpan(this.chkMapFeatures, 3);
            this.chkMapFeatures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkMapFeatures.Location = new System.Drawing.Point(3, 63);
            this.chkMapFeatures.Name = "chkMapFeatures";
            this.chkMapFeatures.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.chkMapFeatures.Size = new System.Drawing.Size(284, 24);
            this.chkMapFeatures.TabIndex = 3;
            this.chkMapFeatures.Text = "&Map features (villages, dungeons, etc)";
            this.chkMapFeatures.UseVisualStyleBackColor = true;
            // 
            // lblWorldType
            // 
            this.lblWorldType.AutoSize = true;
            this.lblWorldType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWorldType.Location = new System.Drawing.Point(3, 30);
            this.lblWorldType.Name = "lblWorldType";
            this.lblWorldType.Size = new System.Drawing.Size(51, 30);
            this.lblWorldType.TabIndex = 1;
            this.lblWorldType.Text = "T&ype:";
            this.lblWorldType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 30);
            this.label3.TabIndex = 11;
            this.label3.Text = "S&eed:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWorldName
            // 
            this.lblWorldName.AutoSize = true;
            this.lblWorldName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWorldName.Location = new System.Drawing.Point(3, 120);
            this.lblWorldName.Name = "lblWorldName";
            this.lblWorldName.Size = new System.Drawing.Size(51, 30);
            this.lblWorldName.TabIndex = 5;
            this.lblWorldName.Text = "&Name:";
            this.lblWorldName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.label5, 3);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(284, 30);
            this.label5.TabIndex = 5;
            this.label5.Text = "Leave these blank to get random values:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbWorldType
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.cmbWorldType, 2);
            this.cmbWorldType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbWorldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWorldType.FormattingEnabled = true;
            this.cmbWorldType.Items.AddRange(new object[] {
            "Creative",
            "Survival",
            "Hardcore"});
            this.cmbWorldType.Location = new System.Drawing.Point(60, 33);
            this.cmbWorldType.Name = "cmbWorldType";
            this.cmbWorldType.Size = new System.Drawing.Size(227, 24);
            this.cmbWorldType.TabIndex = 1;
            // 
            // tpNPCs
            // 
            this.tpNPCs.Controls.Add(this.tableLayoutPanel1);
            this.tpNPCs.Location = new System.Drawing.Point(4, 25);
            this.tpNPCs.Name = "tpNPCs";
            this.tpNPCs.Padding = new System.Windows.Forms.Padding(3);
            this.tpNPCs.Size = new System.Drawing.Size(296, 370);
            this.tpNPCs.TabIndex = 3;
            this.tpNPCs.Text = "NPCs";
            this.tpNPCs.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.cmbNPCs, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lnkRisugamiModLoader, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lnkGhostdancerMobsForMace, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblGhostdancerHelp, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.picGhostdancerNPC, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 167F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(290, 364);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // cmbNPCs
            // 
            this.cmbNPCs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbNPCs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNPCs.FormattingEnabled = true;
            this.cmbNPCs.Items.AddRange(new object[] {
            "Ghostdancer\'s NPCs",
            "Minecraft Villagers",
            "-------------------------",
            "None"});
            this.cmbNPCs.Location = new System.Drawing.Point(3, 34);
            this.cmbNPCs.Name = "cmbNPCs";
            this.cmbNPCs.Size = new System.Drawing.Size(284, 24);
            this.cmbNPCs.TabIndex = 19;
            this.cmbNPCs.SelectedIndexChanged += new System.EventHandler(this.chkNPCs_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(284, 31);
            this.label10.TabIndex = 16;
            this.label10.Text = "Select the NPCs to appear in your cities:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lnkRisugamiModLoader
            // 
            this.lnkRisugamiModLoader.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lnkRisugamiModLoader.AutoSize = true;
            this.lnkRisugamiModLoader.Location = new System.Drawing.Point(3, 317);
            this.lnkRisugamiModLoader.Name = "lnkRisugamiModLoader";
            this.lnkRisugamiModLoader.Size = new System.Drawing.Size(139, 16);
            this.lnkRisugamiModLoader.TabIndex = 1;
            this.lnkRisugamiModLoader.TabStop = true;
            this.lnkRisugamiModLoader.Text = "Risugami\'s ModLoader";
            this.lnkRisugamiModLoader.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRisugamiModLoader_LinkClicked);
            // 
            // lnkGhostdancerMobsForMace
            // 
            this.lnkGhostdancerMobsForMace.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lnkGhostdancerMobsForMace.AutoSize = true;
            this.lnkGhostdancerMobsForMace.Location = new System.Drawing.Point(3, 293);
            this.lnkGhostdancerMobsForMace.Name = "lnkGhostdancerMobsForMace";
            this.lnkGhostdancerMobsForMace.Size = new System.Drawing.Size(181, 16);
            this.lnkGhostdancerMobsForMace.TabIndex = 0;
            this.lnkGhostdancerMobsForMace.TabStop = true;
            this.lnkGhostdancerMobsForMace.Text = "Ghostdancer\'s Mobs for Mace";
            this.lnkGhostdancerMobsForMace.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkGhostdancerMobsForMace_LinkClicked);
            // 
            // lblGhostdancerHelp
            // 
            this.lblGhostdancerHelp.AutoSize = true;
            this.lblGhostdancerHelp.Location = new System.Drawing.Point(3, 235);
            this.lblGhostdancerHelp.Margin = new System.Windows.Forms.Padding(3, 7, 7, 0);
            this.lblGhostdancerHelp.Name = "lblGhostdancerHelp";
            this.lblGhostdancerHelp.Size = new System.Drawing.Size(278, 48);
            this.lblGhostdancerHelp.TabIndex = 10;
            this.lblGhostdancerHelp.Text = "Download and install the following mods to include Ghostdancer\'s NPCs in your Min" +
    "ecraft worlds and Mace cities:";
            // 
            // picGhostdancerNPC
            // 
            this.picGhostdancerNPC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picGhostdancerNPC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picGhostdancerNPC.Location = new System.Drawing.Point(3, 65);
            this.picGhostdancerNPC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picGhostdancerNPC.Name = "picGhostdancerNPC";
            this.picGhostdancerNPC.Padding = new System.Windows.Forms.Padding(2);
            this.picGhostdancerNPC.Size = new System.Drawing.Size(284, 159);
            this.picGhostdancerNPC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picGhostdancerNPC.TabIndex = 20;
            this.picGhostdancerNPC.TabStop = false;
            // 
            // tpLog
            // 
            this.tpLog.Controls.Add(this.tabLog);
            this.tpLog.Location = new System.Drawing.Point(4, 25);
            this.tpLog.Name = "tpLog";
            this.tpLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpLog.Size = new System.Drawing.Size(296, 370);
            this.tpLog.TabIndex = 2;
            this.tpLog.Text = "Log";
            this.tpLog.UseVisualStyleBackColor = true;
            this.tpLog.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.tpNormal);
            this.tabLog.Controls.Add(this.tpVerbose);
            this.tabLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabLog.Location = new System.Drawing.Point(3, 3);
            this.tabLog.Name = "tabLog";
            this.tabLog.SelectedIndex = 0;
            this.tabLog.Size = new System.Drawing.Size(290, 364);
            this.tabLog.TabIndex = 6;
            // 
            // tpNormal
            // 
            this.tpNormal.Controls.Add(this.btnSaveLogNormal);
            this.tpNormal.Controls.Add(this.txtLogNormal);
            this.tpNormal.Location = new System.Drawing.Point(4, 25);
            this.tpNormal.Name = "tpNormal";
            this.tpNormal.Padding = new System.Windows.Forms.Padding(3);
            this.tpNormal.Size = new System.Drawing.Size(282, 335);
            this.tpNormal.TabIndex = 0;
            this.tpNormal.Text = "Normal";
            this.tpNormal.UseVisualStyleBackColor = true;
            // 
            // btnSaveLogNormal
            // 
            this.btnSaveLogNormal.Enabled = false;
            this.btnSaveLogNormal.Location = new System.Drawing.Point(6, 305);
            this.btnSaveLogNormal.Name = "btnSaveLogNormal";
            this.btnSaveLogNormal.Size = new System.Drawing.Size(93, 24);
            this.btnSaveLogNormal.TabIndex = 2;
            this.btnSaveLogNormal.Text = "&Save Log";
            this.btnSaveLogNormal.UseVisualStyleBackColor = true;
            this.btnSaveLogNormal.Click += new System.EventHandler(this.btnSaveLogNormal_Click);
            // 
            // txtLogNormal
            // 
            this.txtLogNormal.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtLogNormal.Font = new System.Drawing.Font("Arial", 9F);
            this.txtLogNormal.Location = new System.Drawing.Point(3, 3);
            this.txtLogNormal.Multiline = true;
            this.txtLogNormal.Name = "txtLogNormal";
            this.txtLogNormal.ReadOnly = true;
            this.txtLogNormal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogNormal.Size = new System.Drawing.Size(276, 296);
            this.txtLogNormal.TabIndex = 1;
            // 
            // tpVerbose
            // 
            this.tpVerbose.Controls.Add(this.btnSaveLogVerbose);
            this.tpVerbose.Controls.Add(this.txtLogVerbose);
            this.tpVerbose.Location = new System.Drawing.Point(4, 22);
            this.tpVerbose.Name = "tpVerbose";
            this.tpVerbose.Padding = new System.Windows.Forms.Padding(3);
            this.tpVerbose.Size = new System.Drawing.Size(282, 338);
            this.tpVerbose.TabIndex = 1;
            this.tpVerbose.Text = "Verbose";
            this.tpVerbose.UseVisualStyleBackColor = true;
            // 
            // btnSaveLogVerbose
            // 
            this.btnSaveLogVerbose.Enabled = false;
            this.btnSaveLogVerbose.Location = new System.Drawing.Point(6, 305);
            this.btnSaveLogVerbose.Name = "btnSaveLogVerbose";
            this.btnSaveLogVerbose.Size = new System.Drawing.Size(93, 24);
            this.btnSaveLogVerbose.TabIndex = 3;
            this.btnSaveLogVerbose.Text = "&Save Log";
            this.btnSaveLogVerbose.UseVisualStyleBackColor = true;
            this.btnSaveLogVerbose.Click += new System.EventHandler(this.btnSaveLogVerbose_Click);
            // 
            // txtLogVerbose
            // 
            this.txtLogVerbose.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtLogVerbose.Font = new System.Drawing.Font("Arial", 9F);
            this.txtLogVerbose.Location = new System.Drawing.Point(3, 3);
            this.txtLogVerbose.Multiline = true;
            this.txtLogVerbose.Name = "txtLogVerbose";
            this.txtLogVerbose.ReadOnly = true;
            this.txtLogVerbose.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogVerbose.Size = new System.Drawing.Size(276, 296);
            this.txtLogVerbose.TabIndex = 2;
            // 
            // tpAbout
            // 
            this.tpAbout.Controls.Add(this.tlpAbout);
            this.tpAbout.Controls.Add(this.picAbout);
            this.tpAbout.Location = new System.Drawing.Point(4, 25);
            this.tpAbout.Name = "tpAbout";
            this.tpAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tpAbout.Size = new System.Drawing.Size(296, 370);
            this.tpAbout.TabIndex = 4;
            this.tpAbout.Text = "About";
            this.tpAbout.UseVisualStyleBackColor = true;
            // 
            // tlpAbout
            // 
            this.tlpAbout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpAbout.ColumnCount = 1;
            this.tlpAbout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAbout.Controls.Add(this.lblFullName, 0, 1);
            this.tlpAbout.Controls.Add(this.lblApplication, 0, 0);
            this.tlpAbout.Controls.Add(this.lnkForumTopic, 0, 2);
            this.tlpAbout.Controls.Add(this.lnkProjectSite, 0, 3);
            this.tlpAbout.Controls.Add(this.lnkCredits, 0, 4);
            this.tlpAbout.Controls.Add(this.flpRobson, 0, 5);
            this.tlpAbout.Location = new System.Drawing.Point(6, 170);
            this.tlpAbout.Name = "tlpAbout";
            this.tlpAbout.RowCount = 6;
            this.tlpAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpAbout.Size = new System.Drawing.Size(284, 190);
            this.tlpAbout.TabIndex = 12;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFullName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullName.Location = new System.Drawing.Point(3, 28);
            this.lblFullName.Margin = new System.Windows.Forms.Padding(3);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(278, 16);
            this.lblFullName.TabIndex = 33;
            this.lblFullName.Text = "Minecraft Application for City Erections";
            // 
            // lblApplication
            // 
            this.lblApplication.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblApplication.AutoSize = true;
            this.lblApplication.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplication.Location = new System.Drawing.Point(3, 3);
            this.lblApplication.Name = "lblApplication";
            this.lblApplication.Size = new System.Drawing.Size(46, 18);
            this.lblApplication.TabIndex = 28;
            this.lblApplication.Text = "Mace";
            // 
            // lnkForumTopic
            // 
            this.lnkForumTopic.AccessibleDescription = "";
            this.lnkForumTopic.AccessibleName = "";
            this.lnkForumTopic.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lnkForumTopic.AutoSize = true;
            this.lnkForumTopic.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkForumTopic.Location = new System.Drawing.Point(3, 54);
            this.lnkForumTopic.Name = "lnkForumTopic";
            this.lnkForumTopic.Size = new System.Drawing.Size(79, 16);
            this.lnkForumTopic.TabIndex = 0;
            this.lnkForumTopic.TabStop = true;
            this.lnkForumTopic.Text = "Forum Topic";
            this.lnkForumTopic.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkForumTopic_LinkClicked);
            // 
            // lnkProjectSite
            // 
            this.lnkProjectSite.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lnkProjectSite.AutoSize = true;
            this.lnkProjectSite.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkProjectSite.Location = new System.Drawing.Point(3, 79);
            this.lnkProjectSite.Name = "lnkProjectSite";
            this.lnkProjectSite.Size = new System.Drawing.Size(173, 16);
            this.lnkProjectSite.TabIndex = 1;
            this.lnkProjectSite.TabStop = true;
            this.lnkProjectSite.Text = "Project Site on Google Code";
            this.lnkProjectSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkProjectSite_LinkClicked);
            // 
            // lnkCredits
            // 
            this.lnkCredits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lnkCredits.AutoSize = true;
            this.lnkCredits.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkCredits.Location = new System.Drawing.Point(3, 104);
            this.lnkCredits.Name = "lnkCredits";
            this.lnkCredits.Size = new System.Drawing.Size(49, 16);
            this.lnkCredits.TabIndex = 2;
            this.lnkCredits.TabStop = true;
            this.lnkCredits.Text = "Credits";
            this.lnkCredits.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCredits_LinkClicked);
            // 
            // flpRobson
            // 
            this.flpRobson.Controls.Add(this.lblBy);
            this.flpRobson.Controls.Add(this.lnkRobson);
            this.flpRobson.Location = new System.Drawing.Point(3, 128);
            this.flpRobson.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.flpRobson.Name = "flpRobson";
            this.flpRobson.Size = new System.Drawing.Size(200, 25);
            this.flpRobson.TabIndex = 4;
            // 
            // lblBy
            // 
            this.lblBy.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBy.AutoSize = true;
            this.lblBy.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBy.Location = new System.Drawing.Point(0, 3);
            this.lblBy.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblBy.Name = "lblBy";
            this.lblBy.Size = new System.Drawing.Size(71, 16);
            this.lblBy.TabIndex = 29;
            this.lblBy.Text = "Created by";
            // 
            // lnkRobson
            // 
            this.lnkRobson.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lnkRobson.AutoSize = true;
            this.lnkRobson.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkRobson.Location = new System.Drawing.Point(71, 3);
            this.lnkRobson.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lnkRobson.Name = "lnkRobson";
            this.lnkRobson.Size = new System.Drawing.Size(52, 16);
            this.lnkRobson.TabIndex = 3;
            this.lnkRobson.TabStop = true;
            this.lnkRobson.Text = "Robson";
            this.lnkRobson.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRobson_LinkClicked);
            // 
            // picAbout
            // 
            this.picAbout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAbout.Location = new System.Drawing.Point(3, 5);
            this.picAbout.Margin = new System.Windows.Forms.Padding(12, 22, 12, 12);
            this.picAbout.Name = "picAbout";
            this.picAbout.Padding = new System.Windows.Forms.Padding(2);
            this.picAbout.Size = new System.Drawing.Size(288, 161);
            this.picAbout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAbout.TabIndex = 11;
            this.picAbout.TabStop = false;
            // 
            // btnGenerateWorld
            // 
            this.btnGenerateWorld.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGenerateWorld.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateWorld.Location = new System.Drawing.Point(181, 494);
            this.btnGenerateWorld.Name = "btnGenerateWorld";
            this.btnGenerateWorld.Size = new System.Drawing.Size(133, 27);
            this.btnGenerateWorld.TabIndex = 2;
            this.btnGenerateWorld.Text = "&Generate World";
            this.btnGenerateWorld.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnGenerateWorld.UseVisualStyleBackColor = true;
            this.btnGenerateWorld.Click += new System.EventHandler(this.btnGenerateWorld_Click);
            this.btnGenerateWorld.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
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
            // lblProgressBack
            // 
            this.lblProgressBack.BackColor = System.Drawing.Color.Black;
            this.lblProgressBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressBack.Location = new System.Drawing.Point(9, 501);
            this.lblProgressBack.Name = "lblProgressBack";
            this.lblProgressBack.Size = new System.Drawing.Size(163, 12);
            this.lblProgressBack.TabIndex = 5;
            this.lblProgressBack.Visible = false;
            this.lblProgressBack.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // lblProgress
            // 
            this.lblProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.Location = new System.Drawing.Point(9, 501);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(163, 12);
            this.lblProgress.TabIndex = 5;
            this.lblProgress.Visible = false;
            this.lblProgress.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ShowHelp);
            // 
            // lblSplash
            // 
            this.lblSplash.BackColor = System.Drawing.SystemColors.Control;
            this.lblSplash.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSplash.Location = new System.Drawing.Point(15, 68);
            this.lblSplash.Name = "lblSplash";
            this.lblSplash.Size = new System.Drawing.Size(301, 14);
            this.lblSplash.TabIndex = 6;
            this.lblSplash.Text = "Splash text";
            this.lblSplash.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblSplash.Click += new System.EventHandler(this.lblSplash_Click);
            // 
            // frmMace
            // 
            this.AcceptButton = this.btnGenerateWorld;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(325, 532);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.lblProgressBack);
            this.Controls.Add(this.btnGenerateWorld);
            this.Controls.Add(this.tabOptions);
            this.Controls.Add(this.picMace);
            this.Controls.Add(this.lblSplash);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMace";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.tabOptions.ResumeLayout(false);
            this.tpCities.ResumeLayout(false);
            this.tlpGeneral.ResumeLayout(false);
            this.tlpGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinimumChunksBetweenCities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmountOfCities)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tpNPCs.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGhostdancerNPC)).EndInit();
            this.tpLog.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.tpNormal.ResumeLayout(false);
            this.tpNormal.PerformLayout();
            this.tpVerbose.ResumeLayout(false);
            this.tpVerbose.PerformLayout();
            this.tpAbout.ResumeLayout(false);
            this.tlpAbout.ResumeLayout(false);
            this.tlpAbout.PerformLayout();
            this.flpRobson.ResumeLayout(false);
            this.flpRobson.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAbout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMace)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabOptions;
        private System.Windows.Forms.TabPage tpCities;
        private System.Windows.Forms.TabPage tpLog;
        private System.Windows.Forms.Button btnGenerateWorld;
        private System.Windows.Forms.PictureBox picMace;
        private System.Windows.Forms.Label lblProgressBack;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.TabPage tpNPCs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabPage tpAbout;
        private System.Windows.Forms.PictureBox picAbout;
        private System.Windows.Forms.TableLayoutPanel tlpAbout;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblApplication;
        private System.Windows.Forms.LinkLabel lnkForumTopic;
        private System.Windows.Forms.LinkLabel lnkProjectSite;
        private System.Windows.Forms.LinkLabel lnkCredits;
        private System.Windows.Forms.FlowLayoutPanel flpRobson;
        private System.Windows.Forms.Label lblBy;
        private System.Windows.Forms.LinkLabel lnkRobson;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblWorldName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblWorldType;
        private System.Windows.Forms.ComboBox cmbWorldType;
        private System.Windows.Forms.CheckBox chkMapFeatures;
        private System.Windows.Forms.TableLayoutPanel tlpGeneral;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblAmountOfCities;
        public System.Windows.Forms.CheckBox chkItemsInChests;
        public System.Windows.Forms.CheckBox chkValuableBlocks;
        private System.Windows.Forms.NumericUpDown numAmountOfCities;
        private System.Windows.Forms.NumericUpDown numMinimumChunksBetweenCities;
        public System.Windows.Forms.CheckedListBox clbCityThemesToUse;
        private System.Windows.Forms.ComboBox cmbSpawnPoint;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabControl tabLog;
        private System.Windows.Forms.TabPage tpNormal;
        private System.Windows.Forms.TextBox txtLogNormal;
        private System.Windows.Forms.TabPage tpVerbose;
        private System.Windows.Forms.TextBox txtLogVerbose;
        public System.Windows.Forms.CheckBox chkExportCities;
        public System.Windows.Forms.Button btnSaveLogNormal;
        public System.Windows.Forms.Button btnSaveLogVerbose;
        private System.Windows.Forms.Label lblSplash;
        private System.Windows.Forms.ComboBox cmbNPCs;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.LinkLabel lnkRisugamiModLoader;
        private System.Windows.Forms.LinkLabel lnkGhostdancerMobsForMace;
        private System.Windows.Forms.Label lblGhostdancerHelp;
        private System.Windows.Forms.PictureBox picGhostdancerNPC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnNewTheme;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbUndergroundOres;
        private System.Windows.Forms.Button btnGenerateRandomSeedIdea;
        private System.Windows.Forms.Button btnGenerateRandomWorldName;
        private System.Windows.Forms.TextBox txtWorldSeed;
        private System.Windows.Forms.TextBox txtWorldName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel lnkMedievalBuildingBundle;

    }
}

