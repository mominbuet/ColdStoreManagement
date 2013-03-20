<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanReport.aspx.cs" Inherits="CSMSys.Web.Pages.LoanDisburseReport.LoanReport" %>

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
                    <table width="100%"  border="0" cellpadding="0" cellspacing="4">
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
                                                <td align="left">
                                                    দলিল নং :
                                                    <asp:Label runat="server" ID="lblLoanID"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    লোন কেস নং :
                                                    <asp:Label runat="server" ID="lblCaseID"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top" colspan="2">
                                                    <asp:Label ID="lblVoucher" runat="server" Text="শাহ ইসমাঈল গাজী (রহঃ) কোল্ড ষ্টোরেজ লিঃ-এ আলু/আলু বীজ রাখিয়া ঋণ গ্রহণ পত্র"
                                                        Font-Bold="True" Font-Size="Medium" Style="text-decoration: underline"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="2">
                                                    <div>
                                                        <p align="justify">
                                                            উল্লিখিত কোল্ড ষ্টোরেজ আলু সংরক্ষণ রসিদ নং-....................................তারিখ
                                                            <asp:Label ID="lblDate" runat="server"></asp:Label>
                                                            যাহাতে
                                                            <asp:Label ID="lblBagNo" runat="server"></asp:Label>
                                                            বস্তা আলু রাখা আছে তাহা জমা দিয়ে বিপরীত বস্তা প্রতি
                                                            <asp:Label ID="lblAmnt" runat="server"></asp:Label>
                                                            টাকা হারে মোট (অংকে) ....................................... টাকা গ্রহণ করিলাম।
                                                        </p>
                                                    </div>
                                                </td>
                                                <tr>
                                                    <td align="left" colspan="2">
                                                        <p align="justify">
                                                            ঋণের টাকা ব্যাংকে দেয় ১৮% সুদ ও বীমাকৃত অন্যান্য দেয় টাকার পড়তা অনুযায়ী সমস্ত টাকা
                                                            ১লা অক্টোবর এর মধ্যে পরিশোধ করিয়া আলু সংরক্ষণ রসিদ ফেরত লইব। সময় মত ঋণের টাকা, সুদ
                                                            ও অন্যান্য দেয় টাকা পরিশোধ করিতে না পারিলে সরক্ষিত আলুর কোন দাবী দাওয়া করিতে পারিব
                                                            না । যদি কোন সময় দাবী করি তাহা আইন আদালতে অগ্রাহ্য বিবেচিত হইবে। আলুর রসিদ যথাক্রমে
                                                            কোল্ড ষ্টোরেজ সংরক্ষণ প্রাপ্য ভাড়া জমা দিয়া বস্তাসহ আলু যথা সময়ে ফেরত লইব।
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="2">
                                                        আলুর রসিদ যথাক্রমে কোল্ড ষ্টোরেজ সংরক্ষণ প্রাপ্য ভাড়া জমা দিয়া বস্তাসহ আলু যথা সময়ে
                                                        ফেরত লইব।
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table width="100%" border="0">
                                                            <tr>
                                                                <td align="left">
                                                                    পার্টি কোড :
                                                                    <asp:Label ID="lblPartyCode" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    সিরিয়াল নম্বর :
                                                                    <asp:Label ID="lblSerialNo" runat="server" Height="100px" Width="129px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <table width="100%" border="0">
                                                            <tr>
                                                                <td align="left" width="100px">
                                                                    স্বাক্ষর-
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" width="100px">
                                                                    নাম-
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" width="100px">
                                                                    পিতার নাম-
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Label ID="lblFName" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" width="100px">
                                                                    গ্রাম-
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Label ID="lblVillage" runat="server"></asp:Label>
                                                                </td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="100px">
                                                        পোষ্ট-
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblPO" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="100px">
                                                        থানা-
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblUpazilla" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="100px">
                                                        জেলা-
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblDistrict" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                    </table>
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
                                        <table border="0" cellpadding="0" cellspacing="4" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td align="center" style="width: 33%;">
                                                        পার্টির স্বাক্ষর
                                                    </td>
                                                    <td align="center" style="width: 33%;">
                                                        সহকারী ষ্টোর কিপারের স্বাক্ষর
                                                    </td>
                                                    <td align="center" style="width: 33%;">
                                                        প্রধান ষ্টোর কিপারের স্বাক্ষর
                                                    </td>
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
                                </tbody> </table> </td>
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
         <%--   <input id="btnDrPrint" type="button"  class="button" value="Print" onclick="printDiv(printReport);" />--%>
        </div>
    </div>
    </form>
</body>
</html>
