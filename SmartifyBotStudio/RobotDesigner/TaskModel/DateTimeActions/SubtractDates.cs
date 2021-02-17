using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.DateTimeActions
{
    [Serializable]
    public class SubtractDates : RobotActionBase, ITask
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
