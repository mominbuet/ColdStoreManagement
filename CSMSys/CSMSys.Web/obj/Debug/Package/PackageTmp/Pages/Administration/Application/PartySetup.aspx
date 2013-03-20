<%@ Page Title="CSMSys :: Customer Setup" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" 
CodeBehind="PartySetup.aspx.cs" Inherits="CSMSys.Web.Pages.INV.PartySetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">
        function ShowEditModal(PartyID) {
            var frame = $get('IframeEdit');
            frame.src = "../../../Controls/INV/Party.aspx?UIMODE=EDIT&PID=" + PartyID;
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

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="title">
  
        <table width="100%" border="0" cellpadding="2" cellspacing="4">
			<tbody>
			<tr>
                <td align="left" style="width:47%;">
                    <h2>
                        <asp:Label ID="lblcustsetup" runat="server" Text="Customer Setup"></asp:Label></h2>
                </td>
                <td align="right" valign="bottom" style="width:44%;">
                    Search by Customer Name/Contact No : <asp:TextBox ID="txtSearch" runat="server" ></asp:TextBox>
                </td>
                <td align="center" valign="bottom" style="width:3%;">
                    <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png" ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                </td>
                <td align="center" valign="bottom" style="width:3%;">
                    <asp:ImageButton ID="imgRefresh" runat="server" CommandName="Refresh" ImageUrl="~/App_Themes/Default/Images/gridview/Refresh.png" ToolTip="Refresh" Width="16px" Height="16px" OnClick="imgRefresh_Click" />
                </td>
                <td align="center" valign="bottom" style="width:3%;">
                    <asp:ImageButton ID="imgNew" runat="server" CommandName="New"  ImageUrl="~/App_Themes/Default/Images/gridview/New.png" ToolTip="New" Width="16px" Height="16px" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="ModalPopupBG"
                        runat="server" CancelControlID="ButtonNewCancel" OkControlID="ButtonNewDone" TargetControlID="imgNew"
                        PopupControlID="DivNewWindow" OnOkScript="NewOkayScript();">
                    </cc1:ModalPopupExtender>
                    <div class="popup_Buttons" style="display: none">
                        <input id="ButtonNewDone" value="Done" type="button" />
                        <input id="ButtonNewCancel" value="Cancel" type="button" />
                    </div>
                    <div id="DivNewWindow" style="display: none;" class="popupParty">
                        <iframe id="IframeNew" frameborder="0" width="870px" height="304px" src="../../../Controls/INV/Party.aspx?UIMODE=NEW" class="frameborder" scrolling="no"></iframe>
                    </div>
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" style="display:none" onclick="btnRefresh_Click" />
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
                            <asp:GridView ID="grvParty" DataKeyNames="PartyID" runat="server" Width="100%" AutoGenerateColumns="False"
                                CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvParty_PageIndexChanging" ShowHeaderWhenEmpty="true" 
                                OnRowDataBound="grvParty_RowDataBound" OnRowCommand="grvParty_RowCommand"
                                EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True" PageSize="14" DataSourceID="dsParty">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSl" Text='<%# Eval("SlNo") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PartyID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyID" Text='<%# Eval("PartyID") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyType" Text='<%# Eval("PartyType") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyCode" Text='<%# HighlightText(Eval("PartyCode").ToString()) %>' runat="server" HorizontalAlign="Left"  Font-Size="13px"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyName" Text='<%# HighlightText(Eval("PartyName").ToString()) %>' runat="server" Font-Size="14px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Father" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFather" Text='<%# Eval("FatherName") %>' runat="server" HorizontalAlign="Left"  Font-Size="14px"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactNo" Text='<%# Eval("ContactNo") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Area/Village" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAreaVillage" Text='<%# Eval("AreaVillageName") %>' runat="server" HorizontalAlign="Left"  Font-Size="14px"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAreaPO" Text='<%# Eval("AreaPOName") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="স্টকে বস্তা" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbc" Text='<%# Eval("bagcount") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgEdit" runat="server" CommandName="Edit" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png" ToolTip="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  <%--  <asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgDelete" runat="server" CommandName="Delete" OnClientClick='return confirm("Are you sure you want to Delete?");' ImageUrl="~/App_Themes/Default/Images/gridview/Delete.png" ToolTip="Delete" />
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
                                SelectCommand="SELECT ROW_NUMBER() OVER (ORDER BY ip.PartyID) As SlNo, ip.PartyID, ip.PartyType, ip.PartyCode,ip.bagcount, ip.PartyName, ip.FatherName, ip.ContactNo, ip.AreaVillageName, ip.AreaPOName, au.UpazilaPSName, ad.DistrictName, ip.Tel, ip.Cell, ip.Email 
                                                FROM INVParty AS ip INNER JOIN ADMDistrict AS ad ON ip.DistrictID = ad.DistrictID INNER JOIN
                                                        ADMUpazilaPS AS au ON ip.UpazilaPSID = au.UpazilaPSID order by ip.partycode" 
                                                        FilterExpression="PartyName LIKE '%{0}%' OR PartyCode LIKE '{1}%'  OR FatherName LIKE '{2}%'  OR AreaVillageName LIKE '{3}%'">
                                <FilterParameters>
                                    <asp:ControlParameter Name="PartyName" ControlID="txtSearch" PropertyName="Text" />
                                    <asp:ControlParameter Name="PartyCode" ControlID="txtSearch" PropertyName="Text" />
                                    <asp:ControlParameter Name="FatherName" ControlID="txtSearch" PropertyName="Text" />
                                    <asp:ControlParameter Name="AreaVillageName" ControlID="txtSearch" PropertyName="Text" />
                                </FilterParameters>
                            </asp:SqlDataSource>
                        </td>
			        </tr>
			        </tbody>
		        </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <asp:Button ID="ButtonEdit" runat="server" Text="Submit" style="display:none" />
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" BackgroundCssClass="ModalPopupBG"
        runat="server" CancelControlID="ButtonEditCancel" OkControlID="ButtonEditDone" 
        TargetControlID="ButtonEdit" PopupControlID="DivEditWindow" 
        OnCancelScript="EditCancelScript();" OnOkScript="EditOkayScript();"
        BehaviorID="EditModalPopup">
    </cc1:ModalPopupExtender>
    <div class="popup_Buttons" style="display: none">
        <input id="ButtonEditDone" value="Done" type="button" />
        <input id="ButtonEditCancel" value="Cancel" type="button" />
    </div>
    <div id="DivEditWindow" style="display: none;" class="popupParty">
        <iframe id="IframeEdit" frameborder="0" width="870px" height="304px" class="frameborder" scrolling="no">
        </iframe>
    </div>
</asp:Content>
