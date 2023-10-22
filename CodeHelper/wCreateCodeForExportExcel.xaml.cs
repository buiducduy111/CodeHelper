using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using Aspose.Cells;

namespace CodeHelper
{
    /// <summary>
    /// Interaction logic for wCreateCodeForExportExcel.xaml
    /// </summary>
    public partial class wCreateCodeForExportExcel : Window
    {
        System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();

        public wCreateCodeForExportExcel()
        {
            InitializeComponent();
        }

        private void btnChooseCsv_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                generateData(openFileDialog.FileName);
            }
        }

        private void generateData(string file)
        {
            try
            {
                List<string> colTitles = new List<string>();
                List<string> colNames = new List<string>();

                Workbook wb = new Workbook(file);
                Worksheet ws = wb.Worksheets[0];

                int colId = 0;
                while (ws.Cells[0, colId].Value != null)
                {
                    string title = Convert.ToString(ws.Cells[0, colId].Value);
                    string name = Convert.ToString(ws.Cells[0, colId].Name).Replace("1", ""); // A1 => A

                    colTitles.Add(title);
                    colNames.Add(name);
                    colId++;
                }

                string structCode = File.ReadAllText("structs\\code_excel.code");

                // Fill code
                string codeTitle = "";
                for (int i = 0; i < colTitles.Count; i++)
                {
                    codeTitle += "\t\t" + $"ws.Cells[\"{colNames[i]}\" + row].Value = \"{colTitles[i]}\";\r\n";
                }

                string codeItems = "";
                for (int i = 0; i < colTitles.Count; i++)
                {
                    codeItems += "\t\t\t" + $"ws.Cells[\"{colNames[i]}\" + row].Value =  ini.Read(\"fixed\", \"{colTitles[i]}\");\r\n";
                }

                structCode = structCode.Replace("@code_title", codeTitle).Replace("@code_items", codeItems);

                rtbLog.Document.Blocks.Clear();
                rtbLog.Document.Blocks.Add(new Paragraph(new Run(structCode)));
                MessageBox.Show("Tạo code thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tạo code thất bại: "+ex.Message);
            }
        }
    }
}
