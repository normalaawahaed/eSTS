using Apps.Common;
using eSTS.DAL;
using System;
using eSTS.Database;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DMSLatLongConverter;
using System.Web.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using System.Text;

namespace eSTS.Operation
{
    public partial class OpAppA : System.Web.UI.Page
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
                    Session["mode"] = Request.QueryString["mode"].ToString();
                    if (Session["mode"].ToString() == "p")
                    {
                        hfApplicationID.Value = Request.QueryString["sno"].ToString();
                        Session["isApprover"] = Request.QueryString["appr"].ToString();
                        Session["isAdmin"] = Request.QueryString["adm"].ToString();
                    }
                    else
                    {
                        //Check Session
                        if ((Session["UserID"] == null) && (Session["UserGroup"] == null))//&& (Session["UserLevel"] != null))
                        {
                            Response.Redirect("~//SignIn.aspx", true);
                        }
                        hfApplicationID.Value = Request.QueryString["sno"].ToString();
                        Session["isApprover"] = Request.QueryString["appr"].ToString();
                        Session["isAdmin"] = Request.QueryString["adm"].ToString();
                    }

                }
                else
                {
                    //Check Session
                    if ((Session["UserID"] == null) && (Session["UserGroup"] == null))//&& (Session["UserLevel"] != null))
                    {
                        Response.Redirect("~//SignIn.aspx", true);
                    }

                    Session["mode"] = "n";
                    Session["isApprover"] = "0";
                    Session["isAdmin"] = "0";
                }
                if (!Page.IsPostBack)
                {
                    //hfCurrentIndex.Value = "0";
                    //hfNewIndex.Value = "1";
                    BindComboBox();
                    LoadForm();
                    ControlForm();

                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void BindComboBox()
        {
            try
            {
                DALMMS objMMS = new DALMMS();
                DataSet dsShipFlag = objMMS.GetShipFlag("");

                cbFlag.DataSource = dsShipFlag;
                cbFlag.TextField = "ShipFlag";
                cbFlag.ValueField = "FlagCode";
                cbFlag.DataBind();

            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        #region Old
        //private void ControlFormOld()
        //{
        //    //if (Session["isApprover"].ToString() == "0" && Session["isAdmin"].ToString() == "0") //Applicant
        //    //{
        //    if (Session["mode"].ToString() == "n")
        //    {
        //        lblEmailMsg.Visible = true;
        //        cbFSU.Enabled = false;
        //        btnEditFSU.Enabled = false;
        //        btnRefreshFSU.Enabled = false;
        //        btnAddVesselDoc.Enabled = false;
        //        btnRefVesselDoc.Enabled = false;
        //        btnSODocRefresh.Enabled = false;
        //        btnSODocAdd.Enabled = false;
        //        divBONewApp.Visible = true;
        //        divBOViewApp.Visible = false;
        //        divApply.Visible = true;
        //        divApprover.Visible = false;
        //    }

        //    if (hfApplicationID.Value != null && hfApplicationID.Value != "")
        //    {
        //        divBONewApp.Visible = false;
        //        divBOViewApp.Visible = true;

        //        using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
        //        {
        //            if (Session["mode"].ToString() == "v" || Session["mode"].ToString() == "p")
        //            {
        //                lblEmailMsg.Visible = false;
        //                //Agent Details
        //                txtAgentCode.Enabled = false;
        //                txtICNumber.Enabled = false;
        //                txtDesignation.Enabled = false;
        //                cbSTSO.Enabled = false;
        //                txtContactPerson.Enabled = false;
        //                txtAgentEmail.Enabled = false;

        //                //Delivery Location
        //                cbDeliveryLoc.Enabled = false;
        //                cbPermitIssuer.Enabled = false;
        //                //Vessel Supplier
        //                btnPopupSearch.Enabled = false;
        //                txtIMONo.Enabled = false;
        //                txtVesselName.Enabled = false;
        //                txtPortReg.Enabled = false;
        //                cbFlag.Enabled = false;
        //                txtGRT.Enabled = false;
        //                txtNRT.Enabled = false;
        //                txtLOA.Enabled = false;
        //                txtMMSINo.Enabled = false;
        //                txtCallSign.Enabled = false;

        //                cbNextPort.Enabled = false;
        //                cbLastPort.Enabled = false;

        //                //Vessel FSU
        //                cbFSU.Enabled = false;
        //                txtFSUCallSign.Enabled = false;
        //                btnAddVesselDoc.Visible = false;
        //                btnRefVesselDoc.Visible = false;
        //                btnSODocRefresh.Visible = false;
        //                btnSODocAdd.Visible = false;

        //                btnEditFSU.Visible = false;
        //                btnRefreshFSU.Visible = false;
        //                txtSupName.Enabled = false;
        //                txtSupTelNo.Enabled = false;
        //                txtLatDegree.Enabled = false;
        //                txtLatMin.Enabled = false;
        //                txtLongDegree.Enabled = false;
        //                txtLongMin.Enabled = false;

        //                //Product Supply
        //                dtOperationDate.Enabled = false;
        //                timeOperation.Enabled = false;
        //                txtMT.Enabled = false;
        //                cbOilType.Enabled = false;
        //                cbUOM.Enabled = false;

        //                chkAck.Enabled = false;
        //                chkIntegrity.Enabled = false;
        //            }


        //            if (Session["isAdmin"].ToString() == "0") //View for Applicant & Approver
        //            {
        //                Guid operationAppID = new Guid(hfApplicationID.Value.ToString());
        //                SystemParam sysParam = dbContext.SystemParams.FirstOrDefault<SystemParam>();
        //                //OperationAppFlow flowPendingVerify = dbContext.OperationAppFlows.Where(w => w.OperationAppID == operationAppID && w.FlowActionStatusID == sysParam.FlowPendingApproval).FirstOrDefault<OperationAppFlow>();
        //                OperationAppFlow currentFlow = dbContext.OperationAppFlows.Where(w => w.OperationAppID == operationAppID && w.IsActive == true).FirstOrDefault<OperationAppFlow>();

        //                if (currentFlow == null) //Dont Have Any Flow
        //                {
        //                    btnEditFSU.Visible = true;
        //                    btnRefreshFSU.Visible = true;
        //                    btnAddVesselDoc.Visible = true;
        //                    btnRefVesselDoc.Visible = true;
        //                    btnSODocRefresh.Visible = true;
        //                    btnSODocAdd.Visible = true;
        //                    // divAttachForm.Visible = true;
        //                    divApply.Visible = true;

        //                    divApprover.Visible = false;
        //                }
        //                else if (currentFlow.FlowActionStatusID == sysParam.FlowDraft)
        //                {
        //                    btnEditFSU.Visible = true;
        //                    btnRefreshFSU.Visible = true;
        //                    btnAddVesselDoc.Visible = true;
        //                    btnRefVesselDoc.Visible = true;
        //                    btnSODocRefresh.Visible = true;
        //                    btnSODocAdd.Visible = true;
        //                    //  divAttachForm.Visible = true;
        //                    divApply.Visible = true;

        //                    divApprover.Visible = false;
        //                }
        //                else if (currentFlow.FlowActionStatusID == sysParam.FlowPendingApproval)
        //                {
        //                    if (currentFlow.ActionBy != null) //Verified -  cant edit
        //                    {
        //                        divAck.Visible = false;
        //                        //  divAttachForm.Visible = false;
        //                        btnEditFSU.Visible = false;
        //                        btnRefreshFSU.Visible = false;
        //                        btnAddVesselDoc.Visible = false;
        //                        btnRefVesselDoc.Visible = false;
        //                        btnSODocRefresh.Visible = false;
        //                        btnSODocAdd.Visible = false;

        //                        divApply.Visible = false;
        //                        divApprover.Visible = false;
        //                    }
        //                    else //not verified - can resubmit
        //                    {

        //                        if (Session["isApprover"].ToString() == "0")
        //                        {
        //                            btnEditFSU.Visible = true;
        //                            btnRefreshFSU.Visible = true;
        //                            btnAddVesselDoc.Visible = true;
        //                            btnRefVesselDoc.Visible = true;
        //                            btnSODocRefresh.Visible = true;
        //                            btnSODocAdd.Visible = true;
        //                            //divAttachForm.Visible = true;
        //                            divApply.Visible = true;
        //                            divApprover.Visible = false;
        //                        }
        //                        else //View for approver
        //                        {
        //                            lblEmailMsg.Visible = false;
        //                            divAck.Visible = false;
        //                            //divAttachForm.Visible = false;
        //                            btnEditFSU.Visible = false;
        //                            btnRefreshFSU.Visible = false;
        //                            btnAddVesselDoc.Visible = false;
        //                            btnRefVesselDoc.Visible = false;
        //                            btnSODocRefresh.Visible = false;
        //                            btnSODocAdd.Visible = false;
        //                            divApply.Visible = false;
        //                            divApprover.Visible = true;
        //                            divAmend.Visible = false;
        //                        }
        //                    }
        //                }
        //                else if (currentFlow.FlowActionStatusID == sysParam.FlowReject)
        //                {
        //                    if (Session["isApprover"].ToString() == "0")
        //                    {
        //                        btnEditFSU.Visible = true;
        //                        btnRefreshFSU.Visible = true;
        //                        btnAddVesselDoc.Visible = true;
        //                        btnRefVesselDoc.Visible = true;
        //                        btnSODocRefresh.Visible = true;
        //                        btnSODocAdd.Visible = true;

        //                        divApply.Visible = true;
        //                        // divAttachForm.Visible = true;
        //                    }
        //                    else
        //                    {
        //                        lblEmailMsg.Visible = false;
        //                        btnEditFSU.Visible = false;
        //                        btnRefreshFSU.Visible = false;
        //                        btnAddVesselDoc.Visible = false;
        //                        btnRefVesselDoc.Visible = false;
        //                        btnSODocRefresh.Visible = false;
        //                        btnSODocAdd.Visible = false;
        //                        // divAttachForm.Visible = false;
        //                        divApply.Visible = false;
        //                    }
        //                    divApprover.Visible = false;
        //                }
        //                else if (currentFlow.FlowActionStatusID==sysParam.FlowPendingCM || currentFlow.FlowActionStatusID == sysParam.FlowPendingBL)
        //                {
        //                    if (Session["mode"].ToString() == "a")
        //                    {
        //                        /* Added : Mala
        //                         * Ref : CR
        //                         * Reason/Purpose : Control view for Amendments Flow
        //                         *  Date : 25/01/2022
        //                        */
        //                        lblEmailMsg.Visible = false;
        //                        //Agent Details
        //                        txtAgentCode.Enabled = false;
        //                        txtICNumber.Enabled = false;
        //                        txtDesignation.Enabled = false;
        //                        cbSTSO.Enabled = false;
        //                        txtContactPerson.Enabled = false;
        //                        txtAgentEmail.Enabled = false;

        //                        //Delivery Location
        //                        cbDeliveryLoc.Enabled = false;
        //                        cbPermitIssuer.Enabled = false;
        //                        //Vessel Supplier
        //                        btnPopupSearch.Enabled = false;
        //                        txtIMONo.Enabled = false;
        //                        txtVesselName.Enabled = false;
        //                        txtPortReg.Enabled = false;
        //                        cbFlag.Enabled = false;
        //                        txtGRT.Enabled = false;
        //                        txtNRT.Enabled = false;
        //                        txtLOA.Enabled = false;
        //                        txtMMSINo.Enabled = false;
        //                        txtCallSign.Enabled = false;

        //                        cbNextPort.Enabled = false;
        //                        cbLastPort.Enabled = false;

        //                        //Vessel FSU
        //                        cbFSU.Enabled = false;
        //                        txtFSUCallSign.Enabled = false;
        //                        btnAddVesselDoc.Visible = false;
        //                        btnRefVesselDoc.Visible = false;
        //                        btnSODocRefresh.Visible = false;
        //                        btnSODocAdd.Visible = false;

        //                        btnEditFSU.Visible = false;
        //                        btnRefreshFSU.Visible = false;
        //                        txtSupName.Enabled = false;
        //                        txtSupTelNo.Enabled = false;
        //                        txtLatDegree.Enabled = false;
        //                        txtLatMin.Enabled = false;
        //                        txtLongDegree.Enabled = false;
        //                        txtLongMin.Enabled = false;


        //                        txtIMONo.Enabled = true;
        //                        txtPortReg.Enabled = true;
        //                        txtVesselName.Enabled = true;
        //                        cbFlag.Enabled = true;
        //                        txtGRT.Enabled = true;
        //                        txtNRT.Enabled = true;
        //                        txtLOA.Enabled = true;
        //                        txtMMSINo.Enabled = true;
        //                        txtCallSign.Enabled = true;
        //                        cbLastPort.Enabled = true;
        //                        cbNextPort.Enabled = true;
        //                        chkAck.Enabled = false;
        //                        chkIntegrity.Enabled = false;

        //                        //Product Supply
        //                        dtOperationDate.Enabled = false;
        //                        timeOperation.Enabled = false;
        //                        txtMT.Enabled = false;
        //                        cbOilType.Enabled = false;
        //                        cbUOM.Enabled = false;

        //                        divAmend.Visible = true;
        //                        divApply.Visible = false;
        //                        btnDraft.Visible = false;
        //                        divApprover.Visible = false;
        //                    }
        //                }
        //                else if (currentFlow.FlowActionStatusID == sysParam.FlowAmendPending || currentFlow.FlowActionStatusID == sysParam.FlowAmendApproved || currentFlow.FlowActionStatusID == sysParam.FlowAmendRejected)
        //                {
        //                    /* Added : Mala
        //                     * Ref : CR
        //                     * Reason/Purpose : Control view for Amendments Flow
        //                     *  Date : 25/01/2022
        //                    */
        //                    if (Session["isApprover"].ToString() == "0")
        //                    { //View for applicant

        //                        divAck.Visible = false;
        //                        btnEditFSU.Visible = false;
        //                        btnRefreshFSU.Visible = false;
        //                        btnAddVesselDoc.Visible = false;
        //                        btnRefVesselDoc.Visible = false;
        //                        btnSODocRefresh.Visible = false;
        //                        btnSODocAdd.Visible = false;
        //                        txtAmend.Enabled = false;

        //                        divAmend.Visible = true;
        //                        //divApply.Visible = false;
        //                        divApprover.Visible = false;
        //                    }
        //                    else
        //                    {  //View for approval
        //                        lblEmailMsg.Visible = false;
        //                        divAck.Visible = false;
        //                        btnEditFSU.Visible = false;
        //                        btnRefreshFSU.Visible = false;
        //                        btnAddVesselDoc.Visible = false;
        //                        btnRefVesselDoc.Visible = false;
        //                        btnSODocRefresh.Visible = false;
        //                        btnSODocAdd.Visible = false;
        //                        divApply.Visible = false;

        //                        if (currentFlow.FlowActionStatusID == sysParam.FlowAmendPending)
        //                        {
        //                            txtAmend.Enabled = false;
        //                            divAmend.Visible = true;
        //                            divApprover.Visible = true;
        //                        }
        //                        else if (currentFlow.FlowActionStatusID == sysParam.FlowAmendApproved || currentFlow.FlowActionStatusID == sysParam.FlowAmendRejected)
        //                        {
        //                            txtAmend.Enabled = false;
        //                            divAmend.Visible = true;
        //                            divApprover.Visible = false;
        //                        }

        //                    }
        //                }
        //                else
        //                {
        //                    lblEmailMsg.Visible = false;
        //                    divAck.Visible = false;
        //                    // divAttachForm.Visible = false;
        //                    btnEditFSU.Visible = false;
        //                    btnRefreshFSU.Visible = false;
        //                    btnAddVesselDoc.Visible = false;
        //                    btnRefVesselDoc.Visible = false;
        //                    btnSODocRefresh.Visible = false;
        //                    btnSODocAdd.Visible = false;
        //                    divApply.Visible = false;
        //                    divApprover.Visible = false;
        //                }
        //            }
        //            else
        //            {
        //                lblEmailMsg.Visible = false;
        //                divApply.Visible = false;
        //                divApprover.Visible = false;
        //            }
        //        }
        //    }
        //}
        #endregion

        private void ControlForm()
        {

            if (hfApplicationID.Value == null || hfApplicationID.Value == "")
            { //New Application
                divBONewApp.Visible = true;
                divBOViewApp.Visible = false;

                EnableControl(true);
                VisibleControl(true);

                divFileAppoint.Visible = true;
                divAttachForm.Visible = true;
                divAck.Visible = true;
                divApply.Visible = true;
                divAmend.Visible = false;
                divApprover.Visible = false;
                divCancel.Visible = false;
            }
            else
            {
                divBONewApp.Visible = false;
                divBOViewApp.Visible = true;

                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    if (Session["mode"].ToString() == "v" || Session["mode"].ToString() == "p" || Session["mode"].ToString() == "c")
                    {
                        lblEmailMsg.Visible = false;
                        EnableControl(false);
                        if (Session["isApprover"].ToString() == "0" && Session["isAdmin"].ToString() == "0") //View for Applicant for Mode View
                        {
                            Guid operationAppID = new Guid(hfApplicationID.Value.ToString());
                            SystemParam sysParam = dbContext.SystemParams.FirstOrDefault<SystemParam>();
                            //OperationAppFlow flowPendingVerify = dbContext.OperationAppFlows.Where(w => w.OperationAppID == operationAppID && w.FlowActionStatusID == sysParam.FlowPendingApproval).FirstOrDefault<OperationAppFlow>();
                            OperationAppFlow currentFlow = dbContext.OperationAppFlows.Where(w => w.OperationAppID == operationAppID && w.IsActive == true).FirstOrDefault<OperationAppFlow>();


                            if (currentFlow.FlowActionStatusID == sysParam.FlowReject)
                            {
                                EnableControl(false);
                                VisibleControl(true);

                                divFileAppoint.Visible = true;
                                divAttachForm.Visible = true;
                                divAck.Visible = true;
                                divApply.Visible = true;
                                divAmend.Visible = false;
                                divApprover.Visible = false;
                                divCancel.Visible = false;
                            }
                            else
                            {
                                EnableControl(false);
                                VisibleControl(false);

                                divFileAppoint.Visible = true;
                                divAttachForm.Visible = false;
                                divAck.Visible = true;
                                divApply.Visible = false;
                                divAmend.Visible = false;
                                divApprover.Visible = false;
                                divCancel.Visible = false;
                            }

                        }
                        else if (Session["isApprover"].ToString() == "1") //View for Approver for Mode View
                        {
                            EnableControl(false);
                            VisibleControl(false);

                            Guid operationAppID = new Guid(hfApplicationID.Value.ToString());
                            SystemParam sysParam = dbContext.SystemParams.FirstOrDefault<SystemParam>();
                            //OperationAppFlow flowPendingVerify = dbContext.OperationAppFlows.Where(w => w.OperationAppID == operationAppID && w.FlowActionStatusID == sysParam.FlowPendingApproval).FirstOrDefault<OperationAppFlow>();
                            OperationAppFlow currentFlow = dbContext.OperationAppFlows.Where(w => w.OperationAppID == operationAppID && w.IsActive == true).FirstOrDefault<OperationAppFlow>();
                            if (Session["mode"].ToString() == "c")
                            {
                                divFileAppoint.Visible = false;
                                divAttachForm.Visible = false;
                                divAck.Visible = false;
                                divApply.Visible = false;
                                divAmend.Visible = false;
                                divApprover.Visible = false;
                                divCancel.Visible = true;
                            }
                            else if (currentFlow.FlowActionStatusID == sysParam.FlowPendingApproval)
                            {
                                divFileAppoint.Visible = true;
                                divAttachForm.Visible = false;
                                divAck.Visible = true;
                                divApply.Visible = false;
                                divAmend.Visible = false;

                                divApprover.Visible = true;
                                divCancel.Visible = false;

                            }
                            else if (currentFlow.FlowActionStatusID == sysParam.FlowReject || currentFlow.FlowActionStatusID == sysParam.FlowPendingCM || currentFlow.FlowActionStatusID == sysParam.FlowPendingBL || currentFlow.FlowActionStatusID == sysParam.FlowAmendApproved || currentFlow.FlowActionStatusID == sysParam.FlowAmendRejected)
                            {
                                divFileAppoint.Visible = true;
                                divAck.Visible = true;

                                divAttachForm.Visible = false;
                                divApply.Visible = false;
                                divAmend.Visible = false;
                                divApprover.Visible = false;
                                divCancel.Visible = false;
                            }
                            else if (currentFlow.FlowActionStatusID == sysParam.FlowAmendPending)
                            {
                                /* Added : Mala
                                 * Ref : CR
                                 * Reason/Purpose : Control view for Amendments Flow
                                 *  Date : 25/01/2022
                                */
                                divFileAppoint.Visible = true;
                                divAck.Visible = true;

                                divAttachForm.Visible = false;
                                divApply.Visible = false;
                                divAmend.Visible = false;
                                divApprover.Visible = true;
                                divCancel.Visible = false;
                            }
                            else
                            {
                                divFileAppoint.Visible = true;
                                divAttachForm.Visible = false;
                                divAck.Visible = true;
                                divApply.Visible = false;
                                divAmend.Visible = false;
                                divApprover.Visible = false;
                                divCancel.Visible = false;
                            }

                        }
                        else //View FOr Admin
                        {
                            divFileAppoint.Visible = true;
                            divAttachForm.Visible = false;
                            divAck.Visible = true;
                            divApply.Visible = false;
                            divAmend.Visible = false;
                            divApprover.Visible = false;
                            divCancel.Visible = false;
                        }

                    }
                    else if (Session["mode"].ToString() == "e") //For Applicant
                    {
                        EnableControl(true);
                        VisibleControl(true);

                        divFileAppoint.Visible = true;
                        divAttachForm.Visible = true;
                        divAck.Visible = true;
                        divApply.Visible = true;
                        divAmend.Visible = false;
                        divApprover.Visible = false;
                        divCancel.Visible = false;
                    }
                    else if (Session["mode"].ToString() == "a")
                    {
                        /* CR : EB/CR/2022/01/001
                       Added by : Normala
                       Date : 25/01/2022
                       Reason/Purpose : Control view for Amendments Flow 
                       */
                        
                        EnableControl(false);
                        VisibleControl(false);
                        chkAck.Enabled = true;
                        chkIntegrity.Enabled = true;

                        txtIMONo.Enabled = true;
                        txtPortReg.Enabled = true;
                        txtVesselName.Enabled = true;
                        cbFlag.Enabled = true;
                        txtGRT.Enabled = true;
                        txtNRT.Enabled = true;
                        txtLOA.Enabled = true;
                        txtMMSINo.Enabled = true;
                        txtCallSign.Enabled = true;
                        cbLastPort.Enabled = true;
                        cbNextPort.Enabled = true;

                        divFileAppoint.Visible = true;
                        divAttachForm.Visible = true;
                        divAck.Visible = true;
                        divApply.Visible = false;
                        divAmend.Visible = true;

                        divApprover.Visible = false;
                        divCancel.Visible = false;
                    }
                }
            }
        }

        private void EnableControl(bool enable)
        {
            //Operator
            lblEmailMsg.Visible = enable;
            //Agent Details
            txtAgentCode.Enabled = enable;
            txtICNumber.Enabled = enable;
            txtDesignation.Enabled = enable;
            cbSTSO.Enabled = enable;
            txtContactPerson.Enabled = enable;
            txtAgentEmail.Enabled = enable;

            //FSU
            cbFSU.Enabled = enable;
            txtFSUCallSign.Enabled = enable;
            //btnAddVesselDoc.Visible = enable;
            //btnRefVesselDoc.Visible = enable;
            //btnSODocRefresh.Visible = enable;
            //btnSODocAdd.Visible = enable;

            btnEditFSU.Visible = enable;
            btnRefreshFSU.Visible = enable;
            txtSupName.Enabled = enable;
            txtSupTelNo.Enabled = enable;
            txtLatDegree.Enabled = enable;
            txtLatMin.Enabled = enable;
            txtLongDegree.Enabled = enable;
            txtLongMin.Enabled = enable;

            ////operator lic doc
            //btnSODocAdd.Enabled = enable;
            //btnSODocRefresh.Enabled = enable;

            //FSU Lic Doc
            btnEditFSU.Enabled = enable;
            btnRefreshFSU.Enabled = enable;
            //btnAddVesselDoc.Enabled = enable;
            //btnRefVesselDoc.Enabled = enable;

            //Operation Location
            cbDeliveryLoc.Enabled = enable;
            cbPermitIssuer.Enabled = enable;

            //Vessel Supplier
            txtIMONo.Enabled = enable;
            txtVesselName.Enabled = enable;
            txtPortReg.Enabled = enable;
            cbFlag.Enabled = enable;
            txtGRT.Enabled = enable;
            txtNRT.Enabled = enable;
            txtLOA.Enabled = enable;
            txtMMSINo.Enabled = enable;
            txtCallSign.Enabled = enable;

            cbNextPort.Enabled = enable;
            cbLastPort.Enabled = enable;

            //Product Supply
            dtOperationDate.Enabled = enable;
            timeOperation.Enabled = enable;
            txtMT.Enabled = enable;
            cbOilType.Enabled = enable;
            cbUOM.Enabled = enable;

            //Operation Supp DOc


            //Acknowledgement 
            chkAck.Enabled = enable;
            chkIntegrity.Enabled = enable;

        }

        private void VisibleControl(bool visible)
        {
            //Operator
            lblEmailMsg.Visible = visible;

            //FSU
            btnEditFSU.Visible = visible;
            //btnRefVesselDoc.Visible = visible;

            ////operator lic doc
            //btnSODocAdd.Visible = visible;
            //btnSODocRefresh.Visible = visible;

            ////FSU Lic Doc
            //btnAddVesselDoc.Visible = visible;
            //btnRefVesselDoc.Visible = visible;

        }

        private void LoadForm()
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    string compID = "";
                    string userID = "";

                    if (Session["mode"].ToString() == "n")
                    {
                        compID = Session["CompID"].ToString();
                        userID = Session["UserID"].ToString();

                    }
                    else if (Session["mode"].ToString() == "e" || Session["mode"].ToString() == "v" || Session["mode"].ToString() == "p" || Session["mode"].ToString() == "a" || Session["mode"].ToString() == "c")
                    {
                        /* CR : EB/CR/2022/01/001
                       Added by : Normala
                       Date : 25/01/2022
                       Reason/Purpose : Allow for Amendments 
                       */
                        Guid ApplicationID = new Guid(hfApplicationID.Value.ToString());
                        OperationApp item = dbContext.OperationApps.Where(w => w.OperationAppID == ApplicationID).FirstOrDefault<OperationApp>();

                        compID = item.CompID;
                        userID = item.CreatedBy;
                        Session["CompID"] = item.CompID;

                        cbSTSO.Value = item.SOCompID;

                        v_LicCompany lic = dbContext.v_LicCompany.Where(w => w.LicCompanyID == item.SOLicID).FirstOrDefault<v_LicCompany>();
                        LicCompanyVessel licVessel = dbContext.LicCompanyVessels.Where(w => w.LicCompanyID == lic.LicCompanyID).FirstOrDefault<LicCompanyVessel>();

                        txtBOName.Text = lic.CompanyName;
                        hfLicExpDate.Value = Convert.ToDateTime(lic.DtLicExp).ToShortDateString();
                        lblLicValid.Text = Convert.ToDateTime(lic.DtLicExp).ToString("dd/MM/yyyy");
                        hfLicLocation.Value = lic.Location.ToString();
                        hfLicCompID.Value = item.SOLicID.ToString();
                        //lblPortLocation.Text = dsLic.Tables[0].Rows[0]["Port"].ToString();
                        lblLicValid.Text = Convert.ToDateTime(lic.DtLicExp).ToString("dd/MM/yyyy");
                        hfCompID.Value = lic.CompID.ToString();
                        hfLicVesselID.Value = licVessel.LicCompanyVesselID.ToString();
                        hfLicExpDate.Value = Convert.ToDateTime(lic.DtLicExp).ToString();
                        hfLicLocation.Value = lic.Location.ToString();
                        txtBOName.Text = lic.CompanyName;

                        //Supply Method
                        //cbSupplyMethod.Value = item.SupplyMethodID;
                        cbDeliveryLoc.DataBind();
                        cbPermitIssuer.DataBind();
                        cbDeliveryLoc.Value = item.DeliveryLocID;
                        cbPermitIssuer.Value = item.PermitIssuerID;

                        //Vessel Supplier
                       
                        txtVesselName.Text = item.VSName;
                        txtPortReg.Text = item.VSPortReg;
                        txtIMONo.Text = item.VSIMONo;
                        cbFlag.Value = item.VSFlag;
                        txtNRT.Text = item.VSNRT.ToString();
                        txtGRT.Text = item.VSGRT.ToString();
                        txtLOA.Text = item.VSLOA.ToString();
                        txtMMSINo.Text = item.VSMMSINo;
                        txtCallSign.Text = item.VSCallSign;
                        cbLastPort.Value = item.VSLastPort;
                        cbNextPort.Value = item.VSNextPort;

                        //FSU
                        cbFSU.Value = item.VRID;
                        txtFSUCallSign.Text = item.VRCallSign;
                        txtLatDegree.Text = item.VRLatDegree.ToString();
                        txtLatMin.Text = item.VRLatMin.ToString();
                        txtLongDegree.Text = item.VRLongDegree.ToString();
                        txtLongMin.Text = item.VRLongMin.ToString();
                        txtSupName.Text = item.VRSupritendentName.ToString();
                        txtSupTelNo.Text = item.VRSupritendentTelNo.ToString();

                        //Product supply
                        dtOperationDate.Value = item.EstOperationDateTime;
                        if (item.EstOperationTime != null)
                            timeOperation.Value = item.EstOperationTime.Value;

                        this.txtMT.Text = item.EstOilMT.ToString();

                        if (item.OilTypeID.ToString() != "")
                            cbOilType.Value = item.OilTypeID;

                        if (item.UOMID.ToString() != "")
                            cbUOM.Value = item.UOMID;

                        if(item.IsAcknowledge==true)
                        {
                            chkAck.Checked = true;
                        }
                        if (item.IsIntegrity == true)
                        {
                            chkIntegrity.Checked = true;
                        }
                        loadTimeline(item.OperationAppID);

                        v_AppointAgentNew appoint = dbContext.v_AppointAgentNew.Where(w => w.SACompID == item.CompID && w.SOCompID==item.SOCompID).FirstOrDefault<v_AppointAgentNew>();
                        var sb = new System.Text.StringBuilder();
                        //Upload File
                        if (appoint.AppointAttachLink != "" && appoint.AppointAttachLink != null)
                        {
                            sb.AppendLine("<a href='" + appoint.AppointAttachLink + "' target='_blank'><span class='corner'></span><div class='icon'><i class='fa fa-file text-info'></i>" +
                                            "</div><div class='file-name text-center'> Download Letter Of Appoinment </div></a>");

                            lilFile.Text = sb.ToString();

                        }
                    }

                    v_UsersSASO user = dbContext.v_UsersSASO.Where(w => w.ROCNO == compID && w.UserID == userID).FirstOrDefault<v_UsersSASO>();

                    lblCompanyName.Text = user.CompanyName;
                    lblTelNo.Text = user.TelNo;
                    lblFaxNo.Text = user.FaxNo;
                    txtAgentCode.Text = user.AgentCode;
                    
                    txtContactPerson.Text = user.ContactPerson;
                    txtAgentEmail.Text = user.EmailAdd;

                    if (user.ICNo != null)
                        txtICNumber.Text = user.ICNo;
                    if (user.Designation != null)
                        txtDesignation.Text = user.Designation;

                    dbContext.Dispose();
                    
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void loadTimeline(Guid ApplicationID)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    var sb = new StringBuilder();
                    SystemParam sysParam = dbContext.SystemParams.FirstOrDefault<SystemParam>();

                    List<v_OperationAppFlow> lFlow = dbContext.v_OperationAppFlow.Where(w => w.OperationAppID == ApplicationID).OrderBy(w => new { w.CreatedDate, w.IsActive }).ToList<v_OperationAppFlow>();//(w => w.CreatedDate,w.isac).ToList<v_OperationAppFlow>();
                    sb.Append("<div id='vertical-timeline' class='vertical-container light-timeline no-margins'>");
                    int index = 1;
                    foreach (v_OperationAppFlow flow in lFlow)
                    {
                        if (flow.IsHideFlow == false || (flow.IsHideFlow == true && flow.ActionDate == null && (flow.FlowActionStatusID != sysParam.FlowPendingVerifyDec || flow.FlowActionStatusID==sysParam.FlowRejectDec)))
                        {
                            if (flow.ActionBy == null  && index < lFlow.Count)
                            {
                            }
                            else
                            { 
                                sb.Append("<div class='vertical-timeline-block'>");
                                sb.Append("<div class='vertical-timeline-icon navy-bg'>");
                                sb.Append("<i class='fa fa-check-square-o'></i>");
                                sb.Append("</div>");
                                sb.Append("<div class='vertical-timeline-content'>");
                                sb.Append("<div class='col-lg-8'>");
                                sb.Append("<dl class='row mb-0'>");
                                sb.Append("<div class='col-sm-4 text-sm-right'><dt>Action Status:</dt> </div>");
                                sb.Append("<div class='col-sm-8 text-sm-left'><dd class='mb-1'><span class='label label-" + flow.LabelColor + "'>" + flow.ActionStatus + "</span></dd></div>");
                                sb.Append("</dl>");
                                if (flow.Remark != "" && flow.Remark != null)
                                {
                                    sb.Append("<dl class='row mb-0'>");
                                    if (flow.FlowActionStatusID == sysParam.FlowReject || flow.FlowActionStatusID == sysParam.FlowAmendRejected || flow.FlowActionStatusID==sysParam.FlowRejectDec)
                                        sb.Append("<div class='col-sm-4 text-sm-right'><dt>Reject Remark:</dt> </div>");
                                    else if (flow.FlowActionStatusID == sysParam.FlowAmendSubmit)
                                        sb.Append("<div class='col-sm-4 text-sm-right'><dt>Amend Remark:</dt> </div>");
                                    else if (flow.FlowActionStatusID == sysParam.FlowCancel)
                                        sb.Append("<div class='col-sm-4 text-sm-right'><dt>Cancel Remark:</dt> </div>");
                                    else if (flow.FlowActionStatusID == sysParam.FlowComplete)
                                        sb.Append("<div class='col-sm-4 text-sm-right'><dt>Invoice No:</dt> </div>");
                                    sb.Append("<div class='col-sm-8 text-sm-left'><dd class='mb-1'>" + flow.Remark + "</dd></div>");
                                    sb.Append("</dl>");
                                }
                                sb.Append("<dl class='row mb-0'>");
                                sb.Append("<div class='col-sm-4 text-sm-right'><dt>Action By:</dt> </div>");
                                sb.Append("<div class='col-sm-8 text-sm-left'><dd class='mb-1'>" + flow.FullName + "</dd></div>");
                                sb.Append("</dl>");
                                sb.Append("<dl class='row mb-0'>");
                                sb.Append("<div class='col-sm-4 text-sm-right'><dt>Action Date:</dt> </div>");
                                if (flow.ActionDate == null)
                                    sb.Append("<div class='col-sm-8 text-sm-left'><dd class='mb-1'></dd></div>");
                                else
                                    sb.Append("<div class='col-sm-8 text-sm-left'><dd class='mb-1'>" + Convert.ToDateTime(flow.ActionDate).ToString("dd/MM/yyyy HH:mm") + "</dd></div>");
                                sb.Append("</dl>");
                                sb.Append("</div>");
                                sb.Append("</div></div>");
                            }
                        }
                        index++;
                    }
                    sb.Append("</div>");
                    lilTimeline.Text = sb.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        #region STSOperator
        protected void cbSTSO_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                 using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                   
                    cbFSU.SelectedIndex = -1;
                    cbFSU.DataBind();
                    hfCompID.Value = cbSTSO.Value.ToString();
                    if (Session["mode"].ToString() == "n")
                    {
                        cbFSU.Enabled = true;
                    }
                    BindComboBox();
                    cbFSU.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void dsSTSOperator_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {
            try
            {
                e.DataSource.WhereParameters.Clear();
                e.DataSource.Where = "it.[SACompID]=@pCompID";

                e.DataSource.WhereParameters.Add("pCompID", TypeCode.String, Session["CompID"].ToString());
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void dsSOdoc_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {
            try
            {
                if (hfLicCompID.Value.ToString() != "")
                    e.DataSource.WhereParameters["pLicCompanyID"].DefaultValue = hfLicCompID.Value.ToString();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        #endregion

        #region Delivery Location
        protected void dsDeliveryLoc_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {
            try
            {
                if (Session["mode"].ToString() != "e" || Session["mode"].ToString() != "n")
                {

                    e.DataSource.WhereParameters["pLocation"].DefaultValue = hfLicLocation.Value.ToString();
                    cbDeliveryLoc.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void cbDeliveryLoc_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                dsDeliveryLoc.DataBind();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void dsPermitIssuer_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {
            try
            {
                if (cbDeliveryLoc.Value != null)
                {
                    string delLoc = cbDeliveryLoc.Value.ToString();

                    e.DataSource.WhereParameters["pMSDeliveryLocID"].DefaultValue = delLoc.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        #endregion

        #region FSU
        protected void cbFSU_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    Guid LicCompanyVesselID = new Guid(cbFSU.Value.ToString());
                    v_OpLicCompanyVessel lic = dbContext.v_OpLicCompanyVessel.Where(w => w.LicCompanyVesselID == LicCompanyVesselID).FirstOrDefault<v_OpLicCompanyVessel>();

                    hfLicExpDate.Value = Convert.ToDateTime(lic.DtLicExp).ToShortDateString();
                    lblLicValid.Text = Convert.ToDateTime(lic.DtLicExp).ToString("dd/MM/yyyy");
                    hfLicLocation.Value = lic.Location.ToString();
                    hfLicVesselID.Value = lic.LicCompanyVesselID.ToString();
                    LicCompanyVessel fsu = dbContext.LicCompanyVessels.Find(cbFSU.Value);
                    txtSupName.Text = fsu.SupritendentName;
                    txtSupTelNo.Text = fsu.SupritendentTelNo;
                    txtFSUCallSign.Text = fsu.CallSign;
                    txtLatDegree.Text = fsu.LatDegree.ToString();
                    txtLatMin.Text = fsu.LatMin.ToString();
                    txtLongDegree.Text = fsu.LongDegree.ToString();
                    txtLongMin.Text = fsu.LongMin.ToString();

                    hfLicCompID.Value = lic.LicCompanyID.ToString();

                    dbContext.Dispose();
                }
                btnEditFSU.Enabled = true;
                btnRefreshFSU.Enabled = true;
                //btnAddVesselDoc.Enabled = true;
                //btnRefVesselDoc.Enabled = true;
                //btnSODocRefresh.Enabled = true;
                //btnSODocAdd.Enabled = true;
                //gridSODoc.DataBind();
                //gridDocVesselFSU.DataBind();
                cbFSU.Focus();
                cbDeliveryLoc.DataBind();
                cbPermitIssuer.DataBind();
                cbDeliveryLoc.SelectedIndex = 0;
                cbPermitIssuer.SelectedIndex = 0;
                cbFSU.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void dsFSU_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {
            try
            {
                //if (Session["mode"].ToString() != "e" || Session["mode"].ToString() != "n")
                //{
                //    e.DataSource.WhereParameters["CompID"].DefaultValue = hfCompID.Value.ToString();
                //}
                //else
                //{
                //    if (cbSTSO.Value != null)
                //    {
                //        e.DataSource.WhereParameters["CompID"].DefaultValue = cbSTSO.Value.ToString();
                //    }
                //}
                if (cbSTSO.Value != null)
                {
                    e.DataSource.Where = "it.[CompID] = @pCompID and it.[DtLicExp]>@pDate";
                    e.DataSource.WhereParameters.Clear();

                    e.DataSource.WhereParameters.Add("pCompID", DbType.String, cbSTSO.Value.ToString());
                     e.DataSource.WhereParameters.Add("pDate", DbType.DateTime,  DateTime.Today.AddDays(-1).ToString());
                }

            }
            catch (Exception ex)
            { 
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        protected void btnRefreshFSU_Click(object sender, EventArgs e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    Guid LicCompanyVesselID = new Guid(cbFSU.Value.ToString());
                    v_OpLicCompanyVessel lic = dbContext.v_OpLicCompanyVessel.Where(w => w.LicCompanyVesselID == LicCompanyVesselID).FirstOrDefault<v_OpLicCompanyVessel>();

                    hfLicExpDate.Value = Convert.ToDateTime(lic.DtLicExp).ToShortDateString();
                    lblLicValid.Text = Convert.ToDateTime(lic.DtLicExp).ToString("dd/MM/yyyy");
                    hfLicLocation.Value = lic.Location.ToString();
                    hfLicVesselID.Value = lic.LicCompanyVesselID.ToString();
                    txtFSUCallSign.Text = lic.CallSign;
                    txtLatDegree.Text = lic.LatDegree.ToString();
                    txtLatMin.Text = lic.LatMin.ToString();
                    txtLongDegree.Text = lic.LongDegree.ToString();
                    txtLongMin.Text = lic.LongMin.ToString();

                    dbContext.Dispose();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        protected void dsDocVesselFSU_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {
            try
            {
                if (cbFSU.Value != null)
                {
                    e.DataSource.WhereParameters["pLicCompanyVesselID"].DefaultValue = cbFSU.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void cbFSU_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                cbFSU.DataBind();
                dsFSU.DataBind();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        #endregion

        #region Attachment 
        protected void dsAttach_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {
            try
            {
                if (hfApplicationID.Value != "")
                    e.DataSource.WhereParameters["pOperationAppID"].DefaultValue = hfApplicationID.Value.ToString();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void gridAttach_CommandButtonInitialize(object sender, DevExpress.Web.Bootstrap.BootstrapGridViewCommandButtonEventArgs e)
        {
            {
                if (Session["mode"].ToString() == "v")
                {
                    if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete)
                        e.Visible = false;
                }
            }
        }

        protected void gridAttach_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    var attachID = dbContext.OperationAppAttaches.Find(new Guid(e.Keys[0].ToString()));

                    dbContext.OperationAppAttaches.Remove(attachID);

                    dbContext.SaveChanges();

                    gridAttach.DataBind();
                    gridAttach.CancelEdit();
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void btnSaveAttachDoc_Click(object sender, EventArgs e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    if (hfApplicationID.Value.ToString() == "")
                    {
                        OperationApp app = new OperationApp();

                        app.OperationAppID = Guid.NewGuid();
                        hfApplicationID.Value = app.OperationAppID.ToString();

                        dbContext.OperationApps.Add(app);
                        dbContext.SaveChanges();
                    }

                    OperationAppAttach item = new OperationAppAttach();
                    //Guid msDocTypeID = new Guid(cbAttachType.Value.ToString());
                    //v_SuppDoc docType = dbContext.v_SuppDoc.Where(w=> w.MSDocTypeID== msDocTypeID).FirstOrDefault<v_su;
                    item.AttachID = Guid.NewGuid();
                    item.OperationAppID = new Guid(hfApplicationID.Value.ToString());
                    item.AttchTypeID = new Guid(cbAttachType.Value.ToString());

                    item.Path = SaveAttach(cbAttachType.Value.ToString());

                    item.CreatedBy = Session["UserID"].ToString();
                    item.CreatedDate = DateTime.Now;

                    dbContext.OperationAppAttaches.Add(item);
                    dbContext.SaveChanges();// new Guid(Session["AccessID"].ToString()), "AttachID");

                    dsAttach.DataBind();
                    gridAttach.DataBind();
                    dbContext.Dispose();
                    chkAck.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected string SaveAttach(string docid)
        {
            string fileName = "";
            string fullfileDirectory = "";
            string extension = "";
            string OriginalFileName = "";
            bool folderExists;
            string UploadDirectory = "Upload/" + Session["CompID"].ToString() + "/Op";

            try
            {

                if (uploadFile.UploadedFiles.Count() > 0)
                {
                    //Upload File 
                    extension = uploadFile.UploadedFiles[0].FileName.Trim().Substring(uploadFile.UploadedFiles[0].FileName.Trim().LastIndexOf("."));
                    OriginalFileName = docid + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + extension;// uploadFile.UploadedFiles[0].FileName.Trim();


                    fileName = UploadDirectory + "/" + OriginalFileName;
                    fullfileDirectory = Server.MapPath(UploadDirectory + "/" + OriginalFileName);

                    //-------------------------------------------------------------
                    // Save File to server directory
                    //-------------------------------------------------------------
                    folderExists = Directory.Exists(Server.MapPath(UploadDirectory));
                    if (!folderExists)
                        Directory.CreateDirectory(Server.MapPath(UploadDirectory));

                    uploadFile.UploadedFiles[0].SaveAs(fullfileDirectory);
                    //------------------------------------------------------------

                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return fileName.Replace("~", "");
        }
        #endregion

        #region Search Ship
        protected void pcSearchShip_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                string filter = "";

                if (txtSearchShipName.Text != "")
                    filter += " And ShipName like '%" + txtSearchShipName.Text + "%'";

                if (txtIMONo.Text != "")
                    filter += " And IMONo like '%" + txtIMONo.Text + "%'";

                if (txtOffNo.Text != "")
                    filter += " And OffNo like '%" + txtOffNo.Text + "%'";

                if (filter == "")
                    filter += "Where 0=1";
                else
                    filter = "Where 1=1 " + filter;

                DALMMS objMMS = new DALMMS();

                DataSet ds = objMMS.GetShipDetails(filter);

                gridSearchShip.DataSource = ds;
                gridSearchShip.DataBind();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        #endregion

        protected void btnDraft_Click(object sender, EventArgs e)
        {
            try
            {
                if (SubmitApplication(true))
                    Response.Redirect("~//Operation/ApplicantDashboard.aspx", false);
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                    if (SubmitApplication(false))
                    {
                        if (Session["isApprover"].ToString() == "0")
                            Response.Redirect("~//Operation/ApplicantDashboard.aspx", false);
                        else
                            Response.Redirect("~//Operation/ApprovalDashboard.aspx", false);
                    }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
            }
        }

        protected void btnAmend_Click(object sender, EventArgs e)
        {
            try
            {
                    if (SubmitAmendments())
                    {
                        if (Session["isApprover"].ToString() == "0")
                            Response.Redirect("~//Operation/ApplicantDashboard.aspx", false);
                        else
                            Response.Redirect("~//Operation/ApprovalDashboard.aspx", false);
                    }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                btnApprove.Enabled = false;
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    Guid accessGroup = new Guid(Session["UserGroup"].ToString());
                    DALOperation objOperationApp = new DALOperation();
                    Guid operationAppID = new Guid(hfApplicationID.Value.ToString());

                    OperationApp item = dbContext.OperationApps.Find(operationAppID);

                   if (item.IsAmend == true)
                    {
                        item.PermitIssuerUserID = Session["UserID"].ToString();
                        item.IsAppCompleted = true;
                        item.CompletedDate = DateTime.Now;
                        item.IsAmendApprove = true;
                      
                        string folderDirectory = Server.MapPath("Upload/" + item.CompID + "/" + item.OperationAppID.ToString());
                        string fileName = "Permit_" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                        string PermitQRFilePath = "~/Operation/Upload/" + item.CompID + "/" + item.OperationAppID.ToString() + "/" + "qrcode_" + fileName + ".jpg";

                        item.PermitDocLink = objOperationApp.GenerateQRCode(item.OperationAppID.ToString(), item.CompID, folderDirectory, fileName, Server.MapPath(PermitQRFilePath)); //GeneratePermit(operationAppID.ToString(),item.CompID, item.RefID,ref QRPhysicalPath);
                        item.PermitQRCode = Server.MapPath(PermitQRFilePath);

                        Guid? supplyMethod = dbContext.SystemParams.FirstOrDefault<SystemParam>().SupplyMethodA;
                       
                        GeneratePermit(item.OperationAppID.ToString(), item.CompID, item.PermitDocLink);
                        dbContext.SaveChanges(Session["UserID"].ToString(), "OperationAppID", item.OperationAppID);

                        if (!objOperationApp.SubmitAmendApproval(Common.FAction.Approve, item.OperationAppID, Session["UserID"].ToString(), accessGroup, ""))
                        {
                            lblErrMsg.Text = "Failed to Approve.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                        }
                    }
                    else
                    {
                        if (!objOperationApp.SubmitApproval(Common.FAction.Approve, item.OperationAppID, Session["UserID"].ToString(), accessGroup, ""))
                        {
                            lblErrMsg.Text = "Failed to Approve.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                        }
                    }
                }
                Response.Redirect("~//Operation/ApprDashboardA.aspx", false);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>successAlert();</script>", false);
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex.InnerException, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {

                    if (txtReject.Text == "")
                    {
                        lblErrMsg.Text = "Please enter rejection reason";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                        txtReject.Focus();
                        return;
                    }

                    var item = dbContext.OperationApps.Find(new Guid(hfApplicationID.Value.ToString()));

                    Guid appID = new Guid(hfApplicationID.Value.ToString());
                    Guid accessGroup = new Guid(Session["UserGroup"].ToString());
                    DALOperation objOperationApp = new DALOperation();
                    if (item.IsAmend == true)
                    {
                        if (objOperationApp.SubmitAmendApproval(Common.FAction.Reject, appID, Session["UserID"].ToString(), accessGroup, txtReject.Text))
                        {
                            Response.Redirect("~//Operation/ApprDashboardA.aspx", false);
                        }
                        else
                        {
                            lblErrMsg.Text = "Failed to Approve.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>successAlert();</script>", false);
                        }
                    }
                    else
                    {
                        if (objOperationApp.SubmitApproval(Common.FAction.Reject, appID, Session["UserID"].ToString(), accessGroup, txtReject.Text))
                        {
                            Response.Redirect("~//Operation/ApprDashboardA.aspx", false);
                        }
                        else
                        {
                            lblErrMsg.Text = "Failed to Approve.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>successAlert();</script>", false);
                        }
                    }
                    dbContext.Dispose();
                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {

                    if (txtCancel.Text == "")
                    {
                        lblErrMsg.Text = "Please enter cancel reason";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                        txtReject.Focus();
                        return;
                    }

                    Guid operationAppID = new Guid(hfApplicationID.Value.ToString());

                    //Update Application Info
                    OperationApp appInfo = dbContext.OperationApps.Find(operationAppID);

                    appInfo.IsCancel = true;
                    appInfo.CancelDate = DateTime.Now;
                    appInfo.CancelRemark = txtCancel.Text;
                    appInfo.CancelBy = Session["UserID"].ToString();

                    dbContext.SaveChanges(Session["UserID"].ToString(), "OperationAppID", appInfo.OperationAppID);

                    //Update current Flow
                    OperationAppFlow appFlow = dbContext.OperationAppFlows.Where(w => w.OperationAppID == appInfo.OperationAppID && w.IsActive == true).FirstOrDefault<OperationAppFlow>();
                    SystemParam sysParam = dbContext.SystemParams.FirstOrDefault<SystemParam>();

                    appFlow.IsActive = false;

                    dbContext.SaveChanges(Session["UserID"].ToString(), "OperationAppID", appInfo.OperationAppID);

                    //create new flow for cancel
                    OperationAppFlow cancelAppFlow = new OperationAppFlow();
                    cancelAppFlow.OperationAppFlowID = Guid.NewGuid();
                    cancelAppFlow.OperationAppID = appInfo.OperationAppID;
                    cancelAppFlow.FlowActionStatusID = sysParam.FlowCancel;
                    cancelAppFlow.Remark = txtCancel.Text;
                    cancelAppFlow.IsActive = true;
                    cancelAppFlow.ActionBy = Session["UserID"].ToString();
                    cancelAppFlow.ActionDate = DateTime.Now.AddSeconds(1);
                    cancelAppFlow.CreatedDate = DateTime.Now.AddSeconds(1);
                    cancelAppFlow.CreatedBy = Session["UserID"].ToString();

                    dbContext.OperationAppFlows.Add(cancelAppFlow);
                    dbContext.SaveChanges(Session["UserID"].ToString(), "OperationAppFlowID", appInfo.OperationAppID);
                    DALOperation.PendingEmailSTS(appInfo.OperationAppID, sysParam.FlowCancel, false);

                    dbContext.Dispose();
                }
                Response.Redirect("~//Operation/ApprDashboardA.aspx", false);

            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private bool SubmitApplication(bool isDraft)
        {
            try
            {
                DALOperation objOperationApp = new DALOperation();

                if (cbSTSO.Value == null)
                {
                    lblErrMsg.Text = "Please select STS Operator";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    cbSTSO.Focus();
                    return false;
                }
                if (cbFSU.Value == null)
                {
                    lblErrMsg.Text = "Please select Vessel FSU";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    cbFSU.Focus();
                    return false;
                }

               
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    SystemParam sysParam = dbContext.SystemParams.FirstOrDefault<SystemParam>();
                    eSTS.Database.OperationApp item = null;

                    Guid licCompVesselID = new Guid(cbFSU.Value.ToString());
                    LicCompanyVessel FSU = dbContext.LicCompanyVessels.Where(w => w.LicCompanyVesselID == licCompVesselID).FirstOrDefault<LicCompanyVessel>();

 
                    if (isDraft == true) //Save As Draft
                    {
                        ///Save Operation App
                        if (Session["mode"].ToString() == "n")
                        {
                            if (hfApplicationID.Value.ToString() == "")
                            {
                                item = new eSTS.Database.OperationApp();
                                item.OperationAppID = Guid.NewGuid();
                                dbContext.OperationApps.Add(item);
                            }
                            else
                                item = dbContext.OperationApps.Find(new Guid(hfApplicationID.Value.ToString()));
                        }
                        else
                        {
                            item = dbContext.OperationApps.Find(new Guid(hfApplicationID.Value.ToString()));
                        }
                        if (cbFSU.Value != null)
                        {
                            item.VRID = licCompVesselID;
                            item.VRIMONo = FSU.IMONo;
                            item.VRName = FSU.ShipName;
                            item.VRFlag = FSU.ShipFlag;
                            item.VRPortReg = FSU.PortReg;
                            item.VRLOA = Convert.ToDouble(FSU.LOA);
                            item.VRGRT = Convert.ToDouble(FSU.GRT);
                            item.VRNRT = Convert.ToDouble(FSU.NRT);
                            item.VRMMSINo = FSU.MMSINo;
                            //FSU
                            item.VRCallSign = txtFSUCallSign.Text;
                            item.VRLatDegree = Convert.ToInt32(txtLatDegree.Text);
                            item.VRLatMin = Convert.ToDecimal(txtLatMin.Text);
                            item.VRLongDegree = Convert.ToInt32(txtLongDegree.Text);
                            item.VRLongMin = Convert.ToDecimal(txtLongMin.Text);
                            item.VRLatitude = LatLong.FromDegreeDecimalMinutes(Convert.ToInt32(txtLatDegree.Text), Convert.ToDouble(txtLatMin.Text)).ToStringD();
                            item.VRLongitude = LatLong.FromDegreeDecimalMinutes(Convert.ToInt32(txtLongDegree.Text), Convert.ToDouble(txtLongMin.Text)).ToStringD();
                            item.VRSupritendentName = txtSupName.Text;
                            item.VRSupritendentTelNo = txtSupTelNo.Text;
                        }

                        item.CompID = Session["CompID"].ToString();
                        item.SOCompID = cbSTSO.Value.ToString();
                        item.SOLicID = FSU.LicCompanyID;
                        item.SupplyMethodID = sysParam.SupplyMethodA;
                        AssignValue(item);
                        item.IsDraft = true;
                        //Default False;
                        item.IsSubmit = false;
                        item.IsAppCompleted = false;
                        item.IsPayment = false;
                        item.IsRejected = false;
                        item.IsCancel = false;
                        item.IsSubmitBL = false;
                        item.IsSubmitCM = false;

                        if (Session["mode"].ToString() == "n")
                        {
                            item.DraftDate = DateTime.Now;
                            item.DraftBy = Session["UserID"].ToString();
                            item.CreatedBy = Session["UserID"].ToString();
                            item.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            item.UpdatedBy = Session["UserID"].ToString();
                            item.UpdatedDate = DateTime.Now;
                        }

                        ///Save CaseNum 
                        //if (item.CaseNum == null || item.CaseNum == "")
                        //{
                        //    item.CaseNum = objOperationApp.GenerateCaseNum(Convert.ToInt32((hfLicLocation.Value.ToString())), item.EstOperationDateTime.Value.Year);
                        //}
                        hfApplicationID.Value = item.OperationAppID.ToString();
                        objOperationApp.SubmitApplication(item, item.SupplyMethodID, Session["UserID"].ToString(), isDraft);

                        dbContext.SaveChanges(Session["UserID"].ToString(), "OperationAppID", item.OperationAppID);
                    }
                    else
                    {
                        //if (objOperationApp.checkPendingBDN(Session["CompID"].ToString()) > 0)
                        //{
                        //    // lblErrMsg.Text = "Sorry, new bunker applications are not allowed because there are Bunker Delivery Note (BDN) that have not been submitted. ";
                        //    lblErrMsg.Text = "Your application could not be proceeded due to pending BDN(s)submission";
                        //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                        //    return false;
                        //}
                        if (!IsValidate(false))
                            return false;

                        //Remark by mala on 24 oktober 2025 
                        //auto approve
                        //if (!IsValidateSuppDoc(FSU.LicCompanyID.ToString(), FSU.LicCompanyVesselID.ToString()))
                        //    return false;


                        ///Save Operation App
                        if (Session["mode"].ToString() == "n")
                        {
                            if (hfApplicationID.Value.ToString() == "")
                            {
                                item = new eSTS.Database.OperationApp();
                                item.OperationAppID = Guid.NewGuid();
                                dbContext.OperationApps.Add(item);

                            }
                            else
                                item = dbContext.OperationApps.Find(new Guid(hfApplicationID.Value.ToString()));
                        }
                        else
                        {
                            item = dbContext.OperationApps.Find(new Guid(hfApplicationID.Value.ToString()));
                        }
                       
                        // Assign FSU in OperationApp
                        item.VRID = licCompVesselID;
                        item.VRIMONo = FSU.IMONo;
                        item.VRName = FSU.ShipName;
                        item.VRFlag = FSU.ShipFlag;
                        item.VRPortReg = FSU.PortReg;
                        item.VRLOA = Convert.ToDouble(FSU.LOA);
                        item.VRGRT = Convert.ToDouble(FSU.GRT);
                        item.VRNRT = Convert.ToDouble(FSU.NRT);
                        item.VRMMSINo = FSU.MMSINo;
                        //FSU
                        item.VSCallSign = txtFSUCallSign.Text;
                        item.VRLatDegree = Convert.ToInt32(txtLatDegree.Text);
                        item.VRLatMin = Convert.ToDecimal(txtLatMin.Text);
                        item.VRLongDegree = Convert.ToInt32(txtLongDegree.Text);
                        item.VRLongMin = Convert.ToDecimal(txtLongMin.Text);
                        item.VRLatitude = LatLong.FromDegreeDecimalMinutes(Convert.ToInt32(txtLatDegree.Text), Convert.ToDouble(txtLatMin.Text)).ToStringD();
                        item.VRLongitude = LatLong.FromDegreeDecimalMinutes(Convert.ToInt32(txtLongDegree.Text), Convert.ToDouble(txtLongMin.Text)).ToStringD();
                        item.VRSupritendentName = txtSupName.Text;
                        item.VRSupritendentTelNo = txtSupTelNo.Text;

                        //Guid LicCompanyVesselID = new Guid(cbFSU.Value.ToString());
                      //  v_OpLicCompanyVessel lic = dbContext.v_OpLicCompanyVessel.Where(w => w.LicCompanyVesselID == LicCompanyVesselID).FirstOrDefault<v_OpLicCompanyVessel>();

                        item.CompID = Session["CompID"].ToString();
                        item.SOCompID = cbSTSO.Value.ToString();
                        item.SOLicID = FSU.LicCompanyID;
                        item.SupplyMethodID = new Guid(sysParam.SupplyMethodA.Value.ToString());

                        AssignValue(item);

                        item.IsSubmit = true;
                        item.SubmitDate = DateTime.Now;
                        item.SubmitBy = Session["UserID"].ToString();
                        item.IsDraft = false;
                        item.IsAppCompleted = false;
                        item.IsPayment = false;
                        item.IsRejected = false;
                        item.IsCancel = false;
                        item.IsSubmitBL = false;
                        item.IsSubmitCM = false;

                        if (Session["mode"].ToString() == "n")
                        {
                            item.CreatedBy = Session["UserID"].ToString();
                            item.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            item.UpdatedBy = Session["UserID"].ToString();
                            item.UpdatedDate = DateTime.Now;
                        }

                        ///Save CaseNum
                        if (item.CaseNum == null)
                        {
                            item.CaseNum = objOperationApp.GenerateCaseNum(Convert.ToInt32((hfLicLocation.Value.ToString())), item.EstOperationDateTime.Value.Year);
                        }


                        int caseNum = dbContext.OperationApps.Where(w => w.CaseNum == item.CaseNum).Count<OperationApp>();
                        //Remark : Auto approve
                        //Amend by: Normala
                        //Amend Date:24/10/25
                        objOperationApp.SubmitAutoApprove(isDraft, item.SupplyMethodID, item.OperationAppID, Session["UserID"].ToString());
                        //----------------------------------------------------------------------------------------------------------------
                        hfApplicationID.Value = item.OperationAppID.ToString();

                        dbContext.SaveChanges(Session["UserID"].ToString(), "OperationAppID", item.OperationAppID);
                      

                    }
                    Generate(item);

                    //Save FSU Master
                    LicCompanyVessel FSUMaster = dbContext.LicCompanyVessels.Find(item.VRID);
                    FSUMaster.SupritendentName = txtSupName.Text.ToString();
                    FSUMaster.SupritendentTelNo = txtSupTelNo.Text.ToString();
                    FSUMaster.CallSign = txtFSUCallSign.Text;
                    FSUMaster.LatDegree = Convert.ToInt32(txtLatDegree.Text);
                    FSUMaster.LatMin = Convert.ToDecimal(txtLatMin.Text);
                    FSUMaster.LongDegree = Convert.ToInt32(txtLongDegree.Text);
                    FSUMaster.LongMin = Convert.ToDecimal(txtLongMin.Text);
                    FSUMaster.Latitude = LatLong.FromDegreeDecimalMinutes(Convert.ToInt32(txtLatDegree.Text), Convert.ToDouble(txtLatMin.Text)).ToStringD();
                    FSUMaster.Longitude = LatLong.FromDegreeDecimalMinutes(Convert.ToInt32(txtLongDegree.Text), Convert.ToDouble(txtLongMin.Text)).ToStringD();
                    FSUMaster.UpdatedBy = Session["UserID"].ToString();
                    FSUMaster.UpdatedDate = DateTime.Now;

                    dbContext.SaveChanges(Session["UserID"].ToString(), "LicCompanyVesselID", FSUMaster.LicCompanyVesselID);
                    dbContext.Dispose();

                    //Save Agent Code
                    using (MMSSyncEntities MMSContext = new MMSSyncEntities())
                    {
                        //Save Company Profile
                        string compID = Session["CompID"].ToString();
                        CompanyProfile comp = MMSContext.CompanyProfiles.Where(w => w.Orgzid == compID).FirstOrDefault<CompanyProfile>();
                        comp.ContactPerson = txtContactPerson.Text;
                        comp.AgentCode = txtAgentCode.Text;

                        MMSContext.SaveChanges();

                        //Save User Profile
                        string userID = Session["UserID"].ToString();
                        User usr = MMSContext.Users.Where(w => w.UserID == userID).FirstOrDefault<User>();

                        usr.ICNo = txtICNumber.Text;
                        usr.Designation = txtDesignation.Text;
                        usr.EmailAddress = txtAgentEmail.Text;
                        MMSContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                return false;
            }
            return true;
        }

        private bool SubmitAmendments()
        {
            /* CR : EB/CR/2022/01/001
               Added by : Normala
               Date : 25/01/2022
               Reason/Purpose : Submit Amendments 
               */
            try
            {
                DALOperation objOperationApp = new DALOperation();

                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    SystemParam sysParam = dbContext.SystemParams.FirstOrDefault<SystemParam>();
                    eSTS.Database.OperationApp item = null;

                    item = dbContext.OperationApps.Find(new Guid(hfApplicationID.Value.ToString()));

                    if (!IsValidate(false))
                        return false;

                    if (!IsValidateSuppDoc(item.SOLicID.ToString(), item.VRID.ToString()))
                        return false;


                    AssignValue(item);

                    item.IsAmend = true;
                    item.UpdatedBy = Session["UserID"].ToString();
                    item.UpdatedDate = DateTime.Now;

                    objOperationApp.SubmitAmendment(item, item.SupplyMethodID, Session["UserID"].ToString(), false, txtAmend.Text);
                    hfApplicationID.Value = item.OperationAppID.ToString();

                    dbContext.SaveChanges(Session["UserID"].ToString(), "OperationAppID", item.OperationAppID);
                    Generate(item);

                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                return false;
            }
            return true;
        }

        private bool IsValidate(bool isDraft)
        {
            try
            {

                if (Convert.ToDateTime(dtOperationDate.Value) > Convert.ToDateTime(hfLicExpDate.Value))
                {
                    lblErrMsg.Text = "Operation on a selected date is not allowed, due to the license expired on " + Convert.ToDateTime(hfLicExpDate.Value).ToString("dd/MM/yyyy");
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    dtOperationDate.Focus();
                   // return false;
                }

                if (txtContactPerson.Text == "")
                {
                    lblErrMsg.Text = "Please enter Contact Person Name";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtContactPerson.Focus();
                    return false;
                }
                if (txtAgentEmail.Text == "")
                {
                    lblErrMsg.Text = "Please enter Contact Person Email";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtAgentEmail.Focus();
                    return false;
                }
                if (txtAgentCode.Text == "")
                {
                    lblErrMsg.Text = "Please enter Agent Code";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtAgentCode.Focus();
                    return false;
                }
                if (txtSupName.Text == "")
                {
                    lblErrMsg.Text = "Please enter Supritendant Name";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtSupName.Focus();
                    return false;
                }
                if (txtSupTelNo.Text == "" || txtSupTelNo.Text == "-" || txtSupTelNo.Text.Length <= 1)
                {
                    lblErrMsg.Text = "Please enter Supritendant Tel No";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtSupTelNo.Focus();
                    return false;
                }
                if (txtFSUCallSign.Text == "" || txtFSUCallSign.Text == "-" || txtFSUCallSign.Text.Length <= 1 )
                {
                    lblErrMsg.Text = "Please enter FSU Call Sign No";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtFSUCallSign.Focus();
                    return false;
                }
                if (txtLatDegree.Text == "" || txtLatDegree.Text == "0")
                {
                    lblErrMsg.Text = "Please Enter Latitude Degree";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtLatDegree.Focus();
                    return false;
                }
                if (txtLatMin.Text == "" || txtLatMin.Text == "0")
                {
                    lblErrMsg.Text = "Please Enter Latitude Min";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtLatMin.Focus();
                    return false;
                }
                if (txtLongDegree.Text == "" || txtLongDegree.Text == "0")
                {
                    lblErrMsg.Text = "Please Enter Longitude Degree";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtLongDegree.Focus();
                    return false;
                }
                if (txtLongMin.Text == "" || txtLongMin.Text == "0")
                {
                    lblErrMsg.Text = "Please Enter Longitude Min";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtLongMin.Focus();
                    return false;
                }
                if (cbPermitIssuer.Value == null)
                {
                    lblErrMsg.Text = "Please select Permit Issuer";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    cbPermitIssuer.Focus();
                    return false;
                }
                //Vessel Supplier
                if (txtIMONo.Text == "" || txtIMONo.Text.Length <= 1 || txtIMONo.Text.Contains("-"))
                {
                    lblErrMsg.Text = "Please enter valid Vessel Supplier IMO No.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtIMONo.Focus();
                    return false;
                }
                if (txtPortReg.Text == "" || txtPortReg.Text.Length <= 1)
                {
                    lblErrMsg.Text = "Please enter valid Port Register";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtPortReg.Focus();
                    return false;
                }
                if (txtVesselName.Text == "" || txtVesselName.Text.Length <= 1 || txtVesselName.Text.Contains("-"))
                {
                    lblErrMsg.Text = "Please enter valid Vessel Supplier Name";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtVesselName.Focus();
                    return false;
                }
                if (cbFlag.Value == null || cbFlag.Value.ToString() == "")
                {
                    lblErrMsg.Text = "Please select Vessel Receiver Flag";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    cbFlag.Focus();
                    return false;
                }
                if (txtGRT.Value.ToString() == "0.00")
                {
                    lblErrMsg.Text = "Please enter GRT value";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtGRT.Focus();
                    return false;
                }
                if (txtNRT.Value.ToString() == "0.00")
                {
                    lblErrMsg.Text = "Please enter NRT value";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtNRT.Focus();
                    return false;
                }
                if (txtLOA.Value.ToString() == "0.00")
                {
                    lblErrMsg.Text = "Please enter LOA value";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtLOA.Focus();
                    return false;
                }
                //if (txtMMSINo.Text == "" || txtMMSINo.Text.Length <= 1 || txtMMSINo.Text.Contains("-"))
                //{
                //    lblErrMsg.Text = "Please enter valid MMSI No.";
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                //    txtMMSINo.Focus();
                //    return false;
                //}
                if (txtCallSign.Text == "" || txtCallSign.Text.Length <= 1 || txtCallSign.Text.Contains("-"))
                {
                    lblErrMsg.Text = "Please enter valid SCN No.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtCallSign.Focus();
                    return false;
                }
                if (cbLastPort.Value == null)
                {
                    lblErrMsg.Text = "Please select Last Port Call";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    cbLastPort.Focus();
                    return false;
                }
                if (cbNextPort.Value == null)
                {
                    lblErrMsg.Text = "Please select Next Port Call";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    cbNextPort.Focus();
                    return false;
                }

                //Product Supply
                if (dtOperationDate.Value == null)
                {
                    lblErrMsg.Text = "Please enter Estimate Operation Date";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    dtOperationDate.Focus();
                    return false;
                }
                if (timeOperation.Value == null)
                {
                    lblErrMsg.Text = "Please enter Estimate Operation Time";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    timeOperation.Focus();
                    return false;
                }
                if (cbOilType.Value == null)
                {
                    lblErrMsg.Text = "Please select Oil Type";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    cbOilType.Focus();
                    return false;
                }
                if (txtMT.Text == "0.00")
                {
                    lblErrMsg.Text = "Please enter Oil Quantity";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    txtMT.Focus();
                    return false;
                }
                if (cbUOM.Value == null)
                {
                    lblErrMsg.Text = "Please enter Unit of Measure";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    cbUOM.Focus();
                    return false;
                }


                if (gridAttach.VisibleRowCount <= 0)
                {
                    lblErrMsg.Text = "You are not allowed to submit. Please attach requirement supporting document.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    return false;
                }
                if (!chkAck.Checked)
                {
                    lblErrMsg.Text = "Please check Company Acknowledgement & Integrity Clause before you submit.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    chkAck.Focus();
                    return false;
                }
                if (!chkIntegrity.Checked)
                {
                    lblErrMsg.Text = "Please check Company Acknowledgement & Integrity Clause before you submit.";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                    chkIntegrity.Focus();
                    return false;
                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                lblErrMsg.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);

                return false;
            }
            return true;
        }

        private bool IsValidateSuppDoc(string licCompanyID,string licCompanyVesselID)
        {
            DALOperation objOperationApp = new DALOperation();
            DataSet dsSuppDoc = objOperationApp.GetSuppDocList(licCompanyID,licCompanyVesselID);

            if (dsSuppDoc != null)
            {
                foreach (DataRow dr in dsSuppDoc.Tables[0].Rows)
                {
                    if(dr["LicID"].ToString() !="")
                    {
                        if (Convert.ToBoolean(dr["IsExpired"]) == true)
                        {
                            lblErrMsg.Text = "Your " + dr["DocDesc"].ToString().ToUpperInvariant() + " has expired. Please upload a new document with a current effective date.";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                            lblErrMsg.Focus();
                            return false;
                        }
                    }
                    else
                    {
                        lblErrMsg.Text = "Please upload " + dr["DocDesc"].ToString().ToUpperInvariant() + " with a current effective date.";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                        lblErrMsg.Focus();
                        return false;
                    }
                }
                           }
            else
            {
                lblErrMsg.Text = "Please upload supporting documents";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>errorAlert();</script>", false);
                lblErrMsg.Focus();
                return false;
            }
            return true;
        }
        private void AssignValue(OperationApp item)
        {
            if (cbDeliveryLoc.Value != null)
                item.DeliveryLocID = new Guid(cbDeliveryLoc.Value.ToString());

            if (cbPermitIssuer.Value != null)
                item.PermitIssuerID = new Guid(cbPermitIssuer.Value.ToString());

            //Vessel Supplier
            if (txtIMONo.Text != "")
                item.VSIMONo = txtIMONo.Text;

            if (txtVesselName.Text != "")
                item.VSName = txtVesselName.Text;

            if (cbFlag.Value != null)
                item.VSFlag = cbFlag.Value.ToString();

            if (txtPortReg.Text != "")
                item.VSPortReg = txtPortReg.Text;

            if (txtLOA.Text != "")
                item.VSLOA = Convert.ToDouble(txtLOA.Text);

            if (txtGRT.Text != "")
                item.VSGRT = Convert.ToDouble(txtGRT.Text);

            if (txtNRT.Text != "")
                item.VSNRT = Convert.ToDouble(txtNRT.Text);

            if (txtMMSINo.Text != "")
                item.VSMMSINo = txtMMSINo.Text;

            if (txtCallSign.Text != "")
                item.VSCallSign = txtCallSign.Text;

            if (cbLastPort.Value != null)
            {
                item.VSLastPort = cbLastPort.Value.ToString();
            }
            if (cbNextPort.Value != null)
            {
                item.VSNextPort = cbNextPort.Value.ToString();
            }

            //Product Supply
            if (dtOperationDate.Value != null)
                item.EstOperationDateTime = Convert.ToDateTime(dtOperationDate.Value);
            if (timeOperation.Value != null)
                item.EstOperationTime = new DateTime(item.EstOperationDateTime.Value.Year, item.EstOperationDateTime.Value.Month, item.EstOperationDateTime.Value.Day, Convert.ToDateTime(timeOperation.Value).Hour, Convert.ToDateTime(timeOperation.Value).Minute, 0);
            if (txtMT.Text != "")
                item.EstOilMT = Convert.ToDouble(this.txtMT.Text);
            //if (cbOilClass.Value != null)
            //    item.OilClassID = new Guid(cbOilClass.Value.ToString());
            if (cbOilType.Value != null)
                item.OilTypeID = new Guid(cbOilType.Value.ToString());

            if (cbUOM.Value != null)
                item.UOMID = new Guid(cbUOM.Value.ToString());

            if (chkAck.Checked)
                item.IsAcknowledge = true;

            if (chkIntegrity.Checked)
                item.IsIntegrity = true;

            item.IntegrityClauseEN = @"<p>I/Company or our servants hereby declare that I/company or our servants will not offer a bribe to Johor Port Authority&rsquo;s servants or other individual which involved direct or indirect business practice to get the approved license.</p>
<p>If I/Company or servants is found to have violated or involved in violation of the integrity pact of any corrupt business practice, then I/Company or servants shall be entitled to:</p>
<p>Termination of the license or<br />Blacklisted and<br />Disciplinary action following by Malaysian government procurement regulations<br />If I/Company or our servants receive an offer/ a bribe from Johor Port Authority&rsquo;s servants or other individual which involved direct or indirect to give the approved license, I/Company or our servants promises that I/Company or our servants will report to Malaysian Anti-Corruption Commission (MACC) or police station immediately.</p>";
            item.IntegrityClauseMY = @"<p>Saya/ Syarikat dengan ini mengisytiharkan bahawa saya atau mana-mana individu dalam yang mewakili syarikat ini tidak akan menawar atau memberi rasuah kepada mana-mana individu dalam Lembaga Pelabuhan Johor atau mana-mana individu lain, sebagai ganjaran mendapatkan kelulusan lesen seperti di atas.</p>
<p>Sekiranya saya atau mana-mana individu yang mewakili syarikat ini di dapati bersalah menawar atau memberi rasuah kepada mana-mana individu dalam Lembaga Pelabuhan Johor atau mana-mana individu lain sebagai ganjaran mendapatkan kelulusan lesen seperti di atas, maka saya sebagai wakil syarikat bersetuju tindakan-tindakan berikut diambil :</p>
<p>Penarikan balik lesen aktiviti pelabuhan; dan<br />Disenarai hitam untuk mohon lesen aktiviti pelabuhan; atau<br />Lain-lain tindakan tatatertib mengikut peraturan Perolehan Kerajaan.<br />Sekiranya terdapat mana-mana individu cuba meminta rasuah daripada saya atau mana-mana individu yang berkaitan dengan syarikat ini sebagai ganjaran mendapatkan sebut harga seperti di atas, maka saya berjanji akan dengan segera melaporkan perbuatan tersebut kepada pejabat Suruhanjaya Rasuah Malaysia(SPRM) atau balai polis yang berhampiran.</p>";


        }
        #region Generate Permit & QRCode
        private bool Generate(OperationApp item)
        {
            try
            {
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    DALOperation objOperationApp = new DALOperation();
                    OperationApp app = dbContext.OperationApps.Find(item.OperationAppID);

                    string folderDirectory = "";
                    string fileName = "";
                    string PermitQRFilePath = "";

                    //////Notis Permit
                    ////folderDirectory = Server.MapPath("Upload/" + app.CompID + "/" + app.OperationAppID.ToString());
                    ////fileName = "NotisPermit_" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    ////PermitQRFilePath = "~/Operation/Upload/" + app.CompID + "/" + app.OperationAppID.ToString() + "/" + "qrcode_" + fileName + ".jpg";

                    ////app.NPDocLink = objOperationApp.GenerateQRCode(app.OperationAppID.ToString(), app.CompID, folderDirectory, fileName, Server.MapPath(PermitQRFilePath)); //GeneratePermit(operationAppID.ToString(),item.CompID, item.RefID,ref QRPhysicalPath);
                    ////app.NPQRCode = Server.MapPath(PermitQRFilePath);
                    ////GeneratePermit(app.OperationAppID.ToString(), app.CompID, app.NPDocLink);

                    //Lampiran 1
                    folderDirectory = Server.MapPath("Upload/" + app.CompID + "/" + app.OperationAppID.ToString());
                    fileName = "Lampiran1_" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    PermitQRFilePath = "~/Operation/Upload/" + app.CompID + "/" + app.OperationAppID.ToString() + "/" + "qrcode_" + fileName + ".jpg";

                    app.Lampiran1DocLink = objOperationApp.GenerateQRCode(app.OperationAppID.ToString(), app.CompID, folderDirectory, fileName, Server.MapPath(PermitQRFilePath)); //GeneratePermit(operationAppID.ToString(),item.CompID, item.RefID,ref QRPhysicalPath);
                    app.Lampiran1QRCode = Server.MapPath(PermitQRFilePath);
                    GenerateLampiran(app.OperationAppID.ToString(), app.CompID, app.Lampiran1DocLink);

                    dbContext.SaveChanges(Session["UserID"].ToString(), "OperationAppID", app.OperationAppID);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return false;
            }
            return true;
        }
        protected void GeneratePermit(string operationAppID, string refID, string permitFilePath)
        {
            try
            {
                ReportDocument oRpt = new ReportDocument();
                string dbServer = System.Web.Configuration.WebConfigurationManager.AppSettings["DBServer"];
                string dbCatalog = WebConfigurationManager.AppSettings["DBCatalog"];
                string dbUser = WebConfigurationManager.AppSettings["DBUser"];
                string dbPass = WebConfigurationManager.AppSettings["DBPass"];

               oRpt.Load(Server.MapPath("~/Operation/PetrolPermit.rpt"));
               

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

     
    }
}