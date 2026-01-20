using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace Apps.Common
{
    public class EmailManager
    {

        private SmtpClient _objSmtpClient;
        private NetworkCredential _ntwrkCredential;
        EncryptDecrypt.cTripleDES objCrypt = new EncryptDecrypt.cTripleDES();

        public bool SendMailNoAttach(string[] mailTo, string subject, string Msgbody, string[] cc)
        {
            try
            {
                string _smtpHost = ConfigurationManager.AppSettings["SmtpServer"].ToString();
                int _smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"].ToString());
                string mailFrom = ConfigurationManager.AppSettings["SenderMailAdd"].ToString();
                string msgSubject = subject;
                string msgBody = Msgbody;
                int enableSSL = Convert.ToInt32(ConfigurationManager.AppSettings["EnableSSL"]);
                int setCredential = Convert.ToInt32(ConfigurationManager.AppSettings["SetCredential"]);
                string userName = ConfigurationManager.AppSettings["Username"].ToString();
                string password = objCrypt.Decrypt(ConfigurationManager.AppSettings["Password"].ToString());
                string domain = "";

                _objSmtpClient = new SmtpClient(_smtpHost, _smtpPort);

                //MailAddress addr = new MailAddress(userName);
                //string userNameOnly = addr.User;

                //set credential
                if (setCredential == 1)
                {
                    if (domain == "")
                        _ntwrkCredential = new NetworkCredential(userName, password);
                    else
                        _ntwrkCredential = new NetworkCredential(userName, password, domain);

                    //if (domain == "")
                    //    _ntwrkCredential = new NetworkCredential(userNameOnly, password);
                    //else
                    //    _ntwrkCredential = new NetworkCredential(userNameOnly, password, domain);

                    _objSmtpClient.Credentials = _ntwrkCredential;

                }
                //set enablessl
                if (enableSSL == 1)
                    _objSmtpClient.EnableSsl = true;


                MailMessage msg = new MailMessage();
                MailAddress receiver = new MailAddress(mailFrom);

                foreach (string mail in mailTo)
                {
                    msg.To.Add(new MailAddress(mail));
                }

                foreach (string mail in cc)
                {
                    msg.CC.Add(mail);
                }
                msg.From = receiver;
                msg.Subject = msgSubject;
                msg.Body = msgBody;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.High;
                
                #region By pass cert
                ServicePointManager.ServerCertificateValidationCallback =
                        delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                 System.Security.Cryptography.X509Certificates.X509Chain chain,
                                 System.Net.Security.SslPolicyErrors sslPolicyErrors)
                        { return true; }; 
                #endregion

                _objSmtpClient.Send(msg);
                return true;
            }
            //catch (SmtpFailedRecipientsException ex)
            //{
            //    return false;
            //}
            catch (SmtpException ex2)
            {
                Log.WriteLog(ex2, "EamilManager.cs", "SendMailNoAttach");
                return false;
            }
            catch (Exception Ex)
            {
                Log.WriteEmailLog(mailTo[0], StandardDefinition.SendEmailStatus.FAILED);
                Log.WriteLog(Ex, "EamilManager.cs", "SendMailNoAttach");
                return false;
            }
        }

        public bool SendMailWithAttach(string mailFrom,string[] mailTo, string subject, string Msgbody, string[] cc,string attachFileName)
        {
            try
            {
                string _smtpHost = ConfigurationManager.AppSettings["SmtpServer"].ToString();
                int _smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"].ToString());
                //string mailFrom = mailFrom;//ConfigurationManager.AppSettings["SenderMailAdd"].ToString();
                int enableSSL = Convert.ToInt32(ConfigurationManager.AppSettings["EnableSSL"]);
                int setCredential = Convert.ToInt32(ConfigurationManager.AppSettings["SetCredential"]);
                string userName = ConfigurationManager.AppSettings["Username"].ToString();
                string password = objCrypt.Decrypt(ConfigurationManager.AppSettings["Password"].ToString());
                string domain = "";
                string msgSubject = subject;
                string msgBody = Msgbody;
                
                _objSmtpClient = new SmtpClient(_smtpHost, _smtpPort);

                //set credential
                if (setCredential == 1)
                {
                    _objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    if (domain == "")
                        _ntwrkCredential = new NetworkCredential(userName, password);
                    else
                        _ntwrkCredential = new NetworkCredential(userName, password, domain);

                    _objSmtpClient.Credentials = _ntwrkCredential;
                }
                //set enablessl
                if (enableSSL == 1)
                    _objSmtpClient.EnableSsl = true;


                MailMessage msg = new MailMessage();
                MailAddress sender = new MailAddress(mailFrom);

                foreach (string mail in mailTo)
                {
                    msg.To.Add(new MailAddress(mail));
                }

                foreach (string mail in cc)
                {
                    msg.CC.Add(mail);
                }
                msg.From = sender;
                msg.Subject = msgSubject;
                msg.Body = msgBody;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.High;

                if (attachFileName != null)
                    msg.Attachments.Add(new Attachment(attachFileName));


                _objSmtpClient.Send(msg);

                msg.Attachments.Dispose();
                return true;
            }
            catch (Exception Ex)
            {
                Log.WriteEmailLog(mailTo[0], StandardDefinition.SendEmailStatus.FAILED);
                Log.WriteLog(Ex, "EmailManager.cs", "SendMailNoAttach");
                return false;
            }
        }

        public bool SendMailNoAttach2(string[] mailTo, string subject, string Msgbody, string[] cc)
        {
            try
            {
                string _smtpHost = ConfigurationManager.AppSettings["SmtpServer2"].ToString();
                int _smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort2"].ToString());
                string mailFrom = ConfigurationManager.AppSettings["SenderMailAdd2"].ToString();
                string msgSubject = subject;
                string msgBody = Msgbody;
                int enableSSL = Convert.ToInt32(ConfigurationManager.AppSettings["EnableSSL2"]);
                int setCredential = Convert.ToInt32(ConfigurationManager.AppSettings["SetCredential2"]);
                string userName = ConfigurationManager.AppSettings["Username2"].ToString();
                string password = objCrypt.Decrypt(ConfigurationManager.AppSettings["Password2"].ToString());
                string domain = "";

                _objSmtpClient = new SmtpClient(_smtpHost, _smtpPort);

                //MailAddress addr = new MailAddress(userName);
                //string userNameOnly = addr.User;

                //set credential
                if (setCredential == 1)
                {
                    if (domain == "")
                        _ntwrkCredential = new NetworkCredential(userName, password);
                    else
                        _ntwrkCredential = new NetworkCredential(userName, password, domain);

                    //if (domain == "")
                    //    _ntwrkCredential = new NetworkCredential(userNameOnly, password);
                    //else
                    //    _ntwrkCredential = new NetworkCredential(userNameOnly, password, domain);

                    _objSmtpClient.Credentials = _ntwrkCredential;

                }
                //set enablessl
                if (enableSSL == 1)
                    _objSmtpClient.EnableSsl = true;


                MailMessage msg = new MailMessage();
                MailAddress receiver = new MailAddress(mailFrom);

                foreach (string mail in mailTo)
                {
                    msg.To.Add(new MailAddress(mail));
                }

                foreach (string mail in cc)
                {
                    msg.CC.Add(mail);
                }
                msg.From = receiver;
                msg.Subject = msgSubject;
                msg.Body = msgBody;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.High;

                #region By pass cert
                ServicePointManager.ServerCertificateValidationCallback =
                        delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                 System.Security.Cryptography.X509Certificates.X509Chain chain,
                                 System.Net.Security.SslPolicyErrors sslPolicyErrors)
                        { return true; };
                #endregion

                _objSmtpClient.Send(msg);
                return true;
            }
            //catch (SmtpFailedRecipientsException ex)
            //{
            //    return false;
            //}
            catch (SmtpException ex2)
            {
                Log.WriteLog(ex2, "EamilManager.cs", "SendMailNoAttach");
                return false;
            }
            catch (Exception Ex)
            {
                Log.WriteEmailLog(mailTo[0], StandardDefinition.SendEmailStatus.FAILED);
                Log.WriteLog(Ex, "EamilManager.cs", "SendMailNoAttach");
                return false;
            }
        }

        public bool SendMailWithAttach2(string mailFrom, string[] mailTo, string subject, string Msgbody, string[] cc, string attachFileName)
        {
            try
            {
                string _smtpHost = ConfigurationManager.AppSettings["SmtpServer2"].ToString();
                int _smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort2"].ToString());
                //string mailFrom = mailFrom;//ConfigurationManager.AppSettings["SenderMailAdd2"].ToString();
                int enableSSL = Convert.ToInt32(ConfigurationManager.AppSettings["EnableSSL2"]);
                int setCredential = Convert.ToInt32(ConfigurationManager.AppSettings["SetCredential2"]);
                string userName = ConfigurationManager.AppSettings["Username2"].ToString();
                string password = objCrypt.Decrypt(ConfigurationManager.AppSettings["Password2"].ToString());
                string domain = "";
                string msgSubject = subject;
                string msgBody = Msgbody;

                _objSmtpClient = new SmtpClient(_smtpHost, _smtpPort);

                //set credential
                if (setCredential == 1)
                {
                    _objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    if (domain == "")
                        _ntwrkCredential = new NetworkCredential(userName, password);
                    else
                        _ntwrkCredential = new NetworkCredential(userName, password, domain);

                    _objSmtpClient.Credentials = _ntwrkCredential;
                }
                //set enablessl
                if (enableSSL == 1)
                    _objSmtpClient.EnableSsl = true;


                MailMessage msg = new MailMessage();
                MailAddress sender = new MailAddress(mailFrom);

                foreach (string mail in mailTo)
                {
                    msg.To.Add(new MailAddress(mail));
                }

                foreach (string mail in cc)
                {
                    msg.CC.Add(mail);
                }
                msg.From = sender;
                msg.Subject = msgSubject;
                msg.Body = msgBody;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.High;

                if (attachFileName != null)
                    msg.Attachments.Add(new Attachment(attachFileName));


                _objSmtpClient.Send(msg);

                msg.Attachments.Dispose();
                return true;
            }
            catch (Exception Ex)
            {
                Log.WriteEmailLog(mailTo[0], StandardDefinition.SendEmailStatus.FAILED);
                Log.WriteLog(Ex, "EmailManager.cs", "SendMailNoAttach");
                return false;
            }
        }

        public string EncryptPassword(string sPassword)
        {
            return objCrypt.Encrypt(sPassword);
        }

        public string DecryptPassword(string sHash)
        {
            return objCrypt.Decrypt(sHash);
        }
    }
}
