using Apps.Common;
using CrystalDecisions.CrystalReports.Engine;
using eSTS.DAL;
using eSTS.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eSTS.Operation
{
    public partial class JLMPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Check Session
                if ((Session["UserID"] == null) && (Session["UserGroup"] == null))//&& (Session["UserLevel"] != null))
                {
                    Response.Redirect("~//SignIn.aspx", true);
                }

                if (Request.QueryString.Count > 0)
                {
                    hfApplicationID.Value = Request.QueryString["sno"].ToString();
                    hfMethod.Value = Request.QueryString["method"].ToString();
                    Session["mode"] = Request.QueryString["mode"].ToString();
                }
                else
                {
                    Session["mode"] = "n";
                }
                if (!Page.IsPostBack)
                {
                    if (Session["mode"].ToString() != "n")
                    {
                        LoadForm();
                        if (Session["mode"].ToString() == "v")
                            DisableControl();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void LoadForm()
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    Guid ApplicationID = new Guid(hfApplicationID.Value.ToString());
                    OperationApp item = dbContext.OperationApps.Where(w => w.OperationAppID == ApplicationID).FirstOrDefault<OperationApp>();

                    if (item.PaymentDate != null)
                        dtPaymentDate.Value = item.PaymentDate;
                    else
                        dtPaymentDate.Value = DateTime.Now;

                    if (item.PaymentTime != null)
                        paymentTime.Value = item.PaymentTime;
                    else
                        paymentTime.Value = DateTime.Now;

                    if (item.PaymentAmount !=null)
                    txtPaymentAmt.Text = item.PaymentAmount.ToString();

                    if(item.ReceiptNo != null)
                    txtReceiptNo.Text = item.ReceiptNo.ToString();

                    if (item.PaymentRefID != null)
                        txtPermitRef.Text = item.PaymentRefID.ToString();
                   
                    dbContext.Dispose();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void DisableControl()
        {
            dtPaymentDate.Enabled = false;
            paymentTime.Enabled = false;
            txtPaymentAmt.Enabled = false;
            txtReceiptNo.Enabled = false;
            txtPermitRef.Enabled = false;
        }
        protected void btnUpdatePayment_Click(object sender, EventArgs e)
        {
            string permitURL = "";
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    DALOperation objOperationApp = new DAL.DALOperation();
                    Guid operationAppID = new Guid(hfApplicationID.Value.ToString());

                    OperationApp item = dbContext.OperationApps.Find(operationAppID);

                    item.IsPayment = true;
                    item.PaymentDate = Convert.ToDateTime(dtPaymentDate.Value);
                    item.PaymentTime = new DateTime(item.PaymentDate.Value.Year, item.PaymentDate.Value.Month, item.PaymentDate.Value.Day, Convert.ToDateTime(paymentTime.Value).Hour, Convert.ToDateTime(paymentTime.Value).Minute, 0);
                    item.PaymentAmount = Convert.ToDouble(txtPaymentAmt.Text);
                    item.ReceiptNo = txtReceiptNo.Text;
                    item.PaymentRefID = txtPermitRef.Text;
                    item.PermitIssuerUserID = Session["UserID"].ToString();
                    item.IsAppCompleted = true;
                    item.CompletedDate = DateTime.Now;

                    string folderDirectory = Server.MapPath("Upload/" + item.CompID + "/" + item.OperationAppID.ToString());
                    string fileName = "Permit_" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string PermitQRFilePath = "~/Operation/Upload/" + item.CompID + "/" + item.OperationAppID.ToString() + "/" + "qrcode_" + fileName + ".jpg";

                    item.PermitDocLink = objOperationApp.GenerateQRCode(item.OperationAppID.ToString(), item.CompID, folderDirectory, fileName, Server.MapPath(PermitQRFilePath)); //GeneratePermit(operationAppID.ToString(),item.CompID, item.RefID,ref QRPhysicalPath);
                    item.PermitQRCode = Server.MapPath(PermitQRFilePath);

                    dbContext.SaveChanges();
                    Guid? supplyMethod = dbContext.SystemParams.FirstOrDefault<SystemParam>().SupplyMethodA;

                    if(item.SupplyMethodID== supplyMethod)
                        GeneratePermit(item.OperationAppID.ToString(), item.CompID, item.PermitDocLink,"A");
                    else
                        GeneratePermit(item.OperationAppID.ToString(), item.CompID, item.PermitDocLink, "B");
                    permitURL = item.PermitDocLink;

                    SystemParam sysParam = dbContext.SystemParams.FirstOrDefault<SystemParam>();
                    
                    //Update Pending Payment
                    OperationAppFlow opAppFlow = dbContext.OperationAppFlows.Where(w => w.OperationAppID == operationAppID && w.IsActive == true).FirstOrDefault<OperationAppFlow>();

                    if (opAppFlow.FlowActionStatusID == sysParam.FlowPendingCM || opAppFlow.FlowActionStatusID == sysParam.FlowPendingBL) //Resubmit
                    {
                        opAppFlow.ActionDate = DateTime.Now;
                        opAppFlow.ActionBy = Session["UserID"].ToString();
                        opAppFlow.IsComplete = false;
                        opAppFlow.IsReject = false;
                        opAppFlow.IsActive = true;

                        DALOperation.PendingEmailSTS(item.OperationAppID, sysParam.FlowPermitIssued, false);

                        dbContext.SaveChanges();
                    }
                    else
                    {
                        //Flow Pending Payment
                        opAppFlow.ActionDate = DateTime.Now;
                        opAppFlow.ActionBy = Session["UserID"].ToString();
                        opAppFlow.IsComplete = false;
                        opAppFlow.IsReject = false;
                        opAppFlow.IsActive = false;

                        //Create Permit Issued
                        OperationAppFlow AppFlowPI = new OperationAppFlow();
                        Guid? FlowActionStatusIDPI = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPermitIssued;
                        AppFlowPI.OperationAppFlowID = Guid.NewGuid();
                        AppFlowPI.OperationAppID = operationAppID;
                        AppFlowPI.FlowActionStatusID = FlowActionStatusIDPI;
                        AppFlowPI.IsComplete = false;
                        AppFlowPI.IsReject = false;
                        AppFlowPI.IsActive = false;
                        AppFlowPI.CreatedDate = DateTime.Now.AddSeconds(1);
                        AppFlowPI.CreatedBy = Session["UserID"].ToString(); ;
                        AppFlowPI.ActionDate = DateTime.Now.AddSeconds(1);
                        AppFlowPI.ActionBy = Session["UserID"].ToString();
                        dbContext.OperationAppFlows.Add(AppFlowPI);

                        //Create Pending CM/BL
                        OperationAppFlow AppFlowBL = new OperationAppFlow();
                        if(item.SupplyMethodID==sysParam.SupplyMethodA)
                            AppFlowBL.FlowActionStatusID = sysParam.FlowPendingCM;
                        else
                            AppFlowBL.FlowActionStatusID = sysParam.FlowPendingBL;

                        AppFlowBL.OperationAppFlowID = Guid.NewGuid();
                        AppFlowBL.OperationAppID = operationAppID;
                       
                        AppFlowBL.IsComplete = false;
                        AppFlowBL.IsReject = false;
                        AppFlowBL.IsActive = true;
                        AppFlowBL.CreatedDate = DateTime.Now.AddSeconds(2);
                        AppFlowBL.CreatedBy = Session["UserID"].ToString(); ;
                        dbContext.OperationAppFlows.Add(AppFlowBL);

                        dbContext.SaveChanges();
                        DALOperation.PendingEmailSTS(item.OperationAppID, sysParam.FlowPermitIssued, false);
                    }
                }
                //Response.Redirect(permitURL, false);
                ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", permitURL.Substring(12)));
               

            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        #region Generate Permit & QRCode
        protected void GeneratePermit(string operationAppID, string refID, string permitFilePath,string method)
        {
            try
            {
                ReportDocument oRpt = new ReportDocument();
                string dbServer = System.Web.Configuration.WebConfigurationManager.AppSettings["DBServer"];
                string dbCatalog = WebConfigurationManager.AppSettings["DBCatalog"];
                string dbUser = WebConfigurationManager.AppSettings["DBUser"];
                string dbPass = WebConfigurationManager.AppSettings["DBPass"];

                if(method=="A")
                    oRpt.Load(Server.MapPath("~/Operation/PetrolPermitNew.rpt"));
                else
                    oRpt.Load(Server.MapPath("~/Operation/PetrolPermitBNew.rpt"));

               

                oRpt.SetDatabaseLogon(dbUser, dbPass, dbServer, dbCatalog);

                 DALOperation objMain = new DALOperation();
                objMain.Get_OperationAppList(operationAppID);

                oRpt.SetDataSource(objMain.ds.Tables["v_permit"]);

                string fileName = refID + "_" + DateTime.Now.ToString("yyyyMMddHHmmssffff");

                string permitFullPath = Server.MapPath(permitFilePath);
               // string contentType = "application/pdf";


                CrystalDecisions.Shared.DiskFileDestinationOptions dfo = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                dfo.DiskFileName = permitFullPath;
                oRpt.ExportOptions.DestinationOptions = dfo;
                oRpt.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                oRpt.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                oRpt.Export();
                oRpt.Close();

               // Response.ClearContent();
               // Response.ClearHeaders();
               // Response.ContentType = contentType;
               // Response.WriteFile(permitFullPath);
               // Response.Close();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }



        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (hfMethod.Value.ToString() == "a")
                Response.Redirect("~//Operation/ApprDashboardA.aspx", false);
            else
                Response.Redirect("~//Operation/ApprDashboardB.aspx", false);
        }
    }
}