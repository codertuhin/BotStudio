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
    public class WriteToExcel : RobotActionBase, ITask
    {

        public string ExcelFileToWriteVar { get; set; }
        public int ExcelInstanceComboIndex = 0;
        public int WriteMoodIndex = 3;
        public string Cell { get; set; }
        public int CellInt { get; set; }
        public bool IsCellNumeric { get; set; }
        public int Row { get; set; }

        public string TextToWrite { get; set; }

        public bool SaveOrDoNotSave { get; set; }


        public string Var_StoreDeletedFiles { get; set; }

        public int Execute()
        {

            //char[] alphabet = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            //                          'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            //string input = "AAA";
            //input = input.ToUpper();

            //char[] inputChararray = input.ToArray();

            //int index;
            //double cellnumber = 0;
            //for (int i = 0; i < input.Length; i++)
            //{

            //    index = Array.IndexOf(alphabet, input[i])+1;
            //    cellnumber +=Math.Pow(26,i)*index;
            //}




            try
            {
                if (WriteMoodIndex == 0)
                {
                    MExcel.Worksheet worksheet = VariableStorage.ExcelVar[ExcelFileToWriteVar].Item2.ActiveWorkbook.ActiveSheet;
                    worksheet.Cells[Row, Cell] = TextToWrite;

                    VariableStorage.ExcelVar[ExcelFileToWriteVar].Item2.ActiveWorkbook.Save();
                }
                else if (WriteMoodIndex == 1)
                {
                    VariableStorage.ExcelVar[ExcelFileToWriteVar].Item2.ActiveCell.Value = TextToWrite;
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
