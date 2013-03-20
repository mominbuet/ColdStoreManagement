<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportVoucher.aspx.cs" Inherits="CSMSys.Web.Pages.ACC.ReportVoucher" %>

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
        <div id="Voucher">
            <asp:MultiView ID="MultiViewVoucher" runat="server">
                <asp:View ID="ViewDebit" runat="server">
                    <table width="637px" border="0" cellpadding="0" cellspacing="4" style="height:382px;">
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
                                <asp:Label ID="lblVoucher" runat="server" Text="ডেবিট ভাউচার" Font-Bold="True" Font-Size="Larger"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <%--<tr>
                            <td align="center" valign="top">
                                &nbsp;
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="left" valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                    <tbody>
                                    <tr>
                                        <td align="left" style="width:40%;">Payment Method : 
                                            <asp:Label ID="lblDrPayMethod" runat="server"></asp:Label>
                                        </td>
                                        <td align="right" style="width:60%;">ভাউচার নং : 
                                            <asp:Label runat="server" ID="lblDrVoucherNo"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">Ref No : 
                                            <asp:Label runat="server" ID="lblDrRefNo"></asp:Label>
                                        </td>
                                        <td align="right">তারিখ : 
                                            <asp:Label runat="server" ID="lblDrDate"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">হিসাবের খাত : 
                                            <asp:Label runat="server" ID="lblDrAccount"></asp:Label>
                                        </td>
                                        <td align="right">হিসাব নং : 
                                            <asp:Label runat="server" ID="lblDrAccountNo"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td align="left" colspan="2">
                                            <asp:GridView ID="grvDrAccount" DataKeyNames="AccountID" runat="server" Width="100%" AutoGenerateColumns="False" GridLines="None"
                                                CellPadding="4" HorizontalAlign="Left" ShowHeader="true" ShowFooter="true" OnRowDataBound="grvDrAccount_RowDataBound" 
                                                EmptyDataText="No Records Found" DataSourceID="dsDrAccount">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
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
                                                    <asp:TemplateField HeaderText="Acc No" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAccountNo" Text='<%# Eval("AccountNo") %>' runat="server" HorizontalAlign="Left" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Account Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAccountTitle" Text='<%# Eval("AccountTitle") %>' runat="server" HorizontalAlign="Left" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblDebit" Text="Total Amount : " runat="server" HorizontalAlign="Right" />
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
                                                </Columns>
                                                <EmptyDataRowStyle ForeColor="#CC0000" />
                                                <FooterStyle Font-Bold="True" ForeColor="#333333" />
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
                                    <%--<tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td align="left" valign="top" colspan="2">বিবরন : 
                                            <asp:Label  runat="server" ID="lblDrDesc" ></asp:Label>
                                        </td>
                                    </tr>                
                                    <tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                                <tbody>
                                                <tr>
                                                    <td align="center" style="width:33%;">APPROVED BY</td>
                                                    <td align="center" style="width:33%;">CHEKED BY</td>
                                                    <td align="center" style="width:33%;">PREPARED BY</td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label runat="server" ID="lblDrAppvBy"></asp:Label>
                                                    </td>
                                                    <td align="center">&nbsp;</td>
                                                    <td align="center">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align="center"><asp:Label runat="server" ID="lblDrAppvDate"></asp:Label></td>
                                                    <td align="center">&nbsp;</td>
                                                    <td align="center">&nbsp;</td>
                                                </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        </tbody>
                    </table>
                </asp:View>
                <asp:View ID="ViewCredit" runat="server">
                    <table width="637px" border="0" cellpadding="0" cellspacing="4" style="height:382px;">
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
                                <asp:Label ID="Label1" runat="server" Text="ক্রেডিট ভাউচার" Font-Bold="True" Font-Size="Larger"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <%--<tr>
                            <td align="center" valign="top">
                                &nbsp;
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="left" valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                    <tbody>
                                    <tr>
                                        <td align="left" style="width:40%;">Payment Method : 
                                            <asp:Label ID="lblCrPayMethod" runat="server"></asp:Label>
                                        </td>
                                        <td align="right" style="width:60%;">ভাউচার নং : 
                                            <asp:Label runat="server" ID="lblCrVoucherNo"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">Ref No : 
                                            <asp:Label runat="server" ID="lblCrRefNo"></asp:Label>
                                        </td>
                                        <td align="right">তারিখ : 
                                            <asp:Label runat="server" ID="lblCrDate"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">হিসাবের খাত : 
                                            <asp:Label runat="server" ID="lblCrAccount"></asp:Label>
                                        </td>
                                        <td align="right">হিসাব নং : 
                                            <asp:Label runat="server" ID="lblCrAccountNo"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td align="left" colspan="2">
                                            <asp:GridView ID="grvCrAccount" DataKeyNames="AccountID" runat="server" Width="100%" AutoGenerateColumns="False" GridLines="None"
                                                CellPadding="4" HorizontalAlign="Left" ShowHeader="true" ShowFooter="true" OnRowDataBound="grvCrAccount_RowDataBound" 
                                                EmptyDataText="No Records Found" DataSourceID="dsCrAccount">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
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
                                                    <asp:TemplateField HeaderText="Acc No" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
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
                                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt") %>' runat="server" HorizontalAlign="Left" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblCreditTotal" Text="0" runat="server" HorizontalAlign="Right" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle ForeColor="#CC0000" />
                                                <FooterStyle Font-Bold="True" ForeColor="#333333" />
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
                                    <%--<tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td align="left" valign="top" colspan="2">বিবরন : 
                                            <asp:Label  runat="server" ID="lblCrDesc" ></asp:Label>
                                        </td>
                                    </tr>                
                                    <tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                                <tbody>
                                                <tr>
                                                    <td align="center" style="width:33%;">APPROVED BY</td>
                                                    <td align="center" style="width:33%;">CHEKED BY</td>
                                                    <td align="center" style="width:33%;">PREPARED BY</td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label runat="server" ID="lblCrAppvBy"></asp:Label>
                                                    </td>
                                                    <td align="center">&nbsp;</td>
                                                    <td align="center">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align="center"><asp:Label runat="server" ID="lblCrAppvDate"></asp:Label></td>
                                                    <td align="center">&nbsp;</td>
                                                    <td align="center">&nbsp;</td>
                                                </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        </tbody>
                    </table>
                </asp:View>
                <asp:View ID="ViewJournal" runat="server">
                    <table width="637px" border="0" cellpadding="0" cellspacing="4" style="height:382px;">
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
                                <asp:Label ID="Label11" runat="server" Text="ডেবিট ভাউচার" Font-Bold="True" Font-Size="Larger"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <%--<tr>
                            <td align="center" valign="top">
                                &nbsp;
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="left" valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                    <tbody>
                                    <tr>
                                        <td align="left" style="width:40%;">Payment Method : 
                                            <asp:Label ID="lblJrPayMethod" runat="server"></asp:Label>
                                        </td>
                                        <td align="right" style="width:60%;">ভাউচার নং : 
                                            <asp:Label runat="server" ID="lblJrVoucherNo"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">Ref No : 
                                            <asp:Label runat="server" ID="lblJrRefNo"></asp:Label>
                                        </td>
                                        <td align="right">তারিখ : 
                                            <asp:Label runat="server" ID="lblJrDate"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">হিসাবের খাত : 
                                            <asp:Label runat="server" ID="lblJrAccount"></asp:Label>
                                        </td>
                                        <td align="right">হিসাব নং : 
                                            <asp:Label runat="server" ID="lblJrAccountNo"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            <asp:GridView ID="grvJrAccount" DataKeyNames="AccountID" runat="server" Width="100%" AutoGenerateColumns="False" GridLines="Both"
                                                CellPadding="4" HorizontalAlign="Left" ShowHeader="true" ShowFooter="true" OnRowDataBound="grvJrAccount_RowDataBound" 
                                                EmptyDataText="No Records Found" DataSourceID="dsJrAccount">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
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
                                                    <asp:TemplateField HeaderText="Acc No" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAccountNo" Text='<%# Eval("AccountNo") %>' runat="server" HorizontalAlign="Left" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Account Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAccountTitle" Text='<%# Eval("AccountTitle") %>' runat="server" HorizontalAlign="Left" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cr Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt") %>' runat="server" HorizontalAlign="Left" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblCreditTotal" Text="0" runat="server" HorizontalAlign="Right" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dr Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDebitAmt" Text='<%# Eval("DebitAmt") %>' runat="server" HorizontalAlign="Left" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblDebitTotal" Text="0" runat="server" HorizontalAlign="Right" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle ForeColor="#CC0000" />
                                                <FooterStyle Font-Bold="True" ForeColor="#333333" />
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
                                    <%--<tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td align="left" valign="top" colspan="2">বিবরন : 
                                            <asp:Label  runat="server" ID="lblJrDesc" ></asp:Label>
                                        </td>
                                    </tr>                
                                    <tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                                <tbody>
                                                <tr>
                                                    <td align="center" style="width:33%;">APPROVED BY</td>
                                                    <td align="center" style="width:33%;">CHEKED BY</td>
                                                    <td align="center" style="width:33%;">PREPARED BY</td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label runat="server" ID="lblJrAppvBy"></asp:Label>
                                                    </td>
                                                    <td align="center">&nbsp;</td>
                                                    <td align="center">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align="center"><asp:Label runat="server" ID="lblJrAppvDate"></asp:Label></td>
                                                    <td align="center">&nbsp;</td>
                                                    <td align="center">&nbsp;</td>
                                                </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        </tbody>
                    </table>
                </asp:View>
            </asp:MultiView>
        </div>
        <div id="print">
        </div>
    </div>
    </form>
</body>
</html>
