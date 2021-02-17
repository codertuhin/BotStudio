using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartifyBotStudio.Models;
using System.Xml.Serialization;
using System.IO;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System.ComponentModel;
//using AE.Net.Mail;
using ActiveUp.Net.Mail;

using MimeKit;
using MailKit.Net.Smtp;

using MailKit.Search;
using MailKit.Net.Imap;
using MailKit.Security;
using MailKit;
using System.Threading;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Email
{


    [Serializable]
    public class GetMessages : RobotActionBase, ITask
    {
        public int RetrieveSchema { get; set; }
        public bool markAsRead { get; set; }
        public string FromSearch { get; set; }
        public string ToSearch { get; set; }
        public string SubjectSearch { get; set; }
        public string BodySearch { get; set; }
        public int AttchmentSchemaIndex = 0;
        public string AttchmentPath { get; set; }
        public string IMAPServerName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string EmailtoSaveVar { get; set; }
        public string FolderPath { get; set; }
        public bool IAMPAuthanticationFlag = true;

        //public IMailFolder Folder { get; set; }

        //public Dictionary<string, IMailFolder> MailFolderBackup { get; set; }
        public int MailFolderComboIndex { get; set; }
        public bool MailFolderAccessCheck = false;

        public int Execute()
        {
            //IMAPServerName = "imap.gmail.com";
            //UserName = "rzrt.edu@gmail.com";
            //Password = "";
            //Port = 993;
            //markAsRead = true;


            try
            {


                var client = VariableStorage.EmailVar[EmailtoSaveVar].Item2;

                if (!client.IsConnected)
                {

                    client.Connect(IMAPServerName, Port, SecureSocketOptions.SslOnConnect);


                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    client.Authenticate(UserName, Password);
                }

                var working_folder = client.GetFolder(VariableStorage.EmailVar[EmailtoSaveVar].Item3);
                working_folder.Open(FolderAccess.ReadOnly);

                Dictionary<string, IMailFolder> retrievedFolder = new Dictionary<string, IMailFolder>();

                SearchResults results = new SearchResults();

                // search from folder
                if (RetrieveSchema == 0)
                {
                    results = working_folder.Search(SearchOptions.All, SearchQuery.Not(SearchQuery.Seen).And((SearchQuery.SubjectContains(SubjectSearch).Or(SearchQuery.ToContains(ToSearch).Or(SearchQuery.FromContains(FromSearch).Or(SearchQuery.BodyContains(BodySearch)))))));
                }

                else if (RetrieveSchema == 1)
                {
                    results = working_folder.Search(SearchOptions.All, SearchQuery.SubjectContains(SubjectSearch).Or(SearchQuery.ToContains(ToSearch).Or(SearchQuery.FromContains(FromSearch).Or(SearchQuery.BodyContains(BodySearch)))));
                }







                if (VariableStorage.EmailVar.ContainsKey(EmailtoSaveVar))
                {
                    VariableStorage.EmailVar.Remove(EmailtoSaveVar);
                }
                VariableStorage.EmailVar.Add(EmailtoSaveVar, Tuple.Create(this.ID, client, FolderPath, (object)results.UniqueIds));



                // marks as read
                if (markAsRead)
                {
                    foreach (var uniqueId in results.UniqueIds)
                    {
                        var message = working_folder.GetMessage(uniqueId);
                        working_folder.AddFlags(uniqueId, MessageFlags.Seen, true);
                    }
                }



                // attachment download
                if (AttchmentSchemaIndex == 1)
                {
                    foreach (UniqueId uid in results.UniqueIds)
                    {
                        MimeMessage message = client.Inbox.GetMessage(uid);

                        foreach (MimeEntity attachment in message.Attachments)
                        {
                            var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;

                            using (var stream = System.IO.File.Create(@AttchmentPath + fileName))
                            {
                                if (attachment is MessagePart)
                                {
                                    var rfc822 = (MessagePart)attachment;

                                    rfc822.Message.WriteTo(stream);
                                }
                                else
                                {
                                    var part = (MimeKit.MimePart)attachment;

                                    part.ContentObject.DecodeTo(stream);
                                }
                            }
                        }
                    }
                }






                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }



        }
        //Lazy<MailMessage>[] ReadUnReadMails(ImapClient imapClient, bool markAsRead)
        //{
        //    List<AE.Net.Mail.MailMessage> unreadails = new List<AE.Net.Mail.MailMessage>();

        //    using (imapClient)
        //    {

        //        var mailMessage = imapClient.SearchMessages(SearchCondition.Unseen(), false, true);
        //        return mailMessage;
        //    }
        //}

        //List<MailMessage> ReadInbox(ImapClient imapClient)
        //{

        //    imapClient.SelectMailbox("INBOX");

        //    List<MailMessage> inboxMails = imapClient.GetMessages(0, imapClient.GetMessageCount()).ToList();
        //    //var inbox = imapClient.SearchMessages(SearchCondition.From(FromSearch).And(SearchCondition.To(ToSearch).And(SearchCondition.Subject(SubjectSearch).And(SearchCondition.Body(BodySearch)))));

        //    return inboxMails;

        //}

    }








}
