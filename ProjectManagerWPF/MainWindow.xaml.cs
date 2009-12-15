using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using ProjectManager;

namespace ProjectManagerWPF
{
    public partial class MainWindow
    {
        private bool close;

        public MainWindow()
        {
            InitializeComponent();
            SetWindowTitle();
            NotifyIcon.Icon = Icon;

            var screen = Screen.AllScreens.First(s => s.Primary);
            Left = screen.WorkingArea.Width - Width;
            Top = screen.WorkingArea.Height - Height;
        }

        private void SetWindowTitle()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var version = new Version(assembly.GetAttribute<AssemblyFileVersionAttribute>().Version).ToString(2);
            var copyright = assembly.GetAttribute<AssemblyCopyrightAttribute>().Copyright;
            var company = assembly.GetAttribute<AssemblyCompanyAttribute>().Company;
            Title += string.Format(CultureInfo.CurrentCulture, " {0} {1} {2}", version, copyright, company);
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            close = true;
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!close)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void NotifyIcon_Click(object sender, RoutedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Hide();
            }
            else
            {
                Show();
                Activate();
            }
        }
    }
}
