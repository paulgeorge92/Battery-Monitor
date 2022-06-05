using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using BatteryMonitor.Properties;
using System.Diagnostics;

namespace BatteryMonitor
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            var settings = SettingsService.LoadSettings();
            numMinCharge.Value = settings.MinCharge;
            numMaxCharge.Value = settings.MaxCharge;
            cb_RunAtStartup.Checked = settings.RunAtStartup;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            setStartup();

            if (SettingsService.SaveSettings((ushort)numMinCharge.Value, (ushort)numMaxCharge.Value, cb_RunAtStartup.Checked))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Something went wrong. Please try again.", "Error", MessageBoxButtons.OK);
            }
        }

        void setStartup()
        {
            string SHORTCUT_PATH = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\Battery Monitor.lnk";
            if (cb_RunAtStartup.Checked)
            {
                if (!System.IO.File.Exists(SHORTCUT_PATH))
                {
                    WshShell wshShell = new WshShell();
                    IWshShortcut shortcut;
                    shortcut = wshShell.CreateShortcut(SHORTCUT_PATH);
                    shortcut.TargetPath = Application.ExecutablePath;
                    shortcut.Description = "Launch Battery Monitor";
                    shortcut.Save();
                }
            }
            else
            {
                if (System.IO.File.Exists(SHORTCUT_PATH))
                {
                    System.IO.File.Delete(SHORTCUT_PATH);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            
           
        }
    }
}
