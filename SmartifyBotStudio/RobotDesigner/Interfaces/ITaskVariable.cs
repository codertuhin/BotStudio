using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.Interfaces
{
    public interface ITaskVariable
    {
        string VariableName { get; set; }
        Guid VariableID { get; set; }
    }
}
