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
    public class DecreaseVariable : RobotActionBase, ITask
    {


        public VariableCollectionModel WorkingVariable { get; set; }
        public VariableCollectionModel DecreaseByVariable { get; set; }
        public string WorkingVarName { get; set; }
        public string IncreaseByVarName { get; set; }

        public int Execute()
        {
            try
            {
                double working_variable = 0;

                double dencreaseBy_variable;

                if (WorkingVariable != null)
                {
                    working_variable = Convert.ToDouble(VariableStorage.SetVariableVar[WorkingVariable.VariableName].Item2);
                    
                }


                if (DecreaseByVariable != null)
                {
                    dencreaseBy_variable = Convert.ToDouble(VariableStorage.SetVariableVar[DecreaseByVariable.VariableName].Item2);
                   
                }
                else
                {
                    dencreaseBy_variable = Convert.ToDouble(IncreaseByVarName);

                }


                working_variable -= dencreaseBy_variable;


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
