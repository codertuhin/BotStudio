using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.DataBase
{
    [Serializable]
    public static class DataBaseConnectionManager
    {
        public static SqlConnection sqlConnection = new SqlConnection();
        
    }
}
