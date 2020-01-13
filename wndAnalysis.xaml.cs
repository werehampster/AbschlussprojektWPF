using System;
using System.Collections;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using DataGrid = System.Windows.Controls.DataGrid;
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
            string[] logFile = null;
            string[] prog = null;
            string[] etj = null;
            int i = 0;

            DataGridTextColumn shortcut = new DataGridTextColumn();
            shortcut.Header = "Tastenkürzel";
            shortcut.Binding = new System.Windows.Data.Binding("shortcut");
            shortcut.Width = 210;
            dgAnalysis.Columns.Add(shortcut);
            DataGridTextColumn program = new DataGridTextColumn();
            program.Header = "Aktives Programm";
            program.Width = 310;
            program.Binding = new System.Windows.Data.Binding("program");
            dgAnalysis.Columns.Add(program);
            DataGridTextColumn easyTaskJob = new DataGridTextColumn();
            easyTaskJob.Header = "Möglicher EasyTask Job";
            easyTaskJob.Width = 310;
            easyTaskJob.Binding = new System.Windows.Data.Binding("easyTaskJob");
            dgAnalysis.Columns.Add(easyTaskJob);

            try
            {
                logFile = File.ReadAllLines(@"D:\KeysOnly.txt");
                prog = File.ReadAllLines(@"D:\ActiveProgram.txt");
                etj = File.ReadAllLines(@"D:\ETJob.txt");
                foreach (string sc in logFile)
                {

                    if (sc != "")
                    {
                        try
                        {
                            dgAnalysis.Items.Add(new Line() { shortcut = sc, program = prog[i], easyTaskJob = etj[i] });
                            dgAnalysis.FontSize = 20;
                            i++;
                        }
                        catch
                        {
                            dgAnalysis.Items.Add(new Line() { shortcut = sc, program = "" });
                            dgAnalysis.FontSize = 20;
                            i++;
                        }
                    }
                    else
                        continue;
                }
                
            }
            catch
            {
                MessageBox.Show("Die Logdateien sind leer", "Log - Fehler leere Logdateien", MessageBoxButton.OK, MessageBoxImage.Error );
            }
            


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
                File.Delete(@"D:\ActiveProgram.txt");
                File.Delete(@"D:\ETJob.txt");
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
