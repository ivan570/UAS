<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="UAS_MSU.Admin.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" AllowSorting="True"
							AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
							BorderStyle="Solid" BorderWidth="1px" CellPadding="3" DataKeyNames="Faculty_Id"
							ForeColor="Black" GridLines="Vertical" OnSorting="GridView1_Sorting">
							<AlternatingRowStyle BackColor="#CCCCCC" />
							<Columns>
								<asp:TemplateField HeaderText="Id" SortExpression="Faculty_Id">
									<ItemTemplate>
										<asp:Label ID="Label1" runat="server" Text='<%# Bind("Faculty_Id") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="First Name" SortExpression="Faculty_name">
									<EditItemTemplate>
										<asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Faculty_name") %>'></asp:TextBox>
									</EditItemTemplate>
									<ItemTemplate>
										<asp:Label ID="Label2" runat="server" Text='<%# Bind("Faculty_name") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
								
								
							</Columns>
							<FooterStyle BackColor="#CCCCCC" />
							<HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
							<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
							<SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
							<SortedAscendingCellStyle BackColor="#FFf" />
							<SortedAscendingHeaderStyle BackColor="#000" />
							<SortedDescendingCellStyle BackColor="#999" />
							<SortedDescendingHeaderStyle BackColor="#383" />
						</asp:GridView>
        </div>
    </form>
</body>
</html>
