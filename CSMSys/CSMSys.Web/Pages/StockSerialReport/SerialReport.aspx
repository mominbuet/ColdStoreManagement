<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SerialReport.aspx.cs" Inherits="CSMSys.Web.Pages.StockSerialReport.SerialReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="80%" border="0" cellpadding="0" cellspacing="4">
            <tbody>
                <tr>
                    <td align="center" valign="top" style="font-size: 10px;">
                        বিসমিল্লাহির রাহমানির রাহিম
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top" style="font-size: 25px;">
                        শাহ্‌ ইসমাঈল গাজী (রহঃ) কোল্ড ষ্টোরেজ লিঃ
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top" style="font-size: 10px;">
                        সায়েকপুর, খেজমতপুর, পীরগঞ্জ, রংপুর, হেড অফিসঃ ধানমণ্ডি, ঢাকা
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top" style="font-size: 10px;">
                        মোবাঃ ০১৭১৫৫৯৭৫৯১,০১৭১৯৭০৮৬২১, ০১৭৩২১১২১৯৬
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top" style="font-size: 14px;">
                        আলুর বস্তা প্রাপ্তির রশিদ
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" style="font-size: 12px;">
                        তারিখ:
                        <asp:Label ID="lbldate" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="right" valign="top" style="font-size: 10px;">
                        S/R No:<asp:Label ID="lblsr" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                </tr>
                <tr>
                </tr>
                <tr>
                    <td>
                        ধন্যবাদের সহিত গ্রহিত হইলোঃ
                        <asp:Label ID="lblpartyname" runat="server" Text="" Font-Underline="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        পিতার নামঃ
                        <asp:Label ID="lblfathername" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        গ্রামঃ
                        <asp:Label ID="lblvillage" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        পার্টির কোডঃ
                        <asp:Label ID="lblcode" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        খালি বস্তাঃ ...................
                    </td>
                </tr>
                <tr>
                    <td>
                        এস আর কতৃক বস্তা সংখ্যা
                        <asp:Label ID="lblbagcount" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" style="font-size: 10px;">
                        ........................<br />
                        সহকারী স্টোর কীপারের সাক্ষর
                    </td>
                    <td align="right" valign="top" style="font-size: 10px;">
                        ........................<br />
                        প্রধান স্টোর কীপারের সাক্ষর
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
