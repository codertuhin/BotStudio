using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Delay
{
    [Serializable]
    public class DelayByTiming : RobotActionBase, ITask
    {
        public int TimeToDelay { get; set; }


        public int Execute()
        {
            try
            {


                Thread.Sleep(TimeToDelay * 1000);





                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        static async Task asyncTask()
        {

            Console.WriteLine("async: Starting");
            Task wait = Task.Delay(5000);

            await wait;

        }

    }
}
