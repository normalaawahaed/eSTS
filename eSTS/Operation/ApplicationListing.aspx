<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="ApplicationListing.aspx.cs" Inherits="eSTS.Operation.ApplicationListing" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <div class="alert alert-danger" id="error_alert" style="display: none">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
        <h4><i class="icon fa fa-check"></i>Alert!</h4>
        <dx:ASPxLabel ID="lblErrMsg" runat="server" Text="" CssClass="description" EnableViewState="False">
        </dx:ASPxLabel>
    </div>
    <div class="row">
        <asp:HiddenField runat="server" ID="hfApplicationID" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hfApplicationFlowID" ClientIDMode="Static" />
    </div>
     <div class="row">
        <div class="col-lg-12">
            <div class="form-group row">
                <label class="col-sm-2 col-form-label">Operation Date/Time</label>
                <div class="col-sm-3">
                    <dx:BootstrapDateEdit ID="dtOperationDateFrom" DisplayFormatString="dd/MM/yyyy"  EditFormatString="dd/MM/yyyy" EditFormat="Custom" runat="server">
                        <ClientSideEvents ValueChanged="function(s, e) {
	gridActive.Refresh();
}" />
                    </dx:BootstrapDateEdit>
                </div>
                <label class="col-sm-1 col-form-label">To</label>
                <div class="col-sm-3">
                    <dx:BootstrapDateEdit ID="dtOperationDateTo" DisplayFormatString="dd/MM/yyyy"  EditFormatString="dd/MM/yyyy" EditFormat="Custom" runat="server"> <ClientSideEvents ValueChanged="function(s, e) {
	gridActive.Refresh();
}" /></dx:BootstrapDateEdit>
                </div>
               
            </div>
        </div>

    </div>
     <%--<div class="row">
        <div class="col-lg-12">
            <div class="form-group row">
                <label class="col-sm-2 col-form-label">Status</label>
                <div class="col-sm-5">
                    <dx:BootstrapComboBox ID="cbStatus" runat="server"  class="form-control required">
                        <Items>
                            <dx:BootstrapListEditItem Text="All" Value="0">
                            </dx:BootstrapListEditItem>
                            <dx:BootstrapListEditItem Text="Pending Verification" Value="1">
                            </dx:BootstrapListEditItem>
                            <dx:BootstrapListEditItem Text="Pending Payment" Value="2">
                            </dx:BootstrapListEditItem>
                             <dx:BootstrapListEditItem Text="Pending Location" Value="3">
                            </dx:BootstrapListEditItem>
                            <dx:BootstrapListEditItem Text="Pending BDN" Value="4">
                            </dx:BootstrapListEditItem>
                              <dx:BootstrapListEditItem Text="Pending BDN Exceeding 72hrs" Value="5">
                            </dx:BootstrapListEditItem>
                            <dx:BootstrapListEditItem Text="Completed" Value="6">
                            </dx:BootstrapListEditItem>
                        </Items>
                         <ClientSideEvents ValueChanged="function(s, e) {
	grid.Refresh();
}" />
                        </dx:BootstrapComboBox>
                    </div>
                 <div class="col-sm-2" style="padding: 0">
                    <dx:BootstrapButton ID="btnSearch" CssClasses-Icon="fa fa-search" runat="server" AutoPostBack="False" Text="Search" Visible="False">
                        <SettingsBootstrap RenderOption="Warning" />
                        <CssClasses Icon="fa fa-search"></CssClasses>

                        <ClientSideEvents Click="function(s, e) {
	grid.Refresh();
}" />
                    </dx:BootstrapButton>
                </div>
                </div>
            </div>
         </div>--%>
    <div class="row">
        <div class="col-lg-12">
            <div class="form-group row">
                <label class="col-sm-2 col-form-label">Method</label>
                <div class="col-sm-5">
                    <dx:BootstrapComboBox ID="cbMethod" runat="server"  class="form-control required">
                        <Items>
                            <dx:BootstrapListEditItem Text="All" Value="0" Selected="true">
                            </dx:BootstrapListEditItem>
                            <dx:BootstrapListEditItem Text="Vessel Supplier to FSU" Value="1">
                            </dx:BootstrapListEditItem>
                            <dx:BootstrapListEditItem Text="FSU to Vessel" Value="2">
                            </dx:BootstrapListEditItem>
                        </Items>
                         <ClientSideEvents ValueChanged="function(s, e) {
	gridActive.Refresh();
}" />
                        </dx:BootstrapComboBox>
                    </div>
                 <div class="col-sm-2" style="padding: 0">
                    <dx:BootstrapButton ID="btnSearch" CssClasses-Icon="fa fa-search" runat="server" AutoPostBack="False" Text="Search" Visible="False">
                        <SettingsBootstrap RenderOption="Warning" />
                        <CssClasses Icon="fa fa-search"></CssClasses>

                        <ClientSideEvents Click="function(s, e) {
	gridActive.Refresh();
}" />
                    </dx:BootstrapButton>
                </div>
                </div>
            </div>
         </div>
    <div class="row">
        <div class="col-lg-12">

                      
                      <asp:EntityDataSource ID="dsOperationApp" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="v_OperationApp" OnSelecting="dsOperationApp_Selecting" OrderBy="it.[EstOperationDateTime] desc" Select="it.[OperationAppID], it.[SupplyMethodID], it.[CaseNum], it.[DeliveryLocation], it.[VSLatDegree], it.[VSLatMin], it.[VSLongDegree], it.[VSLongMin], it.[VRLatDegree], it.[VRLatMin], it.[VRLongDegree], it.[VRLongMin], it.[VSIMONo], it.[VSName], it.[VSFlag], it.[VSPortReg], it.[VSLOA], it.[VSGRT], it.[VSNRT], it.[VRLOA], it.[VRGRT], it.[VRNRT], it.[VRMMSINo], it.[UOMCode], it.[EstOilMT], it.[EstOperationDateTime], it.[EstOperationTime], it.[OilTypeDesc], it.[ActionStatus], it.[LabelColor], it.[VRIMONo], it.[VRName], it.[VRFlag], it.[VRPortReg], it.[CompanyName],it.[SOCompanyName],it.[IsPayment], it.[PermitDocLink], it.[PaymentDate], it.[PaymentDate], it.[PaymentTime], it.[PaymentAmount], it.[ReceiptNo], it.[ReceiptNo], it.[PaymentRefID], it.[NPDocLink], it.[Lampiran1DocLink], it.[FlowActionStatusID], it.[IsSubmitCM],it.[MethodCode],it.[MedhodName],it.[VSLatitude],it.[VSLongitude],it.[VRLatitude],it.[VRLongitude]" >
                        </asp:EntityDataSource>
                      
                      <dx:BootstrapGridView ID="gridActive" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridActive" DataSourceID="dsOperationApp" KeyFieldName="OperationAppID" EnableTheming="False" OnHtmlDataCellPrepared="gridActive_HtmlDataCellPrepared">
                            <SettingsAdaptivity AdaptivityMode="HideDataCells">
                            </SettingsAdaptivity>
                            <SettingsBehavior ConfirmDelete="True" />
                            <SettingsCommandButton>
                                <EditButton IconCssClass="fa fa-edit text-success" Text=" " />
                                <DeleteButton IconCssClass="fa fa-trash text-danger" Text=" " />
                            </SettingsCommandButton>
                            <Columns>
                                <dx:BootstrapGridViewTextColumn FieldName="OperationAppID" Caption="#" VisibleIndex="0">
                                    <CssClasses DataCell="text-navy" />
                                    <DataItemTemplate>
                                        <dx:ASPxHyperLink ID="hyperLink" runat="server" OnInit="lilView2_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                        <dx:ASPxHyperLink ID="linkPayment" runat="server" OnInit="lilPayment_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                        <%--  <dx:ASPxHyperLink ID="lilNotis" runat="server" OnInit="lilNotis_Init" Target="_self">
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
                                <dx:BootstrapGridViewTextColumn FieldName="CompanyName" VisibleIndex="3" Caption="Agent Name">
                                </dx:BootstrapGridViewTextColumn>
                                   <dx:BootstrapGridViewTextColumn FieldName="SOCompanyName" VisibleIndex="3" Caption="Operator Name">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="DeliveryLocation" Caption="Delivery Location" VisibleIndex="2" visible="false">
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="VSName"  Caption="Vessel FSU"  VisibleIndex="3" ReadOnly="True" >
                                </dx:BootstrapGridViewTextColumn>
                                 <dx:BootstrapGridViewTextColumn FieldName="VRName"  Caption="Vessel Supplier / Receiver"  VisibleIndex="4" ReadOnly="True" >
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
     <script type="text/javascript">
       
          function popUpPayment(id,  dtPayYear, dtPayMth, dtPayDay, timePayHour, timePayMin, paymentAmt, receiptNo, permitRef) {
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

              var sPayDate = new Date(dtPayYear, parseInt(dtPayMth)-1, parseInt(dtPayDay), 00, 00, 00);
              console.log(sPayDate);
              dtPaymentDate.SetValue(sPayDate);

              var sPayTime = new Date(dtPayYear, parseInt(dtPayMth), parseInt(dtPayDay), timePayHour, timePayMin, 00);
              paymentTime.SetValue(sPayTime);

              txtPaymentAmt.SetValue(paymentAmt);
              txtReceiptNo.SetValue(receiptNo);
              txtPermitRef.SetValue(permitRef);
              pcPayment.ShowWindow(pcPayment.GetWindow(0));

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
         function popUpCancel(id) {
             // alert(id);
             $("#hfApplicationID").val(id);

             pcCancel.ShowWindow();

         }
         function showRejectReason(reason) {
             txtRejectReason.SetValue(reason);
             pcRejectReason.ShowWindow();

         }
         function ShowPopup(title, body) {
             //    var x = "<a href='#' class='btn btn-danger  dim btn-large-dim'>" + body + "</a><h4>Pending Bunker Delivery Note</h4>";
             //var x = "<h4>eB system found <span class='label label-danger'>" + body + "</span> BDN still pending.  Please submit BDN information immediately to avoid any inconvenience for the next bunkering application.</h4>";
             var x = "<h4>Effectively by 1st August 2021, BDN must be submitted within 72 hours after expiry of petroleum permit for bunker operation issued by JLWS. Failure to do so, applications are not allowed</h4>";
             $("#myModal4 .modal-body").html(x);
             $("#myModal4").modal("show");
         }

    </script>
</asp:Content>
