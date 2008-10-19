using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager;

namespace ProjectManagerGUI
{
    public partial class ProjectTree : UserControl
    {
        private ProjectCollection projects;
        public ProjectCollection Projects
        {
            get { return projects; }
            set
            {
                if (projects != value)
                {
                    projects = value;

                    treeView.BeginUpdate();
                    treeView.Nodes.Clear();

                    var structuredProjectsNode = new TreeNode { Text = "Structured Projects" };
                    treeView.Nodes.Add(structuredProjectsNode);

                    var projectsNode = new TreeNode { Text = "Projects" };
                    treeView.Nodes.Add(projectsNode);

                    foreach (var project in projects)
                    {
                        var sp = project as StructuredProject;
                        if (sp != null)
                        {
                            var projectNode = AddProject(structuredProjectsNode, project);
                            AddTrunk(projectNode, sp.SolutionFile);
                            AddBranchTag(projectNode, sp.BranchesSolutionFiles, "branches");
                            AddBranchTag(projectNode, sp.TagsSolutionFiles, "tags");
                        }
                        else
                        {
                            AddProject(projectsNode, project);
                        }
                    }

                    treeView.Sort();
                    structuredProjectsNode.Expand();
                    
                    treeView.EndUpdate();
                }
            }
        }

        private static TreeNode AddProject(TreeNode node, Project project)
        {
            var projectNode = new TreeNode { Text = project.Name, Tag = project };
            node.Nodes.Add(projectNode);
            return projectNode;
        }

        private static void AddTrunk(TreeNode projectNode, SolutionFile trunk)
        {
            if (trunk != null)
                projectNode.Nodes.Add(new TreeNode { Text = "trunk" });
        }

        private static void AddBranchTag(TreeNode projectNode, ICollection<BranchTagBase> items, string rootName)
        {
            if (items.Count > 0)
            {
                var branchesNode = new TreeNode { Text = rootName };
                projectNode.Nodes.Add(branchesNode);
                foreach (var item in items)
                    branchesNode.Nodes.Add(new TreeNode { Text = item.Name, ToolTipText = item.SolutionFile.FullName });
            }
        }

        public ProjectTree()
        {
            InitializeComponent();
        }

        private void treeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView.SelectedNode = treeView.GetNodeAt(e.X, e.Y);
            }
        }
    }
}
