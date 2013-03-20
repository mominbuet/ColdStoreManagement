<%@ Page Language="C#" AutoEventWireup="true" Title="CSMSys :: Loan Requisitioned"
    MasterPageFile="~/Default.Master" CodeBehind="LoanRequisitionView.aspx.cs" Inherits="CSMSys.Web.Pages.LoanRequisitionReport.LoanRequisitionView" %>

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
                    <td align="left" style="width: 24%;">
                        <h2>Loan Requisition Approval</h2>
                    </td>
                    <td align="right" valign="bottom" style="width: 38%;">
                        Search party by Name/Code/SerialNo  : 
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                    </td>
                    <td align="right" valign="bottom" style="width: 38%;">
                        <asp:RadioButton ID="rdbDate" runat="server" Text=" Search by Date :" 
                            AutoPostBack="True" GroupName="Search" oncheckedchanged="rdbDate_CheckedChanged" />
                        <asp:TextBox ID="txtFromDate" runat="server" Text="From" OnTextChanged="txtfromchanged" Width="87px" ></asp:TextBox>
                        <cc1:CalendarExtender ID="ceFromDate" runat="server" Format="yyyy/MM/dd" TargetControlID="txtFromDate" />
                        <asp:TextBox ID="txtToDate" runat="server" Text="To" Width="87px" ></asp:TextBox>
                        &nbsp;
                        &nbsp;
                        <cc1:CalendarExtender ID="ceToDate" runat="server" Format="yyyy/MM/dd" TargetControlID="txtToDate" />
                        <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png"
                                ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
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
                        <div id="DivNewWindow" style="display: none;" class="popupApprovalLoan">
                            <iframe id="IframeNew" frameborder="0" width="970px" height="530px" src="../../Controls/SRV/LDisburse.aspx"
                                class="frameborder" scrolling="no"></iframe>
                        </div>
                       
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="4">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr><td align="left">
                                <strong>Applied for Loan</strong>
                                        <%--<asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server" OnSelectedIndexChanged="change_query"
                                    AutoPostBack="true">
                                    <asp:ListItem Selected="true">Not Applied</asp:ListItem>
                                    <asp:ListItem>Applied For Loan</asp:ListItem>
                                    <asp:ListItem>Loan Approved</asp:ListItem>
                                    <asp:ListItem>Loan Disbursed</asp:ListItem>
                                </asp:RadioButtonList>--%>
                                    </td></tr> 
                                <tr>
                                    <td align="left">
                                        <asp:GridView ID="grvParty" DataKeyNames="RegistrationID" runat="server" Width="100%"
                                            AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvParty_PageIndexChanging"
                                            ShowHeaderWhenEmpty="true" OnRowDataBound="grvParty_RowDataBound"
                                            OnRowCommand="grvParty_RowCommand" EmptyDataText="No Records Found" CssClass="tablesorterBlue"
                                            AllowPaging="True" PageSize="12" DataSourceID="dsParty">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="2%" SortExpression="slno">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSl" Text='<%# Eval("RegistrationID") %>' runat="server" HorizontalAlign="Left" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Serial No" SortExpression="SerialNo" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSerialNo" Text='<%# HighlightText( Eval("SerialNo").ToString()) %>'
                                                            runat="server" HorizontalAlign="Left" /> 
                                                                           
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Carrying Loan" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCarryingLoan" Text='<%# Eval("CarryingLoan") %>' runat="server"
                                                            HorizontalAlign="Left" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="বস্তা লোন" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="4%"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBagLoan"  runat="server" HorizontalAlign="Left" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code" SortExpression="PartyCode" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="3%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPartyCode" Text='<%# HighlightText(Eval("PartyCode").ToString()) %>'
                                                            runat="server" HorizontalAlign="Left" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name" SortExpression="PartyName" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPartyName" Text='<%# HighlightText(Eval("PartyName").ToString()) %>'
                                                            runat="server" HorizontalAlign="Left" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Created" SortExpression="cdates" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="4%">
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
                                            SelectCommand="select sr.RegistrationID,sr.serialID,sr.CarryingLoan,sr.SerialNo,ip.PartyName,ip.PartyCode,ip.partyid,ip.ContactNo,convert(varchar(10),sr.createddate,10) as cdate
                                            from SRVRegistration as sr
                                            inner JOIN INVParty as ip on sr.PartyID=ip.PartyID
                                            where sr.Requisitioned='Applied For Loan' order by SerialNo asc;" 
                                            FilterExpression="PartyName LIKE '%{0}%' OR PartyCode LIKE '{1}' OR SerialNo LIKE '%{2}%'">
                                            <FilterParameters>

                                                <asp:ControlParameter Name="PartyName" ControlID="txtSearch" PropertyName="Text" />
                                                <asp:ControlParameter Name="PartyCode" ControlID="txtSearch" PropertyName="Text" />
                                                <asp:ControlParameter Name="SerialNo" ControlID="txtSearch" PropertyName="Text" />
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
        <div class="feature-box-actionBar">
            <span class="failureNotification">
                <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
                Please enter amount per bag:<asp:TextBox ID="txtamnt" runat="server" Text="300"></asp:TextBox>
            </span>
                <asp:Button ID="btnReport" runat="server" CssClass="button" Text="Generate Report"  
                        ValidationGroup="SerialValidationGroup" onclick="btnReport_Click" Width="150px"
                         />
            </div>
    </div>
</asp:Content>
