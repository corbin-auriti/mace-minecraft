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
            this.chkIncludeSewers = new System.Windows.Forms.CheckBox();
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
            this.lblMoatLiquid = new System.Windows.Forms.Label();
            this.cmbMoatLiquid = new System.Windows.Forms.ComboBox();
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
            this.tabOptions.Size = new System.Drawing.Size(304, 282);
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
            this.tpFeatures.Size = new System.Drawing.Size(296, 253);
            this.tpFeatures.TabIndex = 1;
            this.tpFeatures.Text = "Features";
            this.tpFeatures.UseVisualStyleBackColor = true;
            // 
            // tlpFeatures
            // 
            this.tlpFeatures.ColumnCount = 2;
            this.tlpFeatures.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpFeatures.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFeatures.Controls.Add(this.chkIncludeSewers, 0, 7);
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
            this.tlpFeatures.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpFeatures.Size = new System.Drawing.Size(258, 205);
            this.tlpFeatures.TabIndex = 0;
            // 
            // chkIncludeSewers
            // 
            this.chkIncludeSewers.AutoSize = true;
            this.chkIncludeSewers.BackColor = System.Drawing.Color.Transparent;
            this.chkIncludeSewers.Checked = true;
            this.chkIncludeSewers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tlpFeatures.SetColumnSpan(this.chkIncludeSewers, 2);
            this.chkIncludeSewers.Location = new System.Drawing.Point(3, 185);
            this.chkIncludeSewers.Name = "chkIncludeSewers";
            this.chkIncludeSewers.Size = new System.Drawing.Size(70, 20);
            this.chkIncludeSewers.TabIndex = 7;
            this.chkIncludeSewers.Text = "&Sewers";
            this.chkIncludeSewers.UseVisualStyleBackColor = false;
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
            this.chkIncludeWalls.TabIndex = 2;
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
            this.chkIncludeDrawbridges.TabIndex = 3;
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
            this.chkIncludeGuardTowers.TabIndex = 4;
            this.chkIncludeGuardTowers.Text = "G&uard towers";
            this.chkIncludeGuardTowers.UseVisualStyleBackColor = false;
            // 
            // lblSelectFeatures
            // 
            this.lblSelectFeatures.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSelectFeatures.AutoSize = true;
            this.lblSelectFeatures.Location = new System.Drawing.Point(6, 9);
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
            this.tpGeneral.Size = new System.Drawing.Size(296, 253);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            // 
            // tlpGeneral
            // 
            this.tlpGeneral.ColumnCount = 2;
            this.tlpGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.27586F));
            this.tlpGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.72414F));
            this.tlpGeneral.Controls.Add(this.cmbCitySize, 1, 0);
            this.tlpGeneral.Controls.Add(this.lblCitySize, 0, 0);
            this.tlpGeneral.Controls.Add(this.lblMoatLiquid, 0, 1);
            this.tlpGeneral.Controls.Add(this.cmbMoatLiquid, 1, 1);
            this.tlpGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGeneral.Location = new System.Drawing.Point(3, 3);
            this.tlpGeneral.Name = "tlpGeneral";
            this.tlpGeneral.RowCount = 5;
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpGeneral.Size = new System.Drawing.Size(290, 247);
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
            this.cmbCitySize.Location = new System.Drawing.Point(84, 3);
            this.cmbCitySize.Name = "cmbCitySize";
            this.cmbCitySize.Size = new System.Drawing.Size(203, 24);
            this.cmbCitySize.TabIndex = 0;
            this.cmbCitySize.SelectedIndexChanged += new System.EventHandler(this.cmbCitySize_SelectedIndexChanged);
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
            // lblMoatLiquid
            // 
            this.lblMoatLiquid.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMoatLiquid.AutoSize = true;
            this.lblMoatLiquid.Location = new System.Drawing.Point(3, 37);
            this.lblMoatLiquid.Name = "lblMoatLiquid";
            this.lblMoatLiquid.Size = new System.Drawing.Size(71, 16);
            this.lblMoatLiquid.TabIndex = 2;
            this.lblMoatLiquid.Text = "&Moat liquid";
            this.lblMoatLiquid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbMoatLiquid
            // 
            this.cmbMoatLiquid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMoatLiquid.FormattingEnabled = true;
            this.cmbMoatLiquid.Items.AddRange(new object[] {
            "Random",
            "------------------------------------",
            "Water",
            "Lava"});
            this.cmbMoatLiquid.Location = new System.Drawing.Point(84, 33);
            this.cmbMoatLiquid.Name = "cmbMoatLiquid";
            this.cmbMoatLiquid.Size = new System.Drawing.Size(203, 24);
            this.cmbMoatLiquid.TabIndex = 1;
            this.cmbMoatLiquid.SelectedIndexChanged += new System.EventHandler(this.cmbMoatLiquid_SelectedIndexChanged);
            // 
            // tpLog
            // 
            this.tpLog.Controls.Add(this.txtLog);
            this.tpLog.Location = new System.Drawing.Point(4, 25);
            this.tpLog.Name = "tpLog";
            this.tpLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpLog.Size = new System.Drawing.Size(296, 253);
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
            this.txtLog.Size = new System.Drawing.Size(284, 241);
            this.txtLog.TabIndex = 0;
            // 
            // btnGenerateCity
            // 
            this.btnGenerateCity.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGenerateCity.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateCity.Location = new System.Drawing.Point(193, 367);
            this.btnGenerateCity.Name = "btnGenerateCity";
            this.btnGenerateCity.Size = new System.Drawing.Size(122, 27);
            this.btnGenerateCity.TabIndex = 1;
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
            // 
            // btnAbout
            // 
            this.btnAbout.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAbout.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbout.Location = new System.Drawing.Point(12, 367);
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
            this.lblProgressBack.Location = new System.Drawing.Point(45, 375);
            this.lblProgressBack.Name = "lblProgressBack";
            this.lblProgressBack.Size = new System.Drawing.Size(142, 12);
            this.lblProgressBack.TabIndex = 5;
            this.lblProgressBack.Visible = false;
            // 
            // lblProgress
            // 
            this.lblProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.Location = new System.Drawing.Point(45, 375);
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
            this.ClientSize = new System.Drawing.Size(326, 403);
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
        private System.Windows.Forms.CheckBox chkIncludeSewers;
        private System.Windows.Forms.Label lblSelectFeatures;
        private System.Windows.Forms.Label lblMoatLiquid;
        private System.Windows.Forms.ComboBox cmbMoatLiquid;
        private System.Windows.Forms.Button btnGenerateCity;
        private System.Windows.Forms.PictureBox picMace;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Label lblProgressBack;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.CheckBox chkIncludeDrawbridges;

    }
}

