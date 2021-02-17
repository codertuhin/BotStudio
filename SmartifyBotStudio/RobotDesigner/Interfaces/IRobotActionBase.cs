using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.Interfaces
{
    public interface IRobotActionBase
    {
        string Name { get; set; }
        string Description { get; set; }
        string ID { get; set; }
        bool Finished { get; set; }
        bool IsExisting { get; set; }

        PackIconKind  Icon { get; set; }
    }
}
