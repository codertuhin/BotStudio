using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Mouse_and_Keyboard
{
    [Serializable]
    public class SetKeyState : RobotActionBase, ITask
    {



       


        #region KEYBOARD


        public int KeyIndex { get; set; }
        public bool OnOff { get; set; }

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        const int KEYEVENTF_EXTENDEDKEY = 0x1;
        const int KEYEVENTF_KEYUP = 0x2;




        public int Execute()
        {
            try
            {

                //System.Windows.MessageBox.Show(GetActiveWindowTitle());


                #region TEST


                dynamic x = 10;
                dynamic y = 3.14;
                dynamic z = "test";
                dynamic k = true;
                dynamic l = x + y * z - k;

                System.Windows.MessageBox.Show(l);

                #endregion


                //if (KeyIndex == 0)            /// CAPS LOCK
                //{
                //    if (OnOff)
                //    {
                //        if (!Control.IsKeyLocked(Keys.CapsLock))
                //        {
                //            Console.WriteLine("Caps Lock key is ON.  We'll turn it off");
                //            keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)1);
                //            keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP,
                //                (UIntPtr)1);
                //        }
                //    }
                //    else if (!OnOff)
                //    {
                //        if (Control.IsKeyLocked(Keys.CapsLock))
                //        {
                //            keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                //            keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP,
                //                (UIntPtr)0);
                //            Console.WriteLine("Caps Lock key is OFF");
                //        }
                //    }

                //}
                //else if (KeyIndex == 1)         /// NUM LOCK
                //{
                //    if (OnOff)
                //    {
                //        if (!Control.IsKeyLocked(Keys.NumLock))
                //        {
                //            Console.WriteLine("Caps Lock key is ON.  We'll turn it off");
                //            keybd_event(0x90, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)1);
                //            keybd_event(0x90, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP,
                //                (UIntPtr)1);
                //        }
                //    }
                //    else if (!OnOff)
                //    {
                //        if (Control.IsKeyLocked(Keys.NumLock))
                //        {
                //            keybd_event(0x90, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                //            keybd_event(0x90, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP,
                //                (UIntPtr)0);
                //            Console.WriteLine("Caps Lock key is OFF");
                //        }
                //    }
                //}
                //else if (KeyIndex == 2)         /// SCROLL LOCK
                //{
                //    if (OnOff)
                //    {
                //        if (!Control.IsKeyLocked(Keys.Scroll))
                //        {
                //            Console.WriteLine("Caps Lock key is ON.  We'll turn it off");
                //            keybd_event(0x91, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)1);
                //            keybd_event(0x91, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP,
                //                (UIntPtr)1);
                //        }
                //    }
                //    else if (!OnOff)
                //    {
                //        if (Control.IsKeyLocked(Keys.Scroll))
                //        {
                //            keybd_event(0x91, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                //            keybd_event(0x91, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP,
                //                (UIntPtr)0);
                //            Console.WriteLine("Caps Lock key is OFF");
                //        }
                //    }

                //}











                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        #endregion


        #region Retrieve list of windows

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();


        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;

            

        }

        #endregion


    }
}
