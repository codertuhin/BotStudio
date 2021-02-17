using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.Variable
{
    [Serializable]
    public class VariableCollectionModel
    {
        public string VariableName { get; set; }
        public string ActionID { get; set; }
        public object ObjectValue { get; set; }
        public Type Type { get; set; }
        public string TypeString { get; set; }
        public dynamic AdditionalInfo_1 { get; set; }
        public dynamic AdditionalInfo_2 { get; set; }
    }
}
