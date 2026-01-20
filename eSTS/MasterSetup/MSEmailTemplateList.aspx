<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="MSEmailTemplateList.aspx.cs" Inherits="eSTS.MasterSetup.MSEmailTemplateList" %>
 
<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12 text-right" runat="server">
            <dx:BootstrapButton ID="btnNew" runat="server" AutoPostBack="True" ToolTip="Add New Email Template" Text="New Email Template" ClientInstanceName="btnNew" OnClick="btnNew_Click">
                <CssClasses Icon="fa fa-plus" />
                <SettingsBootstrap RenderOption="Success" />
            </dx:BootstrapButton>
        </div>
    </div>
    <div class="row">

        <div class="col-lg-12">
            <div class="ibox ">
                <%--<div class="ibox-title">
                            <h5>jQuery Grid Plugin – jqGrid</h5>
                        </div>--%>
                <div class="ibox-content">

                    <asp:EntityDataSource ID="dsEmailTemplateList" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="EmailTemplates" OrderBy="it.[TemplateCode]">
                    </asp:EntityDataSource>
                    <dx:BootstrapGridView ID="grid" runat="server" AutoGenerateColumns="False" DataSourceID="dsEmailTemplateList" KeyFieldName="EmailTempID" >
                          <SettingsAdaptivity AdaptivityMode="HideDataCells">
                            </SettingsAdaptivity>
                        <Settings ShowFilterRow="True" />
                        <SettingsBehavior ConfirmDelete="True" />
                        <SettingsCommandButton>
                            <EditButton IconCssClass="fa fa-edit text-success" Text=" " />
                            <DeleteButton IconCssClass="fa fa-trash text-danger" Text=" " />
                        </SettingsCommandButton>
                        <SettingsDataSecurity AllowDelete="True" />
                        <Columns>
                            <dx:BootstrapGridViewCommandColumn VisibleIndex="0">
                            </dx:BootstrapGridViewCommandColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="EmailTempID" Caption="Action" ReadOnly="True" VisibleIndex="1" >
                                <DataItemTemplate>
                                        <dx:ASPxHyperLink ID="lilNew" runat="server" OnInit="lilNew_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                         <dx:ASPxHyperLink ID="lilEdit" runat="server" OnInit="lilEdit_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                         
                                    </DataItemTemplate>
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="TemplateCode" VisibleIndex="2">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="TempSubject" VisibleIndex="3" Caption="Template Subject">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn Caption="Template Body" FieldName="TempBody" Visible="false" VisibleIndex="4">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="CreatedDate" Visible="False" VisibleIndex="5">
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="CreatedBy" Visible="False" VisibleIndex="6">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="UpdateDate" Visible="False" VisibleIndex="7">
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="UpdateBy" Visible="False" VisibleIndex="8">
                            </dx:BootstrapGridViewTextColumn>
                        </Columns>
                        <SettingsSearchPanel Visible="True" />
                    </dx:BootstrapGridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
