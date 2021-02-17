using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.IE;
using System.Collections.Generic;
using System;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation
{
    [Serializable]
    public static class WebDriverGenerator
    {
        //public static OpenQA.Selenium.Chrome.ChromeDriverService driverService = ChromeDriverService.CreateDefaultService();


        public static IWebDriver working_driver = driver();

        public static IWebDriver driver()

        {
            //var driverService = InternetExplorerDriverService.CreateDefaultService();
            //driverService.HideCommandPromptWindow = true;
            //var driver = new InternetExplorerDriver(driverService, new InternetExplorerOptions());
            //return driver;



            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("disable-infobars");
            
            var driver = new ChromeDriver(driverService, chromeOptions);

            return driver;





        }


        public static void GO_to_URL(string url)
        {
            
            IList<string> tabs = new List<string>(WebDriverGenerator.working_driver.WindowHandles);
            IJavaScriptExecutor js = (IJavaScriptExecutor)WebDriverGenerator.working_driver;
            
            if (tabs.Count==0)
            {
                
                WebDriverGenerator.working_driver.Navigate().GoToUrl(url);
                //js.ExecuteScript("window.open();");
            }
            else

            {

                
                WebDriverGenerator.working_driver.SwitchTo().Window(tabs[tabs.Count - 1]);
                WebDriverGenerator.working_driver.Navigate().GoToUrl(url);
                //js.ExecuteScript("window.open();");


               // WebDriverGenerator.working_driver.SwitchTo().Window(WebDriverGenerator.working_driver.WindowHandles);


            }
            
            
        }


    }
}
