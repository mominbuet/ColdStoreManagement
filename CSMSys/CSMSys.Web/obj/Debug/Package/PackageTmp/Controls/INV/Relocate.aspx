<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Relocate.aspx.cs" Inherits="CSMSys.Web.Controls.INV.Relocate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Enter" />
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Exit" />
    <title></title>
    <link href="../../../App_Themes/Default/Styles/Default.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function getbacktostepone() {
            window.location = "Relocate.aspx";
        }
        function onSuccess() {
            setTimeout(okay, 2000);
        }
        function onError() {
            setTimeout(cancel, 2000);
        }
        function okay() {
            var UIMODE = $get('hdnWindowUIMODE').value;
//            if (UIMODE == "EDIT")
            window.parent.document.getElementById('ButtonEditDone').click();
//            else {
//                window.parent.document.getElementById('ButtonNewDone').click();
//                getbacktostepone();
//            }
        }
        function cancel() {
            var UIMODE = $get('hdnWindowUIMODE').value;
//            if (UIMODE == "EDIT")
                window.parent.document.getElementById('ButtonEditCancel').click();
//            else
//                window.parent.document.getElementById('ButtonNewCancel').click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" value="" runat="server" id="hdnWindowUIMODE" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="popup_Container">
        <div class="popup_Titlebar" id="PopupHeader">
            <div class="TitlebarLeft">
                Stock Relocate
            </div>
            <div class="TitlebarRight" onclick="cancel();">
            </div>
        </div>
        <div class="popup_Body">
            <asp:MultiView ID="MultiViewParty" runat="server">
                <asp:View ID="ViewInput" runat="server">
                    <asp:Panel ID="pnlNew" runat="server" Width="100%">
                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                            <tbody>
                                <tr>
                                    <td align="left" valign="top" style="width: 50%;">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                            <tbody>
                                                <tr>
                                                    <td align="left" colspan="2">
                                                        <strong>Stock Relocation </strong>
                                                    </td>
                                                </tr>
                                                  <tr>
                                                    <td align="left">
                                                        Serial No :
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblSerialNo" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                  <tr>
                                                    <td align="left">
                                                        Party Code :
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblparty" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Chamber No :
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtchamber" runat="server" Text=""></asp:TextBox>
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Floor No :
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtfloor" runat="server" Width="258px"></asp:TextBox>
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Pocket No :
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtpocket" runat="server" Width="258px"></asp:TextBox>
                                                       
                                                    </td>
                                                </tr>
                                               <%-- <tr>
                                                    <td align="left">
                                                        Line No :
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtlineno" runat="server" Width="258px"></asp:TextBox>
                                                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtlineno"
                                                            CssClass="failureNotification" ErrorMessage="Line Number is required." ToolTip="Line Number is required."
                                                            ValidationGroup="PartyValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>--%>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td align="left">
                                                        Remaks :
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtremarks" runat="server" Width="258px" TextMode="MultiLine"></asp:TextBox>
                                                       
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td align="left" valign="top" style="width: 50%;">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                            <%--another table here --%>
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
                        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClick="btnSave_Click" />
                    </div>
                </asp:View>
                <asp:View ID="ViewSuccess" runat="server">
                    <asp:Panel ID="pnlSuccess" runat="server" Width="100%">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td align="center">
                                        <span class="succesNotification">Stock Location Edited Successfully.
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
                                        <span class="failureNotification">Error Occured in Stock Relocating<br />
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
