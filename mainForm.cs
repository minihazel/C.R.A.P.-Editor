using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using C.R.A.P._Editor;

namespace C.R.A.P._Editor
{
    public partial class mainForm : Form
    {
        public string currentDir = "F:\\SPT Iterations\\SPT 3.5.0 21703\\user\\mods\\Tyr-ComprehensiveRewrittenAmmunitionPackage";
        public string configFolder;

        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            selectedAmmoTitle.Text = "";
            selectedPatronTitle.Text = "";

            configFolder = Path.Combine(currentDir, "config");
            bool configFolderExists = Directory.Exists(configFolder);
            if (configFolderExists)
            {
                string[] ammoTypes = Directory.GetDirectories(configFolder);
                foreach (string ammoType in ammoTypes )
                {
                    bool ammoFolderExists = Directory.Exists(ammoType);
                    if (ammoFolderExists)
                    {
                        string itemName = Path.GetFileName(ammoType);

                        configList.Items.Add(itemName);
                    }
                    else
                    {
                        continue;
                    }
                }

                foreach (Control item in ammoBox.Controls)
                {
                    if (item != null && item is Panel)
                    {
                        foreach (Control textItem in item.Controls)
                        {
                            if (textItem is TextBox || textItem is Button)
                            {
                                textItem.Text = "";
                                textItem.Enabled = false;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Config folder doesn\'t exist, or you didn\'t place the editor in the right place.", this.Text, MessageBoxButtons.OK);
                Application.Exit();
            }
        }

        private void configList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (configList.SelectedIndex > -1)
            {
                patronList.Items.Clear();

                string prettify = configList.SelectedItem.ToString().ToString().Replace("_", " ");
                prettify = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(prettify);

                selectedAmmoTitle.Text = $"Selected ammo: {prettify}";
                string ammoPath = Path.Combine(configFolder, configList.SelectedItem.ToString());

                fetchPatronInfo(ammoPath);
            }
        }

        public void fetchPatronInfo(string path)
        {
            string configFile = Path.Combine(path, "config.json");

            bool configFileExists = File.Exists(configFile);
            if (configFileExists)
            {
                string configContent = File.ReadAllText(configFile);
                JavaScriptSerializer configSerializer = new JavaScriptSerializer();
                dynamic configObject = configSerializer.Deserialize<Dictionary<string, object>>(configContent);

                var keys = configObject.Keys;
                foreach (var key in keys)
                {
                    patronList.Items.Add(key);
                }
            }
        }

        private void patronList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (patronList.SelectedIndex > -1)
            {
                foreach (Control item in ammoBox.Controls)
                {
                    if (item != null && item is Panel)
                    {
                        foreach (Control textItem in item.Controls)
                        {
                            if (textItem is TextBox || textItem is Button)
                            {
                                textItem.Text = "";
                                textItem.Enabled = true;
                            }
                        }
                    }
                }

                string _ammoPath = Path.Combine(configFolder, configList.SelectedItem.ToString());

                string prettify = patronList.SelectedItem.ToString().ToString().Replace("_", " ");
                prettify = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(prettify);
                prettify = prettify.Replace("X", "x");

                selectedPatronTitle.Text = $"Selected ammo: {prettify}";
                listPatronInfo(_ammoPath);
            }
        }

        public void listPatronInfo(string path)
        {
            string matchedPatron = patronList.SelectedItem.ToString().ToLower();
            string configFile = Path.Combine(path, "config.json");

            bool configFileExists = File.Exists(configFile);
            if (configFileExists)
            {
                string configContent = File.ReadAllText(configFile);
                JavaScriptSerializer configSerializer = new JavaScriptSerializer();
                dynamic configObject = configSerializer.Deserialize<Dictionary<string, object>>(configContent);

                foreach (KeyValuePair<string, dynamic> item in configObject[matchedPatron])
                {
                    if (item.Value != null)
                    {
                        string key = item.Key;
                        dynamic value = item.Value;

                        switch (key.ToLower())
                        {
                            case "enabled":
                                if (item.Value == false)
                                {
                                    chkAmmoEnabled.Checked = false;
                                }
                                else
                                {
                                    chkAmmoEnabled.Checked = true;
                                }
                                break;

                            case "weight":
                                valueWeight.Text = item.Value.ToString();
                                break;

                            case "initialspeed":
                                valueInitialSpeed.Text = item.Value.ToString();
                                break;

                            case "damage":
                                valueDamage.Text = item.Value.ToString();
                                break;

                            case "ammoaccr":
                                valueAccuracy.Text = item.Value.ToString();
                                break;

                            case "ammorec":
                                valueRecoil.Text = item.Value.ToString();
                                break;

                            case "penetrationpower":
                                valuePenetrationPower.Text = item.Value.ToString();
                                break;

                            case "projectilecount":
                                valueProjectileCount.Text = item.Value.ToString();
                                break;

                            case "misfirechance":
                                valueMisfireChance.Text = item.Value.ToString();
                                break;

                            case "minfragmentscount":
                                valueMinFragmentsCount.Text = item.Value.ToString();
                                break;

                            case "maxfragmentscount":
                                valueMaxFragmentsCount.Text = item.Value.ToString();
                                break;

                            case "fragmentationchance":
                                valueFragmentationChance.Text = item.Value.ToString();
                                break;

                            case "tracercolor":
                                valueTracerColor.Text = item.Value.ToString();
                                break;

                            case "armordamage":
                                valueArmorDamage.Text = item.Value.ToString();
                                break;

                            case "staminaburnperdamage":
                                valueStaminaBurnPerDamage.Text = item.Value.ToString();
                                break;

                            case "heavybleedingdelta":
                                valueHeavyBleedingDelta.Text = item.Value.ToString();
                                break;

                            case "lightbleedingdelta":
                                valueLightBleedingDelta.Text = item.Value.ToString();
                                break;

                            case "malfmisfirechance":
                                valueMalfunctionMisfireChance.Text = item.Value.ToString();
                                break;

                            case "durabilityburnmodificator":
                                valueDurabilityBurnModificator.Text = item.Value.ToString();
                                break;

                            case "heatfactor":
                                valueHeatFactor.Text = item.Value.ToString();
                                break;

                            case "malffeedchance":
                                valueMalfunctionFeedChance.Text = item.Value.ToString();
                                break;

                            case "explosive":
                                valueExplosive.Text = item.Value.ToString();
                                break;
                        };


                        if (key.ToLower() == "enabled")
                        {
                            if (value.ToString() == "False")
                            {
                                chkAmmoEnabled.Checked = false;
                            }
                            else
                            {
                                chkAmmoEnabled.Checked = true;
                            }
                        }
                        else if (key.ToLower() == "bulletid")
                        {
                        }
                        else if (key.ToLower() == "tracer")
                        {
                            if (value.ToString() == "False")
                            {
                                chkAmmoTracer.Checked = false;
                            }
                            else
                            {
                                chkAmmoTracer.Checked = true;
                            }
                        }
                        else if (key.ToLower() == "blinding")
                        {
                            if (value.ToString() == "False")
                            {
                                chkAmmoBlinding.Checked = false;
                            }
                            else
                            {
                                chkAmmoBlinding.Checked = true;
                            }
                        }
                        else if(key.ToLower() == "weight")
                        {

                        }

                    }
                }

            }

            ammoBox.Visible = true;
        }

        private void valueTracerColor_Click(object sender, EventArgs e)
        {
            if (valueTracerColor.Text.ToLower() == "red")
            {
                valueTracerColor.Text = "blue";
            }
            else if (valueTracerColor.Text.ToLower() == "blue")
            {
                valueTracerColor.Text = "green";
            }
            else if (valueTracerColor.Text.ToLower() == "green")
            {
                valueTracerColor.Text = "red";
            }
        }
    }
}
