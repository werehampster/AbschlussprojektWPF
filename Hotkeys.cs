﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AbschlussprojektWPF
{
    /// <summary>
    /// Klasse zur Überwachung von Tastatur Events
    /// </summary>
    class MyHotkeys
    {
        // externe Hotkey Methoden, damit ich Keys loggen kann, wenn mein Fenster nicht im Focus ist

        // Note that you're not providing a method body. The method is defined in user32.dll, you're just adding a way for your application to directly 
        //call that method. FYI, hWnd refers to the window handle (we will pass our form's handle), id is the unique identifier of the hotkey, fsModifiers 
        //is the integer representation of the modifier keys (shift/alt/ctrl/win) that you want pressed with your key, and vk is the virtual key code for 
        //the hotkey.

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private int modifier;
        private int key;
        private IntPtr hWnd;
        private int id;

        public void GlobalHotkey(int modifier, Keys key, Form form)
        {
            this.modifier = modifier;
            this.key = (int)key;
            this.hWnd = form.Handle;
            id = this.GetHashCode();
        }

        public override int GetHashCode()
        {
            return modifier ^ key ^ hWnd.ToInt32();
        }

        public bool Register()
        {
            return RegisterHotKey(hWnd, id, modifier, key);
        }

        public bool Unregiser()
        {
            return UnregisterHotKey(hWnd, id);
        }
    }
    namespace Hotkeys
    {
        public static class Constants
        {
            //modifiers
            public const int NOMOD = 0x0000;
            public const int ALT = 0x0001;
            public const int CTRL = 0x0002;
            public const int SHIFT = 0x0004;
            public const int WIN = 0x0008;

            //windows message id for hotkey
            public const int WM_HOTKEY_MSG_ID = 0x0312;
        }
    }
}
