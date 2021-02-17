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
    public class ClearList : RobotActionBase, ITask
    {
        public VariableCollectionModel ListToClear { get; set; }
        public string ListToClearName { get; set; }

        public int Execute()
        {
            try
            {
                VariableStorage.CreateNewListVar[ListToClear.VariableName].Item2.Clear();

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }

}
