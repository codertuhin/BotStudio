using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Text
{
    [Serializable]
    public class ConvertTextToDateTime : RobotActionBase, ITask
    {
        public string TextToDate { get; set; }
        public bool UseCustomFormat { get; set; }
        public string CustomFormat { get; set; }
        public string DateToStoreVar { get; set; }
        public int Execute()
        {
            try
            {
                DateTime dateTime;
                
                     dateTime = Convert.ToDateTime(TextToDate);
                
                if (UseCustomFormat)
                {
                    
                    //dateTime = DateTime.ParseExact(TextToDate, CustomFormat, CultureInfo.InvariantCulture);

                    string strDate = dateTime.ToString(CustomFormat);

                    dateTime = Convert.ToDateTime(strDate);


                }
               


                if (VariableStorage.TextToDateTimeVar.ContainsKey(DateToStoreVar))
                {
                    VariableStorage.TextToDateTimeVar.Remove(DateToStoreVar);
                }
                VariableStorage.TextToDateTimeVar.Add(DateToStoreVar, Tuple.Create(this.ID, dateTime));


                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}




