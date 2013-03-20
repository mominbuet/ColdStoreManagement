<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="PartyReport.aspx.cs" Inherits="CSMSys.Web.Pages.PartyReports.PartyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function doClick(buttonName, e) {//the purpose of this function is to allow the enter key to point to the correct button to click.
            var key;

            if (window.event)
                key = window.event.keyCode;     //IE
            else
                key = e.which;     //firefox

            if (key == 13) {
                //Get the button the user wants to have clicked
                var btn = document.getElementById(buttonName);
                if (btn != null) { //If we find the button click it
                    btn.click();
                    event.keyCode = 0
                }
            }

        }
    </script>
    <div>
        <asp:Panel ID="pnlNew" runat="server" Width="100%">
            <div>
                <table width="100%" border="0" cellpadding="0" cellspacing="4">
                    <tbody>
                        <tr>
                            <td align="left" valign="top" style="width: 50%;">
                                <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="2">
                                                &nbsp;
                                            </td>
                                            <td align="left" style="width: 70%;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td align="left" valign="top" style="width: 50%;">
                                <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <strong>Search :</strong>
                                            </td>
                                            <td align="left" style="width: 70%;">
                                                <asp:TextBox ID="txtsearch" runat="server" Width="258px"></asp:TextBox>
                                                &nbsp; &nbsp;
                                            </td>
                                            <td align="center" style="width: 3%;">
                                                <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png"
                                                    ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                                            </td>
                                            <td align="center" style="width: 3%;">
                                                <asp:ImageButton ID="imgRefresh" runat="server" CommandName="Refresh" ImageUrl="~/App_Themes/Default/Images/gridview/Refresh.png"
                                                    ToolTip="Refresh" Width="16px" Height="16px" OnClick="imgRefresh_Click" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <table width="100%" border="0" cellpadding="0" cellspacing="4">
                <tbody>
                    <tr>
                        <td align="left" valign="top" style="width: 50%;">
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
                                        <td align="left">
                                            Father's Name :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtFather" runat="server" Width="258px" Enabled="False"></asp:TextBox>
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
                                        <td align="left" style="width: 30%;">
                                            Cell :
                                        </td>
                                        <td align="left" style="width: 70%;">
                                            <asp:TextBox ID="txtCellNo" runat="server" Width="258px" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                <tbody>
                                    <tr>
                                        <td align="left" colspan="2">
                                            <strong>Address </strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" style="width: 30%;">
                                            Area/Village :
                                        </td>
                                        <td align="left" style="width: 70%;">
                                            <asp:TextBox ID="txtVillage" runat="server" Width="258px" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" style="width: 30%;">
                                            PO :
                                        </td>
                                        <td align="left" style="width: 70%;">
                                            <asp:TextBox ID="txtPO" runat="server" Width="258px" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 30%;">
                                            District :
                                        </td>
                                        <td align="left" style="width: 70%;">
                                            <asp:TextBox ID="txtDistrict" runat="server" Width="258px" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 30%;">
                                            Upazila/PS :
                                        </td>
                                        <td align="left" style="width: 70%;">
                                            <asp:TextBox ID="txtUpazilla" runat="server" Width="258px" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        <td align="left" valign="top" style="width: 50%;">
                            <%-- Grid here  --%>
                            <asp:GridView ID="grvParty" DataKeyNames="PartyID" runat="server" Width="100%" AutoGenerateColumns="False"
                                CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvParty_PageIndexChanging"
                                ShowHeaderWhenEmpty="true" OnRowDataBound="grvParty_RowDataBound" OnRowCommand="grvParty_RowCommand"
                                EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True"
                                PageSize="10" DataSourceID="dsParty">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
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
                                    <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyType" Text='<%# Eval("PartyType") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyCode" Text='<%# Eval("PartyCode") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="15%">
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
                                    <%--<asp:TemplateField HeaderText="Address" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAddress" Text='<%# Eval("Address") %>' runat="server" HorizontalAlign="Left" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="District" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDistrictName" Text='<%# Eval("DistrictName") %>' runat="server" HorizontalAlign="Left" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
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
                                FilterExpression="PartyName LIKE '%{0}%' OR ContactNo LIKE '%{1}%'">
                                <FilterParameters>
                                    <asp:QueryStringParameter Name="PartyName" QueryStringField="PartyName" />
                                    <asp:QueryStringParameter Name="ContactNo" QueryStringField="ContactNo" />
                                    <asp:ControlParameter Name="txtSearch" ControlID="txtSearch" PropertyName="Text" />
                                </FilterParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div>
                <table width="100%" border="0" cellpadding="0" cellspacing="4">
                    <tbody>
                        <tr>
                            <td align="left" valign="top" style="width: 50%;">
                                <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <strong>Select Report For </strong>
                                                <br/>
                                                <asp:DropDownList ID="ddlReport" runat="server">
                                                </asp:DropDownList>
                                                </td>
                                            <td>
                                              <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td align="left" valign="top" style="width: 50%;">
                                <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 70%;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </asp:Panel>
        <table style="text-align: center; padding-top: 50px; font-family: Verdana,tahoma,calibri;
            font-size: 12px; width: 1026px;">
            <tr style="height: 35px;">
                <td align="left">
                    <%--    <a href="TestReport.aspx">Get Agreement Report</a>--%>
                </td>
            </tr>
        </table>
    </div>
     <div class="feature-box-actionBar">
                            <span class="failureNotification">
                                <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
                            </span>
                            <%--<asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" 
                                ValidationGroup="SerialValidationGroup" OnClick="btnSave_Click" />--%>
                        </div>
</asp:Content>

