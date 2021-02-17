using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartifyBotStudio.Models;
using System.Xml.Serialization;
using System.IO;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.MessageBoxes
{
    [Serializable]
    public class DisplayMessage : RobotActionBase, ITask
    {
        public const uint MaxValue = 4294967295;
        public System.UInt32 Time = MaxValue;
        public bool CloseMsgAutomatically { get; set; }
        public VariableCollectionModel WorkingVariable { get; set; }

        public string Message = "";
        public string Title = "";
        public int ButtonIndex = 0;
        public int IconIndex = 0;
        public int DefaultButtonIndex = 0;


        public int Execute()
        {
            try
            {


                MessageBoxButtons msgButtons = (MessageBoxButtons)ButtonIndex;

                MessageBoxIcon msgIcon = (MessageBoxIcon)IconIndex;

                MessageBoxDefaultButton msgDefaultBtn = (MessageBoxDefaultButton)DefaultButtonIndex;

                List<VariableCollectionModel> AllVariableCollection = VariableStorage.GetAllVariables();

                if (WorkingVariable != null)
                {

                    if (WorkingVariable.Type == typeof(List<object>))
                    {
                        List<object> temp = VariableStorage.CreateNewListVar[WorkingVariable.VariableName].Item2;

                        Message = "";
                        foreach (var item in temp)
                        {
                            Message += item.ToString() + " ";
                        }
                    }
                    else
                    {
                        foreach (VariableCollectionModel item in AllVariableCollection)
                        {
                            if (item.VariableName == WorkingVariable.VariableName)
                            {

                                Message = item.ObjectValue.ToString();
                            }
                        }
                    }

                }


                if (CloseMsgAutomatically)
                {
                    MessageBoxEx.Show(Message, Title, msgButtons, msgIcon, msgDefaultBtn, Time * 1000);
                }
                else
                {
                    MessageBoxEx.Show(Message, Title, msgButtons, msgIcon, msgDefaultBtn, MaxValue);
                }





                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }


}
