/*
    Mace
    Copyright (C) 2011-2012 Robson
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Substrate;

namespace Mace
{
    public partial class frmTheme : Form
    {
        string[] _buildingTypes = { "City", "City Entrance", "Farm", "Mineshaft Entrance", "Mineshaft Section", "Sky Feature" };
        Dictionary<string, List<XMLNodeStore>> _buildingSets = new Dictionary<string, List<XMLNodeStore>>();
        bool isLoading = true;

        public frmTheme()
        {
            InitializeComponent();
        }
        private void frmTheme_Load(object sender, EventArgs e)
        {
            // populate dynamic lists
            foreach (string emblem in Directory.GetFiles("Resources", "Emblem *.txt"))
            {
                clbCityEntranceEmblems.Items.Add(
                    emblem.Substring("Resources|Emblem ".Length, emblem.Length - "Resources|Emblem .txt".Length));
            }
            // check all items
            clbFeatures.SetCheckValueForAllItems(true);
            clbMoatTypes.SetCheckValueForAllItems(true);
            clbStreetLights.SetCheckValueForAllItems(true);
            clbOutsideLights.SetCheckValueForAllItems(true);
            clbGuardTowerAdditions.SetCheckValueForAllItems(true);
            clbCityEntranceEmblems.SetCheckValueForAllItems(true);
            // checked asterixed items
            clbFlowers.CheckAsterixedItems();
            clbCitySizes.CheckAsterixedItems();
            clbFarmSizes.CheckAsterixedItems();
            clbGroundBlock.CheckAsterixedItems();
            clbPathBlock.CheckAsterixedItems();
            clbWallBlock.CheckAsterixedItems();
            lbUndergroundBlocks.SelectAsterixedItem();
            // other
            lblFlowerFrequency.Text = tbFlowerFrequency.Value + "%";
            cmbCityStartingWord.SelectedIndex = 0;
            cmbCityEndingWord.SelectedIndex = 0;
            // buildings
            StoreBuildingSets();
            DisplayBuildings();
            SelectBuildingsByTag("medieval");
            // piccies
            picMaceTerraria.Load(Path.Combine("Resources", "mace_terraria.png"));
            ToolTip tt = new ToolTip();
            tt.SetToolTip(picMaceTerraria, "This is a good time to clarify that Mace is for Minecraft!");
            tt.SetToolTip(chkUnique, "The building will appear a maximum of once per city.");
            isLoading = false;
        }
        private void chkFeatures_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblFeaturesHelp.Text = clbFeatures.Items[clbFeatures.SelectedIndex].ToString() + "\n" + new String('-', 50) + "\n\n" +
                HelpMessages.SendHelp("features", clbFeatures.Items[clbFeatures.SelectedIndex].ToString().ToLower());
        }
        private void chkMoatTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMoatHelp.Text = clbMoatTypes.Items[clbMoatTypes.SelectedIndex].ToString() + "\n" + new String('-', 50) + "\n\n" +
                HelpMessages.SendHelp("moats", clbMoatTypes.Items[clbMoatTypes.SelectedIndex].ToString().ToLower());
        }
        private void tbFlowerFrequency_ValueChanged(object sender, EventArgs e)
        {
            lblFlowerFrequency.Text = tbFlowerFrequency.Value + "%";
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            txtLog.Text = String.Empty;
            tcTheme.SelectTab(tabLog);
            if (ValidateUserSelections())
            {
                CreateTheme();
                txtLog.Text = "New theme created!";
                txtLog.Text = txtLog.Text + "\r\n\"" + txtThemeName.Text + "\" has been added to the list of available themes.";
                txtLog.Text = txtLog.Text + "\r\nClose this window to return to Mace.";
            }
            else
            {
                txtLog.Text = "Validating selected options..." + txtLog.Text;
            }
        }
        private void chkUseNovvBuildings_CheckedChanged(object sender, EventArgs e)
        {
            clbBuildings.Enabled = !chkUseNovvBuildings.Checked;
            btnBuildingsSelectAll.Enabled = !chkUseNovvBuildings.Checked;
            btnBuildingsCheckNone.Enabled = !chkUseNovvBuildings.Checked;
            btnBuildingsCheckMedieval.Enabled = !chkUseNovvBuildings.Checked;
            btnBuildingsCheckDesert.Enabled = !chkUseNovvBuildings.Checked;
            txtBuildingData.Enabled = !chkUseNovvBuildings.Checked;
            if (chkUseNovvBuildings.Checked)
            {
                cmbBuildingFrequency.Enabled = false;
                chkUnique.Enabled = false;
            }
            else
            {
                clbBuildings_SelectedIndexChanged(sender, e);
            }
        }
        private void btnBuildingsSelectAll_Click(object sender, EventArgs e)
        {
            clbBuildings.SetCheckValueForAllItems(true);
        }
        private void btnBuildingsCheckNone_Click(object sender, EventArgs e)
        {
            clbBuildings.SetCheckValueForAllItems(false);
        }
        private void btnBuildingsCheckMedieval_Click(object sender, EventArgs e)
        {
            SelectBuildingsByTag("medieval");
        }
        private void btnBuildingsCheckDesert_Click(object sender, EventArgs e)
        {
            SelectBuildingsByTag("desert");
        }
        private void txtThemeName_TextChanged(object sender, EventArgs e)
        {
            string safe = txtThemeName.Text.ToSafeFilename();
            if (safe != txtThemeName.Text)
            {
                txtThemeName.Text = txtThemeName.Text.ToSafeFilename();
                txtThemeName.SelectionStart = txtThemeName.Text.Length;
                txtThemeName.SelectionLength = 0;
                txtThemeName.ScrollToCaret();
            }
        }
        private void clbBuildings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clbBuildings.SelectedIndex >= 0)
            {
                string[] selected = clbBuildings.Items[clbBuildings.SelectedIndex].ToString().Split('>');
                foreach (KeyValuePair<string, List<XMLNodeStore>> buildingSet in _buildingSets)
                {
                    if (buildingSet.Key == selected[0].Trim())
                    {
                        foreach (XMLNodeStore building in buildingSet.Value)
                        {
                            if (building.GetData("name") == selected[1].Trim())
                            {
                                isLoading = true;
                                txtBuildingData.Text = building.ToText();
                                switch (buildingSet.Key)
                                {
                                    case "City":
                                    case "Farm":
                                        cmbBuildingFrequency.Enabled = true;
                                        chkUnique.Enabled = true;
                                        break;
                                    case "City Entrance":
                                    case "Mineshaft Entrance":
                                    case "Sky Feature":
                                        cmbBuildingFrequency.Enabled = true;
                                        chkUnique.Enabled = false;
                                        break;
                                    case "Mineshaft Section":
                                        cmbBuildingFrequency.Enabled = false;
                                        chkUnique.Enabled = true;
                                        break;
                                }
                                if (cmbBuildingFrequency.Enabled)
                                {
                                    cmbBuildingFrequency.Text = building.GetData("frequency");
                                }
                                else
                                {
                                    cmbBuildingFrequency.Text = String.Empty;
                                }
                                chkUnique.Checked = chkUnique.Enabled && building.GetData("unique").IsAffirmative();
                                isLoading = false;
                                return;
                            }
                        }
                    }
                }
            }
        }
        private void cmbCityStartingWord_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowExampleCityNames(sender, e);
        }
        private void cmbCityEndingWord_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowExampleCityNames(sender, e);
        }
        private void txtCityPrefix_TextChanged(object sender, EventArgs e)
        {
            ShowExampleCityNames(sender, e);
        }
        private void CheckedListBox_MouseClick(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) & (e.X > 13))
            {
                CheckedListBox clbCurrent = (CheckedListBox)sender;
                clbCurrent.SetItemChecked(clbCurrent.SelectedIndex, !clbCurrent.GetItemChecked(clbCurrent.SelectedIndex));
            }
        }
        private void cmbBuildingFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                XMLNodeStore building = FindBuilding(clbBuildings.Items[clbBuildings.SelectedIndex].ToString());
                if (building.GetData("name").Length > 0)
                {
                    if (building.GetData("frequency_original").Length == 0)
                    {
                        building.SetData("frequency_original", building.GetData("frequency"));
                    }
                    building.SetData("frequency", cmbBuildingFrequency.Text);
                    clbBuildings_SelectedIndexChanged(sender, e);
                }
            }
        }
        private void chkUnique_CheckedChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                XMLNodeStore building = FindBuilding(clbBuildings.Items[clbBuildings.SelectedIndex].ToString());
                if (building.GetData("name").Length > 0)
                {
                    if (building.GetData("unique").Length == 0)
                    {
                        building.SetData("unique", "no");
                    }
                    if (building.GetData("unique_original").Length == 0)
                    {
                        building.SetData("unique_original", building.GetData("unique"));
                    }
                    building.SetData("unique", chkUnique.Checked ? "yes" : "no");
                    clbBuildings_SelectedIndexChanged(sender, e);
                }
            }
        }

        private XMLNodeStore FindBuilding(string sectionAndName)
        {
            string[] selected = sectionAndName.Split('>');
            foreach (KeyValuePair<string, List<XMLNodeStore>> buildingSet in _buildingSets)
            {
                if (buildingSet.Key == selected[0].Trim())
                {
                    foreach (XMLNodeStore building in buildingSet.Value)
                    {
                        if (building.GetData("name") == selected[1].Trim())
                        {
                            return building;
                        }
                    }
                }
            }
            Debug.Fail("No matching building found. Should never get here.");
            return new XMLNodeStore();
        }
        private void SelectBuildingsByTag(string tag)
        {
            clbBuildings.SetCheckValueForAllItems(false);
            foreach (KeyValuePair<string, List<XMLNodeStore>> buildingSet in _buildingSets)
            {
                foreach (XMLNodeStore building in buildingSet.Value)
                {
                    if (building.GetData("tags").Contains(tag))
                    {
                        clbBuildings.CheckItemByText(buildingSet.Key + " > " + building.GetData("name"));
                    }
                }
            }
        }
        private bool ValidateUserSelections()
        {
            bool IsValidTheme = true;
            #region Validate Theme
            if (txtThemeName.Text.Length == 0)
            {
                txtLog.Text = txtLog.Text + "\r\n\tTheme: Please enter a name.";
                IsValidTheme = false;
            }
            else if (File.Exists(Path.Combine("Resources", "Themes", txtThemeName.Text + ".xml")))
            {
                //File.Delete(Path.Combine("Resources", "Themes", txtThemeName.Text + ".xml"));
                txtLog.Text = txtLog.Text + "\r\n\tTheme: This theme name is already in use. Please use a different name.";
                IsValidTheme = false;
            }
            #endregion
            #region Validate City Name
            #endregion
            #region Validate Features
            // not possible to mess up Features
            #endregion
            #region Validate Buildings
            if (!chkUseNovvBuildings.Checked)
            {
                // need at least one city entrance
                if (clbFeatures.IsChecked("Farms") &&
                    !clbBuildings.IsStartsWithChecked("farm"))
                {
                    txtLog.Text = txtLog.Text + "\r\n\tBuildings: Please select at least one farm building.";
                    IsValidTheme = false;
                }

                // need at least one farm building, if a farm is wanted
                if (clbFeatures.IsChecked("Farms") &&
                    !clbBuildings.IsStartsWithChecked("city entrance"))
                {
                    txtLog.Text = txtLog.Text + "\r\n\tBuildings: Please select at least one city entrance.";
                    IsValidTheme = false;
                }

                // need at least one mineshaft entrance, if a mineshaft is wanted
                if (clbFeatures.IsChecked("Mineshaft") &&
                    !clbBuildings.IsStartsWithChecked("mineshaft entrance"))
                {
                    txtLog.Text = txtLog.Text + "\r\n\tBuildings: Please select at least one mineshaft entrance.";
                    IsValidTheme = false;
                }
                // need at least one mineshaft section, if a mineshaft is wanted
                if (clbFeatures.IsChecked("Mineshaft") &&
                    !clbBuildings.IsStartsWithChecked("mineshaft section"))
                {
                    txtLog.Text = txtLog.Text + "\r\n\tBuildings: Please select at least one mineshaft section.";
                    IsValidTheme = false;
                }
                // need at least one sky feature, if a sky feature is wanted
                if (clbFeatures.IsChecked("Sky Feature") &&
                    !clbBuildings.IsStartsWithChecked("sky feature"))
                {
                    txtLog.Text = txtLog.Text + "\r\n\tBuildings: Please select at least one sky feature.";
                    IsValidTheme = false;
                }

            }
            #endregion
            #region Validate Size
            if (clbFeatures.IsChecked("Farms") &&
                clbFarmSizes.CheckedItems.Count == 0)
            {
                txtLog.Text = txtLog.Text + "\r\n\tSize: Please select at least one farm size.";
                IsValidTheme = false;
            }
            if (clbCitySizes.CheckedItems.Count == 0)
            {
                txtLog.Text = txtLog.Text + "\r\n\tSize: Please select at least one city size.";
                IsValidTheme = false;
            }
            #endregion
            #region Validate Decorations
            if (clbFeatures.IsChecked("Walls") &&
                clbCityEntranceEmblems.CheckedItems.Count == 0)
            {
                txtLog.Text = txtLog.Text + "\r\n\tDecorations: Please select at least one city entrance emblem.";
                IsValidTheme = false;
            }
            if (clbFeatures.IsChecked("Guard Towers Addition") &&
                clbGuardTowerAdditions.CheckedItems.Count == 0)
            {
                txtLog.Text = txtLog.Text + "\r\n\tDecorations: Please select at least one guard tower addition.";
                IsValidTheme = false;
            }
            #endregion
            #region Validate Blocks
            if (clbGroundBlock.CheckedItems.Count == 0)
            {
                txtLog.Text = txtLog.Text + "\r\n\tBlocks: Please select at least one ground block.";
                IsValidTheme = false;
            }
            if (clbWallBlock.CheckedItems.Count == 0)
            {
                txtLog.Text = txtLog.Text + "\r\n\tBlocks: Please select at least one wall block.";
                IsValidTheme = false;
            }
            if (clbPathBlock.CheckedItems.Count == 0)
            {
                txtLog.Text = txtLog.Text + "\r\n\tBlocks: Please select at least one path block.";
                IsValidTheme = false;
            }
            // not possible to mess up the Underground Blocks list
            #endregion
            #region Validate Moat
            if (clbFeatures.IsChecked("Moat") &&
                clbMoatTypes.CheckedItems.Count == 0)
            {
                txtLog.Text = txtLog.Text + "\r\n\tMoat: Please select at least one moat.";
                IsValidTheme = false;
            }
            #endregion
            #region Validate Lights
            if (clbFeatures.IsChecked("Street Lights") &&
                clbStreetLights.CheckedItems.Count == 0)
            {
                txtLog.Text = txtLog.Text + "\r\n\tLights: Please select at least one street light.";
                IsValidTheme = false;
            }
            if (clbFeatures.IsChecked("Outside Lights") &&
                clbOutsideLights.CheckedItems.Count == 0)
            {
                txtLog.Text = txtLog.Text + "\r\n\tLights: Please select at least one outside light.";
                IsValidTheme = false;
            }
            #endregion
            #region Validate Flowers
            if (clbFeatures.IsChecked("Flowers") &&
                clbFlowers.CheckedItems.Count == 0)
            {
                txtLog.Text = txtLog.Text + "\r\n\tFlowers: Please select at least one type of flower.";
                IsValidTheme = false;
            }
            #endregion
            return IsValidTheme;
        }
        private void ShowExampleCityNames(object sender, EventArgs e)
        {
            if (cmbCityStartingWord.SelectedIndex >= 0 && cmbCityEndingWord.SelectedIndex >= 0)
            {
                string strNames = String.Empty;
                for (int x = 0; x < 50; x++)
                {
                    string strStart = RNG.RandomFileLine(Path.Combine("Resources", cmbCityStartingWord.Text.Replace(' ', '_') + ".txt"));
                    string strEnd = RNG.RandomFileLine(Path.Combine("Resources", cmbCityEndingWord.Text.Replace(' ', '_') + ".txt"));
                    string strCityName = strStart + strEnd;
                    strNames += txtCityPrefix.Text + strCityName + "\r\n";
                }
                txtExampleCityNames.Text = "Example city names:\r\n\r\n" + strNames.Substring(0, strNames.Length - 2);
            }
        }
        private void DisplayBuildings()
        {
            foreach (KeyValuePair<string, List<XMLNodeStore>> buildingSet in _buildingSets)
            {
                foreach (XMLNodeStore buildingNode in buildingSet.Value)
                {
                    if (buildingNode.GetData("tags").ToLower() != "novv")
                    {
                        clbBuildings.Items.Add(buildingSet.Key + " > " + buildingNode.GetData("name"));
                    }
                }
            }
        }
        private void StoreBuildingSets()
        {
            _buildingSets.Clear();
            foreach (string buildingType in _buildingTypes)
            {
                _buildingSets.Add(buildingType,
                                  XMLHelper.CreateNodeStores(Path.Combine("Resources", "Buildings", buildingType.Replace(" ", "") + ".xml"), "building"));
            }
        }
        private void CreateTheme()
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine("Resources", "Themes", txtThemeName.Text + ".xml")))
            {
                writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                writer.WriteLine("<theme>");

                #region include
                writer.WriteLine("\t<include>");
                for (int a = 0; a < clbFeatures.Items.Count; a++)
                {
                    string feature = clbFeatures.Items[a].ToString().Replace(' ', '_').ToLower();
                    writer.WriteLine("\t\t<" + feature + ">" + (clbFeatures.IsChecked(a) ? "yes" : "no") + "</" + feature + ">");
                }
                writer.WriteLine("\t</include>");
                #endregion include

                #region options
                writer.WriteLine("\t<options>");
                writer.WriteLine("\t\t<moat>" + String.Join(",", clbMoatTypes.CheckedItems.OfType<string>().ToArray()) + "</moat>");
                writer.WriteLine("\t\t<street_lights>" + String.Join(",", clbStreetLights.CheckedItems.OfType<string>().ToArray()) + "</street_lights>");
                writer.WriteLine("\t\t<emblem>" + String.Join(",", clbCityEntranceEmblems.CheckedItems.OfType<string>().ToArray()) + "</emblem>");
                writer.WriteLine("\t\t<outside_lights>" + String.Join(",", clbOutsideLights.CheckedItems.OfType<string>().ToArray()) + "</outside_lights>");
                writer.WriteLine("\t\t<tower_addition>" + String.Join(",", clbGuardTowerAdditions.CheckedItems.OfType<string>().ToArray()) + "</tower_addition>");
                writer.WriteLine("\t\t<flowers>" + TextToBlocks(clbFlowers.CheckedItems.OfType<string>().ToArray()) + "</flowers>");
                writer.WriteLine("\t\t<flower_percent>" + tbFlowerFrequency.Value + "</flower_percent>");
                writer.WriteLine("\t\t<underground>" + UndergroundBlocks(lbUndergroundBlocks.SelectedItem.ToString().Split(' ')[0]) + "</underground>");
                writer.WriteLine("\t\t<wall_material>" + TextToBlocks(clbWallBlock.CheckedItems.OfType<string>().ToArray()) + "</wall_material>");
                writer.WriteLine("\t\t<path>" + String.Join(",", clbPathBlock.CheckedItems.OfType<string>().ToArray()).
                                 Replace("(", "").Replace(")", "").Replace(" ", "_").ToLower() + "</path>");
                writer.WriteLine("\t\t<city_size>" + String.Join(",", clbCitySizes.CheckedItems.OfType<string>().ToArray()) + "</city_size>");
                writer.WriteLine("\t\t<farm_size>" + String.Join(",", clbFarmSizes.CheckedItems.OfType<string>().ToArray()) + "</farm_size>");
                writer.WriteLine("\t\t<ground_block>" + TextToBlocks(clbGroundBlock.CheckedItems.OfType<string>().ToArray()) + "</ground_block>");
                writer.WriteLine("\t\t<city_prefix>" + txtCityPrefix.Text + "</city_prefix>");
                writer.WriteLine("\t\t<city_prefix_file>" + cmbCityStartingWord.Text + ".txt</city_prefix_file>");
                writer.WriteLine("\t\t<city_suffix_file>" + cmbCityEndingWord.Text + ".txt</city_suffix_file>");

                writer.WriteLine("\t</options>");
                #endregion options

                #region buildings
                writer.WriteLine("\t<buildings>");

                foreach (KeyValuePair<string, List<XMLNodeStore>> buildingSet in _buildingSets)
                {
                    writer.WriteLine("\t\t<" + buildingSet.Key.ToLower().Replace(' ', '_') + ">");
                    foreach (XMLNodeStore buildingNode in buildingSet.Value)
                    {
                        if ((chkUseNovvBuildings.Checked && buildingNode.GetData("tags").Contains("novv")) ||
                            (!chkUseNovvBuildings.Checked && clbBuildings.IsChecked(buildingSet.Key + " > " + buildingNode.GetData("name"))))
                        {
                            writer.WriteLine("\t\t\t<building>");
                            writer.WriteLine(buildingNode.ToXML("\t\t\t\t"));
                            writer.WriteLine("\t\t\t</building>");
                        }
                    }
                    writer.WriteLine("\t\t</" + buildingSet.Key.ToLower().Replace(' ', '_') + ">");
                }

                writer.WriteLine("\t</buildings>");
                #endregion

                writer.WriteLine("</theme>");
            }
        }
        private static string UndergroundBlocks(string selectedUnderground)
        {
            switch (selectedUnderground.ToLower())
            {
                case "desert":
                    return "1,3,12,12,13,24";
                default:
                    return "1,1,1,3,3,12,13";
            }
        }
        private static string TextToBlocks(string[] identifiers)
        {
            Dictionary<string, string> blocks = new Dictionary<string, string>();
            blocks.Add("grass", BlockInfo.Grass.ID + "_0");
            blocks.Add("sand", BlockInfo.Sand.ID + "_0");
            blocks.Add("brick", BlockInfo.BrickBlock.ID + "_0");
            blocks.Add("cobblestone", BlockInfo.Cobblestone.ID + "_0");
            blocks.Add("ice", BlockInfo.Ice.ID + "_0");
            blocks.Add("netherrack", BlockInfo.Netherrack.ID + "_0");
            blocks.Add("sandstone", BlockInfo.Sandstone.ID + "_0");
            blocks.Add("stone brick", BlockInfo.StoneBrick.ID + "_0");
            blocks.Add("stone", BlockInfo.Stone.ID + "_0");
            blocks.Add("wood log", BlockInfo.Wood.ID + "_0");
            blocks.Add("wood plank", BlockInfo.WoodPlank.ID + "_0");

            blocks.Add("cactus", BlockInfo.Cactus.ID + "_0");
            blocks.Add("dead shrub", BlockInfo.DeadShrub.ID + "_0");
            blocks.Add("flower: dandelion", BlockInfo.YellowFlower.ID + "_0");
            blocks.Add("flower: rose", BlockInfo.RedRose.ID + "_0");
            blocks.Add("grass: fern", BlockInfo.TallGrass.ID + "_" + (int)TallGrassType.FERN);
            blocks.Add("grass: tall", BlockInfo.TallGrass.ID + "_" + (int)TallGrassType.TALL_GRASS);
            blocks.Add("pumpkin", BlockInfo.Pumpkin.ID + "_0");
            blocks.Add("sapling: birch", BlockInfo.Sapling.ID + "_" + SaplingType.BIRCH);
            blocks.Add("sapling: oak", BlockInfo.Sapling.ID + "_" + SaplingType.OAK);
            blocks.Add("sapling: spruce", BlockInfo.Sapling.ID + "_" + SaplingType.SPRUCE);

            for (int i = 0; i < identifiers.GetLength(0); i++)
            {
                if (blocks.ContainsKey(identifiers[i].ToLower()))
                {
                    identifiers[i] = blocks[identifiers[i].ToLower()];
                }
                else
                {
                    Debug.Fail("Invalid block: " + identifiers[i]);
                }
            }
            return String.Join(",", identifiers);
        }
    }
    class XMLNodeStore
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        public void SetData(string key, string value)
        {
            if (data.ContainsKey(key))
            {
                data.Remove(key);
            }
            data.Add(key, value);
        }
        public string GetData(string key)
        {
            if (data.ContainsKey(key))
            {
                return data[key];
            }
            else
            {
                return String.Empty;
            }
        }
        public string ToXML(string prefix)
        {
            string output = String.Empty;
            foreach (var item in data.OrderBy(entry => entry.Key))
            {
                if (item.Key != "xml_file")
                {
                    output += String.Format("{0}<{1}>{2}</{1}>\r\n", prefix, item.Key, item.Value);
                }
            }
            if (output.Length > 0)
            {
                output = output.Substring(0, output.Length - 2);
            }
            return output;
        }
        public string ToText()
        {
            string output = String.Empty;
            foreach (var item in data.OrderBy(entry => entry.Key))
            {
                if (item.Key != "xml_file")
                {
                    output += item.Key + " = " + item.Value + "\r\n";
                }
            }
            if (output.Length > 0)
            {
                output = output.Substring(0, output.Length - 2);
            }
            return output;
        }
    }
    static class XMLHelper
    {
        public static List<XMLNodeStore> CreateNodeStores(string xmlFileName, string nodeName)
        {
            List<XMLNodeStore> allData = new List<XMLNodeStore>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);
            foreach (XmlNode xmlNode in xmlDoc.GetElementsByTagName(nodeName))
            {
                XMLNodeStore currentData = new XMLNodeStore();
                foreach (XmlElement xmlElement in xmlNode)
                {
                    currentData.SetData(xmlElement.Name, xmlElement.InnerText);
                }
                currentData.SetData("xml_file", Path.GetFileNameWithoutExtension(xmlFileName));
                allData.Add(currentData);
            }
            return allData;
        }
    }
    static class ListExtensions
    {
        public static bool IsChecked(this CheckedListBox clb, string value)
        {
            return clb.CheckedItems.Contains(value);
        }
        public static bool IsChecked(this CheckedListBox clb, int index)
        {
            return clb.CheckedItems.Contains(clb.Items[index]);
        }
        public static bool IsContainsChecked(this CheckedListBox clb, string value)
        {
            for (int a = 0; a < clb.CheckedItems.Count; a++)
            {
                if (clb.CheckedItems[a].ToString().ToLower().Contains(value.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsStartsWithChecked(this CheckedListBox clb, string value)
        {
            for (int a = 0; a < clb.CheckedItems.Count; a++)
            {
                if (clb.CheckedItems[a].ToString().ToLower().StartsWith(value.ToLower()))
                {
                    return true;
                }

            }
            return false;
        }
        public static void SetCheckValueForAllItems(this CheckedListBox clb, bool isChecked)
        {
            for (int a = 0; a < clb.Items.Count; a++)
            {
                clb.SetItemChecked(a, isChecked);
            }
        }
        public static void CheckAsterixedItems(this CheckedListBox clb)
        {
            for (int a = 0; a < clb.Items.Count; a++)
            {
                if (clb.Items[a].ToString().EndsWith("*"))
                {
                    clb.Items[a] = clb.Items[a].ToString().Substring(0, clb.Items[a].ToString().Length - 1);
                    clb.SetItemChecked(a, true);
                }
            }
        }
        public static void CheckItemByText(this CheckedListBox clb, string value)
        {
            for (int a = 0; a < clb.Items.Count; a++)
            {
                if (clb.Items[a].ToString() == value)
                {
                    clb.SetItemChecked(a, true);
                }
            }
        }
        public static void SelectAsterixedItem(this ListBox lb)
        {
            for (int a = 0; a < lb.Items.Count; a++)
            {
                if (lb.Items[a].ToString().EndsWith("*"))
                {
                    lb.Items[a] = lb.Items[a].ToString().Substring(0, lb.Items[a].ToString().Length - 1);
                    lb.SelectedIndex = a;
                }
            }
        }
    }
    static class HelpMessages
    {
        private const string NO_HELP = "No help for this yet :( Apply now to join the Help Department!";
        public static string SendHelp(string section, string item)
        {
            switch (section)
            {
                case "features":
                    switch (item)
                    {
                        case "buildings":
                            return "Buildings inside the city walls, such as houses, shops and services.";
                        case "drawbridges":
                            return "Includes a free city entrance.";
                        case "emblems":
                            return "Decorative art situated next to the city entrances.";
                        case "farms":
                            return "Never go hungry again! Farms are situated outside the city walls.";
                        case "flowers":
                            return "Play it with flowers. Flowers appear in the city and in the farms.";
                        case "guard towers":
                            return "Defend your city and view your surroundings from these.";
                        case "guard towers addition":
                            return "Add a decoration to improve your city.";
                        case "mineshaft":
                            return "Venture further into the mineshaft to get the most valuable treasures and resources, but watch out for monsters!";
                        case "moat":
                            return "Moats help to protect the city during battle.";
                        case "outside lights":
                            return "These help you locate your city when you are far away.";
                        case "paths":
                            return "Paths help you navigate the city. Some paths are named.";
                        case "sky feature":
                            return "Sky features might help you with navigation.";
                        case "street lights":
                            return "Street lights will keep you safe at night.";
                        case "torches on walkways":
                            return "Disable these if you are making your walls from ice or snow.";
                        case "walls":
                            return "City walls help to protect your city and make it look more important.";
                        default:
                            return NO_HELP;
                    }
                case "moats":
                    switch (item)
                    {
                        case "cactus":
                            return "Perfect for your desert or wild west city.";
                        case "cactus low":
                            return "Like the regular cactus moat, but it will break up with you on your birthday.";
                        case "drop to bedrock":
                            return "Not recommended for sufferers of bathophobia.";
                        case "fire":
                            return "Remember - Only YOU can prevent forest fires.";
                        case "lava":
                            return "Watch your step! RIP Carlos";
                        case "water":
                            return "The original moat. It's a classic. Bring a boat for fast navigation.";
                        default:
                            return NO_HELP;
                    }
                default:
                    return NO_HELP;
            }
        }
    }

}