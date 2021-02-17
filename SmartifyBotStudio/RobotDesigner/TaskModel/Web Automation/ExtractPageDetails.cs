using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartifyBotStudio.Models;
using System.Xml.Serialization;
using System.IO;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System.ComponentModel;
using OpenQA.Selenium;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation
{


    [Serializable]
    public class ExtractPageDetails : RobotActionBase, ITask
    {
        public string WebDriverInstanceVar { get; set; }
        public int WebDriverInstanceComboIndex = 0;
        public int OperatinIndex = 0;
        public string PropertyStoreVar { get; set; }

        public int Execute()
        {
            try
            {

                var working_driver = VariableStorage.WebDriverVar[WebDriverInstanceVar].Item2 as IWebDriver;

                string details = "";


                if (OperatinIndex == 0)
                {
                    //head > meta:nth-child(11)  /html/head/meta[3]
                    details = details = working_driver.FindElement(By.XPath ("/html/head/meta[@name='description']")).GetAttribute("content") ;
                    //details = working_driver.FindElement(By.Name("description")).Text; 
                }
                else if (OperatinIndex == 1)
                {
                    // details = details = working_driver.FindElement(By.TagName("meta")).GetAttribute("keywords");
                    details = details = working_driver.FindElement(By.XPath("/html/head/meta[@name='keywords']")).GetAttribute("content");
                }
                else if (OperatinIndex == 2)
                {
                    details = working_driver.Title;
                }
                else if (OperatinIndex == 3)
                {
                    details = working_driver.FindElement(By.TagName("body")).Text;
                }
                else if (OperatinIndex == 4)
                {
                    details = working_driver.PageSource;
                }
                else if (OperatinIndex == 5)
                {
                    details = working_driver.Url;
                }

                // System.Windows.Forms.MessageBox.Show(details);

                if (VariableStorage.WebDetailsVar.ContainsKey(PropertyStoreVar))
                {
                    VariableStorage.WebDetailsVar.Remove(PropertyStoreVar);
                }
                VariableStorage.WebDetailsVar.Add(PropertyStoreVar, Tuple.Create(this.ID, details));

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }



}
