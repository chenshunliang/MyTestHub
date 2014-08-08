<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Prreturn.aspx.cs" Inherits="WebTest.Prreturn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="Script/jquery-1.7.1.js"></script>
</head>
<body>
    <form id="form1" runat="server" action="CKeditor.aspx">
        <div>
            <input type="submit" value="跳转" id="sub" onclick="return prev();" />
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    function prev() {
        $("#form1").submit();
        return false;
    }
</script>
