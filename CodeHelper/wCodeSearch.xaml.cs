using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CodeHelper
{
    /// <summary>
    /// Interaction logic for wCodeSearch.xaml
    /// </summary>
    public partial class wCodeSearch : Window
    {
        public wCodeSearch()
        {
            InitializeComponent();
        }

        private void btnScan_Click(object sender, RoutedEventArgs e)
        {
            string folder = txtInput.Text;
            rtbLog.Document.Blocks.Clear();

            Thread t = new Thread(() =>
            {
                scanFolder(folder);
                MessageBox.Show("Scan thành công!");
            });
            t.IsBackground = true;
            t.Start();
        }

        private void scanFolder(string folder)
        {
            lblMsg.Dispatcher.Invoke(() =>
            {
                lblMsg.Content = "Scan: " + folder;
            });


            string ext = "", searchKey = "";
            this.Dispatcher.Invoke(() =>
            {
                ext = txtFileExtension.Text.Trim();
                searchKey = txtKeyword.Text;
            });

            string[] files = Directory.GetFiles(folder);
            foreach (string file in files)
            {
                string fileExt = System.IO.Path.GetExtension(file);
                if (fileExt != null && fileExt.ToLower() == ext)
                {
                    string content = File.ReadAllText(file);
                    if (content.Contains(searchKey))
                    {
                        rtbLog.Dispatcher.Invoke(() =>
                        {
                            rtbLog.Document.Blocks.Add(new Paragraph(new Run(file)));
                        });
                    }
                }
            }

            string[] childFolders = Directory.GetDirectories(folder);
            foreach (string cfolder in childFolders)
            {
                scanFolder(cfolder);
            }
        }

    }
}
