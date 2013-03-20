<%@ Page Language="C#" Title="CSMSys :: Loan Collection" AutoEventWireup="true" MasterPageFile="~/Default.Master"
    CodeBehind="LoanCollect.aspx.cs" Inherits="CSMSys.Web.Pages.Service.LoanCollect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function ShowEditModal(LoanID) {
            var frame = $get('IframeEdit');
            frame.src = "../../../Controls/SRV/LCollection.aspx?UIMODE=EDIT&PID=" + LoanID;
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
                    <td align="left" style="width: 47%;">
                        <h2>
                            <asp:Label ID="lblcustsetup" runat="server" Text="Loan Collection"></asp:Label></h2>
                    </td>
                    <td align="right" valign="bottom" style="width: 44%;">
                        Search by Customer Name/Contact No :
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                    </td>
                    <td align="center" valign="bottom" style="width: 3%;">
                        <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png"
                            ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                    </td>
                    <td align="center" valign="bottom" style="width: 3%;">
                        <asp:ImageButton ID="imgRefresh" runat="server" CommandName="Refresh" ImageUrl="~/App_Themes/Default/Images/gridview/Refresh.png"
                            ToolTip="Refresh" Width="16px" Height="16px" OnClick="imgRefresh_Click" />
                    </td>
                    <td align="center" valign="bottom" style="width: 3%;">
                        <asp:ImageButton ID="imgNew" runat="server" CommandName="New" ImageUrl="~/App_Themes/Default/Images/gridview/New.png"
                            ToolTip="New" Width="16px" Height="16px" /> 

                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="ModalPopupBG"
                            runat="server" CancelControlID="ButtonNewCancel" OkControlID="ButtonNewDone"
                            TargetControlID="imgNew" PopupControlID="DivNewWindow" OnOkScript="NewOkayScript();">
                        </cc1:ModalPopupExtender>
                        <div class="popup_Buttons" style="display: none">
                            <input id="ButtonNewDone" value="Done" type="button" />
                            <input id="ButtonNewCancel" value="Cancel" type="button" />
                        </div>
                        <div id="DivNewWindow" style="display: none;" class="popupParty">
                            <iframe id="IframeNew" frameborder="0" width="870px" height="500px" src="../../../Controls/SRV/LCollection.aspx"
                                class="frameborder" scrolling="no"></iframe>
                        </div>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Style="display: none" OnClick="btnRefresh_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="feature-box-full">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <contenttemplate>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
			        <tbody>
			        <tr>
				        <td align="left">
                            <asp:GridView ID="grvloan" DataKeyNames="LCollectionID" runat="server" Width="100%" AutoGenerateColumns="False"
                                                            CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvloan_PageIndexChanging"
                                                            ShowHeaderWhenEmpty="true" OnRowDataBound="grvloan_RowDataBound" OnRowCommand="grvloan_RowCommand"
                                                            EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True"
                                                            PageSize="10" DataSourceID="dsParty">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Collection #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblid" Text='<%# Eval("LCollectionID") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Party Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblpartycode" Text='<%# HighlightText(Eval("PartyCode").ToString()) %>' runat="server" HorizontalAlign="Left" />
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
                                                                        <asp:Label ID="lblPartyName" Text='<%# HighlightText(Eval("PartyName").ToString()) %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField> 
                                                                <asp:TemplateField HeaderText="Interest" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblinterest" Text='<%# Eval("InterestAmount") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltotalamount" Text='<%# Eval("TotalLoan") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Case ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="4%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcaseID" Text='<%# HighlightText(Eval("caseID").ToString()) %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Serials" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblserials"  runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Total Bag Taken" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="6%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltotalbagi"  runat="server" HorizontalAlign="Left" Text="0" />
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
                                                                
                                                                <%--<asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgEdit" runat="server" CommandName="Edit" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png" ToolTip="Edit" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                            </Columns>
                                                            <PagerStyle HorizontalAlign="Right" Font-Bold="true" Font-Underline="false" BackColor="#e6EEEE" />
                                                            <AlternatingRowStyle BackColor="#E5EAE8" />
                                                            <EditRowStyle BackColor="#999999" />
                                                            <EmptyDataRowStyle ForeColor="#CC0000" />
                                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="#333333" />
                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        </asp:GridView>
                                                        <asp:SqlDataSource ID="dsParty" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>"
                                                            SelectCommand="SELECT ld.LCollectionID, ld.LoanID,ld.TotalLoan,ld.serialIDs,ld.InterestAmount,convert(varchar(10),ld.CreatedDate,103) as cdate,sld.caseID,
                                                                INVParty.PartyName,INVParty.PartyCode,ld.Remarks,ld.PartyID
                                                                from SRVLoanCollection as ld,INVParty,SRVLoanDisburse as sld
                                                                where INVParty.PartyID = ld.PartyID and ld.LoanID=sld.LoanID"
                                                            FilterExpression="PartyName LIKE '%{0}%' OR PartyCode = '{1}' OR caseID = '{2}'">
                                                            <FilterParameters>
                                                                <asp:QueryStringParameter Name="PartyName" QueryStringField="PartyName" />
                                                                <asp:QueryStringParameter Name="PartyCode" QueryStringField="PartyCode" /> 
                                                                <asp:QueryStringParameter Name="caseID" QueryStringField="caseID" />
                                                                <asp:ControlParameter Name="txtSearch" ControlID="txtSearch" PropertyName="Text" />
                                                            </FilterParameters>
                                                        </asp:SqlDataSource>
                        </td>
			        </tr>
			        </tbody>
		        </table>
            </contenttemplate>
            <triggers>
                <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Click" />
            </triggers>
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
    <div id="DivEditWindow" style="display: none;" class="popupParty">
        <iframe id="IframeEdit" frameborder="0" width="870px" height="304px" class="frameborder"
            scrolling="no"></iframe>
    </div>
</asp:Content>
