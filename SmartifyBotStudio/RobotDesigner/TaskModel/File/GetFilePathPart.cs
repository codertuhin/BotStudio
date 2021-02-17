using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.File
{
    [Serializable]
    public class GetFilePathPart : RobotActionBase, ITask
    {
        public string FilePath { get; set; }
        public string Var_RootPath { get; set; }
        public string Var_Directory { get; set; }
        public string Var_FileName { get; set; }
        public string Var_FileNameWithoutExtension { get; set; }
        public string Var_Extension { get; set; }
 

        public int Execute()
        {
            try
            {
                FileInfo fileInfo = new FileInfo(FilePath);
                Variables.Add(new StringVariable() { VariableName = Var_Extension, Value = fileInfo.Extension });
                Variables.Add(new StringVariable() { VariableName = Var_Directory, Value = fileInfo.DirectoryName });
                Variables.Add(new StringVariable() { VariableName = Var_RootPath, Value = fileInfo.Directory.Root.FullName });
                Variables.Add(new StringVariable() { VariableName = Var_FileName, Value = fileInfo.Name });
                Variables.Add(new StringVariable() { VariableName = Var_FileNameWithoutExtension, Value = fileInfo.Name.Replace("." + fileInfo.Extension, "") });

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }

        }
    }
}
