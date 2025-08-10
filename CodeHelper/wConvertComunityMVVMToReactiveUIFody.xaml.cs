using System;
using System.Collections.Generic;
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

namespace CodeHelper
{
    /// <summary>
    /// Interaction logic for wConvertComunityMVVMToReactiveUIFody.xaml
    /// </summary>
    public partial class wConvertComunityMVVMToReactiveUIFody : Window
    {
        public wConvertComunityMVVMToReactiveUIFody()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string code = (new TextRange(rtbInCode.Document.ContentStart, rtbInCode.Document.ContentEnd)).Text;

            // Incode: 
            // [ObservableProperty]
            // private ProxyType _type = ProxyType.None;
            // --> [Reactive] public ProxyType Type { get; set; } = ProxyType.None;

            code = code.Replace("[ObservableProperty]", "[Reactive]");
            code = code.Replace("private ", "public ");
            code = code.Replace("readonly", "");

            string newCode = "";

            string[] lines = code.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            string currentLine = "";

            foreach (string line in lines)
            {
                // Bỏ qua dòng trắng (vẫn cộng vào kết quả) hoặc comment
                if (string.IsNullOrWhiteSpace(line))
                {
                    newCode += Environment.NewLine;
                    continue;
                }

                if (line.Trim().StartsWith("//"))
                {
                    newCode += line + Environment.NewLine;
                    continue;
                }

                // Đọc từng dòng, nếu dòng là [Reactive] (sau khi replace), thì cộng chung 1 dòng
                if (line.Trim() == "[Reactive]")
                {
                    currentLine = line + " "; // Cộng sẵn 1 khoảng trắng
                    continue;
                }

                // Xử lý 2 trường hợp
                // public string _host = string.Empty;
                // public string _host;
                string l = currentLine + line;
                if (l.Contains("_"))
                {
                    string afterText = l.Substring(l.IndexOf("_") + 1, 1).Trim(); // h
                    l = l.Replace("_" + afterText, afterText.ToUpper()); // _h => H
                }
                if (l.Contains("="))
                {
                    l = l.Replace("=", " { get; set; } =");
                }
                else
                {
                    l = l.Replace(";", " { get; set; }");
                }

                newCode += l + Environment.NewLine;
                currentLine = "";
            }

            rtbOutCode.Document.Blocks.Clear();
            rtbOutCode.Document.Blocks.Add(new Paragraph(new Run(newCode)));
        }
    }
}
