using MahApps.Metro.Controls;
using SmartifyBotStudio.RobotDesigner.TaskModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.OleDb;
using ADODB;
using SmartifyBotStudio.RobotDesigner.TaskModel.DataBase;
using System.Windows.Interop;
using SmartifyBotStudio.RobotDesigner.Variable;

namespace SmartifyBotStudio.RobotDesigner.TaskView.DataBase
{
    /// <summary>
    /// Interaction logic for OpenSQLConnection.xaml
    /// </summary>
    public partial class OpenSQLConnection : UserControl
    {
        public RobotDesigner.TaskModel.DataBase.OpenSQLConnection OpenSQLConnectionTaskData { get; set; }



        public string ConnectionString { get; set; }

        public OpenSQLConnection()
        {
            InitializeComponent();
            Loaded += OpenConnectionLoaded;

        }

        private void OpenConnectionLoaded(object sender, RoutedEventArgs e)
        {
            connStringText.Text = OpenSQLConnectionTaskData.AutomationConnectionString;
            connStrVar.Text = OpenSQLConnectionTaskData.ConnectionStrVar;
        }



        private void connStringPick_button_Click(object sender, RoutedEventArgs e)
        {
            connStringText.Text = string.Empty;
            string strConnString = "";
            //object _con = null;
            //MSDASC.DataLinks _link = new MSDASC.DataLinks();
            //_con = _link.PromptNew();
            //if (_con == null)
            //    connStringText.Text = string.Empty;
            //strConnString = ((ADODB.Connection)_con).ConnectionString;
            //connStringText.Text = strConnString;
            ConnectionString = strConnString;

            //string connString = _cmbConnString.Text;
            if (string.IsNullOrEmpty(ConnectionString) || ConnectionString.IndexOf("provider=", StringComparison.OrdinalIgnoreCase) < 0)
            {
                ConnectionString = "Provider=SQLOLEDB.1;";
            }

            // let user change it

            //var helper = new WindowInteropHelper();
            //helper.Owner = win32Window.Handle;

            var parentWindow = ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow);


            ConnectionString = OleDbConnString.EditConnectionString(parentWindow, ConnectionString);


            connStringText.Text = ConnectionString;

            //IntPtr windowHandle = new WindowInteropHelper(parentWindow).Handle;


        }



        private void more_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            OpenSQLConnectionTaskData.AutomationConnectionString = connStringText.Text;
            OpenSQLConnectionTaskData.ConnectionStrVar = VariableStorage.VariableNameFormator(connStrVar.Text.Trim());



            if (string.IsNullOrEmpty(connStrVar.Text.Trim()))
            {
                MessageBox.Show("Please, Enter a name to store SQL Connection");
            }

            if (string.IsNullOrEmpty(connStringText.Text.Trim()))
            {
                MessageBox.Show("Please, enter a valid connection string");
            }



            //if (VariableStorage.DataBaseConnStrVar.ContainsKey(OpenSQLConnectionTaskData.ConnectionStrVar))
            //{
            //    MessageBox.Show("Variable name already exists");
            //}

            if (VariableStorage.DataBaseConnStrVar.ContainsKey(OpenSQLConnectionTaskData.ConnectionStrVar))
                {

                    VariableStorage.DataBaseConnStrVar.Remove(OpenSQLConnectionTaskData.ConnectionStrVar);

                }
                VariableStorage.DataBaseConnStrVar.Add(OpenSQLConnectionTaskData.ConnectionStrVar, Tuple.Create(OpenSQLConnectionTaskData.ID, new OleDbConnection()
                {
                    ConnectionString = OpenSQLConnectionTaskData.AutomationConnectionString
                }));

            if (!string.IsNullOrEmpty(connStrVar.Text.Trim()) && !string.IsNullOrEmpty(connStringText.Text))
            {

                ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
            }

        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }


    }
}
