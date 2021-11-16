using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace DemoTest.DemoS
{
    public class HtmlFormation
    {
        public static void FormHtml()
        {
            var image = "https://gbdev.s3.amazonaws.com/uat/product/EGVGBLAZSG001/d/mobile/403_microsite.png";
            var htmlCont = new StringBuilder();
            htmlCont.Append("<html><body>");
            htmlCont.Append("<h2>Hi Receiver</h2>");
            htmlCont.Append("<h4>You've got Kotak E-Card Gift</h4>");
            htmlCont.Append("<h4></h4>");
            htmlCont.Append("<hr />");
            htmlCont.Append("<img src= '" + image + "' />");
            htmlCont.Append("<br />");
            htmlCont.Append("<h1>Rs. 50.00</h1>");
            htmlCont.Append("<h5>Card Number</h5>");
            htmlCont.Append("<h5>xxxxxxxxxxxxxxxx</h5>");
            htmlCont.Append("<h5>Pin</h5>");
            htmlCont.Append("<h5>xxxxxx</h5>");
            htmlCont.Append("</body></html>");
            Console.WriteLine(htmlCont);
            //SendEmailProcess(htmlCont.ToString());
        }

        public static void SendEmailProcess(string htmCont)
        {
            try
            {
                NetworkCredential objNetworkCredential = new NetworkCredential("server21138", "b9W8Bxm4N3Zej5R6Ftw");
                NetworkCredential objNetworkCredentialAuth = objNetworkCredential.GetCredential("smtp.socketlabs.com", Int32.Parse("2525"), "Basic");
                using (MailMessage objMailMessage = new MailMessage())
                {
                    using (SmtpClient objSmtpClient = new SmtpClient())
                    {
                        objSmtpClient.Host = "smtp.socketlabs.com";
                        objSmtpClient.Port = Int32.Parse("2525");
                        objSmtpClient.Credentials = objNetworkCredential;
                        //Add SocketLabs MessageID and MailingID [ https://support.socketlabs.com/kb/48 ]
                        objMailMessage.Headers.Add("X-xsMessageId", "SSIPL EasyRewardz Account Summary");
                        objMailMessage.Headers.Add("X-xsMailingId", "aashish@easyrewardz.com");

                        objMailMessage.From = new MailAddress("storepaysupport@easyrewardz.com");

                        if (!string.IsNullOrEmpty("aashish@easyrewardz.com"))
                        {
                            if ("aashish@easyrewardz.com".Contains(";"))
                            {
                                string[] ToEmailAddresses = "aashish@easyrewardz.com".Split(';');
                                foreach (string ToAddress in ToEmailAddresses)
                                {
                                    if (!string.IsNullOrEmpty(ToAddress))
                                    {
                                        objMailMessage.To.Add(new MailAddress(ToAddress));
                                    }
                                }
                            }
                            else
                            {
                                objMailMessage.To.Add(new MailAddress("aashish@easyrewardz.com"));
                            }
                        }

                        objMailMessage.Subject = "ManualRefund";
                        //objMailMessage.Body = "Hi " + _config.email.Body + clientConfig.PaymentNotificationUrl + ", Below are the transaction :" + string.Join(',', failedTxnList);
                        string message = "Cancellation of Order {0} of Amount Rs {1} is pending due to some technical failure.\nPlease initiate it to complete manually.";
                        string mailmessage = "Hi";
                        string mailBody = "Hi this is test mail";
                        objMailMessage.Body = mailmessage;
                        //if (!string.IsNullOrEmpty(Attachment))
                        //{
                        //    objMailMessage.Attachments.Add(new System.Net.Mail.Attachment(Attachment));
                        //}
                        objMailMessage.IsBodyHtml = true;
                        objSmtpClient.Send(objMailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Exception found while send mail to client for mannual refund where Mail Configuration : {JsonConvert.SerializeObject(clientConfig)}");
            }
        }
    }
}
