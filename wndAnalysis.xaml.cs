using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
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
            DataGridView dataGridView = new DataGridView();
            System.Windows.Controls.DataGrid dataGrid = new System.Windows.Controls.DataGrid();
            string[] logFile = System.IO.File.ReadAllLines(@"D:\KeysOnlyNoLogger.txt");

            DataTable dataTable = new DataTable();
            
            dataGridView.DataSource = dataTable;
            dgAnalysis.DataContext = dataTable;
            dataGrid.ItemsSource = logFile;

            dataTable.Columns.Add();
            dataTable.Columns.Add();
            dataTable.Columns.Add();
            int k = 0;
            while(k < logFile.Length -1 )
            {
                dataTable.Rows.Add();
                k++;
            }
            for (int i = 0; i < logFile.Length - 1; i++)
            {

                dataGrid.ItemsSource = logFile[i];
                
                //dataGridView.Rows[i].SetValues(logFile[i]);   
                //dataGridView.Rows[i] = logFile[i];
            }
  
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnEasyTask_Click(object sender, RoutedEventArgs e)
        {
            // nur starten wenn ET noch nicht läuft
            Process.Start(@"C:\Program Files\EasyTask\EasyTask.exe");
        }
    }
}
