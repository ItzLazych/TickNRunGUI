using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;


namespace TickNRunGUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        public string StringTNR(RichTextBox TNR)
        {
            TextRange textRange = new TextRange(
                TNR.Document.ContentStart,
                TNR.Document.ContentEnd
            );
            return textRange.Text.Replace('.', ',');
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            string blitz = BlitzkriegGeneratorMethod();
            if (blitz != null)
            {
                Clipboard.SetText(blitz);
            }
        }
        public string BlitzkriegGeneratorMethod()
        {
            try
            {
                string data = StringTNR(TNR);

                List<double> startpos = data.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)
                                          .Select(num => double.Parse(num))
                                          .ToList();

                startpos.Add(100);
                startpos.Add(0);
                startpos = startpos.Distinct().ToList();
                startpos.Sort();
                startpos.Reverse();

                int numStartpos = startpos.Count - 2;
                string blitz = "";

                for (int stage = 1; stage <= numStartpos; stage++)
                {
                    blitz += $"Stage {stage}\n";
                    for (int i = 0; i < startpos.Count - stage; i++)
                    {
                        if (PlusP.IsChecked == true && startpos[i] != 100)
                            {
                            blitz += $"{startpos[i + stage]} - {startpos[i]}+\n";
                        }
                        else {
                            blitz += $"{startpos[i + stage]} - {startpos[i]}\n";
                        }
                    }
                }
                blitz += $"Stage {numStartpos + 1}\n0 - 100";
                if (comma.IsChecked == true)
                        { return blitz; }
                else if (dot.IsChecked == true)
                { return blitz.Replace(',', '.'); }
                return blitz;

            }
            catch
            {
                MessageBox.Show("Что-то вызвало ошибку в генераторе");
                return null;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
