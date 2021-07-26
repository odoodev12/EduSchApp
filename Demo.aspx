<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="SchoolApp.Web.Demo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server"  action="http://liveview.gsdemosystem.com/MobileAPI/ImageUpload">
    <div>
   <%-- <asp:FileUpload ID="fup" runat="server" />
        <asp:Button ID="btnsend" runat="server" Text="send" />--%>

        <input type="file" runat="server" name="fup1" id="fup" />
        <%--<input type="file" runat="server" name="fup2" id="File1" />
        <input type="file" runat="server" name="fup3" id="File2" />--%>
        <input type="submit" name="send" id="send" runat="server" />
        
    </div>
    </form>
</body>
</html>
