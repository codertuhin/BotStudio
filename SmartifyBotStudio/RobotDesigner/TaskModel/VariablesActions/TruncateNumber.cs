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
    public class TruncateNumber : RobotActionBase, ITask
    {
        public VariableCollectionModel WorkingVariable { get; set; }
        public string NumberToTruncate { get; set; }
        public int TruncateSchema { get; set; }
        public string ResultStoreVar { get; set; }
        public string WorkingVariableStr { get; set; }
        public int RoundUp { get; set; }

        public int Execute()
        {
            try
            {
                double tmp;

                if (WorkingVariable != null)
                {
                    tmp = Convert.ToDouble(VariableStorage.SetVariableVar[WorkingVariable.VariableName].Item2);
                }
                else
                {
                    tmp = Convert.ToDouble(NumberToTruncate);
                }

                if (TruncateSchema == 0)
                {
                    tmp = Math.Truncate(tmp);
                }
                else if (TruncateSchema == 1)
                {
                    tmp = tmp - Math.Truncate(tmp);
                }
                else if (TruncateSchema == 2)
                {
                    tmp = Math.Round(tmp, RoundUp);
                }



                if (VariableStorage.TruncateNuberVar.ContainsKey(ResultStoreVar))
                {
                    VariableStorage.TruncateNuberVar.Remove(ResultStoreVar);
                }
                VariableStorage.TruncateNuberVar.Add(ResultStoreVar, Tuple.Create(this.ID, tmp));

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }

}
