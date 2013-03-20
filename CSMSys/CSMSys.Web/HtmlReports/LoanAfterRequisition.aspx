<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanAfterRequisition.aspx.cs" 
Inherits="CSMSys.Web.HtmlReports.LoanAfterRequisition" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CSMSys :: Loan disbursed</title>
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
                    <td align="center" valign="top" style="font-size:15px;">
                        লোন ডিসবারসড
                    </td>
                </tr>
                <tr>
                    <td align="center" style="font-size:10px;">
                        <asp:GridView ID="grvPreRequisition"  runat="server" Width="100%"
                            AutoGenerateColumns="False" CellPadding="3" HorizontalAlign="Left" ShowHeaderWhenEmpty="true"
                            EmptyDataText="No Records Found" DataSourceID="dsParty" OnRowDataBound="grvPreRequisition_bound">
                            <Columns>
               
                                <asp:TemplateField HeaderText="কোড" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcode" Text='<%# Eval("PartyCode") %>' runat="server" Font-Size="13px" HorizontalAlign="Left" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="নাম" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpartyname" Text='<%# Eval("PartyName") %>' runat="server" Font-Size="15px" HorizontalAlign="Left" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="পূবে্র লোন"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                    
                                    <ItemTemplate>
                                       <asp:GridView ID="grvPrevLoan" runat="server" Width="100%"   AutoGenerateColumns="False" CellPadding="3"
                                             HorizontalAlign="Left" GridLines="None">
                                            <Columns>
                            
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText=" "
                                                    ItemStyle-Width="2%" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrevBags" Text='<%# Eval("Bags") %>' runat="server" HorizontalAlign="Center" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center"    HeaderText=" "  ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrevAmount" Text='<%# Eval("Amount") %>' runat="server" HorizontalAlign="Center" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>     
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Right"    HeaderText=" "  ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrevAmount" Text='<%# Eval("mult") %>' runat="server" HorizontalAlign="Center" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ব্যালান্স বস্তা" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" >
                                    <ItemTemplate>
                                         <br />
                                        <asp:GridView id="grdBalance"  runat="server" GridLines="None" Width="30%" CellPadding="3">
                                        </asp:GridView>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="আবেদনকৃত বস্তা" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" >
                                         
                                    <ItemTemplate>
                                    <br />
                                          <asp:GridView ID="grvAppliedBags" runat="server" Width="100%"   AutoGenerateColumns="False" CellPadding="3"
                                             HorizontalAlign="Left" GridLines="None" >
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText=" "
                                                    ItemStyle-Width="2%" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblapplied" Text='<%# Eval("applied") %>' runat="server" HorizontalAlign="Center" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Right"    HeaderText=" "  ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmdate" Text='<%# Eval("mdate") %>' runat="server" HorizontalAlign="Center" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="অনুমোদিত লোন" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="4%">
                                    <ItemTemplate>
                                         <asp:GridView ID="grvAfterLoan" runat="server" Width="100%"   AutoGenerateColumns="False"  CellPadding="3"
                                             HorizontalAlign="Left" GridLines="None" OnRowDataBound="grvAfterLoan_bound">
                                            <Columns>
                            
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText=" "
                                                    ItemStyle-Width="2%" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAfterBags" Text='<%# Eval("Bags") %>' runat="server" HorizontalAlign="Center" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="center"    HeaderText=" "  ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAfterAmount" Text='<%# Eval("Amount") %>' runat="server" HorizontalAlign="Center" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Right"    HeaderText=" "  ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAfterTotAmount"  runat="server" HorizontalAlign="Center" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="তারিখ" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="4%">
                                    <ItemTemplate>
                           
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" Font-Bold="true" Font-Underline="false" BackColor="#e6EEEE" />
                            <EmptyDataRowStyle ForeColor="#CC0000" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="dsParty" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>"
                            SelectCommand="select DISTINCT sr.PartyID as pid, INVParty.PartyCode,INVParty.PartyName
                            from SRVRegistration as sr
                            INNER JOIN INVParty on sr.PartyID=INVParty.PartyID 
                            where ( sr.Requisitioned='Loan Approved' or  sr.Requisitioned='Loan Disbursed');">
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr></tr>
                <tr></tr>

                <tr>
                <td align="center" valign="top" style="font-size:15px;">
                    <asp:Label ID="lblsummary" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
