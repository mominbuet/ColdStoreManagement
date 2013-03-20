<%@ Page Language="C#" Title="CSMSys :: Bag Loan Report" AutoEventWireup="true" CodeBehind="ReportBagLoan.aspx.cs" Inherits="CSMSys.Web.Pages.BagLoanReport.ReportBagLoan" %>

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
                            <td align="center" valign="top" style="font-size:8px;">
                                বিসমিল্লাহির রাহমানির রাহিম
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top" style="font-size:26px;">
                                শাহ্‌ ইসমাঈল গাজী (রহঃ) কোল্ড ষ্টোরেজ লিঃ
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top" style="font-size:8px;">
                                সায়েকপুর, খেজমতপুর, পীরগঞ্জ, রংপুর। মোবাঃ ০১৭১৫৫৯৭৫৯১,০১৭১৯৭০৮৬২১, ০১৭৩২১১২১৯৬, হেড অফিসঃ ধানমণ্ডি, ঢাকা
                            </td>
                        </tr>
                            <tr>
                                <td align="center" valign="top">
                                    <asp:Label ID="lblVoucher" runat="server" Text="খালি বস্তা প্রাপ্তি রশিদ" 
                                        Font-Bold="True" Font-Size="Medium" style="text-decoration: underline"></asp:Label>
                                </td>
                            </tr>
                           
                            <tr>
                                <td align="left" valign="top">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                        <tbody>
                                        <tr>
                                         <td align="left" colspan="2">ব্যাগ লোন নং : 
                                                <asp:Label runat="server" ID="lblBagLoanID"></asp:Label>
                                            </td>
                                            <td align="right" colspan="2">তারিখ : 
                                                <asp:Label runat="server" ID="lblDate"></asp:Label>
                                            </td>
                                           
                                        </tr>
                                        <tr>
                                            <td align="left">পার্টির নাম : 
                                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                            </td>
                                  
                                            <td align="left">পার্টি কোড নং: 
                                                <asp:Label runat="server" ID="lblCode"></asp:Label>
                                            </td>
                                            
                                            <td align="left">পার্টির ধরন : 
                                                <asp:Label runat="server" ID="lblPartyType"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr >
                                                  <td align="left">পিতার নাম: 
                                                <asp:Label runat="server" ID="lblFName"></asp:Label>
                                            </td>
                                            <td align="left" style="width:25%;">গ্রাম : 
                                                <asp:Label runat="server" ID="lblVillage"></asp:Label>
                                            </td>
                                            <td align="left" style="width:25%;">ডাক : 
                                                <asp:Label runat="server" ID="lblPO"></asp:Label>
                                                </td>

                                        </tr>
                                        <tr>
                                                                                                                     
                                                <td align="left" style="width:25%;">উপজেলা : 
                                                <asp:Label runat="server" ID="lblUpazilla"></asp:Label>
                                            </td>
                                            <td align="left" style="width:25%;">জেলা : 
                                                <asp:Label runat="server" ID="lblDistrict"></asp:Label>
                                            </td>
                                            <td align="left" > খালি বস্তার সংখ্যা :
                                                <asp:Label runat="server" ID="lblBag"></asp:Label>
                                            </td>

                                        </tr>
                                        <tr>

                                            <td align="left" colspan="2">বস্তা সংখ্যা
                                                <asp:Label ID="lblBagNo" runat="server"></asp:Label>
                                                বুঝিয়া পাইলাম।
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
                                            <td align="left" colspan="4">
                                                <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                                    <tbody>
                                                    <tr>
                                                        <td align="center" style="width:25%;">পার্টির স্বাক্ষর</td>
                                                        <td align="center" style="width:25%;">অফিস সহকারীর স্বাক্ষর</td>
                                                        <td align="center" style="width:25%;">যাচাইকারীর স্বাক্ষর</td>
                                                        <td align="center" style="width:25%;">মহাব্যবস্থাপকের স্বাক্ষর</td>
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
