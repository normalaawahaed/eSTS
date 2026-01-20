<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInspinia.Master" AutoEventWireup="true" CodeBehind="MSEmailTemplate.aspx.cs" Inherits="eSTS.MasterSetup.MSEmailTemplate" %>

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
    <asp:HiddenField runat="server" ID="hfEmailTempID" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hfMethod" ClientIDMode="Static" />
                        <h5><i class="fa fa-envelope"></i> Email Template</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="col-sm-2 text-sm-right">
                                <dt>Email Template Code :
                                  
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
                                <dt>Subject :</dt>
                            </div>
                            <div class="col-sm-8 text-sm-left">
                                <dd class="mb-1">
                                    <dx:BootstrapTextBox ID="txtSubject" runat="server"></dx:BootstrapTextBox>
                                </dd>
                            </div>
                            <div class="col-sm-2 text-sm-left">
                                <dx:BootstrapButton ID="btnSave" runat="server" AutoPostBack="true" Text="Save" EnableTheming="True" OnClick="btnSave_Click">
                                    <CssClasses Icon="fa fa-save" />
                                    <SettingsBootstrap RenderOption="Success" />
                                </dx:BootstrapButton>
                            </div>
                        </div>
                        <div class="row">
                            <dx:ASPxHtmlEditor ID="EmailEditor" runat="server" Width="100%">
                                <Toolbars>
                                    <dx:HtmlEditorToolbar>
                                        <Items>
                                            <dx:ToolbarCutButton>
                                            </dx:ToolbarCutButton>
                                            <dx:ToolbarCopyButton>
                                            </dx:ToolbarCopyButton>
                                            <dx:ToolbarPasteButton>
                                            </dx:ToolbarPasteButton>
                                            <dx:ToolbarPasteFromWordButton>
                                            </dx:ToolbarPasteFromWordButton>
                                            <dx:ToolbarUndoButton BeginGroup="True">
                                            </dx:ToolbarUndoButton>
                                            <dx:ToolbarRedoButton>
                                            </dx:ToolbarRedoButton>
                                            <dx:ToolbarRemoveFormatButton BeginGroup="True">
                                            </dx:ToolbarRemoveFormatButton>
                                            <dx:ToolbarSuperscriptButton BeginGroup="True">
                                            </dx:ToolbarSuperscriptButton>
                                            <dx:ToolbarSubscriptButton>
                                            </dx:ToolbarSubscriptButton>
                                            <dx:ToolbarInsertOrderedListButton BeginGroup="True">
                                            </dx:ToolbarInsertOrderedListButton>
                                            <dx:ToolbarInsertUnorderedListButton>
                                            </dx:ToolbarInsertUnorderedListButton>
                                            <dx:ToolbarIndentButton BeginGroup="True">
                                            </dx:ToolbarIndentButton>
                                            <dx:ToolbarOutdentButton>
                                            </dx:ToolbarOutdentButton>
                                            <dx:ToolbarInsertLinkDialogButton BeginGroup="True">
                                            </dx:ToolbarInsertLinkDialogButton>
                                            <dx:ToolbarUnlinkButton BeginGroup="True">
                                            </dx:ToolbarUnlinkButton>
                                            <dx:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                                <Items>
                                                    <dx:ToolbarInsertTableDialogButton BeginGroup="True" Text="Insert Table..." ToolTip="Insert Table...">
                                                    </dx:ToolbarInsertTableDialogButton>
                                                    <dx:ToolbarTablePropertiesDialogButton BeginGroup="True">
                                                    </dx:ToolbarTablePropertiesDialogButton>
                                                    <dx:ToolbarTableRowPropertiesDialogButton>
                                                    </dx:ToolbarTableRowPropertiesDialogButton>
                                                    <dx:ToolbarTableColumnPropertiesDialogButton>
                                                    </dx:ToolbarTableColumnPropertiesDialogButton>
                                                    <dx:ToolbarTableCellPropertiesDialogButton>
                                                    </dx:ToolbarTableCellPropertiesDialogButton>
                                                    <dx:ToolbarInsertTableRowAboveButton BeginGroup="True">
                                                    </dx:ToolbarInsertTableRowAboveButton>
                                                    <dx:ToolbarInsertTableRowBelowButton>
                                                    </dx:ToolbarInsertTableRowBelowButton>
                                                    <dx:ToolbarInsertTableColumnToLeftButton>
                                                    </dx:ToolbarInsertTableColumnToLeftButton>
                                                    <dx:ToolbarInsertTableColumnToRightButton>
                                                    </dx:ToolbarInsertTableColumnToRightButton>
                                                    <dx:ToolbarSplitTableCellHorizontallyButton BeginGroup="True">
                                                    </dx:ToolbarSplitTableCellHorizontallyButton>
                                                    <dx:ToolbarSplitTableCellVerticallyButton>
                                                    </dx:ToolbarSplitTableCellVerticallyButton>
                                                    <dx:ToolbarMergeTableCellRightButton>
                                                    </dx:ToolbarMergeTableCellRightButton>
                                                    <dx:ToolbarMergeTableCellDownButton>
                                                    </dx:ToolbarMergeTableCellDownButton>
                                                    <dx:ToolbarDeleteTableButton BeginGroup="True">
                                                    </dx:ToolbarDeleteTableButton>
                                                    <dx:ToolbarDeleteTableRowButton>
                                                    </dx:ToolbarDeleteTableRowButton>
                                                    <dx:ToolbarDeleteTableColumnButton>
                                                    </dx:ToolbarDeleteTableColumnButton>
                                                </Items>
                                            </dx:ToolbarTableOperationsDropDownButton>
                                            <dx:ToolbarExportDropDownButton BeginGroup="True">
                                                <Items>
                                                    <dx:ToolbarExportDropDownItem>
                                                    </dx:ToolbarExportDropDownItem>
                                                    <dx:ToolbarExportDropDownItem Format="Pdf">
                                                    </dx:ToolbarExportDropDownItem>
                                                    <dx:ToolbarExportDropDownItem Format="Txt">
                                                    </dx:ToolbarExportDropDownItem>
                                                    <dx:ToolbarExportDropDownItem Format="Docx">
                                                    </dx:ToolbarExportDropDownItem>
                                                    <dx:ToolbarExportDropDownItem Format="Odt">
                                                    </dx:ToolbarExportDropDownItem>
                                                    <dx:ToolbarExportDropDownItem Format="Mht">
                                                    </dx:ToolbarExportDropDownItem>
                                                </Items>
                                            </dx:ToolbarExportDropDownButton>
                                            <dx:ToolbarPrintButton ToolTip="">
                                            </dx:ToolbarPrintButton>
                                            <dx:ToolbarFullscreenButton BeginGroup="True">
                                            </dx:ToolbarFullscreenButton>
                                            <dx:ToolbarFindAndReplaceDialogButton BeginGroup="True">
                                            </dx:ToolbarFindAndReplaceDialogButton>
                                        </Items>
                                    </dx:HtmlEditorToolbar>
                                    <dx:HtmlEditorToolbar>
                                        <Items>
                                            <dx:ToolbarParagraphFormattingEdit Width="120px">
                                                <Items>
                                                    <dx:ToolbarListEditItem Text="Normal" Value="p" />
                                                    <dx:ToolbarListEditItem Text="Heading  1" Value="h1" />
                                                    <dx:ToolbarListEditItem Text="Heading  2" Value="h2" />
                                                    <dx:ToolbarListEditItem Text="Heading  3" Value="h3" />
                                                    <dx:ToolbarListEditItem Text="Heading  4" Value="h4" />
                                                    <dx:ToolbarListEditItem Text="Heading  5" Value="h5" />
                                                    <dx:ToolbarListEditItem Text="Heading  6" Value="h6" />
                                                    <dx:ToolbarListEditItem Text="Address" Value="address" />
                                                    <dx:ToolbarListEditItem Text="Normal (DIV)" Value="div" />
                                                </Items>
                                            </dx:ToolbarParagraphFormattingEdit>
                                            <dx:ToolbarFontNameEdit>
                                                <Items>
                                                    <dx:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman" />
                                                    <dx:ToolbarListEditItem Text="Tahoma" Value="Tahoma" />
                                                    <dx:ToolbarListEditItem Text="Verdana" Value="Verdana" />
                                                    <dx:ToolbarListEditItem Text="Arial" Value="Arial" />
                                                    <dx:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif" />
                                                    <dx:ToolbarListEditItem Text="Courier" Value="Courier" />
                                                    <dx:ToolbarListEditItem Text="Segoe UI" Value="Segoe UI" />
                                                </Items>
                                            </dx:ToolbarFontNameEdit>
                                            <dx:ToolbarFontSizeEdit>
                                                <Items>
                                                    <dx:ToolbarListEditItem Text="1 (8pt)" Value="1" />
                                                    <dx:ToolbarListEditItem Text="2 (10pt)" Value="2" />
                                                    <dx:ToolbarListEditItem Text="3 (12pt)" Value="3" />
                                                    <dx:ToolbarListEditItem Text="4 (14pt)" Value="4" />
                                                    <dx:ToolbarListEditItem Text="5 (18pt)" Value="5" />
                                                    <dx:ToolbarListEditItem Text="6 (24pt)" Value="6" />
                                                    <dx:ToolbarListEditItem Text="7 (36pt)" Value="7" />
                                                </Items>
                                            </dx:ToolbarFontSizeEdit>
                                            <dx:ToolbarBoldButton BeginGroup="True">
                                            </dx:ToolbarBoldButton>
                                            <dx:ToolbarItalicButton>
                                            </dx:ToolbarItalicButton>
                                            <dx:ToolbarUnderlineButton>
                                            </dx:ToolbarUnderlineButton>
                                            <dx:ToolbarStrikethroughButton>
                                            </dx:ToolbarStrikethroughButton>
                                            <dx:ToolbarJustifyLeftButton BeginGroup="True">
                                            </dx:ToolbarJustifyLeftButton>
                                            <dx:ToolbarJustifyCenterButton>
                                            </dx:ToolbarJustifyCenterButton>
                                            <dx:ToolbarJustifyRightButton>
                                            </dx:ToolbarJustifyRightButton>
                                            <dx:ToolbarJustifyFullButton>
                                            </dx:ToolbarJustifyFullButton>
                                            <dx:ToolbarBackColorButton BeginGroup="True">
                                            </dx:ToolbarBackColorButton>
                                            <dx:ToolbarFontColorButton>
                                            </dx:ToolbarFontColorButton>
                                        </Items>
                                    </dx:HtmlEditorToolbar>
                                    <dx:HtmlEditorToolbar>
                                        <Items>
                                            <dx:ToolbarInsertImageDialogButton>
                                            </dx:ToolbarInsertImageDialogButton>
                                            <dx:ToolbarInsertFlashDialogButton>
                                            </dx:ToolbarInsertFlashDialogButton>
                                            <dx:ToolbarInsertAudioDialogButton>
                                            </dx:ToolbarInsertAudioDialogButton>
                                            <dx:ToolbarInsertVideoDialogButton>
                                            </dx:ToolbarInsertVideoDialogButton>
                                            <dx:ToolbarInsertYouTubeVideoDialogButton>
                                            </dx:ToolbarInsertYouTubeVideoDialogButton>
                                            <dx:ToolbarInsertPlaceholderDialogButton BeginGroup="True">
                                            </dx:ToolbarInsertPlaceholderDialogButton>
                                        </Items>
                                    </dx:HtmlEditorToolbar>
                                </Toolbars>
                                <Placeholders>
                                    <dx:HtmlEditorPlaceholderItem Value="Placeholder1" />
                                    <dx:HtmlEditorPlaceholderItem Value="Placeholder2" />
                                    <dx:HtmlEditorPlaceholderItem Value="Placeholder3" />
                                </Placeholders>
                                <Settings AllowCustomColorsInColorPickers="True" AllowScriptExecutionInPreview="True" ShowTagInspector="True">
                                    <SettingsHtmlView EnableAutoCompletion="True" HighlightActiveLine="True" HighlightMatchingTags="True" ShowCollapseTagButtons="True" ShowLineNumbers="True" />
                                </Settings>
<SettingsHtmlEditing AllowEditFullDocument="True" AllowFormElements="True" AllowHTML5MediaElements="True" AllowIFrames="True" AllowObjectAndEmbedElements="True" AllowScripts="True" AllowYouTubeVideoIFrames="True" EnablePasteOptions="True" EnterMode="BR" PasteMode="MergeFormatting">
<PasteFiltering Attributes="class"></PasteFiltering>
</SettingsHtmlEditing>
                                <SettingsResize AllowResize="True" />
                            </dx:ASPxHtmlEditor>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
