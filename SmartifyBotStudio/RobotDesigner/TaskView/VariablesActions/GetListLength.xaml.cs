using MahApps.Metro.Controls;
using SmartifyBotStudio.RobotDesigner.TaskModel;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.Collections.Generic;
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

namespace SmartifyBotStudio.RobotDesigner.TaskView.VariablesActions
{
    /// <summary>
    /// Interaction logic for GetListLength.xaml
    /// </summary>
    public partial class GetListLength : UserControl
    {

        public RobotDesigner.TaskModel.VariablesActions.GetListLength GetListLengthData { get; set; }


        public GetListLength()
        {
            InitializeComponent();
            Loaded += GetListLengthData_loaded;
        }


        private void GetListLengthData_loaded(object sender, RoutedEventArgs e)
        {
            if (VariableStorage.CreateNewListVar != null)
            {
                listName_combo.ItemsSource = VariableStorage.CreateNewListVar.Keys;
                listName_combo.SelectedIndex = GetListLengthData.ListVarIndex;
            }

            storeVar_text.Text = GetListLengthData.LengthStoreVar;


        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(storeVar_text.Text.Trim()))
            {



                GetListLengthData.WorkingList = listName_combo.Text;
                GetListLengthData.ListVarIndex = listName_combo.SelectedIndex;
                GetListLengthData.LengthStoreVar = storeVar_text.Text.Trim();

                if (VariableStorage.SetVariableVar.ContainsKey(GetListLengthData.LengthStoreVar))
                {
                    VariableStorage.SetVariableVar.Remove(GetListLengthData.LengthStoreVar);
                }

                VariableStorage.SetVariableVar.Add(GetListLengthData.LengthStoreVar, Tuple.Create(GetListLengthData.ID, (object)"value has not been set yet", typeof
                    (int)));


                ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;

            }
            else
            {
                MessageBox.Show("Please insert a variable name");
            }

        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

    }
}
