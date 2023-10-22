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
    /// Interaction logic for wReverseCode.xaml
    /// </summary>
    public partial class wReverseCode : Window
    {
        public wReverseCode()
        {
            InitializeComponent();
        }

        private void btnReverseCode_Click(object sender, RoutedEventArgs e)
        {
            string input = new TextRange(rtbInput.Document.ContentStart, rtbInput.Document.ContentEnd).Text;
            string output = "";
            if (input.Length == 0)
                return;

            string[] lines = input.Replace("\r", "").Split('\n');

            foreach (string line in lines)
            {
                string rawLine = line.Trim();
                if (string.IsNullOrEmpty(rawLine))
                    continue;

                if (rawLine[rawLine.Length - 1] == ';')
                    rawLine = rawLine.Substring(0, rawLine.Length - 1);

                string[] split = rawLine.Split('=');
                if (split.Length == 2)
                {
                    output += split[1] + " = " + split[0] + ";\r\n";
                }
            }

            rtbOutput.Document.Blocks.Clear();
            rtbOutput.Document.Blocks.Add(new Paragraph(new Run(output)));
        }
    }
}
