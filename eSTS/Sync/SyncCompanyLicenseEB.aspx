<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="SyncCompanyLicenseEB.aspx.cs" Inherits="eSTS.Sync.SyncCompanyLicenseEB" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-content">
                    <asp:EntityDataSource ID="dsLicComp" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="v_LicCompany" OrderBy="it.[CompanyName]">
                    </asp:EntityDataSource>
                    <dx:BootstrapButton ID="btnSync" runat="server" AutoPostBack="false" OnClick="btnSync_Click" Text="Sync">
                    </dx:BootstrapButton>
                    <dx:BootstrapGridView ID="gridLicCompany" runat="server" AutoGenerateColumns="False" DataSourceID="dsLicComp" KeyFieldName="MMSCompLicID">
                          <SettingsAdaptivity AdaptivityMode="HideDataCells">
                            </SettingsAdaptivity>
                        <SettingsPager AlwaysShowPager="True">
                        </SettingsPager>
                        <Columns>
                            <dx:BootstrapGridViewCommandColumn VisibleIndex="0">
                            </dx:BootstrapGridViewCommandColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="MMSCompLicID" ReadOnly="True" VisibleIndex="1" Visible="False">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="CompID" VisibleIndex="2" Caption="ROC No.">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="CompanyName" VisibleIndex="3">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="LicenseID" Visible="False" VisibleIndex="4">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="LicDateIssue" Visible="False" VisibleIndex="5">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="LicDateExp" VisibleIndex="6">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="DtLicIssue" VisibleIndex="7" Visible="False">
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="DtLicExp" VisibleIndex="8">
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="Location" VisibleIndex="9">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="ServiceType" VisibleIndex="10">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="CaseNUm" VisibleIndex="11">
                            </dx:BootstrapGridViewTextColumn>
                        </Columns>
                        <SettingsSearchPanel Visible="True" />
                    </dx:BootstrapGridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
