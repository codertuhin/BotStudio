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
    /// Interaction logic for DeleteList.xaml
    /// </summary>
    public partial class DeleteList : UserControl
    {
        public RobotDesigner.TaskModel.VariablesActions.DeleteList DeleteListData { get; set; }

        public DeleteList()
        {
            InitializeComponent();
            Loaded += DeleteListData_loaded;
        }



        private void DeleteListData_loaded(object sender, RoutedEventArgs e)
        {
            if (VariableStorage.CreateNewListVar != null)
            {
                listName_combo.ItemsSource = VariableStorage.CreateNewListVar.Keys;
                listName_combo.SelectedIndex = DeleteListData.ListNameIndex;
            }

        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {


            DeleteListData.ListToDelete = listName_combo.Text;

            DeleteListData.ListNameIndex = listName_combo.SelectedIndex;


            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }
    }
}
