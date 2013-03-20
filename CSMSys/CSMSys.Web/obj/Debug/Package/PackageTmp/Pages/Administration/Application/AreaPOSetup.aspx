<%@ Page Title="CSMSys :: Area Setup" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" 
    CodeBehind="AreaPOSetup.aspx.cs" Inherits="CSMSys.Web.Pages.Administration.Application.AreaPOSetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">
        function ShowNewModal() {
            var frame = $get('IframeEdit');
            frame.src = "../../../Controls/Administration/Application/AreaPO.aspx?UIMODE=NEW&DID=0";
            $find('EditModalPopup').show();
        }
        function ShowViewModal(AreaPOID) {
            var frame = $get('IframeEdit');
            frame.src = "../../../Controls/Administration/Application/AreaPO.aspx?UIMODE=VIEW&DID=" + AreaPOID;
            $find('EditModalPopup').show();
        }
        function ShowEditModal(AreaPOID) {
            var frame = $get('IframeEdit');
            frame.src = "../../../Controls/Administration/Application/AreaPO.aspx?UIMODE=EDIT&DID=" + AreaPOID;
            $find('EditModalPopup').show();
        }
        function EditCancelScript() {
            var frame = $get('IframeEdit');
            frame.src = "../../../Controls/Loading.aspx";
        }
        function EditOkayScript() {
            RefreshDataGrid();
            EditCancelScript();
        }
        function RefreshDataGrid() {
            $get('imgRefresh').click();
        }
        function NewDataOkay() {
            $get('imgRefresh').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="title">
        <h2>Area/Police Station/Upazila setup</h2>
    </div>
    <div class="feature-box-full">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%" border="0" cellpadding="2" cellspacing="4">
			        <tbody>
			        <tr>
                        <td align="right" style="width:47%;">
                            &nbsp;
                        </td>
                        <td align="right" style="width:44%;">
                            Search by Area/Police Station/Upazila Name : <asp:TextBox ID="txtSearch" runat="server" ></asp:TextBox>
                        </td>
                        <td align="center" style="width:3%;">
                            <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png" ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                        </td>
                        <td align="center" style="width:3%;">
                            <asp:ImageButton ID="imgRefresh" runat="server" CommandName="Refresh" ImageUrl="~/App_Themes/Default/Images/gridview/Refresh.png" ToolTip="Refresh" Width="16px" Height="16px" OnClick="imgRefresh_Click" />
                        </td>
                        <td align="center" style="width:3%;">
                            <asp:ImageButton ID="imgNew" runat="server" CommandName="New" ImageUrl="~/App_Themes/Default/Images/gridview/New.png" ToolTip="New" Width="16px" Height="16px" OnClick="imgNew_Click" />
                        </td>
                    </tr>
                    </tbody>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
			        <tbody>
			        <tr>
				        <td align="left">
                            <asp:GridView ID="grvAreaPO" DataKeyNames="AreaPOID" runat="server" Width="100%" AutoGenerateColumns="False"
                                CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvAreaPO_PageIndexChanging" ShowHeaderWhenEmpty="true" 
                                OnRowDataBound="grvAreaPO_RowDataBound" OnRowDeleting="grvAreaPO_RowDeleting"
                                EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True" PageSize="15" DataSourceID="dsAreaPO">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSl" Text='<%# Eval("SlNo") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AreaPOID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAreaPOID" Text='<%# Eval("AreaPOID") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UpazilaPSID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUpazilaPSID" Text='<%# Eval("UpazilaPSID") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAreaPOCode" Text='<%# Eval("AreaPOCode") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAreaPOName" Text='<%# HighlightText(Eval("AreaPOName").ToString()) %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UpazilaPS" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUpazilaPSName" Text='<%# Eval("UpazilaPSName") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrictName" Text='<%# Eval("DistrictName") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgView" runat="server" CommandName="View" ImageUrl="~/App_Themes/Default/Images/gridview/View.png" ToolTip="View" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgEdit" runat="server" CommandName="Edit" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png" ToolTip="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgDelete" runat="server" CommandName="Delete" OnClientClick='return confirm("Are you sure you want to Delete?");' ImageUrl="~/App_Themes/Default/Images/gridview/Delete.png" ToolTip="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" Font-Bold="true" Font-Underline="false" BackColor="#e6EEEE" />
                                <AlternatingRowStyle BackColor="#E5EAE8" />
                                <EditRowStyle BackColor="#999999" />
                                <EmptyDataRowStyle ForeColor="#CC0000" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="dsAreaPO" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>" 
                                SelectCommand="SELECT ROW_NUMBER() OVER (ORDER BY ADMAreaPO.AreaPOID) As SlNo, ADMAreaPO.AreaPOID, ADMAreaPO.UpazilaPSID, ADMUpazilaPS.UpazilaPSName, ADMUpazilaPS.UpazilaPSName, ADMAreaPO.AreaPOCode, ADMAreaPO.AreaPOCode, 
                                    ADMAreaPO.AreaPOName, ADMAreaPO.AreaPOName, ADMAreaPO.Description, ADMAreaPO.Description, ADMDistrict.DistrictName, ADMDistrict.DistrictName, ADMAreaPO.CreatedBy, ADMAreaPO.CreatedDate, ADMAreaPO.ModifiedBy, ADMAreaPO.ModifiedDate
                                    FROM ADMAreaPO INNER JOIN
                                    ADMUpazilaPS ON ADMAreaPO.UpazilaPSID = ADMUpazilaPS.UpazilaPSID INNER JOIN
                                    ADMDistrict ON ADMUpazilaPS.DistrictID = ADMDistrict.DistrictID" FilterExpression="AreaPOName LIKE '%{0}%'">
                                <FilterParameters>
                                    <asp:ControlParameter Name="AreaPOName" ControlID="txtSearch" PropertyName="Text" />
                                </FilterParameters>
                            </asp:SqlDataSource>
                        </td>
			        </tr>
			        </tbody>
		        </table>
            </ContentTemplate>
            <Triggers>
                <%--<asp:PostBackTrigger ControlID="imgRefresh" />--%>
                <asp:AsyncPostBackTrigger ControlID="imgRefresh" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <asp:Button ID="ButtonEdit" runat="server" Text="Submit" style="display:none" />
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" BackgroundCssClass="ModalPopupBG"
        runat="server" CancelControlID="ButtonEditCancel" OkControlID="ButtonEditDone" 
        TargetControlID="ButtonEdit" PopupControlID="DivEditWindow" 
        OnCancelScript="EditCancelScript();" OnOkScript="EditOkayScript();"
        BehaviorID="EditModalPopup">
    </cc1:ModalPopupExtender>
    <div class="popup_Buttons" style="display: none">
        <input id="ButtonEditDone" value="Done" type="button" />
        <input id="ButtonEditCancel" value="Cancel" type="button" />
    </div>
    <div id="DivEditWindow" style="display: none;" class="popupAreaPO">
        <iframe id="IframeEdit" frameborder="0" width="450px" height="248px" scrolling="no">
        </iframe>
    </div>
</asp:Content>
