using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Text
{
    [Serializable]
    public class ChangeTextCase : RobotActionBase, ITask
    {
        public string TextToConvert { get; set; }
        public int ConvertIntoCaseComboIndex { get; set; }
        public string ConveredTextStoreVar { get; set; }


       

        public int Execute()
        {
            try
            {
                string convertedText = TextToConvert.ToLower();

                if (ConvertIntoCaseComboIndex == 0)
                {
                    convertedText = TextToConvert.ToUpper();
                }
               
                else if (ConvertIntoCaseComboIndex == 2)
                {

                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                                        
                    convertedText = textInfo.ToTitleCase(convertedText);


                }
                else if (ConvertIntoCaseComboIndex == 3)    // For Sentence Case
                {
                    
                    // matches the first sentence of a string, as well as subsequent sentences
                    var regx = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);

                    // MatchEvaluator delegate defines replacement of setence starts to uppercase
                    convertedText = regx.Replace(convertedText, s => s.Value.ToUpper());


                }

                if(VariableStorage.CaseConvetedTextVar.ContainsKey(ConveredTextStoreVar))
                {
                    VariableStorage.CaseConvetedTextVar.Remove(ConveredTextStoreVar);
                }

                VariableStorage.CaseConvetedTextVar.Add(ConveredTextStoreVar, Tuple.Create(this.ID, convertedText));


                //if (testVar.TestVariable.ContainsKey(ConveredTextStoreVar))
                //{
                //    testVar.TestVariable.Remove(ConveredTextStoreVar);
                //}

                ////Application.Current.Resources["something"] = something;

                //testVar.TestVariable.Add(ConveredTextStoreVar, convertedText);


              


                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}




