using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
    /// Interaction logic for wFindDiffFiles.xaml
    /// </summary>
    public partial class wFindDiffFiles : Window
    {
        public wFindDiffFiles()
        {
            InitializeComponent();
        }

        private void btnScan_Click(object sender, RoutedEventArgs e)
        {
            lsbResult1.Items.Clear();
            lsbResult2.Items.Clear();
            Dictionary<string, string> d1 = GetFilesWithMD5(txtInput1.Text);
            Dictionary<string, string> d2 = GetFilesWithMD5(txtInput2.Text);

            foreach (var item in d1)
            {
                ListBoxItem listBoxItem = new ListBoxItem() { Content = item.Key + " - " + item.Value };

                if (d2.ContainsKey(item.Key))
                {
                    if (d2[item.Key] == item.Value)
                        listBoxItem.Background = Brushes.Green;
                    else
                        listBoxItem.Background = Brushes.Red;
                }
                else
                    listBoxItem.Background = Brushes.Yellow;

                lsbResult1.Items.Add(listBoxItem);
            }

            foreach (var item in d2)
            {
                ListBoxItem listBoxItem = new ListBoxItem() { Content = item.Key + " - " + item.Value };

                if (d1.ContainsKey(item.Key))
                {
                    if (d1[item.Key] == item.Value)
                        listBoxItem.Background = Brushes.Green;
                    else
                        listBoxItem.Background = Brushes.Red;
                }
                else
                    listBoxItem.Background = Brushes.Yellow;

                lsbResult2.Items.Add(listBoxItem);
            }
        }

        static Dictionary<string, string> GetFilesWithMD5(string dir)
        {
            var filesWithMD5 = new Dictionary<string, string>();
            var files = Directory.GetFiles(dir);

            foreach (var file in files)
            {
                string ext = System.IO.Path.GetExtension(file).ToLower();
                if (ext == ".xml" || ext == ".pdb")
                    continue;

                string key = System.IO.Path.GetFileName(file);
                string md5 = GetMD5Hash(file);
                filesWithMD5.Add(key, md5);
            }
            return filesWithMD5;
        }

        static string GetMD5Hash(string filePath)
        {
            using (var md5 = MD5.Create())
            using (var stream = File.OpenRead(filePath))
            {
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

    }
}
