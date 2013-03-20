<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" 
CodeBehind="VoucherNew.aspx.cs" Inherits="CSMSys.Web.Pages.ACC.VoucherNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function getbacktostepone() {
            window.location = "VoucherNew.aspx";
        }
        function onSuccess() {
            setTimeout(okay, 2000);
        }
        function onError() {
            setTimeout(cancel, 2000);
        }
        function DrPaymentokay() {
            var UIMODE = $get('hdnWindowUIMODE').value;
            if (UIMODE == "EDIT")
                window.parent.document.getElementById('ButtonEditDone').click();
            else {
                window.parent.document.getElementById('ButtonNewDone').click();
                getbacktostepone();
            }
        }
        function DrPaymentcancel() {
//            var UIMODE = $get('hdnWindowUIMODE').value;
//            if (UIMODE == "EDIT")
//                window.parent.document.getElementById('ButtonEditCancel').click();
//            else
                window.parent.document.getElementById('ButtonDrPaymentCancel').click();
        }
        function VoucherForokay() {
            var UIMODE = $get('hdnWindowUIMODE').value;
            if (UIMODE == "EDIT")
                window.parent.document.getElementById('ButtonEditDone').click();
            else {
                window.parent.document.getElementById('ButtonNewDone').click();
                getbacktostepone();
            }
        }
        function VoucherForcancel() {
            //            var UIMODE = $get('hdnWindowUIMODE').value;
            //            if (UIMODE == "EDIT")
            //                window.parent.document.getElementById('ButtonEditCancel').click();
            //            else
            window.parent.document.getElementById('ButtonVoucherForCancel').click();
        }
        function CrPaymentcancel() {
            //            var UIMODE = $get('hdnWindowUIMODE').value;
            //            if (UIMODE == "EDIT")
            //                window.parent.document.getElementById('ButtonEditCancel').click();
            //            else
            window.parent.document.getElementById('ButtonCrPaymentCancel').click();
        }
        function Receivecancel() {
            //            var UIMODE = $get('hdnWindowUIMODE').value;
            //            if (UIMODE == "EDIT")
            //                window.parent.document.getElementById('ButtonEditCancel').click();
            //            else
            window.parent.document.getElementById('ButtonReceiveCancel').click();
        }
        function JrAccountcancel() {
            //            var UIMODE = $get('hdnWindowUIMODE').value;
            //            if (UIMODE == "EDIT")
            //                window.parent.document.getElementById('ButtonEditCancel').click();
            //            else
            window.parent.document.getElementById('ButtonJrAccountCancel').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdnTMID" runat="server" />
    <asp:HiddenField ID="hdnDrAccID" runat="server" />
    <asp:HiddenField ID="hdnCrAccID" runat="server" />
    <asp:HiddenField ID="hdnCrAccNo" runat="server" />
    <asp:HiddenField ID="hdnDrAccNo" runat="server" />
    <asp:HiddenField ID="hdnDrTotal" runat="server" />
    <asp:HiddenField ID="hdnCrTotal" runat="server" />
    <div class="title">
        <h2>
            <asp:Label ID="lblVoucher" runat="server" Text="Debit Voucher"></asp:Label>
        </h2>
    </div>
    <div class="feature-box-full">
        <asp:MultiView ID="MultiViewVoucher" runat="server">
            <asp:View ID="ViewDebit" runat="server">
                <table width="100%" border="0" cellpadding="0" cellspacing="4">
                    <tbody>
                    <tr>
                        <td align="left" valign="top" style="width:50%;">
                            <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                <tbody>
                                <tr>
                                    <td align="left" style="width:20%;">Voucher No : </td>
                                    <td align="left" style="width:30%;">
                                        <asp:TextBox runat="server" ID="txtDrVoucherNo"></asp:TextBox>
                                    </td>
                                    <td align="left" style="width:20%;">Date : </td>
                                    <td align="left" style="width:30%;">
                                        <asp:TextBox runat="server" ID="txtDrDate"></asp:TextBox>
                                        &nbsp;&nbsp;
                                        <cc1:CalendarExtender ID="txtDrDate_CalendarExtender" runat="server" 
                                            Enabled="True" TargetControlID="txtDrDate" PopupPosition="BottomLeft">
                                        </cc1:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="reValidator11" runat="server" 
                                            ControlToValidate="txtDrDate" ErrorMessage="*" 
                                            ValidationExpression="^[0-9d]{1,2}/[0-9m]{1,2}/[0-9y]{4}$">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Payment Method : </td>
                                    <td align="left">
                                         <asp:DropDownList ID="ddlPayMethod" runat="server" Width="262px">
                                             <asp:ListItem Value="0">&lt;-- Select Payment Method --&gt;</asp:ListItem>
                                             <asp:ListItem Value="1">Cash</asp:ListItem>
                                             <asp:ListItem Value="2">Cheque</asp:ListItem>
                                             <asp:ListItem Value="3">DD/TT</asp:ListItem>
                                             <asp:ListItem Value="4">LC</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left">Ref No : </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtDrRefNo"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Payment A/C : </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtDrAccount" Width="262px" ReadOnly="true"></asp:TextBox>
                                        <asp:ImageButton ID="imgDrPayment" runat="server" CommandName="NewDrPayment" ImageUrl="~/App_Themes/Default/Images/gridview/New.png"
                                            ToolTip="Payment A/C" Width="16px" Height="16px" />
                                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="ModalPopupBG"
                                            runat="server" CancelControlID="ButtonDrPaymentCancel" OkControlID="ButtonDrPaymentDone"
                                            TargetControlID="imgDrPayment" PopupControlID="DivDrPaymentWindow">
                                        </cc1:ModalPopupExtender>
                                        <div class="popup_Buttons" style="display: none">
                                            <input id="ButtonDrPaymentDone" value="Done" type="button" />
                                            <input id="ButtonDrPaymentCancel" value="Cancel" type="button" />
                                        </div>
                                        <div id="DivDrPaymentWindow" style="display: none;" class="popupDrPayment">
                                            <div class="popup_Container">
                                                <div class="popup_Titlebar" id="PopupHeader">
                                                    <div class="TitlebarLeft">
                                                        Payment A/C
                                                    </div>
                                                    <div class="TitlebarRight" onclick="DrPaymentcancel();"></div>
                                                </div>
                                                <div class="popup_Body">
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                                        <tbody>
                                                            <tr>
                                                                <td align="left" style="width:97%;">
                                                                    Search Payment A/C : <asp:TextBox runat="server" ID="txtSearchDrPayment" 
                                                                        Width="202px" ontextchanged="txtSearchDrPayment_TextChanged"></asp:TextBox>
                                                                </td>
                                                                <td align="left" valign="bottom" style="width:3%;">
                                                                    <asp:ImageButton ID="imgSearchDrPayment" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png" ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" colspan="2">
                                                                    <asp:GridView ID="grvDrPayment" DataKeyNames="AccountID" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                        CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvDrPayment_PageIndexChanging" ShowHeaderWhenEmpty="true" 
                                                                        OnRowDataBound="grvDrPayment_RowDataBound" OnRowCommand="grvDrPayment_RowCommand"
                                                                        EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True" PageSize="5" DataSourceID="dsDrPayment">
                                                                        <Columns>
                                                                            <asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="imgSelectDrPayment" runat="server" CommandName="Select" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png" ToolTip="Edit" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="AccountID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAccountID" Text='<%# Eval("AccountID") %>' runat="server" HorizontalAlign="Left" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Acc No" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAccountNo" Text='<%# Eval("AccountNo") %>' runat="server" HorizontalAlign="Left" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Account Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAccountTitle" Text='<%# HighlightText(Eval("AccountTitle").ToString()) %>' runat="server" HorizontalAlign="Left" />
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
                                                                    <asp:SqlDataSource ID="dsDrPayment" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>" 
                                                                        SelectCommand="SELECT AccountID, AccountNo, AccountTitle
                                                                            FROM T_Account
                                                                            WHERE (AccDepth = 2) AND (ParentID IN (2, 19))" FilterExpression="AccountTitle LIKE '%{0}%'">
                                                                        <FilterParameters>
                                                                            <asp:ControlParameter Name="AccountTitle" ControlID="txtSearchDrPayment" PropertyName="Text" />
                                                                        </FilterParameters>
                                                                    </asp:SqlDataSource>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="popup_Buttons" style="display:none;">
                                                    <%--<input id="btnOkay" value="Done" type="button" onclick="cancel();" />--%>
                                                    <input id="btnCancel" value="Cancel" type="button" onclick="DrPaymentcancel();" />
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                    <td align="left">A/C No : </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtDrAccountNo" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="background-color:#FFCC66;">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                            <tbody>
                                                <tr>
                                                    <td align="left" style="width:18%;">Voucher For : </td>
                                                    <td align="left" style="width:32%;">
                                                        <asp:TextBox runat="server" ID="txtDrVoucherFor" Width="262px" ReadOnly="true"></asp:TextBox>
                                                        <asp:ImageButton ID="imgVoucherFor" runat="server" CommandName="NewVoucherFor" ImageUrl="~/App_Themes/Default/Images/gridview/New.png"
                                                            ToolTip="Voucher for" Width="16px" Height="16px" />
                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" BackgroundCssClass="ModalPopupBG"
                                                            runat="server" CancelControlID="ButtonVoucherForCancel" OkControlID="ButtonVoucherForDone"
                                                            TargetControlID="imgVoucherFor" PopupControlID="DivVoucherForWindow">
                                                        </cc1:ModalPopupExtender>
                                                        <div class="popup_Buttons" style="display: none">
                                                            <input id="ButtonVoucherForDone" value="Done" type="button" />
                                                            <input id="ButtonVoucherForCancel" value="Cancel" type="button" />
                                                        </div>
                                                        <div id="DivVoucherForWindow" style="display: none;" class="popupVoucherFor">
                                                            <div class="popup_Container">
                                                                <div class="popup_Titlebar" id="Div2">
                                                                    <div class="TitlebarLeft">
                                                                        Voucher For
                                                                    </div>
                                                                    <div class="TitlebarRight" onclick="VoucherForcancel();"></div>
                                                                </div>
                                                                <div class="popup_Body" style="overflow:auto; height:430px;">
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    Search Voucher For A/C : <asp:TextBox runat="server" ID="txtSearchVoucherFor" Width="202px"></asp:TextBox>
                                                                                </td>
                                                                                <td align="center" valign="bottom" style="width:3%;">
                                                                                    <asp:ImageButton ID="imgSearchVoucherFor" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png" ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" colspan="2">
                                                                                    <asp:GridView ID="grvVoucherFor" DataKeyNames="AccountID" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                                        CellPadding="4" HorizontalAlign="Left"  OnPageIndexChanging="grvVoucherFor_PageIndexChanging" ShowHeaderWhenEmpty="true" 
                                                                                        OnRowDataBound="grvVoucherFor_RowDataBound" OnRowCommand="grvVoucherFor_RowCommand"
                                                                                        EmptyDataText="No Records Found" CssClass="tablesorterBlue" DataSourceID="dsVoucherFor">
                                                                                        <Columns>
                                                                                            <asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
                                                                                                <ItemTemplate>
                                                                                                    <asp:ImageButton ID="imgSelectVoucherFor" runat="server" CommandName="Select" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png" ToolTip="Edit" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="AccountID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblAccountID" Text='<%# Eval("AccountID") %>' runat="server" HorizontalAlign="Left" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Acc No" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblAccountNo" Text='<%# Eval("AccountNo") %>' runat="server" HorizontalAlign="Left" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Account Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblAccountTitle" Text='<%# HighlightText(Eval("AccountTitle").ToString()) %>' runat="server" HorizontalAlign="Left" />
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
                                                                                    <asp:SqlDataSource ID="dsVoucherFor" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>" 
                                                                                        SelectCommand="SELECT AccountID, AccountNo, AccountTitle
                                                                                            FROM T_Account
                                                                                            WHERE (AccDepth >= 2) AND (ParentID IN (1, 2, 10, 13, 14, 17, 27, 29, 30, 32, 567))" FilterExpression="AccountTitle LIKE '%{0}%'">
                                                                                        <FilterParameters>
                                                                                            <asp:ControlParameter Name="AccountTitle" ControlID="txtSearchDrPayment" PropertyName="Text" />
                                                                                        </FilterParameters>
                                                                                    </asp:SqlDataSource>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                                <div class="popup_Buttons" style="display:none;">
                                                                    <%--<input id="btnOkay" value="Done" type="button" onclick="cancel();" />--%>
                                                                    <input id="Button12" value="Cancel" type="button" onclick="VoucherForcancel();" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td align="left" style="width:20%;">Amount Tk. : </td>
                                                    <td align="left" style="width:30%;">
                                                        <asp:TextBox ID="txtDrVoucherAmount" runat="server"></asp:TextBox>
                                                        &nbsp;
                                                        <asp:Button runat="server" ID="btnDrAdd" Text="   Add   " 
                                                            onclick="btnDrAdd_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="4">
                                                        <asp:GridView ID="grvDrAccount" DataKeyNames="AccountID" runat="server" Width="100%" AutoGenerateColumns="False"
                                                            CellPadding="4" HorizontalAlign="Left" ShowHeaderWhenEmpty="true" ShowFooter="true" OnRowDataBound="grvDrAccount_RowDataBound" OnRowCommand="grvDrAccount_RowCommand" 
                                                            EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True" PageSize="5" DataSourceID="dsDrAccount">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSl" Text='<%# Eval("SlNo") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TransDID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTransDID" Text='<%# Eval("TransDID") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TransMID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTransMID" Text='<%# Eval("TransMID") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="AccountID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAccountID" Text='<%# Eval("AccountID") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Acc No" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAccountNo" Text='<%# Eval("AccountNo") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Account Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAccountTitle" Text='<%# Eval("AccountTitle") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblDebit" Text="Total Debit Amount : " runat="server" HorizontalAlign="Right" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDebitAmt" Text='<%# Eval("DebitAmt") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblDebitTotal" Text="0" runat="server" HorizontalAlign="Right" />
                                                                    </FooterTemplate>
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
                                                        <asp:SqlDataSource ID="dsDrAccount" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>" OnSelecting="dsDrAccount_Selecting" 
                                                            SelectCommand="SELECT ROW_NUMBER() OVER (ORDER BY td.TransDID) As SlNo, td.TransDID, td.TransMID, td.AccountID, ta.AccountNo, ta.AccountTitle, td.CreditAmt, td.DebitAmt, td.Comments
                                                                FROM T_Transaction_Detail AS td INNER JOIN
                                                                    T_Account AS ta ON td.AccountID = ta.AccountID 
                                                                WHERE (td.TransMID = @TransMID)">
                                                            <SelectParameters>
                                                                <asp:Parameter Name="TransMID" Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>    
                                </tr>
                                <tr>
                                    <td align="left" valign="top">Description : </td>  
                                    <td align="left" colspan="3">
                                        <asp:TextBox  runat="server" ID="txtDrDesc" TextMode="MultiLine" Width="806px" ></asp:TextBox>
                                    </td>
                                </tr>                
                                <tr>
                                    <td align="left">
                                        <asp:CheckBox runat="server" ID="chkDrAppvBy" Text="Aproved By : " />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtDrAppvBy"></asp:TextBox>
                                    </td>
                                    <td align="left">Approved Date : </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtDrAppvDate"></asp:TextBox>
                                        &nbsp;&nbsp;
                                        <cc1:CalendarExtender ID="txtDrAppvDate_CalendarExtender" runat="server" 
                                            Enabled="True" TargetControlID="txtDrAppvDate" PopupPosition="BottomLeft">
                                        </cc1:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="reValidator2" runat="server" 
                                            ControlToValidate="txtDrAppvDate" ErrorMessage="*" 
                                            ValidationExpression="^[0-9d]{1,2}/[0-9m]{1,2}/[0-9y]{4}$">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>	                                
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    </tbody>
                </table>

                <div class="feature-box-actionBar">
                    <span class="failureNotification">
                        <asp:Literal ID="lblDrFailure" runat="server"></asp:Literal>
                    </span>

                    <asp:Button ID="btnDrDelete" runat="server" CssClass="button" Text="Delete" 
                        onclick="btnDrDelete_Click" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender5" BackgroundCssClass="ModalPopupBG"
                            runat="server" TargetControlID="btnDrDelete" PopupControlID="DivDeleteConfirmation"
                            OkControlID="ButtonDeleteOkay" CancelControlID="ButtonDeleteCancel">
                        </cc1:ModalPopupExtender>
                        <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender" runat="server" Enabled="True"
                            TargetControlID="btnDrDelete" DisplayModalPopupID="ModalPopupExtender5">
                        </cc1:ConfirmButtonExtender>

                    <asp:Button ID="btnDrPrint" runat="server" CssClass="button" Text="Print" OnClick="btnDrPrint_Click" />

                    <asp:Button ID="btnDrPost" runat="server" CssClass="button" Text="Post" ValidationGroup="DrValidationGroup" OnClick="btnDrPost_Click" />

                    <asp:Button ID="btnDrReset" runat="server" CssClass="button" Text="Reset" />
                </div>
            </asp:View>
            <asp:View ID="ViewCredit" runat="server">
                <table width="100%" border="0" cellpadding="0" cellspacing="4">
                    <tbody>
                    <tr>
                        <td align="left" valign="top" style="width:50%;">
                            <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                <tbody>
                                <tr>
                                    <td align="left" style="width:20%;">Voucher No : </td>
                                    <td align="left" style="width:30%;">
                                        <asp:TextBox runat="server" ID="txtCrVoucherNo"></asp:TextBox>
                                    </td>
                                    <td align="left" style="width:20%;">Date : </td>
                                    <td align="left" style="width:30%;">
                                        <asp:TextBox runat="server" ID="txtCrDate"></asp:TextBox>
                                        &nbsp;&nbsp;
                                        <cc1:CalendarExtender ID="txtCrDate_CalendarExtender" runat="server" 
                                            Enabled="True" TargetControlID="txtCrDate" PopupPosition="BottomLeft">
                                        </cc1:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="reValidator3" runat="server" 
                                            ControlToValidate="txtCrDate" ErrorMessage="*" 
                                            ValidationExpression="^[0-9d]{1,2}/[0-9m]{1,2}/[0-9y]{4}$">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Collection Method : </td>
                                    <td align="left">
                                         <asp:DropDownList ID="ddlCollMethod" runat="server" Width="262px">
                                             <asp:ListItem Value="0">&lt;-- Select Collection Method --&gt;</asp:ListItem>
                                             <asp:ListItem Value="1">Cash</asp:ListItem>
                                             <asp:ListItem Value="2">Cheque</asp:ListItem>
                                             <asp:ListItem Value="3">DD/TT</asp:ListItem>
                                             <asp:ListItem Value="4">LC</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left">Ref No : </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtCrRefNo"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Receive A/C : </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtCrAccount" Width="262px"></asp:TextBox>
                                        <asp:ImageButton ID="imgCrPayment" runat="server" CommandName="NewCrPayment" ImageUrl="~/App_Themes/Default/Images/gridview/New.png"
                                            ToolTip="Receive A/C" Width="16px" Height="16px" />
                                        <cc1:ModalPopupExtender ID="ModalPopupExtender3" BackgroundCssClass="ModalPopupBG"
                                            runat="server" CancelControlID="ButtonCrPaymentCancel" OkControlID="ButtonCrPaymentDone"
                                            TargetControlID="imgCrPayment" PopupControlID="DivCrPaymentWindow">
                                        </cc1:ModalPopupExtender>
                                        <div class="popup_Buttons" style="display: none">
                                            <input id="ButtonCrPaymentDone" value="Done" type="button" />
                                            <input id="ButtonCrPaymentCancel" value="Cancel" type="button" />
                                        </div>
                                        <div id="DivCrPaymentWindow" style="display: none;" class="popupDrPayment">
                                            <div class="popup_Container">
                                                <div class="popup_Titlebar" id="Div3">
                                                    <div class="TitlebarLeft">
                                                        Receive A/C
                                                    </div>
                                                    <div class="TitlebarRight" onclick="CrPaymentcancel();"></div>
                                                </div>
                                                <div class="popup_Body">
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                                        <tbody>
                                                            <tr>
                                                                <td align="left" style="width:96%;">
                                                                    Search Receive A/C : <asp:TextBox runat="server" ID="txtSearchCrPayment" Width="202px"></asp:TextBox>
                                                                </td>
                                                                <td align="left" valign="bottom" style="width:3%;">
                                                                    <asp:ImageButton ID="imgSearchCrPayment" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png" ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" colspan="2">
                                                                    <asp:GridView ID="grvCrPayment" DataKeyNames="AccountID" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                        CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvCrPayment_PageIndexChanging" ShowHeaderWhenEmpty="true" 
                                                                        OnRowDataBound="grvCrPayment_RowDataBound" OnRowCommand="grvCrPayment_RowCommand"
                                                                        EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True" PageSize="5" DataSourceID="dsCrPayment">
                                                                        <Columns>
                                                                            <asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="imgSelectCrPayment" runat="server" CommandName="Select" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png" ToolTip="Edit" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="AccountID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAccountID" Text='<%# Eval("AccountID") %>' runat="server" HorizontalAlign="Left" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Acc No" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAccountNo" Text='<%# Eval("AccountNo") %>' runat="server" HorizontalAlign="Left" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Account Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAccountTitle" Text='<%# HighlightText(Eval("AccountTitle").ToString()) %>' runat="server" HorizontalAlign="Left" />
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
                                                                    <asp:SqlDataSource ID="dsCrPayment" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>" 
                                                                        SelectCommand="SELECT AccountID, AccountNo, AccountTitle
                                                                            FROM T_Account
                                                                            WHERE (AccDepth = 2) AND (ParentID IN (2, 19))" FilterExpression="AccountTitle LIKE '%{0}%'">
                                                                        <FilterParameters>
                                                                            <asp:ControlParameter Name="AccountTitle" ControlID="txtSearchCrPayment" PropertyName="Text" />
                                                                        </FilterParameters>
                                                                    </asp:SqlDataSource>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="popup_Buttons" style="display:none;">
                                                    <%--<input id="btnOkay" value="Done" type="button" onclick="cancel();" />--%>
                                                    <input id="Button13" value="Cancel" type="button" onclick="DrPaymentcancel();" />
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                    <td align="left">A/C No : </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtCrAccountNo" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="background-color:#99FF66;">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                            <tbody>
                                                <tr>
                                                    <td align="left" style="width:20%;">Receive From : </td>
                                                    <td align="left" style="width:30%;">
                                                        <asp:TextBox runat="server" ID="txtReceiveFrom" Width="262px"></asp:TextBox>
                                                        <asp:ImageButton ID="imgReceive" runat="server" CommandName="NewReceive" ImageUrl="~/App_Themes/Default/Images/gridview/New.png"
                                                            ToolTip="Voucher for" Width="16px" Height="16px" />
                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender4" BackgroundCssClass="ModalPopupBG"
                                                            runat="server" CancelControlID="ButtonReceiveCancel" OkControlID="ButtonReceiveDone"
                                                            TargetControlID="imgReceive" PopupControlID="DivReceiveWindow">
                                                        </cc1:ModalPopupExtender>
                                                        <div class="popup_Buttons" style="display: none">
                                                            <input id="ButtonReceiveDone" value="Done" type="button" />
                                                            <input id="ButtonReceiveCancel" value="Cancel" type="button" />
                                                        </div>
                                                        <div id="DivReceiveWindow" style="display: none;" class="popupVoucherFor">
                                                            <div class="popup_Container">
                                                                <div class="popup_Titlebar" id="Div4">
                                                                    <div class="TitlebarLeft">
                                                                        Receive From
                                                                    </div>
                                                                    <div class="TitlebarRight" onclick="Receivecancel();"></div>
                                                                </div>
                                                                <div class="popup_Body" style="overflow:auto; height:430px;">
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    Search Receive From A/C : <asp:TextBox runat="server" ID="TextBox1" Width="202px"></asp:TextBox>
                                                                                </td>
                                                                                <td align="center" valign="bottom" style="width:3%;">
                                                                                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png" ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" colspan="2">
                                                                                    <asp:GridView ID="grvReceive" DataKeyNames="AccountID" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                                        CellPadding="4" HorizontalAlign="Left"  OnPageIndexChanging="grvReceive_PageIndexChanging" ShowHeaderWhenEmpty="true" 
                                                                                        OnRowDataBound="grvReceive_RowDataBound" OnRowCommand="grvReceive_RowCommand"
                                                                                        EmptyDataText="No Records Found" CssClass="tablesorterBlue" DataSourceID="dsReceive">
                                                                                        <Columns>
                                                                                            <asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
                                                                                                <ItemTemplate>
                                                                                                    <asp:ImageButton ID="imgSelectReceive" runat="server" CommandName="Select" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png" ToolTip="Edit" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="AccountID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblAccountID" Text='<%# Eval("AccountID") %>' runat="server" HorizontalAlign="Left" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Acc No" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblAccountNo" Text='<%# Eval("AccountNo") %>' runat="server" HorizontalAlign="Left" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Account Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblAccountTitle" Text='<%# HighlightText(Eval("AccountTitle").ToString()) %>' runat="server" HorizontalAlign="Left" />
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
                                                                                    <asp:SqlDataSource ID="dsReceive" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>" 
                                                                                        SelectCommand="SELECT AccountID, AccountNo, AccountTitle
                                                                                            FROM T_Account
                                                                                            WHERE (AccDepth >= 2) AND (FinalParentID IN (2, 17, 21, 22, 547, 624))" FilterExpression="AccountTitle LIKE '%{0}%'">
                                                                                        <FilterParameters>
                                                                                            <asp:ControlParameter Name="AccountTitle" ControlID="txtSearchDrPayment" PropertyName="Text" />
                                                                                        </FilterParameters>
                                                                                    </asp:SqlDataSource>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                                <div class="popup_Buttons" style="display:none;">
                                                                    <%--<input id="btnOkay" value="Done" type="button" onclick="cancel();" />--%>
                                                                    <input id="Button1ReceiveCancel" value="Cancel" type="button" onclick="Receivecancel();" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td align="left" style="width:20%;">Amount Tk. : </td>
                                                    <td align="left" style="width:30%;">
                                                        <asp:TextBox ID="txtReceiveAmount" runat="server"></asp:TextBox>
                                                        &nbsp;
                                                        <asp:Button runat="server" ID="btnCrAdd" Text="   Add   " OnClick="btnCrAdd_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="4">
                                                        <asp:GridView ID="grvCrAccount" DataKeyNames="AccountID" runat="server" Width="100%" AutoGenerateColumns="False"
                                                            CellPadding="4" HorizontalAlign="Left" ShowHeaderWhenEmpty="true" ShowFooter="true" OnRowDataBound="grvCrAccount_RowDataBound" OnRowCommand="grvCrAccount_RowCommand" 
                                                            EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True" PageSize="5" DataSourceID="dsCrAccount">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSl" Text='<%# Eval("SlNo") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TransDID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTransDID" Text='<%# Eval("TransDID") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TransMID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTransMID" Text='<%# Eval("TransMID") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="AccountID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAccountID" Text='<%# Eval("AccountID") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Acc No" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAccountNo" Text='<%# Eval("AccountNo") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Account Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAccountTitle" Text='<%# Eval("AccountTitle") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblCredit" Text="Total Credit Amount : " runat="server" HorizontalAlign="Right" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblCreditTotal" Text="0" runat="server" HorizontalAlign="Left" />
                                                                    </FooterTemplate>
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
                                                        <asp:SqlDataSource ID="dsCrAccount" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>" OnSelecting="dsCrAccount_Selecting" 
                                                            SelectCommand="SELECT ROW_NUMBER() OVER (ORDER BY td.TransDID) As SlNo, td.TransDID, td.TransMID, td.AccountID, ta.AccountNo, ta.AccountTitle, td.CreditAmt, td.DebitAmt, td.Comments
                                                                FROM T_Transaction_Detail AS td INNER JOIN
                                                                    T_Account AS ta ON td.AccountID = ta.AccountID 
                                                                WHERE (td.TransMID = @TransMID)">
                                                            <SelectParameters>
                                                                <asp:Parameter Name="TransMID" Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>    
                                </tr>
                                <tr>
                                    <td align="left" valign="top">Description : </td>  
                                    <td align="left" colspan="3">
                                        <asp:TextBox  runat="server" ID="txtCrDesc" TextMode="MultiLine" Width="806px" ></asp:TextBox>
                                    </td>
                                </tr>                
                                <tr>
                                    <td align="left">
                                        <asp:CheckBox runat="server" ID="chkCrAppvBy" Text="Aproved By : " />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtCrAppvBy"></asp:TextBox>
                                    </td>
                                    <td align="left">Approved Date : </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtCrAppvDate"></asp:TextBox>
                                        &nbsp;&nbsp;
                                        <cc1:CalendarExtender ID="txtCrAppvDate_CalendarExtender" runat="server" 
                                            Enabled="True" TargetControlID="txtCrAppvDate" PopupPosition="BottomLeft">
                                        </cc1:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="reValidator4" runat="server" 
                                            ControlToValidate="txtCrAppvDate" ErrorMessage="*" 
                                            ValidationExpression="^[0-9d]{1,2}/[0-9m]{1,2}/[0-9y]{4}$">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>	                                
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    </tbody>
                </table>

                <div class="feature-box-actionBar">
                    <span class="failureNotification">
                        <asp:Literal ID="lblCrFailure" runat="server"></asp:Literal>
                    </span>

                    <asp:Button ID="Button2" runat="server" CssClass="button" Text="Delete" />

                    <asp:Button ID="btnCrPrint" runat="server" CssClass="button" Text="Print" OnClick="btnCrPrint_Click" />

                    <asp:Button ID="btnCrPost" runat="server" CssClass="button" Text="Post" OnClick="btnCrPost_Click" />

                    <asp:Button ID="Button5" runat="server" CssClass="button" Text="Reset" />
                </div>
            </asp:View>
            <asp:View ID="ViewJournal" runat="server">
                <table width="100%" border="0" cellpadding="0" cellspacing="4">
                    <tbody>
                    <tr>
                        <td align="left" valign="top" style="width:50%;">
                            <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                <tbody>
                                <tr>
                                    <td align="left" style="width:20%;">Voucher No : </td>
                                    <td align="left" style="width:30%;">
                                        <asp:TextBox runat="server" ID="txtJrVoucherNo"></asp:TextBox>
                                    </td>
                                    <td align="left" style="width:20%;">Date : </td>
                                    <td align="left" style="width:30%;">
                                        <asp:TextBox runat="server" ID="txtJrDate"></asp:TextBox>
                                        &nbsp;&nbsp;
                                        <cc1:CalendarExtender ID="txtJrDate_CalendarExtender" runat="server" 
                                            Enabled="True" TargetControlID="txtJrDate" PopupPosition="BottomLeft">
                                        </cc1:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="reValidator5" runat="server" 
                                            ControlToValidate="txtJrDate" ErrorMessage="*" 
                                            ValidationExpression="^[0-9d]{1,2}/[0-9m]{1,2}/[0-9y]{4}$">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="background-color:#99CCFF;">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                            <tbody>
                                                <tr>
                                                    <td colspan="4">
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                                            <tbody>
                                                            <tr>
                                                                <td align="left" style="width:20%;">A/C No</td>
                                                                <td align="left" style="width:40%;">A/C Name</td>
                                                                <td align="left" style="width:15%;">Debit Amount</td>
                                                                <td align="left" style="width:25%;">Credit Amount</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="width:20%;">
                                                                    <asp:TextBox runat="server" ID="txtJrAccountNo"></asp:TextBox>
                                                                </td>
                                                                <td align="left" style="width:30%;">
                                                                    <asp:TextBox runat="server" ID="txtJrAccount" Width="360px"></asp:TextBox>
                                                                    <asp:ImageButton ID="imgJrAccount" runat="server" CommandName="NewJrAccount" ImageUrl="~/App_Themes/Default/Images/gridview/New.png"
                                                                        ToolTip="Account Title" Width="16px" Height="16px" />
                                                                    <cc1:ModalPopupExtender ID="ModalPopupExtender6" BackgroundCssClass="ModalPopupBG"
                                                                        runat="server" CancelControlID="ButtonJrAccountCancel" OkControlID="ButtonJrAccountDone"
                                                                        TargetControlID="imgJrAccount" PopupControlID="DivJrAccountWindow">
                                                                    </cc1:ModalPopupExtender>
                                                                    <div class="popup_Buttons" style="display: none">
                                                                        <input id="ButtonJrAccountDone" value="Done" type="button" />
                                                                        <input id="ButtonJrAccountCancel" value="Cancel" type="button" />
                                                                    </div>
                                                                    <div id="DivJrAccountWindow" style="display: none;" class="popupVoucherFor">
                                                                        <div class="popup_Container">
                                                                            <div class="popup_Titlebar" id="Div5">
                                                                                <div class="TitlebarLeft">
                                                                                    Account Title
                                                                                </div>
                                                                                <div class="TitlebarRight" onclick="JrAccountcancel();"></div>
                                                                            </div>
                                                                            <div class="popup_Body" style="overflow:auto;">
                                                                                <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td align="left">
                                                                                                Search Account Title A/C : <asp:TextBox runat="server" ID="txtSearchJrAccount" Width="202px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td align="center" valign="bottom" style="width:3%;">
                                                                                                <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png" ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" colspan="2">
                                                                                                <asp:GridView ID="grvJrAccountNo" DataKeyNames="AccountID" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                                                    CellPadding="4" HorizontalAlign="Left"  OnPageIndexChanging="grvJrAccountNo_PageIndexChanging" ShowHeaderWhenEmpty="true" 
                                                                                                    OnRowDataBound="grvJrAccountNo_RowDataBound" OnRowCommand="grvJrAccountNo_RowCommand"
                                                                                                    EmptyDataText="No Records Found" CssClass="tablesorterBlue" DataSourceID="dsJrAccountNo">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:ImageButton ID="imgSelectJrAccount" runat="server" CommandName="Select" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png" ToolTip="Edit" />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="AccountID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblAccountID" Text='<%# Eval("AccountID") %>' runat="server" HorizontalAlign="Left" />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Acc No" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblAccountNo" Text='<%# Eval("AccountNo") %>' runat="server" HorizontalAlign="Left" />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Account Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblAccountTitle" Text='<%# HighlightText(Eval("AccountTitle").ToString()) %>' runat="server" HorizontalAlign="Left" />
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
                                                                                                <asp:SqlDataSource ID="dsJrAccountNo" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>" 
                                                                                                    SelectCommand="SELECT AccountID, AccountNo, AccountTitle
                                                                                                        FROM T_Account
                                                                                                        WHERE (AccDepth = 2)" FilterExpression="AccountTitle LIKE '%{0}%'">
                                                                                                    <FilterParameters>
                                                                                                        <asp:ControlParameter Name="AccountTitle" ControlID="txtSearchDrPayment" PropertyName="Text" />
                                                                                                    </FilterParameters>
                                                                                                </asp:SqlDataSource>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                            <div class="popup_Buttons" style="display:none;">
                                                                                <%--<input id="btnOkay" value="Done" type="button" onclick="cancel();" />--%>
                                                                                <input id="Button6" value="Cancel" type="button" onclick="JrAccountcancel();" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                                <td align="left" style="width:15%;">
                                                                    <asp:TextBox runat="server" ID="txtCrAmount"></asp:TextBox>
                                                                </td>
                                                                <td align="left" style="width:25%;">
                                                                    <asp:TextBox runat="server" ID="txtDrAmount"></asp:TextBox>
                                                                    &nbsp;
                                                                    <asp:Button runat="server" ID="btnJrAdd" Text="   Add   " OnClick="btnJrAdd_Click" />
                                                                </td>
                                                            </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="4">
                                                        <asp:GridView ID="grvJrAccount" DataKeyNames="AccountID" runat="server" Width="100%" AutoGenerateColumns="False"
                                                            CellPadding="4" HorizontalAlign="Left" ShowHeaderWhenEmpty="true" ShowFooter="true" OnRowDataBound="grvJrAccount_RowDataBound" OnRowCommand="grvJrAccount_RowCommand"  
                                                            EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True" PageSize="5" DataSourceID="dsJrAccount">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSl" Text='<%# Eval("SlNo") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TransDID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTransDID" Text='<%# Eval("TransDID") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TransMID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTransMID" Text='<%# Eval("TransMID") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="AccountID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAccountID" Text='<%# Eval("AccountID") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Acc No" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAccountNo" Text='<%# Eval("AccountNo") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Account Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAccountTitle" Text='<%# Eval("AccountTitle") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblCredit" Text="Total Amount : " runat="server" HorizontalAlign="Right" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cr Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblCreditTotal" Text="0" runat="server" HorizontalAlign="Left" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Dr Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDebitAmt" Text='<%# Eval("DebitAmt") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblDebitTotal" Text="0" runat="server" HorizontalAlign="Left" />
                                                                    </FooterTemplate>
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
                                                        <asp:SqlDataSource ID="dsJrAccount" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>" OnSelecting="dsJrAccount_Selecting" 
                                                            SelectCommand="SELECT ROW_NUMBER() OVER (ORDER BY td.TransDID) As SlNo, td.TransDID, td.TransMID, td.AccountID, ta.AccountNo, ta.AccountTitle, td.CreditAmt, td.DebitAmt, td.Comments
                                                                FROM T_Transaction_Detail AS td INNER JOIN
                                                                    T_Account AS ta ON td.AccountID = ta.AccountID 
                                                                WHERE (td.TransMID = @TransMID)">
                                                            <SelectParameters>
                                                                <asp:Parameter Name="TransMID" Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>    
                                </tr>
                                <tr>
                                    <td align="left" valign="top">Description : </td>  
                                    <td align="left" colspan="3">
                                        <asp:TextBox  runat="server" ID="txtJrDesc" TextMode="MultiLine" Width="806px" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:CheckBox runat="server" ID="chkJrAppvBy" Text="Aproved By : " />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtJrAppvBy"></asp:TextBox>
                                    </td>
                                    <td align="left">Approved Date : </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtJrAppvDate"></asp:TextBox>
                                        &nbsp;&nbsp;
                                        <cc1:CalendarExtender ID="txtJrAppvDate_CalendarExtender" runat="server" 
                                            Enabled="True" TargetControlID="txtJrAppvDate" PopupPosition="BottomLeft">
                                        </cc1:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="reValidator6" runat="server" 
                                            ControlToValidate="txtJrAppvDate" ErrorMessage="*" 
                                            ValidationExpression="^[0-9d]{1,2}/[0-9m]{1,2}/[0-9y]{4}$">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>	                                
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    </tbody>
                </table>

                <div class="feature-box-actionBar">
                    <span class="failureNotification">
                        <asp:Literal ID="lblJrFailure" runat="server"></asp:Literal>
                    </span>

                    <asp:Button ID="btnJrDelete" runat="server" CssClass="button" Text="Delete" />

                    <asp:Button ID="btnJrPrint" runat="server" CssClass="button" Text="Print" OnClick="btnJrPrint_Click" />

                    <asp:Button ID="btnJrPost" runat="server" CssClass="button" Text="Post" OnClick="btnJrPost_Click" />

                    <asp:Button ID="btnJrReset" runat="server" CssClass="button" Text="Reset" />
                </div>
            </asp:View>
        </asp:MultiView>
    </div>

    <%--this is the modal popup for the delete confirmation--%>
    <asp:Panel runat="server" ID="DivDeleteConfirmation" Style="display: none;" class="popupConfirmation">
        <div class="popup_Container">
            <div class="popup_Titlebar">
                <div class="TitlebarLeft">
                    Delete Voucher</div>
                <div class="TitlebarRight" onclick="$get('ButtonDeleteCancel').click();">
                </div>
            </div>
            <div class="popup_Body">
                <div style="margin: 10px auto 10px 20px;">
                    Are you sure, you want to delete?
                </div>
            </div>
            <div class="popup_Buttons">
                <input id="ButtonDeleleOkay" value="Yes" type="button" />
                <input id="ButtonDeleteCancel" value="No" type="button" />
            </div>
        </div>
    </asp:Panel>

</asp:Content>
