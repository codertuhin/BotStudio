//using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartifyBotStudio.RobotDesigner.TaskModel.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Text.Tests
{
   
    [TestFixture]
    public class ChangeTextCaseTests
    {
       
       [Test]
        public void ChangeTextCaseTests_ExecuteTest()
        {
            var workingClass =new ChangeTextCase() { TextToConvert="kjkj", ConvertIntoCaseComboIndex=0, ConveredTextStoreVar="testStoreVar"};


            var result = workingClass.Execute();

            
            Assert.That(result==1);
        }

        [Test]
        public void ChangeTextCaseTests_ExecuteTest_titleCase()
        {
            var workingClass = new ChangeTextCase() { TextToConvert = "kjkj", ConvertIntoCaseComboIndex = 2, ConveredTextStoreVar = "testStoreVar df ggd d" };


            var result = workingClass.Execute();


            Assert.That(result == 1);
        }

        [Test]
        public void ChangeTextCaseTests_ExecuteTest_RegEx()
        {
            var workingClass = new ChangeTextCase() { TextToConvert = "kjkj", ConvertIntoCaseComboIndex = 3, ConveredTextStoreVar = "testStoreVar" };


            var result = workingClass.Execute();


            Assert.That(result == 1);
        }
    }
}