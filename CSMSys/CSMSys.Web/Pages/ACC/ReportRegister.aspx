<%@ Page Title="Voucher Register" Language="C#" AutoEventWireup="true" CodeBehind="ReportRegister.aspx.cs" Inherits="CSMSys.Web.Pages.ACC.ReportRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Voucher Register</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="feature-box-full">
        <table width="937px" border="0" cellpadding="0" cellspacing="4">
            <tbody>
            <tr>
                <td align="center" valign="top" style="font-size:10px;">
                    বিসমিল্লাহির রাহমানির রাহিম
                </td>
            </tr>
            <tr>
                <td align="center" valign="top" style="font-size:28px;">
                    শাহ্‌ ইসমাঈল গাজী (রহঃ) কোল্ড ষ্টোরেজ লিঃ
                </td>
            </tr>
            <tr>
                <td align="center" valign="top" style="font-size:10px;">
                    সায়েকপুর, খেজমতপুর, পীরগঞ্জ, রংপুর। মোবাঃ ০১৭১৫৫৯৭৫৯১,০১৭১৯০৮৬২১, ০১৭৩২১১২১৯৬, হেড অফিসঃ ধানমণ্ডি, ঢাকা
                </td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    <asp:Label ID="lblRegister" runat="server" Text="Voucher Register" Font-Bold="True" Font-Size="Larger"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">Date From : 
                    <asp:TextBox runat="server" ID="txtDateFrom" Width="72px"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" 
                        Enabled="True" TargetControlID="txtDateFrom" PopupPosition="BottomLeft">
                    </cc1:CalendarExtender>
                    &nbsp;&nbsp;To&nbsp;&nbsp;
                    <asp:TextBox runat="server" ID="txtDateTo" Width="72px"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" 
                        Enabled="True" TargetControlID="txtDateTo" PopupPosition="BottomLeft">
                    </cc1:CalendarExtender>
                    &nbsp;&nbsp;
                    <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png" ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                </td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    <asp:RegularExpressionValidator ID="reValidator1" runat="server" 
                        ControlToValidate="txtDateFrom" ErrorMessage="Invalid Date" ForeColor="Red" 
                        ValidationExpression="^[0-9d]{1,2}/[0-9m]{1,2}/[0-9y]{4}$">
                    </asp:RegularExpressionValidator>
                    &nbsp;&nbsp;
                    <asp:RegularExpressionValidator ID="reValidator2" runat="server" 
                        ControlToValidate="txtDateTo" ErrorMessage="Invalid Date" ForeColor="Red" 
                        ValidationExpression="^[0-9d]{1,2}/[0-9m]{1,2}/[0-9y]{4}$">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    <table width="100%" border="0" cellpadding="0" cellspacing="4">
                        <tbody>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:GridView ID="grvRegister" DataKeyNames="TransDID" runat="server" Width="100%" AutoGenerateColumns="False" GridLines="Both"
                                    CellPadding="4" HorizontalAlign="Left" ShowHeader="true" ShowHeaderWhenEmpty="true" OnPreRender="grvRegister_PreRender"
                                    EmptyDataText="No Records Found" DataSourceID="dsRegister" OnRowDataBound="grvRegister_RowDataBound" ShowFooter="true">
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
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransDate" Text='<%# Eval("TransDate","{0:dd/MM/yyyy}") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Voucher No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherNo" Text='<%# Eval("VoucherNo") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransDescription" Text='<%# Eval("TransDescription") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AccountID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccountID" Text='<%# Eval("AccountID") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Acc No" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccountNo" Text='<%# Eval("AccountNo") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Account" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccount" Text='<%# Eval("Account") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cr Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt","{0:N}") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblCreditTotal" Text="0" runat="server" HorizontalAlign="Right" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dr Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebitAmt" Text='<%# Eval("DebitAmt","{0:N}") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblDebitTotal" Text="0" runat="server" HorizontalAlign="Right" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle ForeColor="#CC0000" />
                                    <FooterStyle Font-Bold="True" ForeColor="#333333" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="dsRegister" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>" OnSelecting="dsRegister_Selecting" 
                                    SelectCommand="SELECT ROW_NUMBER() OVER (ORDER BY td.TransMID) As SlNo, tm.TransMID, td.TransDID, tm.TransDate, tm.VoucherNo, tm.TransDescription, 
                                        td.AccountID, dbo.T_Account.AccountNo, dbo.T_Account.AccountNo + ' - ' + dbo.T_Account.AccountTitle AS Account, td.CreditAmt, td.DebitAmt
                                        FROM T_Transaction_Master AS tm INNER JOIN
                                        T_Transaction_Detail AS td ON tm.TransMID = td.TransMID INNER JOIN
                                        T_Account ON td.AccountID = dbo.T_Account.AccountID 
                                        WHERE ((tm.TransDate >= @DateFrom) AND (tm.TransDate <= @DateTo))">
                                    <SelectParameters>
                                        <asp:Parameter Name="DateFrom" Type="DateTime" />
                                        <asp:Parameter Name="DateTo" Type="DateTime" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
