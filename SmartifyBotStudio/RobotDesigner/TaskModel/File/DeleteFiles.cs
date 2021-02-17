using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.File
{
    [Serializable]
    public class DeleteFiles:RobotActionBase,ITask
    {
        public List<SelectedFileInfo> FilesToDetele { get; set; }

        
        public string Var_StoreDeletedFiles { get; set; }

        public int Execute()
        {
            try
            {
                List<string> values = new List<string>();

                foreach (var file in FilesToDetele)
                {
                    System.IO.File.Delete(file.FilePath);

                    values.Add(file.FilePath);
                    
                }

                Variables.Add(new ListVariable()
                {
                    VariableName = Var_StoreDeletedFiles,
                    Value = values,
                    RobotAction = this,
                });

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
