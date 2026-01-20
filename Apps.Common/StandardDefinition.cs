using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace Apps.Common
{
    public class StandardDefinition
    {
        #region Session
        //Session["UserID"] = "UserID";
        //Session["UserFullName"] = "UserFullName";
        //Session["UserGroup"] = "UserGroup"; 
        //Session["UserIsAdmin"] = "UserIsAdmin"; 
        #endregion

        public enum LogType
        {
            UserAccess, Approval, LockAccount, CompanyProfile, Email, Operation
        }

        public enum AccessType
        {
            Login, Logout, ChangePassword, ForgotPassword
        }

        public enum LogOperationActivity
        {
            Insert, Update, Delete, ExecuteSP, Generate, Posting, ReOpen, Select
        }

        public enum SendEmailStatus
        {
            SENT, FAILED
        }

        public enum ApprovalStatus
        {
            [Description("Submit")]
            submit = 0,
            [Description("Approved")]
            approved = 1,
            [Description("Rejected")]
            rejected = 2
        }

        public enum VerificationStatus
        {
            [Description("Submit")]
            submit = 0,
            [Description("Approved")]
            approved = 1,
            [Description("Rejected")]
            rejected = 2
        }
    }
}
