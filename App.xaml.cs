
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Keystroke.API;

namespace AbschlussprojektWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        string Key = "";
        string CurrentKey = "";
        int Tab = 0;
        string UsedProgram= "";
        
        Logger activeWindow = new Logger();
        public Logger LogFile { get; set; }

        public static bool IsLoggingStarted { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (var api = new KeystrokeAPI())
            {
                KeystrokeAPI ki = new KeystrokeAPI();
                api.CreateKeyboardHook((character) =>
                {

                    if (IsLoggingStarted)
                    {

                            ////////////////////// Logik für Filter kommt hierhin //////////////////////////////
                            //Tab
                            //Enter
                            //Space
                            // I need focused element, because I don't want to log when in textfield
                            // I need to know which program is used, because I'll have dfferent filters (for example word vs firefox)

                        // NVDA

                        // Wenn NVDA Taste gedrückt wird, soll in einer Zeile NVDA plus zweiter gedrückter Taste stehen
                        if (character.KeyCode.ToString() == "Insert" && CurrentKey != "Insert")
                        {
                            Key = "NVDA + ";
                            File.AppendAllText(@"D:\KeysOnly.txt", Key + " ");
                            CurrentKey = "Insert";
                            
                            Tab = 0;
                        }

                        // NVDA Taste + zweite Taste
                        if(character.KeyCode.ToString() == "N" && Key == "NVDA + ")
                        {
                            File.AppendAllText(@"D:\KeysOnly.txt", character.KeyCode + "\r\n");
                            
                            File.AppendAllText(@"D:\ActiveProgram.txt", activeWindow.GetTitleOfActiveWindow() + "\r\n");
                            if (UsedProgram != activeWindow.GetTitleOfActiveWindow())
                            {
                                File.AppendAllText(@"D:\ETJob.txt", "Programm Job erstellen" + "\r\n");
                            }
                            else
                            {
                                File.AppendAllText(@"D:\ETJob.txt", " " + "\r\n");
                            }
                            UsedProgram = activeWindow.GetTitleOfActiveWindow();
                            CurrentKey = "N";
                            Tab = 0;
                            Key = "";
                            
                        }

                        if(character.KeyCode.ToString()== "Tab")
                        {
                            File.AppendAllText(@"D:\KeysOnly.txt", character.KeyCode + "\r\n");
                            File.AppendAllText(@"D:\ActiveProgram.txt", activeWindow.GetTitleOfActiveWindow() + "\r\n");
                            Tab++;
                            CurrentKey = "Tab";

                            if (Tab > 3 && CurrentKey =="Tab")
                            {
                                File.AppendAllText(@"D:\ETJob.txt", "Tab x " + Tab + "\r\n");
                            }
                            else
                            {
                                File.AppendAllText(@"D:\ETJob.txt", " " + "\r\n");
                                
                            }
                            UsedProgram = activeWindow.GetTitleOfActiveWindow();
                        }    

                    }


                });

            }
            base.OnStartup(e);
        }
    }
}