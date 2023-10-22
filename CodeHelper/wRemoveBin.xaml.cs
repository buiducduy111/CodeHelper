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
    /// Interaction logic for wRemoveBin.xaml
    /// </summary>
    public partial class wRemoveBin : Window
    {
        List<string> _result = new List<string>();

        public wRemoveBin()
        {
            InitializeComponent();
        }

        private void btnScan_Click(object sender, RoutedEventArgs e)
        {
            btnDelete.IsEnabled = false;
            _result = new List<string>();
            rtbLog.Document.Blocks.Clear();

            string input = txtInput.Text;
            Thread t = new Thread(() =>
            {
                scanPrc(input);
            });
            t.IsBackground = true;
            t.Start();
        }

        private void scanPrc(string inputFodler)
        {
            scanFolder(inputFodler);

            MessageBox.Show("Quét xong! Bạn có thể xóa các folder này sau khi kiểm tra");
            btnDelete.Dispatcher.Invoke(() =>
            {
                btnDelete.IsEnabled = true;
            });
        }

        private void scanFolder(string folder)
        {
            lblMsg.Dispatcher.Invoke(() =>
            {
                lblMsg.Content = "Scan: " + folder;
            });
            string[] childFolders = Directory.GetDirectories(folder);

            foreach (string cfolder in childFolders)
            {
                string fname = cfolder.Substring(cfolder.LastIndexOf('\\') + 1);
                string fparent = System.IO.Path.GetDirectoryName(cfolder);

                List<string> files = Directory.GetFiles(fparent).ToList();

                if (fname == "bin" && files.Where(p => p.Contains(".csproj")).Count() > 0)
                {
                    _result.Add(cfolder);
                    rtbLog.Dispatcher.Invoke(() =>
                    {
                        rtbLog.Document.Blocks.Add(new Paragraph(new Run(cfolder)));
                    });
                }

                scanFolder(cfolder);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Xóa {_result.Count} folder?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (string f in _result)
                {
                    Directory.Delete(f, true);
                }
                MessageBox.Show("Xóa thành công");
            }
        }
    }
}
