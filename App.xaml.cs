
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
        bool IsNVDA = false;
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
                    //File.AppendAllText(@"D:\Test.txt", character.KeyCode.ToString() + "\r\n" );
                        ////////////////////// Logik für Filter kommt hierhin //////////////////////////////
                        //Tab
                        //Enter
                        //Space
                        // I need focused element, because I don't want to log when in textfield
                        // I need to know which program is used, because I'll have dfferent filters (for example word vs firefox)

                        // NVDA

                        // Wenn NVDA Taste gedrückt wird, soll in einer Zeile NVDA plus zweiter gedrückter Taste stehen
                        if ((character.KeyCode.ToString() == "Insert" || character.KeyCode.ToString() == "NumPad0") && (CurrentKey != "Insert" || character.KeyCode.ToString() != "NumPad0"))
                        {
                            KeyOne = "NVDA + ";
                            File.AppendAllText(@"D:\KeysOnly.txt", KeyOne + " ");
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
                            CurrentKey = "Insert";
                            IsNVDA = true;
                            IsSecond = true;
                            Tab = 0;
                        }

                        // NVDA Taste + zweite Taste
                        if ((character.KeyCode.ToString() == "Tab" || character.KeyCode.ToString() == "Delete" || character.KeyCode.ToString() == "Decimal") && IsNVDA == true)
                        {

                            if (character.KeyCode.ToString() == "Delete" || character.KeyCode.ToString() == "Decimal")
                            {
                                KeyTwo = "Num Entf";
                                File.AppendAllText(@"D:\KeysOnly.txt", KeyTwo + "\r\n");
                                CurrentKey = "Delete";
                            }
                            else
                            {
                                KeyTwo = character.KeyCode.ToString();
                                File.AppendAllText(@"D:\KeysOnly.txt", KeyTwo + "\r\n");
                                CurrentKey = KeyTwo;
                            }



                            IsSecond = true;
                            IsNVDA = false;
                            UsedProgram = activeWindow.GetTitleOfActiveWindow();

                            Tab = 0;
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
                            //IsNVDA = false;
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