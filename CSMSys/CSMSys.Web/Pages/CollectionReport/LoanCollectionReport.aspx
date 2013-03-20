<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="LoanCollectionReport.aspx.cs" Inherits="CSMSys.Web.Pages.CollectionReport.LoanCollectionReport" %>

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
                                                <strong>Agreement No :</strong>
                                            </td>
                                            <td align="left" style="width: 70%;">
                                                <asp:TextBox ID="txtAgreementNo" runat="server"></asp:TextBox>
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
                                                &nbsp;     &nbsp;

                                            </td>
                                            <td align="center" style="width:3%;">
                    <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png" ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                </td>
                <td align="center" style="width:3%;">
                    <asp:ImageButton ID="imgRefresh" runat="server" CommandName="Refresh" ImageUrl="~/App_Themes/Default/Images/gridview/Refresh.png" ToolTip="Refresh" Width="16px" Height="16px" OnClick="imgRefresh_Click" />
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
                                            <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtpartycode" runat="server" Visible="false"></asp:TextBox>
                                            <asp:Button ID="btnCode" runat="server" Text="GetDetails" OnClick="btnCode_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            Customer Name :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtName" runat="server" Width="258px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            Father's Name :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtFather" runat="server" Width="258px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" style="width: 30%;">
                                            Party Type :
                                        </td>
                                        <td align="left" style="width: 70%;">
                                            <asp:TextBox ID="txtPartyType" runat="server" Width="258px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 30%;">
                                            Cell :
                                        </td>
                                        <td align="left" style="width: 70%;">
                                            <asp:TextBox ID="txtCellNo" runat="server" Width="258px"></asp:TextBox>
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
                                            <asp:TextBox ID="txtVillage" runat="server" Width="258px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" style="width: 30%;">
                                            PO :
                                        </td>
                                        <td align="left" style="width: 70%;">
                                            <asp:TextBox ID="txtPO" runat="server" Width="258px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 30%;">
                                            District :
                                        </td>
                                        <td align="left" style="width: 70%;">
                                            <asp:TextBox ID="txtDistrict" runat="server" Width="258px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 30%;">
                                            Upazila/PS :
                                        </td>
                                        <td align="left" style="width: 70%;">
                                            <asp:TextBox ID="txtUpazilla" runat="server" Width="258px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        <td align="left" valign="top" style="width: 50%;">

                       <%-- Grid here  --%>
                       <asp:GridView ID="grvStockSerial" DataKeyNames="SerialID" runat="server" Width="100%" AutoGenerateColumns="False"
                                CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvStockSerial_PageIndexChanging" ShowHeaderWhenEmpty="true" 
                                OnRowDataBound="grvStockSerial_RowDataBound" OnRowCommand="grvStockSerial_RowCommand"
                                EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True" PageSize="10" DataSourceID="dsStockSerial">
                               
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSl" Text='<%# Eval("SlNo") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SerialID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSerialID" Text='<%# Eval("SerialID") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Serial No/Bags" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSerialNo" Text='<%# Eval("SerialNo") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="pid" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpid" Text='<%# Eval("PartyID") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyCode" Text='<%# HighlightText(Eval("PartyCode").ToString()) %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyName" Text='<%# HighlightText(Eval("PartyName").ToString()) %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Inserted On" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinserted" Text='<%# Eval("Inserted") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgEdit" runat="server" CommandName="Edit" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png" ToolTip="Select" />
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
                                SelectCommand="SELECT ROW_NUMBER() OVER (ORDER BY ss.SerialID) As SlNo,ss.PartyID,Convert(varchar(10),ss.SerialDate,103) as Inserted, ss.SerialID, ss.Serial, ss.Bags, ss.SerialNo, ss.PartyID, ss.PartyCode, ip.PartyName, ip.ContactNo, ss.Remarks
                                    FROM INVStockSerial AS ss INNER JOIN INVParty AS ip ON ss.PartyID = ip.PartyID ORDER BY ss.SerialID DESC" 
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
            <div>
                <table width="100%" border="0" cellpadding="0" cellspacing="4">
                    <tbody>
                        <tr>
                            <td align="left" valign="top" style="width: 50%;">
                                <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <strong>Cost Description</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2">
                                                Bag Loans :
                                            </td>
                                            <td align="left" style="width: 70%;">
                                                <asp:TextBox ID="txtBagloans" runat="server"></asp:TextBox>
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
                                            <td align="left" colspan="2">
                                                Carrying Cost :
                                            </td>
                                            <td align="left" style="width: 70%;">
                                                <asp:TextBox ID="txtCarryingCost" runat="server" Width="258px"></asp:TextBox>
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
                <td>
                    <a href="TestReport.aspx">Get Agreement Report</a>
                </td>
                <td align="right">
                    <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
