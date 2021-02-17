using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.Models;
using System.Diagnostics;
using SmartifyBotStudio.RobotDesigner.Variable;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using System.Windows.Forms;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation
{
    [Serializable]
    public class CloseIE : RobotActionBase, ITask
    {

        public string webDriverInstanceVar { get; set; }

        public bool flag = false;
        public int dropDownIndex { get; set; }

        public int Execute()
        {
            try
            {

                (VariableStorage.WebDriverVar[webDriverInstanceVar].Item2 as IWebDriver).Quit();



                return 1;


            }
            catch (Exception)
            {

                return 0;
            }

        }
    }
}
