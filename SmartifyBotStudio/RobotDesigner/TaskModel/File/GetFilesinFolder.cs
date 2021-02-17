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
    public class GetFilesinFolder : RobotActionBase, ITask
    {
        public string FolderPath { get; set; }
        public string FileFilter { get; set; }

        public bool IncludeSubfolder { get; set; }

        public string Var_StoreRetrievedFilesInto { get; set; }
        //public List<TaskVariable> Variables { get; set ; }

        public int Execute()
        {
            string[] files;

            if (IncludeSubfolder)
            
            files = Directory.GetFiles(FolderPath, FileFilter, SearchOption.AllDirectories);
            else
                files = Directory.GetFiles(FolderPath, FileFilter, SearchOption.TopDirectoryOnly);

           

            Variables.Add(new ListVariable()
            {
                Value = files.ToList(),
                VariableName = Var_StoreRetrievedFilesInto,
                RobotAction = this
            });


            return files.Count();
        }


        //public string Sortby { get; set; }
    }
}
