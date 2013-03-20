<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpazilaPS.aspx.cs" Inherits="CSMSys.Web.Controls.Administration.Application.UpazilaPS" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Enter" />
	<meta content="blendTrans(Duration=0.5)" http-equiv="Page-Exit" />
    <title></title>
    <link href="../../../App_Themes/Default/Styles/Site.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function getbacktostepone() {
            window.location = "UpazilaPS.aspx";
        }
        function onSuccess() {
            setTimeout(okay, 2000);
        }
        function onError() {
            setTimeout(getbacktostepone, 2000);
        }
        function okay() {
            var UIMODE = $get('hdnWindowUIMODE').value;
            if ((UIMODE == "NEW") || (UIMODE == "EDIT"))
                window.parent.document.getElementById('ButtonEditDone').click();
            else {
                window.parent.document.getElementById('btnOkay').click();
                getbacktostepone();
            }
        }
        function cancel() {
            var UIMODE = $get('hdnWindowUIMODE').value;
            if ((UIMODE == "NEW") || (UIMODE == "EDIT"))
                window.parent.document.getElementById('ButtonEditCancel').click();
            else
                window.parent.document.getElementById('btnCancel').click();
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
                    Area/Post Office setup
                </div>
                <div class="TitlebarRight" onclick="cancel();"></div>
            </div>
            <div class="popup_Body">
                <asp:MultiView ID="MultiViewUpazilaPS" runat="server">
                    <asp:View ID="ViewInput" runat="server">
                        <div class="feature-box-full">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                <asp:Panel ID="pnlNew" runat="server" width="100%">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="4">
			                                <tbody>
			                                <tr>
				                                <td align="left">Division : </td>
				                                <td align="left">
                                                    <asp:DropDownList ID="ddlDivision" runat="server" Width="262px" 
                                                        onselectedindexchanged="ddlDivision_SelectedIndexChanged" 
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
				                                <td align="left">&nbsp;</td>
			                                </tr>
			                                <tr>
				                                <td align="left">District : </td>
				                                <td align="left">
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" Width="262px" >
                                                    </asp:DropDownList>
                                                </td>
				                                <td align="left">&nbsp;</td>
			                                </tr>
			                                <tr>
				                                <td align="left" style="width:30%;">Code : </td>
				                                <td align="left" style="Width:70%;">
                                                    <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                                                    &nbsp;&nbsp;
                                                    <asp:RequiredFieldValidator ID="rfValidator1" runat="server" ControlToValidate="txtCode"
                                                    CssClass="failureNotification" ErrorMessage="Code is required." ToolTip="Code is required."
                                                    ValidationGroup="NewDistributorValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left">Name : </td>
				                                <td align="left"><asp:TextBox ID="txtName" runat="server" Width="258px"></asp:TextBox></td>
				                                <td align="left">
                                                    <asp:RequiredFieldValidator ID="rfValidator2" runat="server" ControlToValidate="txtName"
                                                    CssClass="failureNotification" ErrorMessage="Name is required." ToolTip="Name is required."
                                                    ValidationGroup="NewDistributorValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left">Description : </td>
				                                <td align="left"><asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="258px"></asp:TextBox></td>
				                                <td align="left">&nbsp;</td>
			                                </tr>
			                                </tbody>
		                                </table>
                                </asp:Panel>

                                <div class="feature-box-actionBar">
                                    <div class="msgBox">
                                        <span class="failureNotification">
                                            <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
                                        </span>
                                    </div>
                                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" 
                                        ValidationGroup="NewDistributorValidationGroup" OnClick="btnSave_Click" />
                                </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </asp:View>
                    <asp:View ID="ViewSuccess" runat="server">
                        <asp:Panel ID="pnlSuccess" runat="server" width="100%">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
			                    <tbody>
                                <tr>
                                    <td align="center">
                                        <span class="succesNotification">
                                            Distributor Edited Successfully. <br />
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
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
			                    <tbody>
                                <tr>
                                    <td align="center">
                                        <span class="failureNotification">
                                            Error Occured Editing Distributor<br />
                                            Please wait<br />
                                            Redirecting to Edit Window
                                        </span>
                                    </td>
                                </tr>
			                    </tbody>
		                    </table>
                        </asp:Panel>
                    </asp:View>
                </asp:MultiView>
            </div>
            <div class="popup_Buttons" style="display:none;">
                <asp:Button ID="btnOkay" Text="Done" runat="server" OnClick="btnSave_Click" />
                <input id="btnCancel" value="Cancel" type="button" onclick="cancel();" />
            </div>
        </div>
    </form>
</body>
</html>