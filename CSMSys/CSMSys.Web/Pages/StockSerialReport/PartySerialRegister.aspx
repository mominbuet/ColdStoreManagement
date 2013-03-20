<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PartySerialRegister.aspx.cs"
    Inherits="CSMSys.Web.Pages.StockSerialReport.PartySerialRegister" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  
    <link href="~/App_Themes/Default/Styles/Default.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
        function getPrint(print_area) {
            //Creating new page
            var pp = window.open();
            //Adding HTML opening tag with <HEAD> … </HEAD> portion 
            pp.document.writeln('<HTML><HEAD><title>Print Preview</title>')
            pp.document.writeln('<LINK href=Styles.css type="text/css" rel="stylesheet">')
            pp.document.writeln('<LINK href=PrintStyle.css ' +
                        'type="text/css" rel="stylesheet" media="print">')
            pp.document.writeln('<base target="_self"></HEAD>')

            //Adding Body Tag
            pp.document.writeln('<body MS_POSITIONING="GridLayout" bottomMargin="0"');
            pp.document.writeln(' leftMargin="0" topMargin="0" rightMargin="0">');
            //Adding form Tag
            pp.document.writeln('<form method="post">');

            //Creating two buttons Print and Close within a HTML table
            pp.document.writeln('<TABLE width=100%><TR><TD></TD></TR><TR><TD align=right>');
            pp.document.writeln('<INPUT ID="PRINT" type="button" value="Print" ');
            pp.document.writeln('onclick="javascript:location.reload(true);window.print();">');
            pp.document.writeln('<INPUT ID="CLOSE" type="button" ' +
                        'value="Close" onclick="window.close();">');
            pp.document.writeln('</TD></TR><TR><TD></TD></TR></TABLE>');

            //Writing print area of the calling page
            pp.document.writeln(document.getElementById(print_area).innerHTML);
            //Ending Tag of </form>, </body> and </HTML>
            pp.document.writeln('</form></body></HTML>');
        }

function btnDrPrint_onclick() {

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
                                    <asp:Label ID="lblSubHeader" runat="server" Text="পার্টির সিরিয়াল রেজিস্টার" Font-Bold="True"
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
                                <asp:GridView ID="grvSerialRegister" DataKeyNames="SerialID" runat="server" Width="100%"
                                    AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvSerialRegister_PageIndexChanging"
                                    ShowHeaderWhenEmpty="True" 
                                    EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True" 
                                                        DataSourceID="dsStockSerial" PageSize="35">
                                    <Columns>
                                        <asp:BoundField DataField="SlNo" HeaderText="ক্রঃনং" ReadOnly="True" 
                                            SortExpression="SlNo" />
                                        <asp:BoundField DataField="SerialNo" HeaderText="সিরিয়াল নম্বর" 
                                            SortExpression="SerialNo" />
                                        <asp:BoundField DataField="Bags" HeaderText="ব্যাগ সংখ্যা" 
                                            SortExpression="Bags" />
                                        <asp:BoundField DataField="PartyCode" HeaderText="পার্টি কোড" 
                                            SortExpression="PartyCode" />
                                        <asp:BoundField DataField="PartyName" HeaderText="পার্টির নাম" 
                                            SortExpression="PartyName" />
                                        <asp:BoundField DataField="Remarks" HeaderText="মন্তব্য" 
                                            SortExpression="Remarks" />
                                        <asp:TemplateField HeaderText="স্বাক্ষর "></asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" Font-Bold="true" Font-Underline="false" BackColor="#e6EEEE" />
                                    <AlternatingRowStyle BackColor="#E5EAE8" />
                                    <EditRowStyle BackColor="#999999" />
                                    <EmptyDataRowStyle ForeColor="#CC0000" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="dsStockSerial" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>"
                                    SelectCommand="SP_Party_Serial_Report"
                                    SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="FromDate" QueryStringField="FD" 
                                            Type="DateTime" />
                                        <asp:QueryStringParameter DefaultValue="" Name="ToDate" QueryStringField="TD" 
                                            Type="DateTime" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                                            <tr>
                                                <td align="left">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
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
            <input id="btnDrPrint" type="button" class="button" value="Print" onclick="getPrint(printReport);" onclick="return btnDrPrint_onclick()" />
        </div>
    </div>
    </form>
</body>
</html>
