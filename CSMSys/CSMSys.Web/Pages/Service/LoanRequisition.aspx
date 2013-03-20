<%@ Page Language="C#" AutoEventWireup="true" Title="CSMSys :: Loan Requisitioned"
    MasterPageFile="~/Default.Master" CodeBehind="LoanRequisition.aspx.cs" Inherits="CSMSys.Web.Pages.Service.LoanRequisition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="title">
        <table width="100%" border="0" cellpadding="2" cellspacing="4">
            <tbody>
                <tr>
                    <td align="left" style="width: 40%;">
                        <h2>Loan Requisition</h2>
                    </td>
                    <td align="left" valign="bottom" style="width: 60%;">
                        Search party by Name/Code/SR No &nbsp;
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png"
                            ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                        <asp:ImageButton ID="imgRefresh" runat="server" CommandName="Refresh" ImageUrl="~/App_Themes/Default/Images/gridview/Refresh.png"
                            ToolTip="Refresh" Width="16px" Height="16px" OnClick="imgRefresh_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" style="width: 40%;">
                        <asp:MultiView ID="MultiViewSL" runat="server">
                            <asp:View ID="ViewInput" runat="server">
                                <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                    <tbody>
                                        <tr>
                                            <td align="left" style="width: 30%;">
                                                Party Name:
                                            </td>
                                            <td align="left" style="width: 70%;">
                                                <asp:Label ID="lblpartyid" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="lblpartyname" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 30%;">
                                                Party Code:
                                            </td>
                                            <td align="left" style="width: 70%;">
                                                <asp:Label ID="lblpartycode" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top" style="width: 30%;">
                                                Serial No:
                                            </td>
                                            <td align="left" style="width: 70%;">
                                                <asp:ListBox ID="lstserials" runat="server" Width="60%" Height="232px" Enabled="False"></asp:ListBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 30%;">
                                                Bags :
                                            </td>
                                            <td align="left" style="width: 70%;">
                                                <asp:Label ID="lbltotalbags" runat="server" Text="0"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <div class="feature-box-actionBar">
                                                    <span class="failureNotification">
                                                        <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
                                                    </span>
                                                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClick="save_requisition" ValidationGroup="SerialValidationGroup" />
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:View>
                            <asp:View ID="ViewSuccess" runat="server">
                                <asp:Panel ID="pnlSuccess" runat="server" Width="100%">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tbody>
                                            <tr>
                                                <td align="center">
                                                    <span class="succesNotification">Loan Requisition Saved Successfully. 
                                                        <br />
                                                        Please press F5 to refresh the page.</span>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </asp:View>
                            <asp:View ID="ViewError" runat="server">
                                <asp:Panel ID="pnlError" runat="server" Width="100%">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tbody>
                                            <tr>
                                                <td align="center" valign="middle">
                                                    <span class="failureNotification">Error Occured Saving Stock Loading<br />
                                                        Dialog will Close automatically within 2 Seconds </span>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </asp:View>
                        </asp:MultiView>
                    </td>
                    <td align="left" valign="top" style="width: 60%;">
                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                            <tbody>
                                <tr>
                                    <td align="left">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tbody>
                                                <tr><td align="left">
                                                        <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server" OnSelectedIndexChanged="change_query"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Selected="true">Not Applied</asp:ListItem>
                                                    <asp:ListItem>Applied For Loan</asp:ListItem>
                                                    <asp:ListItem>Loan Approved</asp:ListItem>
                                                    <asp:ListItem>Loan Disbursed</asp:ListItem>
                                                </asp:RadioButtonList>
                                                    </td></tr> 
                                                <tr>
                                                    <td align="left">
                                                        <asp:GridView ID="grvParty" DataKeyNames="RegistrationID" runat="server" Width="100%"
                                                            AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvParty_PageIndexChanging"
                                                            ShowHeaderWhenEmpty="true" OnRowDataBound="grvParty_RowDataBound"
                                                            OnRowCommand="grvParty_RowCommand" EmptyDataText="No Records Found" CssClass="tablesorterBlue"
                                                            AllowPaging="True" PageSize="11" DataSourceID="dsParty">
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgselect" runat="server" CommandName="Select" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png"
                                                                            ToolTip="Select" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sl #" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="2%" SortExpression="slno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSl" Text='<%# Eval("RegistrationID") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SR No" SortExpression="SerialNo" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSerialNo" Text='<%# Eval("SerialNo") %>'
                                                                            runat="server" HorizontalAlign="Left" /> 
                                                                           
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Code" SortExpression="PartyCode" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPartyCode" Text='<%# HighlightText(Eval("PartyCode").ToString()) %>'
                                                                            runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name" SortExpression="PartyName" HeaderStyle-HorizontalAlign="Left"  >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPartyName" Text='<%# HighlightText(Eval("PartyName").ToString()) %>'
                                                                            runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField> 
                                                                <asp:TemplateField HeaderText="Father" SortExpression="FatherNmae" HeaderStyle-HorizontalAlign="Left" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfathername" Text='<%# HighlightText(Eval("fathername").ToString()) %>'
                                                                            runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField> 
                                                                <asp:TemplateField HeaderText="Village" HeaderStyle-HorizontalAlign="Left" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblvillage" Text='<%# HighlightText(Eval("AreaVillageName").ToString()) %>'
                                                                            runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Carrying" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCarryingLoan" Text='<%# Eval("CarryingLoan") %>' runat="server"
                                                                            HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bags Loan" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="3%"
                                                                    ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBagLoan" Text='<%# Eval("BagLoan") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Created" SortExpression="cdates" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCreatedDate" Text='<%# Eval("cdate") %>' runat="server" HorizontalAlign="Left" />
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
                                                            SelectCommand="select sr.RegistrationID,sr.serialID,sr.BagLoan,sr.CarryingLoan,sr.SerialNo,ip.PartyName,ip.fathername,ip.PartyCode,ip.AreaVillageName, ip.partyid,ip.ContactNo,convert(varchar(10),sr.createddate,10) as cdate
                                                            from SRVRegistration as sr
                                                            inner JOIN INVParty as ip on sr.PartyID=ip.PartyID
                                                            where sr.Requisitioned='Not Applied'
                                                            ORDER BY sr.SerialNo asc;" 
                                                            FilterExpression="PartyName LIKE '%{0}%' OR PartyCode LIKE '{1}%' OR fathername LIKE '%{2}%' OR AreaVillageName LIKE '%{3}%' OR SerialNo LIKE '%{4}%'">
                                                            <FilterParameters>
                                                                <asp:ControlParameter Name="PartyName" ControlID="txtsearch" PropertyName="Text" />
                                                                <asp:ControlParameter Name="PartyCode" ControlID="txtsearch" PropertyName="Text" />
                                                                <asp:ControlParameter Name="fathername" ControlID="txtsearch" PropertyName="Text" />
                                                                <asp:ControlParameter Name="AreaVillageName" ControlID="txtsearch" PropertyName="Text" />
                                                                <asp:ControlParameter Name="SerialNo" ControlID="txtsearch" PropertyName="Text" />

                                                                <%--<asp:ControlParameter Name="SerialNo" ControlID="txtsearch" PropertyName="Text" />--%>
                                                                <%--<asp:QueryStringParameter Name="PartyName" QueryStringField="PartyName" />
                                                                <asp:QueryStringParameter Name="PartyCode" QueryStringField="PartyCode" />
                                                                <asp:QueryStringParameter Name="SerialNo" QueryStringField="SerialNo" />
                                                                <asp:ControlParameter Name="txtSearch" ControlID="txtSearch" PropertyName="Text" />--%>
                                                            </FilterParameters>
                                                        </asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
