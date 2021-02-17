using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Windows;
using System.Data;
//using Sys

namespace SmartifyBotStudio.RobotDesigner.TaskModel.DataBase
{
    [Serializable]
    public class OpenSQLConnection : RobotActionBase, ITask
    {
        //public List<SelectedFileInfo> FilesToDetele { get; set; }


        public string Var_StoreDeletedFiles { get; set; }

        public string AutomationConnectionString { get; set; }

        public string ConnectionStrVar { get; set; }

        public int Execute()
        {
            try
            {
                



                VariableStorage.DataBaseConnStrVar[ConnectionStrVar].Item2.Open();

                //(VariableStorage.DataBaseConnStrVar[ConnectionStrVar].Item2 as System.Data.OleDb.OleDbConnection).Open();

                if (VariableStorage.DataBaseConnStrVar[ConnectionStrVar].Item2.State==ConnectionState.Open)
                {
                   // MessageBox.Show("open");
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
