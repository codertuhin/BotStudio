using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Text
{
    [Serializable]
    public class CompareText : RobotActionBase, ITask
    {
        public string TextToCompare { get; set; }
        public string CompareWtih { get; set; }
        public bool IgnoreCase { get; set; }
        public int Execute()
        {
            try
            {
                bool result;

                if (IgnoreCase)
                {
                    result = TextToCompare.Equals(CompareWtih, StringComparison.OrdinalIgnoreCase);

                }
                else
                {
                    result = TextToCompare.Equals(CompareWtih, StringComparison.Ordinal);
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




