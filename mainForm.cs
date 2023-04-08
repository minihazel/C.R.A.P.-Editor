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
                ammoBox.Visible = false;

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

            Debug.WriteLine(configFile);

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

        public void listPatronInfo(string path)
        {
            string matchedPatron = patronList.SelectedItem.ToString().ToLower();
            string configFile = Path.Combine(path, "config.json");

            Debug.WriteLine(configFile);

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

            ammoBox.Visible = true;
        }

        private void patronList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (patronList.SelectedIndex > -1)
            {
                string _ammoPath = Path.Combine(configFolder, configList.SelectedItem.ToString());

                string prettify = patronList.SelectedItem.ToString().ToString().Replace("_", " ");
                prettify = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(prettify);
                prettify = prettify.Replace("X", "x");

                selectedPatronTitle.Text = $"Selected ammo: {prettify}";
                listPatronInfo(_ammoPath);
            }
        }
    }
}
