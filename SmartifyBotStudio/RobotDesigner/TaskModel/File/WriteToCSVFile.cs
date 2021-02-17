using SmartifyBotStudio.Helpers.Misc;
using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.File
{
    [Serializable]
    public class WriteToCSVFile : RobotActionBase, ITask
    {
        public DataTableVariable VariableToWrite { get; set; }
        public string FilePath { get; set; }
        public Encoding Encoding { get; set; }

        public VariableCollectionModel WorkingVariable { get; set; }
        public int EncodingIndex { get; set; }

        public int Execute()
        {
            try
            {
                if (EncodingIndex == 0)
                {
                    CSVWriter.WriteCSV((System.Data.DataTable)WorkingVariable.ObjectValue, FilePath, Encoding.UTF8);
                }
                if (EncodingIndex == 1)
                {
                    CSVWriter.WriteCSV((System.Data.DataTable)WorkingVariable.ObjectValue, FilePath, Encoding.Unicode);
                }
                if (EncodingIndex == 2)
                {
                    CSVWriter.WriteCSV((System.Data.DataTable)WorkingVariable.ObjectValue, FilePath, Encoding.BigEndianUnicode);
                }
                if (EncodingIndex == 3)
                {
                    CSVWriter.WriteCSV((System.Data.DataTable)WorkingVariable.ObjectValue, FilePath, Encoding);
                }
                if (EncodingIndex == 4)
                {
                    CSVWriter.WriteCSV((System.Data.DataTable)WorkingVariable.ObjectValue, FilePath, Encoding);
                }
                if (EncodingIndex == 5)
                {
                    CSVWriter.WriteCSV((System.Data.DataTable)WorkingVariable.ObjectValue, FilePath, Encoding.Default);
                }
                if (EncodingIndex == 5)
                {
                    CSVWriter.WriteCSV((System.Data.DataTable)WorkingVariable.ObjectValue, FilePath, Encoding.ASCII);
                }


                return 1;
            }
            catch (Exception)
            {

                return 1;
            }
        }
    }
}
