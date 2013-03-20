<%@ Page Language="C#" Title="CSMSys :: Stock Relocate" AutoEventWireup="true" CodeBehind="StockRelocate.aspx.cs"
    Inherits="CSMSys.Web.Controls.INV.StockRelocate" MasterPageFile="~/Default.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function ShowEditModal(SerialID) {
            var frame = $get('IframeEdit');
            frame.src = "../../../Controls/INV/Relocate.aspx?UIMODE=EDIT&LID=" + SerialID;
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
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="title">
        <table width="100%" border="0" cellpadding="2" cellspacing="4">
            <tbody>
                <tr>
                    <td align="left" style="width: 47%;">
                        <h2>
                            Stock Relocate</h2>
                    </td>
                    <td align="right" style="width: 44%;">
                        Search by Serial No/ Party Code/ Party Name :
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
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
    </div>
    <div class="feature-box-full">
        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Style="display: none" OnClick="btnRefresh_Click" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <contenttemplate>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
			        <tbody>
			        <tr>
				        <td align="left">
                            <asp:GridView ID="grvStockSerial" DataKeyNames="SerialNo" runat="server" Width="100%" AutoGenerateColumns="False"
                                CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvStockSerial_PageIndexChanging" ShowHeaderWhenEmpty="true" 
                                OnRowDataBound="grvStockSerial_RowDataBound" OnRowCommand="grvStockSerial_RowCommand"
                                EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True" PageSize="10" DataSourceID="dsStockSerial">
                               
                                <Columns>
                                    <asp:TemplateField HeaderText="Loading #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblli" Text='<%# Eval("lid") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Serial No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSerialno" Text='<%# (Eval("SerialNo")) %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                  
                                    
                                    <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyCode" Text='<%# Eval("PartyCode") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyName" Text='<%# Eval("PartyName") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderText="Chamber No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblChamberNo" Text='<%# Eval("ChamberNo") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Floor No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfloorno" Text='<%# Eval("Floor") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pocket No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpocketno" Text='<%# Eval("Pocket") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Last Relocated" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="14%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrelocated" Text='<%# Eval("rdates") %>' runat="server" HorizontalAlign="Left" ToolTip='<%# Eval("Relocated") %>'/> 
                                            <asp:Panel ID="Panelpopup" runat="server">
                                                    <asp:GridView ID="grvReloctable" runat="server" Width="100%" AutoGenerateColumns="False"
                                CellPadding="4" HorizontalAlign="Left" ShowHeaderWhenEmpty="true" EmptyDataText="No Records Found" CssClass="tablesorterBlue"  DataSourceID="dssReloc">
                               
                                            <Columns>
                                                <asp:TemplateField HeaderText="Chamber" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChamberNot" Text='<%# Eval("Chamber") %>' runat="server" HorizontalAlign="Left" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Floor" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblfloornot" Text='<%# Eval("Floor") %>' runat="server" HorizontalAlign="Left" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pocket" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpocketnot" Text='<%# Eval("Pocket") %>' runat="server" HorizontalAlign="Left" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpocketnot" Text='<%# Eval("Remarks") %>' runat="server" HorizontalAlign="Left" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="dssReloc" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>">
                                            </asp:SqlDataSource>
                                           </asp:Panel>
                                             <cc1:BalloonPopupExtender ID="PopupControlExtender" runat="server"
                                                   TargetControlID="lblrelocated"
                                                    BalloonPopupControlID="Panelpopup"
                                                    Position="BottomRight"
                                                   BalloonStyle="Rectangle"
                                                   BalloonSize="Medium"
                                                     UseShadow="true"
                                                    ScrollBars="Auto"
                                                     DisplayOnMouseOver="true"
                                                     DisplayOnFocus="false"
                                                     DisplayOnClick="false" > </cc1:BalloonPopupExtender>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Relocation Count" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrelocatedcnt" Text='<%# Eval("RelocatedCount") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Loading Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldate" Text='<%# Eval("cdates") %>' runat="server" HorizontalAlign="Left" ToolTip='<%# Eval("LoadedDate") %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblremarks" Text='<%# Eval("Remarks") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgedit" runat="server" CommandName="Edit" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png" ToolTip="Relocate" />
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
                                SelectCommand="select sl.LoadingID as lid,serialID,sl.SerialNo,sl.Bags,sl.PartyCode,sl.LoadedDate,CONVERT(VARCHAR(10), sl.LoadedDate, 103) AS cdates,
                                sl.Relocated,convert(varchar(10),sl.Relocated,103) as rdates,sl.RelocatedCount,par.PartyName,par.ContactNo,sl.ChamberNo,sl.Floor,sl.Pocket,sl.Line,sl.Remarks
                                from INVStockLoading sl
                                INNER JOIN INVParty as par on par.PartyCode = sl.PartyCode order by lid desc"
                                     FilterExpression="SerialNo LIKE '%{0}%' OR PartyName LIKE '%{1}%' or PartyCode like '{1}' ">
                                <FilterParameters>
                                    <asp:ControlParameter Name="SerialNo" ControlID="txtSearch" PropertyName="Text" />
                                    <asp:ControlParameter Name="PartyName" ControlID="txtSearch" PropertyName="Text" />
                                    <asp:ControlParameter Name="PartyCode" ControlID="txtSearch" PropertyName="Text" />
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
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="ModalPopupBG"
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
