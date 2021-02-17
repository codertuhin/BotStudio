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
    public class RemoveItemFromList : RobotActionBase, ITask
    {
        public VariableCollectionModel WorkingListVariable { get; set; }
        public VariableCollectionModel IndexVariable { get; set; }

        public string WorkingListVariableStr { get; set; }
        public string IndexVariableStr { get; set; }



        public int Execute()
        {
            try
            {
                int index;

                if (IndexVariable != null)
                {
                    index = Convert.ToInt32(IndexVariable.ObjectValue);
                }
                else
                {
                    index = Convert.ToInt32(IndexVariableStr);
                }


                VariableStorage.CreateNewListVar[WorkingListVariable.VariableName].Item2.RemoveAt(index);



                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }

}
