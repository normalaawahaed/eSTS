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

namespace eSTS.Operation
{
    public partial class HistoryActivityB : System.Web.UI.Page
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
                if (!Page.IsPostBack){
                    dtOperationDateFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
                    dtOperationDateTo.Value =new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 0, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void gridComplete_HtmlDataCellPrepared(object sender, DevExpress.Web.Bootstrap.BootstrapGridViewTableDataCellEventArgs e)
        {
            try
            {
                
                if (e.DataColumn.FieldName == "VSName")
                {
                    BootstrapGridView gv = (BootstrapGridView)sender;
                    string vsName = gv.GetRowValues(e.VisibleIndex, "VSName").ToString();
                    string vsIMO = gv.GetRowValues(e.VisibleIndex, "VSIMONo").ToString();
                    string vsPOR = gv.GetRowValues(e.VisibleIndex, "VSPortReg").ToString();

                    e.Cell.Text = @"<p><span><strong>Vessel Name :</strong> " + vsName + "</span><br>" +
                        "<span><strong>IMO NO. :</strong> " + vsIMO + "</span><br>" +
                    "<span><strong>POR :</strong> " + vsPOR + "</span><br>";
                }
                if (e.DataColumn.FieldName == "VRName")
                {
                    BootstrapGridView gv = (BootstrapGridView)sender;
                    string vrName = gv.GetRowValues(e.VisibleIndex, "VRName").ToString();
                    string vrIMO = gv.GetRowValues(e.VisibleIndex, "VRIMONo").ToString();
                    string vrPOR = gv.GetRowValues(e.VisibleIndex, "VRPortReg").ToString();
                    string latDegree = gv.GetRowValues(e.VisibleIndex, "VRLatDegree").ToString();
                    string latMin = gv.GetRowValues(e.VisibleIndex, "VRLatMin").ToString();
                    string longDegree = gv.GetRowValues(e.VisibleIndex, "VRLongDegree").ToString();
                    string longMin = gv.GetRowValues(e.VisibleIndex, "VRLongMin").ToString();

                    e.Cell.Text = @"<p><span><strong>Vessel Name :</strong> " + vrName + "</span><br>" +
                        "<span><strong>IMO NO. :</strong> " + vrIMO + "</span><br>" +
                    "<span><strong>POR :</strong> " + vrPOR + "</span><br>" +
                                      "<span><strong>Lat (DMS) :</strong> " + latDegree + "° " + latMin + "<br>" +
                                      "<span><strong>Long (DMS):</strong> " + longDegree + "° " + longMin + "</span><br>" +
                                      "</p>";
                }
                if (e.DataColumn.FieldName == "UOMCode")
                {
                    BootstrapGridView gv = (BootstrapGridView)sender;
                    string operationDate = Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "EstOperationTime").ToString()).ToString("dd/MM/yyyy");
                    string operationTime = Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "EstOperationTime").ToString()).ToString("HH:mm");
                    string oilType = gv.GetRowValues(e.VisibleIndex, "OilTypeDesc").ToString();
                    string uom = gv.GetRowValues(e.VisibleIndex, "UOMCode").ToString();
                    string oilAmt = gv.GetRowValues(e.VisibleIndex, "EstOilMT").ToString();
                    string delLoc = gv.GetRowValues(e.VisibleIndex, "DeliveryLocation").ToString();

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
                    e.Cell.Text = "<p><span class='label label-" + labelColor + "'>" + action + " </span></P>";

                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }


        #region Literal Init

        protected void lilView_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            contentUrl = string.Format("OpAppB.aspx?mode=v&appr=0&adm=0&sno={0}", DataBinder.Eval(container.DataItem, "OperationAppID"));

            link.EncodeHtml = false;
            link.NavigateUrl = contentUrl;// "javascript:void(0);";
            //link.Text = "<i class='fa fa-eye fa-lg text-success' aria-hidden='true'>" + string.Format("{0}", "") + "</i>";
            link.Text = "<button class='btn btn-default btn-circle btn-outline' type='button'><i class='fa fa-eye'></i></button>" + string.Format("{0}", "") + "</i>";
            link.Target = "self";
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
            if (DataBinder.Eval(container.DataItem, "FlowActionStatusID").ToString() != "be03d85d-f092-4d6f-be7f-ce883fe58aca")
            {
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
        }

        protected void lilNotis_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            link.EncodeHtml = false;

            if (DataBinder.Eval(container.DataItem, "NPDocLink").ToString() != "")
            {
                contentUrl = string.Format("{0}", DataBinder.Eval(container.DataItem, "NPDocLink"));

                link.EncodeHtml = false;
                link.NavigateUrl = contentUrl;// "javascript:void(0);";
                link.Text = " <button class='btn btn-success btn-circle btn-outline' type='button'><i class='fa fa-file-pdf-o'></i></button>" + string.Format("{0}", "") + "</i>";
                link.Target = "blank";
                link.ToolTip = "STS Notice";
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
            if(DataBinder.Eval(container.DataItem, "OperationAppID").ToString()=="b30d30b7-498f-4b95-a18d-ec04bed99fce")
            {
                int a = 0;
            }
            contentUrl = string.Format("UploadCMBL_B.aspx?mode=v&m=a&appr=0&adm=0&sno={0}", DataBinder.Eval(container.DataItem, "OperationAppID"));

            link.EncodeHtml = false;
            if (DataBinder.Eval(container.DataItem, "FlowActionStatusID").ToString() == "7f99dadd-6895-4091-a5f3-c2803acfff1f" || Convert.ToBoolean(DataBinder.Eval(container.DataItem, "IsSubmitBL")) == true)
            {
                link.NavigateUrl = contentUrl;// "javascript:void(0);";
                link.Text = "<button class='btn btn-success btn-circle btn-outline' type='button'><i class='fa fa-clipboard'></i></button>" + string.Format("{0}", "") + "</i>";
                link.Target = "_self";
                link.ToolTip = "View Bill Of Lading";
            }
        }

        #endregion
        protected void gridComplete_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {
                gridComplete.DataBind();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void dsComplete_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new Database.eSTS_StagEntities())
                {
                    //string flowPermitIssued = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPermitIssued.ToString();
                    //string flowPendingBL = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPendingBL.ToString();
                    //string flowPendingPayment = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPendingPayment.ToString();
                    string flowCancel = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowCancel.ToString();
                    string flowComplete = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowComplete.ToString();
                    string flowAmendReject = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowAmendRejected.ToString();
                    string supplyMethodID = dbContext.SystemParams.FirstOrDefault<SystemParam>().SupplyMethodB.ToString();

                    e.DataSource.WhereParameters.Clear();
                    if (Convert.ToInt32(Session["PortLocation"]) == 0)
                        e.DataSource.Where = "it.[CompID]=@pCompID and it.[SupplyMethodID] = @pSupplyMethodID and (it.[FlowActionStatusID] = @pComplete or it.[FlowActionStatusID] = @pCancel or it.[FlowActionStatusID] = @pflowAmendReject)";
                    else
                    {
                        if (Session["PermitIssuerID"].ToString() != "")
                            e.DataSource.Where = "it.[CompID]=@pCompID and it.[SupplyMethodID]=@pSupplyMethodID and it.[Location]=@pLocation and it.[PermitIssuerID]=@pPermitIssuerID and (it.[FlowActionStatusID] = @pComplete or it.[FlowActionStatusID] = @pCancel or it.[FlowActionStatusID] = @pflowAmendReject)";
                        else
                            e.DataSource.Where = "it.[CompID]=@pCompID and it.[SupplyMethodID]=@pSupplyMethodID and it.[Location]=@pLocation and (it.[FlowActionStatusID] = @pComplete or it.[FlowActionStatusID] = @pCancel or it.[FlowActionStatusID] = @pflowAmendReject)";
                    }
                    e.DataSource.WhereParameters.Add("pCompID", DbType.String, Session["CompID"].ToString());
                    e.DataSource.WhereParameters.Add("pLocation", TypeCode.Int32, Session["PortLocation"].ToString());
                    e.DataSource.WhereParameters.Add("pSupplyMethodID", DbType.Guid, supplyMethodID);
                    e.DataSource.WhereParameters.Add("pPermitIssuerID", DbType.Guid, Session["PermitIssuerID"].ToString());
                    e.DataSource.WhereParameters.Add("pComplete", DbType.Guid, flowComplete);
                    e.DataSource.WhereParameters.Add("pCancel", DbType.Guid, flowCancel);
                    e.DataSource.WhereParameters.Add("pflowAmendReject", DbType.Guid, flowAmendReject);
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