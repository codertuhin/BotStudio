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
//using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using MimeKit;
using MailKit.Net.Smtp;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Email
{


    [Serializable]
    public class SendMessages : RobotActionBase, ITask
    {
        public string From { get; set; }
        public string SenderDisplayName { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool BodyAsHTML { get; set; }
        public string Attachment { get; set; }
        public string SMTPServer = "smtp.gmail.com";
        public int Port = 587;
        public string Password { get; set; }

        //string fromPassword = "10800219?";
        //string fromPassword = "c99c++101*/";
        public int Execute()
        {
            try
            {



                string FromAddress = From;      //alauddin.setu@gmail.com";
                string FromAdressTitle = SenderDisplayName;
                //To Address  
                string ToAddress = To;
                string ToAdressTitle = "";


                //FromAddress = "alauddin.setu@gmail.com";
                //ToAddress = "rzrt.edu@gmail.com";

                //Smtp Server  
                string SmtpServer = SMTPServer;  //"smtp.gmail.com";
                //Smtp Port Number  
                int SmtpPortNumber = Port;  //587;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
                mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, ToAddress));
                mimeMessage.Subject = Subject;
                if (!string.IsNullOrEmpty(CC))
                {
                    mimeMessage.Cc.Add(new MailboxAddress(CC));

                }
                if (!string.IsNullOrEmpty(BCC))
                {
                    mimeMessage.Bcc.Add(new MailboxAddress(BCC));

                }


                var builder = new BodyBuilder();

                if (!string.IsNullOrEmpty(Body))
                {
                    builder.TextBody = @Body;
                }


                if (!string.IsNullOrEmpty(Attachment))
                {
                    builder.Attachments.Add(@Attachment);

                }

                // Now we just need to set the message body and we're done

                mimeMessage.Body = builder.ToMessageBody();
                using (var client = new SmtpClient())
                {

                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    // Note: only needed if the SMTP server requires authentication  
                    // Error 5.5.1 Authentication   
                    client.Authenticate(FromAddress, Password);
                    client.Send(mimeMessage);
                    // Console.WriteLine("The mail has been sent successfully !!");
                    //Console.ReadLine();
                    client.Disconnect(true);

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
