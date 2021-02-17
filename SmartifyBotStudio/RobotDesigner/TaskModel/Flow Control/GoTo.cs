using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Flow_Control
{
    [Serializable]
    public class GoTo : RobotActionBase, ITask
    {
        public string LabelNameToGo { get; set; }
        public int LabelNameIndex { get; set; }
        public int Execute()
        {
            try
            {
                LabelToGo = LabelNameToGo;


                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}
