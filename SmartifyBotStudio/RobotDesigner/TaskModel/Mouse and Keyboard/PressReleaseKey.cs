using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WindowsInput;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Mouse_and_Keyboard
{

    [Serializable]
    public class PressReleaseKey : RobotActionBase, ITask
    {

        public bool Control { get; set; }
        public bool Alt { get; set; }
        public bool Shift { get; set; }
        public bool Win { get; set; }


        public bool PressOrRelease { get; set; }


        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
        public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag

        public const int VK_LCONTROL = 0xA2;
        public const int VK_RCONTROL = 0xA3;

        public const int ALT = 0x12;
        public const int VK_LSHIFT = 0xA0;
        public const int VK_RSHIFT = 0xA1;

        public const int VK_LWIN = 0x5B;
        public const int VK_RWIN = 0x5C;
        private const int MOUSEEVENTF_WHEEL = 0x0800;
        //const uint MOUSEEVENTF_WHEEL = 0x0800;

   //     [DllImport("user32.dll")]
   //     static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData,
   //UIntPtr dwExtraInfo);


        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, int cButtons, int dwExtraInfo);

        public int Execute()
        {
            try
            {
                //for (int i = 0; i < 5; i++)
                //{
                //    mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -120, 0);

                //}

                //mouse_event(MOUSEEVENTF_WHEEL, 0, 0, (uint)-120, 0);

                if (PressOrRelease)
                {
                    if (Control)
                    {
                        keybd_event(VK_LCONTROL, 0, KEYEVENTF_EXTENDEDKEY, 0);
                    }
                    if (Shift)
                    {
                        keybd_event(VK_LSHIFT, 0, KEYEVENTF_EXTENDEDKEY, 0);
                    }
                    if (Alt)
                    {
                        keybd_event(ALT, 0, KEYEVENTF_EXTENDEDKEY, 0);
                    }
                    if (Win)
                    {
                        keybd_event(VK_LWIN, 0, KEYEVENTF_EXTENDEDKEY, 0);
                    }
                }
                else
                {
                    if (Control)
                    {
                        keybd_event(VK_LCONTROL, 0, KEYEVENTF_KEYUP, 0);
                        keybd_event(VK_RCONTROL, 0, KEYEVENTF_KEYUP, 0);
                    }
                    if (Shift)
                    {
                        keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
                        keybd_event(VK_RSHIFT, 0, KEYEVENTF_KEYUP, 0);
                    }
                    if (Alt)
                    {
                        keybd_event(ALT, 0, KEYEVENTF_KEYUP, 0);
                    }
                    if (Win)
                    {
                        keybd_event(VK_LWIN, 0, KEYEVENTF_KEYUP, 0);
                        keybd_event(VK_RWIN, 0, KEYEVENTF_KEYUP, 0);
                    }
                }



                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }






    }
}
