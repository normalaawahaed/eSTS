using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using eSTSEmailServices.Database;
using System.Configuration;
using Apps.Common;
using System.Data.Entity.Validation;
using Newtonsoft.Json;
using System.Data;
using Newtonsoft.Json.Linq;
namespace eSTSEmailServices
{
    public class SendEmailService
    {
        internal bool execSTSSendEmail()
        {
            try
            {
                using (eSTS_LiveEntities dbContext = new eSTS_LiveEntities())
                {
                    //Guid refid = new Guid("a35ab7fc-a069-47d1-8e05-435f0600c083");
                    //List<PendingEmail> lPendingEmail = dbContext.PendingEmails.Where(w =>
                    //  w.RefID == refid).OrderBy(w => w.LogDate).ToList<PendingEmail>();
                    List<PendingEmail> lPendingEmail = dbContext.PendingEmails.Where(w =>
                    w.IsSend != true).OrderBy(w => w.LogDate).ToList<PendingEmail>();

                    foreach (PendingEmail email in lPendingEmail)
                    {
                        if (SendEmailSTS2(email.RefID, email.RefFlowID, email.IsReject))
                        {
                            Console.WriteLine("Process pending transaction.....");
                            email.IsSend = true;
                            email.SendDate = DateTime.Now;

                            dbContext.SaveChanges();
                            Console.WriteLine("Sent.....");
                        }
                    }

                    // List<EmailSendLog> lEmailSend = dbContext.EmailSendLogs.Where(w =>
                    //w.IsSend != true).OrderBy(w => w.LogDate).ToList<EmailSendLog>();

                    // foreach (EmailSendLog email in lEmailSend)
                    // {
                    //     if (sendEmail(email.RefID.ToString(), email.EmailSubject, email.EmailBody, email.EmailTo))

                    //         Console.WriteLine("Sending success..");
                    //     email.IsSend = true;
                    //     email.SendDate = DateTime.Now;

                    //     dbContext.SaveChanges();
                    // }
                    // Console.WriteLine("Process complete..");
                    dbContext.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                //return a message for reason of fail
                Console.WriteLine("Sending failed..");
                //Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return false;
            }
        }
        public static bool SendEmailSTS2(Guid? operationAppID, Guid? FlowActionStatusID, bool? isReject)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            bool result = false;
            try
            {
                using (eSTS_LiveEntities dbContext = new eSTS_LiveEntities())
                {

                    SystemParam sysParam = dbContext.SystemParams.FirstOrDefault<SystemParam>();

                    v_OperationApp appInfo = dbContext.v_OperationApp.Where(w => w.OperationAppID == operationAppID).FirstOrDefault<v_OperationApp>();

                    v_UsersSASO v_BunkerAgentUser = dbContext.v_UsersSASO.Where(w => w.ROCNO == appInfo.CompID && w.UserID == appInfo.CreatedBy).FirstOrDefault<v_UsersSASO>();

                    v_LicCompany v_LicCompany = dbContext.v_LicCompany.Where(w => w.LicCompanyID == appInfo.SOLicID).FirstOrDefault<v_LicCompany>();

                    List<v_opGroupEmail> lGroupMails = dbContext.v_opGroupEmail.Where(w => w.FlowActionStatusID == FlowActionStatusID).ToList<v_opGroupEmail>();

                    foreach (v_opGroupEmail groupMail in lGroupMails)
                    {
                        string mailReceipient = "";
                        EmailTemplate emailTemplate = dbContext.EmailTemplates.Where(w => w.EmailTempID == groupMail.EmailTempID).FirstOrDefault<EmailTemplate>();

                        if (groupMail.IsApplicant == true)
                        {
                            mailReceipient = v_BunkerAgentUser.EmailAdd;
                        }
                        else if (groupMail.IsOperator == true)
                        {
                            mailReceipient = v_LicCompany.EmailAddress;
                        }
                        else if (groupMail.PermitIssuerID != null)
                        {
                            if (groupMail.PermitIssuerID == appInfo.PermitIssuerID)
                            {
                                List<v_AdminUsers> lUserList = dbContext.v_AdminUsers.Where(w => w.AccessGroupID == groupMail.ReceiptAGID && w.IsActive == true).ToList<v_AdminUsers>();

                                foreach (v_AdminUsers user in lUserList)
                                {
                                    mailReceipient = mailReceipient + ";" + user.EmailAddress;
                                }

                            }
                            if (mailReceipient != "")
                                mailReceipient = mailReceipient.Substring(1, mailReceipient.Length - 1);


                        }
                        else if (groupMail.ReceiptAG1.ToString().ToUpper() == "ELPJT")
                        {
                            List<v_AdminUsers> lUserList = dbContext.v_AdminUsers.Where(w => w.AccessGroupID == groupMail.ReceiptAGID && w.IsActive == true).ToList<v_AdminUsers>();

                            foreach (v_AdminUsers user in lUserList)
                            {
                                mailReceipient = mailReceipient + ";" + user.EmailAddress;
                            }
                            if (mailReceipient != "")
                                mailReceipient = mailReceipient.Substring(1, mailReceipient.Length - 1);

                        }
                        else
                        {
                            if (groupMail.PortLoc != null)
                            {
                                if (groupMail.PortLoc == appInfo.Location)
                                {
                                    List<v_AdminUsers> lUserList = dbContext.v_AdminUsers.Where(w => w.AccessGroupID == groupMail.ReceiptAGID && w.IsActive == true).ToList<v_AdminUsers>();

                                    foreach (v_AdminUsers user in lUserList)
                                    {
                                        mailReceipient = mailReceipient + ";" + user.EmailAddress;
                                    }
                                    if (mailReceipient != "")
                                        mailReceipient = mailReceipient.Substring(1, mailReceipient.Length - 1);
                                }

                            }
                            else if (groupMail.IsEmailGroup == true)
                            {
                                mailReceipient = groupMail.EmailGroup;

                            }
                            else
                            {
                                List<v_AdminUsers> lUserList = dbContext.v_AdminUsers.Where(w => w.AccessGroupID == groupMail.ReceiptAGID && w.IsActive == true).ToList<v_AdminUsers>();

                                foreach (v_AdminUsers user in lUserList)
                                {
                                    mailReceipient = mailReceipient + ";" + user.EmailAddress;
                                }
                                if (mailReceipient != "")
                                    mailReceipient = mailReceipient.Substring(1, mailReceipient.Length - 1);

                            }
                        }

                        if (mailReceipient != "")
                        {
                            string emailSubject = emailTemplate.TempSubject;
                            string emailBody = emailTemplate.TempBody;
                            //if (isReject == true)
                            //{
                            //    emailSubject = emailSubject.Replace("[Action]", "Rejected");
                            //    emailBody = emailBody.Replace("[action]", "rejected");
                            //    emailBody = emailBody.Replace("[ApprovalAction]", "Rejection");
                            //    emailBody = emailBody.Replace("[RejectReason]", "Reason : " + appInfo.RejectRemark);
                            //    emailBody = emailBody.Replace("[ApprovedDate]", appInfo.RejectedDate.Value.ToString("dd/MM/yyyy hh:mm tt"));
                            //}
                            //else
                            //{
                            //    emailSubject = emailSubject.Replace("[Action]", "Verification");
                            //    emailBody = emailBody.Replace("[Action]", "verification");
                            //    emailBody = emailBody.Replace("[action]", "verified");
                            //    emailBody = emailBody.Replace("[ApprovalAction]", "Verified");
                            //    emailBody = emailBody.Replace("[RejectReason]", "");
                            //    if (appInfo.CompletedDate != null)
                            //        emailBody = emailBody.Replace("[ApprovedDate]", appInfo.CompletedDate.Value.ToString("dd/MM/yyyy hh:mm tt"));
                            //}
                            //if (appInfo.PermitIssuerID != null)
                            //    emailBody = emailBody.Replace("[PermitIssuer]", textInfo.ToTitleCase(appInfo.PermitIssuer.ToLower()));

                            emailBody = emailBody.Replace("[CaseNum]", appInfo.CaseNum);
                            emailBody = emailBody.Replace("[Location]", appInfo.DeliveryLocation);
                            if (appInfo.MethodCode == "A")
                            {
                                emailBody = emailBody.Replace("[FSUName]", textInfo.ToTitleCase(appInfo.VRName.ToLower()));
                                emailBody = emailBody.Replace("[VesselName]", textInfo.ToTitleCase(appInfo.VSName.ToLower()));
                                emailBody = emailBody.Replace("[IMONO]", textInfo.ToTitleCase(appInfo.VSIMONo.ToLower()));
                                if (appInfo.VSMMSINo != null)
                                    emailBody = emailBody.Replace("[OFFNO]", textInfo.ToTitleCase(appInfo.VSMMSINo.ToLower()));
                                else
                                    emailBody = emailBody.Replace("[OFFNO]", "");
                                emailBody = emailBody.Replace("[LatLong]", "Lat(DMS) :" + appInfo.VRLatDegree + "° " + appInfo.VRLatMin + "'N Long(DMS) :" + appInfo.VRLongDegree + "° " + appInfo.VRLongMin + "'E");
                            }
                            else
                            {
                                emailBody = emailBody.Replace("[FSUName]", textInfo.ToTitleCase(appInfo.VSName.ToLower()));
                                emailBody = emailBody.Replace("[VesselName]", textInfo.ToTitleCase(appInfo.VRName.ToLower()));
                                emailBody = emailBody.Replace("[IMONO]", textInfo.ToTitleCase(appInfo.VRIMONo.ToLower()));
                                if (appInfo.VRMMSINo != null)
                                    emailBody = emailBody.Replace("[OFFNO]", textInfo.ToTitleCase(appInfo.VRMMSINo.ToLower()));
                                else
                                    emailBody = emailBody.Replace("[OFFNO]", "");
                                emailBody = emailBody.Replace("[LatLong]", "Lat(DMS) :" + appInfo.VSLatDegree + "° " + appInfo.VSLatMin + "'N Long(DMS) :" + appInfo.VSLongDegree + "° " + appInfo.VSLongMin + "'E");
                            }


                          //  emailBody = emailBody.Replace("[PermitValidity]", appInfo.EstOperationTime.Value.ToString("dd/MM/yyyy HH:mm") + " - " + appInfo.ValidPermit.Value.ToString("dd/MM/yyyy HH:mm"));
                            emailBody = emailBody.Replace("[AgentName]", appInfo.CompanyName);
                            emailBody = emailBody.Replace("[RejectReason]", textInfo.ToTitleCase(appInfo.DeliveryLocation.ToLower()));


                            emailBody = emailBody.Replace("[SysURL]", sysParam.SysURL);
                            EmailSendLog emailLog = new EmailSendLog();
                            emailLog.EmailSendLogID = Guid.NewGuid();
                            emailLog.RefID = new Guid?(appInfo.OperationAppID);
                            emailLog.EmailFrom = v_BunkerAgentUser.EmailAdd;
                            emailLog.EmailTo = mailReceipient;
                            emailLog.EmailTempID = groupMail.EmailTempID;
                            // emailLog.EmailSubject = "FOR TESTING - "+ emailSubject;
                            emailLog.EmailSubject =  emailSubject;
                            emailLog.EmailBody = emailBody;
                            emailLog.LogDate = new DateTime?(DateTime.Now);

                            string attachment = "";
                            //if (emailTemplate.IsAttachment == true)
                            //    attachment =  ConfigurationManager.AppSettings["UploadFolderPath"].ToString() + appInfo.PermitDocLink.Replace("~", "");

                            if (sendEmail(appInfo.OperationAppID.ToString(), emailSubject, emailBody, emailLog.EmailTo, attachment))
                            {
                                emailLog.IsSend = true;
                            emailLog.SendDate = DateTime.Now;
                            dbContext.EmailSendLogs.Add(emailLog);
                            dbContext.SaveChanges();
                                result = true;
                                Console.WriteLine("Sending success..");
                            }
                            else
                            {
                                result = false;
                            }

                        }
                    }
                }
               
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
               
                Log.WriteLog(ex, "send email", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + "_" + operationAppID.ToString());
                result = false;
                //return result;
            }
           
            return result;
        }
        private static bool sendEmail(string refID, string subject, string content, string recipientEmail, string attachment)
        {
            try
            {
                using (eSTS_LiveEntities dbContext = new eSTS_LiveEntities())
                {

                    TblMailServer mailSvr = dbContext.TblMailServers.FirstOrDefault();

                    if (mailSvr == null) return false;

                    // setup sending mail
                    SmtpClient client = new SmtpClient(mailSvr.SMTPAddress, (int)mailSvr.SMTPPort);
                    // client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(mailSvr.SMTPUsername, mailSvr.SMTPPassword);
                    client.EnableSsl = Convert.ToBoolean(mailSvr.SMTPRequiredSSL);

                    if (Convert.ToBoolean(mailSvr.SMTPRequiredSSL))
                        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                    MailMessage mailMessage = new MailMessage();
                    //mailMessage.From = new MailAddress(mailSvr.SMTPUsername);
                    mailMessage.From = new MailAddress(mailSvr.SMTPAddressIncoming);

                    string[] lMailTo = recipientEmail.Split(';');

                    foreach (var mailTo in lMailTo)
                    {
                        mailMessage.To.Add(mailTo);
                    }
                    //if (attachment != "")
                    //{
                    //    mailMessage.Attachments.Add(new Attachment(attachment));
                    //}

                    //mailMessage.To.Add("nazri@lpj.gov.my");
                    mailMessage.Priority = MailPriority.Normal;

                    mailMessage.Subject = subject;
                    mailMessage.Body = content;

                    mailMessage.IsBodyHtml = true;

                    // send email
                    client.Send(mailMessage);
                }

                Log.WriteMessageLog("Success Send", "WebAPI", "SendEmail() - "+ refID);
                return true;


            }
            catch (Exception ex)
            {
                //return a message for reason of fail
                Log.WriteLog(ex, "send email", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + "_" + refID);
                return false;
            }
        }

        internal bool SendingWhatsapp()
        {
            try
            {
                    using (WebClient wc = new WebClient())
                    {
                        string json = wc.DownloadString("http://bulk.blaster-pro.com/sendAPI.php?apikey=33b3bc47db5e274d1f09541386adb49f&number=60177373394&message=burung puyuh di makan biawak..kadang cantik kadang imut ahahaha");
                        DataSet ds = JObject.Parse(json)["root"].ToObject<DataSet>();
                        Console.WriteLine("Sent..");
                }
                
            }
            catch (Exception ex)
            {
                //return a message for reason of fail
                Console.WriteLine("Sending failed..");
                //Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return false;
            }
            return true;
        }
    }
}
