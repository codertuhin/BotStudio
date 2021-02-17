using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Conditional
{
    [Serializable]
    public class Else : RobotActionBase, ITask
    {
        public int Execute()
        {
            try
            {

                // ConditionResult = true;     // Must be true 


                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
