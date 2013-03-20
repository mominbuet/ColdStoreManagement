<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemDetails.aspx.cs" Inherits="CSMSys.Web.Controls.Item.ItemDetails" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Enter" />
	<meta content="blendTrans(Duration=0.5)" http-equiv="Page-Exit" />
    <title></title>
    <link href="../../../App_Themes/Default/Styles/Default.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function getbacktostepone() {
            window.location = "ItemDetails.aspx";
        }
        function onSuccess() {
            setTimeout(okay, 2000);
        }
        function onError() {
            setTimeout(cancel, 2000);
        }
        function okay() {
            var UIMODE = $get('hdnWindowUIMODE').value;
            if (UIMODE == "EDIT")
                window.parent.document.getElementById('ButtonEditDone').click();
            else {
                window.parent.document.getElementById('ButtonNewDone').click();
                getbacktostepone();
            }
        }
        function cancel() {
            var UIMODE = $get('hdnWindowUIMODE').value;
            if (UIMODE == "EDIT")
                window.parent.document.getElementById('ButtonEditCancel').click();
            else
                window.parent.document.getElementById('ButtonNewCancel').click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" value="" runat="server" id="hdnWindowUIMODE" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="popup_Container">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Customer Setup
                </div>
                <div class="TitlebarRight" onclick="cancel();"></div>
            </div>
            <div class="popup_Body">
                <asp:MultiView ID="MultiViewParty" runat="server">
                    <asp:View ID="ViewInput" runat="server">
                        <asp:Panel ID="pnlNew" runat="server" width="100%">
                            <table width="100%" border="0" cellpadding="0" cellspacing="4">
			                    <tbody>
			                    <tr>
				                    <td align="left" valign="top" style="width:50%;">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
			                                <tbody>
			                                <tr>
				                                <td align="left" colspan="2"><strong>General Information </strong></td>
			                                </tr>
			                                <tr>
				                                <td align="left">Registration Code : </td>
				                                <td align="left"><asp:TextBox ID="txtRegistrationID" runat="server"></asp:TextBox>
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left">Customer Code : </td>
				                                <td align="left">
                                                    <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                                                </td>
			                                </tr>
			                               <tr>
                                        <td align="left" colspan="2">
                                            <strong>Description of Potato</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 30%;">
                                            Type :
                                        </td>
                                        <td align="left" valign="top" style="width: 70%;">
                                            <asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 30%;">
                                            Number of Bags :
                                        </td>
                                        <td align="left" valign="top" style="width: 70%;">
                                            <asp:TextBox ID="txtBagNo" runat="server" Enabled="False">0</asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 30%;">
                                            Weight per Bag:
                                        </td>
                                        <td align="left" valign="top" style="width: 70%;">
                                            <asp:DropDownList ID="ddlWeight" runat="server" OnSelectedIndexChanged="ddlWeight_SelectedIndexChanged"
                                                Enabled="False" AutoPostBack="True" Style="height: 22px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 30%;">
                                            Fair per Bag :
                                        </td>
                                        <td align="left" valign="top" style="width: 70%;">
                                            <asp:TextBox ID="txtBagFair" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 30%;">
                                            Advance :
                                        </td>
                                        <td align="left" valign="top" style="width: 70%;">
                                            <asp:TextBox ID="txtAdvance" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 30%;">
                                            Total Fair :
                                        </td>
                                        <td align="left" valign="top" style="width: 70%;">
                                            <asp:TextBox ID="txtTotalFair" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                                                        <tr>
                                        <td>
                                        </td>
                                        <td align="right" valign="top" style="width: 70%;">
                                            <asp:Button ID="btnAddItem" runat="server" Text="Add Item" OnClick="btnAddItem_Click" />
                                        </td>
                                    </tr>
			                                </tbody>
		                                </table>
                                    </td>
				                    <td align="left" valign="top" style="width:50%;">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
			                                <tbody>
			                              <tr>
                                        <td>
                                            <strong>Item Details</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grvItemDetails" DataKeyNames="ItemDetailID" runat="server" Width="100%"
                                                AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Left" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True"
                                                PageSize="5" DataSourceID="dsItemDetails">
                                                <Columns>
                                                    <asp:BoundField DataField="TypeName" HeaderText="TypeName" 
                                                        SortExpression="TypeName" />
                                                    <asp:BoundField DataField="BagNo" HeaderText="BagNo" SortExpression="BagNo" />
                                                    <asp:BoundField DataField="BagWeight" HeaderText="BagWeight" 
                                                        SortExpression="BagWeight" />
                                                    <asp:BoundField DataField="BagFair" HeaderText="BagFair" 
                                                        SortExpression="BagFair" />
                                                    <asp:BoundField DataField="TotalFair" HeaderText="TotalFair" 
                                                        SortExpression="TotalFair" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="dsItemDetails" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>" >
<%--                                                SelectCommand="SELECT     itd.RegistrationID, it.TypeName, itd.BagNo, itd.BagWeight, itd.BagFair, itd.TotalFair, itd.Advance
                                                                            FROM          dbo.INVItemDetail as itd INNER JOIN dbo.INVItemType AS it ON itd.ItemTypeID = it.TypeID
                                                                            where itd.RegistrationID=0 ORDER BY itd.ItemDetailID DESC">--%>
                                            </asp:SqlDataSource>
                                           </td>
			                                            </tr>
			                                            </tbody>
		                                            </table>
                                                </td>
			                                </tr>
			                                </tbody>
		                                </table>
                        </asp:Panel>

                        <div class="feature-box-actionBar">
                            <span class="failureNotification">
                                <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
                            </span>
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save"  OnClick="btnSave_Click" />
                        </div>
                    </asp:View>
                    <asp:View ID="ViewSuccess" runat="server">
                        <asp:Panel ID="pnlSuccess" runat="server" width="100%">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
			                    <tbody>
                                <tr>
                                    <td align="center">
                                        <span class="succesNotification">
                                            Party Saved/Edited Successfully. <br />
                                            Dialog will Close automatically within 2 Seconds
                                        </span>
                                    </td>
                                </tr>
			                    </tbody>
		                    </table>
                        </asp:Panel>
                    </asp:View>
                    <asp:View ID="ViewError" runat="server">
                        <asp:Panel ID="pnlError" runat="server" width="100%">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
			                    <tbody>
                                <tr>
                                    <td align="center" valign="middle">
                                        <span class="failureNotification">
                                            Error Occured Saving/Editing Party<br />
                                            Dialog will Close automatically within 2 Seconds
                                        </span>
                                    </td>
                                </tr>
			                    </tbody>
		                    </table>
                        </asp:Panel>
                    </asp:View>
                </asp:MultiView>
            </div>
            <div class="popup_Buttons" style="display:none;">
                <asp:Button ID="btnOkay" Text="Done" runat="server" OnClick="btnSave_Click" />
                <input id="btnCancel" value="Cancel" type="button" onclick="cancel();" />
            </div>
        </div>
    </form>
</body>
</html>