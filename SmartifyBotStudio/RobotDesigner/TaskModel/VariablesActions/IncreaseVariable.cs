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
    public class IncreaseVariable : RobotActionBase, ITask
    {

        public VariableCollectionModel WorkingVariable { get; set; }
        public VariableCollectionModel IncreaseByVariable { get; set; }
        public string WorkingVarName { get; set; }
        public string IncreaseByVarName { get; set; }

        public int Execute()
        {
            try
            {
                double working_variable=0;

                double increaseBy_variable;

                if (WorkingVariable != null)
                {
                    working_variable = Convert.ToDouble(VariableStorage.SetVariableVar[WorkingVariable.VariableName].Item2);

                }
               

                if (IncreaseByVariable != null)
                {
                    increaseBy_variable = Convert.ToDouble(VariableStorage.SetVariableVar[IncreaseByVariable.VariableName].Item2);

                }
                else
                {
                    increaseBy_variable = Convert.ToDouble(IncreaseByVarName);

                }


                working_variable += increaseBy_variable;


                string tmpID = VariableStorage.SetVariableVar[WorkingVariable.VariableName].Item1;

                if (VariableStorage.SetVariableVar.ContainsKey(WorkingVariable.VariableName))
                {                    
                    VariableStorage.SetVariableVar.Remove(WorkingVariable.VariableName);
                }
                VariableStorage.SetVariableVar.Add(WorkingVariable.VariableName, Tuple.Create(tmpID, (object)working_variable, typeof(double)));


                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }

}
