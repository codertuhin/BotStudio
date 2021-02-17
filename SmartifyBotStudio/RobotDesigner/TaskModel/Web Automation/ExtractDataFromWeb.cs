using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation
{
    [Serializable]
    public class ExtractDataFromWeb : RobotActionBase, ITask
    {

        public int SaveAsIndex { get; set; }

        public string Var_StoreDeletedFiles { get; set; }

        public List<string> DataList;

        public List<string> HtmlList;

        public List<string> WebData;
        public string ExcelFileName { get; set; }
        public string Path { get; set; }
        public bool Data { get; set; }
        public bool Html { get; set; }
        public string Address { get; set; }

        public int DataNumber { get; set; }

        public int HtmlNumber { get; set; }




        public int Execute()
        {
            try
            {
               

                if (Data)
                    WebData = DataList;
                else
                    WebData = HtmlList;



                if (SaveAsIndex == 0)
                {




                    ExcelPackage ExcelPkg = new ExcelPackage();
                    ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                    for (int i = 0, j = 0; i < WebData.Count; i++)
                    {
                        //wsSheet1.Cells[i + 1, 1].Value = Attribute[i] + " = " + Value[i];
                        //if(WebData[i]!=null || WebData[i] !="")
                        wsSheet1.Cells[i + 1, 1].Value = WebData[i];


                    }

                    wsSheet1.Protection.IsProtected = false;
                    wsSheet1.Protection.AllowSelectLockedCells = false;
                    // ExcelPkg.SaveAs(new FileInfo(@"D:\" + ExcelFileName + ".xlsx"));

                    ExcelPkg.SaveAs(new FileInfo(Path + ExcelFileName + ".xlsx"));
                }
                else
                {
                    string textFileName = Path + ExcelFileName + ".txt";

                    System.IO.File.WriteAllLines(textFileName, WebData);

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
