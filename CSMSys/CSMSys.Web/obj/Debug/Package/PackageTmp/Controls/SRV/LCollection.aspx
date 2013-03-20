<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LCollection.aspx.cs" Inherits="CSMSys.Web.Controls.SRV.LCollection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Enter" />
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Exit" />
    <title></title>
    <link href="../../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/Default/Styles/Default.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function getbacktostepone() {
            window.location = "LCollection.aspx";
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
            window.parent.document.getElementById('ButtonNewDone').click();
            //            else {
            //                window.parent.document.getElementById('ButtonNewDone').click();
            //                getbacktostepone();
            //            }
        }
        function cancel() {
            var UIMODE = $get('hdnWindowUIMODE').value;
            //            if (UIMODE == "EDIT")
            window.parent.document.getElementById('ButtonNewCancel').click();
            //            else
            //                window.parent.document.getElementById('ButtonNewCancel').click();
        }
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
                Loan Collection
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
                                        <asp:UpdatePanel ID="pnlupdate" runat="server" Width="100%">
                                            <ContentTemplate>   
                                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
			                                <tbody>
			                                    <tr>
				                                <td align="left" style="width:40%;">Case ID: </td>
				                                <td align="left" style="Width:60%;">
                                                    <asp:Label ID="lblcaseID" runat="server"></asp:Label> 
                                                    <asp:Label ID="lblloanid" runat="server" Visible="False"></asp:Label>
                                                </td>
			                                </tr>   
			                                <tr>
				                                <td align="left" style="width:40%;">Party Name: </td>
				                                <td align="left" style="Width:60%;">
                                                    <asp:TextBox ID="txtPartyName" runat="server" Enabled="False"></asp:TextBox>
                                                    <asp:Label ID="lblpartyID" runat="server" Visible="False"></asp:Label>
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left" style="width:40%;">Party Code:</td>
				                                <td align="left" style="Width:60%;">
                                                    <asp:TextBox ID="txtpartyCode" runat="server"  Enabled="False"></asp:TextBox>
                                                    &nbsp;&nbsp;
                                                    
                                                    <asp:RequiredFieldValidator ID="rfValidator3" runat="server" ControlToValidate="txtpartyCode"
                                                    CssClass="failureNotification" ErrorMessage="Serial Date is required." ToolTip="Serial Date is required."
                                                    ValidationGroup="SerialValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                                </td>
			                                </tr> 
                                            <tr>
                                                <td align="left" style="width: 40%;">
                                                    Serial No(Select):
                                                </td>
                                                <td align="left" style="width: 60%;">
                                                    <asp:ListBox ID="lstSerial" runat="server" Width="45%" SelectionMode="Multiple"
                                                     AutoPostBack="True" OnSelectedIndexChanged="lst_indchanged">
                                                    </asp:ListBox>
                                                    &nbsp;&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server"
                                                        ControlToValidate="lstSerial" CssClass="failureNotification" ErrorMessage="Please select a serial."
                                                        ToolTip="Please select a serial." ValidationGroup="SerialValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                             <tr>
				                                <td align="left" style="width:40%;">Bag Count: </td>
				                                <td align="left" style="Width:60%;">
                                                    <asp:Label ID="lblbagc" runat="server" ></asp:Label>  
                                                    <asp:Label ID="lblserialids" runat="server" Visible="False"></asp:Label>
                                                </td>
			                                </tr> 
                                             <tr>
				                                <td align="left" style="width:40%;">Loan Per Bag: </td>
				                                <td align="left" style="Width:60%;">
                                                    <asp:Label ID="lblloanperbag" runat="server"></asp:Label> টাকা
                                                </td>
			                                </tr>   
			                                <tr>
				                                <td align="left" style="width:40%;">Loan: </td>
				                                <td align="left" style="width:60%;">
				                                    <asp:Label ID="lblloandesc" runat="server"></asp:Label> টাকা-- 
                                                    <asp:Label ID="lblloan" runat="server"></asp:Label>টাকা
                                                    
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left" style="width:40%;">Interest: </td>
				                                <td align="left" style="width:60%;">
				                                    <asp:Label ID="lbldays" runat="server"></asp:Label> Days-- 
				                                    <asp:Label ID="lblinterest" runat="server"></asp:Label> টাকা
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left" style="width:40%;">Total Amount:</td>
				                                <td align="left" style="Width:60%;">
                                                    <asp:Label ID="lbltotalamount" runat="server"></asp:Label> টাকা
                                                </td>
			                                </tr>
			                                <tr>
				                                <td align="left" style="width:40%;">Remarks: </td>
				                                <td align="left" style="Width:60%;">
                                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="258px"></asp:TextBox>
                                                </td>
			                                </tr>
			                                </tbody>
		                                </table> 
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td align="left" valign="top" style="width: 60%;">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                            <tbody>
                                                <tr>
                                                    <td align="left">
                                                        Search Party by Name Code / CaseID &nbsp;
                                                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                                        <asp:ImageButton ID="imgSearch" runat="server" CommandName="Search" ImageUrl="~/App_Themes/Default/Images/gridview/Search.png"
                                                            ToolTip="Search" Width="16px" Height="16px" OnClick="imgSearch_Click"  />  &nbsp;
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
                                                                        <asp:GridView ID="grvParty" DataKeyNames="LoanID" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                            CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvParty_PageIndexChanging"
                                                                            ShowHeaderWhenEmpty="true" OnRowDataBound="grvParty_RowDataBound" OnRowCommand="grvParty_RowCommand"
                                                                            EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True"
                                                                            PageSize="10" DataSourceID="dsParty">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Loan ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSl" Text='<%# Eval("LoanID") %>' runat="server" HorizontalAlign="Left" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="PartyID" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPartyID" Text='<%# Eval("PartyID") %>' runat="server" HorizontalAlign="Left" />
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
                                                                                        <asp:Label ID="lblPartyName" Text='<%# Eval("PartyName") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Case ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblcaseID" Text='<%# Eval("caseID") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Created" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblcreated" Text='<%# Eval("cdate") %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgEdit" runat="server" CommandName="Select" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png"
                                                                                            ToolTip="Edit" />
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
                                                                            SelectCommand="select ld.LoanID,ld.PartyID,ld.serialIDs,ld.caseID,CONVERT(varchar(10),ld.createddate,103) as cdate,ld.LoanAmount,ip.PartyName,ip.PartyCode
                                                                    from SRVLoanDisburse as ld,INVParty as ip
                                                                    where ld.PartyID=ip.PartyID" 
                                                                    FilterExpression="PartyName LIKE '%{0}%' OR PartyCode LIKE '%{1}%' or caseID = '{2}'">
                                                                            <FilterParameters>
                                                                                <asp:QueryStringParameter Name="PartyName" QueryStringField="ip.PartyName" />
                                                                                <asp:QueryStringParameter Name="PartyCode" QueryStringField="ip.PartyCode" />
                                                                                <asp:QueryStringParameter Name="caseID" QueryStringField="ld.caseID" />
                                                                                <asp:ControlParameter Name="txtSearch" ControlID="txtSearch" PropertyName="Text" />
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
                    <div class="feature-box-actionBar">
                        <span class="failureNotification">
                            <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
                        </span>
                        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="SerialValidationGroup"
                            OnClick="btnsave_click" />
                    </div>
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
            <asp:Button ID="btnOkay" Text="Done" runat="server" />
            <input id="btnCancel" value="Cancel" type="button" onclick="cancel();" />
        </div>
    </div>
    </form>
</body>
</html>
