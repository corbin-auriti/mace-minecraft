namespace Mace
{
    partial class frmAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.tlpAbout = new System.Windows.Forms.TableLayoutPanel();
            this.picWires = new System.Windows.Forms.PictureBox();
            this.lblApplication = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.flpRobson = new System.Windows.Forms.FlowLayoutPanel();
            this.lblBy = new System.Windows.Forms.Label();
            this.lnkRobson = new System.Windows.Forms.LinkLabel();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lnkProjectSite = new System.Windows.Forms.LinkLabel();
            this.lnkForumTopic = new System.Windows.Forms.LinkLabel();
            this.tlpAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWires)).BeginInit();
            this.flpRobson.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAbout
            // 
            this.tlpAbout.ColumnCount = 2;
            this.tlpAbout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.88018F));
            this.tlpAbout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.11981F));
            this.tlpAbout.Controls.Add(this.picWires, 0, 0);
            this.tlpAbout.Controls.Add(this.lblApplication, 1, 0);
            this.tlpAbout.Controls.Add(this.btnOK, 1, 5);
            this.tlpAbout.Controls.Add(this.flpRobson, 1, 4);
            this.tlpAbout.Controls.Add(this.lblFullName, 1, 1);
            this.tlpAbout.Controls.Add(this.lnkProjectSite, 1, 3);
            this.tlpAbout.Controls.Add(this.lnkForumTopic, 1, 2);
            this.tlpAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAbout.Location = new System.Drawing.Point(9, 9);
            this.tlpAbout.Name = "tlpAbout";
            this.tlpAbout.RowCount = 6;
            this.tlpAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAbout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpAbout.Size = new System.Drawing.Size(434, 255);
            this.tlpAbout.TabIndex = 0;
            // 
            // picWires
            // 
            this.picWires.Location = new System.Drawing.Point(3, 3);
            this.picWires.Name = "picWires";
            this.tlpAbout.SetRowSpan(this.picWires, 6);
            this.picWires.Size = new System.Drawing.Size(114, 249);
            this.picWires.TabIndex = 27;
            this.picWires.TabStop = false;
            // 
            // lblApplication
            // 
            this.lblApplication.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblApplication.AutoSize = true;
            this.lblApplication.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplication.Location = new System.Drawing.Point(123, 3);
            this.lblApplication.Name = "lblApplication";
            this.lblApplication.Size = new System.Drawing.Size(143, 18);
            this.lblApplication.TabIndex = 28;
            this.lblApplication.Text = "Application Version";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(356, 229);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 26;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // flpRobson
            // 
            this.flpRobson.Controls.Add(this.lblBy);
            this.flpRobson.Controls.Add(this.lnkRobson);
            this.flpRobson.Location = new System.Drawing.Point(123, 103);
            this.flpRobson.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.flpRobson.Name = "flpRobson";
            this.flpRobson.Size = new System.Drawing.Size(200, 25);
            this.flpRobson.TabIndex = 32;
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
            this.lnkRobson.TabIndex = 31;
            this.lnkRobson.TabStop = true;
            this.lnkRobson.Text = "Robson";
            this.lnkRobson.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRobson_LinkClicked);
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFullName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullName.Location = new System.Drawing.Point(123, 28);
            this.lblFullName.Margin = new System.Windows.Forms.Padding(3);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(308, 16);
            this.lblFullName.TabIndex = 33;
            this.lblFullName.Text = "Minecraft Application for City Erections";
            // 
            // lnkProjectSite
            // 
            this.lnkProjectSite.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lnkProjectSite.AutoSize = true;
            this.lnkProjectSite.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkProjectSite.Location = new System.Drawing.Point(123, 79);
            this.lnkProjectSite.Name = "lnkProjectSite";
            this.lnkProjectSite.Size = new System.Drawing.Size(155, 16);
            this.lnkProjectSite.TabIndex = 30;
            this.lnkProjectSite.TabStop = true;
            this.lnkProjectSite.Text = "Google Code Project Site";
            this.lnkProjectSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkProjectSite_LinkClicked);
            // 
            // lnkForumTopic
            // 
            this.lnkForumTopic.AccessibleDescription = "";
            this.lnkForumTopic.AccessibleName = "";
            this.lnkForumTopic.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lnkForumTopic.AutoSize = true;
            this.lnkForumTopic.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkForumTopic.Location = new System.Drawing.Point(123, 54);
            this.lnkForumTopic.Name = "lnkForumTopic";
            this.lnkForumTopic.Size = new System.Drawing.Size(79, 16);
            this.lnkForumTopic.TabIndex = 31;
            this.lnkForumTopic.TabStop = true;
            this.lnkForumTopic.Text = "Forum Topic";
            this.lnkForumTopic.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkForumTopic_LinkClicked);
            // 
            // frmAbout
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(452, 273);
            this.Controls.Add(this.tlpAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About Mace";
            this.tlpAbout.ResumeLayout(false);
            this.tlpAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWires)).EndInit();
            this.flpRobson.ResumeLayout(false);
            this.flpRobson.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAbout;
        private System.Windows.Forms.PictureBox picWires;
        private System.Windows.Forms.Label lblApplication;
        private System.Windows.Forms.Label lblBy;
        private System.Windows.Forms.LinkLabel lnkProjectSite;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.LinkLabel lnkForumTopic;
        private System.Windows.Forms.FlowLayoutPanel flpRobson;
        private System.Windows.Forms.LinkLabel lnkRobson;
        private System.Windows.Forms.Label lblFullName;
    }
}
