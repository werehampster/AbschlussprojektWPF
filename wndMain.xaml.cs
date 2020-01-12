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
using Keystroke.API;

namespace AbschlussprojektWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// All references to SuperNova have been commented out because only a 30 day trial version was available
    /// </summary>
    public partial class wndMain : Window
    {
        Logger logFile = new Logger();

        bool LoggerRunning = false;

        // bools zum Überprüfen der Checkboxen
        bool firefox = false;
        bool chrome = false;
        bool ie = false;
        bool word = false;
        bool excel = false;
        bool outlook = false;
        public static bool jaws = false;
        public static bool nvda = false;
        public static bool zoomtext = false;
        //bool supernova = false;

        // bools zum Überprüfen der Prozesse
        bool procFirefox;
        bool procChrome;
        bool procIE;
        bool procWord;
        bool procExcel;
        bool procOutlook;
        bool procJaws;
        bool procNvda;
        bool procZoomtext;
        // bool procSupernova;
        public wndMain()
        {
            InitializeComponent();

        }

        void wndMain_Closing(object sender, ConsoleCancelEventArgs e)
        {
            var result = MessageBox.Show("Möchten Sie die Log Dateien löschen?", "Log - Log Dateien Löschen", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                File.Delete(@"D:\KeysOnly.txt");
                File.Delete(@"D:\log.txt");
                File.Delete(@"D:\ActiveProgram.txt");
                File.Delete(@"D:\ETJob.txt");
            }   
        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
            var result = MessageBox.Show("Möchten Sie die Log Dateien löschen?", "Log - Log Dateien Löschen", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                File.Delete(@"D:\KeysOnly.txt");
                File.Delete(@"D:\log.txt");
                File.Delete(@"D:\ActiveProgram.txt");
                File.Delete(@"D:\ETJob.txt");
            }

        }

        private void BtnAnalysis_Click(object sender, RoutedEventArgs e)
        {
            // initialisiere Auswertungsfenster
            if (File.Exists(@"D:\KeysOnly.txt"))
            {
                wndAnalysis wndAnalysis = new wndAnalysis();
                wndAnalysis.Show();
            }
            else
            {
                if (App.IsLoggingStarted == false)
                {
                    MessageBox.Show("Starten Sie bitte erst den Logger", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                {
                    File.Create(@"D:\KeysOnly.txt");
                    File.Create(@"D:\log.txt");
                    File.Create(@"D:\ActiveProgram.txt");
                    File.Create(@"D:\ETJob.txt");
                    wndAnalysis wndAnalysis = new wndAnalysis();
                    wndAnalysis.Show();
                }

            }
        }

        private void BtnLog_Click(object sender, RoutedEventArgs e)
        {

            procFirefox = false;
            procChrome = false;
            procIE = false;
            procWord = false;
            procExcel = false;
            procOutlook = false;
            procJaws = false;
            procNvda = false;
            procZoomtext = false;
            // procSupernova = false;

            // wenn dieser Button gedrückt wird, soll eine Überprüfung stattfinden, ob der Logvorgang überhaupt gestartet werden soll, wenn nicht
            // soll die Möglichkeit bestehen, JAWS/ZoomText zu starten... zur Überprüfung soll zunächst nur auf JAWS und ZoomText geprüft werden, 
            // Browser /Word können nachher noch gestartet werden 
            CheckCheckBox();
            CheckProcesses();
            isCheckedAndRunning();

            if (LoggerRunning == false)
            {

                // überprüft, ob Assistenzprogramme laufen, wenn nicht, wird ein Fenster geöffnet, in dem man ein Assistenzprogramm starten kann.
                if (jaws == false && nvda == false && zoomtext == false)
                {
                    App.IsLoggingStarted = false;
                    wndMessageBoxAsstTech wndMessageBox = new wndMessageBoxAsstTech();
                    wndMessageBox.Show();
                   
                }
                else
                {
                    wndMessageBoxStartLog wndMessageBoxStartedLog = new wndMessageBoxStartLog();
                    wndMessageBoxStartedLog.Show();
                    App.IsLoggingStarted = true;
                    
                }
                
                LoggerRunning = true;
            }
            else
            {
                wndMessageBoxStartLog wndMessageBoxStartedLog = new wndMessageBoxStartLog();
                wndMessageBoxStartedLog.Show();
                App.IsLoggingStarted = false;
                LoggerRunning = false;
            }
            
        }

        private void CheckCheckBox()
        {
            if (cbFirefox.IsChecked == true)
            {
                firefox = true;
                
            }

            if (cbChrome.IsChecked == true)
            {
                chrome = true;
                
            }

            if (cbIE.IsChecked == true)
            {
                ie = true;
                
            }

            if (cbWord.IsChecked == true)
            {
                word = true;
                
            }

            if (cbOutlook.IsChecked == true)
            {
                outlook = true;
                
            }

            if (cbExcel.IsChecked == true)
            {
                excel = true;
                
            }

            if (cbJaws.IsChecked == true)
            {
                jaws = true;
                
            }

            if (cbNVDA.IsChecked == true)
            {
                nvda = true;
                
            }

            if (cbZoomtext.IsChecked == true)
            {
                zoomtext = true;
                
            }

            //if (cbSupernova.IsChecked == true)
            //{
            //    supernova = true;
            //    clsGlobal.programsChecked = true;  
            //}
        }

        private void CheckProcesses()
        {
            string ProcessName = string.Empty;
            Process[] processlist = Process.GetProcesses();
            // läuft durch die Prozesse
            foreach (Process theprocess in processlist)
            {
              
                ProcessName = theprocess.ProcessName;

                if (ProcessName == "firefox")
                {
                    procFirefox = true;
                }
                if (ProcessName == "chrome")
                {
                    procChrome = true;
                }
                if (ProcessName == "iexplore")
                {
                    procIE = true;
                }
                if (ProcessName == "WINWORD")
                {
                    procWord = true;
                }
                if (ProcessName == "EXCEL")
                {
                    procExcel = true;
                }
                if (ProcessName == "OUTLOOK")
                {
                    procOutlook = true;
                }
                if (ProcessName == "nvda")
                {
                    procNvda = true;
                }
                if (ProcessName == "jfw")
                {
                    procJaws = true;
                }
                if (ProcessName == "Zt")
                {
                    procZoomtext = true;
                }
                //if (ProcessName == "supernova")
                //{
                //    procSupernova = true;
                //}
            }
        }

        private void isCheckedAndRunning()
        {
            if (firefox == true && procFirefox == false)
            {
                // Zeigt eine MessageBox wenn Firefox angehakt ist, aber nicht läuft. Ermöglicht es Firefox zu starten
                MessageBoxResult result = MessageBox.Show("Firefox läuft nicht, soll das Programm geöffnet werden?", "Logger - Firefox öffnen", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Process.Start(@"C:\Program Files\Firefox Developer Edition\firefox.exe");
                }
                else
                {
                    cbFirefox.IsChecked = false;
                }
            }

            if (chrome == true && procChrome == false)
            {
                // Zeigt eine MessageBox wenn Chrome angehakt ist, aber nicht läuft. Ermöglicht es Chrome zu starten
                MessageBoxResult result = MessageBox.Show("Chrome läuft nicht, soll das Programm geöffnet werden?", "Logger - Chrome öffnen", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe");
                }
                else
                {
                    cbChrome.IsChecked = false;
                }
            }

            if (ie == true && procIE == false)
            {
                // Zeigt eine MessageBox wenn Internet Explorer angehakt ist, aber nicht läuft. Ermöglicht es Internet Explorer zu starten
                MessageBoxResult result = MessageBox.Show("Internet Explorer läuft nicht, soll das Programm geöffnet werden?", "Logger - Internet Explorer öffnen", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Process.Start(@"C:\Program Files\Internet Explorer\iexplore.exe");
                }
                else
                {
                    cbIE.IsChecked = false;
                }
            }

            if (word == true && procWord == false)
            {
                // Zeigt eine MessageBox wenn Word angehakt ist, aber nicht läuft. Ermöglicht es Word zu starten
                MessageBoxResult result = MessageBox.Show("Word läuft nicht, soll das Programm geöffnet werden?", "Logger - Word öffnen", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Process.Start(@"C:\Program Files\Microsoft Office\root\Office16\WINWORD.EXE");
                }
                else
                {
                    cbWord.IsChecked = false;
                }
            }

            if (outlook == true && procOutlook == false)
            {
                // Zeigt eine MessageBox wenn Outlook angehakt ist, aber nicht läuft. Ermöglicht es Outlook zu starten
                MessageBoxResult result = MessageBox.Show("Outlook läuft nicht, soll das Programm geöffnet werden?", "Logger - Outlook öffnen", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Process.Start(@"C:\Program Files\Microsoft Office\root\Office16\OUTLOOK.EXE");
                }
                else
                {
                    cbOutlook.IsChecked = false;
                }
            }

            if (excel == true && procExcel == false)
            {
                // Zeigt eine MessageBox wenn Excel angehakt ist, aber nicht läuft. Ermöglicht es Excel zu starten
                MessageBoxResult result = MessageBox.Show("Excel läuft nicht, soll das Programm geöffnet werden?", "Logger - Excel öffnen", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Process.Start(@"C:\Program Files\Microsoft Office\root\Office16\EXCEL.EXE");
                }
                else
                {
                    cbExcel.IsChecked = false;
                }
            }

            if (jaws == true && procJaws == false)
            {
                // Zeigt eine MessageBox wenn JAWS angehakt ist, aber nicht läuft. Ermöglicht es JAWS zu starten
                MessageBoxResult result = MessageBox.Show("JAWS läuft nicht, soll das Programm geöffnet werden?", "Logger - JAWS öffnen", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Process.Start(@"C:\Program Files\Freedom Scientific\JAWS\2019\jfw.exe");
                }
                else
                {
                    cbJaws.IsChecked = false;
                }
            }

            if (nvda == true && procNvda == false)
            {
                // Zeigt eine MessageBox wenn NVDA angehakt ist, aber nicht läuft. Ermöglicht es NVDA zu starten
                MessageBoxResult result = MessageBox.Show("NVDA läuft nicht, soll das Programm geöffnet werden?", "Logger - NVDA öffnen", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Process.Start(@"C:\Program Files\NVDA\mynvda.exe");
                }
                else
                {
                    cbNVDA.IsChecked = false;
                }
            }

            if (zoomtext == true && procZoomtext == false)
            {
                // Zeigt eine MessageBox wenn Zoomtext angehakt ist, aber nicht läuft. Ermöglicht es Zoomtext zu starten
                MessageBoxResult result = MessageBox.Show("ZoomText läuft nicht, soll das Programm geöffnet werden?", "Logger - ZoomText öffnen", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Process.Start(@"C:\Program Files (x86)\Freedom Scientific\ZoomText\2019\Zt.exe");
                }
                else
                {
                    cbZoomtext.IsChecked = false;
                }
            }

            //if (supernova == true && procSupernova == false)
            //{
            //    // Zeigt eine MessageBox wenn SuperNova angehakt ist, aber nicht läuft. Ermöglicht es SuperNova zu starten
            //    MessageBoxResult result = MessageBox.Show("SuperNova läuft nicht, soll das Programm geöffnet werden?", "Logger - SuperNova öffnen", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //    if (result == MessageBoxResult.Yes)
            //    {
            //        Process.Start(@"");
            //    }
            //    else
            //    {
            //        cbSupernova.IsChecked = false;
            //    }
            //}
        }
    }
}