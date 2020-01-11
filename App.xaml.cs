
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
        string KeyOne = "";
        string KeyTwo = "";
        string KeyThree = "";
        string CurrentKey = "";
        int Tab = 0;
        string UsedProgram= "";
        bool IsFirst = false;
        bool IsSecond = false;
        
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

                    // Test in case I need to know which names were assigned to the pressed keys
                    File.AppendAllText(@"D:\Test.txt", character.KeyCode.ToString() + "\r\n" );


                        ////////////////////// Logik für Filter//////////////////////////////

                        // NVDA

                        // Erste Taste
                        if ((character.KeyCode.ToString() == "Insert" || character.KeyCode.ToString() == "NumPad0") && (CurrentKey != "Insert" || character.KeyCode.ToString() != "NumPad0") ||
                            (character.KeyCode.ToString() == "LMenu" && CurrentKey != "LMenu") || character.KeyCode.ToString() == "LControlKey")
                        {
                            //NVDA Taste 
                            if (character.KeyCode.ToString() == "Insert" || character.KeyCode.ToString() == "NumPad0")
                            {
                                KeyOne = "NVDA + ";
                                CurrentKey = "Insert";
                                
                            }
                            // Alt Taste
                            else if(character.KeyCode.ToString() == "LMenu")
                            {
                                KeyOne = "Alt + ";
                                CurrentKey = "LMenu";
                            }
                            else if (character.KeyCode.ToString() == "LControlKey")
                            {
                                KeyOne = "Strg + ";
                                CurrentKey = "LControlKey";
                            }
                            else
                            {
                                KeyOne = character.KeyCode.ToString();
                            }
                            File.AppendAllText(@"D:\KeysOnly.txt", KeyOne);
                            File.AppendAllText(@"D:\ActiveProgram.txt", activeWindow.GetTitleOfActiveWindow() + "\r\n");

                            // EasyTask Job Program Job add to 3rd column when active program switches

                            if (UsedProgram != activeWindow.GetTitleOfActiveWindow())
                            {
                                File.AppendAllText(@"D:\ETJob.txt", "Programm Job erstellen" + "\r\n");

                            }
                            else
                            {
                                File.AppendAllText(@"D:\ETJob.txt", " " + "\r\n");
                            }
                            IsFirst = true;
                            IsSecond = true;
                            Tab = 0;
                        }

                        // Erste Taste + zweite Taste
                        if ((character.KeyCode.ToString() == "Tab" || character.KeyCode.ToString() == "Delete" || character.KeyCode.ToString() == "Decimal" || character.KeyCode.ToString() == "LMenu" || character.KeyCode.ToString() == "N") && IsFirst == true)
                        {
                            // Entfernen Taste
                            if (character.KeyCode.ToString() == "Delete" || character.KeyCode.ToString() == "Decimal")
                            {
                                KeyTwo = "Num Entf";
                                
                                CurrentKey = "Delete";
                            }
                            // Alt Taste
                            else if(character.KeyCode.ToString() == "LMenu")
                            {
                                KeyTwo = "Alt";
                                CurrentKey = "LMenu";
                            }
                            else
                            {
                                KeyTwo = character.KeyCode.ToString();
                                CurrentKey = KeyTwo;
                            }

                            File.AppendAllText(@"D:\KeysOnly.txt", KeyTwo + "\r\n");

                            IsSecond = true;
                            IsFirst = false;
                            UsedProgram = activeWindow.GetTitleOfActiveWindow();

                            Tab = 1;
                            KeyOne = "";

                        }

                        // EasyTask Job Tab x
                        if (character.KeyCode.ToString() == "Tab" && IsSecond == false)
                        {

                            File.AppendAllText(@"D:\KeysOnly.txt", character.KeyCode + "\r\n");
                            File.AppendAllText(@"D:\ActiveProgram.txt", activeWindow.GetTitleOfActiveWindow() + "\r\n");


                            if (UsedProgram != activeWindow.GetTitleOfActiveWindow())
                            {
                                Tab = 1;
                            }

                            if (Tab > 3)
                            {
                                File.AppendAllText(@"D:\ETJob.txt", "Tab x " + Tab + "\r\n");
                            }
                            else
                            {

                                if (UsedProgram != activeWindow.GetTitleOfActiveWindow())
                                {
                                    File.AppendAllText(@"D:\ETJob.txt", "Programm Job erstellen" + " " + "\r\n");

                                }
                                else
                                {
                                    File.AppendAllText(@"D:\ETJob.txt", " " + "\r\n");
                                }


                            }
                            UsedProgram = activeWindow.GetTitleOfActiveWindow();
                            
                            Tab++;
                            CurrentKey = "Tab";
                        }
                    }
                    IsSecond = false;
                });

            }
            base.OnStartup(e);
        }
    }                                                                                 
}