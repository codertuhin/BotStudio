using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Loops
{
    [Serializable]
    public class Loop : RobotActionBase, ITask
    {
        public int Start { get; set; }
        public int End { get; set; }
        public int IncrementBy { get; set; }

        public int Execute()
        {
            try
            {


                LoopStart = Start;

                LoopEnd = End;

                IncreaseBy = IncrementBy;


                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}
