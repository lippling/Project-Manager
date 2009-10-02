using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ProjectManager;
using System.Threading;
using ProjectManagerGUI.Properties;

namespace ProjectManagerGUI
{
    public partial class FormMain : Form
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();

        public FormMain()
        {
            InitializeComponent();
            SetWindowTitle();
            notifyIcon.Icon = Icon;

            var screen = Screen.AllScreens.First(s => s.Primary);
            Left = screen.WorkingArea.Width - Width;
            Top = screen.WorkingArea.Height - Height;

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
                container.Load(Settings.Default.WorkingCopyLocation);

                var projects = new ProjectCollection();
                projects.Load(container);
                e.Result = projects;
            };

            worker.RunWorkerCompleted += (s, e) =>
            {
                var fav = Settings.Default.FavoriteProjects;
                projectTree.FavoriteProjects = fav != null ? fav.Cast<string>() : new List<string>();
                projectTree.Projects = (ProjectCollection)e.Result;
                progressBar.MarqueeAnimationSpeed = 0;
                progressBar.Visible = false;
                menuItemRefresh.Enabled = true;
            };

            worker.RunWorkerAsync();
        }

        private void SetWindowTitle()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var version = new Version(assembly.GetAttribute<AssemblyFileVersionAttribute>().Version).ToString(2);
            var copyright = assembly.GetAttribute<AssemblyCopyrightAttribute>().Copyright;
            var company = assembly.GetAttribute<AssemblyCompanyAttribute>().Company;
            Text += string.Format(" {0} {1} {2}", version, copyright, company);
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
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Show();
                Activate();
            }
        }
    }
}
