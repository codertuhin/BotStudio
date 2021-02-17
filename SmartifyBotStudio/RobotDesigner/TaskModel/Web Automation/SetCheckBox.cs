using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
namespace SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation
{
    [Serializable]
    public class SetCheckBox : RobotActionBase, ITask
    {
        public string selenium_url { get; set; }

        //public string input_text { get; set; }
        public string selenium_title { get; set; }
        public string selenium_className { get; set; }
        public string selenium_innerText { get; set; }
        public int selenium_intex { get; set; }
        public string webDriverInstanceVar { get; set; }

        //public List<HtmlElementInfo> PopulateTextField_HtmlElemtnInfo_list { get; set; }
        public ObservableCollection<HtmlElementInfo> SetCheckBoxL_HtmlElemtnInfo_list { get; set; }

        public string Var_StoreDeletedFiles { get; set; }

        public int Execute()
        {
            try
            {

                var working_driver = VariableStorage.WebDriverVar[webDriverInstanceVar].Item2 as IWebDriver;


                System.Uri input_uri = new Uri(selenium_url);
                string input_uriWithoutScheme = input_uri.Host.Replace("www.", "") + input_uri.PathAndQuery + input_uri.Fragment;


                System.Uri base_uri = new Uri(working_driver.Url);
                string Base_uriWithoutScheme = base_uri.Host.Replace("www.", "") + base_uri.PathAndQuery + base_uri.Fragment;




                if (Base_uriWithoutScheme == input_uriWithoutScheme)
                {
                    IList<string> tabs = new List<string>(working_driver.WindowHandles);
                    working_driver.SwitchTo().Window(tabs[0]);

                }
                else
                {
                    working_driver.Navigate().GoToUrl(selenium_url);

                }




                //var dropdown = WebDriverGenerator.working_driver.FindElement(By.Id(SetDropDownList_HtmlElemtnInfo_list[i].Html_element_idv));





                IWebElement element;



                for (int i = 0; i < SetCheckBoxL_HtmlElemtnInfo_list.Count; i++)
                {


                    if (SetCheckBoxL_HtmlElemtnInfo_list[i].Html_element_id != "")
                    {
                        element = working_driver.FindElement(By.Id(SetCheckBoxL_HtmlElemtnInfo_list[i].Html_element_id));
                    }
                    else if (SetCheckBoxL_HtmlElemtnInfo_list[i].Html_element_XPATH != "")
                    {
                        element = working_driver.FindElement(By.XPath(SetCheckBoxL_HtmlElemtnInfo_list[i].Html_element_XPATH));
                    }

                    else
                    {
                        element = working_driver.FindElement(By.Name(SetCheckBoxL_HtmlElemtnInfo_list[i].Html_element_name));
                    }

                    //element.SendKeys(ClickOnWebPage_HtmlElemtnInfo_list[i].Html_element_input_text);

                    //var selectElement = new OpenQA.Selenium.Support.UI.SelectElement(element);
                    //selectElement.SelectByIndex(SetCheckBoxL_HtmlElemtnInfo_list[i].Html_drop_down_index);

                    element.Click();
                }

                //driver.Quit();
                // driver.FindElement(By.Id(selenium_id)).SendKeys(Keys.Enter);





                return 1;

            }
            catch (Exception)
            {

                return 0;
            }



            throw new NotImplementedException();
        }
    }
}
