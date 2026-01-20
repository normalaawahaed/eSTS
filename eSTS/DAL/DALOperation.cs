using Apps.Common;
using eSTS.Common;
using eSTS.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Configuration;

namespace eSTS.DAL
{
    public class DALOperation
    {
        private string connectionstring = ConfigurationManager.ConnectionStrings["STSConnectionString"].ToString();

        private SqlConnection sqlCon = new SqlConnection();

        private SqlCommand cmd = new SqlCommand();

        private SqlDataAdapter ad = new SqlDataAdapter();

        public DataSet ds = new DataSet();

        public DataSet GetFSU()
        {
            try
            {
                string querystring = null;
                querystring = "select * from v_FSU";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "ShipFlag");
                    cmd.Connection.Close();
                }
            }
            catch (Exception)
            {
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return ds;
        }

        public DataSet GetOperation(DateTime dateFrom, DateTime dateTo, string FSU)
        {
            try
            {
                string querystring = null;
                if (FSU == "ALL")
                {
                    querystring = @"select * from " +
                                    "(  " +
                                    "select OperationAppID,CaseNum, CompID, CompanyName as AgentName, SOCompanyName as Operator,MethodCode, MedhodName as MethodName, VRName as FSU, VSNAME as Vessel " +
                                    ", EstOperationTime, EstOilMT, ActOilMT, ActionStatus,IsPayment,PaymentDate,PaymentTime,PaymentAmount,ReceiptNo,PaymentRefID,FlowActionStatusID,PermitDocLink " +
                                    ",NPDocLink,Lampiran1DocLink,IsSubmitCM,IsSubmitBL " +
                                    "from v_OperationApp " +
                                    "where MethodCode = 'A' and  EstOperationDateTime > '" + dateFrom.ToString("yyyy/MM/dd") + "' and EstOperationDateTime < '" + dateTo.ToString("yyyy/MM/dd") + "' " +
                                    "union " +
                                    "select OperationAppID,CaseNum, CompID, CompanyName as AgentName, SOCompanyName as Operator,MethodCode, MedhodName as MethodName, VSName as FSU, VRName As Vessel " +
                                     ", EstOperationTime, EstOilMT, ActOilMT, ActionStatus,IsPayment,PaymentDate,PaymentTime,PaymentAmount,ReceiptNo,PaymentRefID,FlowActionStatusID,PermitDocLink " +
                                    ",NPDocLink,Lampiran1DocLink,IsSubmitCM,IsSubmitBL " +
                                    "from v_OperationApp " +
                                    "where MethodCode = 'B' and EstOperationDateTime > '" + dateFrom.ToString("yyyy/MM/dd") + "' and EstOperationDateTime < '" + dateTo.ToString("yyyy/MM/dd") + "' " +
                                    ") A ";
                }
                else
                {
                    querystring = @"select * from " +
                                    "(  " +
                                    "select OperationAppID,CaseNum, CompID, CompanyName as AgentName, SOCompanyName as Operator,MethodCode, MedhodName as MethodName, VRName as FSU, VSNAME as Vessel " +
                                    ", EstOperationTime, EstOilMT, ActOilMT, ActionStatus,IsPayment,PaymentDate,PaymentTime,PaymentAmount,ReceiptNo,PaymentRefID,FlowActionStatusID,PermitDocLink " +
                                    ",NPDocLink,Lampiran1DocLink,IsSubmitCM,IsSubmitBL " +
                                    "from v_OperationApp " +
                                    "where MethodCode = 'A' and vrname = '" + FSU + "' and EstOperationDateTime > '" + dateFrom.ToString("yyyy/MM/dd") + "' and EstOperationDateTime < '" + dateTo.ToString("yyyy/MM/dd") + "' " +
                                    "union " +
                                    "select OperationAppID,CaseNum, CompID, CompanyName as AgentName, SOCompanyName as Operator,MethodCode, MedhodName as MethodName, VSName as FSU, VRName As Vessel " +
                                     ", EstOperationTime, EstOilMT, ActOilMT, ActionStatus,IsPayment,PaymentDate,PaymentTime,PaymentAmount,ReceiptNo,PaymentRefID,FlowActionStatusID,PermitDocLink " +
                                    ",NPDocLink,Lampiran1DocLink,IsSubmitCM,IsSubmitBL " +
                                    "from v_OperationApp " +
                                    "where MethodCode = 'B' and vsname = '" + FSU + "' and EstOperationDateTime > '" + dateFrom.ToString("yyyy/MM/dd") + "' and EstOperationDateTime < '" + dateTo.ToString("yyyy/MM/dd") + "' " +
                                    ") A ";
                }

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "ShipFlag");
                    cmd.Connection.Close();
                }
            }
            catch (Exception)
            {
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return ds;
        }

        public string GenerateCaseNum(int location,int year)
        {
            string text = "";
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    OpCaseNum opCaseNum = dbContext.OpCaseNums.Where(w=>w.OpYear==year).FirstOrDefault<OpCaseNum>();
                    string currentCaseNum = "";
                    if (opCaseNum == null)
                    {
                        opCaseNum =  new eSTS.Database.OpCaseNum();
                        opCaseNum.id = Guid.NewGuid();
                        opCaseNum.OpYear = year;
                        opCaseNum.CaseNumPG = "LPJ/PG/" + year.ToString().Substring(2, 2) + "-00000";
                        opCaseNum.CaseNumTp = "LPJ/TP/" + year.ToString().Substring(2, 2) + "-00000";

                        if (location == 1)
                        {
                            currentCaseNum = opCaseNum.CaseNumPG;
                        }
                        else
                        {
                            currentCaseNum = opCaseNum.CaseNumTp;
                        }
                        dbContext.OpCaseNums.Add(opCaseNum);
                    }
                    else
                    {
                        if (location == 1)
                        {
                            currentCaseNum = opCaseNum.CaseNumPG;
                        }
                        else
                        {
                            currentCaseNum = opCaseNum.CaseNumTp;
                        }
                    }
                    int runNo = 0;
                    int CurrYear = 0;

                    if (currentCaseNum != "")
                    {
                        runNo = Convert.ToInt32(currentCaseNum.Substring(10, 5));
                        CurrYear = Convert.ToInt32(currentCaseNum.Substring(7, 2));
                    }

                    //if (CurrYear != Convert.ToInt32(DateTime.Now.ToString("yy")))
                    //{
                    //    if (location == 1)
                    //    {
                    //        currentCaseNum = "LPJ/PG/" + DateTime.Now.ToString("yy") + "-00000";
                    //    }
                    //    else
                    //    {
                    //        currentCaseNum = "LPJ/TP/" + DateTime.Now.ToString("yy") + "-00000";
                    //    }

                    //}

                    text = currentCaseNum.Substring(0, 10) + Convert.ToString(runNo + 1).PadLeft(5, '0');

                    if (location == 1)
                    {
                        opCaseNum.CaseNumPG = text;
                    }
                    else
                    {
                        opCaseNum.CaseNumTp = text;
                    }
                    dbContext.SaveChanges();
                    dbContext.Dispose();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, "DALOperation", MethodBase.GetCurrentMethod().Name.ToString());
            }
            return text;
        }

        public bool SubmitApplication(OperationApp appInfo, Guid? supplyMethodID, string userID, bool isDraft)
        {
            try
            {
                Guid? flowActionStatusID = null;

                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    SystemParam sysParam = dbContext.SystemParams.FirstOrDefault<SystemParam>();
                    OperationAppFlow activeFlow = dbContext.OperationAppFlows.Where(w => w.OperationAppID == appInfo.OperationAppID && w.IsActive == true).FirstOrDefault<OperationAppFlow>();

                    if (isDraft==true) //Step 1
                    {
                        if (activeFlow != null)
                        {
                            appInfo.UpdatedBy = userID;
                            appInfo.UpdatedDate = new DateTime?(DateTime.Now);

                            dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);

                        }
                        else
                        {
                            flowActionStatusID = sysParam.FlowDraft;

                            appInfo.CurrentFlowActionStatusID = flowActionStatusID;
                            appInfo.UpdatedBy = userID;
                            appInfo.UpdatedDate = new DateTime?(DateTime.Now);

                            OperationAppFlow operationAppFlow = new OperationAppFlow();
                            operationAppFlow.OperationAppFlowID = Guid.NewGuid();
                            operationAppFlow.OperationAppID = appInfo.OperationAppID;
                            operationAppFlow.FlowActionStatusID = flowActionStatusID;
                            operationAppFlow.IsComplete = new bool?(false);
                            operationAppFlow.IsActive = new bool?(true);
                            operationAppFlow.ActionDate = new DateTime?(DateTime.Now);
                            operationAppFlow.ActionBy = userID;
                            operationAppFlow.CreatedDate = new DateTime?(DateTime.Now);
                            operationAppFlow.CreatedBy = userID;
                            dbContext.OperationAppFlows.Add(operationAppFlow);

                            dbContext.SaveChanges();
                        }
                    }
                   else //Submit Flow - Step 2 & 3 (Submit & Pending Verification)
                    {

                        //Capture submission details
                        flowActionStatusID = sysParam.FlowSubmit; //dbContext.FlowActionStatus.Where(w => w.ActionStatusSeq == 2).FirstOrDefault<FlowActionStatu>().FlowActionStatusID;

                        if (activeFlow != null) //Resubmit
                        {
                            //if active flow == pending verification
                            //Resubmit 
                            if (sysParam.FlowPendingApproval == activeFlow.FlowActionStatusID)
                            {
                                OperationAppFlow submitFlow = dbContext.OperationAppFlows.Where(w => w.OperationAppID == appInfo.OperationAppID && w.FlowActionStatusID == sysParam.FlowSubmit).FirstOrDefault<OperationAppFlow>();
                                submitFlow.ActionDate = new DateTime?(DateTime.Now);
                                submitFlow.ActionBy = userID;

                                dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);
                                PendingEmailSTS(appInfo.OperationAppID, sysParam.FlowSubmit, false);
                            }
                            //Resubmit due to reject
                            else if (sysParam.FlowReject == activeFlow.FlowActionStatusID)
                            {
                                activeFlow.IsComplete = new bool?(false);
                                activeFlow.IsActive = new bool?(false); //For CApture purpose

                                dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);

                                //Create Submit Flow
                                OperationAppFlow newSubmitAppFlow = new OperationAppFlow();
                                newSubmitAppFlow.OperationAppFlowID = Guid.NewGuid();
                                newSubmitAppFlow.OperationAppID = appInfo.OperationAppID;
                                newSubmitAppFlow.FlowActionStatusID = flowActionStatusID;
                                newSubmitAppFlow.IsComplete = new bool?(false);
                                newSubmitAppFlow.IsActive = new bool?(false); //For CApture purpose
                                newSubmitAppFlow.ActionDate = new DateTime?(DateTime.Now);
                                newSubmitAppFlow.ActionBy = userID;
                                newSubmitAppFlow.CreatedDate = new DateTime?(DateTime.Now);
                                newSubmitAppFlow.CreatedBy = userID;
                                dbContext.OperationAppFlows.Add(newSubmitAppFlow);


                                //Create Pending Verification Flow
                                //Capture next action 
                                flowActionStatusID = sysParam.FlowPendingApproval; //dbContext.FlowActionStatus.Where(w => w.ActionStatusSeq == 3).FirstOrDefault<FlowActionStatu>().FlowActionStatusID;

                                OperationAppFlow operationAppFlow = new OperationAppFlow();
                                operationAppFlow.OperationAppFlowID = Guid.NewGuid();
                                operationAppFlow.OperationAppID = appInfo.OperationAppID;
                                operationAppFlow.FlowActionStatusID = flowActionStatusID;
                                operationAppFlow.IsComplete = new bool?(false);
                                operationAppFlow.IsActive = new bool?(true); //To show current status (Pending Verification)
                                operationAppFlow.CreatedDate = DateTime.Now.AddSeconds(1);
                                operationAppFlow.CreatedBy = userID;
                                dbContext.OperationAppFlows.Add(operationAppFlow);

                                dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);
                                PendingEmailSTS(appInfo.OperationAppID, sysParam.FlowSubmit, false);
                            }
                            else if (sysParam.FlowDraft == activeFlow.FlowActionStatusID)
                            {
                                activeFlow.IsComplete = new bool?(false);
                                activeFlow.IsActive = new bool?(false); //For CApture purpose

                                dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);

                                //Create Submit Flow
                                OperationAppFlow newSubmitAppFlow = new OperationAppFlow();
                                newSubmitAppFlow.OperationAppFlowID = Guid.NewGuid();
                                newSubmitAppFlow.OperationAppID = appInfo.OperationAppID;
                                newSubmitAppFlow.FlowActionStatusID = flowActionStatusID;
                                newSubmitAppFlow.IsComplete = new bool?(false);
                                newSubmitAppFlow.IsActive = new bool?(false); //For CApture purpose
                                newSubmitAppFlow.ActionDate = new DateTime?(DateTime.Now);
                                newSubmitAppFlow.ActionBy = userID;
                                newSubmitAppFlow.CreatedDate = new DateTime?(DateTime.Now);
                                newSubmitAppFlow.CreatedBy = userID;
                                dbContext.OperationAppFlows.Add(newSubmitAppFlow);


                                //Create Pending Verification Flow
                                //Capture next action 
                                flowActionStatusID = sysParam.FlowPendingApproval; //dbContext.FlowActionStatus.Where(w => w.ActionStatusSeq == 3).FirstOrDefault<FlowActionStatu>().FlowActionStatusID;

                                OperationAppFlow operationAppFlow = new OperationAppFlow();
                                operationAppFlow.OperationAppFlowID = Guid.NewGuid();
                                operationAppFlow.OperationAppID = appInfo.OperationAppID;
                                operationAppFlow.FlowActionStatusID = flowActionStatusID;
                                operationAppFlow.IsComplete = new bool?(false);
                                operationAppFlow.IsActive = new bool?(true); //To show current status (Pending Verification)
                                operationAppFlow.CreatedDate = DateTime.Now.AddSeconds(1);
                                operationAppFlow.CreatedBy = userID;
                                dbContext.OperationAppFlows.Add(operationAppFlow);

                                dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);
                                PendingEmailSTS(appInfo.OperationAppID, sysParam.FlowSubmit, false);
                            }
                            
                        }
                        else //New submit
                        {
                            OperationAppFlow newSubmitAppFlow = new OperationAppFlow();
                            newSubmitAppFlow.OperationAppFlowID = Guid.NewGuid();
                            newSubmitAppFlow.OperationAppID = appInfo.OperationAppID;
                            newSubmitAppFlow.FlowActionStatusID = flowActionStatusID;
                            newSubmitAppFlow.IsComplete = new bool?(false);
                            newSubmitAppFlow.IsActive = new bool?(false); //For CApture purpose
                            newSubmitAppFlow.ActionDate = new DateTime?(DateTime.Now);
                            newSubmitAppFlow.ActionBy = userID;
                            newSubmitAppFlow.CreatedDate = new DateTime?(DateTime.Now);
                            newSubmitAppFlow.CreatedBy = userID;
                            dbContext.OperationAppFlows.Add(newSubmitAppFlow);

                            //Capture next action 
                            flowActionStatusID = sysParam.FlowPendingApproval; //dbContext.FlowActionStatus.Where(w => w.ActionStatusSeq == 3).FirstOrDefault<FlowActionStatu>().FlowActionStatusID;

                            OperationAppFlow operationAppFlow = new OperationAppFlow();
                            operationAppFlow.OperationAppFlowID = Guid.NewGuid();
                            operationAppFlow.OperationAppID = appInfo.OperationAppID;
                            operationAppFlow.FlowActionStatusID = flowActionStatusID;
                            operationAppFlow.IsComplete = new bool?(false);
                            operationAppFlow.IsActive = new bool?(true); //To show current status (Pending Verification)
                            operationAppFlow.CreatedDate = DateTime.Now.AddSeconds(1);
                            operationAppFlow.CreatedBy = userID;
                            dbContext.OperationAppFlows.Add(operationAppFlow);

                            dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);

                            PendingEmailSTS(appInfo.OperationAppID, sysParam.FlowSubmit, false);

                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, "DALOperation", MethodBase.GetCurrentMethod().Name.ToString());
            }
            return true;
        }

        public bool SubmitAmendment(OperationApp appInfo, Guid? supplyMethodID, string userID, bool isDraft,string remark)
        {
            /* Added : Mala
            * Ref : CR
            * Reason/Purpose : Control view for Amendments Flow
            *  Date : 25/01/2022
           */
            try
            {
               
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    //update current flow
                    OperationAppFlow activeFlow = dbContext.OperationAppFlows.Where(w => w.OperationAppID == appInfo.OperationAppID && w.IsActive == true).FirstOrDefault<OperationAppFlow>();

                    activeFlow.IsActive = new bool?(false); //For CApture purpose

                    dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);

                    //Create Flow Submit Amend Flow
                    SystemParam sysParam = dbContext.SystemParams.FirstOrDefault<SystemParam>();
                    OperationAppFlow newSubmitAmendFlow = new OperationAppFlow();
                    newSubmitAmendFlow.OperationAppFlowID = Guid.NewGuid();
                    newSubmitAmendFlow.OperationAppID = appInfo.OperationAppID;
                    newSubmitAmendFlow.FlowActionStatusID = sysParam.FlowAmendSubmit;
                    newSubmitAmendFlow.Remark = remark;
                    newSubmitAmendFlow.IsComplete = new bool?(false);
                    newSubmitAmendFlow.IsActive = new bool?(false); //For Capture purpose
                    newSubmitAmendFlow.ActionDate = new DateTime?(DateTime.Now);
                    newSubmitAmendFlow.ActionBy = userID;
                    newSubmitAmendFlow.CreatedDate = new DateTime?(DateTime.Now);
                    newSubmitAmendFlow.CreatedBy = userID;
                    dbContext.OperationAppFlows.Add(newSubmitAmendFlow);
                    dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);
                    
                    //Create Flow Submit Amend Flow
                    OperationAppFlow pendingAmendFlow = new OperationAppFlow();
                    pendingAmendFlow.OperationAppFlowID = Guid.NewGuid();
                    pendingAmendFlow.OperationAppID = appInfo.OperationAppID;
                    pendingAmendFlow.FlowActionStatusID = sysParam.FlowAmendPending;
                    pendingAmendFlow.IsComplete = new bool?(false);
                    pendingAmendFlow.IsActive = new bool?(true); //To show current status (Pending Verification)
                    pendingAmendFlow.CreatedDate = DateTime.Now.AddSeconds(1);
                    pendingAmendFlow.CreatedBy = userID;
                    dbContext.OperationAppFlows.Add(pendingAmendFlow);
                    dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);

                    PendingEmailSTS(appInfo.OperationAppID, sysParam.FlowAmendSubmit, false);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, "DALOperation", MethodBase.GetCurrentMethod().Name.ToString());
            }
            return true;
        }

        public bool SubmitApproval(FAction action, Guid operationAppID, string userID, Guid? accessGroup, string Remark)
        {
            bool result;
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {

                    OperationApp appInfo = dbContext.OperationApps.Find(operationAppID);
                    OperationAppFlow appFlow = dbContext.OperationAppFlows.Where(w => w.OperationAppID==appInfo.OperationAppID && w.IsActive == true).FirstOrDefault<OperationAppFlow>();
                    SystemParam sysParam = dbContext.SystemParams.FirstOrDefault<SystemParam>();

                  
                    if (action == FAction.Reject)
                    {
                        appInfo.CurrentFlowActionStatusID = sysParam.FlowReject;
                        appInfo.IsRejected = true;
                        appInfo.RejectBy = userID;
                        appInfo.RejectedDate = DateTime.Now;
                        appInfo.RejectRemark = Remark;
                        dbContext.SaveChanges(userID, "OperationAppID", appInfo.OperationAppID);

                        ///Update Current Flow
                        
                        appFlow.ActionBy = userID;
                        appFlow.ActionDate = DateTime.Now;
                        appFlow.IsActive = false;

                        dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);

                        //create new flow for reject
                        OperationAppFlow operationAppFlow = new OperationAppFlow();
                        operationAppFlow.OperationAppID = appInfo.OperationAppID;
                        operationAppFlow.OperationAppFlowID = Guid.NewGuid();
                        operationAppFlow.FlowActionStatusID = sysParam.FlowReject;
                        operationAppFlow.ActionDate = new DateTime?(DateTime.Now);
                        operationAppFlow.ActionBy = userID;
                        operationAppFlow.IsReject = new bool?(true);
                        operationAppFlow.IsActive = true;
                        operationAppFlow.Remark = Remark;
                        operationAppFlow.CompID = appInfo.CompID;
                        operationAppFlow.CreatedDate = DateTime.Now;
                        operationAppFlow.CreatedBy = userID;
                        dbContext.OperationAppFlows.Add(operationAppFlow);
                        dbContext.SaveChanges(userID, "OperationAppFlowID",appInfo.OperationAppID);

                        PendingEmailSTS(appInfo.OperationAppID, sysParam.FlowReject, false);
                    }
                    else
                    {

                        appInfo.CurrentFlowActionStatusID = sysParam.FlowPendingPayment;
                        dbContext.SaveChanges();

                        ///Update Current Flow

                        appFlow.ActionBy = userID;
                        appFlow.ActionDate = DateTime.Now;
                        appFlow.IsActive = false;

                        dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);

                        //create new flow for approved
                        OperationAppFlow approvedFlow = new OperationAppFlow();
                        approvedFlow.OperationAppFlowID = Guid.NewGuid();
                        approvedFlow.OperationAppID = appInfo.OperationAppID;
                        approvedFlow.FlowActionStatusID = sysParam.FlowApproved;
                        approvedFlow.IsActive = false;
                        approvedFlow.ActionBy = userID;
                        approvedFlow.ActionDate = DateTime.Now;
                        approvedFlow.CreatedDate = DateTime.Now;
                        approvedFlow.CreatedBy = userID;

                        dbContext.OperationAppFlows.Add(approvedFlow);
                        dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);

                        //create new flow for pending payment
                        OperationAppFlow operationAppFlow = new OperationAppFlow();
                        operationAppFlow.OperationAppFlowID = Guid.NewGuid();
                        operationAppFlow.OperationAppID = appInfo.OperationAppID;
                        operationAppFlow.FlowActionStatusID = sysParam.FlowPendingPayment;
                        operationAppFlow.IsActive = true;
                        operationAppFlow.CreatedDate = DateTime.Now.AddSeconds(1);
                        operationAppFlow.CreatedBy = userID;

                        dbContext.OperationAppFlows.Add(operationAppFlow);
                        dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);

                        PendingEmailSTS(appInfo.OperationAppID, sysParam.FlowApproved, false);
                    }
                    dbContext.Dispose();
                }
            }
            catch (Exception)
            {
                bool flag5 = this.cmd.Connection.State == ConnectionState.Open;
                if (flag5)
                {
                    this.cmd.Connection.Close();
                    result = false;
                    return result;
                }
                throw;
            }
            result = true;
            return result;
        }

        public bool SubmitAutoApprove(bool isDraft, Guid? supplyMethodID, Guid operationAppID, string userID)
        {
            bool result;
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    Guid? flowActionStatusID = null;
                    OperationApp appInfo = dbContext.OperationApps.Find(operationAppID);
                    OperationAppFlow activeFlow = dbContext.OperationAppFlows.Where(w => w.OperationAppID == appInfo.OperationAppID && w.IsActive == true).FirstOrDefault<OperationAppFlow>();
                    SystemParam sysParam = dbContext.SystemParams.FirstOrDefault<SystemParam>();

                    //If Draft
                    if (isDraft == true) //Step 1
                    {
                        if (activeFlow != null)
                        {
                            appInfo.UpdatedBy = userID;
                            appInfo.UpdatedDate = new DateTime?(DateTime.Now);

                            dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);

                        }
                        else
                        {
                            flowActionStatusID = sysParam.FlowDraft;

                            appInfo.CurrentFlowActionStatusID = flowActionStatusID;
                            appInfo.UpdatedBy = userID;
                            appInfo.UpdatedDate = new DateTime?(DateTime.Now);

                            OperationAppFlow operationAppFlow = new OperationAppFlow();
                            operationAppFlow.OperationAppFlowID = Guid.NewGuid();
                            operationAppFlow.OperationAppID = appInfo.OperationAppID;
                            operationAppFlow.FlowActionStatusID = flowActionStatusID;
                            operationAppFlow.IsComplete = new bool?(false);
                            operationAppFlow.IsActive = new bool?(true);
                            operationAppFlow.ActionDate = new DateTime?(DateTime.Now);
                            operationAppFlow.ActionBy = userID;
                            operationAppFlow.CreatedDate = new DateTime?(DateTime.Now);
                            operationAppFlow.CreatedBy = userID;
                            dbContext.OperationAppFlows.Add(operationAppFlow);

                            dbContext.SaveChanges();
                        }
                    }
                    else
                    {
                        //Submit 
                        OperationAppFlow submitFlow = new OperationAppFlow();
                        submitFlow.OperationAppFlowID = Guid.NewGuid();
                        submitFlow.OperationAppID = appInfo.OperationAppID;
                        submitFlow.FlowActionStatusID = sysParam.FlowSubmit;
                        submitFlow.IsActive = false;
                        submitFlow.ActionBy = userID;
                        submitFlow.ActionDate = DateTime.Now;
                        submitFlow.CreatedDate = DateTime.Now;
                        submitFlow.CreatedBy = userID;

                        dbContext.OperationAppFlows.Add(submitFlow);
                        dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);
                        
                        //Approved

                        //create new flow for approved
                        OperationAppFlow approvedFlow = new OperationAppFlow();
                        approvedFlow.OperationAppFlowID = Guid.NewGuid();
                        approvedFlow.OperationAppID = appInfo.OperationAppID;
                        approvedFlow.FlowActionStatusID = sysParam.FlowApproved;
                        approvedFlow.IsActive = false;
                        approvedFlow.ActionBy = "System";
                        approvedFlow.ActionDate = DateTime.Now;
                        approvedFlow.CreatedDate = DateTime.Now;
                        approvedFlow.CreatedBy = userID;

                        dbContext.OperationAppFlows.Add(approvedFlow);
                        dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);

                        //create new flow for pending CM
                        OperationAppFlow operationAppFlow = new OperationAppFlow();
                        operationAppFlow.OperationAppFlowID = Guid.NewGuid();
                        operationAppFlow.OperationAppID = appInfo.OperationAppID;
                        if (supplyMethodID == sysParam.SupplyMethodA)
                            operationAppFlow.FlowActionStatusID = sysParam.FlowPendingCM;
                        else
                            operationAppFlow.FlowActionStatusID = sysParam.FlowPendingBL;
                        operationAppFlow.IsActive = true;
                        operationAppFlow.CreatedDate = DateTime.Now.AddSeconds(1);
                        operationAppFlow.CreatedBy = userID;

                        dbContext.OperationAppFlows.Add(operationAppFlow);
                        dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);



                        appInfo.CurrentFlowActionStatusID = operationAppFlow.OperationAppFlowID;
                        appInfo.IsAppCompleted = true;
                        dbContext.SaveChanges();
                        PendingEmailSTS(appInfo.OperationAppID, sysParam.FlowApproved, false);
                    }
                    dbContext.Dispose();
                }
            }
            catch (Exception)
            {
                bool flag5 = this.cmd.Connection.State == ConnectionState.Open;
                if (flag5)
                {
                    this.cmd.Connection.Close();
                    result = false;
                    return result;
                }
                throw;
            }
            result = true;
            return result;
        }

        public bool SubmitAmendApproval(FAction action, Guid operationAppID, string userID, Guid? accessGroup, string Remark)
        {
            bool result;
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {

                    OperationApp appInfo = dbContext.OperationApps.Find(operationAppID);
                    OperationAppFlow appFlow = dbContext.OperationAppFlows.Where(w => w.OperationAppID == appInfo.OperationAppID && w.IsActive == true).FirstOrDefault<OperationAppFlow>();
                    SystemParam sysParam = dbContext.SystemParams.FirstOrDefault<SystemParam>();


                    if (action == FAction.Reject)
                    {
                        appInfo.CurrentFlowActionStatusID = sysParam.FlowAmendRejected;
                        appInfo.IsRejected = true;
                        appInfo.RejectBy = userID;
                        appInfo.RejectedDate = DateTime.Now;
                        appInfo.RejectRemark = Remark;
                        dbContext.SaveChanges(userID, "OperationAppID", appInfo.OperationAppID);

                        ///Update Current Flow

                        appFlow.ActionBy = userID;
                        appFlow.ActionDate = DateTime.Now;
                        appFlow.IsActive = false;

                        dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);

                        //create new flow for reject
                        OperationAppFlow operationAppFlow = new OperationAppFlow();
                        operationAppFlow.OperationAppID = appInfo.OperationAppID;
                        operationAppFlow.OperationAppFlowID = Guid.NewGuid();
                        operationAppFlow.FlowActionStatusID = sysParam.FlowAmendRejected;
                        operationAppFlow.ActionDate = new DateTime?(DateTime.Now);
                        operationAppFlow.ActionBy = userID;
                        operationAppFlow.IsReject = new bool?(true);
                        operationAppFlow.IsActive = true;
                        operationAppFlow.Remark = Remark;
                        operationAppFlow.CompID = appInfo.CompID;
                        operationAppFlow.CreatedDate = DateTime.Now;
                        operationAppFlow.CreatedBy = userID;
                        dbContext.OperationAppFlows.Add(operationAppFlow);
                        dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);

                        PendingEmailSTS(appInfo.OperationAppID, sysParam.FlowAmendRejected, false);
                    }
                    else
                    {
                        appInfo.CurrentFlowActionStatusID = sysParam.FlowAmendApproved;
                        dbContext.SaveChanges();

                        ///Update Current Flow

                        appFlow.ActionBy = userID;
                        appFlow.ActionDate = DateTime.Now;
                        appFlow.IsActive = false;

                        dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);

                        //create new flow for approved
                        OperationAppFlow approvedFlow = new OperationAppFlow();
                        approvedFlow.OperationAppFlowID = Guid.NewGuid();
                        approvedFlow.OperationAppID = appInfo.OperationAppID;
                        approvedFlow.FlowActionStatusID = sysParam.FlowAmendApproved;
                        approvedFlow.IsActive = false;
                        approvedFlow.ActionBy = userID;
                        approvedFlow.ActionDate = DateTime.Now;
                        approvedFlow.CreatedDate = DateTime.Now;
                        approvedFlow.CreatedBy = userID;

                        dbContext.OperationAppFlows.Add(approvedFlow);
                        dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);

                        //create new flow for pending CM/BL
                        OperationAppFlow operationAppFlow = new OperationAppFlow();
                        operationAppFlow.OperationAppFlowID = Guid.NewGuid();
                        operationAppFlow.OperationAppID = appInfo.OperationAppID;
                        if(appInfo.SupplyMethodID==sysParam.SupplyMethodA)
                            operationAppFlow.FlowActionStatusID = sysParam.FlowPendingCM;
                        else
                            operationAppFlow.FlowActionStatusID = sysParam.FlowPendingBL;
                        operationAppFlow.IsActive = true;
                        operationAppFlow.CreatedDate = DateTime.Now.AddSeconds(1);
                        operationAppFlow.CreatedBy = userID;

                        dbContext.OperationAppFlows.Add(operationAppFlow);
                        dbContext.SaveChanges(userID, "OperationAppFlowID", appInfo.OperationAppID);

                        PendingEmailSTS(appInfo.OperationAppID, sysParam.FlowAmendApproved, false);
                    }
                    dbContext.Dispose();
                }
            }
            catch (Exception)
            {
                bool flag5 = this.cmd.Connection.State == ConnectionState.Open;
                if (flag5)
                {
                    this.cmd.Connection.Close();
                    result = false;
                    return result;
                }
                throw;
            }
            result = true;
            return result;
        }

        public static bool PendingEmailSTS(Guid? operationAppID, Guid? FlowActionStatusID, bool isReject)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            bool result;
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {

                    PendingEmail emailLog = new PendingEmail();
                    emailLog.PendingMailID = Guid.NewGuid();
                    emailLog.RefID = operationAppID;
                    emailLog.RefFlowID = FlowActionStatusID;
                    emailLog.IsReject = isReject;
                    emailLog.LogDate = new DateTime?(DateTime.Now);
                    emailLog.IsSend = new bool?(false);
                    dbContext.PendingEmails.Add(emailLog);
                    dbContext.SaveChanges();
                }

                //using (var client = new HttpClient())
                //{
                //    client.BaseAddress = new Uri("");

                //    HttpContent content = new StringContent(
                //       JsonConvert.SerializeObject(emailLog),
                //       Encoding.UTF8,
                //       "application/json"
                //   );
                //    var responseTask = client.PostAsync();

                //    responseTask.Wait();

                //    var res = responseTask.Result;

                //    HttpClient httpClient = new HttpClient();
              
                //    HttpResponseMessage response =await httpClient.PostAsync("http://172.16.8.10:8181/api/email/e812eef2-42fe-4da0-95ae-0b45c6df2829", content);
                //    string statusCode = response.StatusCode.ToString();
                //}
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, "DALOperation", MethodBase.GetCurrentMethod().Name.ToString());
                result = false;
                return result;
            }
            result = true;
            return result;
        }
        //Generate and Return PermitFilePDFPath
        public string GenerateQRCode(string operationAppID, string compID, string folderDirectory, string fileName, string PermitQRFilePath)
        {
            string permitFilePath = "";
            try
            {
                //Check Folder Directory
                CreateFolder(folderDirectory);
                permitFilePath = "~/Operation/Upload/" + compID + "/" + operationAppID + "/" + fileName + ".pdf";

                //string PermitQRFilePath = "~/Operation/Upload/" + compID + "/Permit/";

                string permitAccessURL = WebConfigurationManager.AppSettings["UploadFolderPath"].ToString() + compID + "/" + operationAppID + "/" + fileName + ".pdf";//HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery.ToString(), "")

                QRCodeGen obj = new Common.QRCodeGen();
                obj.RenderQrCode(permitAccessURL, "Q", PermitQRFilePath);
                //obj.RenderQrCode(permitAccessURL, "Q", Server.MapPath(PermitQRFilePath));

            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return permitFilePath;
        }

        public DataSet Get_OperationAppList(string operationAppID)
        {
            try
            {
                string commandText = "select * from v_permit where OperationAppID='" + operationAppID + "'";
                using (SqlConnection sqlConnection = new SqlConnection(this.connectionstring))
                {
                    this.cmd.Connection = sqlConnection;
                    this.cmd.CommandType = CommandType.Text;
                    this.cmd.Parameters.Clear();
                    this.cmd.CommandText = commandText;
                    this.cmd.CommandTimeout = 0;
                    this.cmd.Connection.Open();
                    this.ad.SelectCommand = this.cmd;
                    //bool flag = this.ds.Tables.Count > 0;
                    //if (flag)
                    //{
                    //    this.ds.Tables.Clear();
                    //}
                    this.ad.Fill(this.ds, "v_permit");
                    this.cmd.Connection.Close();
                }
            }
            catch (Exception)
            {
                bool flag2 = this.cmd.Connection.State == ConnectionState.Open;
                if (flag2)
                {
                    this.cmd.Connection.Close();
                }
                throw;
            }
            return this.ds;
        }

        public DataSet GetSuppDocList(string licCompanyID, string licCompanyVesselID)
        {
            try
            {
                string commandText = @"select X.DocCode,X.DocDesc,Y.LicID,Y.ValidTo,case when Y.ValidTo < CONVERT(VARCHAR(10), GETDATE(), 111) then 1 else 0 end as IsExpired  from  v_SuppDoc X LEFT OUTER JOIN
                                    (
                                    select AttachID,LicCompanyID as LicID,AttachTypeID,a.ValidTo
                                    from LicCompanyAttach a
                                    inner join v_SuppDoc b on a.AttachTypeID=b.MSDocTypeID
                                    where LicCompanyID = @pLicCompanyID
                                    )Y
                                      on Y.AttachTypeID=X.MSDocTypeID 
                                      where  X.ModuleID='STSOL' and X.DocStatus=1
                                    select X.DocCode,X.DocDesc,Y.LicID,Y.ValidTo,case when Y.ValidTo < CONVERT(VARCHAR(10), GETDATE(), 111) then 1 else 0 end as IsExpired   from  v_SuppDoc X LEFT OUTER JOIN
                                    (
                                    select AttachID,LicCompanyVesselID as LicID,a.AttchTypeID,a.ValidTo
                                    from LicCompanyVesselAttach a
                                    inner join v_SuppDoc b on a.AttchTypeID=b.MSDocTypeID
                                    where LicCompanyVesselID = @pLicCompanyVesselID
                                    )Y
                                      on Y.AttchTypeID=X.MSDocTypeID
                                      where  X.ModuleID='STSVL' and X.DocStatus=1";
                using (SqlConnection sqlConnection = new SqlConnection(this.connectionstring))
                {
                    this.cmd.Connection = sqlConnection;
                    this.cmd.CommandType = CommandType.Text;
                    this.cmd.Parameters.Clear();
                    this.cmd.Parameters.AddWithValue("pLicCompanyID", licCompanyID);
                    this.cmd.Parameters.AddWithValue("pLicCompanyVesselID", licCompanyVesselID);
                    this.cmd.CommandText = commandText;
                    this.cmd.CommandTimeout = 0;
                    this.cmd.Connection.Open();
                    this.ad.SelectCommand = this.cmd;

                    bool flag = this.ds.Tables.Count > 0;
                    if (flag)
                    {
                        this.ds.Tables.Clear();
                    }

                    this.ad.Fill(this.ds, "SuppDoc");
                    this.cmd.Connection.Close();
                }
            }
            catch (Exception)
            {
                bool flag2 = this.cmd.Connection.State == ConnectionState.Open;
                if (flag2)
                {
                    this.cmd.Connection.Close();
                }
                throw;
            }
            return this.ds;
        }

        private void CreateFolder(string folderDirectory)
        {
            try
            {
                bool folderExists;
                //-------------------------------------------------------------
                // Save File to server directory
                //-------------------------------------------------------------
                folderExists = Directory.Exists(folderDirectory);
                if (!folderExists)
                    Directory.CreateDirectory(folderDirectory);
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex.InnerException, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

    }
}