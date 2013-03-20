<%@ Page Title="CSMSys :: Stock Serial" Language="C#" MasterPageFile="~/Default.Master"
    AutoEventWireup="true" CodeBehind="StockSerial.aspx.cs" Inherits="CSMSys.Web.Pages.INV.StockSerial" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function ShowEditModal(SerialID) {
            var frame = $get('IframeEdit');
            frame.src = "../../../Controls/INV/Serial.aspx?UIMODE=EDIT&PID=" + SerialID;
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
                        <h2>
                            Stock Serial
                        </h2>
                    </td>
                    <td align="right" style="width: 74%;">
                        Search by Customer Name/Code :
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        <%--<br />--%>
                        <asp:RadioButton ID="rdbDate" runat="server" AutoPostBack="true" Text=" Search by Date :" 
                            GroupName="Search" oncheckedchanged="rdbDate_CheckedChanged"  />
                         <asp:TextBox ID="txtFromDate" runat="server" Text="From" Width="87px" 
                            Enabled="False"></asp:TextBox>
                        <cc1:CalendarExtender ID="ceFromDate" runat="server" Format="yyyy/MM/dd" TargetControlID="txtFromDate" />
                        <asp:TextBox ID="txtToDate" runat="server" Text="To" Width="87px" 
                            Enabled="False"></asp:TextBox>
                        <cc1:CalendarExtender ID="ceToDate" runat="server" Format="yyyy/MM/dd" TargetControlID="txtToDate" />
                    </td>
                    <td align="center" style="width: 3%;">
                        <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png"
                            ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                    </td>
                    <td align="center" style="width: 3%;">
                        <asp:ImageButton ID="imgRefresh" runat="server" CommandName="Refresh" ImageUrl="~/App_Themes/Default/Images/gridview/Refresh.png"
                            ToolTip="Refresh" Width="16px" Height="16px" OnClick="imgRefresh_Click" />
                    </td>
                    <td align="center" style="width: 3%;">
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
                        <div id="DivNewWindow" style="display: none;" class="popupStockSerial">
                            <iframe id="IframeNew" frameborder="0" width="950px" height="450px" src="../../../Controls/INV/Serial.aspx"
                                class="frameborder" scrolling="no"></iframe>
                        </div>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Style="display: none" OnClick="btnRefresh_Click" />
                    </td>
                    <td align="center" style="width: 3%;">
                        <asp:ImageButton ID="imgnewparty" runat="server" CommandName="New" ImageUrl="~/App_Themes/Default/Images/gridview/add_user_256.png"
                            ToolTip="New" Width="16px" Height="16px" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender3" BackgroundCssClass="ModalPopupBG"
                            runat="server" CancelControlID="ButtonNewCancel" OkControlID="ButtonNewDone"
                            TargetControlID="imgnewparty" PopupControlID="DivNewPWindow" OnOkScript="NewOkayScript();">
                        </cc1:ModalPopupExtender>

                        <div id="DivNewPWindow" style="display: none;" class="popupParty">
                            <iframe id="Iframe1" frameborder="0" width="870px" height="304px" src="../../../Controls/INV/Party.aspx?UIMODE=NEW" class="frameborder" scrolling="no"></iframe>

                        </div>
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
                                <asp:GridView ID="grvStockSerial" DataKeyNames="SerialID" runat="server" Width="100%"
                                    AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvStockSerial_PageIndexChanging"
                                    ShowHeaderWhenEmpty="true" OnRowDataBound="grvStockSerial_RowDataBound" OnRowCommand="grvStockSerial_RowCommand"
                                    EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True"
                                    PageSize="15" DataSourceID="dsStockSerial">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSl" Text='<%# Eval("SlNo") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SerialID" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSerialID" Text='<%# Eval("SerialID") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SR No" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSerialNo" Text='<%# HighlightText(Eval("SerialNo").ToString()) %>'
                                                    runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPartyCode" Text='<%# HighlightText(Eval("PartyCode").ToString()) %>'
                                                    runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPartyName" Text='<%# HighlightText(Eval("PartyName").ToString()) %>'
                                                    runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContactNo" Text='<%# Eval("ContactNo") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblinserted" Text='<%# Eval("Inserted","{0:MMM dd, yyyy}") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblremarks" Text='<%# Eval("Remarks") %>' runat="server" HorizontalAlign="Left" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hplprint" runat="server" Target="_blank">Print</asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgEdit" runat="server" CommandName="Edit" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png"
                                                    ToolTip="Edit" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgDelete" runat="server" CommandName="Delete" OnClientClick='return confirm("Are you sure you want to Delete?");'
                                                    ImageUrl="~/App_Themes/Default/Images/gridview/Delete.png" ToolTip="Delete" />
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
                                <asp:SqlDataSource ID="dsStockSerial" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>"
                                    SelectCommand="SELECT ROW_NUMBER() OVER (ORDER BY ss.SerialID) As SlNo,ss.remarks,Convert(varchar(10),ss.SerialDate,103) as Inserted, ss.SerialID, ss.Serial, ss.Bags, ss.SerialNo, ss.PartyID, ss.PartyCode, ip.PartyName, ip.ContactNo
                                    FROM INVStockSerial AS ss 
                                    INNER JOIN INVParty AS ip ON ss.PartyID = ip.PartyID 
                                    ORDER BY ss.SerialID DESC"
                                    FilterExpression="SerialNo LIKE '%{0}%' OR PartyCode LIKE '%{1}%' or PartyName LIKE '%{2}%'">
                                    <FilterParameters>
                                        <asp:ControlParameter Name="SerialNo" ControlID="txtSearch" PropertyName="Text" />
                                        <asp:ControlParameter Name="PartyCode" ControlID="txtSearch" PropertyName="Text" />
                                        <asp:ControlParameter Name="PartyName" ControlID="txtSearch" PropertyName="Text" />
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
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
         <div class="feature-box-actionBar">
            <span class="failureNotification">
                <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
            </span>
            <asp:Button ID="btnReport" runat="server" CssClass="button" Text="Generate Report" 
                onclick="btnReport_Click" Visible="False" />
        </div>
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
    <div id="DivEditWindow" style="display: none;" class="popupStockSerial">
        <iframe id="IframeEdit" frameborder="0" width="950px" height="382px" class="frameborder"
            scrolling="no"></iframe>
    </div>
</asp:Content>
