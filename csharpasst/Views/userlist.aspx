<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userlist.aspx.cs" Inherits="csharpasst.Views.userlist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:LinkButton ID="Filename" onclick="download" Text= 'Download' visible="true" runat="server"/>

    </div>
        <br />
        <h4>Allowed List</h4>
        <div>
            <asp:Repeater id="allowedList" runat="server" OnItemCommand="denyUser" >
            <ItemTemplate>
            <table>
                <tr>
                    <td><asp:LinkButton ID="allowedName" Text= '<%# Container.DataItem.ToString() %>' visible="true" runat="server" CommandName='<%# Container.DataItem.ToString() %>'/> </td>
                </tr>
            </table>
          </ItemTemplate>
    </asp:Repeater>
        </div>
        <h4>Denied List</h4>
        <div>
            <asp:Repeater id="deniedList" runat="server" OnItemCommand="allowUser" >
            <ItemTemplate>
            <table>
                <tr>
                    <td><asp:LinkButton ID="deniedName" Text= '<%# Container.DataItem.ToString() %>' visible="true" runat="server" CommandName='<%# Container.DataItem.ToString() %>'/> </td>
                </tr>
            </table>
          </ItemTemplate>
    </asp:Repeater>
        </div>
    </form>
</body>
</html>
