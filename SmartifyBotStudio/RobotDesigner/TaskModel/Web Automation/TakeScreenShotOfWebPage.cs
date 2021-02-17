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
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation
{



    [Serializable]
    public class TakeScreenShotOfWebPage : RobotActionBase, ITask
    {
        public string WebDriverInstanceVar { get; set; }
        public int WebDriverInstanceComboIndex = 0;
        public string URl { get; set; }
        public int OperationIndex { get; set; }
        public int SaveImageToIndex { get; set; }
        public int ImgFormatIndex = 0;
        public string ImgFileName { get; set; }
        public HtmlElementInfo htmlElementInfo;

        Screenshot fullPageScreenshot;
        Bitmap CloneFile;
        public int Execute()
        {
            try
            {
                var working_driver = VariableStorage.WebDriverVar[WebDriverInstanceVar].Item2 as IWebDriver;

                working_driver.Manage().Window.Maximize();

                if (OperationIndex == 0)
                {
                    fullPageScreenshot = ((ITakesScreenshot)working_driver).GetScreenshot();
                    if (SaveImageToIndex == 0)
                    {
                        //clipboard
                        SaveImage(ImgFileName, OperationIndex);
                        Clipboard.SetDataObject(fullPageScreenshot);
                        System.IO.File.Delete(ImgFileName);
                    }
                    else if (SaveImageToIndex == 1)
                    {
                        SaveImage(ImgFileName, OperationIndex);
                    }


                }
                else if (OperationIndex == 1)
                {
                    IWebElement ElementToCapture;
                    if (!string.IsNullOrEmpty(htmlElementInfo.Html_element_id))
                    {
                        ElementToCapture = working_driver.FindElement(By.Id(htmlElementInfo.Html_element_id));
                    }
                    else if (!string.IsNullOrEmpty(htmlElementInfo.Html_element_XPATH))
                    {
                        ElementToCapture = working_driver.FindElement(By.XPath(htmlElementInfo.Html_element_XPATH));
                    }
                    else if (!string.IsNullOrEmpty(htmlElementInfo.Html_element_name))
                    {
                        ElementToCapture = working_driver.FindElement(By.Name(htmlElementInfo.Html_element_name));
                    }
                    else //if (!string.IsNullOrEmpty(htmlElementInfo.Html_element_class))
                    {

                        if (htmlElementInfo.Html_element_class.Contains(" "))
                        {
                            string compoundClassName = htmlElementInfo.Html_element_class.Replace(' ', '.');
                            compoundClassName = "." + compoundClassName;
                            ElementToCapture = working_driver.FindElement(By.CssSelector(compoundClassName));
                        }
                        else
                        {
                            ElementToCapture = working_driver.FindElement(By.ClassName(htmlElementInfo.Html_element_class));
                        }

                        //ElementToCapture = working_driver.FindElement(By.ClassName(htmlElementInfo.Html_element_class));
                    }


                    int eleLocX = ElementToCapture.Location.X;
                    int eleLocY = ElementToCapture.Location.Y;
                    //Get the element Size
                    int The_Element_Width = ElementToCapture.Size.Width;
                    int The_Element_Height = ElementToCapture.Size.Height;


                    //Get the Element location Via X/Y coordinates
                    int The_Element_Location_X = ElementToCapture.Location.X;
                    int The_Element_Location_Y = ElementToCapture.Location.Y;


                    //Creating the Rectangle that we going to use to extract the element
                    Rectangle Test = new Rectangle(The_Element_Location_X, The_Element_Location_Y, The_Element_Width, The_Element_Height);

                    //Take Screenshot and save it on TMP file
                    ((ITakesScreenshot)working_driver).GetScreenshot().SaveAsFile(@"e:\TMP.jpeg", OpenQA.Selenium.ScreenshotImageFormat.Png);

                    //import The File that we save earlier
                    Bitmap ImportFile = new Bitmap(@"e:\TMP.jpeg");

                    //Clone and extract the requested Element (Based on our Rectangle)
                    CloneFile = (Bitmap)ImportFile.Clone(Test, ImportFile.PixelFormat);

                    //Save The Extract Element
                    //CloneFile.Save(@"e:\SpecificElementAfter.jpeg");
                    if (SaveImageToIndex == 0)
                    {
                        //clipboard
                    }
                    else if (SaveImageToIndex == 1)
                    {
                        SaveImage(ImgFileName, OperationIndex);
                    }




                    //Dispose the TMP file that we don't need anymore
                    ImportFile.Dispose();

                    //Delete the TMP file
                    System.IO.File.Delete(@"e:\TMP.jpeg");
                }







                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public void SaveImage(string fileName, int OperationIndex)
        {
            if (OperationIndex == 0)
            {
                if (ImgFormatIndex == 0)
                {
                    fullPageScreenshot.SaveAsFile(fileName + ".bmp", OpenQA.Selenium.ScreenshotImageFormat.Bmp);
                }
                else if (ImgFormatIndex == 1)
                {
                    fullPageScreenshot.SaveAsFile(fileName + ".gif", OpenQA.Selenium.ScreenshotImageFormat.Gif);
                }
                else if (ImgFormatIndex == 2)
                {
                    fullPageScreenshot.SaveAsFile(fileName + ".jpg", OpenQA.Selenium.ScreenshotImageFormat.Jpeg);
                }
                else if (ImgFormatIndex == 3)
                {
                    fullPageScreenshot.SaveAsFile(fileName + ".png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                }
                else if (ImgFormatIndex == 4)
                {
                    fullPageScreenshot.SaveAsFile(fileName + ".tiff", OpenQA.Selenium.ScreenshotImageFormat.Tiff);
                }

            }
            else if (OperationIndex == 1)
            {
                if (ImgFormatIndex == 0)
                {
                    CloneFile.Save(fileName + ".bmp");
                }
                else if (ImgFormatIndex == 1)
                {
                    CloneFile.Save(fileName + ".gif");
                }
                else if (ImgFormatIndex == 2)
                {
                    CloneFile.Save(fileName + ".jpeg");
                }
                else if (ImgFormatIndex == 3)
                {
                    CloneFile.Save(fileName + ".png");
                }
                else if (ImgFormatIndex == 4)
                {
                    CloneFile.Save(fileName + ".tiff");
                }
            }


        }
    }



}
