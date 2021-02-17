using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.DataBase
{
    [Serializable]
    public class ExecuteSQLStatement : RobotActionBase, ITask
    {
        public string Var_StoreDeletedFiles { get; set; }

        public int GetConnSchemaIndex = 3;
        public string newConnStr { get; set; }

        public int connStrVarComboIndex { get; set; }
        public string connStrVar { get; set; }

        //[NonSerialized]
        //public OleDbConnection OledbConnection;
        public string Query { get; set; }

        public string sqlResultVarName { get; set; }

        public int Execute()
        {
            try
            {

                sqlResultVarName = VariableStorage.VariableNameFormator(sqlResultVarName);

                if (GetConnSchemaIndex == 0)
                {
                    OleDbConnection OledbConnection = new OleDbConnection(newConnStr);
                    if (OledbConnection.State.ToString() != "Open")
                    {
                        OledbConnection.Open();
                    }


                    OleDbCommand command = new OleDbCommand(Query, OledbConnection);

                    OleDbDataAdapter adp = new OleDbDataAdapter(command);


                    DataSet dataSet = new DataSet();





                    adp.Fill(dataSet);


                    dataSet.WriteXml(@"E:\queryresult.xml");

                    if (VariableStorage.DataBaseQueryResultlVar.ContainsKey(sqlResultVarName))
                    {
                        VariableStorage.DataBaseQueryResultlVar.Remove(sqlResultVarName);
                    }

                    VariableStorage.DataBaseQueryResultlVar.Add(sqlResultVarName, Tuple.Create(this.ID, dataSet));


                }
                else if (GetConnSchemaIndex == 1)
                {
                    OleDbConnection OledbConnection = VariableStorage.DataBaseConnStrVar[connStrVar].Item2;

                    if (OledbConnection.State.ToString() != "Open")
                    {
                        OledbConnection.Open();
                    }

                    OleDbCommand command = new OleDbCommand(Query, OledbConnection);

                    OleDbDataAdapter adp = new OleDbDataAdapter(command);


                    DataSet dataSet = new DataSet();





                    adp.Fill(dataSet);


                    dataSet.WriteXml(@"E:\queryresult.xml");

                    if(VariableStorage.DataBaseQueryResultlVar.ContainsKey(sqlResultVarName))
                    {
                        VariableStorage.DataBaseQueryResultlVar.Remove(sqlResultVarName);
                    }
                    
                    VariableStorage.DataBaseQueryResultlVar.Add(sqlResultVarName, Tuple.Create(this.ID, dataSet));
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
