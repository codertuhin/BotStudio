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
    public class SetVariable : RobotActionBase, ITask
    {
        public string VarName { get; set; }
        public string ValueStr { get; set; }
        public Type DataType { get; set; }

        public int Execute()
        {
            try
            {
                if (VariableStorage.SetVariableVar.ContainsKey(VarName))
                {
                    VariableStorage.SetVariableVar.Remove(VarName);
                }
                VariableStorage.SetVariableVar.Add(VarName, Tuple.Create(ID, (object)ValueStr, DataType));


                if (RobotActionBase.testVariable.ContainsKey(VarName))
                {
                    RobotActionBase.testVariable.Remove(VarName);
                }
                RobotActionBase.testVariable.Add(VarName, Tuple.Create(ID, ValueStr));

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }

}
