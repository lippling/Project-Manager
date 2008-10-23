using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager;
using System.Drawing;
using System.ComponentModel;
using ProjectManagerGUI.Properties;

namespace ProjectManagerGUI
{
    public class SolutionNode : TreeNode
    {
        public Solution Solution { get; private set; }

        public SolutionNode(Solution solution)
        {
            Solution = solution;

            ContextMenuStrip = new ContextMenuStrip();

            var open = ContextMenuStrip.Items.Add("Open");
            open.Font = new Font(open.Font, FontStyle.Bold);
            open.Click += (s, e) => Solution.Open();

            var openFolder = ContextMenuStrip.Items.Add("Open Containing Folder");
            openFolder.Click += (s, e) => Solution.OpenContainingFolder();

            ContextMenuStrip.Items.Add("-");

            foreach (var conf in Solution.BuildConfigurations)
            {
                var compile = ContextMenuStrip.Items.Add("Compile " + conf.Name);
                compile.Tag = conf;
                compile.Click += new EventHandler(compile_Click);
            }
        }

        private void compile_Click(object sender, EventArgs e)
        {
            var compile = (ToolStripItem)sender;

            StateImageKey = "Play";
            var worker = new BackgroundWorker();
            worker.DoWork += (s1, e1) => e1.Result = ((BuildConfiguration)compile.Tag).Compile();
            worker.RunWorkerCompleted += (s1, e1) =>
                {
                    var r = (int)e1.Result;
                    StateImageKey = r == 0 ? null : "Record";
                };
            worker.RunWorkerAsync();
        }
    }
}
