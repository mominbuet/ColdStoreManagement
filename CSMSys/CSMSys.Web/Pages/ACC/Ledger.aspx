<%@ Page Title="CSMSys :: Account Ledger" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" 
CodeBehind="Ledger.aspx.cs" Inherits="CSMSys.Web.Pages.ACC.Ledger" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">
        function ShowEditModal(AccountID) {
            var frame = $get('IframeEdit');
            frame.src = "../../../Controls/INV/Account.aspx?UIMODE=EDIT&PID=" + AccountID;
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
            $get('btnRefresh').click();
        }
        function NewOkayScript() {
            $get('btnRefresh').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdnAccountID" runat="server" />
    <asp:Label ID="hdnLedgerID" runat="server" Visible="False" />
    <asp:HiddenField ID="hdnCurBal" runat="server" />
    <div class="title">
        <h2>
            Account Ledger
        </h2>
    </div>
    <div class="feature-box-full">
        <table width="100%" border="0" cellpadding="2" cellspacing="0">
			<tbody>
            <%--<tr>
                <td align="left" style="width:75%;">
                    <strong>New Account Ledger</strong>
                </td>
				<td align="left" style="width:25%;">
                    <strong>Account Title</strong>
                </td>
            </tr>--%>
			<tr>
				<td align="left" style="width:25%;">
                    <div style="overflow:auto; width:95%; height:480px; border: 1px solid #996600; padding:0px 0px 10px 10px;">
                        <asp:TreeView ID="tvLedger" runat="server" ImageSet="Arrows" 
                            SelectedNodeStyle-ForeColor="Green"
                            SelectedNodeStyle-VerticalPadding="0"
                            OnSelectedNodeChanged="tvLedger_SelectedNodeChanged">
                            <HoverNodeStyle 
                                Font-Underline="True" ForeColor="#5555DD" />
                            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" 
                                HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                            <ParentNodeStyle Font-Bold="False" />
                            <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" 
                                VerticalPadding="0px" ForeColor="#5555DD" />
                        </asp:TreeView>
                    </div>
                </td>
                <td align="left" valign="top" style="width:75%;">
                    <div style="height:470px; border: 1px solid #996600; padding:10px;">
                        <div class="feature-box-full">
                            <table width="100%" border="0" cellpadding="4" cellspacing="0">
			                    <tbody>
                                <tr>
                                    <td align="left" style="width:20%;">
                                        Ledger Title : 
                                    </td>
				                    <td align="left" style="width:30%;">
                                        <asp:TextBox runat="server" ID="txtLedgerTitle" Width="202px"></asp:TextBox>
                                    </td>
                                    <td align="left" style="width:20%;">
                                        Ledger Type : 
                                    </td>
				                    <td align="left" style="width:30%;">
                                        <asp:DropDownList ID="ddlLedgerType" runat="server" Width="262px" AutoPostBack="True" 
                                            onselectedindexchanged="ddlLedgerType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
			                    </tr>
                                <tr>
                                    <td align="left" colspan="4">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="4">
                                        <asp:CheckBox runat="server" ID="chkInChartOfAcc" 
                                            Text="Effect on Chart of Account" />
                                    </td>
                                </tr>
			                    <tr>
                                    <td align="left" style="width:20%;">
                                        Parent Ledger : 
                                    </td>
				                    <td align="left" style="width:30%;">
                                        <asp:DropDownList ID="ddlParentLedger" runat="server" Width="202px" AutoPostBack="True" 
                                            onselectedindexchanged="ddlParentLedger_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" style="width:20%;">
                                        Category : 
                                    </td>
				                    <td align="left" style="width:30%;">
                                        <asp:DropDownList ID="ddlAccOrGroup" runat="server" Width="202px">
                                            <asp:ListItem>Sub Ledger</asp:ListItem>
                                            <asp:ListItem>Account</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
			                    </tr>
			                    <tr>
                                    <td align="left" style="width:20%;">
                                        OP Balance : 
                                    </td>
				                    <td align="left" style="width:30%;">
                                        <asp:TextBox ID="txtOpBal" runat="server">
                                        </asp:TextBox>
                                    </td>
                                    <td align="left" style="width:20%;">
                                        Ledger No : 
                                    </td>
				                    <td align="left" style="width:30%;">
                                        <asp:TextBox ID="txtLedgerNo" runat="server" ReadOnly="true">
                                        </asp:TextBox>
                                    </td>
			                    </tr>
                                <tr>
                                    <td align="left" colspan="4">
                                        <hr />
                                    </td>
                                </tr>
			                    <tr>
                                    <td align="left" colspan="2">
                                        <asp:CheckBox runat="server" ID="chkInvRel" Text="Inventory Related" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        Status : <asp:DropDownList ID="ddlAccStatus" runat="server">
                                            <asp:ListItem>Active</asp:ListItem>
                                            <asp:ListItem>Inactive</asp:ListItem>
                                            <asp:ListItem>Suspend</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" style="width:20%;">
                                        Date : 
                                    </td>
				                    <td align="left" style="width:30%;">
                                        <asp:TextBox ID="txtCreateDate" runat="server"></asp:TextBox>
                                        &nbsp;&nbsp;
                                        <cc1:CalendarExtender ID="txtCreateDate_CalendarExtender" runat="server" 
                                            Enabled="True" TargetControlID="txtCreateDate" PopupPosition="BottomLeft">
                                        </cc1:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="reValidator11" runat="server" 
                                            ControlToValidate="txtCreateDate" ErrorMessage="*" 
                                            ValidationExpression="^[0-9d]{1,2}/[0-9m]{1,2}/[0-9y]{4}$">
                                        </asp:RegularExpressionValidator>
                                    </td>
			                    </tr>
                                <tr>
                                    <td align="left" colspan="4">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="4">
                                        <asp:CheckBox runat="server" ID="chkDetail" Text="Detail Information" />
                                    </td>
                                </tr>
			                    <tr>
                                    <td align="left" style="width:20%;">
                                        <asp:Label runat="server" ID="lblContact" Text="Contact Person : " />
                                    </td>
				                    <td align="left" style="width:30%;">
                                        <asp:TextBox ID="txtContactPerson" runat="server" Width="202px" Text="">
                                        </asp:TextBox>
                                    </td>
                                    <td align="left" style="width:20%;">
                                        Bank A/C Type : 
                                    </td>
				                    <td align="left" style="width:30%;">
                                        <asp:DropDownList ID="ddlAcType" runat="server" Width="202px">
                                        </asp:DropDownList>
                                    </td>
			                    </tr>
			                    <tr>
                                    <td align="left" style="width:20%;">
                                        Address : 
                                    </td>
				                    <td align="left" style="width:30%;">
                                        <asp:TextBox ID="txtAddress" runat="server" Width="202px">
                                        </asp:TextBox>
                                    </td>
                                    <td align="left" style="width:20%;">
                                        Phone : 
                                    </td>
				                    <td align="left" style="width:30%;">
                                        <asp:TextBox ID="txtPhone" runat="server" Width="202px">
                                        </asp:TextBox>
                                    </td>
			                    </tr>
			                    <tr>
                                    <td align="left" style="width:20%;">
                                        Fax : 
                                    </td>
				                    <td align="left" style="width:30%;">
                                        <asp:TextBox ID="txtFax" runat="server" Width="202px">
                                        </asp:TextBox>
                                    </td>
                                    <td align="left" style="width:20%;">
                                        Email : 
                                    </td>
				                    <td align="left" style="width:30%;">
                                        <asp:TextBox ID="txtEmail" runat="server" Width="202px">
                                        </asp:TextBox>
                                    </td>
			                    </tr>
			                    <tr>
                                    <td align="left" style="width:20%;">
                                        Country : 
                                    </td>
				                    <td align="left" style="width:30%;">
                                        <asp:DropDownList ID="ddlCountry" runat="server" Width="202px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" style="width:20%;">
                                        Currency : 
                                    </td>
				                    <td align="left" style="width:30%;">
                                        <asp:DropDownList ID="ddlCurrency" runat="server" Width="202px">
                                        </asp:DropDownList>
                                    </td>
			                    </tr>
			                    <tr>
                                    <td align="left" style="width:20%;">
                                        <asp:Label runat="server" ID="lblBusiness" Text="Business Type : " />
                                    </td>
				                    <td align="left" style="width:30%;">
                                        <asp:TextBox ID="txtBusinessType" runat="server" Width="202px">
                                        </asp:TextBox>
                                    </td>
                                    <td align="left" style="width:20%;">
                                        Remarks : 
                                    </td>
				                    <td align="left" style="width:30%;">
                                        <asp:TextBox ID="txtRemarks" runat="server" Width="202px">
                                        </asp:TextBox>
                                    </td>
			                    </tr>
			                    <tr>
                                    <td align="left" style="width:20%;">
                                        Sales Team : 
                                    </td>
				                    <td align="left" style="width:30%;">
                                        <asp:DropDownList ID="ddlTeam" runat="server" Width="202px" AutoPostBack="True" 
                                            onselectedindexchanged="ddlTeam_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" style="width:20%;">
                                        Member : 
                                    </td>
				                    <td align="left" style="width:30%;">
                                        <asp:DropDownList ID="ddlMember" runat="server" Width="202px">
                                        </asp:DropDownList>
                                    </td>
			                    </tr>
			                    </tbody>
		                    </table>
                            <div class="feature-box-actionBar">
                                <span class="failureNotification">
                                    <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
                                </span>

                                <asp:Button ID="btnDelete" runat="server" CssClass="button" Text="Delete" />

                                <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" 
                                    onclick="btnSave_Click" />

                                <asp:Button ID="btnNew" runat="server" CssClass="button" Text="New" 
                                    onclick="btnNew_Click" />
                            </div>
                        </div>
                    </div>
                </td>
			</tr>
			</tbody>
		</table>
    </div>
</asp:Content>
