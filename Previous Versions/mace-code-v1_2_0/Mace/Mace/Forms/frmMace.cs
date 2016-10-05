/*
    Mace
    Copyright (C) 2011 Robson
    http://iceyboard.no-ip.org

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>
*/

using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace Mace
{
    public partial class frmMace : Form
    {
        public frmMace()
        {
            InitializeComponent();
            picMace.Load("mace.png");
            cmbCitySize.SelectedIndex = 0;
            cmbMoatLiquid.SelectedIndex = 0;
            Version ver = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
            this.Text = String.Format("Mace v{0}.{1}.{2}", ver.Major, ver.Minor, ver.Build);

        }
        private void chkIncludeWalls_CheckedChanged(object sender, EventArgs e)
        {
            chkIncludeNoticeboard.Enabled = chkIncludeWalls.Checked;
        }
        // todo: there's probably some way to combine the two methods below
        private void cmbCitySize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCitySize.Text.StartsWith("-"))
                cmbCitySize.SelectedIndex = 0;
        }
        private void cmbMoatLiquid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMoatLiquid.Text.StartsWith("-"))
                cmbMoatLiquid.SelectedIndex = 0;
        }
        private void tabOptions_KeyDown(object sender, KeyEventArgs e)
        {
            if (tabOptions.SelectedIndex == 1)
            {
                if (e.Alt)
                {
                    if (e.KeyCode == Keys.C)
                        cmbCitySize.Focus();
                    if (e.KeyCode == Keys.M)
                        cmbMoatLiquid.Focus();
                }
            }
        }
        private void btnAbout_Click(object sender, EventArgs e)
        {
            frmAbout fA = new frmAbout();
            fA.ShowDialog();
        }
        private void btnGenerateCity_Click(object sender, EventArgs e)
        {
            tabOptions.SelectedIndex = 2;
            lblProgress.Visible = true;
            lblProgressBack.Visible = true;
            txtLog.Text = "";
            UpdateProgress(0);
            this.Enabled = false;
            GenerateCity gc = new GenerateCity();
            gc.Generate(this, chkIncludeFarms.Checked, chkIncludeMoat.Checked, chkIncludeWalls.Checked, chkIncludeDrawbridges.Checked,
                        chkIncludeGuardTowers.Checked, chkIncludeNoticeboard.Checked, chkIncludeBuildings.Checked, chkIncludeSewers.Checked,
                        cmbCitySize.Text, cmbMoatLiquid.Text);
            lblProgressBack.Visible = false;
            lblProgress.Visible = false;
            this.Enabled = true;
        }
        public void UpdateLog(string strMessage)
        {
            txtLog.Text += strMessage + "\r\n";
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.SelectionLength = 0;
            txtLog.Refresh();
        }
        public void UpdateProgress(int intPercent)
        {
            lblProgress.Width = (lblProgressBack.Width * intPercent) / 100;
            lblProgress.Refresh();
        }
    }
}
