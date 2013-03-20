<%@ Page Language="C#" Title="CSMSys :: Bag Loan Report" AutoEventWireup="true" CodeBehind="AllPartyBagLoanReportViewer.aspx.cs" Inherits="CSMSys.Web.Pages.BagLoanRreport.AllPartyBagLoanReportViewer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="Ledger" style="min-height:867px; background-color:Gray; padding:10px 10px 10px 10px;">
            <table width="100%" border="0" cellpadding="0" cellspacing="4" style="background-color:White;">
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
                    <td align="center" valign="top" style="font-size:12px;">
                        সায়েকপুর, খেজমতপুর, পীরগঞ্জ, রংপুর। মোবাঃ ০১৭১৫৫৯৭৫৯১,০১৭১৯৭০৮৬২১, ০১৭৩২১১২১৯৬, হেড অফিসঃ ধানমণ্ডি, ঢাকা
                    </td>
                </tr>
                 
                  <tr>
                    <td align="center" valign="top" style="font-size:12px;">
                        তারিখ <asp:Label ID="lblfrom" runat="server" Text=""></asp:Label>
                        
                        হতে <asp:Label ID="lblto" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top" style="font-size:16px;">
                       খালি বস্তা রেজিস্টার
                    </td>
                </tr>
                <tr></tr>
                <tr></tr>
                <tr>
                    <td align="center" style="font-size:10px;">
                        <asp:GridView ID="grvPreRequisition"  runat="server" Width="100%"
                            AutoGenerateColumns="False" CellPadding="3" HorizontalAlign="Left" ShowHeaderWhenEmpty="true"
                            EmptyDataText="No Records Found" DataSourceID="dsBagloan" AllowPaging="true" PageSize="30">
                            <Columns>
               
                                <asp:TemplateField HeaderText="SL#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="4%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcode" Text='<%# Eval("bagloanid") %>' runat="server" HorizontalAlign="Left" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="4%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcode" Text='<%# Eval("PartyCode") %>' runat="server" HorizontalAlign="Left" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Previous Loans"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpartyname" Text='<%# Eval("PartyName") %>' runat="server" HorizontalAlign="Left" />
                                      </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblcontact" Text='<%# Eval("contactno") %>' runat="server" HorizontalAlign="Left" />                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bags Loaded" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbagloaded" Text='<%# Eval("bagloaded") %>' runat="server" HorizontalAlign="Left" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bags Loaned" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblbagloaned" Text='<%# Eval("bagnumber") %>' runat="server" HorizontalAlign="Left" />                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sum Loaned" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblsumloaned" Text='<%# Eval("smbags") %>' runat="server" HorizontalAlign="Left" />                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Loan" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="4%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcontact" Text='<%# Eval("LoanAmount") %>' runat="server" HorizontalAlign="Center" />                                                               
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" >
                                    <ItemTemplate>
                                        <asp:Label ID="lvlcdate" Text='<%# Eval("createddate") %>' runat="server" HorizontalAlign="Left" />                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Font-Bold="true" Font-Underline="false" BackColor="#e6EEEE" />
                            <EmptyDataRowStyle ForeColor="#CC0000" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="dsBagloan" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>">
                        </asp:SqlDataSource>
                    </td>
                </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
