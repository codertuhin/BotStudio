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
    public class MoveFiles : RobotActionBase, ITask
    {
        public List<SelectedFileInfo> FilesToMove { get; set; }
        public string DestinationFolder { get; set; }
        public bool OverWriteIfExists { get; set; }
        public string Var_StoreMovedFilesInto { get; set; }

        public int Execute()
        {
            try
            {
                List<string> values = new List<string>();
                foreach (var file in FilesToMove)
                {
                    var path1 = file.FilePath;
                    var path1IsExists = System.IO.File.Exists(path1);
                    if (path1IsExists == true)
                    {
                        var destinationpath = Path.Combine(DestinationFolder, file.FileName);
                        var isExists = System.IO.File.Exists(destinationpath);
                        if (isExists)
                        {

                            if (OverWriteIfExists == true)
                            {
                                System.IO.File.Copy(file.FilePath, destinationpath, OverWriteIfExists);

                                System.IO.File.Delete(destinationpath);

                            }
                            else
                            {
                                continue;
                            }
                        }
                        System.IO.File.Move(file.FilePath, Path.Combine(DestinationFolder, file.FileName));

                        values.Add(file.FilePath);
                    }
                    else
                    {
                        continue;
                    }

                }

                Variables.Add(new ListVariable()
                {
                    RobotAction = this,
                    Value = values,
                    VariableName = Var_StoreMovedFilesInto
                });

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
