<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PartyWiseLedger.aspx.cs"
    Inherits="CSMSys.Web.Pages.PartyLedger.PartyWiseLedger" %>

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
        <div id="printBagLoan" class="VerticalyMiddle">
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
                                    <asp:Label ID="lblSubHeader" runat="server" Text="Party Ledger" Font-Bold="True"
                                        Font-Size="Medium" Style="text-decoration: underline"></asp:Label>
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
                                                <td align="left" colspan="2">
                                                    তারিখ :
                                                    <asp:Label runat="server" ID="lblDate"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    পার্টির নাম :
                                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    পার্টি কোড নং:
                                                    <asp:Label runat="server" ID="lblCode"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    পার্টির ধরন :
                                                    <asp:Label runat="server" ID="lblPartyType"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    পিতার নাম:
                                                    <asp:Label runat="server" ID="lblFName"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 25%;">
                                                    গ্রাম :
                                                    <asp:Label runat="server" ID="lblVillage"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%;">
                                                    ডাক :
                                                    <asp:Label runat="server" ID="lblPO"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%;">
                                                    উপজেলা :
                                                    <asp:Label runat="server" ID="lblUpazilla"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%;">
                                                    জেলা :
                                                    <asp:Label runat="server" ID="lblDistrict"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="2">
                                                    <strong>তারিখ</strong> :
                                                    <asp:Label ID="lblFromDate" runat="server"></asp:Label>
                                                    <strong>থেকে</strong>
                                                    <asp:Label ID="lblToDate" runat="server"></asp:Label>
                                                    <strong>পর্যন্ত</strong>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <asp:GridView ID="grvSerialRegister" runat="server" Width="100%"
                                        AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvSerialRegister_PageIndexChanging"
                                        ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="tablesorterBlue"
                                        AllowPaging="True" DataSourceID="dsPartyWiseLedger" PageSize="35">
                                        <PagerStyle HorizontalAlign="Right" Font-Bold="true" Font-Underline="false" BackColor="#e6EEEE" />
                                        <AlternatingRowStyle BackColor="#E5EAE8" />
                                        <Columns>
                                            <asp:BoundField DataField="IndividualDate" HeaderText="তারিখ" ReadOnly="True" 
                                                SortExpression="IndividualDate" />
                                            <asp:BoundField DataField="PartyID" HeaderText="PartyID" InsertVisible="False" 
                                                ReadOnly="True" SortExpression="PartyID" Visible="False" />
                                            <asp:BoundField DataField="BagLoaded" HeaderText="ব্যাগ লোড" ReadOnly="True" 
                                                SortExpression="BagLoaded" />
                                            <asp:BoundField DataField="BagNumber" HeaderText="খালি বস্তা সংখ্যা" 
                                                ReadOnly="True" SortExpression="BagNumber" />
                                            <asp:BoundField DataField="BagLoan" HeaderText="ব্যাগ লোন" ReadOnly="True" 
                                                SortExpression="BagLoan" />
                                            <asp:BoundField DataField="CarryingLoan" HeaderText="ক্যারিং লোন" 
                                                ReadOnly="True" SortExpression="CarryingLoan" />
                                            <asp:BoundField DataField="LoanAmount" HeaderText="লোন" ReadOnly="True" 
                                                SortExpression="LoanAmount" />
                                            <asp:BoundField DataField="TotalLoan" HeaderText="মোট লোন" ReadOnly="True" 
                                                SortExpression="TotalLoan" />
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <EmptyDataRowStyle ForeColor="#CC0000" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="dsPartyWiseLedger" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>"
                                        SelectCommand="SP_PartyWise_ledger" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:QueryStringParameter DefaultValue="" Name="PartyID" QueryStringField="PID" Type="Int32" />
                                            <asp:QueryStringParameter DefaultValue="" Name="FromDate" QueryStringField="FD" 
                                                Type="DateTime" />
                                            <asp:QueryStringParameter DefaultValue="" Name="ToDate" QueryStringField="TD" 
                                                Type="DateTime" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    </td>
                            </tr>
                            <tr>
                                <td align="right">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <table border="0" cellpadding="0" cellspacing="4" width="100%">
                                        <tbody>
                                            <tr>
                                                <td align="center" style="width: 25%;">
                                                    পার্টির স্বাক্ষর
                                                </td>
                                                <td align="center" style="width: 25%;">
                                                    অফিস সহকারীর স্বাক্ষর
                                                </td>
                                                <td align="center" style="width: 25%;">
                                                    যাচাইকারীর স্বাক্ষর
                                                </td>
                                                <td align="center" style="width: 25%;">
                                                    মহাব্যবস্থাপকের স্বাক্ষর
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;
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
            <%--<input id="btnDrPrint" type="button" class="button" value="Print" onclick="printDiv(printBagLoan);" />--%>
        </div>
    </div>
    </form>
</body>
</html>
