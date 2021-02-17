using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System.Collections.Generic;


namespace SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation
{
    [Serializable]
    public sealed class WebGeneratorSingleton               
    {


        private WebGeneratorSingleton()  {       }

        private static WebGeneratorSingleton instance = null;

        public static WebGeneratorSingleton Instance

        {

            get

            {

                if (instance == null)

                {

                    instance = new WebGeneratorSingleton();

                }

                return instance;

            }

        }



      

        private IWebDriver myVar;

        public IWebDriver working_driver
        {
            get { return myVar; }
            set { myVar = driver(); }
        }

               
        

        public IWebDriver driver()
        {

            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            var driver = new ChromeDriver(driverService, new ChromeOptions());

            return driver;

        }


    }
}
