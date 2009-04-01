using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager;
using System.Diagnostics;
using ProjectManagerGUI.Properties;

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

                    var projectsNode = new TreeNode { Text = "Projects" };
                    treeView.Nodes.Add(projectsNode);
                    foreach (var project in projects)
                    {
                        var sp = project as StructuredProjectDefinition;
                        if (sp != null)
                        {
                            var projectNode = AddProjectRoot(projectsNode, project);
                            AddTrunk(projectNode, sp.Solution);
                            AddBranchTag(projectNode, sp.BranchesSolutions, "branches");
                            AddBranchTag(projectNode, sp.TagsSolutions, "tags");
                        }
                        else
                        {
                            AddProject(projectsNode, project);
                        }
                    }

                    treeView.Sort();
                    projectsNode.Expand();

                    treeView.EndUpdate();
                }
            }
        }

        private static TreeNode AddProjectRoot(TreeNode node, ProjectDefinition project)
        {
            var projectNode = new TreeNode { Text = project.Name, Tag = project };
            node.Nodes.Add(projectNode);
            return projectNode;
        }

        private static void AddProject(TreeNode node, ProjectDefinition project)
        {
            var projectNode = new SolutionNode(project.Solution) { Text = project.Name };
            node.Nodes.Add(projectNode);
        }

        private static void AddTrunk(TreeNode projectNode, Solution trunk)
        {
            if (trunk != null)
                projectNode.Nodes.Add(new SolutionNode(trunk) { Text = "trunk" });
        }

        private static void AddBranchTag(TreeNode projectNode, ICollection<BranchTagBase> items, string rootName)
        {
            if (items.Count > 0)
            {
                var branchesNode = new TreeNode { Text = rootName };
                projectNode.Nodes.Add(branchesNode);
                foreach (var item in items)
                    branchesNode.Nodes.Add(new SolutionNode(item.Solution) { Text = item.Name });
            }
        }

        public ProjectTree()
        {
            InitializeComponent();
            imageList.Images.Add("Play", Resources.Play);
            imageList.Images.Add("Record", Resources.Record);
        }

        private void treeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView.SelectedNode = treeView.GetNodeAt(e.X, e.Y);
            }
        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var node = e.Node as SolutionNode;
                if (node != null)
                    node.Solution.Open();
            }
        }
    }
}
