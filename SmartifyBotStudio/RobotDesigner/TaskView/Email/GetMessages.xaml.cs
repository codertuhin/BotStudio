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
    public partial class GetMessages : UserControl
    {

        public RobotDesigner.TaskModel.Email.GetMessages GetMessagesData { get; set; }

        Dictionary<string, IMailFolder> retrievedFolder;
        Dictionary<string, IMailFolder> retrievedFolderBackUp;

        string emptySearch = "``";

        ImapClient client = new ImapClient();

        public GetMessages()
        {
            InitializeComponent();
            Loaded += GetMessages_loaded;
        }

        private void GetMessages_loaded(object sender, RoutedEventArgs e)
        {

            imapServerName.Text = GetMessagesData.IMAPServerName;
            passWordText.Password = GetMessagesData.Password;
            smtpServerPortNo.Text = GetMessagesData.Port.ToString();
            userNameText.Text = GetMessagesData.UserName;

            if (GetMessagesData.MailFolderAccessCheck)
            {
                mailFolderCombo.ItemsSource = GetEmailFolders().Keys;
                mailFolderCombo.SelectedIndex = GetMessagesData.MailFolderComboIndex;

            }


            markAsRead_checkBox.IsChecked = GetMessagesData.markAsRead;

            attachment_schemaCombo.SelectedIndex = GetMessagesData.AttchmentSchemaIndex;
            getSchema_combo.SelectedIndex = GetMessagesData.RetrieveSchema;

            saveEmailIntoVar.Text = GetMessagesData.EmailtoSaveVar;

            saveAttachmentInto_text.Text = GetMessagesData.AttchmentPath;


            if (GetMessagesData.FromSearch != emptySearch)
            {
                from_search.Text = GetMessagesData.FromSearch;
            }
            if (GetMessagesData.ToSearch != emptySearch)
            {
                to_search.Text = GetMessagesData.ToSearch;
            }
            if (GetMessagesData.SubjectSearch != emptySearch)
            {
                body_search.Text = GetMessagesData.BodySearch;
            }
            if (GetMessagesData.BodySearch != emptySearch)
            {
                subject_search.Text = GetMessagesData.SubjectSearch;
            }



            if (GetMessagesData.IAMPAuthanticationFlag)
            {
                imapServerName.Text = "imap.gmail.com";
                smtpServerPortNo.Text = 993.ToString();
            }





        }

        private void MailFolderCombo_Drop(object sender, DragEventArgs e)
        {
            mailFolderCombo.ItemsSource = GetEmailFolders().Keys;
        }

        private void MailFolderCombo_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // if (e.MouseDevice == Key.Down)
            {
                mailFolderCombo.ItemsSource = GetEmailFolders().Keys;
            }
        }

        private void MailFolderCombo_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void btnOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog()
            {
                ShowNewFolderButton = true,
            };

            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //folder_text.Text = folderBrowserDialog.SelectedPath;
            }
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

                    //mailFolderCombo.ItemsSource = retrievedFolder.Keys;

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


            GetMessagesData.FromSearch = from_search.Text;
            GetMessagesData.ToSearch = to_search.Text;
            GetMessagesData.BodySearch = body_search.Text;
            GetMessagesData.SubjectSearch = subject_search.Text;

            GetMessagesData.IMAPServerName = imapServerName.Text;
            GetMessagesData.Password = passWordText.Password;
            GetMessagesData.UserName = userNameText.Text.Trim();

            int intergerCheck;
            if (int.TryParse(smtpServerPortNo.Text.Trim(), out intergerCheck))
            {
                GetMessagesData.Port = Convert.ToInt32(smtpServerPortNo.Text.Trim());

            }

            GetMessagesData.markAsRead = (bool)markAsRead_checkBox.IsChecked;
            GetMessagesData.AttchmentSchemaIndex = attachment_schemaCombo.SelectedIndex;
            GetMessagesData.RetrieveSchema = getSchema_combo.SelectedIndex;
            GetMessagesData.AttchmentPath = saveAttachmentInto_text.Text.Trim();

            if (!string.IsNullOrEmpty(mailFolderCombo.Text.Trim()))
            {
                GetMessagesData.FolderPath = retrievedFolder[mailFolderCombo.Text].ToString();

            }

            GetMessagesData.EmailtoSaveVar = saveEmailIntoVar.Text.Trim();

            if (string.IsNullOrEmpty(from_search.Text))
            {
                GetMessagesData.FromSearch = emptySearch;
            }
            if (string.IsNullOrEmpty(to_search.Text))
            {
                GetMessagesData.ToSearch = emptySearch;
            }
            if (string.IsNullOrEmpty(subject_search.Text))
            {
                GetMessagesData.SubjectSearch = emptySearch;
            }
            if (string.IsNullOrEmpty(body_search.Text))
            {
                GetMessagesData.BodySearch = emptySearch;
            }

            if (retrievedFolder != null)
            {
                GetMessagesData.MailFolderAccessCheck = true;
                GetMessagesData.MailFolderComboIndex = mailFolderCombo.SelectedIndex;

            }



            if (!string.IsNullOrEmpty(mailFolderCombo.Text))
            {
                if (VariableStorage.EmailVar.ContainsKey(saveEmailIntoVar.Text.Trim()))
                {
                    VariableStorage.EmailVar.Remove(saveEmailIntoVar.Text.Trim());
                }
                if (!client.IsConnected)
                {
                    client.Connect(imapServerName.Text.Trim(), Convert.ToInt32(smtpServerPortNo.Text), SecureSocketOptions.SslOnConnect);
                    VariableStorage.EmailVar.Add(saveEmailIntoVar.Text.Trim(), Tuple.Create(GetMessagesData.ID, client, retrievedFolder[mailFolderCombo.Text].ToString(), (object)null));
                    VariableStorage.IMAPCredentialVar.Add(saveEmailIntoVar.Text.Trim(),Tuple.Create( GetMessagesData.IMAPServerName, GetMessagesData.Port, GetMessagesData.UserName, GetMessagesData.Password));
                    GetMessagesData.IAMPAuthanticationFlag = false;
                }
                else
                {
                    VariableStorage.EmailVar.Add(saveEmailIntoVar.Text.Trim(), Tuple.Create(GetMessagesData.ID, client, retrievedFolder[mailFolderCombo.Text].ToString(), (object)null));
                    VariableStorage.IMAPCredentialVar.Add(saveEmailIntoVar.Text.Trim(), Tuple.Create(GetMessagesData.IMAPServerName, GetMessagesData.Port, GetMessagesData.UserName, GetMessagesData.Password));
                    GetMessagesData.IAMPAuthanticationFlag = false;
                }
            }


            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void test_Button_Click(object sender, RoutedEventArgs e)
        {
            //mailFolderCombo.ItemsSource = GetEmailFolders().Keys;
        }



    }
}
