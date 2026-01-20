<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspiniaMethodB.Master" AutoEventWireup="true" CodeBehind="HistoryActivityB.aspx.cs" Inherits="eSTS.Operation.HistoryActivityB" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     
      <div class="row">
  <asp:HiddenField runat="server" ID="hfApplicationID" ClientIDMode="Static" />
          <asp:HiddenField runat="server" ID="hfApplicationFlowID" ClientIDMode="Static" />
          </div>

    <div class="row">
        <div class="col-lg-12">
            <div class="form-group row">
                <label class="col-sm-2 col-form-label">Operation Date/Time</label>
                <div class="col-sm-3">
                    <dx:BootstrapDateEdit ID="dtOperationDateFrom" DisplayFormatString="dd/MM/yyyy"  EditFormatString="dd/MM/yyyy" EditFormat="Custom" runat="server"></dx:BootstrapDateEdit>
                </div>
                <label class="col-sm-1 col-form-label">To</label>
                <div class="col-sm-3">
                    <dx:BootstrapDateEdit ID="dtOperationDateTo" DisplayFormatString="dd/MM/yyyy"  EditFormatString="dd/MM/yyyy" EditFormat="Custom" runat="server"></dx:BootstrapDateEdit>
                </div>
                <div class="col-sm-2" style="padding: 0">
                    <dx:BootstrapButton ID="btnSearch" CssClasses-Icon="fa fa-search" runat="server" AutoPostBack="False" Text="Search">
                        <SettingsBootstrap RenderOption="Warning" />
                        <CssClasses Icon="fa fa-search"></CssClasses>

                        <ClientSideEvents Click="function(s, e) {
	gridComplete.Refresh();
}" />
                    </dx:BootstrapButton>
                </div>
            </div>
        </div>

    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <%--<i class="fa fa-warning"></i>--%> Complete Supply Vessel To FSU Activity
                </div>
                <div class="panel-body">
                    <div class="col-lg-12">
                             <dx:BootstrapPopupControl ID="pcPayment" runat="server" Width="430px" CloseAction="CloseButton" CloseOnEscape="True" Modal="false"
                            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcPayment"
                            HeaderText="Permit Petrol">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <div class="form-group row">
                                        <label class="col-sm-4 col-form-label">Payment Date</label>
                                        <div class="col-lg-8">
                                            <dx:BootstrapDateEdit ID="dtPaymentDate" ReadOnly="true" ClientInstanceName="dtPaymentDate" runat="server" DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dx:BootstrapDateEdit>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-4 col-form-label">Payment Time</label>
                                        <div class="col-lg-8">
                                            <dx:BootstrapTimeEdit ID="paymentTime" ReadOnly="true" ClientInstanceName="paymentTime" runat="server" DisplayFormatString="HH:mm" EditFormatString="HH:mm"></dx:BootstrapTimeEdit>

                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-4 col-form-label">Payment Amount</label>
                                        <div class="col-lg-8">
                                            <dx:BootstrapTextBox ID="txtPaymentAmt" ReadOnly="true" ClientInstanceName="txtPaymentAmt" runat="server" class="form-control required">
                                                <MaskSettings Mask="&lt;0..99999g&gt;.&lt;00..99&gt;" />
                                            </dx:BootstrapTextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-4 col-form-label">Receipt No</label>
                                        <div class="col-lg-8">
                                            <dx:BootstrapTextBox ID="txtReceiptNo" ReadOnly="true" ClientInstanceName="txtReceiptNo" MaxLength="50" runat="server" class="form-control required"></dx:BootstrapTextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-4 col-form-label">Permit Reference</label>
                                        <div class="col-lg-8">
                                            <dx:BootstrapTextBox ID="txtPermitRef" ReadOnly="true" ClientInstanceName="txtPermitRef" MaxLength="50" runat="server" class="form-control required"></dx:BootstrapTextBox>
                                        </div>
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>

                        </dx:BootstrapPopupControl>
                         <asp:EntityDataSource ID="dsComplete" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="v_OperationApp" OnSelecting="dsComplete_Selecting" OrderBy="it.[EstOperationTime] desc" Select="it.[OperationAppID], it.[SupplyMethodID], it.[CaseNum], it.[DeliveryLocation], it.[VSLatDegree], it.[VSLatMin], it.[VSLongDegree], it.[VSLongMin], it.[VRLatDegree], it.[VRLatMin], it.[VRLongDegree], it.[VRLongMin], it.[VSIMONo], it.[VSName], it.[VSFlag], it.[VSPortReg], it.[VSLOA], it.[VSGRT], it.[VSNRT], it.[VRLOA], it.[VRGRT], it.[VRNRT], it.[VRMMSINo], it.[UOMCode], it.[EstOilMT], it.[EstOperationDateTime], it.[EstOperationTime], it.[OilTypeDesc], it.[ActionStatus], it.[LabelColor], it.[VRIMONo], it.[VRName], it.[VRFlag], it.[VRPortReg], it.[CompanyName], it.[IsPayment], it.[PermitDocLink], it.[NPDocLink], it.[Lampiran1DocLink], it.[IsSubmitCM], it.[PaymentDate], it.[PaymentTime], it.[PaymentAmount], it.[ReceiptNo], it.[PaymentRefID], it.[FlowActionStatusID], it.[IsSubmitBL],it.[ValidPermit],it.[IsAmend],it.[IsAmendApprove]" >
                        </asp:EntityDataSource>
                        <dx:BootstrapGridView ID="gridComplete" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridComplete" DataSourceID="dsComplete" KeyFieldName="OperationAppID" EnableTheming="False" OnHtmlDataCellPrepared="gridComplete_HtmlDataCellPrepared">
                            <SettingsAdaptivity AdaptivityMode="HideDataCells">
                            </SettingsAdaptivity>
                            <SettingsBehavior ConfirmDelete="True" />
                            <SettingsPager EnableAdaptivity="True" PageSize="3">
                            </SettingsPager>
                            <SettingsCommandButton>
                                <EditButton IconCssClass="fa fa-edit text-success" Text=" " />
                                <DeleteButton IconCssClass="fa fa-trash text-danger" Text=" " />
                            </SettingsCommandButton>
                            <Columns>
                                <dx:BootstrapGridViewTextColumn FieldName="OperationAppID" Caption="#" VisibleIndex="0">
                                    <CssClasses DataCell="text-navy" />
                                    <DataItemTemplate>
                                        <dx:ASPxHyperLink ID="hyperLink" runat="server" OnInit="lilView_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                        <dx:ASPxHyperLink ID="linkPayment" runat="server" OnInit="lilPayment_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                        <%-- <dx:ASPxHyperLink ID="lilNotis" runat="server" OnInit="lilNotis_Init" Target="_self">
                                        </dx:ASPxHyperLink>--%>
                                          <dx:ASPxHyperLink ID="LilLampiran" runat="server" OnInit="LilLampiran_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                        <dx:ASPxHyperLink ID="lilPermit" runat="server" OnInit="lilPermit_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                           <dx:ASPxHyperLink ID="lilCM" runat="server" OnInit="lilCM_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                    </DataItemTemplate>
                                </dx:BootstrapGridViewTextColumn>
                                 <dx:BootstrapGridViewTextColumn FieldName="CompID" VisibleIndex="1" Caption="ROC No." Visible="false">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="CaseNum" VisibleIndex="2" Caption="Case Num">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="CompanyName" VisibleIndex="3" Caption="Company Name">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="DeliveryLocation" Caption="Delivery Location" visible="false">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="VSName"  Caption="Vessel Supplier"  VisibleIndex="3" ReadOnly="True" >
                                </dx:BootstrapGridViewTextColumn>
                                 <dx:BootstrapGridViewTextColumn FieldName="VRName"  Caption="Vessel Receiver (FSU)"  VisibleIndex="4" ReadOnly="True" >
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="UOMCode" Caption="Product Supply" ReadOnly="True" VisibleIndex="5">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="ActionStatus" Caption="Status" VisibleIndex="6" ReadOnly="True">
                                </dx:BootstrapGridViewTextColumn>
                            </Columns>
                            <SettingsSearchPanel Visible="True" />
                        </dx:BootstrapGridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function popUpNewPayment(id, id2) {
            // alert(id);
            $("#hfApplicationID").val(id);
            $("#hfApplicationFlowID").val(id2);

            pcPayment.ShowWindow(pcPayment.GetWindow(0));

        }

        function popUpPayment(id, dtPayYear, dtPayMth, dtPayDay, timePayHour, timePayMin, paymentAmt, receiptNo, permitRef) {
            // alert(id);
            console.log("dtPayYear >" + dtPayYear);
            console.log("dtPayMth >" + dtPayMth);
            console.log("dtPayDay > " + dtPayDay);
            console.log("timePayHour >" + timePayHour);
            console.log("timePayMin >" + timePayMin);
            console.log("paymentAmt >" + paymentAmt);
            console.log("receiptNo >" + receiptNo);
            console.log("permitRef >" + permitRef);

            $("#hfApplicationID").val(id);

            var sPayDate = new Date(dtPayYear, parseInt(dtPayMth) - 1, parseInt(dtPayDay), 00, 00, 00);
            console.log(sPayDate);
            dtPaymentDate.SetValue(sPayDate);

            var sPayTime = new Date(dtPayYear, parseInt(dtPayMth), parseInt(dtPayDay), timePayHour, timePayMin, 00);
            paymentTime.SetValue(sPayTime);

            txtPaymentAmt.SetValue(paymentAmt);
            txtReceiptNo.SetValue(receiptNo);
            txtPermitRef.SetValue(permitRef);
            pcPayment.ShowWindow(pcPayment.GetWindow(0));

        }
        function popUpLongLatForm(id, latDegree) {
            // alert(id);
            $("#hfApplicationID").val(id);

            pcLongLat.ShowWindow(pcLongLat.GetWindow(0));

        }
        function showMap(lat, long) {
            if (lat != "" || long != "")
                window.open("http://maps.google.com/maps?q=" + lat + "," + long + "&z=18");
            else {
                var msg = document.getElementById("lblErrMsg");
                if (lat == "")
                    msg = "Please enter Latitude";

                if (long == "")
                    msg = "Please enter Longitude";

                var x = document.getElementById("error_alert");
                x.style.display = "block";
            }

        }

    </script>
</asp:Content>
