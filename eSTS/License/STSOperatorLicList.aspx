<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="STSOperatorLicList.aspx.cs" Inherits="eSTS.License.STSOperatorLicList" %>


<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-content">
                    <asp:EntityDataSource ID="dsLicComp" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="v_LicCompanyActive" OrderBy="it.[CompanyName]" OnSelecting="dsLicComp_Selecting">
                    </asp:EntityDataSource>
                    <dx:BootstrapGridView ID="grid" runat="server" AutoGenerateColumns="False" DataSourceID="dsLicComp" KeyFieldName="LicCompanyID">
                        <Settings ShowFilterRow="True" />
                        <SettingsAdaptivity AdaptivityMode="HideDataCells">
                        </SettingsAdaptivity>
                        <SettingsBehavior ConfirmDelete="True" />
                        <Columns>

                            <dx:BootstrapGridViewHyperLinkColumn Caption="#" FieldName="LicCompanyID" HorizontalAlign="Center" Name="#" ShowUrlAsDisplayText="False" VisibleIndex="2" Width="5px">
                               <DataItemTemplate>
                                        <dx:ASPxHyperLink ID="lilView" runat="server" OnInit="lilView_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                    </DataItemTemplate>
                            </dx:BootstrapGridViewHyperLinkColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="LicCompanyID" VisibleIndex="1" Visible="False">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="CreatedBy" VisibleIndex="7" Visible="False">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="CreatedDate" VisibleIndex="8" Visible="False">
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="UpdatedBy" Visible="False" VisibleIndex="9">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="UpdatedDate" Visible="False" VisibleIndex="10">
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewTextColumn Caption="ROC No." FieldName="CompID" VisibleIndex="3">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="CompanyName" VisibleIndex="4" Caption="Company Name">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="ServiceCode" Visible="False" VisibleIndex="12">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="LicDateIssue" Visible="False" VisibleIndex="13">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="LicDateExp" Visible="False" VisibleIndex="14">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="DtLicExp" VisibleIndex="5" Caption="License Exp Date" Visible="false">
                                <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                </PropertiesDateEdit>
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="LicenseID" Visible="False" VisibleIndex="15">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="LOCATION" Visible="False" VisibleIndex="16">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="ServiceType" Visible="False" VisibleIndex="17">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="PortName" ReadOnly="True" VisibleIndex="6">
                            </dx:BootstrapGridViewTextColumn>
                        </Columns>
                    </dx:BootstrapGridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
