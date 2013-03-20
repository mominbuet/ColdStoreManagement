<%@ Page Language="C#" Title="CSMSys :: Stock Status" AutoEventWireup="true" MasterPageFile="~/Default.Master"
    CodeBehind="StockStatus.aspx.cs" Inherits="CSMSys.Web.Pages.INV.StockStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%" border="0" cellpadding="2" cellspacing="4">
        <tbody>
            <tr>
                <td align="left" style="width: 40%;">
                    <h2>
                        Stock Status</h2>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 40%;">
                    Enter Party Code
                </td>
                <td align="left" style="width: 60%;">
                    <asp:TextBox ID="txtpartyCode" runat="server"></asp:TextBox>
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <div class="feature-box-actionBar">
        <span class="failureNotification">
            <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
        </span>
        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Check" OnClick="checkSum" />
    </div>
    <table>
        <tbody>
            <tr>
                <td align="left" style="width: 40%;">
                    Top:
                </td>
                <td align="left" style="width: 60%;">
                    <asp:DropDownList ID="ddltop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddltop_changed">
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    <asp:ListItem>500</asp:ListItem>
                    <asp:ListItem>1000</asp:ListItem>
                    <asp:ListItem>All</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <tr>
                <asp:GridView ID="grvStockSerial" runat="server" Width="100%"
                                    AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvStockSerial_PageIndexChanging"
                                    ShowHeaderWhenEmpty="true" 
                                    EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True"
                                    PageSize="15" DataSourceID="dsStockSerial">
                                    <Columns>
                                    <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPartyName" Text='<%# Eval("PartyName") %>'
                                                    runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Father" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfather" Text='<%# Eval("fathername") %>'
                                                    runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Village" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblvillage" Text='<%# Eval("areavillagename") %>'
                                                    runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcode" Text='<%# Eval("partycode") %>'
                                                    runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Bags" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPartyName" Text='<%# Eval("smbags") %>'
                                                    runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    
                                    </asp:GridView>
                                     <asp:SqlDataSource ID="dsStockSerial" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>"
                                    SelectCommand="select top 5 partycode,PartyName,sum(bagcount) as smbags,Areavillagename,fathername
                                                    from INVParty
                                                    GROUP BY PartyCode,PartyName,Areavillagename,fathername
                                                    ORDER BY sum(bagcount) desc">
                                  
                                </asp:SqlDataSource>
                                    </tr>
            </tr>
        </tbody>
    </table>
</asp:Content>
