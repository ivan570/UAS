<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test7.aspx.cs" Inherits="UAS_MSU.test7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
            ControlToValidate="txtLastName"
            ErrorMessage="Last name is a required field.">
        </asp:RequiredFieldValidator>
        <asp:Button runat="server" type="submit" class="btn btn-primary mb-2" Text="Add faculty"
            ID="bt_add" />

    </form>
</body>
</html>
