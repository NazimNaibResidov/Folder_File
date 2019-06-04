using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_system.Core
{
    public delegate void SearchHandler(DirectoryInfo a, string b, ListBox c);
    public class Tools
    {
        private static List<FileInfo> Finder(DirectoryInfo info, string Search)
        {
            List<FileInfo> files = new List<FileInfo>();
            try
            {

                foreach (FileInfo item in info.GetFiles())
                {
                    if (item.Name.Contains(Search))
                    {
                        files.Add(item);
                    }
                }
            }
            catch
            {

            }
            try
            {
                foreach (DirectoryInfo item in info.GetDirectories())
                {
                    files.AddRange(Finder(item, Search));
                }
            }
               
           catch 
            {


            }
            return files;

        }
        public static void SubDirectory(TreeNode Mainnode)
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
        public static void AfterSelect(DirectoryInfo inf, ListBox box)
        {
            box.Items.Clear();
            try
            {
                foreach (var item in inf.GetFiles())
                {
                    box.Items.Add(item.Name);
                }
            }
            catch
            {


            }
        }
        public static void InformationFile(FileInfo info)
        {
            try
            {

                MessageBox.Show($"Name {info.Name}/n Size {info.Length / 1024} KB/Last Access {info.LastWriteTime}");
            }
            catch
            {

                MessageBox.Show($"Name ");
            }

        }
        public static void ShowDirve(TreeView view)
        {
            foreach (var item in DriveInfo.GetDrives())
            {
                TreeNode node = new TreeNode();
                node.Text = item.Name;
                view.Tag = item.Name;
                DirectoryInfo info = new DirectoryInfo(item.Name);
                node.Tag = info;
                foreach (var directory in info.GetDirectories())
                {
                    TreeNode nodes = new TreeNode();
                    nodes.Text = directory.Name;
                    nodes.Tag = directory;
                    node.Nodes.Add(nodes);
                }
                view.Nodes.Add(node);
            }
        }
        public static void Search(DirectoryInfo info, string Search, ListBox box)
        {
            box.Items.Clear();
            box.Items.AddRange(Finder(info, Search).ToArray());
        }
        public static void ToSpy(IAsyncResult result)
        {
            SearchHandler handler = (SearchHandler)result.AsyncState;
            handler.EndInvoke(result);
        }
    }
}
