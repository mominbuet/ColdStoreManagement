<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PartyLedgerReport.aspx.cs"
    Inherits="CSMSys.Web.Pages.PartyLedger.PartyLedgerReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/App_Themes/Default/Styles/Default.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
        function btnDrPrint_onclick() {

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="feature-box-full">
        <div id="printReport" class="VerticalyMiddle">
            <asp:MultiView ID="MultiViewBagLoan" runat="server">
                <asp:View ID="ViewBagLoan" runat="server">
                    <table width="100%" border="0" cellpadding="0" cellspacing="4">
                        <tbody>
                            <tr>
                                <td align="center" valign="top" style="font-size: 10px;">
                                    বিসমিল্লাহির রাহমানির রাহিম
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top" style="font-size: 28px;">
                                    শাহ্‌ ইসমাঈল গাজী (রহঃ) কোল্ড ষ্টোরেজ লিঃ
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top" style="font-size: 10px;">
                                    সায়েকপুর, খেজমতপুর, পীরগঞ্জ, রংপুর। মোবাঃ ০১৭১৫৫৯৭৫৯১,০১৭১৯৭০৮৬২১, ০১৭৩২১১২১৯৬, হেড
                                    অফিসঃ ধানমণ্ডি, ঢাকা
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top">
                                    <asp:Label ID="lblSubHeader" runat="server" Font-Bold="True" Font-Size="Medium" Style="text-decoration: underline"
                                        Text="Party Ledger"></asp:Label>
                                </td>
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
                                                    <td align="left">
                                                        <strong>তারিখ</strong> :
                                                        <asp:Label runat="server" ID="lblFromDate"></asp:Label>
                                                        <strong>থেকে</strong>
                                                        <asp:Label runat="server" ID="lblToDate"></asp:Label>
                                                        <strong>পর্যন্ত</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                         <asp:GridView ID="grvPartyLedger" DataKeyNames="PartyID" runat="server" Width="100%"
                                                            AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvPartyLedger_PageIndexChanging"
                                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="tablesorterBlue"
                                                             DataSourceID="dsPartyLedger">
                                                            <Columns>
                                                                <asp:BoundField DataField="PartyCode" HeaderText="পার্টি কোড " SortExpression="PartyCode" />
                                                                <asp:BoundField DataField="PartyName" HeaderText="পার্টির নাম " SortExpression="PartyName" />
                                                                <asp:BoundField DataField="BagLoaded" HeaderText="লোড বস্তা " ReadOnly="True" SortExpression="BagLoaded" />
                                                                <asp:BoundField DataField="TotalBagLoan" HeaderText="খালি বস্তার সংখ্যা" 
                                                                    ReadOnly="True" SortExpression="TotalBagLoan" Visible="false"/>
                                                                <asp:BoundField DataField="BagLoanAmount" HeaderText="বস্তা লোন" 
                                                                    SortExpression="BagLoanAmount" />
                                                                <asp:BoundField DataField="TotalCarry" HeaderText="ক্যারিং লোন" ReadOnly="True" SortExpression="TotalCarry" />
                                                                <asp:BoundField DataField="Loan" HeaderText="লোন " ReadOnly="True" SortExpression="Loan" />
                                                                <asp:BoundField DataField="TotalLoan" HeaderText="মোট লোন" ReadOnly="True" SortExpression="TotalLoan" />
                                                            </Columns>
                                                            <PagerStyle HorizontalAlign="Right" Font-Bold="true" Font-Underline="false" BackColor="#e6EEEE" />
                                                            <AlternatingRowStyle BackColor="#E5EAE8" />
                                                            <EditRowStyle BackColor="#999999" />
                                                            <EmptyDataRowStyle ForeColor="#CC0000" />
                                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="#333333" />
                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        </asp:GridView>
                                                        <asp:SqlDataSource ID="dsPartyLedger" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>"
                                                            SelectCommand="SP_Party_ledger" SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:QueryStringParameter Name="FromDate" QueryStringField="FD" Type="DateTime" />
                                                                <asp:QueryStringParameter Name="ToDate" QueryStringField="TD" Type="DateTime" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
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
        <div>
            <span class="failureNotification">
                <asp:Literal ID="lblDrFailure" runat="server"></asp:Literal>
            </span>
            <%--            <input id="btnDrPrint" type="button" class="button" value="Print" onclick="printDiv(printReport);" />--%>
        </div>
    </div>
    </form>
</body>
</html>
