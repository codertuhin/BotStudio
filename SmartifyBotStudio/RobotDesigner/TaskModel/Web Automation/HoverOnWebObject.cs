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
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation
{


    [Serializable]
    public class HoverOnWebObject : RobotActionBase, ITask
    {

        public string WebDriverInstanceVar { get; set; }
        public int WebBrowserInstanceIndex { get; set; }
        public HtmlElementInfo htmlElementInfo { get; set; }
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
                else if (!string.IsNullOrEmpty(htmlElementInfo.Html_element_XPATH))
                {
                    element = working_driver.FindElement(By.XPath(htmlElementInfo.Html_element_XPATH));
                }

                else if (!string.IsNullOrEmpty(htmlElementInfo.Html_element_name))
                {
                    element = working_driver.FindElement(By.Name(htmlElementInfo.Html_element_name));
                }
               
                else
                {
                    if (htmlElementInfo.Html_element_class.Contains(" "))
                    {
                        string compoundClassName = htmlElementInfo.Html_element_class.Replace(' ', '.');
                        compoundClassName = "." + compoundClassName;
                        element = working_driver.FindElement(By.CssSelector(compoundClassName));
                    }
                    else
                    {
                        element = working_driver.FindElement(By.ClassName(htmlElementInfo.Html_element_class));
                    }

                }

                //WebDriverWait wait = new WebDriverWait(working_driver, TimeSpan.FromSeconds(10));


                Actions action = new Actions(working_driver);
                action.MoveToElement(element).Build().Perform();



                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }



}
