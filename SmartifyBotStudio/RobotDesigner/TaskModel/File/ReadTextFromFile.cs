using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.File
{
    [Serializable]
    public class ReadTextFromFile : RobotActionBase, ITask
    {
        public string FilePath { get; set; }
        public StoreFileContentMode StoreFileContentAs { get; set; }
        public Encoding Encoding { get; set; }
        public string Var_StoreContentInto { get; set; }

        public int Execute()
        {
            try
            {
                string text = "";
                switch (StoreFileContentAs)
                {
                    case StoreFileContentMode.SingleTextValue:
                        text = System.IO.File.ReadAllText(FilePath, Encoding);
                        Variables.Add(new StringVariable() { RobotAction = this, Value = text, VariableName = Var_StoreContentInto });
                        break;
                    case StoreFileContentMode.List:
                        var textList = System.IO.File.ReadAllLines(FilePath, Encoding);
                        Variables.Add(new ListVariable() { RobotAction = this, Value = textList.ToList(), VariableName = Var_StoreContentInto });
                        break;
                }

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }



        }
    }


    public enum StoreFileContentMode
    {
        SingleTextValue,
        List,
    }
}
