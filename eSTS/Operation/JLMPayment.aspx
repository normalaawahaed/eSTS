<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="JLMPayment.aspx.cs" Inherits="eSTS.Operation.JLMPayment" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <asp:HiddenField runat="server" ID="hfApplicationID" ClientIDMode="Static" />
       <asp:HiddenField runat="server" ID="hfMethod" ClientIDMode="Static" />
    <div>
        <div class="alert alert-success" id="success_alert" style="display: none">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <h4><i class="icon fa fa-check"></i>Alert!</h4>
            Record submit successfully.
        </div>
        <div class="alert alert-danger" id="error_alert" style="display: none">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <h4><i class="icon fa fa-check"></i>Alert!</h4>
            <dx:ASPxLabel ID="lblErrMsg" runat="server" Text="" CssClass="description" EnableViewState="False">
            </dx:ASPxLabel>
        </div>
        <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" ClientInstanceName="lPanel" Modal="true" runat="server"></dx:ASPxLoadingPanel>
        <div class="form-group row">
            <label class="col-sm-2 col-form-label">Payment Date</label>
            <div class="col-lg-2">
                <dx:BootstrapDateEdit ID="dtPaymentDate" ClientInstanceName="dtPaymentDate" runat="server"   DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" EditFormat="Custom"></dx:BootstrapDateEdit>
            </div>
             <div class="col-lg-8">
                 </div>
        </div>
        <div class="form-group row">
            <label class="col-sm-2 col-form-label">Payment Time</label>
            <div class="col-lg-2">
                <dx:BootstrapTimeEdit ID="paymentTime" ClientInstanceName="paymentTime" runat="server" DisplayFormatString="HH:mm" EditFormatString="HH:mm"></dx:BootstrapTimeEdit>

            </div>
              <div class="col-lg-8">
                 </div>
        </div>
        <div class="form-group row">
            <label class="col-sm-2 col-form-label">Payment Amount</label>
            <div class="col-lg-2">
                <dx:BootstrapTextBox ID="txtPaymentAmt" ClientInstanceName="txtPaymentAmt" runat="server" class="form-control required"  >
                    <MaskSettings Mask="&lt;0..99999g&gt;.&lt;00..99&gt;" />
                </dx:BootstrapTextBox>
            </div>
              <div class="col-lg-8">
                 </div>
        </div>
        <div class="form-group row">
            <label class="col-sm-2 col-form-label">Receipt No</label>
            <div class="col-lg-4">
                <dx:BootstrapTextBox ID="txtReceiptNo" ClientInstanceName="txtReceiptNo" runat="server" class="form-control required" MaxLength="50"  ></dx:BootstrapTextBox>
            </div>
              <div class="col-lg-6">
                 </div>
        </div>
        <div class="form-group row">
            <label class="col-sm-2 col-form-label">Permit Reference</label>
            <div class="col-lg-4">
                <dx:BootstrapTextBox ID="txtPermitRef" ClientInstanceName="txtPermitRef" runat="server" class="form-control required" MaxLength="50" ></dx:BootstrapTextBox>
            </div>
              <div class="col-lg-6">
                 </div>
        </div>
        <div class="row">
              <div class="col-lg-4">
                 </div>
            <div class="col-lg-1 text-right">
                 <dx:BootstrapButton ID="btnBack" runat="server" AutoPostBack="True" Text="Back" EnableTheming="True" OnClick="btnBack_Click">
                    <CssClasses Icon="fa fa-chevron-left" />
                    <SettingsBootstrap RenderOption="Default" />
                    </dx:BootstrapButton>
            </div>
            <div class="col-lg-1 text-right">
                <dx:BootstrapButton ID="btnUpdatePayment" runat="server" AutoPostBack="True" Text="Submit" EnableTheming="True" OnClick="btnUpdatePayment_Click">
                    <CssClasses Icon="fa fa-save" />
                    <SettingsBootstrap RenderOption="Success" />
                    </dx:BootstrapButton>
            </div>
              <div class="col-lg-6">
                 </div>
        </div>
        </div>
</asp:Content>
