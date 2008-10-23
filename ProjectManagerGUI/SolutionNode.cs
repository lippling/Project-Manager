using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager;

namespace ProjectManagerGUI
{
    public class SolutionNode : TreeNode
    {
        public Solution Solution { get; set; }
    }
}
