<%@ Page Language="C#" AutoEventWireup="true" Title="CSMSys :: Serial Register" CodeBehind="SerialReportViewer.aspx.cs" Inherits="CSMSys.Web.Pages.StockSerialReport.SerialReportViewer" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

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
                        তারিখ: <asp:Label ID="lblfrom" runat="server" Text=""></asp:Label>
                        হতেঃ<asp:Label ID="lblto" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="font-size:10px;">
                        <asp:GridView ID="grvPreRequisition"  runat="server" Width="100%"
                            AutoGenerateColumns="False" CellPadding="3" HorizontalAlign="Left" ShowHeaderWhenEmpty="true"
                            EmptyDataText="No Records Found" DataSourceID="dsBagloan"  AllowPaging="true" PageSize="28" AllowSorting="true"
                            OnPageIndexChanging="grvPreRequisition_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsl" Text='<%# Eval("slno") %>' runat="server" HorizontalAlign="Left" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="এস আর" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="4%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsrno" Text='<%# Eval("SerialNo") %>' runat="server" HorizontalAlign="Left" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="বস্তা" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblbagloaned" Text='<%# Eval("bags") %>' runat="server" HorizontalAlign="Left" />                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="মোট বস্তা" HeaderStyle-HorizontalAlign="Center" 
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblsumbags" Text='<%# Eval("smbags") %>' runat="server" HorizontalAlign="Center" />                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="কোড"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="4%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpartycode" Text='<%# Eval("PartyCode") %>' runat="server" HorizontalAlign="Left" Font-Size="12px" />
                                      </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="নাম"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpartyname" Text='<%# Eval("PartyName") %>' runat="server" HorizontalAlign="Left" Font-Size="15px" />
                                      </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="পিতার নাম" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblfather" Text='<%# Eval("fathername") %>' runat="server" HorizontalAlign="Left" Font-Size="15px" />                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="গ্রাম" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblvill" Text='<%# Eval("areavillagename") %>' runat="server" HorizontalAlign="Left" Font-Size="15px"/>                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblcontact" Text='<%# Eval("contactno") %>' runat="server" HorizontalAlign="Left" Font-Size="15px" />                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                               
                                <asp:TemplateField HeaderText="তারিখ" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%" >
                                    <ItemTemplate>
                                        <asp:Label ID="lvlcdate" Text='<%# Eval("cdate") %>' runat="server" HorizontalAlign="Left" Font-Size="12px" />                                       
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
