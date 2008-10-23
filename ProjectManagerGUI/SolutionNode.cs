using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager;
using System.Drawing;

namespace ProjectManagerGUI
{
    public class SolutionNode : TreeNode
    {
        public Solution Solution { get; set; }

        public SolutionNode()
        {
            ContextMenuStrip = new ContextMenuStrip();

            var open = ContextMenuStrip.Items.Add("Open");
            open.Font = new Font(open.Font, FontStyle.Bold);
            open.Click += (s, e) => Solution.Open();

            var openFolder = ContextMenuStrip.Items.Add("Open Containing Folder");
            openFolder.Click += (s, e) => Solution.OpenContainingFolder();
        }
    }
}
