<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="UploadCMBL.aspx.cs" Inherits="eSTS.Operation.UploadCMBL" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function successAlert() {
            var elem = document.getElementById('success_alert');
            elem.style.display = 'block';
            //$('#success_alert').delay(800).fadeOut('slow');
        }
        function errorAlert() {
            console.log("masuk");
            var elem = document.getElementById('error_alert');
            elem.style.display = 'block';
            // $('#error_alert').delay(800).fadeOut('slow');
        }
    </script>
    <asp:HiddenField runat="server" ID="hfApplicationID" ClientIDMode="Static" />
    <div class="wrapper wrapper-content animated fadeInRight">
    <asp:HiddenField runat="server" ID="hfMethod" ClientIDMode="Static" />
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
        <div id="divCM" runat="server" class="row">
            <div class="col-lg-12">
                <div class="ibox ">
                    <div class="ibox-title">
                        <h3 class="box-title"></h3>
                        <h5><i class="fa fa-clipboard fa-lg"></i> Declaration of Ullage report / Surveyor report</h5>

                    </div>
                    <div class="ibox-content">
                         <div class="form-group row">
                            <label class="col-sm-3 text-sm-right"> ETA <font color="red">*</font></label>
                            <div class="col-sm-2">
                                <dx:BootstrapDateEdit ID="dtETA" ClientInstanceName="dtETA" runat="server" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" EditFormat="Custom"></dx:BootstrapDateEdit>
                            </div>
                            <label class="col-sm-1 text-sm-right"> ETD <font color="red">*</font></label>
                            <div class="col-sm-2">
                                <dx:BootstrapDateEdit ID="dtETD" ClientInstanceName="dtETD" runat="server" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" EditFormat="Custom"></dx:BootstrapDateEdit>
                            </div>
                            <div class="col-sm-2">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 text-sm-right"> Actual Operation Date/Time<font color="red">*</font></label>
                            <div class="col-sm-2">
                                <dx:BootstrapDateEdit ID="dtOperationDate" ClientInstanceName="dtOperationDate" runat="server" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" EditFormat="Custom"></dx:BootstrapDateEdit>
                            </div>
                            <div class="col-sm-2">
                                <dx:BootstrapTimeEdit ID="operationTime" ClientInstanceName="operationTime" runat="server" DisplayFormatString="HH:mm" EditFormatString="HH:mm"></dx:BootstrapTimeEdit>
                            </div>
                            <div class="col-sm-2">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 text-sm-right">Oil Quantity <font color="red">*</font></label>
                            <div class="col-sm-2">
                                <dx:BootstrapTextBox ID="txtMT" runat="server" class="form-control required">
                                                                      <MaskSettings Mask="&lt;0..999999g&gt;.&lt;00..99&gt;" />
                                </dx:BootstrapTextBox>
                            </div>
                            <label class="col-sm-1 text-sm-right">
                                Unit <font color="red">*</font>
                            </label>
                            <div class="col-sm-2">
                                <dx:BootstrapComboBox ID="cbUOM" runat="server" DataSourceID="dsUOM" TextField="UOMDesc" ValueField="UOMID" ValueType="System.Guid">
                                    <CssClasses Button="form-control m-b" />
                                </dx:BootstrapComboBox>
                                <asp:EntityDataSource ID="dsUOM" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" OrderBy="it.[UOMDesc]" EntitySetName="MSUOMs" EntityTypeFilter="" Select="it.[UOMID], it.[UOMCode], it.[UOMDesc], it.[IsActive]" Where="it.[IsActive]=true">
                                </asp:EntityDataSource>
                            </div>
                              <div class="col-sm-4">
                                  </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divUploadCM" runat="server" class="row">
            <div class="col-lg-12">
                <div class="ibox ">
                    <div runat="server" class="ibox-title">
                        <h3 class="box-title"></h3>
                        <h5><i class="fa fa-upload"></i> Upload Ullage report</h5>

                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="col-lg-10">
                                <div class="form-group row">
                                    <label class="col-sm-3 text-sm-right">Ullage report No.<font color="red">*</font></label>
                                    <div class="col-sm-5">
                                        <dx:BootstrapTextBox ID="txtCMNo" runat="server" MaxLength="30"></dx:BootstrapTextBox>
                                    </div>

                                </div>
                                <div class="form-group row"  runat="server" id="divAttachCM">
                                    <label class="col-sm-3 text-sm-right">Attachment File <font color="red">*</font></label>
                                    <div class="col-sm-6">
                                        <dx:BootstrapUploadControl ID="uploadFileCM" runat="server">
                                            <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".jpg,.jpeg,.gif,.png,.pdf" />

                                        </dx:BootstrapUploadControl>
                                        <small>Allowed file extensions: .jpg, .jpeg, .gif, .png,.pdf</small>
                                        <br />
                                        <small>Maximum file size: 4 MB.</small>

                                    </div>
                                 <div class="col-sm-1">
                                        <dx:BootstrapButton ID="btnSaveFileCM" runat="server" Text="Upload Attach" EnableTheming="True" OnClick="btnSaveFileCM_Click" ValidationGroup="SaveFileCM">
                                            <CssClasses Icon="fa fa-upload" />
                                            <SettingsBootstrap RenderOption="Dark" />
                                        </dx:BootstrapButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div id="divFileCM" runat="server" class="form-group row">
                                    <div class="file-box">
                                        <div class="file">
                                            <asp:Literal ID="lilFileCM" runat="server"></asp:Literal>

                                        </div>

                                    </div>
                                </div>
                            </div>
                            
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div id="divUploadBL" runat="server" class="row">
            <div class="col-lg-12">
                <div class="ibox ">
                    <div id="div1" runat="server" class="ibox-title">
                        <h3 class="box-title"></h3>
                        <h5><i class="fa fa-upload"></i> Upload Surveyor Report</h5>

                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="col-lg-10">
                                <div class="form-group row">
                                    <label class="col-sm-3 text-sm-right">Surveyor Report No. </label>
                                    <div class="col-sm-5">
                                        <dx:BootstrapTextBox ID="txtBLNo" runat="server" MaxLength="30"></dx:BootstrapTextBox>
                                    </div>

                                </div>
                                <div class="form-group row" runat="server" id="divAttachBL">
                                    <label class="col-sm-3 text-sm-right">Attachment File  </label>
                                    <div class="col-sm-6">
                                        <dx:BootstrapUploadControl ID="uploadFileBL" runat="server">
                                            <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".jpg,.jpeg,.gif,.png,.pdf" />

                                        </dx:BootstrapUploadControl>
                                        <small>Allowed file extensions: .jpg, .jpeg, .gif, .png,.pdf</small>
                                        <br />
                                        <small>Maximum file size: 4 MB.</small>

                                    </div>
                                           <div class="col-sm-1">
                                        <dx:BootstrapButton ID="btnSaveFileBL" runat="server" Text="Upload Attach" EnableTheming="True" OnClick="btnSaveFileBL_Click" ValidationGroup="SaveFileBL">
                                            <CssClasses Icon="fa fa-upload" />
                                            <SettingsBootstrap RenderOption="Dark" />
                                        </dx:BootstrapButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div id="divFileBL" runat="server" class="form-group row">
                                    <div class="file-box">
                                        <div class="file">
                                            <asp:Literal ID="lilFileBL" runat="server"></asp:Literal>

                                        </div>

                                    </div>
                                </div>
                            </div>
                           
                        </div>
                    </div>

                </div>
            </div>
        </div>

    
             
  <div class="row" id="divAck" runat="server">
            <div class="col-lg-12">
                <div class="ibox ">
                    <div class="ibox-title">
                        <h3 class="box-title"></h3>
                        <h5><i class="fa fa-check-square-o"></i> Company Acknowledgement & Integrity Clause</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-group row">
                            <div class="col-sm-10">
                                &nbsp;<dx:ASPxCheckBox ID="chkAck" runat="server" Text="I hereby / we acknowledged that all 
