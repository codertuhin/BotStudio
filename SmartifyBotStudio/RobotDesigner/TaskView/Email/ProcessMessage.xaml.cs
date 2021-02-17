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

using MimeKit;
using MailKit.Net.Smtp;

using MailKit.Search;
using MailKit.Net.Imap;
using MailKit.Security;
using MailKit;
using System.Threading;

namespace SmartifyBotStudio.RobotDesigner.TaskView.Email
{
    /// <summary>
    /// Interaction logic for LaunchNewIE.xaml
    /// </summary>
    public partial class ProcessMessage : UserControl
    {

        public RobotDesigner.TaskModel.Email.ProcessMessage ProcessMessageData { get; set; }
        Dictionary<string, IMailFolder> retrievedFolder;
        Dictionary<string, IMailFolder> retrievedFolderBackUp;

        ImapClient client = new ImapClient();

        public ProcessMessage()
        {
            InitializeComponent();
            Loaded += Template_loaded;
        }

        private void Template_loaded(object sender, RoutedEventArgs e)
        {

          

            imapServerName.Text = ProcessMessageData.IMAPServerName ;
            smtpServerPortNo.Text = ProcessMessageData.Port.ToString();
            emailVars_combo.ItemsSource = VariableStorage.EmailVar.Keys;

            emailOpeparationCombo.SelectedIndex=ProcessMessageData.EmailOpeparationComboIndex;

            userNameText.Text = ProcessMessageData.UserName;
            passWordText.Password = ProcessMessageData.Password;

            if (ProcessMessageData.MailFolderAccessCheck)
            {
                mailFolderCombo.ItemsSource = GetEmailFolders().Keys;
                mailFolderCombo.SelectedIndex = ProcessMessageData.MailFolderComboIndex;

            }
            emailVars_combo.SelectedIndex=ProcessMessageData.EmailVarComboIndex;

            if (ProcessMessageData.IAMPAuthanticationFlag)
            {
                imapServerName.Text = "imap.gmail.com";
                smtpServerPortNo.Text = 993.ToString();
            }

        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }

       
        private Dictionary<string, IMailFolder> GetEmailFolders()
        {


            //if( !client.IsConnected)
            //{
            try
            {
                if (client.IsConnected)
                {
                    client.Disconnect(true);
                    client.Connect(imapServerName.Text.Trim(), Convert.ToInt32(smtpServerPortNo.Text), SecureSocketOptions.SslOnConnect);

                }
                else
                {
                    client.Connect(imapServerName.Text.Trim(), Convert.ToInt32(smtpServerPortNo.Text), SecureSocketOptions.SslOnConnect);

                }

                try
                {
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    client.Authenticate(userNameText.Text.Trim(), passWordText.Password.Trim());


                    var inbox = client.Inbox;

                    inbox.Open(FolderAccess.ReadWrite);


                    retrievedFolder = new Dictionary<string, IMailFolder>();

                    //// Get all folders 
                    var cancel = new CancellationTokenSource();
                    var personal = client.GetFolder(client.PersonalNamespaces[0]);
                    foreach (var folder in personal.GetSubfolders(false, cancel.Token))

                    {
                        retrievedFolder.Add(folder.Name, folder);

                        foreach (var subfolders in folder.GetSubfolders(false, cancel.Token))
                        {

                            retrievedFolder.Add(folder.Name + "/" + subfolders.Name, subfolders);
                        }
                    }

                    retrievedFolderBackUp = retrievedFolder;

                   

                    return retrievedFolder;
                }
                catch (Exception)
                {
                    //client.Disconnect(true);
                    MessageBox.Show("User Name or Password is incorrect");
                }
            }
            catch (Exception ex)
            {
                // client.Disconnect(true);

                MessageBox.Show("Please, check your IMAP settings");

            }







            return retrievedFolderBackUp;


        }

        private void getEmailFolder_click(object sender, RoutedEventArgs e)
        {
            if (GetEmailFolders() != null)
            {
                mailFolderCombo.ClearValue(ItemsControl.ItemsSourceProperty);
                mailFolderCombo.ItemsSource = retrievedFolder.Keys;
                mailFolderCombo.SelectedIndex = 0;
                //client.Disconnect(true);

            }
        }
       
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            ProcessMessageData.EmailtoSaveVar = emailVars_combo.Text;

            ProcessMessageData.IMAPServerName = imapServerName.Text;

            ProcessMessageData.UserName=userNameText.Text;
            ProcessMessageData.Password=passWordText.Password;

            int intergerCheck;
            if (int.TryParse(smtpServerPortNo.Text.Trim(), out intergerCheck))
            {
                ProcessMessageData.Port = Convert.ToInt32(smtpServerPortNo.Text.Trim());

            }


            if (!string.IsNullOrEmpty( mailFolderCombo.Text))
            {
                ProcessMessageData.FolderPath = retrievedFolder[mailFolderCombo.Text].ToString();
                ProcessMessageData.MailFolderAccessCheck = true;
                ProcessMessageData.MailFolderComboIndex = mailFolderCombo.SelectedIndex;
                ProcessMessageData.IAMPAuthanticationFlag = false;

            }

            ProcessMessageData.EmailOpeparationComboIndex = emailOpeparationCombo.SelectedIndex;
            ProcessMessageData.EmailVarComboIndex = emailVars_combo.SelectedIndex;


            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }
       
    }
}
