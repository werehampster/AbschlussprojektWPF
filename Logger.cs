using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AbschlussprojektWPF
{
    class Logger
    {
        // brauche ich, damit ich abfragen kann, welches Fenster sich im Vordergrung befindet
        #region declaration of Windows API functions
        [DllImport("user32.dll")]

        //returns some sort of int
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        // returns name of active window
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        // returns length of text of active window
        static extern int GetWindowTextLength(IntPtr hWnd);
        #endregion



        string word = string.Empty;
        string excel = string.Empty;
        string outlook = string.Empty;


    public Logger()
        {
            // wenn das Programm gestartet wird, werden zum Einen die laufenden Assistenzprogramme geloggt, 
            // zum Anderen wird geloggt, welcher Browser verwendet wird und welche Office Programme laufen

            WriteInitialLog();

        }

        // Methode, die den Titel des aktiven (vordergrund) Fensters zurückliefert
        public string GetTitleOfActiveWindow()
        {
            string WindowTitle = "";

            IntPtr handle = GetForegroundWindow();

            // Obtain the length of the text   
            int TitleLength = GetWindowTextLength(handle) + 1;

            StringBuilder stringBuilder = new StringBuilder(TitleLength);

            if (GetWindowText(handle, stringBuilder, TitleLength) > 0)
            {
                WindowTitle = stringBuilder.ToString();
            }

            return WindowTitle;
        }
        public void WriteInitialLog()
        {

            // Überschrift für die Log Datei
            File.WriteAllText(@"D:\log.txt", "Active Processes: \r\n\r\n");
            // Aktives Fenster MUSS NOCH WOANDERS HIN!
            File.AppendAllText(@"D:\log.txt", "Aktives Fenster: " + GetTitleOfActiveWindow() + "\r\n");
            // Browser
            File.AppendAllText(@"D:\log.txt", "Browser: " + GetBrowser() + "\r\n");
            // Assistive Technology
            File.AppendAllText(@"D:\log.txt", "Assistive Tech: " + GetAssistTech() + "\r\n");
            // Office Programme
            GetOffice();
            File.AppendAllText(@"D:\log.txt", "Office Programme: " + word +" " + outlook + " " + excel + "\r\n");
        }


        public string GetBrowser()
        {
            string BrowserName = string.Empty;
            bool firefox = false;
            bool chrome = false;
            bool intex = false;

            // holt alle Prozesse, die laufen
            Process[] processlist = Process.GetProcesses();
            // läuft durch die Prozesse
            foreach (Process theprocess in processlist)
            {
                //schreibt nur Browser in die Logdatei
                if ((theprocess.ProcessName == "firefox" && firefox == false) || (theprocess.ProcessName == "chrome" && chrome == false) || (theprocess.ProcessName == "iexplore" && intex == false))
                {
                    BrowserName = theprocess.ProcessName;
                    
                    switch (BrowserName)
                    {
                        case "firefox":
                            firefox = true;
                            break;
                        case "chrome":
                            chrome = true;
                            break;
                        case "iexplore":
                            intex = true;
                            break;
                        default:
                            // hier muss ich mir noch was ausdenken
                            break;
                    }
                }
            }

            return BrowserName;

        }

        public string GetAssistTech()
        {
            string AssistTech = string.Empty;
            bool jaws = false;
            bool nvda = false;
            bool zoomtext = false;


            // holt alle Prozesse, die laufen
            Process[] processlist = Process.GetProcesses();
            // läuft durch die Prozesse
            foreach (Process theprocess in processlist)
            {
                //schreibt nur Browser in die Logdatei
                if ((theprocess.ProcessName == "jfw" && jaws == false) || (theprocess.ProcessName == "nvda" && nvda == false) || (theprocess.ProcessName == "Zt" && zoomtext == false))
                {
                    AssistTech = theprocess.ProcessName;

                    switch (AssistTech)
                    {
                        case "jfw":
                            jaws = true;
                            break;
                        case "nvda":
                            nvda = true;
                            break;
                        case "Zt":
                            zoomtext = true;
                            break;
                        default:
                            // hier muss ich mir noch was ausdenken
                            break;
                    }
                }
            }
            return AssistTech;
        }

        public void GetOffice()
        {

            // holt alle Prozesse, die laufen
            Process[] processlist = Process.GetProcesses();
            // läuft durch die Prozesse
            foreach (Process theprocess in processlist)
            {
                
                if (theprocess.ProcessName == "WINWORD")
                {
                     word = "word";
                }
                if (theprocess.ProcessName == "EXCEL")
                {
                    excel = "excel";
                }
                if (theprocess.ProcessName == "OUTLOOK") 
                {
                    outlook = "outlook";
                }
  
            }
            
        }
    }


}
