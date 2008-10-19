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
            worker.DoWork += (s, e) =>
            {
                if (InvokeRequired)
                {
                    Invoke((ThreadStart)(() =>
                    {
                        menuItemRefresh.Enabled = false;
                        progressBar1.Visible = true;
                        progressBar1.MarqueeAnimationSpeed = 100;
                    }));
                }
                else
                {
                    menuItemRefresh.Enabled = false;
                    progressBar1.Visible = true;
                    progressBar1.MarqueeAnimationSpeed = 100;
                }

                var container = new SolutionFileCollection();
                container.Load(@"D:\SVN_Working_Copies\GAdvance");

                var projects = new ProjectCollection();
                projects.Load(container);
                e.Result = projects;
            };

            worker.RunWorkerCompleted += (s, e) =>
            {
                projectTree1.Projects = (ProjectCollection)e.Result;
                progressBar1.MarqueeAnimationSpeed = 0;
                progressBar1.Visible = false;
                menuItemRefresh.Enabled = true;
            };

            worker.RunWorkerAsync();
        }

        private void menuItemRefresh_Click(object sender, EventArgs e)
        {
            worker.RunWorkerAsync();
        }

        private void menuItemQuit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
