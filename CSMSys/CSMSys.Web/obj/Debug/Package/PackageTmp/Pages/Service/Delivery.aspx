<%@ Page Language="C#" Title="CSMSys :: Delivery" AutoEventWireup="true" MasterPageFile="~/Default.Master"
    CodeBehind="Delivery.aspx.cs" Inherits="CSMSys.Web.Pages.Service.Delivery" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../App_Themes/TableSorter/Blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/TableSorter/Green/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="title">
        <table width="100%" border="0" cellpadding="2" cellspacing="4">
            <tbody>
                <tr>
                    <td align="left" style="width: 47%;">
                        <h2>
                            Delivery</h2>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" style="width: 40%;">
                        <asp:UpdatePanel id="updatepnl" runat="server">
                            <ContentTemplate>
                        <asp:MultiView ID="MultiViewSL" runat="server" >
                            <asp:View ID="ViewInput" runat="server">
                                <table width="100%" border="0" cellpadding="0" cellspacing="4">
                                    <tbody>
                                        <tr>
                                            <td align="left" style="width: 30%;">
                                                Party Name:
                                            </td>
                                            <td align="left" style="width: 70%;">
                                                <asp:TextBox ID="txtpartyname" runat="server" Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 30%;">
                                                Serial No:
                                            </td>
                                            <td align="left" style="width: 70%;">
                                                <asp:TextBox ID="txtSerialNo" runat="server" Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 30%;">
                                                Bags :
                                            </td>
                                            <td align="left" style="width: 70%;">
                                                <asp:TextBox ID="txthidbag" runat="server" Visible="False" Text="0"></asp:TextBox>                  
                                                <asp:TextBox ID="txtbags" runat="server" OnTextChanged="txtchanged" AutoPostBack="True"></asp:TextBox>
                                                &nbsp;&nbsp;
                                                <asp:RequiredFieldValidator ID="rfValidator3" runat="server" ControlToValidate="txtbags"
                                                    CssClass="failureNotification" ErrorMessage="Bags Count is required." ToolTip="Bags Count is required."
                                                    ValidationGroup="SerialValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                Carry Cost:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtcarrycost" runat="server" Enabled="False" Text="0"></asp:TextBox>
                                                <asp:TextBox ID="txthidcarrycost" runat="server" Visible="False" Text="0"></asp:TextBox>
                                                    
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 30%;">
                                                Empty Bag Loan:
                                            </td>
                                            <td align="left" style="width: 70%;">
                                                <asp:TextBox ID="txtemptybag" runat="server" Enabled="False"  Text="0" ></asp:TextBox>
                                                <asp:TextBox ID="txthidemptybag" runat="server" Visible="False" Text="0"></asp:TextBox>
                                            </td>
                                        </tr>    
                                         <tr>
                                            <td align="left" style="width: 30%;">
                                               Loan Disbursed:
                                            </td>
                                            <td align="left" style="width: 70%;">
                                                <asp:TextBox ID="txtloandisbursed" runat="server" Enabled="False"  Text="0" ></asp:TextBox>
                                                <asp:TextBox ID="txthidloandisbursed" runat="server" Visible="False" Text="0"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 30%;">
                                                Keeping Charge:
                                            </td>
                                            <td align="left" style="width: 70%;">
                                                <asp:TextBox ID="txtkeeping" runat="server" Enabled="False"></asp:TextBox>
                                                <asp:TextBox ID="txthidkeeping" runat="server" Visible="False"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtkeeping"
                                                    CssClass="failureNotification" ErrorMessage="Insert Keeping Charge." ToolTip="Keeping charge is required."
                                                    ValidationGroup="SerialValidationGroup"><img src="../../../App_Themes/Default/Images/Left_Arrow.png" 
                                                    alt="*" /></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 30%;">
                                                Total Amount:
                                            </td>
                                            <td align="left" style="width: 70%;">
                                                <asp:Label ID="lbltotalamount" runat="server" Enabled="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 30%;">
                                                Remarks :
                                            </td>
                                            <td align="left" style="width: 70%;">
                                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="258px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:View>
                            <asp:View ID="ViewSuccess" runat="server">
                                <asp:Panel ID="pnlSuccess" runat="server" Width="100%">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tbody>
                                            <tr>
                                                <td align="center">
                                                    <span class="succesNotification">Stock Location Saved Successfully.
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
                                                    <span class="failureNotification">Error Occured Saving Stock Loading<br />
                                                        Dialog will Close automatically within 2 Seconds </span>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </asp:View>
                        </asp:MultiView>  
                               </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" valign="top" style="width: 60%;">
                        <table width="100%" border="0" cellpadding="0" cellspacing="4">
                            <tbody>
                                <tr>
                                    <td align="left">
                                        Search party by Name or Code/SerialNo &nbsp;
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
                                                        <asp:GridView ID="grvParty" DataKeyNames="SlNo" runat="server" Width="100%" AutoGenerateColumns="False"
                                                            CellPadding="4" HorizontalAlign="Left" OnPageIndexChanging="grvParty_PageIndexChanging"  AllowSorting="True"
                                                            ShowHeaderWhenEmpty="true" OnRowDataBound="grvParty_RowDataBound" OnRowCommand="grvParty_RowCommand"
                                                            EmptyDataText="No Records Found" CssClass="tablesorterBlue" AllowPaging="True"
                                                            PageSize="8" DataSourceID="dsParty">
                                                            
                                                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"  
                                                                    ItemStyle-Width="2%" SortExpression="slno" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSl" Text='<%# Eval("SlNo") %>' runat="server" HorizontalAlign="Left"  />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>  
                                                                <asp:TemplateField HeaderText="Serial No" SortExpression="SerialNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSerialNo" Text='<%# HighlightText( Eval("SerialNo").ToString()) %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bags On Stock" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblbags" Text='<%# Eval("BagNo") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PartyID" Visible="false" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPartyID" Text='<%# Eval("PartyID") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Code" SortExpression="PartyCode" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPartyCode" Text='<%# HighlightText(Eval("PartyCode").ToString()) %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name" SortExpression="PartyName" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPartyName" Text='<%# HighlightText(Eval("PartyName").ToString()) %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                <asp:TemplateField HeaderText="Carrying Cost" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcarryingcost" Text='<%# Eval("CarryingLoan") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Empty bag Loan"  HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblemptybagloan" Text='<%# Eval("BagLoan") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Keeping Charge"  HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblkeepcharge"  runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Created" SortExpression="cdates" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCreatedDate" Text='<%# Eval("cdates") %>' runat="server" HorizontalAlign="Left" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgselect" runat="server" CommandName="Select" ImageUrl="~/App_Themes/Default/Images/gridview/Edit.png"
                                                                            ToolTip="Select" />
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
                                                            SelectCommand="SELECT ss.SerialID As SlNo,ss.SerialNo,itemdet.BagNo,CONVERT(VARCHAR(10), ss.CreatedDate, 103) AS cdates, 
                                                            ip.PartyID,  ip.PartyCode, ip.PartyName,  ip.ContactNo,srvr.CarryingLoan,srvr.BagLoan,itemdet.TotalFair,itemdet.Advance
                                                            FROM INVStockSerial as ss
                                                            INNER JOIN INVParty AS ip ON ss.PartyCode = ip.PartyCode
                                                            left JOIN SRVRegistration as srvr on srvr.SerialID=ss.SerialID
                                                            INNER JOIN INVItemDetail as itemdet on itemdet.RegistrationID=srvr.RegistrationID"
                                                            FilterExpression="PartyName LIKE '%{0}%' OR PartyCode LIKE '{1}' OR Serial = {2}">
                                                            <FilterParameters>
                                                                <asp:QueryStringParameter Name="PartyName" QueryStringField="PartyName" />
                                                                <asp:QueryStringParameter Name="PartyCode" QueryStringField="PartyCode" />
                                                                <asp:QueryStringParameter Name="Serial" QueryStringField="Serial" />
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
        <div class="feature-box-actionBar">
            <span class="failureNotification">
                <asp:Literal ID="lblFailure" runat="server"></asp:Literal>
            </span>
            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="SerialValidationGroup" />
        </div>
    </div>
</asp:Content>
