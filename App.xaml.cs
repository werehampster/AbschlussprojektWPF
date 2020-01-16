
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
        //string KeyThree = "";
        string CurrentKey = "";
        int Tab = 0;
        string UsedProgram = "";
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

                        /////// Test in case I need to know which names were assigned to the pressed keys ///////
                        File.AppendAllText(@"D:\Test.txt", character.KeyCode.ToString() + "\r\n");

                        ////////////////////// Logik für Filter//////////////////////////////

                        /////// NVDA ///////


                        #region NVDA
                        if (wndMain.nvda == true)
                        {
                            // First Key
                            if ((character.KeyCode.ToString() == "Insert" || character.KeyCode.ToString() == "NumPad0") && (CurrentKey != "Insert" || character.KeyCode.ToString() != "NumPad0") ||
                        (character.KeyCode.ToString() == "LMenu" && CurrentKey != "LMenu") ||
                        (character.KeyCode.ToString() == "LControlKey" && CurrentKey != "LControlKey") ||
                        character.KeyCode.ToString() == "RMenu")
                            {
                                //NVDA Key 
                                if (character.KeyCode.ToString() == "Insert" || character.KeyCode.ToString() == "NumPad0")
                                {
                                    KeyOne = "NVDA + ";
                                    CurrentKey = "Insert";
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyOne);
                                    File.AppendAllText(@"D:\ActiveProgram.txt", activeWindow.GetTitleOfActiveWindow() + "\r\n");


                                }
                                // Alt Key
                                else if (character.KeyCode.ToString() == "LMenu")
                                {
                                    KeyOne = "Alt + ";
                                    CurrentKey = "LMenu";
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyOne);
                                    File.AppendAllText(@"D:\ActiveProgram.txt", activeWindow.GetTitleOfActiveWindow() + "\r\n");

                                }
                                else if (character.KeyCode.ToString() == "LControlKey")
                                {
                                    KeyOne = "Strg + ";
                                    CurrentKey = "LControlKey";


                                }
                                else if (character.KeyCode.ToString() == "RMenu")
                                {
                                    ;
                                }
                                else
                                {
                                    KeyOne = character.KeyCode.ToString();
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyOne);
                                    File.AppendAllText(@"D:\ActiveProgram.txt", activeWindow.GetTitleOfActiveWindow() + "\r\n");

                                }

                                // EasyTask Job Program Job add to 3rd column when active program switches

                                if (UsedProgram != activeWindow.GetTitleOfActiveWindow())
                                {
                                    File.AppendAllText(@"D:\ETJob.txt", "Programm Job erstellen" + "\r\n");

                                    UsedProgram = activeWindow.GetTitleOfActiveWindow();

                                }
                                else
                                {
                                    File.AppendAllText(@"D:\ETJob.txt", " " + "\r\n");

                                }
                                IsFirst = true;
                                IsSecond = true;
                                Tab = 1;
                            }

                            // First Key + second key
                            if ((character.KeyCode.ToString() == "Tab" ||
                                character.KeyCode.ToString() == "Delete" ||
                                character.KeyCode.ToString() == "Decimal" || //Numpad comma
                                (character.KeyCode.ToString() == "LMenu" && CurrentKey != "LMenu") || //Alt
                                character.KeyCode.ToString() == "N" ||
                                character.KeyCode.ToString() == "S" ||
                                character.KeyCode.ToString() == "RMenu") //AltGR needed to deactivate
                                && IsFirst == true)
                            {

                                // Delete key
                                if (character.KeyCode.ToString() == "Delete" || character.KeyCode.ToString() == "Decimal")
                                {
                                    KeyTwo = "Num Entf";

                                    CurrentKey = "Delete";
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyTwo + "\r\n");
                                }
                                // Alt key
                                else if (character.KeyCode.ToString() == "LMenu")
                                {
                                    KeyTwo = "Alt";
                                    CurrentKey = "LMenu";
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyTwo + "\r\n");
                                }
                                else if (character.KeyCode.ToString() == "RMenu")
                                {
                                    ;
                                }
                                // Alt GR key needs to be disabled because it logs as RMenu plus LControlKey
                                else if (character.KeyCode.ToString() == "RMenu" && KeyOne == "LControlKey")
                                {
                                    KeyTwo = "RMenu";
                                }
                                // Control key
                                else if (KeyOne == "Strg + " && KeyTwo != "RMenu")
                                {
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyOne + " " + character.KeyCode.ToString() + "\r\n");
                                    File.AppendAllText(@"D:\ActiveProgram.txt", activeWindow.GetTitleOfActiveWindow() + "\r\n");
                                    CurrentKey = character.KeyCode.ToString();
                                }
                                else
                                {
                                    KeyTwo = character.KeyCode.ToString();
                                    CurrentKey = KeyTwo;
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyTwo + "\r\n");
                                }

                                UsedProgram = activeWindow.GetTitleOfActiveWindow();

                                IsSecond = true; // needed only for Tab
                                IsFirst = false;
                                //Tab = 1;
                                KeyOne = "";

                            }
                        } 
                        #endregion


                        ////// JAWS ///////
                        #region JAWS
                        if (wndMain.jaws == true)
                        {
                            // First Key
                            if ((character.KeyCode.ToString() == "Insert" || character.KeyCode.ToString() == "NumPad0") && (CurrentKey != "Insert" || character.KeyCode.ToString() != "NumPad0") ||
                        (character.KeyCode.ToString() == "LMenu" && CurrentKey != "LMenu") ||
                        (character.KeyCode.ToString() == "LControlKey" && CurrentKey != "LControlKey") ||
                        character.KeyCode.ToString() == "RMenu")
                            {
                                //JAWS Key 
                                if (character.KeyCode.ToString() == "Insert" || character.KeyCode.ToString() == "NumPad0")
                                {
                                    KeyOne = "JAWS + ";
                                    CurrentKey = "Insert";
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyOne);
                                    File.AppendAllText(@"D:\ActiveProgram.txt", activeWindow.GetTitleOfActiveWindow() + "\r\n");


                                }
                                // Alt Key
                                else if (character.KeyCode.ToString() == "LMenu")
                                {
                                    KeyOne = "Alt + ";
                                    CurrentKey = "LMenu";
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyOne);
                                    File.AppendAllText(@"D:\ActiveProgram.txt", activeWindow.GetTitleOfActiveWindow() + "\r\n");

                                }
                                else if (character.KeyCode.ToString() == "LControlKey")
                                {
                                    KeyOne = "Strg + ";
                                    CurrentKey = "LControlKey";


                                }
                                else if (character.KeyCode.ToString() == "RMenu")
                                {
                                    ;
                                }
                                else
                                {
                                    KeyOne = character.KeyCode.ToString();
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyOne);
                                    File.AppendAllText(@"D:\ActiveProgram.txt", activeWindow.GetTitleOfActiveWindow() + "\r\n");

                                }

                                // EasyTask Job Program Job add to 3rd column when active program switches

                                if (UsedProgram != activeWindow.GetTitleOfActiveWindow())
                                {
                                    File.AppendAllText(@"D:\ETJob.txt", "Programm Job erstellen" + "\r\n");

                                    UsedProgram = activeWindow.GetTitleOfActiveWindow();

                                }
                                else
                                {
                                    File.AppendAllText(@"D:\ETJob.txt", " " + "\r\n");

                                }
                                IsFirst = true;
                                IsSecond = true;
                                Tab = 1;
                            }

                            // First Key + second key
                            if ((character.KeyCode.ToString() == "Tab" ||
                                character.KeyCode.ToString() == "Delete" ||
                                character.KeyCode.ToString() == "Decimal" || //Numpad comma
                                (character.KeyCode.ToString() == "LMenu" && CurrentKey != "LMenu") || //Alt
                                character.KeyCode.ToString() == "N" ||
                                character.KeyCode.ToString() == "S" ||
                                character.KeyCode.ToString() == "RMenu") //AltGR needed to deactivate
                                && IsFirst == true)
                            {

                                // Delete key
                                if (character.KeyCode.ToString() == "Delete" || character.KeyCode.ToString() == "Decimal")
                                {
                                    KeyTwo = "Num Entf";

                                    CurrentKey = "Delete";
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyTwo + "\r\n");
                                }
                                // Alt key
                                else if (character.KeyCode.ToString() == "LMenu")
                                {
                                    KeyTwo = "Alt";
                                    CurrentKey = "LMenu";
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyTwo + "\r\n");
                                }
                                else if (character.KeyCode.ToString() == "RMenu")
                                {
                                    ;
                                }
                                // Alt GR key needs to be disabled because it logs as RMenu plus LControlKey
                                else if (character.KeyCode.ToString() == "RMenu" && KeyOne == "LControlKey")
                                {
                                    KeyTwo = "RMenu";
                                }
                                // Control key
                                else if (KeyOne == "Strg + " && KeyTwo != "RMenu")
                                {
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyOne + " " + character.KeyCode.ToString() + "\r\n");
                                    File.AppendAllText(@"D:\ActiveProgram.txt", activeWindow.GetTitleOfActiveWindow() + "\r\n");
                                    CurrentKey = character.KeyCode.ToString();
                                }
                                else
                                {
                                    KeyTwo = character.KeyCode.ToString();
                                    CurrentKey = KeyTwo;
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyTwo + "\r\n");
                                }

                                UsedProgram = activeWindow.GetTitleOfActiveWindow();

                                IsSecond = true; // needed only for Tab
                                IsFirst = false;
                                //Tab = 1;
                                KeyOne = "";

                            }
                        } 
                        #endregion

                        ////// ZoomText /////

                        #region ZOOMTEXT
                        if (wndMain.zoomtext == true)
                        {
                            // First Key
                            if ((character.KeyCode.ToString() == "Insert" || character.KeyCode.ToString() == "NumPad0") && (CurrentKey != "Insert" || character.KeyCode.ToString() != "NumPad0") ||
                        (character.KeyCode.ToString() == "LMenu" && CurrentKey != "LMenu") ||
                        (character.KeyCode.ToString() == "LControlKey" && CurrentKey != "LControlKey") ||
                        character.KeyCode.ToString() == "RMenu")
                            {
                                //NVDA Key 
                                if (character.KeyCode.ToString() == "Insert" || character.KeyCode.ToString() == "NumPad0")
                                {
                                    KeyOne = "NVDA + ";
                                    CurrentKey = "Insert";
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyOne);
                                    File.AppendAllText(@"D:\ActiveProgram.txt", activeWindow.GetTitleOfActiveWindow() + "\r\n");


                                }
                                // Alt Key
                                else if (character.KeyCode.ToString() == "LMenu")
                                {
                                    KeyOne = "Alt + ";
                                    CurrentKey = "LMenu";
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyOne);
                                    File.AppendAllText(@"D:\ActiveProgram.txt", activeWindow.GetTitleOfActiveWindow() + "\r\n");

                                }
                                else if (character.KeyCode.ToString() == "LControlKey")
                                {
                                    KeyOne = "Strg + ";
                                    CurrentKey = "LControlKey";


                                }
                                else if (character.KeyCode.ToString() == "RMenu")
                                {
                                    ;
                                }
                                else
                                {
                                    KeyOne = character.KeyCode.ToString();
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyOne);
                                    File.AppendAllText(@"D:\ActiveProgram.txt", activeWindow.GetTitleOfActiveWindow() + "\r\n");

                                }

                                // EasyTask Job Program Job add to 3rd column when active program switches

                                if (UsedProgram != activeWindow.GetTitleOfActiveWindow())
                                {
                                    File.AppendAllText(@"D:\ETJob.txt", "Programm Job erstellen" + "\r\n");

                                    UsedProgram = activeWindow.GetTitleOfActiveWindow();

                                }
                                else
                                {
                                    File.AppendAllText(@"D:\ETJob.txt", " " + "\r\n");

                                }
                                IsFirst = true;
                                IsSecond = true;
                                Tab = 1;
                            }

                            // First Key + second key
                            if ((character.KeyCode.ToString() == "Tab" ||
                                character.KeyCode.ToString() == "Delete" ||
                                character.KeyCode.ToString() == "Decimal" || //Numpad comma
                                (character.KeyCode.ToString() == "LMenu" && CurrentKey != "LMenu") || //Alt
                                character.KeyCode.ToString() == "N" ||
                                character.KeyCode.ToString() == "S" ||
                                character.KeyCode.ToString() == "RMenu") //AltGR needed to deactivate
                                && IsFirst == true)
                            {

                                // Delete key
                                if (character.KeyCode.ToString() == "Delete" || character.KeyCode.ToString() == "Decimal")
                                {
                                    KeyTwo = "Num Entf";

                                    CurrentKey = "Delete";
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyTwo + "\r\n");
                                }
                                // Alt key
                                else if (character.KeyCode.ToString() == "LMenu")
                                {
                                    KeyTwo = "Alt";
                                    CurrentKey = "LMenu";
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyTwo + "\r\n");
                                }
                                else if (character.KeyCode.ToString() == "RMenu")
                                {
                                    ;
                                }
                                //// Alt key
                                //else if(character.KeyCode.ToString() == "LMenu")
                                //{
                                //    KeyTwo = "Alt";
                                //    CurrentKey = "LMenu";
                                //    File.AppendAllText(@"D:\KeysOnly.txt", KeyTwo + "\r\n");
                                //}
                                // Alt GR key needs to be disabled because it logs as RMenu plus LControlKey
                                else if (character.KeyCode.ToString() == "RMenu" && KeyOne == "LControlKey")
                                {
                                    KeyTwo = "RMenu";
                                }
                                // Control key
                                else if (KeyOne == "Strg + " && KeyTwo != "RMenu")
                                {
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyOne + " " + character.KeyCode.ToString() + "\r\n");
                                    File.AppendAllText(@"D:\ActiveProgram.txt", activeWindow.GetTitleOfActiveWindow() + "\r\n");
                                    CurrentKey = character.KeyCode.ToString();
                                }
                                else
                                {
                                    KeyTwo = character.KeyCode.ToString();
                                    CurrentKey = KeyTwo;
                                    File.AppendAllText(@"D:\KeysOnly.txt", KeyTwo + "\r\n");
                                }

                                UsedProgram = activeWindow.GetTitleOfActiveWindow();

                                IsSecond = true; // needed only for Tab
                                IsFirst = false;
                                //Tab = 1;
                                KeyOne = "";

                            }
                        } 
                        #endregion

                        // EasyTask Job Tab x
                        if (character.KeyCode.ToString() == "Tab" && IsSecond == false)
                        {

                            File.AppendAllText(@"D:\KeysOnly.txt", character.KeyCode + "\r\n");
                            File.AppendAllText(@"D:\ActiveProgram.txt", activeWindow.GetTitleOfActiveWindow() + "\r\n");
                            
 

                            if (UsedProgram != activeWindow.GetTitleOfActiveWindow())
                            {
                                Tab = 1;
   
                            }

                            if (Tab > 2)
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