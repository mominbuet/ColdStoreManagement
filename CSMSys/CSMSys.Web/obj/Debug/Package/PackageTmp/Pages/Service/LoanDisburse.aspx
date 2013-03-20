<%@ Page Language="C#" Title="CSMSys :: Loan Disburse" MasterPageFile="~/Default.Master"
    AutoEventWireup="true" CodeBehind="LoanDisburse.aspx.cs" Inherits="CSMSys.Web.Pages.Service.LoanDisburse1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function ShowEditModal(LoanID) {
            var frame = $get('IframeEdit');
            frame.src = "../../../Controls/SRV/DisburseUser.aspx?UIMODE=EDIT&LID=" + LoanID;
            $find('EditModalPopup').show();
        }
        function EditCancelScript() {
            var frame = $get('IframeEdit');
            frame.src = "../../../Controls/Loading.aspx";
        }
        function EditOkayScript() {
            RefreshDataGrid();
            EditCancelScript();
        }
        function RefreshDataGrid() {
            $get('btnRefresh').click();
        }
        function NewOkayScript() {
            $get('btnRefresh').click();
        }
    </script>
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
                            <asp:Label ID="lblcustsetup" runat="server" Text="Loan Disbursement"></asp:Label></h2>
                    </td>
                    <td align="right" valign="bottom" style="width: 70%;">
                        Search by Customer Name/Contact No :
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        <asp:RadioButton ID="rdbDate" runat="server" Text=" Search by Date :" 
                            AutoPostBack="True" GroupName="Search" oncheckedchanged="rdbDate_CheckedChanged"  />
                         <asp:TextBox ID="txtFromDate" runat="server" Text="From" Width="87px" Enabled="false"></asp:TextBox>
                        <cc1:CalendarExtender ID="ceFromDate" runat="server" Format="yyyy/MM/dd" TargetControlID="txtFromDate" />
                        <asp:TextBox ID="txtToDate" runat="server" Text="To" Width="87px" Enabled="false"></asp:TextBox>
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
            <Contenttemplate>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
			        <tbody>
			        <tr>
				        <td align="left">
                            <asp:GridView ID="grvloan" DataKeyNames="LoanID" runat="server" Width="100%" AutoGenerateColumns="False"
                                CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvloan_PageIndexChanging"
                                ShowHeaderWhenEmpty="true" OnRowDataBound="grvloan_RowDataBound" OnRowCommand="grvloan_RowCommand"
                                EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True"
                                PageSize="10" DataSourceID="dsParty">
                                <Columns>
                                    <asp:TemplateField HeaderText="Loan No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblid" Text='<%# Eval("LoanID") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Party Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpartycode" Text='<%# Eval("PartyCode") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PartyID" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyID" Text='<%# Eval("PartyID") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Party Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyName" Text='<%# Eval("PartyName") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Case No" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcaseID" Text='<%# Eval("caseID") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Serials" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserials"  runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Bag Issued" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotalbagi" Text='<%# Eval("Bags") %>'  runat="server" HorizontalAlign="Left"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Bag on Stock" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotalbag"  runat="server" HorizontalAlign="Left" Text="0" />
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Loan Per Bag" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblperbagloan" Text='<%# Eval("rndloan") %>'  runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Loan" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotalLoan"  runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreatedDate" Text='<%# Eval("cdate") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblremarks" Text='<%# Eval("Remarks") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgselect" runat="server" CommandName="Select" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png"
                                                ToolTip="Select" />
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
                                SelectCommand="SELECT ld.LoanID,ROUND(ld.LoanAmount,2) as rndloan,ld.serialIDs,convert(varchar(10),ld.CreatedDate,103) as cdate,ld.caseID,ld.Bags,
                                    INVParty.PartyName,INVParty.PartyCode,ld.Remarks,ld.PartyID
                                    from SRVLoanDisburse as ld,INVParty
                                    where INVParty.PartyID = ld.PartyID"
                                FilterExpression="PartyName LIKE '%{0}%' OR PartyCode = '{1}'">
                                <FilterParameters>
                                    <asp:QueryStringParameter Name="PartyName" QueryStringField="PartyName" />
                                    <asp:QueryStringParameter Name="PartyCode" QueryStringField="PartyCode" />
                                    <asp:ControlParameter Name="txtSearch" ControlID="txtSearch" PropertyName="Text" />
                                </FilterParameters>
                            </asp:SqlDataSource>
                        </td>
			        </tr>
                    <tr>
                        <td align="right">
                               
                             <div class="feature-box-actionBar">
                                <span class="failureNotification">
                                    <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
                                </span>
                              
                                <asp:Button ID="btnReport" runat="server" Text="Generate Report" CssClass="button" 
                                    onclick="btnReport_Click" />
                            </div>
                        </td>
                    </tr>
			        </tbody>
		        </table>
            </Contenttemplate>
           
        </asp:UpdatePanel>
    </div>
    <asp:Button ID="ButtonEdit" runat="server" Text="Submit" Style="display: none" />
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" BackgroundCssClass="ModalPopupBG"
        runat="server" CancelControlID="ButtonEditCancel" OkControlID="ButtonEditDone"
        TargetControlID="ButtonEdit" PopupControlID="DivEditWindow" OnCancelScript="EditCancelScript();"
        OnOkScript="EditOkayScript();" BehaviorID="EditModalPopup">
    </cc1:ModalPopupExtender>
    <div class="popup_Buttons" style="display: none">
        <input id="ButtonEditDone" value="Done" type="button" />
        <input id="ButtonEditCancel" value="Cancel" type="button" />
    </div>
    <div id="DivEditWindow" style="display: none;" class="popupDisLoan">
        <iframe id="IframeEdit" frameborder="0" width="450px" height="330px" class="frameborder"
            scrolling="no"></iframe>
    </div>
</asp:Content>
