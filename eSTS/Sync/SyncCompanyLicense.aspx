<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="SyncCompanyLicense.aspx.cs" Inherits="eSTS.Sync.SyncCompanyLicense" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-content">
                    <asp:EntityDataSource ID="dsLicComp" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="v_LicCompany" OrderBy="it.[DtLicExp] desc">
                    </asp:EntityDataSource>
                    <dx:BootstrapButton ID="btnSync" runat="server" AutoPostBack="false" OnClick="btnSync_Click" Text="Sync">
                    </dx:BootstrapButton>
                    <dx:BootstrapGridView ID="gridLicCompany" runat="server" AutoGenerateColumns="False" DataSourceID="dsLicComp" KeyFieldName="MMSCompLicID">
                         <SettingsAdaptivity AdaptivityMode="HideDataCells">
                            </SettingsAdaptivity>
                         <SettingsPager AlwaysShowPager="True">
                        </SettingsPager>
                        <Columns>
                            <dx:BootstrapGridViewTextColumn FieldName="MMSCompLicID" ReadOnly="True" VisibleIndex="0" Visible="False">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="CompID" VisibleIndex="1" Caption="ROC No.">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="CompanyName" VisibleIndex="2">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="LicenseID" Visible="False" VisibleIndex="3">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="LicDateIssue" Visible="False" VisibleIndex="4">
                                      <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                </PropertiesDateEdit>
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="LicDateExp" Visible="False" VisibleIndex="5" >
                                      <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                </PropertiesDateEdit>
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="DtLicIssue" VisibleIndex="6" Visible="False">
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="DtLicExp" Caption="Lic. Date Exp." VisibleIndex="7">
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="VesselName" VisibleIndex="8">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="ServiceType" VisibleIndex="9">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="CaseNUm" VisibleIndex="10">
                            </dx:BootstrapGridViewTextColumn>
                        </Columns>
                        <SettingsSearchPanel Visible="True" />
                    </dx:BootstrapGridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
