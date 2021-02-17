using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.Variable
{
    [Serializable]
    public class DataTableVariable : VariableBase
    {
        public DataTable Value { get; set; }
    }
}
