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
    public class CloseSQLConnection : RobotActionBase, ITask
    {
        //public List<SelectedFileInfo> FilesToDetele { get; set; }

       
        public string Var_StoreDeletedFiles { get; set; }


        public string ConnectionToClose { get; set; }




        public int Execute()
        {
            try
            {
               






                 VariableStorage.DataBaseConnStrVar[ConnectionToClose].Item2.Close();
                //(VariableStorage.DataBaseConnStrVar[ConnectionToClose].Item2 as System.Data.OleDb.OleDbConnection).Close();


                if (VariableStorage.DataBaseConnStrVar[ConnectionToClose].Item2.State == ConnectionState.Closed)
                {
                    //MessageBox.Show("Closed");
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
