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
using System.Drawing;

using SHDocVw;
using OpenQA.Selenium.Remote;
using mshtml;
using System.Threading;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation
{
    [Serializable]
    public class LaunchNewIE : RobotActionBase, ITask
    {
        public string Operation { get; set; }
        public string URL_ABrowser { get; set; }
        public string URL_New_IEBrowser { get; set; }
        public string URL_Attach_IEBrowser { get; set; }
        public int Window_state { get; set; }
        public string webDriverInstanceVar { get; set; }

        //WebDriverGenerator driver = new WebDriverGenerator();

        private enum BrowserNavConstants
        {
            /// <summary>
            /// Open the resource or file in a new window.
            /// </summary>
            navOpenInNewWindow = 0x1,

            /// <summary>
            /// Do not add the resource or file to the history list. The new page replaces the current page in the list.
            /// </summary>
            navNoHistory = 0x2,

            /// <summary>
            /// Do not consult the Internet cache; retrieve the resource from the origin server (implies BINDF_PRAGMA_NO_CACHE and BINDF_RESYNCHRONIZE).
            /// </summary>
            navNoReadFromCache = 0x4,

            /// <summary>
            /// Do not add the downloaded resource to the Internet cache. See BINDF_NOWRITECACHE.
            /// </summary>
            navNoWriteToCache = 0x8,

            /// <summary>
            /// If the navigation fails, the autosearch functionality attempts to navigate common root domains (.com, .edu, and so on). If this also fails, the URL is passed to a search engine.
            /// </summary>
            navAllowAutosearch = 0x10,

            /// <summary>
            /// Causes the current Explorer Bar to navigate to the given item, if possible.
            /// </summary>
            navBrowserBar = 0x20,

            /// <summary>
            /// Microsoft Internet Explorer 6 for Microsoft Windows XP Service Pack 2 (SP2) and later. If the navigation fails when a hyperlink is being followed, this constant specifies that the resource should then be bound to the moniker using the BINDF_HYPERLINK flag.
            /// </summary>
            navHyperlink = 0x40,

            /// <summary>
            /// Internet Explorer 6 for Windows XP SP2 and later. Force the URL into the restricted zone.
            /// </summary>
            navEnforceRestricted = 0x80,

            /// <summary>
            /// Internet Explorer 6 for Windows XP SP2 and later. Use the default Popup Manager to block pop-up windows.
            /// </summary>
            navNewWindowsManaged = 0x0100,

            /// <summary>
            /// Internet Explorer 6 for Windows XP SP2 and later. Block files that normally trigger a file download dialog box.
            /// </summary>
            navUntrustedForDownload = 0x0200,

            /// <summary>
            /// Internet Explorer 6 for Windows XP SP2 and later. Prompt for the installation of Microsoft ActiveX controls.
            /// </summary>
            navTrustedForActiveX = 0x0400,

            /// <summary>
            /// Windows Internet Explorer 7. Open the resource or file in a new tab. Allow the destination window to come to the foreground, if necessary.
            /// </summary>
            navOpenInNewTab = 0x0800,

            /// <summary>
            /// Internet Explorer 7. Open the resource or file in a new background tab; the currently active window and/or tab remains open on top.
            /// </summary>
            navOpenInBackgroundTab = 0x1000,

            /// <summary>
            /// Internet Explorer 7. Maintain state for dynamic navigation based on the filter string entered in the search band text box (wordwheel). Restore the wordwheel text when the navigation completes.
            /// </summary>
            navKeepWordWheelText = 0x2000
        }

        private static object m = Type.Missing;

        public int Execute()
        {
            try
            {
                #region Test



                //// Create the Url
                //string Url = "http://www.thepicketts.org";

                //// Get all (if any) InternetExplorer instances
                //var shellWindows = new SHDocVw.ShellWindows();


                //object o = null;
                //SHDocVw.InternetExplorer ie5 =  openNewInternetExplorerInstance(Url);
                //SHDocVw.WebBrowser wb = (SHDocVw.WebBrowser)ie5;
                //wb.Visible = true;
                ////Do anything else with the window here that you wish
                //wb.Navigate("https://adwords.google.co.uk/um/Logout", ref o, ref o, ref o, ref o);
                //while (wb.Busy) { Thread.Sleep(100); }
                //HTMLDocument document = ((HTMLDocument)wb.Document);
                ////HtmlDocument resultat = new HtmlDocument();
                ////IHTMLElement element = document.docu ("Email");
                //// If there were any found
                //if (shellWindows.Count > 0)
                //{
                //    // Get the first InternetExplorer instance found
                //    InternetExplorer ie = shellWindows.Item(0);
                //    InternetExplorer ie2 = shellWindows.Item(0) ;

                //    // Open a new tab in the located InternetExplorer and navigate to the URL
                //    openNewInternetExplorerTab(ref ie, Url);
                //}
                //else
                //{
                //    // Open a new InternetExplorer instance and navigate to the URL
                //    InternetExplorer ie = openNewInternetExplorerInstance(Url);
                //}
                #endregion
                #region Selenium


                ///////////////////////************************* Selenium Driver ********************************

                var driverService = ChromeDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;
                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments("disable-infobars");
                // chromeOptions.AddArguments("--start-maximized");

                IWebDriver driver = new ChromeDriver(driverService, chromeOptions);

               // DesiredCapabilities capabilities = DesiredCapabilities.Chrome();

               

                if (Window_state == 1)
                {
                    driver.Manage().Window.Maximize();
                }
                if (Window_state == 2)
                {
                    // driver.Manage().Window.Position = new Point(0, -1000);
                }



                VariableStorage.WebDriverVar.Remove(webDriverInstanceVar);
                VariableStorage.WebDriverVar.Add(webDriverInstanceVar, Tuple.Create(this.ID, (object)driver));


                string url = URL_New_IEBrowser;
                Uri uri;

                if (URL_New_IEBrowser == null)
                {
                    //WebDriverGenerator.working_driver.Navigate().GoToUrl("http://google.com");
                    (VariableStorage.WebDriverVar[webDriverInstanceVar].Item2 as IWebDriver).Navigate().GoToUrl("http://google.com");
                }
                else if ((Uri.TryCreate(url, UriKind.Absolute, out uri) || Uri.TryCreate("http://" + url, UriKind.Absolute, out uri) || Uri.TryCreate("https://" + url, UriKind.Absolute, out uri)) &&
                    (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
                {
                    //WebDriverGenerator.working_driver.Navigate().GoToUrl(uri);
                    (VariableStorage.WebDriverVar[webDriverInstanceVar].Item2 as IWebDriver).Navigate().GoToUrl(uri);
                }


                ///////////////////////************************* Selenium Driver ********************************
                #endregion


                return 1;


            }
            catch (Exception ex)
            {

                return 0;
            }




        }



        public static void openNewInternetExplorerTab(ref InternetExplorer ie, string url)
        {
            // Open the URL in a new tab of the given InternetExplorer instance
            ie.Navigate2(url, BrowserNavConstants.navOpenInNewTab, ref m, ref m, ref m);

            // Make InternetExplorer visible
            ie.Visible = true;
        }


        public static InternetExplorer openNewInternetExplorerInstance(string url)
        {
            // Create an InternetExplorer instance
            InternetExplorer ie = new InternetExplorer();

            // Open the URL
            ie.Navigate2(url, ref m, ref m, ref m, ref m);

            // Make InternetExplorer visible
            ie.Visible = true;

            return ie;
        }


    }
}
