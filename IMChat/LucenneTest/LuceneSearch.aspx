<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LuceneSearch.aspx.cs" Inherits="LucenneTest.LuceneSerrch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="text" name="search" />
            <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" style="height: 21px" />
            <asp:Button ID="btnDel" runat="server" Text="删除" OnClick="btnDel_Click" />
        </div>
    </form>
</body>
</html>
