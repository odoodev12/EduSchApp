<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UnauthorisedAccess.aspx.cs" Inherits="SchoolApp.Web.UnauthorisedAccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CSSContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <center>
        <h1>Unauthorised Access</h1>
    <p>
        You have attempted to access a page that you are not authorised to view.
    </p>
    <p>
        If you have any questions, please contact the administrator.
    </p></center>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="jsContent" runat="server">
</asp:Content>
