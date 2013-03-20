<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" 
CodeBehind="WebForm1.aspx.cs" Inherits="CSMSys.Web.Pages.Administration.Application.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div>
    <asp:PlaceHolder id="LocalPlaceHolder" runat="server"></asp:PlaceHolder>

    </div>
</asp:Content>
