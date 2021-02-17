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
    public class ProcessMessage : RobotActionBase, ITask
    {
        public string FolderPath { get; set; }
        public string IMAPServerName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string EmailtoSaveVar { get; set; }
        public bool MailFolderAccessCheck = false;
        public int MailFolderComboIndex { get; set; }

        public int EmailOpeparationComboIndex { get; set; }
        public int EmailVarComboIndex { get; set; }
        public bool IAMPAuthanticationFlag = true;

        public int Execute()
        {
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


                working_folder.Close();
                working_folder.Open(FolderAccess.ReadWrite);

                if (EmailOpeparationComboIndex == 0)   ///Delete mails
                {
                    foreach (var uniqueId in (VariableStorage.EmailVar[EmailtoSaveVar].Item4 as IList<UniqueId>))
                    {
                        var message = working_folder.GetMessage(uniqueId);
                        working_folder.AddFlags(uniqueId, MessageFlags.Deleted, true);
                        working_folder.Expunge();
                    }
                }
                else if (EmailOpeparationComboIndex == 1)  /// mark unread
                {
                    foreach (var uniqueId in (VariableStorage.EmailVar[EmailtoSaveVar].Item4 as IList<UniqueId>))
                    {
                        var message = working_folder.GetMessage(uniqueId);
                        working_folder.AddFlags(uniqueId, MessageFlags.Seen, false);

                    }
                }
                else if (EmailOpeparationComboIndex == 2)   /// move to folder        
                {
                    var to_folder = client.GetFolder(FolderPath);
                    
                    foreach (var uid in (VariableStorage.EmailVar[EmailtoSaveVar].Item4 as IList<UniqueId>))
                    {
                        working_folder.MoveTo(uid, to_folder);
                    }

                }
                else if (EmailOpeparationComboIndex == 3)   /// mark unread and move to folder 
                {
                    var to_folder = client.GetFolder(FolderPath);
                    to_folder.Open(FolderAccess.ReadWrite);

                    foreach (var uniqueId in (VariableStorage.EmailVar[EmailtoSaveVar].Item4 as IList<UniqueId>))
                    {
                        var message = working_folder.GetMessage(uniqueId);
                        working_folder.AddFlags(uniqueId, MessageFlags.Seen, false);
                    }

                    foreach (var uid in (VariableStorage.EmailVar[EmailtoSaveVar].Item4 as IList<UniqueId>))
                    {
                        working_folder.MoveTo(uid, to_folder);
                    }
                }












                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }



}
