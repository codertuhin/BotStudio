using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.VariablesActions
{

    [Serializable]
    public class DeleteList : RobotActionBase, ITask
    {

        public string ListToDelete { get; set; }
        public int ListNameIndex { get; set; }

        public int Execute()
        {
            try
            {
                if (VariableStorage.CreateNewListVar.ContainsKey(ListToDelete))
                {
                    VariableStorage.CreateNewListVar.Remove(ListToDelete);
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
