using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace Apps.Common
{
    public class Log
    {
        public static void WriteLog(Exception Ex__1, string Page, string Method)
        {
            try
            {
                DateTime d = DateTime.Now;
                string LogFileName = "ErrorLog_" + d.Year + "_" + d.Month + "_" + d.Day + ".txt";
                string ApplicationPath = AppDomain.CurrentDomain.BaseDirectory.Trim() + "Logs\\";
                if (!Directory.Exists(ApplicationPath))
                {
                    Directory.CreateDirectory(ApplicationPath);
                }
                if (ApplicationPath.Length == 0)
                {
                    return;
                }

                using (StreamWriter sw = new StreamWriter(ApplicationPath + LogFileName, true))
                {
                    sw.WriteLine("Exception Message           : " + Ex__1.Message);
                    sw.WriteLine("Details                     : " + Ex__1.ToString());
                    sw.WriteLine("Exception Source            : " + Ex__1.Source);
                    sw.WriteLine("Time of Occurence           : " + d.ToString());
                    sw.WriteLine("Page                        : " + Page);
                    sw.WriteLine("Method                      : " + Method);
                    sw.WriteLine("-------------------------------------------------------------------------------");
                }
            }
            catch (Exception)
            {
            }
        }
        public static void WriteLog(Exception Ex__1, string Page, string Method, string key)
        {
            try
            {
                DateTime d = DateTime.Now;
                string LogFileName = "ErrorLog_" + d.Year + "_" + d.Month + "_" + d.Day + ".txt";
                string ApplicationPath = AppDomain.CurrentDomain.BaseDirectory.Trim() + "Logs\\";
                if (!Directory.Exists(ApplicationPath))
                {
                    Directory.CreateDirectory(ApplicationPath);
                }
                if (ApplicationPath.Length == 0)
                {
                    return;
                }

                using (StreamWriter sw = new StreamWriter(ApplicationPath + LogFileName, true))
                {
                    sw.WriteLine("Exception Message           : " + Ex__1.Message);
                    sw.WriteLine("Details                     : " + Ex__1.ToString());
                    sw.WriteLine("Exception Source            : " + Ex__1.Source);
                    sw.WriteLine("Time of Occurence           : " + d.ToString());
                    sw.WriteLine("Page                        : " + Page);
                    sw.WriteLine("Method                      : " + Method);
                    sw.WriteLine("Key                      : " + key);
                    sw.WriteLine("-------------------------------------------------------------------------------");
                }
            }
            catch (Exception)
            {
            }
        }
        public static void WriteUserAccessLog(string UserID, string UserGroup, StandardDefinition.AccessType accessType)
        {
            try
            {
                DALLogDB objMain = new DALLogDB();

                objMain._BL.LogActivityType = StandardDefinition.LogType.UserAccess.ToString();
                objMain._BL.LogActivity = accessType.ToString();
                objMain._BL.LogDatetime = DateTime.Now;
                objMain._BL.LogRemark = "UserGroup:" + UserGroup;
                objMain._BL.Logger = UserID;

                if (objMain.Insert())
                    return;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void WriteLogOperation(string UserID, string Module, string TableName, string RefID, StandardDefinition.LogOperationActivity Activity)
        {
            try
            {
                DALLogDB objMain = new DALLogDB();

                objMain._BL.LogActivityType = StandardDefinition.LogType.Operation.ToString();
                objMain._BL.LogActivity = Activity.ToString();
                objMain._BL.LogDatetime = DateTime.Now;
                objMain._BL.LogRemark = "Module:" + Module + ", Table:" + TableName + ", Ref ID:" + RefID;
                objMain._BL.Logger = UserID;

                if (objMain.Insert())
                    return;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void WriteEmailLog(string UserID, StandardDefinition.SendEmailStatus status)
        {
            try
            {
                DALLogDB objMain = new DALLogDB();
                objMain._BL.LogActivity = status.ToString();
                objMain._BL.LogActivityType = StandardDefinition.LogType.Email.ToString();
                objMain._BL.LogDatetime = DateTime.Now;
                objMain._BL.Logger = UserID;

                if (objMain.Insert())
                    return;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void WriteEmailLog(string UserID, StandardDefinition.SendEmailStatus status, string Remarks)
        {
            try
            {
                DALLogDB objMain = new DALLogDB();
                objMain._BL.LogActivity = status.ToString();
                objMain._BL.LogActivityType = StandardDefinition.LogType.Email.ToString();
                objMain._BL.LogDatetime = DateTime.Now;
                objMain._BL.LogRemark = Remarks;
                objMain._BL.Logger = UserID;

                if (objMain.Insert())
                    return;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void WriteServiceLog(string ServiceName, DateTime runDate,string Module, string RefID,string EmailAdd )
        {
            try
            {
                DateTime d = DateTime.Now;
                string LogFileName = "ServiceRun_" + d.Year + "_" + d.Month + "_" + d.Day + ".txt";
                string ApplicationPath = AppDomain.CurrentDomain.BaseDirectory.Trim() + "Logs\\";
                if (!Directory.Exists(ApplicationPath))
                {
                    Directory.CreateDirectory(ApplicationPath);
                }
                if (ApplicationPath.Length == 0)
                {
                    return;
                }

                using (StreamWriter sw = new StreamWriter(ApplicationPath + LogFileName, true))
                {
                    sw.WriteLine("ServiceName                 : " + ServiceName);
                    sw.WriteLine("Run Date/Time               : " + runDate);
                    sw.WriteLine("Receipient                  : " + EmailAdd);
                    sw.WriteLine("RefID                       : " + RefID);
                    sw.WriteLine("-------------------------------------------------------------------------------");
                }
            }
            catch (Exception)
            {
            }
        }

        public static void WriteMessageLog(string msg, string Page, string Method)
        {
            try
            {
                DateTime d = DateTime.Now;
                string LogFileName = "MessageLog_" + d.Year + "_" + d.Month + "_" + d.Day + ".txt";
                string ApplicationPath = AppDomain.CurrentDomain.BaseDirectory.Trim() + "Logs\\";
                if (!Directory.Exists(ApplicationPath))
                {
                    Directory.CreateDirectory(ApplicationPath);
                }
                if (ApplicationPath.Length == 0)
                {
                    return;
                }

                using (StreamWriter sw = new StreamWriter(ApplicationPath + LogFileName, true))
                {
                    sw.WriteLine("Message           : " + msg);
                    sw.WriteLine("Time of Occurence           : " + d.ToString());
                    sw.WriteLine("Page                        : " + Page);
                    sw.WriteLine("Method                      : " + Method);
                    sw.WriteLine("-------------------------------------------------------------------------------");
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
