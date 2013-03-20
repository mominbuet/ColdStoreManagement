<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BagLoanRegister.aspx.cs"
    Inherits="CSMSys.Web.Pages.BagLoanReport.BagLoanRegister" %>

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
                                সায়েকপুর, খেজমতপুর, পীরগঞ্জ, রংপুর। মোবাঃ ০১৭১৫৫৯৭৫৯১,০১৭১৯৭০৮৬২১, ০১৭৩২১১২১৯৬, হেড অফিসঃ ধানমণ্ডি, ঢাকা
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                <asp:Label ID="lblSubHeader" runat="server" Font-Bold="True" Font-Size="Medium" 
                                    Style="text-decoration: underline" Text="পার্টির ব্যাগ লোন রেজিস্টার"></asp:Label>
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
                                                <td align="left" colspan="2">
                                                    <strong>তারিখ</strong> :
                                                    <asp:Label runat="server" ID="lblFromDate"></asp:Label>
                                                    <strong>থেকে</strong>
                                                    <asp:Label runat="server" ID="lblToDate"></asp:Label>
                                                    <strong>পর্যন্ত</strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="4">
                                                    <asp:GridView ID="grvBagloanRegister" DataKeyNames="BagLoanID" runat="server" Width="100%"
                                                        AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvBagloanRegister_PageIndexChanging"
                                                        ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="tablesorterBlue"
                                                        AllowPaging="True" PageSize="35" DataSourceID="dsBagloans">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ক্রঃনং">
                                                                <ItemTemplate>
                                                                    <%# ((GridViewRow)Container).RowIndex + 1%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="PartyCode" HeaderText="পার্টি কোড" SortExpression="PartyCode" />
                                                            <asp:BoundField DataField="PartyName" HeaderText="পার্টির নাম" SortExpression="PartyName" />
                                                            <asp:BoundField DataField="Bagloaded" HeaderText="ব্যাগ লোড" SortExpression="Bagloaded" />
                                                            <asp:BoundField DataField="BagNumber" HeaderText="খালি বস্তা" SortExpression="BagNumber" />
                                                            <asp:BoundField DataField="AmountPerBag" HeaderText="বস্তা প্রতি লোন" Visible="false"
                                                                SortExpression="AmountPerBag" />
                                                            <asp:BoundField DataField="LoanAmount" HeaderText="মোট লোন " 
                                                                SortExpression="LoanAmount" />
                                                            <asp:BoundField DataField="Remarks" HeaderText="মন্তব্য" SortExpression="Remarks" />
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Right" Font-Bold="true" Font-Underline="false" BackColor="#e6EEEE" />
                                                        <AlternatingRowStyle BackColor="#E5EAE8" />
                                                        <EditRowStyle BackColor="#999999" />
                                                        <EmptyDataRowStyle ForeColor="#CC0000" />
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="#333333" />
                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    </asp:GridView>
                                                    <asp:SqlDataSource ID="dsBagloans" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>"
                                                        SelectCommand="SP_Party_BagLoan_Report" SelectCommandType="StoredProcedure">
                                                        <SelectParameters>
                                                            <asp:QueryStringParameter Name="FromDate" QueryStringField="FD" Type="DateTime" />
                                                            <asp:QueryStringParameter Name="ToDate" QueryStringField="TD" Type="DateTime" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
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
