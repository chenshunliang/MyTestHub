<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CKeditor.aspx.cs" Inherits="WebTest.CKeditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="Script/CKeditor/ckeditor.js"></script>
    <script type="text/javascript" src="Script/CKfinder/ckfinder.js"></script>
    <script type="text/javascript" src="Script/jquery-1.7.1.js"></script>
    <!--必写的东西-->
    <%--<style type="text/css">
        .ckeditor
        {
        }
    </style>--%>
</head>
<body>
    <div id="content" style="width: 700px; height: 500px; background-color: grey;">
    </div>
    <form id="form1" runat="server">
        <textarea id="post_content" name="post_content"></textarea>


        <asp:Button ID="btn" runat="server" Text="提交" OnClientClick="return Show();" OnClick="btn_Click" />
    </form>
</body>
</html>
<script type="text/javascript">
    function Show() {
        var content = CKEDITOR.instances.post_content.getData();
        $('#content').prepend(content);
        return false;
    }

    var editor = CKEDITOR.replace('post_content');// 创建编辑器
    CKFinder.setupCKEditor(editor, '/PlugIns/ckfinder/');// 为编辑器绑定"上传控件"
</script>
