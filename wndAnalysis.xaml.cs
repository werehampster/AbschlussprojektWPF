using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace AbschlussprojektWPF
{
    /// <summary>
    /// Interaction logic for wndAnalysis.xaml
    /// </summary>
    public partial class wndAnalysis : Window
    {
        
        
        public wndAnalysis()
        {
            InitializeComponent();


            fillDataGrid();

        }


        private void fillDataGrid()
        {

            List<string> Content = new List<string>();

   
            string[] logFile = File.ReadAllLines(@"D:\KeysOnly.txt");
            foreach (string line in logFile)
            {
                if (line != "")
                {
                    Content.Add(line);
                }
                else
                    continue;
            }
       


            //for(int i = 0; i< logFile.Length; i++)
            //{
            //    if (logFile[i] != "")
            //    {
            //        Content.Add(logFile[i]);
            //    }
            //    else
            //        continue;
            //}
            dgAnalysis.ItemsSource = Content;

            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Möchten Sie die Log Dateien Löschen?", "Log - Log Dateien Löschen", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                File.Delete(@"D:\KeysOnly.txt");
                File.Delete(@"D:\log.txt");
            }
            Environment.Exit(0);
        }

        private void btnEasyTask_Click(object sender, RoutedEventArgs e)
        {
            // nur starten wenn ET noch nicht läuft
            Process.Start(@"C:\Program Files\EasyTask\EasyTask.exe");
        }
    }
}
