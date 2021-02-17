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
    public class MouseScroll : RobotActionBase, ITask
    {

        public int UpOrDown = 120;
        public int WheelAmount { get; set; }

        private const int MOUSEEVENTF_WHEEL = 0x0800;


        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, int cButtons, int dwExtraInfo);

        public int Execute()
        {
            try
            {


                for (int i = 0; i < WheelAmount + 2; i++)
                {
                    mouse_event(MOUSEEVENTF_WHEEL, 0, 0, UpOrDown, 0);

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
