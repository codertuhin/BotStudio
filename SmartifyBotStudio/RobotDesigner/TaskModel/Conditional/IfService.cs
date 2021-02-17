using System.ServiceProcess;
using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Conditional
{

    [Serializable]
    public class IfService : RobotActionBase, ITask
    {
        public string ServiceName { get; set; }
        public bool IsRunning { get; set; }
        public bool IsPaused { get; set; }
        public bool IsStopped { get; set; }
        public bool IsInstalled { get; set; }
        public bool IsNotInstalled { get; set; }

        public int StatusIndex = 0;

        public int ServiceNameIndex = 0;

        public int Execute()
        {
            try
            {
                ConditionResult = false;

                if (IsRunning)
                {
                    if (new ServiceController(ServiceName).Status == ServiceControllerStatus.Running)
                    {
                        ConditionResult = true;
                    }
                }
                if (IsPaused)
                {
                    if (new ServiceController(ServiceName).Status == ServiceControllerStatus.Paused)
                    {
                        ConditionResult = true;
                    }
                }
                if (IsStopped)
                {
                    if (new ServiceController(ServiceName).Status == ServiceControllerStatus.Stopped)
                    {
                        ConditionResult = true;
                    }
                }

                if (IsInstalled)
                {
                    ServiceController[] scServices;
                    scServices = ServiceController.GetServices();

                    List<string> serviceName = new List<string>();

                    if (serviceName.Contains(ServiceName))
                    {
                        ConditionResult = true;
                    }

                }

                if (IsNotInstalled)
                {
                    ServiceController[] scServices;
                    scServices = ServiceController.GetServices();

                    List<string> serviceName = new List<string>();


                    if (!serviceName.Contains(ServiceName))
                    {
                        ConditionResult = true;
                    }

                }


                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
