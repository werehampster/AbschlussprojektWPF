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
        bool firefox = false;
        bool chrome = false;
        bool ie = false;
        bool word = false;
        bool excel = false;
        bool outlook = false;
        bool jaws = false;
        bool nvda = false;
        bool zoomtext = false;
        bool supernova = false;

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
            wndAnalysis frmAnalysis = new wndAnalysis();
            frmAnalysis.Show();
        }

        private void BtnLog_Click(object sender, RoutedEventArgs e)
        {
            // wenn dieser Button gedrückt wird, soll eine Überprüfung stattfinden, ob der Logvorgang überhaupt gestartet werden soll, wenn nicht
            // soll die Möglichkeit bestehen, JAWS/ZoomText zu starten... zur Überprüfung soll zunächst nur auf JAWS und ZoomText geprüft werden, 
            // Browser /Word können nachher noch gestartet werden 


            //////////TEST ///////////
            Logger logFile = new Logger();
            logFile.WriteInitialLog();

        }

        private void checkCheckBox()
        {

            Process[] processlist = Process.GetProcesses();

            foreach (Process theprocess in processlist)
            {

                // mit switch!

                if (theprocess.ProcessName == "jfw")
                {
                    Logger logFile = new Logger();
                    logFile.WriteInitialLog();
                }


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

        private void CbSuperNova_CheckedChanged(object sender, EventArgs e)
        {
            supernova = true;
        }
        #endregion
    }
}
