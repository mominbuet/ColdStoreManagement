<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BLoan.aspx.cs" Inherits="CSMSys.Web.Controls.SRV.BLoan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Enter" />
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Exit" />
    <title></title>
    <link href="../../App_Themes/Default/Styles/Default.css" rel="stylesheet" type="text/css" />
    <script src="../../App_Themes/Default/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <link href="../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function getbacktostepone() {
            window.location = "BLoan.aspx";
        }
        function onSuccess() {
            setTimeout(okay, 2000);
        }
        function onError() {
            setTimeout(cancel, 2000);
        }
        function okay() {
            var UIMODE = $get('hdnWindowUIMODE').value;
            if (UIMODE == "EDIT")
                window.parent.document.getElementById('ButtonEditDone').click();
            else {
                window.parent.document.getElementById('ButtonNewDone').click();
                getbacktostepone();
            }
        }
        function cancel() {
            var UIMODE = $get('hdnWindowUIMODE').value;
            if (UIMODE == "EDIT")
                window.parent.document.getElementById('ButtonEditCancel').click();
            else
                window.parent.document.getElementById('ButtonNewCancel').click();
        }
    </script>

    <script type="text/javascript">
        $().ready(function () {

            $('#txtBag').keyup(function () {

                $('#txtAmnt').val($('#txtBag').val() * $('#txtAmtPerBag').val());

            });

            $('#txtAmtPerBag').keyup(function () {

                $('#txtAmnt').val($('#txtBag').val() * $('#txtAmtPerBag').val());

            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" value="" runat="server" id="hdnWindowUIMODE" />
    <input type="hidden" value="" runat="server" id="hdnPartyID" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="popup_Container">
        <div class="popup_Titlebar" id="PopupHeader">
            <div class="TitlebarLeft">
                Bag Loan
            </div>
            <div class="TitlebarRight" onclick="cancel();">
            </div>
        </div>
        <div class="popup_Body">
            <asp:MultiView ID="MultiViewSerial" runat="server">
                <asp:View ID="ViewInput" runat="server">
                    <asp:Panel ID="pnlNew" runat="server" Width="100%">
                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                            <tbody>
                                <tr>
                                    <td align="left" valign="top" style="width: 40%;">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                            <tbody>
                                                <tr>
                                                    <td align="left" colspan="2">
                                                        <strong>General Information </strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 40%;">
                                                        Party Code:
                                                    </td>
                                                    <td align="left" style="width: 60%;">
                                                        <asp:TextBox ID="txtCode" runat="server" Enabled="False" EnableTheming="True"></asp:TextBox>
                                                        <asp:TextBox ID="txtpartycode" runat="server" Visible="false"></asp:TextBox>
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Party Name :
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtName" runat="server" Width="238px" Enabled="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 40%;">
                                                        New Issue Limit (%)
                                                    </td>
                                                    <td align="left" style="width: 60%;">
                                                        <asp:TextBox ID="txtperc" runat="server" Enabled="False" EnableTheming="True"></asp:TextBox>
                                                        
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 40%;">
                                                        Bags Already Loaned
                                                    </td>
                                                    <td align="left" style="width: 60%;">
                                                        <asp:TextBox ID="txtbloaned" runat="server" Enabled="False" EnableTheming="True"></asp:TextBox>
                                                        
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 40%;">
                                                        Bags Already Loaded
                                                    </td>
                                                    <td align="left" style="width: 60%;">
                                                        <asp:TextBox ID="txtbstock" runat="server" Enabled="False" EnableTheming="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="2">
                                                        <strong>Bags Loan Details </strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Date Disbursed
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtdatedisbursed" runat="server" Text=""></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtdatedisbursed"
                                                            CssClass="failureNotification" ErrorMessage="Date is required." ToolTip="Date is required."
                                                            ValidationGroup="SerialValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Format="yyyy/MM/dd" TargetControlID="txtdatedisbursed" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        No of Bags Loan:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtBag" runat="server" ValidationGroup="^\d*"></asp:TextBox>
                                                        <asp:TextBox ID="txtBagLoanID" runat="server" 
                                                            ValidationGroup="^\d*" Enabled="False" Visible="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Amount/Bag:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtAmtPerBag" runat="server" ValidationGroup="^\d*" Text="80"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td align="left">
                                                        Bags Loan Amount:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtAmnt" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td align="left" colspan="2">
                                                        <div class="feature-box-actionBar">
                                                            <span class="failureNotification">
                                                                <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
                                                            </span>
                                                            <%--<asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="SerialValidationGroup"
                                                                OnClick="btnsave_click" />--%>
                                                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClick="btnSave_Click" />
                                                        </div>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td align="left">
                                                        Branch Name:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtBranch" runat="server" Width="238px"></asp:TextBox>
                                                    </td>
                                                </tr>--%>
                                                
                                            </tbody>
                                        </table>
                                    </td>
                                    <td align="left" valign="top" style="width: 60%;">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                            <tbody>
                                                <tr>
                                                    <td align="left">
                                                        Search Party by Name Code / CaseID &nbsp;
                                                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                                        <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png"
                                                            ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                                                        &nbsp;
                                                        <asp:ImageButton ID="imgRefresh" runat="server" CommandName="Refresh" ImageUrl="~/App_Themes/Default/Images/gridview/Refresh.png"
                                                            ToolTip="Refresh" Width="16px" Height="16px" OnClick="imgRefresh_Click" />
                                                    </td>
                                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Style="display: none" OnClick="btnRefresh_Click" />
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:GridView ID="grvParty" DataKeyNames="PartyID" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                            CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvParty_PageIndexChanging"
                                                                            ShowHeaderWhenEmpty="true" OnRowDataBound="grvParty_RowDataBound" OnRowCommand="grvParty_RowCommand"
                                                                            EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True"
                                                                            PageSize="7" DataSourceID="dsParty">
                                                                            <Columns>
                                                                                <asp:TemplateField ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgEdit" runat="server" CommandName="Select" ImageUrl="~/App_Themes/Default/Images/gridview/select.png"
                                                                                            ToolTip="Edit" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <%--<asp:TemplateField HeaderText="Sl #" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSl" Text='<%# Eval("SlNo") %>' runat="server" HorizontalAlign="Left" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>--%>
                                                                                <asp:TemplateField HeaderText="PartyID" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPartyID" Text='<%# Eval("PartyID") %>' runat="server" HorizontalAlign="Left" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPartyType" Text='<%# Eval("PartyType") %>' runat="server" HorizontalAlign="Left" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPartyCode" Text='<%# Eval("PartyCode") %>' runat="server" HorizontalAlign="Left" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-Width="15%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPartyName" Text='<%# HighlightText(Eval("PartyName").ToString()) %>'
                                                                                            runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                  <asp:TemplateField HeaderText="Father" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-Width="10%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblFName" Text='<%# HighlightText(Eval("fathername").ToString()) %>'
                                                                                            runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Village" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-Width="10%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblvillage" Text='<%# HighlightText(Eval("AreaVillageName").ToString()) %>'
                                                                                            runat="server" />
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
                                                                            SelectCommand="SELECT ROW_NUMBER() OVER (ORDER BY ip.PartyID) As SlNo, ip.PartyID, ip.PartyType, ip.PartyCode, ip.PartyName,ip.fathername,ip.areavillagename, ip.ContactNo,ip.bagcount, ip.DistrictID, ad.DistrictName
                                                                        FROM INVParty AS ip INNER JOIN ADMDistrict AS ad ON ip.DistrictID = ad.DistrictID"
                                                                            FilterExpression="PartyName LIKE '%{0}%' OR PartyCode LIKE '{1}%' OR ContactNo LIKE '{2}%' OR FatherName LIKE '{3}%' OR AreaVillageName LIKE '{4}%'">
                                                                    <FilterParameters>
                                                                        <asp:ControlParameter Name="PartyName" ControlID="txtSearch" PropertyName="Text" />
                                                                        <asp:ControlParameter Name="PartyCode" ControlID="txtSearch" PropertyName="Text" />
                                                                        <asp:ControlParameter Name="ContactNo" ControlID="txtSearch" PropertyName="Text" />
                                                                        <asp:ControlParameter Name="FatherName" ControlID="txtSearch" PropertyName="Text" />
                                                                        <asp:ControlParameter Name="AreaVillageName" ControlID="txtSearch" PropertyName="Text" />
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
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:Panel>
                </asp:View>
                <asp:View ID="ViewSuccess" runat="server">
                    <asp:Panel ID="pnlSuccess" runat="server" Width="100%">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td align="center">
                                        <span class="succesNotification">Bag Loan Saved/Edited Successfully.
                                            <br />
                                            Dialog will Close automatically within 2 Seconds </span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:Panel>
                </asp:View>
                <asp:View ID="ViewError" runat="server">
                    <asp:Panel ID="pnlError" runat="server" Width="100%">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td align="center" valign="middle">
                                        <span class="failureNotification">Error Occured Saving/Editing Bag loan<br />
                                            Dialog will Close automatically within 2 Seconds </span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:Panel>
                </asp:View>
            </asp:MultiView>
        </div>
        <div class="popup_Buttons" style="display: none;">
            <asp:Button ID="btnOkay" Text="Done" runat="server" />
            <input id="btnCancel" value="Cancel" type="button" onclick="cancel();" />
        </div>
    </div>
    </form>
</body>
</html>
