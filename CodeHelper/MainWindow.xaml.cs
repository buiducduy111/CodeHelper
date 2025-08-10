using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CodeHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            cboTool.Items.Add("-- Chọn tool");
            cboTool.Items.Add("Tạo code mẫu Export từ Excel");
            cboTool.Items.Add("Tìm kiếm Json hợp lệ");
            cboTool.Items.Add("Xóa bin c# project");
            cboTool.Items.Add("Code search");
            cboTool.Items.Add("Đảo code = trên dòng");
            cboTool.Items.Add("Patch undetected chromedrivers");
            cboTool.Items.Add("Release tool");
            cboTool.Items.Add("So sánh file khác giữa 2 folder");
            cboTool.Items.Add("Convert community.mvvm -> reactiveUI");

            cboTool.SelectedIndex = 0;
        }

        private void cboTool_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboTool.SelectedIndex == 1)
                new wCreateCodeForExportExcel().Show();

            if (cboTool.SelectedIndex == 2)
                new wFindJson().Show();

            if (cboTool.SelectedIndex == 3)
                new wRemoveBin().Show();

            if (cboTool.SelectedIndex == 4)
                new wCodeSearch().Show();

            if (cboTool.SelectedIndex == 5)
                new wReverseCode().Show();

            if (cboTool.SelectedIndex == 6)
                Process.Start(AppDomain.CurrentDomain.BaseDirectory + "py\\undetectedchromedriver_patch");

            if (cboTool.SelectedIndex == 7)
            {
                Ookii.Dialogs.Wpf.VistaFolderBrowserDialog dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
                if (dialog.ShowDialog() == true)
                    releaseTool(dialog.SelectedPath);
            }

            if (cboTool.SelectedIndex == 8)
            {
                wFindDiffFiles w = new wFindDiffFiles();
                w.Show();
            }

            if (cboTool.SelectedIndex == 9)
            {
                wConvertComunityMVVMToReactiveUIFody w = new wConvertComunityMVVMToReactiveUIFody();
                w.Show();
            }
        }

        private void releaseTool(string folder)
        {
            string[] files = System.IO.Directory.GetFiles(folder);
            foreach (string file in files)
            {
                string ext = System.IO.Path.GetExtension(file)?.ToLower();
                if (ext == ".pdb" || ext == ".xml")
                    System.IO.File.Delete(file);
            }
            MessageBox.Show("DONE");
        }
    }
}
