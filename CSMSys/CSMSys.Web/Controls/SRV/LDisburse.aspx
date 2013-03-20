<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LDisburse.aspx.cs" Inherits="CSMSys.Web.Controls.SRV.LDisburse" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Enter" />
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Exit" />
    <title></title>
    <link href="../../../App_Themes/Default/Styles/Default.css" rel="stylesheet" type="text/css" />
    <script src="../../App_Themes/Default/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <link href="../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function getbacktostepone() {
            window.location = "LDisburse.aspx";
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
    <%--<script type="text/javascript">

        $(document).ready(function () {
            $("#<%= txtloanamount.ClientID %>").bind("keyup", function (e) {
                //             alert(e.keyCode);
                $('#<%= lbltotloan.ClientID %>').text($('#<%= lblbags.ClientID %>').text * $('#<%= txtloanamount.ClientID %>').text);

            });

            //         $('#txtBags').keyup(function () {

            //             $('#txtSerialNo').val($('#txtSerial').val() + "/" + $('#txtBags').val());

            //         });
        });
    </script>--%>
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
                Loan Requisition Approval
            </div>
            <div class="TitlebarRight" onclick="cancel();">
            </div>
        </div>
        <div class="popup_Body">
            <asp:MultiView ID="MultiViewSerial" runat="server">
                <asp:View ID="ViewInput" runat="server">
                    <asp:UpdatePanel ID="pnlNew" runat="server" Width="100%">
                        <ContentTemplate>
                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                            <tbody>
                                <tr>
                                    <td align="left" valign="top" style="width: 35%;">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                            <tbody>
                                                <tr>
                                                    <td align="left" style="width: 40%;">
                                                        Case ID:
                                                    </td>
                                                    <td align="left" style="width: 60%;">
                                                        <asp:Label ID="lblcase" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>    
                                                 <tr>
                                                    <td align="left">
                                                        Bags Applied For:
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblbagsApplied" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Party Code:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtpartycode" runat="server" Enabled="False"></asp:TextBox>
                                                        <asp:TextBox ID="txtpartyID" runat="server" Visible="False" ></asp:TextBox>
                                                        &nbsp;&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                            ControlToValidate="txtpartycode" CssClass="failureNotification" ErrorMessage="Loan amount is needed."
                                                            ToolTip="Party Code is needed" ValidationGroup="SerialValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td align="left">
                                                        Serial No:
                                                    </td>
                                                    <td align="left">
                                                        <asp:ListBox ID="lstSerial" runat="server" Width="45%" SelectionMode="Multiple" AutoPostBack="True" OnSelectedIndexChanged="lst_indchanged">
                                                        </asp:ListBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Bags :
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtbags_selected" runat="server" AutoPostBack="True" OnTextChanged="txtbags_changed"></asp:TextBox>  
                                                          
                                                    </td>
                                                </tr>   
                                                <tr>
                                                    <td align="left">
                                                        Serials :
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblserials" runat="server" Text="" Visible="false"></asp:Label> 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Per Bag Loan
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtloanamount" runat="server" AutoPostBack="True" OnTextChanged="txtloanchanged"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtloanamount"
                                                            CssClass="failureNotification" ErrorMessage="Loan amount is needed." ToolTip="Loan amount is needed"
                                                            ValidationGroup="SerialValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td align="left">
                                                        Total Loan
                                                    </td>
                                                    <td align="left">
                                                        <asp:Image ID="imageload" ImageUrl="../../App_Themes/Default/Images/ajax-loader.gif" runat="server" /> 
                                                        <asp:Label ID="lbltotloan" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Last date to repay
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtlastdate" runat="server" Text="2013/10/15"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtlastdate"
                                                            CssClass="failureNotification" ErrorMessage="Date is required." ToolTip="Date is required."
                                                            ValidationGroup="SerialValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  Format="yyyy/MM/dd" TargetControlID="txtlastdate" />
                                                    </td>
                                                </tr>
                                                  <tr>
                                                    <td align="left">
                                                        Loan Disbursed
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
                                                        Remarks :
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="2">
                                                        <div class="feature-box-actionBar">
                                                            <span class="failureNotification">
                                                                <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
                                                            </span>
                                                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="SerialValidationGroup"
                                                                OnClick="save_loan" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td align="left" valign="top" style="width: 65%;">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                            <tbody>
                                                <tr>
                                                    <td align="left">
                                                        Search Customer by Name or Party Code: &nbsp;
                                                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                                        <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png"
                                                            ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click" />
                                                        <asp:ImageButton ID="imgRefresh" runat="server" CommandName="Refresh" ImageUrl="~/App_Themes/Default/Images/gridview/Refresh.png"
                                                            ToolTip="Refresh" Width="16px" Height="16px" OnClick="imgRefresh_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:GridView ID="grvParty" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                            CellPadding="3" HorizontalAlign="Left" OnPageIndexChanging="grvParty_PageIndexChanging"
                                                                            ShowHeaderWhenEmpty="true" OnRowDataBound="grvParty_RowDataBound" OnRowCommand="grvParty_RowCommand"
                                                                            EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True"
                                                                            PageSize="10" DataSourceID="dsParty">
                                                                            <Columns>
                                                                                <asp:TemplateField ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgselect" runat="server" CommandName="Select" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png"
                                                                                            ToolTip="Select" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Party Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblpartycode" Text='<%# Eval("PartyCode") %>' runat="server" HorizontalAlign="Left" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Party Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPartyName" Text='<%# Eval("PartyName") %>' runat="server" HorizontalAlign="Left" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Total Bag Loan" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbltotalbag" Text='<%# Eval("bagloan") %>' runat="server" HorizontalAlign="Left" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>     
                                                                                <asp:TemplateField HeaderText="Total Carrying Loan" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblcarryloan" Text='<%# Eval("carryloan") %>' runat="server" HorizontalAlign="Left" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>    
                                                                                 <asp:TemplateField HeaderText="Total Loan Disbursed" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbltotloandisbursed" runat="server" HorizontalAlign="Left" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Serials Applied" HeaderStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-Width="7%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbltotalserials" Text='<%# Eval("total_serials") %>' runat="server" HorizontalAlign="Left" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>  
                                                                                
                                                                                <asp:TemplateField HeaderText="Bags On Store" HeaderStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-Width="7%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbltotbags"  runat="server" HorizontalAlign="Left" />
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
                                                                            SelectCommand="SELECT COUNT(sr.RegistrationID) as total_serials,sr.PartyID,PartyName,sum(sr.bagloan) as bagloan,sum(sr.carryingloan) as carryloan,INVParty.PartyCode
                                                                            from SRVRegistration as sr
                                                                            INNER JOIN INVParty on sr.PartyID=INVParty.PartyID
                                                                            GROUP BY PartyCode,PartyName,sr.Requisitioned,sr.PartyID
                                                                            having sr.Requisitioned='Applied For Loan'
                                                                            order by total_serials desc;" 
                                                                        FilterExpression="PartyName LIKE '%{0}%' OR PartyCode  like '{1}%'">
                                                                            <FilterParameters>
                                                                                <asp:ControlParameter Name="PartyName" ControlID="txtSearch" PropertyName="Text" />
                                                                                <asp:ControlParameter Name="PartyCode" ControlID="txtSearch" PropertyName="Text" />

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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:View>
                <asp:View ID="ViewSuccess" runat="server">
                    <asp:Panel ID="pnlSuccess" runat="server" Width="100%">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td align="center">
                                        <span class="succesNotification">Serial Saved/Edited Successfully.
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
                                        <span class="failureNotification">Error Occured Saving/Editing Serial<br />
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
            <asp:Button ID="btnOkay" Text="Done" runat="server" ValidationGroup="SerialValidationGroup"
                OnClick="save_loan" />
            <input id="btnCancel" value="Cancel" type="button" onclick="cancel();" />
        </div>
    </div>
    </form>
</body>
</html>
