using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.Variable
{
    [Serializable]
    public class ListVariable : VariableBase
    {
        public List<string> Value { get; set; }
    }
}
