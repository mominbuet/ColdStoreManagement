<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Party.aspx.cs" Inherits="CSMSys.Web.Controls.INV.Party" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Enter" />
	<meta content="blendTrans(Duration=0.5)" http-equiv="Page-Exit" />
    <title></title>
    <link href="../../../App_Themes/Default/Styles/Default.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function getbacktostepone() {
            window.location = "Party.aspx";
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
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" value="" runat="server" id="hdnWindowUIMODE" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="popup_Container">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Customer Setup
                </div>
                <div class="TitlebarRight" onclick="cancel();"></div>
            </div>
            <div class="popup_Body">
                <asp:UpdatePanel ID="updtpanel" runat="server">
                    <ContentTemplate>          
                <asp:MultiView ID="MultiViewParty" runat="server">
                    <asp:View ID="ViewInput" runat="server">
                        <asp:Panel ID="pnlNew" runat="server" width="100%">
                            <table width="100%" border="0" cellpadding="0" cellspacing="4">
			                    <tbody>
			                    <tr>
				                    <td align="left" valign="top" style="width:50%;">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
			                                <tbody>
			                                <tr>
				                                <td align="left" colspan="2"><strong>General Information </strong></td>
			                                </tr>
			                                <tr>
				                                <td align="left" style="width:30%;">Customer Type : </td>
				                                <td align="left" style="Width:70%;">
                                                    <asp:DropDownList ID="ddlType" runat="server" Width="262px" AutoPostBack="True" OnSelectedIndexChanged="ddltype_changed">
                                                    </asp:DropDownList>
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left">Customer Code : </td>
				                                <td align="left">
                                                    <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfValidator1" runat="server" ControlToValidate="txtCode"
                                                    CssClass="failureNotification" ErrorMessage="Code is required." ToolTip="Code is required."
                                                    ValidationGroup="PartyValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator> 
                                                    <%--<cc1:BalloonPopupExtender ID="Balloonpopupextender1" runat="server" TargetControlID="txtCode"
                                                        BalloonPopupControlID="lblseridtobe" Position="BottomRight" BalloonStyle="Rectangle"
                                                        BalloonSize="Small" UseShadow="true" ScrollBars="Auto" DisplayOnMouseOver="True"
                                                        DisplayOnFocus="False" DisplayOnClick="False" />
                                                    <asp:Label runat="server" ID="lblseridtobe">
                                                        </asp:Label>--%>
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left">Customer Name : </td>
				                                <td align="left"><asp:TextBox ID="txtName" runat="server" Width="258px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfValidator2" runat="server" ControlToValidate="txtName"
                                                    CssClass="failureNotification" ErrorMessage="Name is required." ToolTip="Name is required."
                                                    ValidationGroup="PartyValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left">Father's Name : </td>
				                                <td align="left">
                                                    <asp:TextBox ID="txtFather" runat="server" Width="258px"></asp:TextBox>
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left">Contact No : </td>
				                                <td align="left">
                                                    <asp:TextBox ID="txtContactNo" runat="server" Width="258px"></asp:TextBox>
                                                </td>
			                                </tr>
			                                <%--<tr>
				                                <td align="left" style="width:30%;">Gender : </td>
				                                <td align="left" style="Width:70%;">
                                                    <asp:DropDownList ID="ddlGender" runat="server" Width="262px">
                                                    </asp:DropDownList>
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left" style="width:30%;">Religion : </td>
				                                <td align="left" style="Width:70%;">
                                                    <asp:DropDownList ID="ddlReligion" runat="server" Width="262px">
                                                    </asp:DropDownList>
                                                </td>
			                                </tr>--%>
			                                <tr>
				                                <td align="left" colspan="2"><strong>Bags Loading Information</strong> </td>
			                                </tr>
			                                <tr>
				                                <td align="left">Bags Loaded (%) : </td>
				                                <td align="left">
                                                    <asp:TextBox ID="txtBLoan" runat="server" Text="70"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfValidator3" runat="server" ControlToValidate="txtBLoan"
                                                    CssClass="failureNotification" ErrorMessage="Bags Loaded is required." ToolTip="Bags Loaded is required."
                                                    ValidationGroup="PartyValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                                </td>
			                                </tr>
			                                </tbody>
		                                </table>
                                    </td>
				                    <td align="left" valign="top" style="width:50%;">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
			                                <tbody>
			                                <tr>
				                                <td align="left" colspan="2"><strong>Address </strong></td>
			                                </tr>
			                                <tr>
				                                <td align="left" valign="top" style="width:30%;">Area/Village : </td>
				                                <td align="left" style="Width:70%;">
                                                    <asp:TextBox ID="txtVillage" runat="server" Width="258px"></asp:TextBox>
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left" valign="top" style="width:30%;">PO : </td>
				                                <td align="left" style="Width:70%;">
                                                    <asp:TextBox ID="txtPO" runat="server" Width="258px"></asp:TextBox>
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left" style="width:30%;">District : </td>
				                                <td align="left" style="Width:70%;">
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" Width="262px" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left" style="width:30%;">Upazila/PS : </td>
				                                <td align="left" style="Width:70%;">
                                                    <asp:DropDownList ID="ddlUpazila" runat="server" Width="262px">
                                                    </asp:DropDownList>
                                                </td>
			                                </tr>
			                                <tr>
                                                <td align="left" colspan="2"><strong>Ledger Information </strong></td>
				                                <%--<td align="left" style="width:30%;">Tel : </td>
				                                <td align="left" style="Width:70%;">
                                                    <asp:TextBox ID="txtTel" runat="server" Width="258px"></asp:TextBox>
                                                </td>--%>
			                                </tr>
			                                <tr>
				                                <td align="left" style="width:30%;">Parent Ledger : </td>
				                                <td align="left" style="Width:70%;">
                                                    <asp:TextBox ID="txtParent" runat="server" Width="258px" ReadOnly="true"></asp:TextBox>
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left" style="width:30%;">Ledger No : </td>
				                                <td align="left" style="Width:70%;">
                                                    <asp:TextBox ID="txtLedger" runat="server" Width="258px" ReadOnly="true"></asp:TextBox>
                                                </td>
			                                </tr>
			                                </tbody>
		                                </table>
                                    </td>
			                    </tr>
			                    </tbody>
		                    </table>
                        </asp:Panel>

                        <div class="feature-box-actionBar">
                            <span class="failureNotification">
                                <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
                            </span>
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" 
                                ValidationGroup="PartyValidationGroup" OnClick="btnSave_Click" />
                        </div>
                    </asp:View>
                    <asp:View ID="ViewSuccess" runat="server">
                        <asp:Panel ID="pnlSuccess" runat="server" width="100%">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" style="height:284px;">
			                    <tbody>
                                <tr>
                                    <td align="center">
                                        <span class="succesNotification">
                                            Party Saved/Edited Successfully. <br />
                                            Dialog will Close automatically within 2 Seconds
                                        </span>
                                    </td>
                                </tr>
			                    </tbody>
		                    </table>
                        </asp:Panel>
                    </asp:View>
                    <asp:View ID="ViewError" runat="server">
                        <asp:Panel ID="pnlError" runat="server" width="100%">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" style="height:284px;">
			                    <tbody>
                                <tr>
                                    <td align="center" valign="middle">
                                        <span class="failureNotification">
                                            Error Occured Saving/Editing Party<br />
                                            Dialog will Close automatically within 2 Seconds
                                        </span>
                                    </td>
                                </tr>
			                    </tbody>
		                    </table>
                        </asp:Panel>
                    </asp:View>
                </asp:MultiView> 
                </ContentTemplate>
                </asp:UpdatePanel>  
            </div>
            <div class="popup_Buttons" style="display:none;">
                <asp:Button ID="btnOkay" Text="Done" runat="server" OnClick="btnSave_Click" />
                <input id="btnCancel" value="Cancel" type="button" onclick="cancel();" />
            </div>
        </div>
    </form>
</body>
</html>