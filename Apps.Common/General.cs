using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Apps.Common
{
    public class General
    {
        public enum MsgType
        {
            SaveErr, SaveSucc, Exist, FieldMandatory, Warning, Info, SubmitApproveSucc, SubmitApproveErr, RejectSucc, RejectErr, ApproveSucc, ApproveErr, EndorseSucc, EndorseErr, SendMailErr, Error
        }

        public static void SetLabelMessage(ref Label  lblMsg, string Page, MsgType msgType)
        {
            switch (msgType)
            {
                case MsgType.SaveErr:
                    {
                        lblMsg.CssClass = "text-red";
                        lblMsg.Text = "Unable to save " + Page + ". Please contact administrator. ";
                        lblMsg.Visible = true;
                        lblMsg.Focus();
                    }
                    break;
                case MsgType.SaveSucc:
                    {
                        lblMsg.CssClass = "text-green";
                        lblMsg.Text = Page + " has been saved successfully. ";
                        lblMsg.Visible = true;
                        lblMsg.Focus();
                    }
                    break;
                case MsgType.Exist:
                    {
                        //lblMsg.ForeColor = System.Drawing.Color.Red;
                        //lblMsg.Text = Page + " already exist! ";
                        //lblMsg.Visible = true;
                        //lblMsg.Focus();
                    }
                    break;
                case MsgType.FieldMandatory:
                    {
                        //    lblMsg.ForeColor = System.Drawing.Color.Red;
                        //    lblMsg.Text = "Please enter " + Page + ". ";
                        //    lblMsg.Visible = true;
                        //    lblMsg.Focus();
                        }
                        break;
                case MsgType.Warning:
                    {
                        //lblMsg.ForeColor = System.Drawing.Color.Red;
                        //lblMsg.Text = Page;
                        //lblMsg.Visible = true;
                        //lblMsg.Focus();
                    }
                    break;
                case MsgType.Info:
                    {
                        //lblMsg.ForeColor = System.Drawing.Color.Blue;
                        //lblMsg.Text = Page;
                        //lblMsg.Visible = true;
                        //lblMsg.Focus();
                    }
                    break;
                case MsgType.SubmitApproveErr:
                    {
                        //lblMsg.ForeColor = System.Drawing.Color.Red;
                        //lblMsg.Text = "Unable to Submit " + Page + " for approval. Please contact administrator.";
                        //lblMsg.Visible = true;
                        //lblMsg.Focus();
                    }
                    break;
                case MsgType.SubmitApproveSucc:
                    {
                        //lblMsg.ForeColor = System.Drawing.Color.Blue;
                        //lblMsg.Text = Page + " has been submit for approval. Approvers will be notified via email.";
                        //lblMsg.Visible = true;
                        //lblMsg.Focus();
                    }
                    break;
                case MsgType.RejectErr:
                    {
                        //lblMsg.ForeColor = System.Drawing.Color.Red;
                        //lblMsg.Text = "Unable to Reject the " + Page + ". Please contact administrator.";
                        //lblMsg.Visible = true;
                        //lblMsg.Focus();
                    }
                    break;
                case MsgType.RejectSucc:
                    {
                        //lblMsg.ForeColor = System.Drawing.Color.Blue;
                        //lblMsg.Text = Page + " has been reject.";
                        //lblMsg.Visible = true;
                        //lblMsg.Focus();
                    }
                    break;
                case MsgType.ApproveErr:
                    {
                        //lblMsg.ForeColor = System.Drawing.Color.Red;
                        //lblMsg.Text = "Unable to Approve the " + Page + ". Please contact administrator.";
                        //lblMsg.Visible = true;
                        //lblMsg.Focus();
                    }
                    break;
                case MsgType.ApproveSucc:
                    {
                        //lblMsg.ForeColor = System.Drawing.Color.Blue;
                        //lblMsg.Text = Page + " has been approve.";
                        //lblMsg.Visible = true;
                        //lblMsg.Focus();
                    }
                    break;
                case MsgType.EndorseErr:
                    {
                        //lblMsg.ForeColor = System.Drawing.Color.Red;
                        //lblMsg.Text = "Unable to Endorse the " + Page + ". Please contact administrator.";
                        //lblMsg.Visible = true;
                        //lblMsg.Focus();
                    }
                    break;
                case MsgType.EndorseSucc:
                    {
                        //lblMsg.ForeColor = System.Drawing.Color.Blue;
                        //lblMsg.Text = Page + " has been endorse.";
                        //lblMsg.Visible = true;
                        //lblMsg.Focus();
                    }
                    break;
                case MsgType.SendMailErr:
                    {
                        //lblMsg.ForeColor = System.Drawing.Color.Red;
                        //lblMsg.Text = "Failed to send email to approver. Please contact Administrator";
                        //lblMsg.Visible = true;
                        //lblMsg.Focus();
                    }
                    break;
                case MsgType.Error:
                    {
                        lblMsg.CssClass = "text-red";
                        lblMsg.Text = Page;
                        lblMsg.Visible = true;
                        lblMsg.Focus();
                    }
                    break;
            }
        }

        //public static void DisabledControl(ControlCollection controls)
        //{

        //    foreach (Control ctrl in controls)
        //    {
        //        if (ctrl is ASPxTextBox)
        //        {
        //            ASPxTextBox txt = (ASPxTextBox)ctrl;
        //            txt.ReadOnly = true;
        //            txt.ForeColor = System.Drawing.Color.Black;
        //        }
        //        if (ctrl is ASPxButton)
        //        {
        //            ASPxButton btn = (ASPxButton)ctrl;
        //            btn.Enabled = false;
        //        }

        //        if (ctrl is ASPxPanel)
        //        {
        //            ASPxPanel pnl = (ASPxPanel)ctrl;
        //            DisabledControl(pnl.Controls);
        //        }
        //        if (ctrl is ASPxMemo)
        //        {
        //            ASPxMemo mem = (ASPxMemo)ctrl;
        //            mem.ReadOnly = true;
        //            mem.ForeColor = System.Drawing.Color.Black;
        //        }
        //    }
        //}

        //public static void HideControl(ControlCollection controls)
        //{

        //    foreach (Control ctrl in controls)
        //    {
        //        if (ctrl is ASPxTextBox)
        //        {
        //            ASPxTextBox txt = (ASPxTextBox)ctrl;
        //            txt.ReadOnly = true;
        //            txt.ForeColor = System.Drawing.Color.Black;
        //        }
        //        if (ctrl is ASPxButton)
        //        {
        //            ASPxButton btn = (ASPxButton)ctrl;
        //            btn.Visible = false;
        //        }

        //        if (ctrl is ASPxPanel)
        //        {
        //            ASPxPanel pnl = (ASPxPanel)ctrl;
        //            HideControl(pnl.Controls);
        //        }
        //        if (ctrl is ASPxMemo)
        //        {
        //            ASPxMemo mem = (ASPxMemo)ctrl;
        //            mem.ReadOnly = true;
        //            mem.ForeColor = System.Drawing.Color.Black;
        //        }
        //    }
        //}

        //public static void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        //{
        //    try
        //    {
        //        if (errors.ContainsKey(column))
        //        {
        //            return;
        //        }
        //        errors[column] = errorText;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteLog(ex, "ppsb.Common", "AddError");
        //    }
        //}

        //public static string GenerateAutoId(int no)
        //{
        //    string autoNo = "";
        //    try
        //    {
        //        int newNo = no + 1;
        //        if (newNo < 10)
        //        {
        //            autoNo = "000" + newNo.ToString();
        //        }
        //        else if (newNo < 100)
        //        {
        //            autoNo = "00" + newNo.ToString();
        //        }
        //        else if (newNo < 1000)
        //        {
        //            autoNo = "0" + newNo.ToString();
        //        }
        //        else if (newNo < 10000)
        //        {
        //            autoNo = newNo.ToString();
        //        }
        //        else
        //            autoNo = "0001";

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return autoNo;
        //}

        //public static bool IsValidEmail(string email)
        //{
        //    try
        //    {
        //        var addr = new System.Net.Mail.MailAddress(email);
        //        return addr.Address == email;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}
