using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tankowanie
{
    public partial class frmMain : Form
    {
        public void openProcess()
        {
            string fn = ConfigurationManager.AppSettings["fn"];

            //uruchomienie procesu
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = fn;
            Process.Start(startInfo);

            Process isOpen = new Process();
            string path = fn;
            string fileName = Path.GetFileName(path);

            Process[] processName = Process.GetProcessesByName(fileName.Substring(0, fileName.LastIndexOf('.')));
            if (processName.Length > 0)
            {
                tsLbl.Text = "MySQL server is running";
                tsMenu.Enabled = true;
            }
            else
            {
                tsLbl.Text = "MySQL server is not running";
            }
        }
        public void closingProcess()
        {
            foreach (var process in Process.GetProcessesByName("mysqld"))
            {
                process.Kill();
            }
        }
        public frmMain()
        {
            InitializeComponent();
        }
 
        private void frmMain_Load(object sender, EventArgs e)
        {
            openProcess();
        }

        private void loginToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
                frmLogin form = new frmLogin();
                //jezeli połączenie powiodło się
                if (form.ShowDialog() == DialogResult.OK)
                {
                    tsSamochody.Enabled = true;
                }
            
        }

        private void dodajTankowanieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCars form = new frmCars();
            form.ShowDialog();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            closingProcess();
        }

        private void tsExit_Click(object sender, EventArgs e)
        {
            closingProcess();
            Dispose();
        }


    }
}
