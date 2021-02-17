using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartifyBotStudio.RobotDesigner.Variable;
using Microsoft.Office.Core;
using MExcel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

using OfficeOpenXml;
using System.Windows;
using System.Reflection;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Excel
{
    [Serializable]
    public class CloseExcel : RobotActionBase, ITask
    {

        public string ExcelFileToCloseVar { get; set; }

        public int ExcelClosingSchemaIndex = 3;
        public int ExcelInstanceComboIndex = 0;
        public string newFileName { get; set; }
        public int newFileNameExtIndex { get; set; }


        public string Var_StoreDeletedFiles { get; set; }

        public int Execute()
        {
            try
            {
                if (ExcelClosingSchemaIndex == 0)
                {
                    VariableStorage.ExcelVar[ExcelFileToCloseVar].Item2.ActiveWorkbook.Close(false, Missing.Value, Missing.Value);
                }
                else if (ExcelClosingSchemaIndex == 1)
                {
                    VariableStorage.ExcelVar[ExcelFileToCloseVar].Item2.ActiveWorkbook.Save();
                    VariableStorage.ExcelVar[ExcelFileToCloseVar].Item2.ActiveWorkbook.Close();
                }
                else if (ExcelClosingSchemaIndex == 2)
                {
                    if (newFileNameExtIndex == 0)
                    {

                    }
                    VariableStorage.ExcelVar[ExcelFileToCloseVar].Item2.ActiveWorkbook.SaveAs(newFileName);
                    VariableStorage.ExcelVar[ExcelFileToCloseVar].Item2.ActiveWorkbook.Close();
                }




                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
