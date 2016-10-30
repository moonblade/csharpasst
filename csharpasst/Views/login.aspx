<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="csharpasst.Views.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<form id="form1" runat="server">
<div>
<table>
<tr>
<td>
Email:
</td>
<td>
<asp:TextBox ID="txtEmail" runat="server"/>
<asp:RequiredFieldValidator ID="rfvUser" ErrorMessage="Please enter email" ControlToValidate="txtEmail" runat="server" />
</td>
</tr>
<tr>
<td>
Password:
</td>
<td>
<asp:TextBox ID="txtPWD" runat="server" TextMode="Password"/>
<asp:RequiredFieldValidator ID="rfvPWD" runat="server" ControlToValidate="txtPWD" ErrorMessage="Please enter Password"/>
</td>
</tr>
<tr>
<td>
</td>
<td>
<asp:Button ID="btnSubmit" runat="server" Text="Login" onclick="btnSubmit_Click" />
</td>
</tr>
    <tr><td><asp:Button ID="registerButton" runat="server" Text="Register" onclick="gotoRegister" />
</td></tr>
</table>
</div>
</form></body>
</html>
