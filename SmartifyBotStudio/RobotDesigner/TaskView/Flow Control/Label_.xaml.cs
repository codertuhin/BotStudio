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

namespace SmartifyBotStudio.RobotDesigner.TaskView.Flow_Control
{
    /// <summary>
    /// Interaction logic for Label_.xaml
    /// </summary>
    public partial class Label_ : UserControl
    {

        public RobotDesigner.TaskModel.Flow_Control.Label_ LabelData { get; set; }

        public Label_()
        {
            InitializeComponent();
            Loaded += Template_loaded;
        }

        private void Template_loaded(object sender, RoutedEventArgs e)
        {
            labelName_text.Text = LabelData.Label_Name;


        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {


            LabelData.Label_Name = labelName_text.Text.Trim();
            LabelData.LabelName= labelName_text.Text.Trim();


            if (VariableStorage.LabelVar.ContainsKey(LabelData.Label_Name))
            {
                VariableStorage.LabelVar.Remove(LabelData.Label_Name);
            }
            VariableStorage.LabelVar.Add(LabelData.Label_Name, Tuple.Create(LabelData.ID, LabelData.Label_Name));


            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }
    }
}
