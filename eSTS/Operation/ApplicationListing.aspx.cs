using Apps.Common;
using CrystalDecisions.CrystalReports.Engine;
using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using eSTS.DAL;
using eSTS.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eSTS.Common;
using System.Web.Configuration;
using DMSLatLongConverter;
using System.IO;

namespace eSTS.Operation
{
    public partial class ApplicationListing : System.Web.UI.Page
    {
         protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Check Session
                if (Request.QueryString.Count > 0)
                {
                    Session["mode"] = Request.QueryString["mode"].ToString();
                }
                else if ((Session["UserID"] == null) && (Session["UserGroup"] == null))//&& (Session["UserLevel"] != null))
                {
                    Response.Redirect("~//SignIn.aspx", true);
                   
                }
                else
                {
                    Session["mode"] = "";
                }
                if(!Page.IsPostBack)
                {
                    dtOperationDateFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
                    dtOperationDateTo.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0).AddDays(1);
                    //if (Session["mode"].ToString() != "p")
                    //{
                    //    if (Session["UserGroupDesc"].ToString() == "VTMSPG" || Session["UserGroupDesc"].ToString() == "VTMSPTP")
                    //        cbStatus.Value = "3";
                    //    else
                    //        cbStatus.Value = "1";
                    //}
                    //else
                    //    cbStatus.Value = "1";
                }
                
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void gridActive_HtmlDataCellPrepared(object sender, DevExpress.Web.Bootstrap.BootstrapGridViewTableDataCellEventArgs e)
        {
            try
            {
                //if (e.DataColumn.FieldName == "DeliveryLocation")
                //{
                //    BootstrapGridView gv = (BootstrapGridView)sender;
                //    string delLoc = gv.GetRowValues(e.VisibleIndex, "DeliveryLocation").ToString();
                //    e.Cell.Text = @"<p><span><strong>Delivery Location :</strong> " + delLoc + "</span><br>";

                //}
                if (e.DataColumn.FieldName == "VSName")
                {
                    BootstrapGridView gv = (BootstrapGridView)sender;
                    string method = gv.GetRowValues(e.VisibleIndex, "MethodCode").ToString();
                    string Name = "";  
                    string IMO = "";  
                    string POR = "";
                    string latDegree = "";
                    string latMin = "";
                    string longDegree = "";
                    string longMin = "";
                    string latitude = "";
                    string longitude = "";

                    if (method == "A")
                    {
                        Name = gv.GetRowValues(e.VisibleIndex, "VRName").ToString();
                        IMO = gv.GetRowValues(e.VisibleIndex, "VRIMONo").ToString();
                        POR = gv.GetRowValues(e.VisibleIndex, "VRPortReg").ToString();
                        latDegree = gv.GetRowValues(e.VisibleIndex, "VRLatDegree").ToString();
                        latMin = gv.GetRowValues(e.VisibleIndex, "VRLatMin").ToString();
                        longDegree = gv.GetRowValues(e.VisibleIndex, "VRLongDegree").ToString();
                        longMin = gv.GetRowValues(e.VisibleIndex, "VRLongMin").ToString();
                        latitude = gv.GetRowValues(e.VisibleIndex, "VRLatitude").ToString();
                        longitude = gv.GetRowValues(e.VisibleIndex, "VRLongitude").ToString();
                    }
                    else
                    {
                        Name = gv.GetRowValues(e.VisibleIndex, "VSName").ToString();
                        IMO = gv.GetRowValues(e.VisibleIndex, "VSIMONo").ToString();
                        POR = gv.GetRowValues(e.VisibleIndex, "VSPortReg").ToString();
                        latDegree = gv.GetRowValues(e.VisibleIndex, "VSLatDegree").ToString();
                        latMin = gv.GetRowValues(e.VisibleIndex, "VSLatMin").ToString();
                        longDegree = gv.GetRowValues(e.VisibleIndex, "VSLongDegree").ToString();
                        longMin = gv.GetRowValues(e.VisibleIndex, "VSLongMin").ToString();
                        latitude = gv.GetRowValues(e.VisibleIndex, "VSLatitude").ToString();
                        longitude = gv.GetRowValues(e.VisibleIndex, "VSLongitude").ToString();
                    }

                    e.Cell.Text = @"<p><span><strong>FSU Vessel Name :</strong> " + Name + "</span><br>" +
                        "<span><strong>FSU IMO NO. :</strong> " + IMO + "</span><br>" +
                    "<span><strong> FSU POR :</strong> " + POR + "</span><br>"+
                    "<span><strong>Lat (DMS) :</strong> " + latDegree + "° " + latMin + "<br>" +
                                      "<span><strong>Long (DMS):</strong> " + longDegree + "° " + longMin + "</span><br>";
                }
                if (e.DataColumn.FieldName == "VRName")
                {
                    BootstrapGridView gv = (BootstrapGridView)sender;
                    string method = gv.GetRowValues(e.VisibleIndex, "MethodCode").ToString();
                    string methodName = gv.GetRowValues(e.VisibleIndex, "MedhodName").ToString();
                    string Name = "";
                    string IMO = "";
                    string POR = "";
                   
                    
                    if (method == "A")
                    {
                        Name = gv.GetRowValues(e.VisibleIndex, "VRName").ToString();
                        IMO = gv.GetRowValues(e.VisibleIndex, "VRIMONo").ToString();
                        POR = gv.GetRowValues(e.VisibleIndex, "VRPortReg").ToString();
                        
                    }
                    else
                    {
                        Name = gv.GetRowValues(e.VisibleIndex, "VSName").ToString();
                        IMO = gv.GetRowValues(e.VisibleIndex, "VSIMONo").ToString();
                        POR = gv.GetRowValues(e.VisibleIndex, "VSPortReg").ToString();
                       
                    }
                
                    e.Cell.Text = @"<p><span><strong>Method : " + methodName + "</strong></span><br><br>" +
                        "<span><strong> Vessel Name :</strong> " + Name + "</span><br>" +
                        "<span><strong>IMO NO. :</strong> " + IMO + "</span><br>" +
                    "<span><strong>POR :</strong> " + POR + "</span><br>" +
                                      
                                      "</p>";
                }
                if (e.DataColumn.FieldName == "UOMCode")
                {
                    BootstrapGridView gv = (BootstrapGridView)sender;
                    string delLoc = gv.GetRowValues(e.VisibleIndex, "DeliveryLocation").ToString();
                    string operationDate = Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "EstOperationTime").ToString()).ToString("dd/MM/yyyy");
                    string operationTime = Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "EstOperationTime").ToString()).ToString("HH:mm");
                    string oilType = gv.GetRowValues(e.VisibleIndex, "OilTypeDesc").ToString();
                    string uom = gv.GetRowValues(e.VisibleIndex, "UOMCode").ToString();
                    string oilAmt = gv.GetRowValues(e.VisibleIndex, "EstOilMT").ToString();

                    e.Cell.Text = @"<p><span><strong>Delivery Location :</strong> " + delLoc + "</span><br>" +
                                "<p><span><strong>Operation Date :</strong> " + operationDate + "</span><br>" +
                        "<span><strong>Operation Time :</strong> " + operationTime + "</span><br>" +
                        "<span><strong>Oil Type :</strong> " + oilType + "</span><br>" +
                    "<span><strong>Est Oil Amt :</strong> " + oilAmt + "(" + uom + ")</span><br>";
                }
                if (e.DataColumn.FieldName == "ActionStatus")
                {

                    BootstrapGridView gv = (BootstrapGridView)sender;
                    string action = gv.GetRowValues(e.VisibleIndex, "ActionStatus").ToString();
                    string labelColor = gv.GetRowValues(e.VisibleIndex, "LabelColor").ToString();
                    if (action.Length > 30)
                    {
                        e.Cell.Text = "<p><span class='label label-" + labelColor + "'>" + action.Substring(0, 22) + "<br>" +
                            action.Substring(22, action.Length - 22) + "</span></P>";
                    }
                    else
                        e.Cell.Text = "<p><span class='label label-" + labelColor + "'>" + action + " </span></P>";

                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        protected void lilView2_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            contentUrl = string.Format("OpAppA.aspx?mode=v&appr=0&adm=1&sno={0}", DataBinder.Eval(container.DataItem, "OperationAppID"));

            link.EncodeHtml = false;
            link.NavigateUrl = contentUrl;// "javascript:void(0);";
            link.Text = "<button class='btn btn-default btn-circle btn-outline' type='button'><i class='fa fa-eye'></i></button>" + string.Format("{0}", "") + "</i>";
            link.Target = "_self";
            link.ToolTip = "View Application";
        }
        protected void lilPayment_Init(object sender, EventArgs e)
        {
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            link.EncodeHtml = false;

            if (Convert.ToBoolean(DataBinder.Eval(container.DataItem, "IsPayment")) == true)
            {
                string dtPayYear = Convert.ToDateTime(DataBinder.Eval(container.DataItem, "PaymentDate")).Year.ToString();
                string dtPayMth = Convert.ToDateTime(DataBinder.Eval(container.DataItem, "PaymentDate")).Month.ToString();
                string dtPayDay = Convert.ToDateTime(DataBinder.Eval(container.DataItem, "PaymentDate")).Day.ToString();
                string timePayHour = Convert.ToDateTime(DataBinder.Eval(container.DataItem, "PaymentTime")).Hour.ToString();
                string timePayMin = Convert.ToDateTime(DataBinder.Eval(container.DataItem, "PaymentTime")).Minute.ToString();
                string paymentAmt = Convert.ToDouble(DataBinder.Eval(container.DataItem, "PaymentAmount")).ToString();
                string receiptNo = DataBinder.Eval(container.DataItem, "ReceiptNo").ToString();
                string permitRef = DataBinder.Eval(container.DataItem, "PaymentRefID").ToString();

                link.Text = " <button class='btn btn-warning btn-circle btn-outline' type='button'><i class='fa fa-dollar'></i></button>" + string.Format("{0}", "") + "</i>";
                //link.Text = "<a href='javascript:void(0);' onclick='OnMoreInfoClick(this, '')'>More Info...</a>";
                link.NavigateUrl = "javascript:popUpPayment('" + DataBinder.Eval(container.DataItem, "OperationAppID") + "','" + dtPayYear + "','" + dtPayMth + "','" + dtPayDay + "','" + timePayHour + "','" + timePayMin + "','" + paymentAmt + "','" + receiptNo + "','" + permitRef + "');";
                link.ToolTip = "Payment Details";
            }
        }
        protected void lilPermit_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            link.EncodeHtml = false;

            if (Convert.ToBoolean(DataBinder.Eval(container.DataItem, "IsPayment")) == true)
            {
                contentUrl = string.Format("{0}", DataBinder.Eval(container.DataItem, "PermitDocLink"));

                link.EncodeHtml = false;
                link.NavigateUrl = contentUrl;// "javascript:void(0);";
                link.Text = " <button class='btn btn-primary btn-circle btn-outline' type='button'><i class='fa fa-file-pdf-o'></i></button>" + string.Format("{0}", "") + "</i>";
                link.Target = "blank";
                link.ToolTip = "Permit";
            }
        }
        protected void LilLampiran_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            link.EncodeHtml = false;

            if (DataBinder.Eval(container.DataItem, "Lampiran1DocLink").ToString() != "")
            {
                contentUrl = string.Format("{0}", DataBinder.Eval(container.DataItem, "Lampiran1DocLink"));

                link.EncodeHtml = false;
                link.NavigateUrl = contentUrl;// "javascript:void(0);";
                link.Text = "<button class='btn btn-info btn-circle btn-outline' type='button'><i class='fa fa-file-pdf-o'></i></button>" + string.Format("{0}", "") + "</i>";
                link.Target = "blank";
                link.ToolTip = "Lampiran 1 491B";
            }
        }
        protected void lilCM_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            if (DataBinder.Eval(container.DataItem, "MethodCode").ToString() == "A")
                contentUrl = string.Format("UploadCMBL.aspx?mode=v&m=a&appr=1&adm=1&sno={0}", DataBinder.Eval(container.DataItem, "OperationAppID"));
            else
                contentUrl = string.Format("UploadCMBL_B.aspx?mode=v&m=a&appr=1&adm=1&sno={0}", DataBinder.Eval(container.DataItem, "OperationAppID"));

            link.EncodeHtml = false;
            // string FlowApprovedDec = "1f6246de-0afa-41c2-be83-b02ee03383ee";

            //if (DataBinder.Eval(container.DataItem, "FlowActionStatusID").ToString() == "333a69e2-104f-4d87-acc9-8ce0ee87204b")
            //{
            //    link.NavigateUrl = contentUrl;// "javascript:void(0);";
            //    link.Text = "<button class='btn btn-success btn-circle btn-outline' type='button'><i class='fa fa-upload'></i></button>" + string.Format("{0}", "") + "</i>";
            //    link.Target = "_self";
            //    link.ToolTip = "Upload Cargo Manifest / Survey Report / Bill Of Lading";
            //}
            //else if (Convert.ToBoolean(DataBinder.Eval(container.DataItem, "IsSubmitCM")) == true)
            //{
            //    link.NavigateUrl = contentUrl;// "javascript:void(0);";
            //    link.Text = "<button class='btn btn-success btn-circle btn-outline' type='button'><i class='fa fa-clipboard'></i></button>" + string.Format("{0}", "") + "</i>";
            //    link.Target = "_self";
            //    link.ToolTip = "Upload Cargo Manifest / Survey Report / Bill Of Lading";
            //}
            if (DataBinder.Eval(container.DataItem, "FlowActionStatusID").ToString() == "FlowApprovedDec")
            {
                link.NavigateUrl = contentUrl;// "javascript:void(0);";
                link.Text = "<button class='btn btn-success btn-circle btn-outline' type='button'><i class='fa fa-clipboard'></i></button>" + string.Format("{0}", "") + "</i>";
                link.Target = "_self";
                link.ToolTip = "Upload Cargo Manifest / Survey Report / Bill Of Lading";
            }
        }

        protected void dsOperationApp_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new Database.eSTS_StagEntities())
                {
                    string filter = "";
                    string flowDraft =  dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowDraft.ToString();
                    string flowSubmit = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowSubmit.ToString();
                    string flowReject = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowReject.ToString();
                    string flowPendingApproval = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPendingApproval.ToString();
                    string flowPermitIssued = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPermitIssued.ToString();
                    string flowPendingCM = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPendingCM.ToString();
                    string flowPendingBL = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPendingBL.ToString();
                    string flowPendingPayment = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPendingPayment.ToString();
                    string flowProcessInv = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowProcessInvoice.ToString();
                    string flowPendingApprovedDec = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPendingApproveDec.ToString();
                    string flowRejectDec = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowRejectDec.ToString();
                    string supplyMethod = "";

                    if (Convert.ToInt32(cbMethod.Value) == 1)
                    {
                        supplyMethod = dbContext.SystemParams.FirstOrDefault<SystemParam>().SupplyMethodA.ToString();
                        filter = "and it.[SupplyMethodID] = @pSupplyMethod";
                    }
                    else if (Convert.ToInt32(cbMethod.Value) == 2)
                    {
                        supplyMethod = dbContext.SystemParams.FirstOrDefault<SystemParam>().SupplyMethodB.ToString();
                        filter = "and it.[SupplyMethodID] = @pSupplyMethod";
                    }

                    e.DataSource.WhereParameters.Clear();
                    e.DataSource.Where = "(it.[EstOperationDateTime] >= @pDateFrom and it.[EstOperationDateTime]<= @pDateTo) and (it.[FlowActionStatusID] = @pPermitIssued or it.[FlowActionStatusID] = @pPendingCM or it.[FlowActionStatusID] = @pPendingBL or it.[FlowActionStatusID] = @pPendingPayment or it.[FlowActionStatusID] = @pflowPendingApprovedDec or it.[FlowActionStatusID] = @pflowRejectDec or it.[FlowActionStatusID] = @pflowProcessInv)" + filter;
                    //e.DataSource.WhereParameters.Add("pCompID", DbType.String, Session["CompID"].ToString());
                    //e.DataSource.WhereParameters.Add("pLocation", TypeCode.Int32, Session["PortLocation"].ToString());
                    //e.DataSource.WhereParameters.Add("pPermitIssuerID", DbType.Guid, Session["PermitIssuerID"].ToString());
                    e.DataSource.WhereParameters.Add("pFlowDraft", DbType.Guid, flowDraft);
                    e.DataSource.WhereParameters.Add("pFlowReject", DbType.Guid, flowReject);
                    e.DataSource.WhereParameters.Add("pFlowSubmit", DbType.Guid, flowSubmit);
                    e.DataSource.WhereParameters.Add("pflowPendingApproval", DbType.Guid, flowPendingApproval);
                    e.DataSource.WhereParameters.Add("pPermitIssued", DbType.Guid, flowPermitIssued);
                    e.DataSource.WhereParameters.Add("pPendingPayment", DbType.Guid, flowPendingPayment);
                    e.DataSource.WhereParameters.Add("pPendingCM", DbType.Guid, flowPendingCM);
                    e.DataSource.WhereParameters.Add("pPendingBL", DbType.Guid, flowPendingBL);
                    e.DataSource.WhereParameters.Add("pflowProcessInv", DbType.Guid, flowProcessInv);
                    e.DataSource.WhereParameters.Add("pflowPendingApprovedDec", DbType.Guid, flowPendingApprovedDec);
                    e.DataSource.WhereParameters.Add("pflowRejectDec", DbType.Guid, flowRejectDec);
                    e.DataSource.WhereParameters.Add("pDateFrom", DbType.DateTime, dtOperationDateFrom.Value.ToString());
                    e.DataSource.WhereParameters.Add("pDateTo", DbType.DateTime, dtOperationDateTo.Value.ToString());

                    if (Convert.ToInt32(cbMethod.Value) != 0 )
                        e.DataSource.WhereParameters.Add("pSupplyMethod", DbType.Guid, supplyMethod);

                    dbContext.Dispose();
                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(ex.InnerException, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
             
        }

       
           
    }
}