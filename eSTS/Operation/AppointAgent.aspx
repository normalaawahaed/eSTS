<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="AppointAgent.aspx.cs" Inherits="eSTS.Operation.AppointAgent" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function grid_SelectionChanged(s, e) {
            console.log('masuk');
            s.GetSelectedFieldValues("ShipName;ShipID", GetSelectedFieldValuesCallback);
        }
        function GetSelectedFieldValuesCallback(values) {
            console.log(" value " + values[0][0]);
            console.log(" value " + values[0][1]);

            txtShipName.SetText(values[0][0]);
            $("#hfShipRecID").val(values[0][1]);

            txtSearchShipName.SetText('');
            txtIMONo.SetText('');
            txtOffNo.SetText('');

            pcSearchShip.PerformCallback();
            pcSearchShip.Hide();

        }
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
    <asp:HiddenField runat="server" ID="hfOpAppointAgentID" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hfCompID" ClientIDMode="Static" />
    <div class="wrapper wrapper-content animated fadeInRight">
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
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox ">
                    <div class="ibox-title">
                        <h3 class="box-title"></h3>
                        <h5>Appointment by STS Operator</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-group row">
                            <label class="col-sm-3 col-form-label">STS Operator <font color="red">*</font></label>
                            <div class="col-sm-6">
                                <dx:BootstrapComboBox ID="cbSO" runat="server" DataSourceID="dsSTSOperator" TextField="CompanyName" ValueField="Orgzid"  class="form-control required" TextFormatString="{1}-{0}">
                                    <Fields>
                                        <dx:BootstrapListBoxField FieldName="CompanyName" />
                                        <dx:BootstrapListBoxField FieldName="Orgzid" />
                                    </Fields>
                                    <ValidationSettings ValidationGroup="SaveBO">
                                        <RequiredField IsRequired="True" />
                                    </ValidationSettings>
                                </dx:BootstrapComboBox>
  <asp:EntityDataSource ID="dsSTSOperator" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="v_STSOperator"  Select="it.[Orgzid], it.[CompanyName]">
                                </asp:EntityDataSource>
                            </div>
                            <div class="col-sm-3">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 col-form-label">Start Date of Appointment <font color="red">*</font></label>
                            <div class="col-sm-2">
                                <dx:BootstrapDateEdit ID="dtFromDate" runat="server" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    <ValidationSettings ValidationGroup="SaveBO">
                                        <RequiredField IsRequired="True" />
                                    </ValidationSettings>
                                </dx:BootstrapDateEdit>
                            </div>
                            <div class="col-sm-7">
                            </div>

                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 col-form-label">End Date of Appointment <font color="red">*</font></label>
                            <div class="col-sm-2">
                                <dx:BootstrapDateEdit ID="dtToDate" runat="server" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    <ValidationSettings ValidationGroup="SaveBO">
                                        <RequiredField IsRequired="True" />
                                    </ValidationSettings>
                                </dx:BootstrapDateEdit>
                            </div>
                            <div class="col-sm-3">
                               

                            </div>
                            <div class="col-sm-4 text-left">
                                <dx:BootstrapButton ID="btnSaveBO" runat="server" AutoPostBack="true" Text="Save" OnClick="btnSaveBO_Click" ValidationGroup="SaveBO">
                                    <CssClasses Icon="fa fa-save" />
                                    <SettingsBootstrap RenderOption="Success" />
                                </dx:BootstrapButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
            <div id="divUpload" runat="server" class="row">
                <div class="col-lg-12">
                    <div class="ibox ">
                        <div runat="server" class="ibox-title">
                            <h3 class="box-title"></h3>
                            <h5><i class="fa fa-upload"></i>Upload Letter of Appointment </h5>

                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-lg-10">
                                    <div class="form-group row">
                                        <label class="col-sm-3 text-sm-right">Attachment File <font color="red">*</font></label>
                                        <div class="col-sm-6">
                                            <dx:BootstrapUploadControl ID="uploadFile" runat="server">
                                                <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".jpg,.jpeg,.gif,.png,.pdf" />

                                            </dx:BootstrapUploadControl>
                                            <small>Allowed file extensions: .jpg, .jpeg, .gif, .png.</small>
                                            <br />
                                            <small>Maximum file size: 4 MB.</small>
                                        </div>
                                        <div class="col-sm-1">
                                            <dx:BootstrapButton ID="btnSaveFile" runat="server" Text="Upload Attach" EnableTheming="True" OnClick="btnSaveFile_Click" ValidationGroup="SaveFile">
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
                                                <asp:Literal ID="lilFile" runat="server"></asp:Literal>

                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>
            </div>
    </div>
</asp:Content>
