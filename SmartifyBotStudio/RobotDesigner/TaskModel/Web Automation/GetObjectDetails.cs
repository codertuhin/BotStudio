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
    public class GetObjectDetails : RobotActionBase, ITask
    {
        public HtmlElementInfo htmlElementInfo;
        public int WebBrowserInstanceIndex = 0;
        public string WebDriverInstanceVar { get; set; }
        public string WebElementDetailsStoreVar { get; set; }
        public string Address { get; set; }
        public string Control { get; set; }

        public int Execute()
        {
            try
            {
                var working_driver = VariableStorage.WebDriverVar[WebDriverInstanceVar].Item2 as IWebDriver;

                IWebElement element = null;

                if (!string.IsNullOrEmpty(htmlElementInfo.Html_element_id))
                {
                    element = working_driver.FindElement(By.Id(htmlElementInfo.Html_element_id));
                }

                else if (!string.IsNullOrEmpty(htmlElementInfo.Html_element_name))
                {
                    element = working_driver.FindElement(By.Name(htmlElementInfo.Html_element_name));
                }
                else if (!string.IsNullOrEmpty(htmlElementInfo.Html_element_XPATH))
                {
                    element = working_driver.FindElement(By.XPath(htmlElementInfo.Html_element_XPATH));
                }
                else
                {
                    element = working_driver.FindElement(By.ClassName(htmlElementInfo.Html_element_class));
                }

                string elementDetails = element.Text;

                if (VariableStorage.WebElementDetailsVar.ContainsKey(WebElementDetailsStoreVar))
                {
                    VariableStorage.WebElementDetailsVar.Remove(WebElementDetailsStoreVar);
                }
                VariableStorage.WebElementDetailsVar.Add(WebElementDetailsStoreVar, Tuple.Create(this.ID, elementDetails));

                //System.Windows.Forms.MessageBox.Show(elementDetails);


                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }



}
