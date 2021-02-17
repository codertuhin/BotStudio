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
    public class GetListLength : RobotActionBase, ITask
    {

        public string WorkingList { get; set; }
        public int ListVarIndex { get; set; }
        public string LengthStoreVar { get; set; }


        public int Execute()
        {
            try
            {

                if (VariableStorage.SetVariableVar.ContainsKey(LengthStoreVar))
                {
                    VariableStorage.SetVariableVar.Remove(LengthStoreVar);
                }

                VariableStorage.SetVariableVar.Add(LengthStoreVar, Tuple.Create(ID, (object)VariableStorage.CreateNewListVar[WorkingList].Item2.Count, typeof(int)));


                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}
