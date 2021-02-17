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
    public class IfWindowExists : RobotActionBase, ITask
    {
        public int Execute()
        {
            try
            {




                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