details of declaration information above mentioned is true,
I / we agree to notify LPJ immediately, if there any changes in the declaration information. I / we agree to comply rules and regulation enforced upon from time to time.. ">
                                    <ValidationSettings ValidationGroup="btnSubmit">
                                        <RequiredField IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxCheckBox>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-10">
                                &nbsp;<dx:ASPxCheckBox ID="chkIntegrity" runat="server" Text="By Checking the checkbox, you have agreed to the Integrity Clause By LPJ.">
                                    <ValidationSettings ValidationGroup="btnSubmit" SetFocusOnError="True">
                                        <RequiredField IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxCheckBox>
                                 <label data-toggle="modal" data-target="#myModal5" style="">
                                    <u style="text-decoration-color: blue">View Integrity Clause</u>
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    <div class="row">
       
        <div class="col-lg-1 text-right">
            <dx:BootstrapButton ID="btnBack" runat="server" AutoPostBack="True" Text="Back" EnableTheming="True" OnClick="btnBack_Click">
                <CssClasses Icon="fa fa-chevron-left" />
                <SettingsBootstrap RenderOption="Default" />
            </dx:BootstrapButton>
        </div>
        <div class="col-lg-1 text-right">
            <dx:BootstrapButton ID="btnSave" runat="server" Text="Submit" EnableTheming="True" OnClick="btnSave_Click">
                <CssClasses Icon="fa fa-location-arrow" />
                <SettingsBootstrap RenderOption="Primary" />
                <ClientSideEvents Click="function(s, e) {
       lPanel.Show();  
    e.processOnServer = true;  
	}" />
            </dx:BootstrapButton>
        </div>
      <div class="col-lg-10">
        </div>
    </div>
        </div>
 
</asp:Content>