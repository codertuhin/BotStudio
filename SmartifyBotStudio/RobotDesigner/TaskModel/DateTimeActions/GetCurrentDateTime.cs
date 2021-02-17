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
    public class GetCurrentDateTime : RobotActionBase, ITask
    {

        public int DateRetrieveOption { get; set; }
        public string ResultStoreVar { get; set; }
        public int Execute()
        {
            try
            {
                DateTime dateTime = DateTime.Now;

                if (DateRetrieveOption == 1)
                {
                    dateTime = DateTime.Now.Date;
                }



                VariableStorage.GetCurrentDateTimeVar.Add(ResultStoreVar, Tuple.Create(this.ID, dateTime));


                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}
