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
    public class GetSubText : RobotActionBase, ITask
    {
        public string OriginalText { get; set; }
        public int StartIndex { get; set; }
        public int StartIndexComboIndex = 1;
        public int LengthComboIndex = 1;
        public int Length { get; set; }
        public string SubTextStorVar { get; set; }
        public int Execute()
        {
            try
            {
                string subText = "";

                subText = OriginalText.Substring(StartIndex, Length);

                if (VariableStorage.SubTextVar.ContainsKey(SubTextStorVar))
                {
                    VariableStorage.SubTextVar.Remove(SubTextStorVar);
                }
                VariableStorage.SubTextVar.Add(SubTextStorVar, Tuple.Create(this.ID, subText));


                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}




