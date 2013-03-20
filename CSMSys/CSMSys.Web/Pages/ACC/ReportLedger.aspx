<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="ReportLedger.aspx.cs" Inherits="CSMSys.Web.Pages.ACC.ReportLedger" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="feature-box-full">
        <div id="Search">
            <table width="100%" border="0" cellpadding="0" cellspacing="4">
                <tbody>
                <tr>
                    <td align="left">Account Title : </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlParentLedger" runat="server" Width="202px" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td align="right">Date From : </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="txtDateFrom"></asp:TextBox>
                        &nbsp;&nbsp;
                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtDateFrom" PopupPosition="BottomLeft">
                        </cc1:CalendarExtender>
                        <asp:RegularExpressionValidator ID="reValidator1" runat="server" 
                            ControlToValidate="txtDateFrom" ErrorMessage="Invalid Date" ForeColor="Red" 
                            ValidationExpression="^[0-9d]{1,2}/[0-9m]{1,2}/[0-9y]{4}$">
                        </asp:RegularExpressionValidator>
                    </td>
                    <td align="center">To : </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="txtDateTo"></asp:TextBox>
                        &nbsp;&nbsp;
                        <cc1:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtDateTo" PopupPosition="BottomLeft">
                        </cc1:CalendarExtender>
                        <asp:RegularExpressionValidator ID="reValidator2" runat="server" 
                            ControlToValidate="txtDateTo" ErrorMessage="Invalid Date" ForeColor="Red" 
                            ValidationExpression="^[0-9d]{1,2}/[0-9m]{1,2}/[0-9y]{4}$">
                        </asp:RegularExpressionValidator>
                    </td>
                    <td align="left" valign="bottom" style="width:3%;">
                        <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png" ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                    </td>
                    <td align="left" valign="bottom" style="width:3%;">
                        <asp:ImageButton ID="imgPrint" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/print.png" ToolTip="Search" Width="16px" Height="16px" />
                    </td>
                </tr>
                </tbody>
            </table>
        </div>
        <div id="Ledger" style="background-color:Gray; padding:10px 0 10px 0;">
            <table width="637px" border="0" cellpadding="0" cellspacing="4" style="height:382px; background-color:White; margin-left:162px;">
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
                    <td align="center" style="font-size:28px;">
                        Ledger Book
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top">
                        From <asp:Label ID="lblDateFrom" runat="server"></asp:Label>&nbsp;
                        To <asp:Label ID="lblDateTo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                            <tbody>
                            <tr>
                                <td align="left" colspan="3">একাউন্ট টাইটেল : 
                                    <asp:Label ID="lblAccountTitle" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width:30%;">একাউন্ট নং : 
                                    <asp:Label runat="server" ID="lblAccountNo"></asp:Label>
                                </td>
                                <td align="center" style="width:30%;">একাউন্ট টাইপ : 
                                    <asp:Label runat="server" ID="lblType"></asp:Label>
                                </td>
                                <td align="right">ওপেনিং ব্যালান্স : 
                                    <asp:Label runat="server" ID="lblOpBalance"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <%--<tr>
                                <td align="left" colspan="3">
                                    &nbsp;
                                </td>
                            </tr>--%>
                            <tr>
                                <td align="left" colspan="3">
                                    <asp:GridView ID="grvLedger" DataKeyNames="AccountID" runat="server" Width="100%" AutoGenerateColumns="False" GridLines="Both"
                                        CellPadding="4" HorizontalAlign="Left" ShowHeader="true" ShowFooter="false" OnRowDataBound="grvLedger_RowDataBound" 
                                        EmptyDataText="No Records Found" DataSourceID="dsLedger">
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
                                            <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTransDate" Text='<%# Eval("TransDate","{0:dd/MM/yyyy}") %>' runat="server" HorizontalAlign="Left" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" Text='<%# Eval("Description") %>' runat="server" HorizontalAlign="Left" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblDebit" Text="Total Amount : " runat="server" HorizontalAlign="Right" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cr Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt","{0:N}") %>' runat="server" HorizontalAlign="Left" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblCreditTotal" Text="0" runat="server" HorizontalAlign="Right" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dr Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDebitAmt" Text='<%# Eval("DebitAmt","{0:N}") %>' runat="server" HorizontalAlign="Left" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblDebitTotal" Text="0" runat="server" HorizontalAlign="Right" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLineTotal" Text='<%# Eval("LineTotal","{0:N}") %>' runat="server" HorizontalAlign="Left" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotal" Text="0" runat="server" HorizontalAlign="Right" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle ForeColor="#CC0000" />
                                        <FooterStyle Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="dsLedger" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>" OnSelecting="dsLedger_Selecting" 
                                        SelectCommand="SELECT ROW_NUMBER() OVER (ORDER BY tm.TransMID) As SlNo, tm.TransMID, tm.TransDate, tm.VoucherNo, tm.VoucherType AS VoucherTypeID, vt.VoucherType, tm.TransMethodID, mt.TransMethod, tm.MethodRefID, 
                                            tm.MethodRefNo, tm.TransDescription, td.TransDID, td.AccountID, (ta.AccountNo + '-' + ta.AccountTitle) AS Description, ta.Nature, td.CreditAmt, td.DebitAmt, td.Comments, 
                                            ta.Nature * (td.DebitAmt - td.CreditAmt) AS LineTotal
                                            FROM T_Transaction_Master AS tm INNER JOIN
                                            T_Transaction_Detail AS td ON tm.TransMID = td.TransMID INNER JOIN
                                            T_Account AS ta ON td.AccountID = ta.AccountID LEFT OUTER JOIN
                                            T_VoucherType AS vt ON tm.VoucherType = vt.VoucherTypeID LEFT OUTER JOIN
                                            T_TransactionMethod AS mt ON tm.TransMethodID = mt.TransMethodID 
                                            WHERE ((td.AccountID = @AccountID) AND (tm.TransDate >= @DateFrom) AND (tm.TransDate <= @DateTo))">
                                        <SelectParameters>
                                            <asp:Parameter Name="AccountID" Type="Int32" />
                                            <asp:Parameter Name="DateFrom" Type="DateTime" />
                                            <asp:Parameter Name="DateTo" Type="DateTime" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="3">ক্লোজিং ব্যালান্স : 
                                    <asp:Label runat="server" ID="lblClBalance"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3">
                                    &nbsp;
                                </td>
                            </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                </tbody>
            </table>
        </div>
        <div id="print">
        </div>
    </div>
    </form>
</body>
</html>
