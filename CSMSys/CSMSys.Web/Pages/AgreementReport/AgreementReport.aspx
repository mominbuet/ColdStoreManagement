<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" Title="CSMSys :: Agreement Report"
    CodeBehind="AgreementReport.aspx.cs" Inherits="CSMSys.Web.Pages.AgreementReport.AgreementReport" %>

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<title>CSMSys :: Agreement</title>
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">

        function checkit(value) {
            alert(value);
        }
        function ShowItemModal(registrationID) {
            var frame = $get('iframeItem');
            frame.src = "../../../Controls/Item/ItemDetails.aspx?UIMODE=EDIT&RID=" + registrationID;
            $find('ItemModalPopup').show();
        }
        function ItemCancelScript() {
            var frame = $get('iframeItem');
            frame.src = "../../../Controls/Loading.aspx";
        }
        function ItemOkayScript() {
            ItemCancelScript();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="title">
        <table width="100%" border="0" cellpadding="2" cellspacing="4">
			<tbody>
			<tr>
                <td align="left" style="width:40%;">
                    <h2>Registration/Agreement</h2>
                </td>
                <td align="right" style="width:60%;">
                    Search by Party Code/Party Name/SR No : 
                    <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>
                    &nbsp;
                    <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png"
                        ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
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
                                <td align="left" style="width: 30%;">
                                    <strong>Registration ID :</strong>
                                </td>
                                <td align="left" style="width: 70%;">
                                    <asp:TextBox ID="txtRegistrationID" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <strong>Agreement No :</strong>
                                </td>
                                <td align="left" style="width: 70%;">
                                    <asp:TextBox ID="txtAgreementNo" runat="server" Enabled="False"></asp:TextBox>
                                    <asp:TextBox ID="txtserid" runat="server" Visible="False" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
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
                                    <asp:TextBox ID="txtCode" runat="server" Enabled="False"></asp:TextBox>
                                    <asp:TextBox ID="txtpartycode" runat="server" Visible="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Party Name :
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
                                    <strong>Loans</strong>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 30%;">
                                    Carrying Loans :
                                </td>
                                <td align="left" valign="top" style="width: 70%;">
                                    <asp:TextBox ID="txtCarryingCost" runat="server" Enabled="False">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 30%;">
                                    Empty Bag Remaining :
                                </td>
                                <td align="left" valign="top" style="width: 70%;">
                                    <asp:TextBox ID="txtEmptyBag" runat="server" Enabled="False">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 30%;">
                                    Weight per Bag:
                                </td>
                                <td align="left" valign="top" style="width: 70%;">
                                    <asp:DropDownList ID="ddlWeight" runat="server" Style="height: 22px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 30%;">
                                    Remarks :
                                </td>
                                <td align="left" valign="top" style="width: 70%;">
                                    <asp:TextBox ID="txtRemarks" runat="server" Width="258px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <div class="feature-box-actionBar">
                                        <span class="failureNotification">
                                            <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
                                        </span>
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClick="btnSave_Click" />
                                        <%--<asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" 
                                                                ValidationGroup="SerialValidationGroup" OnClick="btnSave_Click" />--%>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                        </table>
                    </td>
                    <td align="left" valign="top" style="width: 60%;">
                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                            <tbody>
                                <tr>
                                    <td>
                                        <%-- Grid here  --%>
                                        <asp:GridView ID="grvStockSerial" DataKeyNames="SerialID" runat="server" Width="100%"
                                            AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvStockSerial_PageIndexChanging"
                                            ShowHeaderWhenEmpty="true" OnRowDataBound="grvStockSerial_RowDataBound" OnRowCommand="grvStockSerial_RowCommand"
                                            EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True"
                                            PageSize="13" DataSourceID="dsStockSerial">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgEdit" runat="server" CommandName="Edit" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png"
                                                            ToolTip="Select" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Sl #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSl" Text='<%# Eval("SlNo") %>' runat="server" HorizontalAlign="Left" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="SerialID" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSerialID" Text='<%# Eval("SerialID") %>' runat="server" HorizontalAlign="Left" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SR No" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSerialNo" Text='<%# Eval("SerialNo") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="pid" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpid" Text='<%# Eval("PartyID") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPartyCode" Text='<%# HighlightText(Eval("PartyCode").ToString()) %>'
                                                            runat="server" HorizontalAlign="Left" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPartyName" Text='<%# HighlightText(Eval("PartyName").ToString()) %>'
                                                            runat="server" HorizontalAlign="Left" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinserted" Text='<%# Eval("Inserted") %>' runat="server" HorizontalAlign="Left" />
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
                                            SelectCommand="SELECT ROW_NUMBER() OVER (ORDER BY ss.SerialID) As SlNo,ss.SerialID,ss.PartyID,Convert(varchar(10),ss.SerialDate,103) as Inserted, ss.SerialID, ss.Serial, ss.Bags, ss.SerialNo, ss.PartyID, ss.PartyCode, ip.PartyName, ip.ContactNo, ss.Remarks
                                            FROM INVStockSerial AS ss 
                                            INNER JOIN INVParty AS ip ON ss.PartyID = ip.PartyID 
                                            where not EXISTS(select srv.SerialID from SRVRegistration as srv where ss.SerialID=srv.SerialID)
                                            ORDER BY ss.SerialID DESC"
                                            FilterExpression="SerialNo LIKE '%{0}%' OR PartyCode = '{1}' or PartyName LIKE '%{2}%'">
                                            <FilterParameters>
                                                <asp:ControlParameter Name="SerialNo" ControlID="txtsearch" PropertyName="Text" />
                                                <asp:ControlParameter Name="PartyCode" ControlID="txtsearch" PropertyName="Text" />
                                                <asp:ControlParameter Name="PartyName" ControlID="txtsearch" PropertyName="Text" />
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
    </div>
</asp:Content>
