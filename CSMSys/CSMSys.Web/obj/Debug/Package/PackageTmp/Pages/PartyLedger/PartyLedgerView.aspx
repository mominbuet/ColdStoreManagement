<%@ Page Language="C#" Title="CSMSys :: Party Ledger" AutoEventWireup="true" MasterPageFile="~/Default.Master"
    CodeBehind="PartyLedgerView.aspx.cs" Inherits="CSMSys.Web.Pages.PartyLedger.PartyLedgerView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="title">
        <table width="100%" border="0" cellpadding="2" cellspacing="4">
            <tbody>
                <tr>
                    <td align="left" style="width: 17%;">
                        <h2>
                            Party Ledger Register</h2>
                    </td>
                    <td align="right" valign="bottom" style="width: 74%;">
                        Search by Party Name/Code :
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        <%--<br />--%>
                        <asp:RadioButton ID="rdbDate" runat="server" Text=" and Date :" GroupName="Search"
                            AutoPostBack="True" OnCheckedChanged="rdbDate_CheckedChanged" />
                        <asp:TextBox ID="txtFromDate" runat="server" Text="From" Width="87px" Enabled="False"></asp:TextBox>
                        <cc1:CalendarExtender ID="ceFromDate" runat="server" Format="yyyy/MM/dd" TargetControlID="txtFromDate" />
                        <asp:TextBox ID="txtToDate" runat="server" Text="To" Width="87px" Enabled="False"></asp:TextBox>
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
                                <asp:GridView ID="grvStockSerial" DataKeyNames="PartyID" runat="server" Width="100%"
                                    AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvStockSerial_PageIndexChanging"
                                    ShowHeaderWhenEmpty="True" OnRowDataBound="grvStockSerial_RowDataBound" OnRowCommand="grvStockSerial_RowCommand"
                                    EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True"
                                    PageSize="14" DataSourceID="dsPartyLedger" 
                                    onselectedindexchanged="grvStockSerial_SelectedIndexChanged">
                                    <Columns>
                                        <asp:BoundField DataField="CreatedDate" HeaderText="নিবন্ধন তারিখ" 
                                            SortExpression="CreatedDate" DataFormatString=" {0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="PartyID" HeaderText="পার্টি আইডি" 
                                            ReadOnly="True" SortExpression="PartyID" />
                                        <asp:BoundField DataField="PartyCode" HeaderText="পার্টি কোড " SortExpression="PartyCode" />
                                        <asp:BoundField DataField="PartyName" HeaderText="পার্টির নাম " SortExpression="PartyName" />
                                        <%--<asp:BoundField DataField="BagLoaded" HeaderText="লোড বস্তা " ReadOnly="True" SortExpression="BagLoaded" />--%>
                                         <asp:TemplateField HeaderText="লোড বস্তা"  HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbagloaded" Text='<%# Eval("BagLoaded") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TotalBagLoan" HeaderText="খালি বস্তার সংখ্যা" 
                                            ReadOnly="True" SortExpression="TotalBagLoan" />
                                        <asp:BoundField DataField="BagLoanAmount" HeaderText="বস্তা লোন" 
                                            SortExpression="BagLoanAmount" />
                                        <asp:BoundField DataField="TotalCarry" HeaderText="ক্যারিং লোন" ReadOnly="True" SortExpression="TotalCarry" />
                                        <asp:BoundField DataField="Loan" HeaderText="লোন " ReadOnly="True" SortExpression="Loan" />
                                        <asp:BoundField DataField="TotalLoan" HeaderText="মোট লোন" ReadOnly="True" SortExpression="TotalLoan" />

                                        <asp:CommandField HeaderText="পূর্ণ রিপোর্ট" ShowSelectButton="True" />

                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" Font-Bold="true" Font-Underline="false" BackColor="#e6EEEE" />
                                    <AlternatingRowStyle BackColor="#E5EAE8" />
                                    <EditRowStyle BackColor="#999999" />
                                    <EmptyDataRowStyle ForeColor="#CC0000" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="dsPartyLedger" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>"
                                    SelectCommand="SP_Party_ledger" SelectCommandType="StoredProcedure"
                                     FilterExpression="PartyName LIKE '%{0}%' or PartyCode Like '%{1}%'">
                                    <FilterParameters>
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
         <asp:Label ID="lblsummary" runat="server" Text=""></asp:Label>
            <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
        </span>
        <asp:Button ID="btnReport" runat="server" Text="Generate Report" CssClass="button"
            OnClick="btnReport_Click" 
            OnClientClick="document.getElementById('form1').target ='_blank';" 
            Enabled="False" Visible="False" />
        <%--<asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" 
                                ValidationGroup="SerialValidationGroup" OnClick="btnSave_Click" />--%>
    </div>
</asp:Content>
