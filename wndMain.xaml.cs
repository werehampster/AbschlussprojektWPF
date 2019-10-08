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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;

namespace AbschlussprojektWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        public string processName;

        // alle booleans initalisieren und auf false setzen
        bool firefox;
        bool chrome;
        bool ie;
        bool word;
        bool excel;
        bool outlook;
        bool jaws;
        bool nvda;
        bool zoomtext;
        //bool supernova = false;

        public wndMain()
        {
            InitializeComponent();
        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAnalysis_Click(object sender, RoutedEventArgs e)
        {
            // initialisiere Auswertungsfenster
            wndAnalysis wndAnalysis = new wndAnalysis();
            wndAnalysis.Show();
        }

        private void BtnLog_Click(object sender, RoutedEventArgs e)
        {
            firefox = false;
            chrome = false;
            ie = false;
            word = false;
            excel = false;
            outlook = false;
            jaws = false;
            nvda = false;
            zoomtext = false;

            // wenn dieser Button gedrückt wird, soll eine Überprüfung stattfinden, ob der Logvorgang überhaupt gestartet werden soll, wenn nicht
            // soll die Möglichkeit bestehen, JAWS/ZoomText zu starten... zur Überprüfung soll zunächst nur auf JAWS und ZoomText geprüft werden, 
            // Browser /Word können nachher noch gestartet werden 
            checkCheckBox();
            if (jaws == false && nvda == false && zoomtext == false)
            {
                wndMessageBox wndMessageBox = new wndMessageBox();
                wndMessageBox.Show();
            }
            else
            {
                Logger logFile = new Logger();
            }

        }

        private void checkCheckBox()
        {
            if(cbFirefox.IsChecked == true)
            {
                firefox = true;
            }

            if(cbJaws.IsChecked == true)
            {
                jaws = true;
            }

        }


        #region Überprüfung der Checkboxen
        private void CbFirefox_CheckedChanged(object sender, EventArgs e)
        {
            firefox = true;

        }

        private void CbChrome_CheckedChanged(object sender, EventArgs e)
        {
            chrome = true;
        }

        private void CbIE_CheckedChanged(object sender, EventArgs e)
        {
            ie = true;
        }

        private void CbWord_CheckedChanged(object sender, EventArgs e)
        {
            word = true;
        }

        private void CbOutlook_CheckedChanged(object sender, EventArgs e)
        {
            outlook = true;
        }

        private void CbExcel_CheckedChanged(object sender, EventArgs e)
        {
            excel = true;
        }

        private void CbJaws_CheckedChanged(object sender, EventArgs e)
        {
            jaws = true;
        }

        private void CbNVDA_CheckedChanged(object sender, EventArgs e)
        {
            nvda = true;
        }

        private void CbZoomText_CheckedChanged(object sender, EventArgs e)
        {
            zoomtext = true;
        }

        //private void CbSuperNova_CheckedChanged(object sender, EventArgs e)
        //{
        //    supernova = true;
        //}


        #endregion
    }
}
