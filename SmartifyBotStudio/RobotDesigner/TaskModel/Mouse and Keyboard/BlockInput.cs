using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WindowsInput;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Mouse_and_Keyboard
{

    [Serializable]
    public class Block_Input : RobotActionBase, ITask
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool BlockInput(bool fBlockIt);
        public int Execute()
        {
            try
            {
                //MessageBox.Show("sdfsdfdddddd");

                BlockInput(true);


                //Thread.Sleep(15*1000);
                //BlockInput(false);


                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }






    }
}
