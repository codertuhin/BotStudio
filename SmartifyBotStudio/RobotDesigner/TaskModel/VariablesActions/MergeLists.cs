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
    public class MergeLists : RobotActionBase, ITask
    {

        public VariableCollectionModel FirstList { get; set; }
        public VariableCollectionModel SecondList { get; set; }

        public string FirstListStr { get; set; }
        public string SecondListStr { get; set; }

        public string ResultStoreVar { get; set; }

        public int Execute()
        {
            try
            {

                List<object> first = VariableStorage.CreateNewListVar[FirstList.VariableName].Item2;
                List<object> second = VariableStorage.CreateNewListVar[SecondList.VariableName].Item2;


                List<object> third = new List<object>();



                third.AddRange(first);
                third.AddRange(second);


                if (VariableStorage.CreateNewListVar.ContainsKey(ResultStoreVar))
                {
                    VariableStorage.CreateNewListVar.Remove(ResultStoreVar);
                }
                VariableStorage.CreateNewListVar.Add(ResultStoreVar, Tuple.Create(this.ID, third, typeof(object)));


                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }

}
