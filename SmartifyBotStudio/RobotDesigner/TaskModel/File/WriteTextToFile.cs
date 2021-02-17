using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.File
{
    [Serializable]
    public class WriteTextToFile : RobotActionBase, ITask
    {
        public string FilePath { get; set; }
        public string TextToWrite { get; set; }
        public bool OverwriteIfExists { get; set; }
        public System.Text.Encoding Encoding { get; set; }

        public bool AppendNewLine { get; set; }

        public int Execute()
        {
            try
            {
                if (OverwriteIfExists)
                    if (AppendNewLine)
                        System.IO.File.AppendAllText(FilePath, TextToWrite, Encoding);
                    else
                        System.IO.File.WriteAllText(FilePath, TextToWrite, Encoding);

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}
