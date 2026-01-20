<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="AccessGroupModule.aspx.cs" Inherits="eSTS.SystemSetup.AccessGroupModule" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <script type="text/javascript">
        function OnSelectionChanged(s, e) {
            //console(e.isSelected);
            //grid.GetRowValues(e.visibleIndex, "UniqueId", onGetValues(e.visible);

            //if (e.isSelected) {
                var key = s.GetRowKey(e.visibleIndex);
            //    alert('Key = ' + key);
            //}
                console.log(key);
                console.log(e.isSelected);
            grid.PerformCallback(key+"|"+e.isSelected);
        }
        function onGetValues(data) {
            console(data);
            
        }
    </script>

    <div class="row">
        <div class="col-lg-12">
            <div class="ibox ">
                <%--<div class="ibox-title">
                            <h5>jQuery Grid Plugin – jqGrid</h5>
                        </div>--%>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-3">
                            <label class="login2 pull-right pull-right-pro">Access Group</label>
                        </div>
                        <div class="col-lg-5">
                            <dx:BootstrapComboBox ID="cbAccessGroup" runat="server" DataSourceID="dsAccessGroup" TextField="AccessGroupDesc" TextFormatString="{0}" ValueField="AccessGroupID" NullValueItemDisplayText="{0}" ValueType="System.Guid">
                                <Fields>
                                    <dx:BootstrapListBoxField FieldName="AccessGroupDesc" />
                                </Fields>
                            </dx:BootstrapComboBox>
                        </div>
                        <div class="col-lg-1" style="padding-right: 0px">
                            <dx:BootstrapButton ID="btnShow" runat="server" AutoPostBack="false" Text="Show" OnClick="btnShow_Click">
                            </dx:BootstrapButton>
                        </div>
                        <div class="col-lg-3" style="padding-left: 5px">
                        </div>
                    </div>
                    <div class="row">
                         <div class="col-lg-12">
                             <div class="hr-line-dashed"></div>
                             <div class="form-group  row">
                    <asp:EntityDataSource ID="dsAccessModule" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="v_AccessGroupModule">
                    </asp:EntityDataSource>
                    <asp:EntityDataSource ID="dsAccessGroup" runat="server" ConnectionString="name=eSTS_StagEntities" DefaultContainerName="eSTS_StagEntities" EnableFlattening="False" EntitySetName="AccessGroups" OrderBy ="it.[AccessGroupName]">
                    </asp:EntityDataSource>
                    <dx:BootstrapGridView ID="grid" runat="server" AutoGenerateColumns="False"  OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating" KeyFieldName="UniqueId" OnDataBound="grid_DataBound" OnCustomCallback="gridGroupModule_CustomCallback" ClientInstanceName="grid">
                          <SettingsAdaptivity AdaptivityMode="HideDataCells">
                            </SettingsAdaptivity>
                        <Settings ShowFilterRow="True" ShowGroupPanel="True" />
                        <SettingsBehavior ConfirmDelete="True" />
                        <SettingsCommandButton>
                            <EditButton IconCssClass="fa fa-edit text-success" Text=" " />
                            <DeleteButton IconCssClass="fa fa-trash text-danger" Text=" " />
                        </SettingsCommandButton>
                        <SettingsDataSecurity AllowDelete="True" AllowEdit="True" AllowInsert="True" />
                        <SettingsBehavior AutoExpandAllGroups="True" AllowSelectByRowClick="True"  />
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                        <Columns>
                            <dx:BootstrapGridViewCommandColumn SelectAllCheckboxMode="Page" ShowSelectCheckbox="True" VisibleIndex="0">
                            </dx:BootstrapGridViewCommandColumn>
                              <dx:BootstrapGridViewTextColumn FieldName="UniqueId" VisibleIndex="1" Caption="No." Visible="false">
                    </dx:BootstrapGridViewTextColumn>
                           <%-- <dx:BootstrapGridViewTextColumn FieldName="AccessGroupModuleID" ReadOnly="True" VisibleIndex="1" Visible="False">
                            </dx:BootstrapGridViewTextColumn>--%>
                          <%--  <dx:BootstrapGridViewTextColumn FieldName="AccessGroupID" VisibleIndex="2" Visible="False">
                            </dx:BootstrapGridViewTextColumn>--%>
                            <dx:BootstrapGridViewTextColumn FieldName="lvl0ID" VisibleIndex="3" Visible="False">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="lvl0Code" Caption="Parent" VisibleIndex="4" GroupIndex="0" SortIndex="0" SortOrder="Ascending">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="lvl0DescDisp" VisibleIndex="5" Caption="Menu / Parent">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="lvl0Seq" VisibleIndex="6" Visible="False">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="lvl1ID" VisibleIndex="7" Visible="False" >
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="lvl1Desc" VisibleIndex="8" Caption="Level 1">
                            </dx:BootstrapGridViewTextColumn>
                          <%--  <dx:BootstrapGridViewTextColumn FieldName="lvl1DescDisp" VisibleIndex="9">
                            </dx:BootstrapGridViewTextColumn>--%>
                            <dx:BootstrapGridViewTextColumn FieldName="lvl1Seq" Visible="False" VisibleIndex="10">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="lvl2ID" Visible="False" VisibleIndex="11">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="lvl2Desc" VisibleIndex="12" Caption="Level 2">
                            </dx:BootstrapGridViewTextColumn>
                           <%-- <dx:BootstrapGridViewTextColumn FieldName="lvl2DescDisp" VisibleIndex="13">
                            </dx:BootstrapGridViewTextColumn>--%>
                            <dx:BootstrapGridViewTextColumn FieldName="lvl2Seq" Visible="False" VisibleIndex="14">
                            </dx:BootstrapGridViewTextColumn>
                        </Columns>
                         <ClientSideEvents SelectionChanged="OnSelectionChanged" />
                    </dx:BootstrapGridView>
                                 </div>
                   </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
