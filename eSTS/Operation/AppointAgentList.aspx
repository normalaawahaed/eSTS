<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="AppointAgentList.aspx.cs" Inherits="eSTS.Operation.AppointAgentList" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-content">
                    <asp:EntityDataSource ID="dsAppointAgent" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="v_AppointAgentNew" Where="it.[SACompID]=@pCompID" OrderBy="it.[AppointStartDate]">
                        <WhereParameters>
                            <asp:SessionParameter DbType="String" Name="pCompID" SessionField="CompID" />
                        </WhereParameters>
                    </asp:EntityDataSource>
                    <dx:BootstrapGridView ID="grid" runat="server" AutoGenerateColumns="False" DataSourceID="dsAppointAgent" KeyFieldName="OpAppointAgentID">
                         <SettingsAdaptivity AdaptivityMode="HideDataCells">
                            </SettingsAdaptivity>
                        <Columns>
                            <dx:BootstrapGridViewHyperLinkColumn Caption="#" FieldName="OpAppointAgentID" HorizontalAlign="Center" Name="#" ShowUrlAsDisplayText="False" VisibleIndex="2" Width="5px">
                                  <DataItemTemplate>
                                        <dx:ASPxHyperLink ID="lilEdit" runat="server" OnInit="lilEdit_Init" Target="_self">
                                        </dx:ASPxHyperLink>
                                    </DataItemTemplate>
                            </dx:BootstrapGridViewHyperLinkColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="SACompID" VisibleIndex="1" Visible="False">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="SOCompName" VisibleIndex="4" Caption="STS Operator">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn Caption="ROC No" FieldName="SOCompID" VisibleIndex="3">
                            </dx:BootstrapGridViewTextColumn>
                            
                            <dx:BootstrapGridViewDateColumn FieldName="AppointStartDate" VisibleIndex="6" Caption="Start Date of Appointment">
                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                </PropertiesDateEdit>
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="AppointEndDate" VisibleIndex="7" Caption="End Date of Appointment">
                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                </PropertiesDateEdit>
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="CreatedBy" Visible="False" VisibleIndex="8">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="CreatedDate" Visible="False" VisibleIndex="9">
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="UpdatedBy" Visible="False" VisibleIndex="10">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="UpdatedDate" Visible="False" VisibleIndex="11">
                            </dx:BootstrapGridViewDateColumn>
                        </Columns>
                    </dx:BootstrapGridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

