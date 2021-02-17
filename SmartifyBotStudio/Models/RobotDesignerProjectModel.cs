using SmartifyBotStudio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartifyBotStudio.RobotDesigner;
using SmartifyBotStudio.RobotDesigner.Variable;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using SmartifyBotStudio.RobotDesigner.TaskModel.File;

namespace SmartifyBotStudio.Models
{
    [Serializable]
    [XmlInclude(typeof(CopyFiles))]
    [XmlInclude(typeof(DeleteFiles))]
    [XmlInclude(typeof(MoveFiles))]
    [XmlInclude(typeof(RenameFiles))]
    [XmlInclude(typeof(WriteTextToFile))]
    [XmlInclude(typeof(ReadTextFromFile))]
    [XmlInclude(typeof(WriteToCSVFile))]
    [XmlInclude(typeof(ReadFromCSVFile))]
    [XmlInclude(typeof(GetFilePathPart))]
    [XmlInclude(typeof(GetFilesinFolder))]
    [XmlInclude(typeof(ListVariable))]

    public class RobotDesignerProjectModel : IProjectModel
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public string ProjectPath { get; set; }

        
        public ObservableCollection<RobotActionBase> Actions { get; set; }
    }
}
