using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Flow_Control
{
    [Serializable]
    public class Label_ : RobotActionBase, ITask
    {
        public string Label_Name { get; set; }
        public int Execute()
        {
            try
            {
                LabelName = Label_Name;


                if (VariableStorage.LabelVar.ContainsKey(Label_Name))
                {
                    VariableStorage.LabelVar.Remove(Label_Name);
                }
                VariableStorage.LabelVar.Add(Label_Name, Tuple.Create(ID, Label_Name));


                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}
