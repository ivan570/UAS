<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="csv.aspx.cs" Inherits="UAS_MSU.csv" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Read and Display Data From an Excel File (.xsl or .xlsx) in ASP.NET</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:TextBox ID="txtSearch" runat="server" OnTextChanged="Search" AutoPostBack="true"></asp:TextBox>
        <hr />
        <asp:GridView ID="gvCustomers" runat="server" 
            AutoGenerateColumns="false" 
            AllowPaging="true" 
            OnPageIndexChanging="OnPaging">
            <Columns>
                <asp:BoundField DataField="Department_id" HeaderText="Name" ItemStyle-Width="150" />
                <asp:BoundField DataField="Department_Name" HeaderText="City" ItemStyle-Width="150" />
                <asp:BoundField DataField="Hod_Name" HeaderText="Country" ItemStyle-Width="150" />
                <asp:BoundField DataField="Faculty_Id" HeaderText="Country" ItemStyle-Width="150" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
