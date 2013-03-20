<%@ Page Language="C#" Title="CSMSys :: Loan Pre Requisition" AutoEventWireup="true" CodeBehind="LoanPreRequisition.aspx.cs" Inherits="CSMSys.Web.HtmlReports.LoanPreRequisition" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                    <td align="center" valign="top" style="font-size:14px;">
                        লোন রিকুইজিশন
                    </td>
                </tr>
                  <tr>
                    <td align="center" valign="top" style="font-size:12px;">
                        তারিখ <asp:Label ID="lblfrom" runat="server" Text=""></asp:Label>
                        হতে <asp:Label ID="lblto" runat="server" Text=""></asp:Label>
                        &nbsp; 
                        <asp:Label ID="lblamount" runat="server" Text=""> </asp:Label> টাকা দরে
                    </td>
                </tr>
                <tr>
                    <td align="center" style="font-size:10px;">
                        <asp:GridView ID="grvPreRequisition"  runat="server" Width="100%"
                            AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Left" ShowHeaderWhenEmpty="true" AllowPaging="true"
                            PageSize="25" OnPageIndexChanging="grvPreRequisition_changing"
                            EmptyDataText="No Records Found" DataSourceID="dsParty" OnRowDataBound="grvPreRequisition_bound">
                            <Columns>
               
                                <asp:TemplateField HeaderText="পার্টি কোড" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcode" Text='<%# Eval("PartyCode") %>' runat="server" HorizontalAlign="Left"  Font-Size="12px"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="পার্টি নাম" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpartyname" Text='<%# Eval("PartyName") %>' runat="server" HorizontalAlign="Left" Font-Size="15px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="পূবে্র লোন"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                    
                                    <ItemTemplate>
                                        <asp:GridView ID="grvPrevLoan" runat="server" Width="100%"   AutoGenerateColumns="False"
                                             HorizontalAlign="Left" GridLines="None">
                                            <Columns>
                            
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText=" "
                                                    ItemStyle-Width="2%" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrevBags" Text='<%# Eval("Bags") %>' runat="server" HorizontalAlign="Center"  Font-Size="12px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center"    HeaderText=" "  ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrevAmount" Text='<%# Eval("Amount") %>' runat="server" HorizontalAlign="Center"  Font-Size="12px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>     
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Right"    HeaderText=" "  ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrevAmount" Text='<%# Eval("mult") %>' runat="server" HorizontalAlign="Center"  Font-Size="12px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ব্যালান্স বস্তা" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" >
                                    <ItemTemplate>
                                        <asp:GridView id="grdBalance"  runat="server" GridLines="None" Width="30%">
                       
                                        </asp:GridView>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="আবেদনকৃত বস্তা" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%" >
                                    <ItemTemplate>
                                    <asp:GridView ID="grvAppliedBags" runat="server" Width="100%"   AutoGenerateColumns="False"
                                             HorizontalAlign="Left" GridLines="None" OnRowDataBound="grvAppliedBags_bound">
                                            <Columns>
                            
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText=" "
                                                    ItemStyle-Width="2%" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblapplied" Text='<%# Eval("applied") %>' runat="server" HorizontalAlign="Center"  Font-Size="12px"/>
                                                    </ItemTemplate>
                                                  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText=" "
                                                    ItemStyle-Width="2%" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltotapplied"  runat="server" HorizontalAlign="Center"  Font-Size="12px" />
                                                    </ItemTemplate>
                                                  
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Right"    HeaderText=" "  ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmdate" Text='<%# Eval("mdate") %>' runat="server" HorizontalAlign="right"  Font-Size="12px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                         
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="অনুমোদিত লোন" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="10%">
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
                            where sr.Requisitioned='Applied For Loan' order by PartyCode asc;">
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                 <td align="center" style="font-size:10px;">
                     <asp:Label ID="lblsummary" runat="server" Text=""  Font-Size="14px"></asp:Label>
                 </td>
                </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
