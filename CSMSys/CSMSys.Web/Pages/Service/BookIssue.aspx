<%@ Page Title="CSMSys :: Book Issue" Language="C#" MasterPageFile="~/Default.Master"
    AutoEventWireup="true" CodeBehind="BookIssue.aspx.cs" Inherits="CSMSys.Web.Pages.Service.BookIssue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>CSMSys :: Book Issue</title>
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="title">
        <table width="100%" border="0" cellpadding="0" cellspacing="4">
            <tbody>
                <tr>
                    <td align="left" valign="top" style="width: 40%;">
                        <h2>
                            Book/Token Issue</h2>
                    </td>
                    <td align="right" valign="bottom" style="width: 60%;">
                        Search by Party Code/Party Name/SR No :
                        <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>
                        &nbsp;
                        <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png"
                            ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                           &nbsp;
                           <asp:ImageButton ID="imgRefresh" runat="server" CommandName="Refresh" ImageUrl="~/App_Themes/Default/Images/gridview/Refresh.png"
                            ToolTip="Refresh" Width="16px" Height="16px" OnClick="imgRefresh_Click" />
                    </td>
                  
                </tr>
            </tbody>
        </table>
    </div>
    <div class="feature-box-full">
        <table width="100%" border="0" cellpadding="0" cellspacing="4">
            <tbody>
                <tr>
                    <td align="left" valign="top" style="width: 40%;">
                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                            <tbody>
                                <tr>
                                    <td align="left" colspan="2">
                                        <strong>General Information </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        Party Code :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCode" runat="server" Enabled="False" EnableTheming="True"></asp:TextBox>
                                        <asp:TextBox ID="txtpartycode" runat="server" Visible="false"></asp:TextBox>
                                        <%--<asp:Button ID="btnCode" runat="server" Text="GetDetails" 
                                            OnClick="btnCode_Click" Visible="False" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        Customer Name :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtName" runat="server" Width="258px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top" style="width: 30%;">
                                        Party Type :
                                    </td>
                                    <td align="left" style="width: 70%;">
                                        <asp:TextBox ID="txtPartyType" runat="server" Width="258px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <strong>Book Issue Details </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 30%;">
                                        Book Number:
                                    </td>
                                    <td align="left" style="width: 70%;">
                                        <asp:TextBox ID="txtBookNo" runat="server" ValidationGroup="^\d*"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="style1" style="width: 30%;">
                                        Page Number:
                                    </td>
                                    <td align="left" class="style1" style="width: 70%;">
                                        <asp:TextBox ID="txtPageNo" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        Date Issued
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtdatedisbursed" runat="server" Text=""></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtdatedisbursed"
                                            CssClass="failureNotification" ErrorMessage="Date is required." ToolTip="Date is required."
                                            ValidationGroup="SerialValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy/MM/dd" TargetControlID="txtdatedisbursed" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top">
                                        Remarks:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox TextMode="MultiLine" ID="txtRemarks" runat="server" Width="258px" Columns="4"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <div class="feature-box-actionBar">
                                            <span class="failureNotification">
                                                <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
                                            </span>
                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="button" />
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td align="left" valign="top" style="width: 60%;">
                        <%-- Grid here  --%>
                        <asp:GridView ID="grvParty" DataKeyNames="PartyID" runat="server" Width="100%" AutoGenerateColumns="False"
                            CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvParty_PageIndexChanging"
                            ShowHeaderWhenEmpty="true" OnRowDataBound="grvParty_RowDataBound" OnRowCommand="grvParty_RowCommand"
                            EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True"
                            PageSize="10" DataSourceID="dsParty">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSl" Text='<%# Eval("SlNo") %>' runat="server" HorizontalAlign="Left" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PartyID" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartyID" Text='<%# Eval("PartyID") %>' runat="server" HorizontalAlign="Left" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartyType" Text='<%# Eval("PartyType") %>' runat="server" HorizontalAlign="Left" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartyCode" Text='<%# Eval("PartyCode") %>' runat="server" HorizontalAlign="Left" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartyName" Text='<%# HighlightText(Eval("PartyName").ToString()) %>'
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactNo" Text='<%# HighlightText(Eval("ContactNo").ToString()) %>'
                                            runat="server" HorizontalAlign="Left" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                                <asp:TemplateField ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEdit" runat="server" CommandName="Select" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png"
                                            ToolTip="Edit" />
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
                        <asp:SqlDataSource ID="dsParty" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>"
                            SelectCommand="SELECT ROW_NUMBER() OVER (ORDER BY ip.PartyID) As SlNo, ip.PartyID, ip.PartyType, ip.PartyCode, ip.PartyName, ip.ContactNo, /*ip.Address,*/ ip.DistrictID, ad.DistrictName
                                                                    FROM INVParty AS ip INNER JOIN ADMDistrict AS ad ON ip.DistrictID = ad.DistrictID"
                            FilterExpression="PartyName LIKE '%{0}%' OR partycode LIKE '{1}%'">
                            <FilterParameters>
                                <asp:ControlParameter Name="PartyName" ControlID="txtSearch" PropertyName="Text" />
                                <asp:ControlParameter Name="partycode" ControlID="txtSearch" PropertyName="Text" />
                            </FilterParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
