using MahApps.Metro.Controls;
using SmartifyBotStudio.RobotDesigner.TaskModel;
using SmartifyBotStudio.RobotDesigner.TaskModel.Web_Automation;
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
using SmartifyBotStudio.RobotDesigner.Variable;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace SmartifyBotStudio.RobotDesigner.TaskView.Email
{
    /// <summary>
    /// Interaction logic for LaunchNewIE.xaml
    /// </summary>
    public partial class SendMessages : UserControl
    {

        public RobotDesigner.TaskModel.Email.SendMessages SendMessagesData { get; set; }

        public SendMessages()
        {
            InitializeComponent();
            Loaded += SendMessages_loaded;
        }

        private void SendMessages_loaded(object sender, RoutedEventArgs e)
        {
            smtpServerName.Text = SendMessagesData.SMTPServer;
            smtpServerPortNo.Text = SendMessagesData.Port.ToString();
            paasWordText.Password=SendMessagesData.Password;

            userNameText.Text= SendMessagesData.From;
            from_text.Text = SendMessagesData.From;
            senderDisplayName_text.Text = SendMessagesData.SenderDisplayName;
            to_text.Text = SendMessagesData.To;
            cc_text.Text = SendMessagesData.CC;
            bcc_text.Text = SendMessagesData.BCC;
            subject_text.Text = SendMessagesData.Subject;
            attachment_text.Text=SendMessagesData.Attachment;


            body_richText.Document.Blocks.Clear();
            body_richText.Document.Blocks.Add(new Paragraph(new Run(SendMessagesData.Body)));

        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void btnOpenFiles_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {

                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                attachment_text.Text = openFileDialog.FileName;

                string sourceFile = openFileDialog.FileName;


            }
        }

        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            SendMessagesData.From = from_text.Text;
            SendMessagesData.SenderDisplayName = senderDisplayName_text.Text;
            SendMessagesData.To = to_text.Text;
            SendMessagesData.CC = cc_text.Text;
            SendMessagesData.BCC = bcc_text.Text;
            SendMessagesData.Subject = subject_text.Text;
            SendMessagesData.SMTPServer = smtpServerName.Text;
            SendMessagesData.Port = Convert.ToInt32(smtpServerPortNo.Text);
            SendMessagesData.Password = paasWordText.Password.Trim();

            TextRange textRange = new TextRange(body_richText.Document.ContentStart, body_richText.Document.ContentEnd);

            SendMessagesData.Body = textRange.Text;
            SendMessagesData.Attachment = attachment_text.Text.Trim();

            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

    }
}
