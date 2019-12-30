
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
                        Logger activeWindow = new Logger();
                        File.AppendAllText(@"D:\KeysOnly.txt", character.KeyCode + "\r\n");
                        File.AppendAllText(@"D:\ActiveProgram.txt", activeWindow.GetTitleOfActiveWindow() + "\r\n");
                        

                    }
                });
                //api.CreateKeyboardHook((character) =>
                //{
                //    if (IsLoggingStarted)
                //    {
                //        File.AppendAllText(@"D:\KeyLog.txt",
                //            character + " " + LogFile.GetTitleOfActiveWindow() + " " + LogFile.GetFocusedControl() +
                //            "\r\n\r\n");
                //    }
                //});

            }
            base.OnStartup(e);
        }
    }
}