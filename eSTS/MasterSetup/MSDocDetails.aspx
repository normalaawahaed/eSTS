<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="MSDocDetails.aspx.cs" Inherits="eSTS.MasterSetup.MSDocDetails" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
                        <asp:HiddenField runat="server" ID="hfMSDocTypeID" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hfModuleAttachID" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hfMethod" ClientIDMode="Static" />
                        <h5><i class="fa fa-envelope"></i> Supporting Document</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="col-sm-2 text-sm-right">
                                <dt>Document Code :
                                  
                                <dt></dt>
                            </div>
                            <div class="col-sm-4 text-sm-left">
                                <dd class="mb-1">
                                      <dx:BootstrapTextBox ID="txtCode" runat="server"></dx:BootstrapTextBox>
                                </dd>
                            
                            </div>
                            <div class="col-sm-6 text-sm-left">
                                   
                                </div> 
                        </div>
                        <div class="row">
                            <div class="col-sm-2 text-sm-right">
                                <dt>Document Description :</dt>
                            </div>
                            <div class="col-sm-8 text-sm-left">
                                <dd class="mb-1">
                                    <dx:BootstrapTextBox ID="txtDesc" runat="server"></dx:BootstrapTextBox>
                                </dd>
                            </div>
                           
                        </div>
                        <div class="row">
                             <div class="col-sm-2 text-sm-right">
                                <dt>Module :</dt>
                            </div>
                            <div class="col-sm-8 text-sm-left">
                                <dd class="mb-1">
                                    <dx:BootstrapComboBox ID="cbModule" runat="server">
                                       <Items>
                                        <dx:BootstrapListEditItem Text="STS Operator License" Value="STSOL">
                                        </dx:BootstrapListEditItem>
                                        <dx:BootstrapListEditItem Text="STS FSU License" Value="STSVL">
                                        </dx:BootstrapListEditItem>
                                        <dx:BootstrapListEditItem Text="STS Receiver/Supplier Vessel" Value="STSO">
                                        </dx:BootstrapListEditItem>
                                    </Items>
                                    </dx:BootstrapComboBox>
                                </dd>
                            </div>
                            </div>
                        <div class="row">
                             <div class="col-sm-2 text-sm-right">
                                <dt>Status :</dt>
                            </div>
                            <div class="col-sm-8 text-sm-left">
                                <dd class="mb-1">
                                    <dx:BootstrapCheckBox ID="chkStatus" runat="server">
                                    </dx:BootstrapCheckBox>
                                </dd>
                            </div>
                            <div class="col-sm-2 text-sm-left">
                                <dx:BootstrapButton ID="btnSave" runat="server" AutoPostBack="true" Text="Save" EnableTheming="True" OnClick="btnSave_Click">
                                    <CssClasses Icon="fa fa-save" />
                                    <SettingsBootstrap RenderOption="Success" />
                                </dx:BootstrapButton>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
