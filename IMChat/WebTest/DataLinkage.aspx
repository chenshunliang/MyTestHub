<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataLinkage.aspx.cs" Inherits="WebTest.DataLinkage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="Script/jquery-1.7.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="first" runat="server" OnTextChanged="first_TextChanged"></asp:DropDownList>
        <asp:DropDownList ID="second" runat="server"></asp:DropDownList>
        <asp:DropDownList ID="third" runat="server"></asp:DropDownList>
        <asp:DropDownList style="display:none;" ID="forth" runat="server"></asp:DropDownList>
    </div>
    </form>
</body>
</html>
