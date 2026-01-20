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
    public partial class ApprDashboardA : System.Web.UI.Page
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

            contentUrl = string.Format("OpAppA.aspx?mode=v&appr=1&adm=0&sno={0}", DataBinder.Eval(container.DataItem, "OperationAppID"));

            link.EncodeHtml = false;
            link.NavigateUrl = contentUrl;// "javascript:void(0);";
            //link.Text = "<i class='fa fa-eye fa-lg text-success' aria-hidden='true'>" + string.Format("{0}", "") + "</i>";
            link.Text = "<button class='btn btn-default btn-circle btn-outline' type='button'><i class='fa fa-eye'></i></button>" + string.Format("{0}", "") + "</i>";
            link.Target = "_self";
            link.ToolTip = "View Application";
        }
        protected void lilPayment_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            contentUrl = string.Format("JLMPayment.aspx?mode=e&method=a&appr=1&adm=0&sno={0}", DataBinder.Eval(container.DataItem, "OperationAppID"));

            link.EncodeHtml = false;
            link.NavigateUrl = contentUrl;// "javascript:void(0);";
            //link.Text = "<i class='fa fa-dollar fa-lg text-success' aria-hidden='true'>" + string.Format("{0}", "") + "</i>";
             link.Text = " <button class='btn btn-warning btn-circle btn-outline' type='button'><i class='fa fa-dollar'></i></button>" + string.Format("{0}", "") + "</i>";
            //link.Target = "_self";
            link.ToolTip = "Pay and Generate Permit";
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
                link.Target = "_blank";
                link.ToolTip = "Permit";
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
                link.Target = "_blank";
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
                link.Target = "_blank";
                link.ToolTip = "Lampiran 1 491B";
            }
        }
        protected void lilCM_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;

            contentUrl = string.Format("UploadCMBL.aspx?mode=v&m=a&appr=1&adm=0&sno={0}", DataBinder.Eval(container.DataItem, "OperationAppID"));

            link.EncodeHtml = false;
            if (DataBinder.Eval(container.DataItem, "FlowActionStatusID").ToString() == "333a69e2-104f-4d87-acc9-8ce0ee87204b" || Convert.ToBoolean(DataBinder.Eval(container.DataItem, "IsSubmitCM")) == true)
            {
                link.NavigateUrl = contentUrl;// "javascript:void(0);";
                link.Text = "<button class='btn btn-success btn-circle btn-outline' type='button'><i class='fa fa-clipboard'></i></button>" + string.Format("{0}", "") + "</i>";
                link.Target = "_self";
                link.ToolTip = "View Cargo Manifest / Bill Of Lading";
            }
        }
        protected void lilCancel_Init(object sender, EventArgs e)
        {
            string contentUrl = "";
            ASPxHyperLink link = (ASPxHyperLink)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)link.NamingContainer;
            contentUrl = string.Format("OpAppA.aspx?mode=c&appr=1&adm=0&sno={0}", DataBinder.Eval(container.DataItem, "OperationAppID"));

            link.EncodeHtml = false;
            link.NavigateUrl = contentUrl;// "javascript:void(0);";
            link.Text = " <button class='btn btn-danger btn-circle btn-outline' type='button'><i class='fa fa-times'></i></button>" + string.Format("{0}", "") + "</i>";
            link.Target = "_self";
            link.ToolTip = "Cancel Application";
        }
     
        #endregion

        #region Grid
        protected void grid_HtmlDataCellPrepared(object sender, DevExpress.Web.Bootstrap.BootstrapGridViewTableDataCellEventArgs e)
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
                    string latitude = gv.GetRowValues(e.VisibleIndex, "VRLatitude").ToString();
                    string longitude = gv.GetRowValues(e.VisibleIndex, "VRLongitude").ToString();
                    string soCompanyName = gv.GetRowValues(e.VisibleIndex, "SOCompanyName").ToString();
                    e.Cell.Text = @"<p><span><strong>Vessel Name :</strong> " + vrName + "</span><br>" +
                        "<span><strong>IMO NO. :</strong> " + vrIMO + "</span><br>" +
                    "<span><strong>POR :</strong> " + vrPOR + "</span><br>" +
                                      "<span><strong>Lat (DMS) :</strong> " + latDegree + "° " + latMin + "'N<br>" +
                                      "<span><strong>Long (DMS):</strong> " + longDegree + "° " + longMin + "'E </span><br>" +
                                          "<a href='javascript:showMap(" + latitude + "," + longitude + ");'>Show Map <i class='fa fa-map-marker text-info' aria-hidden='true'></i></a>" +
                                            "<br><br><span><strong>Operator Name :</strong> " + soCompanyName + "</span></p>";
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
                    e.Cell.Text = @"<p><span><strong>Delivery Location :</strong> " + delLoc + "</span><br>"+
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
                    e.Cell.Text = "<p><span class='label label-"+ labelColor + "'>" + action + " </span></P>";

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
                    string latitude = gv.GetRowValues(e.VisibleIndex, "VRLatitude").ToString();
                    string longitude = gv.GetRowValues(e.VisibleIndex, "VRLongitude").ToString();
                    string soCompanyName = gv.GetRowValues(e.VisibleIndex, "SOCompanyName").ToString();
                    e.Cell.Text = @"<p><span><strong>Vessel Name :</strong> " + vrName + "</span><br>" +
                        "<span><strong>IMO NO. :</strong> " + vrIMO + "</span><br>" +
                    "<span><strong>POR :</strong> " + vrPOR + "</span><br>" +
                                      "<span><strong>Lat (DMS) :</strong> " + latDegree + "° " + latMin + "'N<br>" +
                                      "<span><strong>Long (DMS):</strong> " + longDegree + "° " + longMin + "'E </span><br>" +
                                          "<a href='javascript:showMap(" + latitude + "," + longitude + ");'>Show Map <i class='fa fa-map-marker text-info' aria-hidden='true'></i></a>" +
                                            "<br><br><span><strong>Operator Name :</strong> " + soCompanyName + "</span></p>";
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
                    string validPermit = Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "ValidPermit").ToString()).ToString("dd/MM/yyyy HH:mm");
                    bool isPayment = Convert.ToBoolean(gv.GetRowValues(e.VisibleIndex, "IsPayment").ToString());

                    e.Cell.Text = @"<p><span><strong>Delivery Location :</strong> " + delLoc + "</span><br>" +
                                 "<p><span><strong>Operation Date :</strong> " + operationDate + "</span><br>" +
                         "<span><strong>Operation Time :</strong> " + operationTime + "</span><br>" +
                        "<span><strong>Oil Type :</strong> " + oilType + "</span><br>" +
                    "<span><strong>Est Oil Amt :</strong> " + oilAmt + "(" + uom + ")</span><br>";

                    if (isPayment == true)
                    {
                        if (DateTime.Now <= Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "ValidPermit").ToString()))
                            e.Cell.Text = e.Cell.Text + "<span><strong>Permit Validity :</strong><font color='blue'>" + validPermit + "</font></span><br>";
                        else
                            e.Cell.Text = e.Cell.Text + "<span><strong>Permit Validity :</strong><font color='red'>" + validPermit + "</font></span><br>";
                    }
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
                    {
                        e.Cell.Text = "<p><span class='label label-" + labelColor + "'>" + action + " </span></P>";
                    }

                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        //protected void gridPendingBDN_HtmlDataCellPrepared(object sender, DevExpress.Web.Bootstrap.BootstrapGridViewTableDataCellEventArgs e)
        //{
        //    try
        //    {
        //        if (e.DataColumn.FieldName == "DeliveryLocation")
        //        {
        //            BootstrapGridView gv = (BootstrapGridView)sender;
        //            string delLoc = gv.GetRowValues(e.VisibleIndex, "DeliveryLocation").ToString();
        //            e.Cell.Text = @"<p><span><strong>Delivery Location :</strong> " + delLoc + "</span><br>";

        //        }
        //        if (e.DataColumn.FieldName == "VSName")
        //        {
        //            BootstrapGridView gv = (BootstrapGridView)sender;
        //            string vsName = gv.GetRowValues(e.VisibleIndex, "VSName").ToString();
        //            string vsIMO = gv.GetRowValues(e.VisibleIndex, "VSIMONo").ToString();
        //            string vsPOR = gv.GetRowValues(e.VisibleIndex, "VSPortReg").ToString();

        //            e.Cell.Text = @"<p><span><strong>Vessel Name :</strong> " + vsName + "</span><br>" +
        //                "<span><strong>IMO NO. :</strong> " + vsIMO + "</span><br>" +
        //            "<span><strong>POR :</strong> " + vsPOR + "</span><br>";
        //        }
        //        if (e.DataColumn.FieldName == "VRName")
        //        {
        //            BootstrapGridView gv = (BootstrapGridView)sender;
        //            string vrName = gv.GetRowValues(e.VisibleIndex, "VRName").ToString();
        //            string vrIMO = gv.GetRowValues(e.VisibleIndex, "VRIMONo").ToString();
        //            string vrPOR = gv.GetRowValues(e.VisibleIndex, "VRPortReg").ToString();
        //            string latDegree = gv.GetRowValues(e.VisibleIndex, "VRLatDegree").ToString();
        //            string latMin = gv.GetRowValues(e.VisibleIndex, "VRLatMin").ToString();
        //            string longDegree = gv.GetRowValues(e.VisibleIndex, "VRLongDegree").ToString();
        //            string longMin = gv.GetRowValues(e.VisibleIndex, "VRLongMin").ToString();

        //            e.Cell.Text = @"<p><span><strong>Vessel Name :</strong> " + vrName + "</span><br>" +
        //                "<span><strong>IMO NO. :</strong> " + vrIMO + "</span><br>" +
        //            "<span><strong>POR :</strong> " + vrPOR + "</span><br>" +
        //                              "<span><strong>Lat (DMS) :</strong> " + latDegree + "° " + latMin + "<br>" +
        //                              "<span><strong>Long (DMS):</strong> " + longDegree + "° " + longMin + "</span><br>" +
        //                              "</p>";
        //        }
        //        if (e.DataColumn.FieldName == "UOMCode")
        //        {
        //            BootstrapGridView gv = (BootstrapGridView)sender;
        //            string operationDate = Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "EstOperationTime").ToString()).ToString("dd/MM/yyyy");
        //            string operationTime = Convert.ToDateTime(gv.GetRowValues(e.VisibleIndex, "EstOperationTime").ToString()).ToString("HH:mm");
        //            string oilType = gv.GetRowValues(e.VisibleIndex, "OilTypeDesc").ToString();
        //            string uom = gv.GetRowValues(e.VisibleIndex, "UOMCode").ToString();
        //            string oilAmt = gv.GetRowValues(e.VisibleIndex, "EstOilMT").ToString();

        //            e.Cell.Text = @"<p><span><strong>Operation Date :</strong> " + operationDate + "</span><br>" +
        //                "<span><strong>Operation Time :</strong> " + operationTime + "</span><br>" +
        //                "<span><strong>Oil Type :</strong> " + oilType + "</span><br>" +
        //            "<span><strong>Est Oil Amt :</strong> " + oilAmt + "(" + uom + ")</span><br>";
        //        }
        //        if (e.DataColumn.FieldName == "ActionStatus")
        //        {

        //            BootstrapGridView gv = (BootstrapGridView)sender;
        //            string action = gv.GetRowValues(e.VisibleIndex, "ActionStatus").ToString();

        //            e.Cell.Text = "<p><span class='label label-warning'>" + action + " </span></P>";

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
        //    }
        //}

        protected void grid_CommandButtonInitialize(object sender, BootstrapGridViewCommandButtonEventArgs e)
        {
            //BootstrapGridView gv = (BootstrapGridView)sender;
            //bool isAppCompleted = Convert.ToBoolean(gv.GetRowValues(e.VisibleIndex, "IsAppCompleted").ToString());

            //if (isAppCompleted == true)
            //{
            //    if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Edit)
            //        e.Visible = true;
            //}
        }
        #endregion

        #region Dataset
        protected void dsOperationApp_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new Database.eSTS_StagEntities())
                {
                    string flowPendingApproval = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPendingApproval.ToString();
                    string flowPendingRejected = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowReject.ToString();
                    string flowPendingAmend = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowAmendPending.ToString();
                    string supplyMethodID = dbContext.SystemParams.FirstOrDefault<SystemParam>().SupplyMethodA.ToString();


                    e.DataSource.WhereParameters.Clear();
                    if (Convert.ToInt32(Session["PortLocation"]) == 0)
                        e.DataSource.Where = "it.[SupplyMethodID] = @pSupplyMethodID and (it.[FlowActionStatusID]=@pflowPendingApproval or it.[FlowActionStatusID]=@pflowPendingRejected or it.[FlowActionStatusID] = @pflowAmendPending)";
                    else
                    {
                        if (Session["PermitIssuerID"].ToString() != "")
                            e.DataSource.Where = "it.[SupplyMethodID] = @pSupplyMethodID and (it.[FlowActionStatusID]=@pflowPendingApproval or it.[FlowActionStatusID]=@pflowPendingRejected or it.[FlowActionStatusID] = @pflowAmendPending) and it.[Location]=@pLocation and it.[PermitIssuerID]=@pPermitIssuerID";
                        else
                            e.DataSource.Where = "it.[SupplyMethodID] = @pSupplyMethodID and (it.[FlowActionStatusID]=@pflowPendingApproval or it.[FlowActionStatusID]=@pflowPendingRejected or it.[FlowActionStatusID] = @pflowAmendPending) and it.[Location]=@pLocation ";
                    }
                    e.DataSource.WhereParameters.Add("pLocation", TypeCode.Int32, Session["PortLocation"].ToString());
                    e.DataSource.WhereParameters.Add("pPermitIssuerID", DbType.Guid, Session["PermitIssuerID"].ToString());
                    e.DataSource.WhereParameters.Add("pflowPendingApproval", DbType.Guid, flowPendingApproval);
                    e.DataSource.WhereParameters.Add("pflowPendingRejected", DbType.Guid, flowPendingRejected);
                    e.DataSource.WhereParameters.Add("pflowAmendPending", DbType.Guid, flowPendingAmend);
                    e.DataSource.WhereParameters.Add("pSupplyMethodID", DbType.Guid, supplyMethodID);

                    dbContext.Dispose();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex.InnerException, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void dsActiveApp_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {

            try
            {
                using (eSTS_StagEntities dbContext = new Database.eSTS_StagEntities())
                {
                    string flowPermitIssued = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPermitIssued.ToString();
                    string flowPendingCM = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPendingCM.ToString();
                    string flowPendingPayment = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPendingPayment.ToString();
                    string flowPendingApprovedDec = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPendingApproveDec.ToString();
                    string flowRejectDec = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowRejectDec.ToString();
                    string flowProcessInv = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowProcessInvoice.ToString();
                    string supplyMethodID = dbContext.SystemParams.FirstOrDefault<SystemParam>().SupplyMethodA.ToString();

                    e.DataSource.WhereParameters.Clear();
                    if (Convert.ToInt32(Session["PortLocation"]) == 0)
                        e.DataSource.Where = "it.[SupplyMethodID] = @pSupplyMethodID and (it.[FlowActionStatusID] = @pPermitIssued or it.[FlowActionStatusID] = @pPendingCM or it.[FlowActionStatusID] = @pPendingPayment or it.[FlowActionStatusID] = @pflowPendingApprovedDec or it.[FlowActionStatusID] = @pflowRejectDec or it.[FlowActionStatusID] = @pflowProcessInv)";
                    else
                    {
                        if (Session["PermitIssuerID"].ToString() != "")
                            e.DataSource.Where = "it.[SupplyMethodID]=@pSupplyMethodID and it.[Location]=@pLocation and it.[PermitIssuerID]=@pPermitIssuerID and (it.[FlowActionStatusID] = @pPermitIssued  or it.[FlowActionStatusID] = @pPendingCM or it.[FlowActionStatusID] = @pPendingPayment or it.[FlowActionStatusID] = @pflowPendingApprovedDec or it.[FlowActionStatusID] = @pflowRejectDec or it.[FlowActionStatusID] = @pflowProcessInv)";
                        else
                            e.DataSource.Where = "it.[SupplyMethodID]=@pSupplyMethodID and it.[Location]=@pLocation and (it.[FlowActionStatusID] = @pPermitIssued  or it.[FlowActionStatusID] = @pPendingCM or it.[FlowActionStatusID] = @pPendingPayment or it.[FlowActionStatusID] = @pflowPendingApprovedDec or it.[FlowActionStatusID] = @pflowRejectDec or it.[FlowActionStatusID] = @pflowProcessInv)";
                    }
                    e.DataSource.WhereParameters.Add("pLocation", TypeCode.Int32, Session["PortLocation"].ToString());
                    e.DataSource.WhereParameters.Add("pSupplyMethodID", DbType.Guid, supplyMethodID);
                    e.DataSource.WhereParameters.Add("pPermitIssuerID", DbType.Guid, Session["PermitIssuerID"].ToString());
                    e.DataSource.WhereParameters.Add("pPermitIssued", DbType.Guid, flowPermitIssued);
                    e.DataSource.WhereParameters.Add("pPendingPayment", DbType.Guid, flowPendingPayment);
                    e.DataSource.WhereParameters.Add("pPendingCM", DbType.Guid, flowPendingCM);
                    e.DataSource.WhereParameters.Add("pflowProcessInv", DbType.Guid, flowProcessInv);
                    e.DataSource.WhereParameters.Add("pflowPendingApprovedDec", DbType.Guid, flowPendingApprovedDec);
                    e.DataSource.WhereParameters.Add("pflowRejectDec", DbType.Guid, flowRejectDec);
                    dbContext.Dispose();
                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(ex.InnerException, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        //protected void dsPendingBDN_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        //{
        //    try
        //    {
        //        using (eSTS_StagEntities dbContext = new Database.eSTS_StagEntities())
        //        {
        //            string flowPendingBL = dbContext.SystemParams.FirstOrDefault<SystemParam>().FlowPendingBL.ToString();
        //            string supplyMethodID = dbContext.SystemParams.FirstOrDefault<SystemParam>().SupplyMethodA.ToString();

        //            e.DataSource.WhereParameters.Clear();
        //            if (Convert.ToInt32(Session["PortLocation"]) == 0)
        //                e.DataSource.Where = "it.[SupplyMethodID] = @pSupplyMethodID and it.[FlowActionStatusID]=@FlowActionStatusID";
        //            else
        //            {
        //                if (Session["PermitIssuerID"].ToString() != "")
        //                    e.DataSource.Where = "it.[SupplyMethodID]=@pSupplyMethodID and it.[Location]=@pLocation and it.[PermitIssuerID]=@pPermitIssuerID and it.[FlowActionStatusID] = @pPendingBL";
        //                else
        //                    e.DataSource.Where = "it.[SupplyMethodID]=@pSupplyMethodID and it.[Location]=@pLocation  and it.[FlowActionStatusID] = @pPendingBL";
        //            }
        //            e.DataSource.WhereParameters.Add("pLocation", TypeCode.Int32, Session["PortLocation"].ToString());
        //            e.DataSource.WhereParameters.Add("pPermitIssuerID", DbType.Guid, Session["PermitIssuerID"].ToString());
        //            e.DataSource.WhereParameters.Add("pPendingBL", DbType.Guid, flowPendingBL);
        //            e.DataSource.WhereParameters.Add("pSupplyMethodID", DbType.Guid, supplyMethodID);
        //            dbContext.Dispose();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteLog(ex.InnerException, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
        //    }
        //}
        #endregion

       

        #region Generate Permit & QRCode
        protected void GeneratePermit(string operationAppID, string refID,string permitFilePath)
        {
            try
            {
                ReportDocument oRpt = new ReportDocument();
                string dbServer = WebConfigurationManager.AppSettings["DBServer"];
                string dbCatalog = WebConfigurationManager.AppSettings["DBCatalog"];
                string dbUser = WebConfigurationManager.AppSettings["DBUser"];
                string dbPass = WebConfigurationManager.AppSettings["DBPass"];


                oRpt.Load(Server.MapPath("~/Operation/rptNotisPermi.rpt"));// = new eBunkering.Operation.PetrolPermit();
                oRpt.SetDatabaseLogon(dbUser, dbPass, dbServer, dbCatalog);

                //oRpt = new eBunkering.Operation.PetrolPermit();
                //oRpt.SetDataSource(objReport.ds.Tables["company"]);
                DALOperation objMain = new DALOperation();
                objMain.Get_OperationAppList(operationAppID);

                oRpt.SetDataSource(objMain.ds.Tables["v_permit"]);

                string fileName =   refID + "_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") ;

                string permitFullPath =Server.MapPath(permitFilePath);
                //string contentType = "application/pdf";
               

                CrystalDecisions.Shared.DiskFileDestinationOptions dfo = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                dfo.DiskFileName = permitFullPath;
                oRpt.ExportOptions.DestinationOptions = dfo;
                oRpt.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                oRpt.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                oRpt.Export();
                oRpt.Close();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void GenerateLampiran(string operationAppID, string refID, string permitFilePath)
        {
            try
            {
                ReportDocument oRpt = new ReportDocument();
                string dbServer = WebConfigurationManager.AppSettings["DBServer"];
                string dbCatalog = WebConfigurationManager.AppSettings["DBCatalog"];
                string dbUser = WebConfigurationManager.AppSettings["DBUser"];
                string dbPass = WebConfigurationManager.AppSettings["DBPass"];


                oRpt.Load(Server.MapPath("~/Operation/rptLampiran1.rpt"));// = new eBunkering.Operation.PetrolPermit();
                oRpt.SetDatabaseLogon(dbUser, dbPass, dbServer, dbCatalog);

                //oRpt = new eBunkering.Operation.PetrolPermit();
                //oRpt.SetDataSource(objReport.ds.Tables["company"]);
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
                //Response.ClearContent();
                //Response.ClearHeaders();
                //Response.ContentType = contentType;
                //Response.WriteFile(permitFullPath);
              
                //Response.Close();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        #endregion

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                //btnApprove.Enabled = false;
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {

                    List<object> fieldValues = grid.GetSelectedFieldValues(new string[] { "OperationAppID","CompanyName" });
                    foreach (object[] item in fieldValues)
                    {
                        Guid operationAppID = Guid.Parse(item[0].ToString());
                        Guid accessGroup = new Guid(Session["UserGroup"].ToString());
                        DALOperation objOperationApp = new DALOperation();

                        if (!objOperationApp.SubmitApproval(Common.FAction.Approve, operationAppID, Session["UserID"].ToString(), accessGroup, ""))
                        {
                            lblErrMsg.Text = "Failed to Approve.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                        }
                        else
                        {
                            OperationApp app = dbContext.OperationApps.Find(operationAppID);

                            string folderDirectory = "";
                            string fileName = "";
                            string PermitQRFilePath = "";

                            //Notis Permit
                            folderDirectory = Server.MapPath("Upload/" + app.CompID + "/" + app.OperationAppID.ToString());
                            fileName = "NotisPermit_" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            PermitQRFilePath = "~/Operation/Upload/" + app.CompID + "/" + app.OperationAppID.ToString() + "/" + "qrcode_" + fileName + ".jpg";

                            app.NPDocLink = objOperationApp.GenerateQRCode(app.OperationAppID.ToString(), app.CompID, folderDirectory, fileName, Server.MapPath(PermitQRFilePath)); //GeneratePermit(operationAppID.ToString(),item.CompID, item.RefID,ref QRPhysicalPath);
                            app.NPQRCode = Server.MapPath(PermitQRFilePath);
                            GeneratePermit(app.OperationAppID.ToString(), app.CompID, app.NPDocLink);

                            //Lampiran 1
                            folderDirectory = Server.MapPath("Upload/" + app.CompID + "/" + app.OperationAppID.ToString());
                            fileName = "Lampiran1_" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            PermitQRFilePath = "~/Operation/Upload/" + app.CompID + "/" + app.OperationAppID.ToString() + "/" + "qrcode_" + fileName + ".jpg";

                            app.Lampiran1DocLink = objOperationApp.GenerateQRCode(app.OperationAppID.ToString(), app.CompID, folderDirectory, fileName, Server.MapPath(PermitQRFilePath)); //GeneratePermit(operationAppID.ToString(),item.CompID, item.RefID,ref QRPhysicalPath);
                            app.Lampiran1QRCode = Server.MapPath(PermitQRFilePath);
                            GenerateLampiran(app.OperationAppID.ToString(), app.CompID, app.Lampiran1DocLink);

                            dbContext.SaveChanges();

                        }
                    }
                    dbContext.Dispose();
                }
               // btnApprove.Enabled = true;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>successAlert();</script>", false);
                gridActive.DataBind();
                grid.DataBind();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex.InnerException, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void pcReject_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    List<object> fieldValues = grid.GetSelectedFieldValues(new string[] { "OperationAppID", "CompanyName" });
                    foreach (object[] item in fieldValues)
                    {
                        Guid operationAppID = Guid.Parse(item[0].ToString());
                        var appInfo = dbContext.OperationApps.Find(operationAppID);

                        Guid accessGroup = new Guid(Session["UserGroup"].ToString());
                        DALOperation objOperationApp = new DALOperation();

                        if (objOperationApp.SubmitApproval(Common.FAction.Reject, operationAppID, Session["UserID"].ToString(), accessGroup, txtReject.Text))
                        {
                            Response.Redirect("~//Operation/ApprDashboardA.aspx", false);
                        }
                        else
                        {
                            lblErrMsg.Text = "Failed to Approve.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>successAlert();</script>", false);
                        }
                        dbContext.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
    }
}