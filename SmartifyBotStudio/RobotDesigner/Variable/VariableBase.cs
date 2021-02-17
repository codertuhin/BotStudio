using SmartifyBotStudio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SmartifyBotStudio.RobotDesigner.Variable
{
    [Serializable]
    public class VariableBase
    {
        public string VariableName { get; set; }

        string _VariableID;
        public string VariableID
        {
            get
            {
                return _VariableID;
            }
            set
            {
                _VariableID = Guid.NewGuid().ToString();
            }
        }


        [NonSerialized]
        RobotActionBase _robotActionBase;
        public RobotActionBase RobotAction
        {
            get { return _robotActionBase; }
            set { _robotActionBase = value; }
        }

    }
}
