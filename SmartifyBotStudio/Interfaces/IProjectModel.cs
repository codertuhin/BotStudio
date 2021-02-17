using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner;
using SmartifyBotStudio.RobotDesigner.TaskModel.File;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SmartifyBotStudio.Interfaces
{

  
    public interface IProjectModel
    {
        string ProjectName { get; set; }
        string Description { get; set; }
        string Owner { get; set; }
        string ProjectPath { get; set; }
     
        ObservableCollection<RobotActionBase> Actions { get; set; }
    }
}
