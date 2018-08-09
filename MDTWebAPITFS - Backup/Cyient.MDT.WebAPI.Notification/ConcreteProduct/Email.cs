using System;
using Cyient.MDT.WebAPI.Notification.Interface;
using Cyient.MDT.WebAPI.Notification.Product;
using Cyient.MDT.WebAPI.Core.Common;
using System.Net.Mail;
using System.Configuration;
namespace Cyient.MDT.WebAPI.Notification.ConcreteProduct
{
    public class Email : IMessager
    {

        public Email()
        {

        }
        /// <summary>
        /// This method will send the email notification to recipient
        /// </summary>
        /// <param name="sendMailRequest"> Pass SendMailRequest type object to process the request</param>
        /// <returns></returns>
        public bool SendNotification(SendMailRequest sendMailRequest)
        {
            MailMessage mailMessage = new MailMessage();
            try
            {
                // Setting To recipient
                string[] emailAddress = sendMailRequest.recipient.Split(',');
                foreach (var email in emailAddress)
                {
                    mailMessage.To.Add(email);
                }

                // Separate the cc array , if not null
                //if (sendMailRequest.cc != null)
                //{
                //    string[] cc_emailAddress = sendMailRequest.cc.Split(',');
                //    foreach (var email in cc_emailAddress)
                //    {
                //        mailMessage.CC.Add(email);
                //    }
                //}

                //// Include the reply to if not null
                //if (sendMailRequest.replyto != null)
                //{
                //    mailMessage.ReplyToList.Add(new MailAddress(sendMailRequest.replyto));
                //}

                // Include the file attachment if the filename is not null
                //if (sendMailRequest.filename != null)
                //{
                //    // Declare a temp file path where we can assemble our file
                //    string tempPath = Properties.Settings.Default["TempFile"].ToString();

                //    string filePath = Path.Combine(tempPath, sendMailRequest.filename);

                //    using (System.IO.FileStream reader = System.IO.File.Create(filePath))
                //    {
                //        byte[] buffer = Convert.FromBase64String(sendMailRequest.filecontent);
                //        reader.Write(buffer, 0, buffer.Length);
                //        reader.Dispose();
                //    }

                //msg.Attachments.Add(new Attachment(filePath));

                //}

                string sendFromEmail = ConfigurationManager.AppSettings["SendFromEmail"].ToString();
                string sendFromName = ConfigurationManager.AppSettings["SendFromName"].ToString();
                string sendFromPwd = SecurityEncryptDecrypt.Decrypt(ConfigurationManager.AppSettings["SendFromPwd"].ToString());
                mailMessage.From = new MailAddress(sendFromEmail, sendFromName);
                mailMessage.Subject = sendMailRequest.subject;
                mailMessage.Body = sendMailRequest.body;
                mailMessage.IsBodyHtml = true;

                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTPName"].ToString());
                client.Port = 25;
                //client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                System.Net.NetworkCredential nCred = new System.Net.NetworkCredential(sendFromEmail, sendFromPwd);
                client.Credentials = nCred;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mailMessage);
                //return "Email sent successfully to " + sendMailRequest.recipient.ToString();
                return true;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Password has been reset but unable to send an email due to some technical issue, please contact to administrator");
            }
            finally
            {
                mailMessage.Dispose();
            }
        }
    }
}