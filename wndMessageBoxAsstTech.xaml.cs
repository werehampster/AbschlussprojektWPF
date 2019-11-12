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
using System.Diagnostics;
using Keystroke.API;
using System.IO;

namespace AbschlussprojektWPF
{
    /// <summary>
    /// Interaction logic for wndMessageBox.xaml
    /// </summary>
    public partial class wndMessageBoxAsstTech : Window
    {
        public wndMessageBoxAsstTech()
        {
            InitializeComponent();
        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if(cbJaws.IsChecked == true)
            {
                Process.Start(@"C:\Program Files\Freedom Scientific\JAWS\2019\jfw.exe");
            }

            if(cbZoomText.IsChecked == true)
            {
                Process.Start(@"C:\Program Files (x86)\Freedom Scientific\ZoomText\2019\Zt.exe");
            }

            if(cbNVDA.IsChecked == true)
            {
                Process.Start(@"C:\Program Files\NVDA\mynvda.exe");
            }

            
            App.IsLoggingStarted = true;

            wndMessageBoxStartLog wndMessageBoxStartLog = new wndMessageBoxStartLog();
            wndMessageBoxStartLog.Show();
        }
    }
}
