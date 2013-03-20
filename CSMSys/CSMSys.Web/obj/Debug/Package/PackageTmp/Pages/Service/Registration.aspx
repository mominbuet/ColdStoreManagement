<%@ Page Title="CSMSys :: Registration" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" 
CodeBehind="Registration.aspx.cs" Inherits="CSMSys.Web.Pages.Service.Registration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">
        function ShowEditModal(RegistrationID) {
            var frame = $get('IframeEdit');
            frame.src = "../../Controls/SRV/Registration.aspx?UIMODE=EDIT&PID=" + RegistrationID;
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
                    <h2>Agreement Edit</h2>
                </td>
                <td align="right" valign="bottom" style="width:44%;">
                    Search by নাম/Contact No/Village/এস আর : <asp:TextBox ID="txtSearch" runat="server" ></asp:TextBox>
                </td>
                <td align="center" valign="bottom" style="width:3%;">
                    <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png" ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                </td>
                <td align="center" valign="bottom" style="width:3%;">
                    <asp:ImageButton ID="imgRefresh" runat="server" CommandName="Refresh" ImageUrl="~/App_Themes/Default/Images/gridview/Refresh.png" ToolTip="Refresh" Width="16px" Height="16px" OnClick="imgRefresh_Click" />
                </td>
                <td align="center" valign="bottom" style="width:3%;">
                 
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
                            <asp:GridView ID="grvRegistration" DataKeyNames="RegistrationID" runat="server" Width="100%" AutoGenerateColumns="False"
                                CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvRegistration_PageIndexChanging" ShowHeaderWhenEmpty="true" 
                                OnRowDataBound="grvRegistration_RowDataBound" OnRowCommand="grvRegistration_RowCommand"
                                EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True" PageSize="15" DataSourceID="dsRegistration">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSl" Text='<%# Eval("SlNo") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RegistrationID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegistrationID" Text='<%# Eval("RegistrationID") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Party Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyType" Text='<%# Eval("PartyType") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="নাম" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyName" Text='<%# HighlightText(Eval("PartyName").ToString()) %>' runat="server" Font-Size="14px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="পিতার নাম" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfathrname" Text='<%# HighlightText(Eval("fathername").ToString()) %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="গ্রাম" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblvill" Text='<%# HighlightText(Eval("AreaVillageName").ToString()) %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactNo" Text='<%# HighlightText(Eval("ContactNo").ToString()) %>' runat="server" Font-Size="14px" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="এস আর" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsr" Text='<%# Eval("serialno") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="বস্তা লোন" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBagLoan" Text='<%# Eval("BagLoan") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ক্যরিং" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCarryingLoan" Text='<%# Eval("CarryingLoan") %>' runat="server" HorizontalAlign="Left" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgEdit" runat="server" CommandName="Edit" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png" ToolTip="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Center">
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
                            <asp:SqlDataSource ID="dsRegistration" runat="server" ConnectionString="<%$ ConnectionStrings:CSMSysConnection %>" 
                                SelectCommand="SELECT ROW_NUMBER() OVER (ORDER BY sr.RegistrationID) As SlNo,ip.FatherName,ip.AreaVillageName, sr.RegistrationID, sr.PartyID, ip.PartyType, ip.PartyCode, ip.PartyName, ip.ContactNo, sr.BagLoan, sr.CarryingLoan, sr.Remarks, sr.SerialID, sr.SerialNo, ss.Serial, ss.Bags
                                                FROM SRVRegistration AS sr INNER JOIN INVParty AS ip ON sr.PartyID = ip.PartyID INNER JOIN
                                                INVStockSerial AS ss ON sr.SerialID = ss.SerialID" 
                                FilterExpression="PartyName LIKE '%{0}%' OR ContactNo LIKE '{1}%' OR SerialNo LIKE '{2}%' OR AreaVillageName LIKE '{3}%'">
                                <FilterParameters>
                                    <asp:ControlParameter Name="PartyName" ControlID="txtSearch" PropertyName="Text" />
                                    <asp:ControlParameter Name="ContactNo" ControlID="txtSearch" PropertyName="Text" />
                                    <asp:ControlParameter Name="SerialNo" ControlID="txtSearch" PropertyName="Text" />
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
    <div id="DivEditWindow" style="display: none;" class="popupRegistration">
        <iframe id="IframeEdit" frameborder="0" width="900px" height="550px" class="frameborder" scrolling="no">
        </iframe>
    </div>
</asp:Content>
