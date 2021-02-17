using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace SmartifyBotStudio.RobotDesigner.Variable
{
    [Serializable]
    public class SerializationTest
    {
        public string connStr;
        public DataSet dt;
        [NonSerialized]
        public OleDbConnection conn;
        public SerializationTest(string connstr)
        {
            connStr = connstr;
            dt = new DataSet();
        }



        public void Open()
        {
            conn = new OleDbConnection(connStr);
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                MessageBox.Show("Open");
            }
            else
            {
                MessageBox.Show("Not open");

            }
        }

    }
}
