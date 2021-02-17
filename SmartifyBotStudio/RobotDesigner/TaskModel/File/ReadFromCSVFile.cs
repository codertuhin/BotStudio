using SmartifyBotStudio.Helpers.Misc;
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
    public class ReadFromCSVFile : RobotActionBase, ITask
    {
        public string FilePath { get; set; }
        public Encoding Encoding { get; set; }
        public string Var_StoreTableInto { get; set; }

        public int Execute()
        {
            try
            {
                var data = CSVReader.ReadCsvFile(FilePath);

                if (VariableStorage.CSVVar.ContainsKey(Var_StoreTableInto))
                {
                    VariableStorage.CSVVar.Remove(Var_StoreTableInto);
                }

                VariableStorage.CSVVar.Add(Var_StoreTableInto, Tuple.Create(ID, data));

                Variables.Add(new DataTableVariable()
                {
                    RobotAction = this,
                    Value = data,
                    VariableName = Var_StoreTableInto
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
