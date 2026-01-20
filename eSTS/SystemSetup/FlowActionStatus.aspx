
<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="FlowActionStatus.aspx.cs" Inherits="eSTS.SystemSetup.FlowActionStatus" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox ">
                <%--<div class="ibox-title">
                            <h5>jQuery Grid Plugin – jqGrid</h5>
                        </div>--%>
                <div class="ibox-content">

                   

                    <asp:EntityDataSource ID="dsFlowActionStatus" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="FlowActionStatus" orderby="it.[ActionStatusSeq]" EnableDelete="True" EnableInsert="True" EnableUpdate="True">
                         </asp:EntityDataSource>
                    <dx:BootstrapGridView ID="grid" runat="server" AutoGenerateColumns="False" DataSourceID="dsFlowActionStatus" KeyFieldName="FlowActionStatusID" OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating" >
                          <SettingsAdaptivity AdaptivityMode="HideDataCells">
                            </SettingsAdaptivity>
                        <SettingsBehavior ConfirmDelete="True" />
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <SettingsCommandButton>
                            <EditButton IconCssClass="fa fa-edit text-success" Text=" " />
                            <DeleteButton IconCssClass="fa fa-trash text-danger" Text=" " />
                        </SettingsCommandButton>
                        <SettingsDataSecurity AllowDelete="True" AllowEdit="True" AllowInsert="True" />
                        <Columns>
                            <dx:BootstrapGridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                            </dx:BootstrapGridViewCommandColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="FlowActionStatusID" ReadOnly="True" Visible="False" VisibleIndex="1">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn Caption="Sequence" FieldName="ActionStatusSeq" VisibleIndex="3">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="ActionStatus" VisibleIndex="2">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewCheckColumn FieldName="IsHideFlow" VisibleIndex="4">
                            </dx:BootstrapGridViewCheckColumn>
                            <dx:BootstrapGridViewCheckColumn FieldName="IsActive" VisibleIndex="9">
                            </dx:BootstrapGridViewCheckColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="LabelColor" VisibleIndex="8">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="CreatedBy" Visible="False" VisibleIndex="10">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="CreatedDate" Visible="False" VisibleIndex="11">
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="UpdatedBy" Visible="False" VisibleIndex="12">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="UpdatedDate" Visible="False" VisibleIndex="13">
                            </dx:BootstrapGridViewDateColumn>
                        </Columns>
                    </dx:BootstrapGridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
