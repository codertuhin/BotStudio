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
    public class SetActiveWorkSheet : RobotActionBase, ITask
    {

        public string ExcelFileToSetActiveSheetVar { get; set; }

        public int ExcelSheetActivateIndex = 3;


        public int ByIndex { get; set; }
        public string ByName { get; set; }


        public string Var_StoreDeletedFiles { get; set; }

        public int Execute()
        {
            try
            {

                if (ExcelSheetActivateIndex == 0)
                {

                    MExcel.Worksheet xlWorkSheetFocus = (MExcel.Worksheet)VariableStorage.ExcelVar[ExcelFileToSetActiveSheetVar].Item2.ActiveWorkbook.Sheets[ByName];
                    xlWorkSheetFocus.Activate();

                }
                else if (ExcelSheetActivateIndex == 1)
                {
                    MExcel.Worksheet xlWorkSheetFocus = (MExcel.Worksheet)VariableStorage.ExcelVar[ExcelFileToSetActiveSheetVar].Item2.ActiveWorkbook.Sheets[ByIndex];
                    xlWorkSheetFocus.Activate();
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
