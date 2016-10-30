<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uploader.aspx.cs" Inherits="csharpasst.Views.uploader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<form id="Form1" method="post" enctype="multipart/form-data" runat="server">
<input type="file" id="File1" name="File1" runat="server" />
<br/>
<input type="submit" id="Submit1" value="upload" onclick="uploadButtonClicked" runat="server" />
    <br />
    <div>
    <h1>Downloads</h1>
    <asp:Repeater id="DataGrid" runat="server" OnItemCommand="Repeater_btn" >

        <ItemTemplate>
            <table>
                <tr><td><asp:LinkButton  ID="Filename" Text= '<%# Container.DataItem.ToString() %>' visible="true" runat="server" CommandName='<%# Container.DataItem.ToString() %>'/> </td></tr>
            </table>
          </ItemTemplate>

    </asp:Repeater>
    </div>
</form>

		</body>
</html>
