using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SmartifyBotStudio.RobotDesigner.TaskModel.Conditional
{
    [Serializable]
    public class IfProcessExists : RobotActionBase, ITask
    {
        public string ProcessName { get; set; }
        public bool RunOrNotRun { get; set; }
        public int Execute()
        {
            try
            {

                ConditionResult = false;

                Process[] pname = Process.GetProcessesByName(ProcessName);



                if (RunOrNotRun)
                {
                    if (pname.Length > 0)
                        ConditionResult = true;
                }
                if (!RunOrNotRun)
                {
                    if (pname.Length == 0)
                        ConditionResult = true;
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
