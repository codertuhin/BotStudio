using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Text
{
    [Serializable]
    public class ConverNumberToText : RobotActionBase, ITask
    {

        public string TextToConvert { get; set; }
        public int NumberOfDecimalPlaces { get; set; }
        public bool UseSeparator { get; set; }
        public string ResultStoreVar { get; set; }

        public int Execute()
        {
            try
            {

                string result;


                decimal dec = Convert.ToDecimal(TextToConvert);

                string formator = new string('#', NumberOfDecimalPlaces);
                formator = "#." + formator;

                string zeroFormator = new string('0', NumberOfDecimalPlaces);
                zeroFormator = "0." + zeroFormator;


             



                result = dec.ToString(formator);

                result = dec.ToString(zeroFormator);

                if (UseSeparator)
                {
                    result = dec.ToString("n"+ NumberOfDecimalPlaces.ToString());
                }

                if (VariableStorage.NumberToTextVar.ContainsKey(ResultStoreVar))
                {
                    VariableStorage.NumberToTextVar.Remove(ResultStoreVar);
                }
                VariableStorage.NumberToTextVar.Add(ResultStoreVar, Tuple.Create(this.ID, result));


                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}




