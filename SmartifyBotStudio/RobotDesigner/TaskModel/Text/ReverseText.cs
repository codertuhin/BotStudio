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
    public class ReverseText : RobotActionBase, ITask
    {
        public string TextToReverse { get; set; }
        public string ReversedTextStoreVar { get; set; }
        public int Execute()
        {
            try
            {
                char[] strTochar = TextToReverse.ToCharArray();

                Array.Reverse(strTochar);

                string reversedText = new String(strTochar);

                if (VariableStorage.ReversedTextVar.ContainsKey(ReversedTextStoreVar))
                {
                    VariableStorage.ReversedTextVar.Remove(ReversedTextStoreVar);
                }
                VariableStorage.ReversedTextVar.Add(ReversedTextStoreVar, Tuple.Create(this.ID, reversedText));

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}




