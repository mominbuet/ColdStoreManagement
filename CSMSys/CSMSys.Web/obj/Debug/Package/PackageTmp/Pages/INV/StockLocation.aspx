<%@ Page Language="C#" Title="CSMSys :: Stock Location" AutoEventWireup="true" MasterPageFile="~/Default.Master"
    CodeBehind="StockLocation.aspx.cs" Inherits="CSMSys.Web.Pages.INV.StockLocation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="title">
        <table width="100%" border="0" cellpadding="2" cellspacing="4">
            <tbody>
                <tr>
                    <td align="left" style="width: 14%;">
                        <h2>
                            Stock Location</h2>
                    </td>
                    <td align="right" valign="bottom" style="width: 80%;">
                        Search by Serial No/Party Name/Party Code : 
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        <asp:RadioButton ID="rdbDate" runat="server" Text=" Search by Date :" 
                            GroupName="Search" AutoPostBack="True" 
                            oncheckedchanged="rdbDate_CheckedChanged" />
                        <asp:TextBox ID="txtFromDate" runat="server" Text="From" Width="87px" 
                            Enabled="False"></asp:TextBox>
                        <cc1:CalendarExtender ID="ceFromDate" runat="server" Format="yyyy/MM/dd" TargetControlID="txtFromDate" />
                        <asp:TextBox ID="txtToDate" runat="server" Text="To" Width="87px" 
                            Enabled="False"></asp:TextBox>
                        <cc1:CalendarExtender ID="ceToDate" runat="server" Format="yyyy/MM/dd" TargetControlID="txtToDate" />
                    </td>
                    <td align="center" valign="bottom" style="width: 3%;">
                        <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png"
                            ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                    </td>
                    <td align="center" valign="bottom" style="width: 3%;">
                        <asp:ImageButton ID="imgRefresh" runat="server" CommandName="Refresh" ImageUrl="~/App_Themes/Default/Images/gridview/Refresh.png"
                            ToolTip="Refresh" Width="16px" Height="16px" OnClick="imgRefresh_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="feature-box-full">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tbody>
                        <tr>
                            <td align="left">
                                <asp:GridView ID="grvStockSerial" DataKeyNames="SerialNo" runat="server" Width="100%"
                                    AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvStockSerial_PageIndexChanging"
                                    ShowHeaderWhenEmpty="true" OnRowDataBound="grvStockSerial_RowDataBound" OnRowCommand="grvStockSerial_RowCommand"
                                    EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True"
                                    PageSize="14" DataSourceID="dsStockSerial">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Loading #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblli" Text='<%# Eval("lid") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Serial No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="9%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSerialno" Text='<%# HighlightText(Eval("SerialNo").ToString()) %>'
                                                    runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bags" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="4%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBags" Text='<%# Eval("Bags") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPartyCode" Text='<%# HighlightText(Eval("PartyCode").ToString()) %>'
                                                    runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPartyName" Text='<%# HighlightText(Eval("PartyName").ToString()) %>'
                                                    runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContactNo" Text='<%# Eval("ContactNo") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Chamber No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="6%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblChamberNo" Text='<%# Eval("ChamberNo") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Floor No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="6%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfloorno" Text='<%# Eval("Floor") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pocket No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="6%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpocketno" Text='<%# Eval("Pocket") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Line No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="6%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLineno" Text='<%# Eval("Line") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loading Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldate" Text='<%# Eval("cdates") %>' runat="server" HorizontalAlign="Left"
                                                    ToolTip='<%# Eval("LoadedDate") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblremarks" Text='<%# Eval("Remarks") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" Font-Bold="true" Font-Underline="false" BackColor="#e6EEEE" />
                                    <AlternatingRowStyle BackColor="#E5EAE8" />
                                    <EditRowStyle BackColor="#999999" />
                                    <EmptyDataRowStyle ForeColor="#CC0000" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="dsStockSerial" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>"
                                    SelectCommand="select sl.LoadingID as lid,sl.SerialNo,sl.Bags,sl.SerialNo+'/'+CAST(sl.Bags AS VARCHAR(10)) as slb,sl.LoadedDate,CONVERT(VARCHAR(10), sl.LoadedDate, 103) AS cdates,sl.PartyCode,par.PartyName,par.ContactNo,sl.ChamberNo,sl.Floor,sl.Pocket,sl.Line,sl.Remarks
                                from INVStockLoading sl
                                INNER JOIN INVParty as par on par.PartyCode = sl.PartyCode
                                order by loadingid desc" FilterExpression="slb LIKE '%{0}%' OR PartyName LIKE '%{1}%' or PartyCode LIKE '%{1}%' ">
                                    <FilterParameters>
                                        <asp:ControlParameter Name="slb" ControlID="txtSearch" PropertyName="Text" />
                                        <asp:ControlParameter Name="PartyName" ControlID="txtSearch" PropertyName="Text" />
                                        <asp:ControlParameter Name="PartyCode" ControlID="txtSearch" PropertyName="Text" />
                                    </FilterParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                                                <tr>
                        <td align="right">
                        </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
            <%--<Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Click" />
            </Triggers>--%>
        </asp:UpdatePanel>
    </div>
    <div class="feature-box-actionBar">
        <span class="failureNotification">
            <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
        </span>
        <asp:Button ID="btnReport" runat="server" Text="Generate Report" 
            onclick="btnReport_Click" />
    </div>
</asp:Content>
