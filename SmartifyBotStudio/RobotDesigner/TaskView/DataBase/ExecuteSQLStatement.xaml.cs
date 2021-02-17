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
    public partial class ExecuteSQLStatement : UserControl
    {
        public RobotDesigner.TaskModel.DataBase.ExecuteSQLStatement ExecuteSQLStatementData { get; set; }

        public string ConnectionString { get; set; }

        public ExecuteSQLStatement()
        {
            InitializeComponent();
            Loaded += ExecuteSQLStatementLoaded;

        }

        private void ExecuteSQLStatementLoaded(object sender, RoutedEventArgs e)
        {
            //connStringText.Text = OpenSQLConnectionTaskData.AutomationConnectionString;
            connectionVariableComboBox.ItemsSource = VariableStorage.DataBaseConnStrVar.Keys;
            sqlResultVarText.Text = ExecuteSQLStatementData.sqlResultVarName;

            if (ExecuteSQLStatementData.GetConnSchemaIndex == 0)
            {
                connectionStrText.Text = ExecuteSQLStatementData.newConnStr;
            }
            else if (ExecuteSQLStatementData.GetConnSchemaIndex == 1)
            {
                getConnSchemaComboBox.SelectedIndex = 1;
                connectionVariableComboBox.SelectedIndex = ExecuteSQLStatementData.connStrVarComboIndex;
            }


            sqlQueryRichText.Document.Blocks.Clear();
            sqlQueryRichText.Document.Blocks.Add(new Paragraph(new Run(ExecuteSQLStatementData.Query)));
        }



        private void connStringPick_button_Click(object sender, RoutedEventArgs e)
        {

            connectionStrText.Text = string.Empty;
            string strConnString = "";

            ConnectionString = strConnString;


            if (string.IsNullOrEmpty(ConnectionString) || ConnectionString.IndexOf("provider=", StringComparison.OrdinalIgnoreCase) < 0)
            {
                ConnectionString = "Provider=SQLOLEDB.1;";
            }


            var parentWindow = ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow);


            ConnectionString = OleDbConnString.EditConnectionString(parentWindow, ConnectionString);

            connectionStrText.Text = ConnectionString;







        }



        private void more_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            int counter = 0;


            if (string.IsNullOrEmpty(sqlResultVarText.Text))
            {
                MessageBox.Show("Please, Enter a variable name to store result.");
            }





            if (!string.IsNullOrEmpty(sqlResultVarText.Text))
            {

                if (getConnSchemaComboBox.SelectedIndex == 0)
                {
                    if (string.IsNullOrEmpty(connectionStrText.Text.ToString()))
                    {
                        MessageBox.Show("Please, Insert Connection String.");
                    }
                    else
                    {
                        ExecuteSQLStatementData.GetConnSchemaIndex = 0;
                        ExecuteSQLStatementData.newConnStr = ConnectionString;
                        counter++;
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(connectionVariableComboBox.Text))
                    {

                        ExecuteSQLStatementData.GetConnSchemaIndex = 1;
                        ExecuteSQLStatementData.connStrVarComboIndex = connectionVariableComboBox.SelectedIndex;
                        ExecuteSQLStatementData.connStrVar = connectionVariableComboBox.Text;
                        counter++;

                    }
                }


                TextRange textRange = new TextRange(sqlQueryRichText.Document.ContentStart, sqlQueryRichText.Document.ContentEnd);

                if (string.IsNullOrEmpty(textRange.Text.ToString().Trim()))
                {
                    MessageBox.Show("Please, Insert Query.");
                }
                else
                {
                    ExecuteSQLStatementData.Query = textRange.Text.ToString().Trim();
                    counter++;
                }


                ExecuteSQLStatementData.sqlResultVarName = sqlResultVarText.Text.Trim();


                if (counter == 2)
                {
                    ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
                }


            }




        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
