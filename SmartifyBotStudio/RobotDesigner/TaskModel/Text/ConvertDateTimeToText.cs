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
    public class ConvertDateTimeToText : RobotActionBase, ITask
    {
        public int DateTiemToConvertOptionComboIndex { get; set; }
        public string DateTimeToConvertVar { get; set; }
        public int DateTimeToCovertVarComboIndex { get; set; }
        public string ResultStoreVar { get; set; }
        public int StandardFormatIndex { get; set; }
        public string CustomFormat { get; set; }


        public int Execute()
        {
            try
            {

                string FormatedDateString = "";

                if (DateTiemToConvertOptionComboIndex == 0)
                {
                    if (StandardFormatIndex == 0)
                    {
                        FormatedDateString = VariableStorage.TextToDateTimeVar[DateTimeToConvertVar].Item2.ToString("MM/dd/yyyy");
                    }
                    else if (StandardFormatIndex == 1)
                    {
                        FormatedDateString = VariableStorage.TextToDateTimeVar[DateTimeToConvertVar].Item2.ToString("dddd, MMMM dd, yyyy");
                    }
                    else if (StandardFormatIndex == 2)
                    {
                        FormatedDateString = VariableStorage.TextToDateTimeVar[DateTimeToConvertVar].Item2.ToString("h:mm tt");
                    }
                    else if (StandardFormatIndex == 3)
                    {
                        FormatedDateString = VariableStorage.TextToDateTimeVar[DateTimeToConvertVar].Item2.ToString("HH:mm:ss tt");
                    }
                    else if (StandardFormatIndex == 4)
                    {
                        FormatedDateString = VariableStorage.TextToDateTimeVar[DateTimeToConvertVar].Item2.ToString("dddd, MMMM dd, yyyy h:mm tt");
                    }
                    else if (StandardFormatIndex == 5)
                    {
                        FormatedDateString = VariableStorage.TextToDateTimeVar[DateTimeToConvertVar].Item2.ToString("dddd, MMMM dd, yyyy HH:mm:ss tt");
                    }
                    else if (StandardFormatIndex == 6)
                    {
                        FormatedDateString = VariableStorage.TextToDateTimeVar[DateTimeToConvertVar].Item2.ToString("MM/dd/yyyy h:mm tt");
                    }
                    else if (StandardFormatIndex == 7)
                    {
                        FormatedDateString = VariableStorage.TextToDateTimeVar[DateTimeToConvertVar].Item2.ToString("MM/dd/yyyy HH:mm:ss tt");
                    }
                    else if (StandardFormatIndex == 8)
                    {
                        FormatedDateString = VariableStorage.TextToDateTimeVar[DateTimeToConvertVar].Item2.ToString("");
                    }
                }
                else
                {
                    FormatedDateString = VariableStorage.TextToDateTimeVar[DateTimeToConvertVar].Item2.ToString(CustomFormat);
                }




                if (VariableStorage.DateToTextVar.ContainsKey(ResultStoreVar))
                {
                    VariableStorage.DateToTextVar.Remove(ResultStoreVar);
                }

                VariableStorage.DateToTextVar.Add(ResultStoreVar, Tuple.Create(this.ID, FormatedDateString));




                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}




