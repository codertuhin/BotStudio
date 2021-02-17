using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.Variable
{
    [Serializable]
    public class IntegerVariable : VariableBase
    {
        public int Value { get; set; }
    }
}
