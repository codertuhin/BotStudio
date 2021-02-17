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

namespace SmartifyBotStudio.RobotDesigner.TaskModel.File
{

   
    [Serializable]
    public class CopyFiles:RobotActionBase, ITask
    {
        public List<SelectedFileInfo> FilesToCopy { get; set; }
        
        public string Destination { get; set; }
        [DisplayName("Overwrite if exists")]
        public bool OverWriteIfFilesExixts { get; set; }
       
        public string Var_StoreCopiedFiles { get; set; }

        
        public int Execute()
        {
            try
            {
                List<string> values = new List<string>();

                foreach (var file in FilesToCopy)
                {
                    System.IO.File.Copy(file.FilePath, Path.Combine(Destination, file.FileName), OverWriteIfFilesExixts);

                    values.Add(file.FilePath);
                }

                Variables.Add(new ListVariable()
                {
                    RobotAction = this,
                    Value = values,
                    VariableName = Var_StoreCopiedFiles
                });

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }


    [Serializable]
    public class SelectedFileInfo
    {
        public string FileInfo { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileNameWithoutExtension { get; set; }
        public string FileExtention { get; set; }

        
    }
}
