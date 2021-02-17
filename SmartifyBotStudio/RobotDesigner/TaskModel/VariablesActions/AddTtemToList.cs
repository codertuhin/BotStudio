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
    public class AddTtemToList : RobotActionBase, ITask
    {

        public VariableCollectionModel WorkingList { get; set; }
        public VariableCollectionModel ItemToAdd { get; set; }
        public string WorkingListStr { get; set; }
        public string ItemToAddStr { get; set; }


        public int Execute()
        {
            try
            {
                if (ItemToAdd!=null)
                {
                    VariableStorage.CreateNewListVar[WorkingList.VariableName].Item2.Add((object)ItemToAdd.ObjectValue);

                }
                else
                {
                    VariableStorage.CreateNewListVar[WorkingList.VariableName].Item2.Add((object)ItemToAddStr);

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
