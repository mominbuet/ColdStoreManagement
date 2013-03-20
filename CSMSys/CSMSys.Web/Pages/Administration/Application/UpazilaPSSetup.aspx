<%@ Page Title="CSMSys :: Police Station/Upazila Setup" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" 
CodeBehind="UpazilaPSSetup.aspx.cs" Inherits="CSMSys.Web.Pages.Administration.Application.UpazilaPSSetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">
        function ShowNewModal() {
            var frame = $get('IframeEdit');
            frame.src = "../../../Controls/Administration/Application/UpazilaPS.aspx?UIMODE=NEW&UID=0";
            $find('EditModalPopup').show();
        }
        function ShowEditModal(UpazilaPSID, DistrictID, DivisionID) {
            var frame = $get('IframeEdit');
            frame.src = "../../../Controls/Administration/Application/UpazilaPS.aspx?UIMODE=EDIT&UID=" + UpazilaPSID + "&DID=" + DistrictID + "&VID=" + DivisionID;
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
        <h2>Police Station/Upazila setup</h2>
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
                            Search by Police Station/Upazila Name : <asp:TextBox ID="txtSearch" runat="server" ></asp:TextBox>
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
                            <asp:GridView ID="grvUpazilaPS" DataKeyNames="UpazilaPSID" runat="server" Width="100%" AutoGenerateColumns="False"
                                CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvUpazilaPS_PageIndexChanging" ShowHeaderWhenEmpty="true" 
                                OnRowDataBound="grvUpazilaPS_RowDataBound" OnRowDeleting="grvUpazilaPS_RowDeleting"
                                EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True" PageSize="10" DataSourceID="dsUpazilaPS">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSl" Text='<%# Eval("SlNo") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UpazilaPSID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUpazilaPSID" Text='<%# Eval("UpazilaPSID") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DistrictID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrictID" Text='<%# Eval("DistrictID") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DivisionID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDivisionID" Text='<%# Eval("DivisionID") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUpazilaPSCode" Text='<%# Eval("UpazilaPSCode") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUpazilaPSName" Text='<%# HighlightText(Eval("UpazilaPSName").ToString()) %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrictName" Text='<%# Eval("DistrictName") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Division" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDivisionName" Text='<%# Eval("DivisionName") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgEdit" runat="server" CommandName="Edit" ImageUrl="~/App_Themes/Default/Images/gridview/View.png" ToolTip="Edit" />
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
                            <asp:SqlDataSource ID="dsUpazilaPS" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>" 
                                SelectCommand="SELECT ROW_NUMBER() OVER (ORDER BY ADMUpazilaPS.UpazilaPSID) As SlNo, ADMUpazilaPS.UpazilaPSID, ADMUpazilaPS.DistrictID, ADMDistrict.DistrictName, ADMDistrict.DistrictName, ADMUpazilaPS.UpazilaPSCode, ADMUpazilaPS.UpazilaPSCode, 
                                    ADMUpazilaPS.UpazilaPSName, ADMUpazilaPS.UpazilaPSName, ADMUpazilaPS.Description, ADMUpazilaPS.Description, ADMDivision.DivisionID, ADMDivision.DivisionName, ADMDivision.DivisionName, ADMUpazilaPS.CreatedBy, ADMUpazilaPS.CreatedDate, ADMUpazilaPS.ModifiedBy, ADMUpazilaPS.ModifiedDate
                                    FROM ADMUpazilaPS INNER JOIN
                                    ADMDistrict ON ADMUpazilaPS.DistrictID = ADMDistrict.DistrictID INNER JOIN
                                    ADMDivision ON ADMDistrict.DivisionID = ADMDivision.DivisionID" FilterExpression="UpazilaPSName LIKE '%{0}%'">
                                <FilterParameters>
                                    <asp:ControlParameter Name="UpazilaPSName" ControlID="txtSearch" PropertyName="Text" />
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
    <div id="DivEditWindow" style="display: none;" class="popupUpazilaPS">
        <iframe id="IframeEdit" frameborder="0" width="450px" height="248px" scrolling="no">
        </iframe>
    </div>
</asp:Content>
