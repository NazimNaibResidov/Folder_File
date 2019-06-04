using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace File_system
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            foreach (var item in DriveInfo.GetDrives())
            {
                TreeNode node = new TreeNode();
                node.Text = item.Name;
              
                DirectoryInfo info = new DirectoryInfo(item.Name);
                foreach (var directory in info.GetDirectories())
                {
                    TreeNode nodes = new TreeNode();
                    nodes.Text = directory.Name;
                    nodes.Tag = directory;
                    node.Nodes.Add(nodes);
                }
                treeView1.Nodes.Add(node);
            }
        }
       void SubDirectory(TreeNode Mainnode)
        {
            foreach (TreeNode item in Mainnode.Nodes)
            {
              
              
                try
                {
                    DirectoryInfo inf = (DirectoryInfo)item.Tag;
                    foreach (var ite in inf.GetDirectories())
                    {
                        TreeNode mynode = new TreeNode();
                        mynode.Text = ite.Name;
                        mynode.Tag = ite;
                        item.Nodes.Add(mynode);
                    }
                }
                catch (Exception)
                {

                   // throw;
                }
            }
          
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            SubDirectory(e.Node);
        }
    }
}
