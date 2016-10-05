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
        DateTime startTime;
        public frmMace()
        {
            InitializeComponent();
            picMace.Load("Resources\\mace.png");
            cmbCitySize.SelectedIndex = 0;
            cmbMoatType.SelectedIndex = 0;
            cmbCityEmblem.SelectedIndex = 0;
            cmbOutsideLights.SelectedIndex = 0;
            cmbFireBeacons.SelectedIndex = 0;
            Version ver = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
            this.Text = String.Format("Mace v{0}.{1}.{2}", ver.Major, ver.Minor, ver.Build);

        }
        private void chkIncludeWalls_CheckedChanged(object sender, EventArgs e)
        {
            chkIncludeNoticeboard.Enabled = chkIncludeWalls.Checked;
        }       
        private void tabOptions_KeyDown(object sender, KeyEventArgs e)
        {
            if (tabOptions.SelectedIndex == 1)
            {
                if (e.Alt)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.C:
                            cmbCitySize.Focus();
                            break;
                        case Keys.M:
                            cmbMoatType.Focus();
                            break;
                        case Keys.E:
                            cmbCityEmblem.Focus();
                            break;
                        case Keys.O:
                            cmbOutsideLights.Focus();
                            break;
                        case Keys.F:
                            cmbFireBeacons.Focus();
                            break;
                    }
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
            this.Cursor = Cursors.WaitCursor;
            UpdateProgress(0);
            this.Enabled = false;
            GenerateCity gc = new GenerateCity();
            startTime = DateTime.Now;
            gc.Generate(this, chkIncludeFarms.Checked, chkIncludeMoat.Checked, chkIncludeWalls.Checked, chkIncludeDrawbridges.Checked,
                        chkIncludeGuardTowers.Checked, chkIncludeNoticeboard.Checked, chkIncludeBuildings.Checked, chkIncludePaths.Checked,
                        cmbCitySize.Text, cmbMoatType.Text, cmbCityEmblem.Text, cmbOutsideLights.Text, cmbFireBeacons.Text);
            lblProgressBack.Visible = false;
            lblProgress.Visible = false;
            this.Enabled = true;
            this.Cursor = Cursors.Default;
        }
        public void UpdateLog(string strMessage)
        {
            TimeSpan duration = DateTime.Now - startTime;
#if DEBUG
            txtLog.Text += duration.TotalSeconds + "\r\n";
#endif
            txtLog.Text += strMessage + "\r\n";
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.SelectionLength = 0;
            txtLog.Refresh();
            Application.DoEvents();
            startTime = DateTime.Now;
        }
        public void UpdateProgress(int intPercent)
        {
            lblProgress.Width = (lblProgressBack.Width * intPercent) / 100;
            lblProgress.Refresh();
            Application.DoEvents();
        }

        private void picMace_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                string strNames = "";
                for (int x = 0; x < 50; x++)
                    strNames += "City of " + RandomHelper.RandomFileLine("Resources\\CityStartingWords.txt") +
                                             RandomHelper.RandomFileLine("Resources\\CityEndingWords.txt") + "\r\n";
                MessageBox.Show(strNames);
            }
            else
            {
#if DEBUG
                MessageBox.Show(SourceWorld.ConvertToSignText("[the writing is hidden behind vines]"));
#endif
            }
        }

        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.Text.StartsWith("--"))
                cb.SelectedIndex = 0;

        }
    }
}
