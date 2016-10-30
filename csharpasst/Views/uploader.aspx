<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uploader.aspx.cs" Inherits="csharpasst.Models.uploader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

<form id="Form1" method="post" enctype="multipart/form-data" runat="server">
<input type="file" id="File1" name="File1" runat="server" />
<br/>
<input type="submit" id="Submit1" value="Upload" runat="server" />
</form>
		</body>
</html>
