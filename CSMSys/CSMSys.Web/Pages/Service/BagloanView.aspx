<%@ Page Language="C#" Title="CSMSys :: Bag Loans" AutoEventWireup="true" MasterPageFile="~/Default.Master"
    CodeBehind="BagloanView.aspx.cs" Inherits="CSMSys.Web.Pages.Service.BagloanView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function ShowEditModal(LoanID, TMID) {
            var frame = $get('IframeEdit');
            frame.src = "../../../Controls/SRV/BLoan.aspx?UIMODE=EDIT&LID=" + LoanID + "&TMID=" + TMID;
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
                    <td align="left" style="width: 17%;">
                        <h2>Bag Loan</h2>
                    </td>
                    <td align="right" valign="bottom" style="width: 74%;">
                         Search by Serial No/Party Name/Code : 
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        <%--<br />--%>
                        <asp:RadioButton ID="rdbDate" runat="server" Text=" and Date :" GroupName="Search" 
                             AutoPostBack="True" oncheckedchanged="rdbDate_CheckedChanged" />
                        <asp:TextBox ID="txtFromDate" runat="server" Text="From" Width="87px" 
                             Enabled="False"></asp:TextBox>
                        <cc1:CalendarExtender ID="ceFromDate" runat="server" Format="yyyy/MM/dd" TargetControlID="txtFromDate" />
                        <asp:TextBox ID="txtToDate" runat="server" Text="To" Width="87px" 
                             Enabled="False"></asp:TextBox>
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
                        <div id="DivNewWindow" style="display: none;" class="popupBagLoan">
                            <iframe id="IframeNew" frameborder="0" width="950px" height="402px" src="../../../Controls/SRV/BLoan.aspx?"
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
            <ContentTemplate>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tbody>
                        <tr>
                            <td align="left">
                                <asp:GridView ID="grvStockSerial" DataKeyNames="BagLoanID" runat="server" Width="100%"
                                    AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvStockSerial_PageIndexChanging"
                                    ShowHeaderWhenEmpty="true" OnRowDataBound="grvStockSerial_RowDataBound" OnRowCommand="grvStockSerial_RowCommand"
                                    EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True"
                                    PageSize="14" DataSourceID="dsBagloans">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl #" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSl" Text='<%# Eval("BagLoanID") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TMID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTMID" Text='<%# Eval("TMID") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="কোড" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPartyCode" Text='<%# HighlightText(Eval("PartyCode").ToString()) %>'
                                                    runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="নাম" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblPartyName" Text='<%# HighlightText(Eval("PartyName").ToString()) %>'
                                                    runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContactNo" Text='<%# Eval("ContactNo") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="তারিখ" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblinserted" Text='<%# Eval("cdates") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="বস্তা" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSerialNo" Text='<%# HighlightText(Eval("BagNumber").ToString()) %>'
                                                    runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="মোট বস্তা" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsmbags" Text='<%# Eval("smbags") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount/Bag" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmountPerBag" Text='<%# Eval("AmountPerBag") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%"> 
                                            <ItemTemplate>
                                                <asp:Label ID="lblLoanAmount" Text='<%# Eval("LoanAmount") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblremarks" Text='<%# Eval("Remarks") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hplprint" runat="server"  Text="Print" Target="_blank"></asp:HyperLink>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgEdit" runat="server" CommandName="Edit" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png" ToolTip="Edit" />
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
                               <asp:SqlDataSource ID="dsBagloans" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>"
                                    SelectCommand="SELECT   sbl.BagLoanID, sbl.PartyID, sbl.BagNumber, sbl.AmountPerBag, sbl.LoanAmount, sbl.Remarks,  CONVERT(VARCHAR(10),sbl.CreatedDate, 103) AS cdates, 
                                                                ip.PartyType, ip.PartyCode, ip.PartyName, ip.ContactNo, sbl.TMID,
                                                                (select sum(BagNumber) from SRVBagLoan where BagLoanID>=sbl.BagLoanID) as smbags    
                                                                FROM  dbo.SRVBagLoan AS sbl INNER JOIN
                                                                dbo.INVParty AS ip ON ip.PartyID = sbl.PartyID
                                                                ORDER BY sbl.BagLoanID DESC" 
                      FilterExpression="ContactNo LIKE '%{0}%' or PartyName LIKE '%{1}%' or PartyCode Like '%{1}'">
                                    <FilterParameters>
                                        <asp:ControlParameter Name="ContactNo" ControlID="txtSearch" PropertyName="Text" />
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
    <div id="DivEditWindow" style="display: none;" class="popupBagLoan">
        <iframe id="IframeEdit" frameborder="0" width="950px" height="402px" class="frameborder"
            scrolling="no"></iframe>
    </div>
         <div class="feature-box-actionBar">
                            <span class="failureNotification">
                                <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
                            </span>
                            <asp:Button ID="Button1" runat="server" Text="Total Report" CssClass="button" Width="120px"
                                onclick="btntotal_Click" />
                            <asp:Button ID="btnReport" runat="server" Text="Generate Report" CssClass="button" Width="120px"
                                onclick="btnReport_Click" />
                            <%--<asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" 
                                ValidationGroup="SerialValidationGroup" OnClick="btnSave_Click" />--%>
                        </div>
</asp:Content>

