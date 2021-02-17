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
    public class GetTextLength : RobotActionBase, ITask
    {
        public string TextToMeasure { get; set; }
        public string TextLengthStoreVar { get; set; }
        public int Execute()
        {
            try
            {

                int textLength = 0;

                textLength = TextToMeasure.Length;


                if (VariableStorage.TextLengthVar.ContainsKey(TextLengthStoreVar))
                {
                    VariableStorage.TextLengthVar.Remove(TextLengthStoreVar);
                }
                VariableStorage.TextLengthVar.Add(TextLengthStoreVar, Tuple.Create(this.ID, textLength));

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}




