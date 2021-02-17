using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Text
{
    [Serializable]
    public class ReplaceText : RobotActionBase, ITask
    {
        public string TextToParse { get; set; }
        public string TextToFind { get; set; }
        public string ReplaceWith { get; set; }
        public string ReplaceTextStoreVar { get; set; }
        public bool UseRegulerExpression { get; set; }
        public bool IgnoreCase { get; set; }
        public bool ActiveEscapeSequence { get; set; }
        public int Execute()
        {
            try
            {
                string result = "";

                if (UseRegulerExpression)
                {
                    result = Regex.Replace(TextToParse, TextToFind, ReplaceWith);

                }
                else if (IgnoreCase || (UseRegulerExpression && IgnoreCase))
                {
                    result = Regex.Replace(TextToParse, TextToFind, ReplaceWith, RegexOptions.IgnoreCase);

                }
                else
                {
                    if (ActiveEscapeSequence)
                    {
                        result = TextToParse.Replace(TextToFind, @ReplaceWith);
                    }
                    else
                    {
                        result = TextToParse.Replace(TextToFind, ReplaceWith);
                    }
                }


                if (VariableStorage.ReplaceTextVar.ContainsKey(ReplaceTextStoreVar))
                {
                    VariableStorage.ReplaceTextVar.Remove(ReplaceTextStoreVar);
                }

                VariableStorage.ReplaceTextVar.Add(ReplaceTextStoreVar, Tuple.Create(this.ID, result));



                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}




