<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="csharpasst.Views.register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<form id="form1" runat="server">
<div>
<table>
<tr>
<td>
Name:
</td>
<td>
<asp:TextBox ID="txtName" runat="server"/>
<asp:RequiredFieldValidator ID="rfvname" ErrorMessage="Please enter name" ControlToValidate="txtName" runat="server" />
</td>
</tr>
<tr>
<tr>
<td>
Email:
</td>
<td>
<asp:TextBox ID="TxtEmail" runat="server"/>
<asp:RequiredFieldValidator ID="rfvemail" ErrorMessage="Please enter email" ControlToValidate="txtEmail" runat="server" />
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
<asp:Button ID="btnSubmit" runat="server" Text="Submit" onclick="Register" />
</td>
</tr>
</table>
</div>
</form>
</body>
</html>
