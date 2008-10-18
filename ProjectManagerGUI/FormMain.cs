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

namespace ProjectManagerGUI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //ProjectCollection container;
            //using (var sr = new StreamReader("ProjectCollection.ser"))
            //{
            //    var x = new XmlSerializer(typeof(ProjectCollection));
            //    container = (ProjectCollection)x.Deserialize(sr);
            //}

            SolutionFileCollection container;
            using (var sw = new StreamReader("SolutionContainer.ser"))
            {
                var x = new XmlSerializer(typeof(SolutionFileCollection));
                container = (SolutionFileCollection)x.Deserialize(sw);
            }

            var projects = new ProjectCollection();
            projects.Load(container);

            projectTree1.Projects = projects;

            //var container = new SolutionFileCollection();
            //container.Load(@"D:\SVN_Working_Copies\GAdvance");

            //using (var sw = new StreamWriter("SolutionContainer.ser"))
            //{
            //    var x = new XmlSerializer(typeof(SolutionFileCollection));
            //    x.Serialize(sw, container);    
            //}

            //var projects = new ProjectCollection();
            //projects.Load(container);

            //using (var sw = new StreamWriter("ProjectCollection.ser"))
            //{
            //    var x = new XmlSerializer(typeof(ProjectCollection));
            //    x.Serialize(sw, projects);
            //}

        }
    }
}
