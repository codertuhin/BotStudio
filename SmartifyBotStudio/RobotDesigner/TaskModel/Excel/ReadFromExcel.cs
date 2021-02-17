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
using System.Data;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Excel
{
    [Serializable]
    public class ReadFromExcel : RobotActionBase, ITask
    {
        public string ExcelStoreVar { get; set; }
        public string ExcelFileToReadVar { get; set; }

        public int RetrieveSchemaIndex = 4;
        public string RangeStartsatCol { get; set; }
        public int RangeStartsatRow { get; set; }
        public string RangeEndssatCol { get; set; }
        public int RangeEndssatRow { get; set; }
        public string SingleCol { get; set; }
        public int SingleRow { get; set; }
        public bool SaveOrDoNotSave { get; set; }


        public string Var_StoreDeletedFiles { get; set; }

        string starting_point;

        string ending_point;

        public int Execute()
        {
            try
            {

                if (RetrieveSchemaIndex == 0)
                {
                    starting_point = SingleCol + SingleRow.ToString();
                    ending_point = SingleCol + SingleRow.ToString();
                }
                if (RetrieveSchemaIndex == 1)
                {


                    starting_point = RangeStartsatCol + RangeStartsatRow.ToString();
                    ending_point = RangeEndssatCol + RangeEndssatRow.ToString();


                }




                MExcel.Worksheet excelSheet = VariableStorage.ExcelVar[ExcelFileToReadVar].Item2.ActiveWorkbook.ActiveSheet;

                MExcel.Range rng = (MExcel.Range)excelSheet.get_Range(starting_point, ending_point);

                Object[,] myvalues = (Object[,])rng.Cells.Value;



                DataTable dt = new DataTable();


                for (int dimension = 0; dimension < myvalues.Rank; dimension++)
                {
                    dt.Columns.Add("Column" + (dimension + 1));
                }


                for (int element = 0; element < (myvalues.Length / myvalues.Rank); element++)
                {
                    DataRow row = dt.NewRow();
                    for (int dimension = 0; dimension < myvalues.Rank; dimension++)
                    {

                        string str = "Column" + (dimension + 1).ToString();
                        row[str] = myvalues[element + 1, dimension + 1];

                    }
                    dt.Rows.Add(row);

                }

                if (VariableStorage.ExcelDataVar.ContainsKey(ExcelStoreVar))
                {
                    VariableStorage.ExcelDataVar.Remove(ExcelStoreVar);
                }
                VariableStorage.ExcelDataVar.Add(ExcelStoreVar, Tuple.Create(this.ID, dt));

                DataTable dtt = dt;



                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public DataTable ArraytoDatatable(Object[,] numbers)
        {
            DataTable dt = new DataTable();

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                // This array holds the row data
                Object[] data = new Object[numbers.GetLength(0)];
                for (int j = 0; j < numbers.GetLength(0); j++)
                {
                    // fill the row data
                    data[j] = numbers[i, j];
                }
                // Add the row to the DataTable
                dt.Rows.Add(data);
            }


            //for (int i = 0; i < numbers.GetLength(1); i++)
            //{
            //    dt.Columns.Add("Column" + (i + 1));
            //}

            //for (var i = 0; i < numbers.GetLength(0); ++i)
            //{
            //    DataRow row = dt.NewRow();
            //    for (var j = 0; j < numbers.GetLength(1); ++j)
            //    {
            //        row[j] = numbers[i, j];
            //    }
            //    dt.Rows.Add(row);
            //}
            return dt;
        }

    }
}
