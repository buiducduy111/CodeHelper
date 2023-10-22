using Newtonsoft.Json;
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
    /// Interaction logic for wFindJson.xaml
    /// </summary>
    public partial class wFindJson : Window
    {
        public wFindJson()
        {
            InitializeComponent();
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            string input = new TextRange(rtbInput.Document.ContentStart, rtbInput.Document.ContentEnd).Text;
            string output = "";
            if (input.Length == 0)
                return;
            input = input.Replace("\r", "").Replace("\n", "");

            char firstChar = input[0];
            char lastChar = '}';
            if (firstChar == '{') lastChar = '}';
            else if (firstChar == '[') lastChar = ']';

            int index = 1;
            string json = "";

            while (true)
            {
                index = input.IndexOf(lastChar, index);
                if (index == -1)
                    break;

                json = input.Substring(0, index + 1);

                try
                {
                    dynamic test = JsonConvert.DeserializeObject<dynamic>(json);
                    output = json;
                    rtbOutput.Document.Blocks.Clear();
                    rtbOutput.Document.Blocks.Add(new Paragraph(new Run(output)));
                    MessageBox.Show("Đã tìm thấy Json hợp lệ");
                    return;
                }
                catch 
                {
                    index++;
                }
            }

            MessageBox.Show("Không tìm thấy Json hợp lệ");
        }
    }
}
