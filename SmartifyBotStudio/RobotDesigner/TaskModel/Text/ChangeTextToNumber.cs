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
    public class ChangeTextToNumber : RobotActionBase, ITask
    {
        public string TextToConvert { get; set; }
        public string TextToNumverStoreVar { get; set; }
        public int Execute()
        {
            try
            {


                int integer;

                if( int.TryParse(TextToConvert, out integer))
                {
                    integer = Convert.ToInt32(TextToConvert);
                }
                else
                {
                    integer = 0;
                }


               
                if (VariableStorage.TextToNumberVar.ContainsKey(TextToNumverStoreVar))
                {
                    VariableStorage.TextToNumberVar.Remove(TextToNumverStoreVar);
                }

                VariableStorage.TextToNumberVar.Add(TextToNumverStoreVar, Tuple.Create( this.ID,integer));

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}




