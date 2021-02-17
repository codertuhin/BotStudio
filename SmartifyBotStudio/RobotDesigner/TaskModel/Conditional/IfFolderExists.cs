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
    public class IfFolderExists : RobotActionBase, ITask
    {
        public string FolderPath { get; set; }
        public bool ExistsOrNotExists { get; set; }
        public int Execute()
        {
            try
            {
                ConditionResult = false;

                if (ExistsOrNotExists)
                {
                    if (System.IO.Directory.Exists(FolderPath))
                    {
                        ConditionResult = true;
                    }
                }

                if (!ExistsOrNotExists)
                {
                    if (!System.IO.Directory.Exists(FolderPath))
                    {
                        ConditionResult = true;
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
