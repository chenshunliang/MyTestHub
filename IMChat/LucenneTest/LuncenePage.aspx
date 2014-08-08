<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LuncenePage.aspx.cs" Inherits="LucenneTest.LuncenePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="编号"></asp:Label>
            <asp:TextBox ID="txtIndex" runat="server"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="标题"></asp:Label>
            <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
            <asp:Label ID="Label3" runat="server" Text="正文"></asp:Label>
            <asp:TextBox ID="txtBody" runat="server"></asp:TextBox>
            <asp:Button ID="btnAdd" runat="server" Text="添加索引" OnClick="btnAdd_Click1" />
        </div>
        <div>
            <asp:Button ID="btnCombine" runat="server" Text="整合索引" OnClick="btnCombine_Click" />
        </div>
        <div>
            <asp:Button ID="btnAnalys" runat="server" Text="解析" OnClick="btnAnalys_Click" />
        </div>
    </form>
</body>
</html>
