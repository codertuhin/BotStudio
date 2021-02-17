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


//namespace SmartifyBotStudio.Templates

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Text
{


    [Serializable]
    public class TemplateClass : RobotActionBase, ITask
    {


        public int Execute()
        {
            try
            {


                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }



}
