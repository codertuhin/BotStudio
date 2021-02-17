using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MExcel = Microsoft.Office.Interop.Excel;
using System.Reflection;

using Microsoft.Office.Core;

using System.Runtime.InteropServices;

using OfficeOpenXml;
using Microsoft.Office.Interop.Excel;
using System.ComponentModel;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Excel
{
    
    [Serializable]
    //[ComVisibleAttribute(true)]
    public class LaunchExcel : RobotActionBase, ITask // Microsoft.Office.Interop.Excel
    {

        public string ExcelFileName { get; set; }

        public int ExcelSchemaIndex = 3;
        public string ExcelVar { get; set; }
        public bool MakeVisible { get; set; }


        public string Var_StoreDeletedFiles { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public int Execute()
        {

            MExcel.Application excel_application;
            MExcel.Workbook excel_workBook;
            MExcel._Worksheet excel_workSheet;
            MExcel.Range oRng;


            try
            {

                if (ExcelSchemaIndex == 0)
                {


                    excel_workBook = VariableStorage.ExcelVar[ExcelVar].Item2.Workbooks.Add(MExcel.XlWBATemplate.xlWBATWorksheet);
                    excel_workSheet = (Worksheet)excel_workBook.Worksheets[1];


                    excel_workBook.SaveAs(ExcelFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                                                                              false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                                                                                     Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                    VariableStorage.ExcelVar[ExcelVar].Item2.Visible = MakeVisible;
                    //excel_workBook.Close();

                }
                else if (ExcelSchemaIndex == 1)
                {

                    excel_workBook = VariableStorage.ExcelVar[ExcelVar].Item2.Workbooks.Open(ExcelFileName);
                    VariableStorage.ExcelVar[ExcelVar].Item2.Visible = MakeVisible;

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
