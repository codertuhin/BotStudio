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
    public class TrimText : RobotActionBase, ITask
    {
        public string TrimToText { get; set; }
        public int WhatToTrimComboIndex { get; set; }
        public string TrimmedTextStoreVar { get; set; }
        public int Execute()
        {
            string trimmedText = "";

            try
            {
                if (WhatToTrimComboIndex == 0)
                {
                    trimmedText = TrimToText.TrimStart();
                }
                else if (WhatToTrimComboIndex == 1)
                {
                    trimmedText = TrimToText.TrimEnd();
                }
                else if (WhatToTrimComboIndex == 2)
                {
                    trimmedText = TrimToText.Trim();
                }

                if (VariableStorage.TrimmedTextVar.ContainsKey(TrimmedTextStoreVar))
                {
                    VariableStorage.TrimmedTextVar.Remove(TrimmedTextStoreVar);
                }
                VariableStorage.TrimmedTextVar.Add(TrimmedTextStoreVar, Tuple.Create(this.ID, trimmedText));

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}




