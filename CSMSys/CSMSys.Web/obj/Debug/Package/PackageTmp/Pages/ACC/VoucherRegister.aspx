<%@ Page Title="CSMSys :: Voucher Register" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" 
CodeBehind="VoucherRegister.aspx.cs" Inherits="CSMSys.Web.Pages.ACC.VoucherRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
    <script src="../../App_Themes/GridView/HierarchicalGrid.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
//        function ShowEditModal(AccountID) {
//            var frame = $get('IframeEdit');
//            frame.src = "../../../Controls/INV/Account.aspx?UIMODE=EDIT&PID=" + AccountID;
//            $find('EditModalPopup').show();
//        }
//        function EditCancelScript() {
//            var frame = $get('IframeEdit');
//            frame.src = "../../../Controls/Loading.aspx";
//        }
//        function EditOkayScript() {
//            RefreshDataGrid();
//            EditCancelScript();
//        }
//        function RefreshDataGrid() {
//            $get('btnRefresh').click();
//        }
//        function NewOkayScript() {
//            $get('btnRefresh').click();
//        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="title">
  
        <table width="100%" border="0" cellpadding="2" cellspacing="4">
			<tbody>
			<tr>
                <td align="left" style="width:37%;">
                    <h2>
                        Voucher Register
                    </h2>
                </td>
                <td align="right" valign="bottom" style="width:40%;">
                    Search by Transaction Date : <asp:TextBox ID="txtDateFrom" runat="server" ></asp:TextBox>
                    &nbsp;&nbsp;
                    <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" 
                        Enabled="True" TargetControlID="txtDateFrom" PopupPosition="BottomLeft">
                    </cc1:CalendarExtender>
                    <asp:RegularExpressionValidator ID="reValidator11" runat="server" 
                        ControlToValidate="txtDateFrom" ErrorMessage="*" 
                        ValidationExpression="^[0-9d]{1,2}/[0-9m]{1,2}/[0-9y]{4}$">
                    </asp:RegularExpressionValidator>
                </td>
                <td align="right" valign="bottom" class="width: 18%;">
                    to : <asp:TextBox ID="txtDateTo" runat="server" ></asp:TextBox>
                    &nbsp;&nbsp;
                    <cc1:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" 
                        Enabled="True" TargetControlID="txtDateTo" PopupPosition="BottomLeft">
                    </cc1:CalendarExtender>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtDateTo" ErrorMessage="*" 
                        ValidationExpression="^[0-9d]{1,2}/[0-9m]{1,2}/[0-9y]{4}$">
                    </asp:RegularExpressionValidator>
                </td>
                <td align="center" valign="bottom" style="width:3%;">
                    <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png" ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
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
                            <asp:GridView ID="grvRegister" DataKeyNames="TransMID" runat="server" Width="100%" AutoGenerateColumns="False"
                                CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvRegister_PageIndexChanging" ShowHeaderWhenEmpty="true" 
                                OnRowDataBound="grvRegister_RowDataBound" OnRowCommand="grvRegister_RowCommand"
                                EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True" PageSize="14" DataSourceID="dsRegister">
                                <Columns>
                                    <%--<asp:HyperLinkField Text="[+]" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%" />--%>
                                    <asp:TemplateField HeaderText="Sl #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSl" Text='<%# Eval("SlNo") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TransMID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransMID" Text='<%# Eval("TransMID") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransDate" Text='<%# Eval("TransDate","{0:MMM dd, yyyy}") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Voucher No" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVoucherNo" Text='<%# Eval("VoucherNo") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VoucherType" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVoucherType" Text='<%# Eval("VoucherType") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransDescription" Text='<%# Eval("TransDescription") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cr Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalCredit" Text='<%# Eval("TotalCredit","{0:#,##0.00}") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dr Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalDebit" Text='<%# Eval("TotalDebit","{0:#,##0.00}") %>' DataFormatString="{0:#,##0.00;(£#,##0.00);''}" runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgEdit" runat="server" CommandName="Edit" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png" ToolTip="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgDelete" runat="server" CommandName="Delete" OnClientClick='return confirm("Are you sure you want to Delete?");' ImageUrl="~/App_Themes/Default/Images/gridview/Delete.png" ToolTip="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="Amount" DataField="TotalDebit"></asp:BoundField>--%>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" Font-Bold="true" Font-Underline="false" BackColor="#e6EEEE" />
                                <AlternatingRowStyle BackColor="#E5EAE8" />
                                <EditRowStyle BackColor="#999999" />
                                <EmptyDataRowStyle ForeColor="#CC0000" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="dsRegister" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>" OnSelecting="dsRegister_Selecting"  
                                SelectCommand="SELECT ROW_NUMBER() OVER (ORDER BY tm.TransMID) As SlNo, tm.TransMID, tm.TransDate, tm.VoucherNo, 
                                    tm.VoucherPayee, tm.VoucherType, tm.TransMethodID, tm.TransDescription, vd.TotalCredit, vd.TotalDebit 
                                    FROM T_Transaction_Master AS tm INNER JOIN 
                                    vw_Transaction_Detail_Total AS vd ON tm.TransMID = vd.TransMID 
                                    WHERE ((tm.TransDate >= @TransDateFrom) AND (tm.TransDate <= @TransDateTo))">
                                    <SelectParameters>
                                        <asp:Parameter Name="TransDateFrom" Type="DateTime" />
                                        <asp:Parameter Name="TransDateTo" Type="DateTime" />
                                    </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
			        </tr>
				    <tr>
					    <td align="left">
						    <p><asp:textbox id="txtExpandedDivs" runat="server" Font-Size="8pt" Font-Names="Tahoma" Width="0px" Visible="false"></asp:textbox></p>
					    </td>
				    </tr>
			        </tbody>
		        </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
