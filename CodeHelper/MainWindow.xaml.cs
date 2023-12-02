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
        }
    }
}
