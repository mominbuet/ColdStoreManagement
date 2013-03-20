<%@ Page Language="C#" AutoEventWireup="true" Title="CSMSys :: Bag Issued" MasterPageFile="~/Default.Master" CodeBehind="BookIssueView.aspx.cs" Inherits="CSMSys.Web.Pages.Service.BookIssueView" %>

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
                    <td align="left" style="width: 21%;">
                        <h2>
                            <asp:Label ID="lblcustsetup" runat="server" Text="Book Issued View"></asp:Label></h2>
                    </td>
                    <td align="right" valign="bottom" style="width:44%;">
                    Search by নাম/Contact No/Village/এস আর : <asp:TextBox ID="txtSearch" runat="server" ></asp:TextBox>
                </td>
                <td align="center" valign="bottom" style="width:3%;">
                    <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png" ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                </td>
                <td align="center" valign="bottom" style="width:3%;">
                    <asp:ImageButton ID="imgRefresh" runat="server" CommandName="Refresh" ImageUrl="~/App_Themes/Default/Images/gridview/Refresh.png" ToolTip="Refresh" Width="16px" Height="16px" OnClick="imgRefresh_Click" />
                </td>
                <td align="center" valign="bottom" style="width:3%;">
                 
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" style="display:none" onclick="btnRefresh_Click" />
                </td>
                    
                </tr>
            </tbody>
        </table>
    </div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
			        <tbody>
			        <tr>
				        <td align="left">
     <asp:GridView ID="grvloan" DataKeyNames="BookID" runat="server" Width="100%" AutoGenerateColumns="False"
                                CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvloan_PageIndexChanging"
                                ShowHeaderWhenEmpty="true"
                                EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True"
                                PageSize="14" DataSourceID="dsParty">
                                <Columns>
                                    <asp:TemplateField HeaderText="#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblid" Text='<%# Eval("BookID") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="কোড" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpartycode" Text='<%# Eval("PartyCode") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="নাম" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpname" Text='<%# Eval("partyname") %>' runat="server" HorizontalAlign="Left" Font-Size="13px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcontact" Text='<%# Eval("contactno") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="পিতা" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfather" Text='<%# Eval("fathername") %>' runat="server" HorizontalAlign="Left" Font-Size="13px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="গ্রাম" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblvill" Text='<%# Eval("AreaVillageName") %>' runat="server" HorizontalAlign="Left" Font-Size="13px"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="বই নং" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbookno" Text='<%# Eval("BookNumber") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="পৃষ্ঠা নং" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpageno" Text='<%# Eval("PageNo") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="তারিখ" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpageno" Text='<%# Eval("cdate") %>' runat="server" HorizontalAlign="Left" />
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
                                SelectCommand="SELECT BookID,BookNumber,PageNo,ip.partycode,convert(varchar(10),srvbi.CreatedDate,103) as cdate,ip.contactno,ip.PartyName,ip.AreaVillageName,ip.FatherName,ip.ContactNo,ip.bagcount
                                                from SRVBookIssue as srvbi
                                                INNER JOIN INVParty as ip on srvbi.partyID = ip.partyid"
                                FilterExpression="PartyName LIKE '%{0}%' OR PartyCode like '{1}'">
                                <FilterParameters>
                                    <asp:ControlParameter Name="PartyName" ControlID="txtSearch" PropertyName="Text" />
                                    <asp:ControlParameter Name="PartyCode" ControlID="txtSearch" PropertyName="Text"  />
                                </FilterParameters>
                            </asp:SqlDataSource>
                            </td>
			        
			        <tr>
				        </tbody>
                        </table>
    </asp:Content>
