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
    public class ReverseList : RobotActionBase, ITask
    {
        public VariableCollectionModel ListToReverse { get; set; }
        public string ListToReverseStr { get; set; }

        public int Execute()
        {
            try
            {
                VariableStorage.CreateNewListVar[ListToReverse.VariableName].Item2.Reverse();

                //VariableStorage.CreateNewListVar[ListToReverse.VariableName].Item2.Distinct();

                

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }

}
