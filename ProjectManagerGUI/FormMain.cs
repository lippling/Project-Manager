using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager;
using System.IO;
using System.Xml.Serialization;
using System.Threading;

namespace ProjectManagerGUI
{
    public partial class FormMain : Form
    {
        private BackgroundWorker worker = new BackgroundWorker();

        public FormMain()
        {
            InitializeComponent();
            notifyIcon.Icon = Icon;
            worker.DoWork += (s, e) =>
            {
                if (InvokeRequired)
                {
                    Invoke((ThreadStart)(() =>
                    {
                        menuItemRefresh.Enabled = false;
                        progressBar.Visible = true;
                        progressBar.MarqueeAnimationSpeed = 100;
                    }));
                }
                else
                {
                    menuItemRefresh.Enabled = false;
                    progressBar.Visible = true;
                    progressBar.MarqueeAnimationSpeed = 100;
                }

                var container = new SolutionCollection();
                container.Load(@"D:\SVN_Working_Copies\GAdvance");

                var projects = new ProjectCollection();
                projects.Load(container);
                e.Result = projects;
            };

            worker.RunWorkerCompleted += (s, e) =>
            {
                projectTree.Projects = (ProjectCollection)e.Result;
                progressBar.MarqueeAnimationSpeed = 0;
                progressBar.Visible = false;
                menuItemRefresh.Enabled = true;
            };

            worker.RunWorkerAsync();
        }

        private void menuItemRefresh_Click(object sender, EventArgs e)
        {
            worker.RunWorkerAsync();
        }

        private bool close;
        private void menuItemQuit_Click(object sender, EventArgs e)
        {
            close = true;
            Close();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!close)
            {
                e.Cancel = true;
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Show();
                notifyIcon.Visible = false;
            }
        }
    }
}
