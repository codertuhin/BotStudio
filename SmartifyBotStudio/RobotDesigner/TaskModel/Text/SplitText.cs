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
    public class SplitText : RobotActionBase, ITask
    {
        public string TextToSplit { get; set; }
        public string Delimiter { get; set; }
        public int DelimiterOccurneceNumber { get; set; }
        public int DelimiterOption { get; set; }
        public string RegulerExpression { get; set; }
        public bool IsRegulerExpression { get; set; }
        public int StandardDelimeterIndex { get; set; }
        public string SplitTextStoreVar { get; set; }
        public string CustomDelimiter { get; set; }

        public int Execute()
        {
            try
            {
                string[] result= { };



                if (DelimiterOption == 0)   // For standard delimiter
                {
                    for (int i = 0; i < DelimiterOccurneceNumber - 1; i++)
                    {
                        Delimiter += Delimiter;
                    }

                    result = TextToSplit.Split(new string[] { Delimiter }, StringSplitOptions.None);

                   

                }
                else if (DelimiterOption == 1)      // For custom delimiter
                {
                    if (IsRegulerExpression)
                    {
                        result = Regex.Split(TextToSplit, CustomDelimiter);
                    }
                    else
                    {
                        result = TextToSplit.Split(new string[] { Delimiter }, StringSplitOptions.None);
                    }

                }


                if (VariableStorage.SplitTextVar.ContainsKey(SplitTextStoreVar))
                {
                    VariableStorage.SplitTextVar.Remove(SplitTextStoreVar);
                }

                VariableStorage.SplitTextVar.Add(SplitTextStoreVar, Tuple.Create(this.ID, result));



                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}




