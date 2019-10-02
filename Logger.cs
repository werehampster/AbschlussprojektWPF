﻿using System;
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

        public bool isJaws = false;
        public bool isZoomText = false;
        public bool isNVDA = false;

        public Logger()
        {
            // wenn das Programm gestartet wird, werden zum Einen die laufenden Prozesse geloggt, 
            // zum Anderen wird geloggt, welcher Browser verwendet wird.

            //runningProcess = logProcess();
            //logProcess();
            //logBrowser();

        }


        //public void logProcess()
        //{

        //}

        //laufen alle relevanten Prozesse?
        //public bool isRelevant()
        //{
        //    Process[] processlist = Process.GetProcesses();
        //    bool isRelevant = false;


        //    foreach (Process theprocess in processlist)
        //    {

        //        if (theprocess.ProcessName == "firefox" || theprocess.ProcessName == "chrome" || theprocess.ProcessName =="ie"|| theprocess.ProcessName == "edge")
        //        {
        //            this.browserName = theprocess.ProcessName;

        //        }


        //    }

        //    return false;
        //}


        //public void logBrowser()
        //{
        //    Browser browserDetails = new Browser();

        //   //File.WriteAllText(@"D:\test.txt", Browser.GetBrowserDetails());
        //}


        // Methode, die den Titel des aktiven (vordergrun) Fensters zurückliefert
        private static string GetTitleOfActiveWindow()
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


                
            //schreibt noch alle Prozesse in die Datei, wenn firefox schon drin steht, soll es nicht nochmal geschrieben werden
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
            return AssistTech;
        }
    }


}
