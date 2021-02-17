using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.DateTimeActions
{
    [Serializable]
    public class AddToDateTime : RobotActionBase, ITask
    {

        public string CurrentDateTimeVar { get; set; }
        public int AddOption { get; set; }
        public double AddTo { get; set; }
        public string ResultStoreVar { get; set; }
        public int Execute()
        {
            try
            {
                DateTime result = DateTime.Now;

                if (AddOption == 0)
                {
                    result = VariableStorage.GetCurrentDateTimeVar[CurrentDateTimeVar].Item2.AddSeconds(AddTo);
                }
                else if (AddOption == 1)
                {
                    result = VariableStorage.GetCurrentDateTimeVar[CurrentDateTimeVar].Item2.AddMinutes(AddTo);
                }
                else if (AddOption == 2)
                {
                    result = VariableStorage.GetCurrentDateTimeVar[CurrentDateTimeVar].Item2.AddHours(AddTo);
                }
                else if (AddOption == 3)
                {
                    result = VariableStorage.GetCurrentDateTimeVar[CurrentDateTimeVar].Item2.AddDays(AddTo);
                }
                else if (AddOption == 4)
                {
                    result = VariableStorage.GetCurrentDateTimeVar[CurrentDateTimeVar].Item2.AddMonths(Convert.ToInt32(AddTo));
                }
                else if (AddOption == 4)
                {
                    result = VariableStorage.GetCurrentDateTimeVar[CurrentDateTimeVar].Item2.AddYears(Convert.ToInt32(AddTo));
                }


                if (VariableStorage.AddToDateTimeVar.ContainsKey(ResultStoreVar))
                {
                    VariableStorage.AddToDateTimeVar.Remove(ResultStoreVar);
                }

                VariableStorage.AddToDateTimeVar.Add(ResultStoreVar, Tuple.Create(this.ID, result));


                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}
