using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eSTS.Database;
using System.Configuration;
using System.Data.SqlClient;
using Apps.Common;
using System.Globalization;

namespace eSTSeLPJInteg
{
    public class Integration
    {
        internal bool execIntegration()
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                   // List<v_IntegrationFlow> lIntegrationFlows = dbContext.v_IntegrationFlow.Where(w => w.isSync == null || w.isSync == false && w.OperationAppID != null).OrderBy(o=>o.OperationAppID).ThenBy(o=>o.create_time).ToList<v_IntegrationFlow>();
                    List<v_IntegrationFlow> lIntegrationFlows = dbContext.v_IntegrationFlow.Where(w => w.OperationAppID == new Guid("6d3822e6-5de1-455b-8335-ecc8e5955d51")).OrderBy(o => o.OperationAppID).ThenBy(o => o.create_time).ToList<v_IntegrationFlow>();
                    SystemParam sysParam = dbContext.SystemParams.FirstOrDefault<SystemParam>();
                    foreach (v_IntegrationFlow item in lIntegrationFlows)
                    {
                       if (item.status.ToUpper()== "AMENDMENT")
                        {
                            //1) Update Current Flow
                            OperationAppFlow activeFlow = dbContext.OperationAppFlows.Where(w => w.OperationAppID == item.OperationAppID && w.IsActive == true).FirstOrDefault<OperationAppFlow>();
                            activeFlow.ActionDate = item.create_time;
                            activeFlow.ActionBy = "elpjt";
                            activeFlow.IsActive = new bool?(false);
                            dbContext.SaveChanges("elpjt", "OperationAppFlowID", item.OperationAppID);

                            //2) Create New Flow - Rejected
                            OperationAppFlow operationRejectFlow = new OperationAppFlow();
                            operationRejectFlow.OperationAppFlowID = Guid.NewGuid();
                            operationRejectFlow.OperationAppID = item.OperationAppID;
                            operationRejectFlow.FlowActionStatusID = sysParam.FlowRejectDec;
                            operationRejectFlow.IsReject = true;
                            operationRejectFlow.Remark = item.AmendRemarks;
                            operationRejectFlow.IsComplete = new bool?(false);
                            operationRejectFlow.IsActive = new bool?(true);
                            operationRejectFlow.ActionDate = item.create_time;
                            operationRejectFlow.ActionBy = "elpjt";
                            operationRejectFlow.CreatedDate = DateTime.Now.AddSeconds(1);
                            operationRejectFlow.CreatedBy = "elpjt"; 
                            dbContext.OperationAppFlows.Add(operationRejectFlow);

                            dbContext.SaveChanges("elpjt", "OperationAppFlowID", item.OperationAppID);
                            PendingEmailSTS(item.OperationAppID, sysParam.FlowSubmitDec, false);

                            //3) Update eLPJT -  IsSync==true
                            UpdateELPJT(item.ID_status);
                            Log.WriteMessageLog("Success Integrate", "execIntegration", "AMENDMENT - " + item.OperationAppID);
                        }
                        else if (item.status.ToUpper() == "VERIFIED")
                        {
                            ////1) Update Verified
                            OperationAppFlow activeFlow = dbContext.OperationAppFlows.Where(w => w.OperationAppID == item.OperationAppID && w.FlowActionStatusID==sysParam.FlowPendingVerifyDec && w.ActionDate==null).FirstOrDefault<OperationAppFlow>();
                            activeFlow.ActionDate = DateTime.Now;
                            activeFlow.ActionBy = "elpjt";
                            activeFlow.IsActive = new bool?(false);
                            dbContext.SaveChanges("elpjt", "OperationAppFlowID", item.OperationAppID);

                            //2) Create Flow Verified
                            OperationAppFlow flowVerified = dbContext.OperationAppFlows.Where(w => w.OperationAppID == item.OperationAppID && w.FlowActionStatusID == sysParam.FlowVerifiedDec).FirstOrDefault<OperationAppFlow>();

                            if(flowVerified==null)
                            {
                                OperationAppFlow verified = new OperationAppFlow();
                                verified.OperationAppFlowID = Guid.NewGuid();
                                verified.OperationAppID = item.OperationAppID;
                                verified.FlowActionStatusID = sysParam.FlowVerifiedDec;
                                verified.IsComplete = new bool?(false);
                                verified.IsActive = new bool?(false);
                                verified.CreatedDate = DateTime.Now.AddSeconds(1);
                                verified.CreatedBy = "elpjt";
                                verified.ActionDate = DateTime.Now;
                                verified.ActionBy = "elpjt";
                                dbContext.OperationAppFlows.Add(verified);
                                dbContext.SaveChanges("elpjt", "OperationAppFlowID", item.OperationAppID);
                            }

                            //3) Update eLPJT -  IsSync==true
                            UpdateELPJT(item.ID_status);
                            Log.WriteMessageLog("Success Integrate", "execIntegration", "VERIFIED - " + item.OperationAppID);
                        }
                        else if (item.status.ToUpper() == "APPROVED")
                        {
                            OperationAppFlow flowProcessInv = dbContext.OperationAppFlows.Where(w => w.OperationAppID == item.OperationAppID && w.FlowActionStatusID == sysParam.FlowProcessInvoice && w.ActionDate == null).FirstOrDefault<OperationAppFlow>();

                            if (flowProcessInv == null)  ///Not yet approve
                            {
                                //1) Update Current Flow
                                //Remark sebab kene guna isactive utk ambil current flow
                                // OperationAppFlow activeFlow = dbContext.OperationAppFlows.Where(w => w.OperationAppID == item.OperationAppID && w.FlowActionStatusID == sysParam.FlowPendingApproveDec && w.ActionDate == null).FirstOrDefault<OperationAppFlow>();
                                OperationAppFlow activeFlow = dbContext.OperationAppFlows.Where(w => w.OperationAppID == item.OperationAppID && w.IsActive==true).FirstOrDefault<OperationAppFlow>();

                                //If 
                                if (activeFlow.ActionDate == null)
                                {
                                    activeFlow.ActionDate = DateTime.Now;
                                    activeFlow.ActionBy = "elpjt";
                                }
                                    activeFlow.IsActive = new bool?(false);
                                    dbContext.SaveChanges("elpjt", "OperationAppFlowID", item.OperationAppID);
                                
                                //2) Create New Flow - Approval
                                OperationAppFlow operationAppFlow = new OperationAppFlow();
                                operationAppFlow.OperationAppFlowID = Guid.NewGuid();
                                operationAppFlow.OperationAppID = item.OperationAppID;
                                operationAppFlow.FlowActionStatusID = sysParam.FlowApprovedDec;
                                operationAppFlow.IsComplete = new bool?(false);
                                operationAppFlow.IsActive = new bool?(false);
                                operationAppFlow.ActionDate = item.create_time;
                                operationAppFlow.ActionBy = "elpjt";
                                operationAppFlow.CreatedDate = DateTime.Now.AddSeconds(1);
                                operationAppFlow.CreatedBy = "elpjt";
                                dbContext.OperationAppFlows.Add(operationAppFlow);
                                dbContext.SaveChanges("elpjt", "OperationAppFlowID", item.OperationAppID);

                                //3) Create New Processing Invoice
                                OperationAppFlow operationProcessInvFlow = new OperationAppFlow();
                                operationProcessInvFlow.OperationAppFlowID = Guid.NewGuid();
                                operationProcessInvFlow.OperationAppID = item.OperationAppID;
                                operationProcessInvFlow.FlowActionStatusID = sysParam.FlowProcessInvoice;
                                operationProcessInvFlow.IsComplete = new bool?(false);
                                operationProcessInvFlow.IsActive = new bool?(true);
                                operationProcessInvFlow.CreatedDate = DateTime.Now.AddSeconds(1);
                                operationProcessInvFlow.CreatedBy = "elpjt";
                                dbContext.OperationAppFlows.Add(operationProcessInvFlow);
                                dbContext.SaveChanges("elpjt", "OperationAppFlowID", item.OperationAppID);

                                Log.WriteMessageLog("Success Integrate", "execIntegration", "APPROVED - " + item.OperationAppID);
                            }
                            else //Approved but update the invoice details
                            {
                                if(item.jbar_invoiceno!=null)
                                {
                                    flowProcessInv.ActionDate = item.jbar_posteddate;
                                    flowProcessInv.ActionBy = "elpjt";
                                    flowProcessInv.Remark = item.jbar_invoiceno;
                                    flowProcessInv.IsActive = false;

                                    dbContext.SaveChanges("elpjt", "OperationAppFlowID", item.OperationAppID);
                                    //1) Create Invoice Prepared
                                    OperationAppFlow invoicePrepared = new OperationAppFlow();
                                    invoicePrepared.OperationAppFlowID = Guid.NewGuid();
                                    invoicePrepared.OperationAppID = item.OperationAppID;
                                    invoicePrepared.FlowActionStatusID = sysParam.FlowInvoicePrepared;
                                    invoicePrepared.IsComplete = new bool?(false);
                                    invoicePrepared.IsActive = new bool?(false);
                                    invoicePrepared.CreatedDate = DateTime.Now;
                                    invoicePrepared.CreatedBy = "elpjt";
                                    invoicePrepared.ActionDate = DateTime.Now;
                                    invoicePrepared.ActionBy = "elpjt";
                                    dbContext.OperationAppFlows.Add(invoicePrepared);
                                    dbContext.SaveChanges("elpjt", "OperationAppFlowID", item.OperationAppID);

                                    //2) Create New Complete
                                    OperationAppFlow operationComplete = new OperationAppFlow();
                                    operationComplete.OperationAppFlowID = Guid.NewGuid();
                                    operationComplete.OperationAppID = item.OperationAppID;
                                    operationComplete.FlowActionStatusID = sysParam.FlowComplete;
                                    operationComplete.IsComplete = new bool?(false);
                                    operationComplete.IsActive = new bool?(true);
                                    operationComplete.CreatedDate = DateTime.Now.AddSeconds(1);
                                    operationComplete.CreatedBy = "elpjt";
                                    operationComplete.Remark = item.jbar_invoiceno;
                                    operationComplete.ActionDate = item.jbar_posteddate;
                                    operationComplete.ActionBy = "elpjt";
                                    dbContext.OperationAppFlows.Add(operationComplete);
                                    dbContext.SaveChanges("elpjt", "OperationAppFlowID", item.OperationAppID);

                                    //3) Update OperationApp
                                    OperationApp main = dbContext.OperationApps.Where(w => w.OperationAppID == item.OperationAppID).FirstOrDefault<OperationApp>();
                                    main.IsCompleteDeclare = new bool?(true);
                                    dbContext.SaveChanges("elpjt", "OperationAppID", item.OperationAppID);


                                    //4) Update eLPJT -  IsSync==true
                                    UpdateELPJT(item.ID_status);
                                    Log.WriteMessageLog("Success Integrate", "execIntegration", "Invoice - " + item.OperationAppID);
                                }
                            }
                        }
                    }


                }

                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return false;
            }
        }
        private void UpdateELPJT(string IDStatus)
        {
            try
            {
                string conns = ConfigurationManager.ConnectionStrings["PJConnectionString"].ToString();
                using (SqlConnection sqlConn = new SqlConnection(conns))
                {
                    sqlConn.Open();
                    string sqlCmd = @"UPDATE [status_sts] SET ISSync=1 WHERE id_status=@id_status";
                   
                    using (SqlCommand cmd = new SqlCommand(sqlCmd, sqlConn))
                    {
                        
                        cmd.Parameters.AddWithValue("@id_status", IDStatus);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
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

            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, "DALOperation", "PendingEmailSTS");
                result = false;
                return result;
            }
            result = true;
            return result;
        }
    }
}
