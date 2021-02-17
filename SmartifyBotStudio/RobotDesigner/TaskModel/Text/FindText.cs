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
    public class FindText : RobotActionBase, ITask
    {
        public string TextToParse { get; set; }
        public string TextToFind { get; set; }
        public bool UseRegulerExpression { get; set; }
        public bool FirstOccurenceOnly { get; set; }
        public bool IgnoreCase { get; set; }

        public static int VarCounter = 0;
        public string ResultStoreVar ="%FindTextVar%";
        
        public int StartingPoint { get; set; }
        public int Execute()
        {
            try
            {
                string textToParse;
                string textToFind;

                List<int> indexes = new List<int>();


                if (IgnoreCase)
                {
                    textToParse = TextToParse.ToLower();
                    textToFind = TextToFind.ToLower();
                }
                else
                {
                    textToParse = TextToParse;
                    textToFind = TextToFind;
                }

                if (UseRegulerExpression)
                {
                    string pattern = textToFind;
                    textToParse=textToParse.Remove(0, StartingPoint+1);
                    Regex rgx = new Regex(textToFind);
                    
                    foreach (Match match in rgx.Matches(textToParse))
                    {
                        indexes.Add( match.Index);

                    }
                }
                else
                {
                    indexes = FindIndexes(TextToParse + " ", TextToFind, StartingPoint).ToList();

                }


                if (FirstOccurenceOnly && indexes.Count > 1)
                {
                    indexes.RemoveRange(1, indexes.Count - 1);
                }


               
                if (VariableStorage.FindTextVar.ContainsKey(ResultStoreVar))
                {
                    VariableStorage.FindTextVar.Remove(ResultStoreVar);
                }

                VariableStorage.FindTextVar.Add(ResultStoreVar, Tuple.Create(this.ID, indexes));
                VarCounter++;



                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public IEnumerable<int> FindIndexes(string text, string query, int startingPoint)
        {
            return Enumerable.Range(startingPoint, text.Length - query.Length)
                .Where(i => query.Equals(text.Substring(i, query.Length)));
        }
    }
}




