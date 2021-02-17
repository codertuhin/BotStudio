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

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Excel
{
    [Serializable]
    public class AddNewWorkSheet : RobotActionBase, ITask
    {

        public string ExcelFileToAddWorksheetVar { get; set; }

        public int AddWorksheetSchemaIndex = 3;
        public string newWorksheetName { get; set; }
        public bool SaveOrDoNotSave { get; set; }


        public string Var_StoreDeletedFiles { get; set; }

        public int Execute()
        {
            try
            {

                MExcel.Application excel_application = VariableStorage.ExcelVar[ExcelFileToAddWorksheetVar].Item2;

                foreach (MExcel.Worksheet w in excel_application.ActiveWorkbook.Sheets)
                {
                    if (w.Name == newWorksheetName)
                    {
                        w.Delete();
                    }
                }

                if (AddWorksheetSchemaIndex == 0)
                {
                    var newSheet = (MExcel.Worksheet)excel_application.ActiveWorkbook.Sheets.Add(Before: excel_application.ActiveWorkbook.Sheets[1]);
                    newSheet.Name = newWorksheetName;



                }
                else if (AddWorksheetSchemaIndex == 1)
                {
                    var newSheet = (MExcel.Worksheet)excel_application.ActiveWorkbook.Sheets.Add(After: excel_application.ActiveWorkbook.Sheets[excel_application.ActiveWorkbook.Sheets.Count]);
                    newSheet.Name = newWorksheetName;
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
