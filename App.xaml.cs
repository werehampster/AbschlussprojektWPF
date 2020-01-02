
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
        string SecondKey = "";
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


                        if (character.KeyCode.ToString() == "Insert")
                        {
                            Key = "NVDA + ";
                            File.AppendAllText(@"D:\KeysOnly.txt", Key + " ");
                            File.AppendAllText(@"D:\ActiveProgram.txt", activeWindow.GetTitleOfActiveWindow() + "\r\n");
                        }
                        else
                        {
                            File.AppendAllText(@"D:\KeysOnly.txt", character.KeyCode + "\r\n");
                            File.AppendAllText(@"D:\ActiveProgram.txt", activeWindow.GetTitleOfActiveWindow() + "\r\n");
                        }

                        
                    }


                });

            }
            base.OnStartup(e);
        }
    }
}