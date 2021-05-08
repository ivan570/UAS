<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Faculty.aspx.cs" Inherits="UAS_MSU.Admin.Faculty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <form class="form-inline">
        <br />
        <fieldset>
            <legend>Add Faculty</legend>
            <div class="form-group row">
                <asp:Label ID="label_faculty_name" class="col-sm-2 col-form-label" runat="server" Text="Faculty Name"></asp:Label>
                <asp:TextBox ID="textBox_faculty_name" class="form-control" runat="server"></asp:TextBox>
            </div>
            <asp:Button runat="server" type="submit" class="btn btn-primary mb-2" Text="Add faculty" ID="bt_add" OnClick="bt_add_Click" />
            <asp:Button runat="server" type="reset" class="btn btn-primary mb-2" Text="Reset" ID="bt_reset" />
        </fieldset>
        <br />
        <br />
    </form>
    <fieldset>
        <legend>View Faculty</legend>
        <div>
            <asp:GridView ID="facultyGrid" runat="server"
                AllowSorting="True"
                AutoGenerateColumns="False"
                OnRowCancelingEdit="facultyGrid_RowCancelingEdit"
                OnRowEditing="facultyGrid_RowEditing"
                OnRowUpdating="facultyGrid_RowUpdating"
                OnRowDeleting="facultyGrid_RowDeleting"
                OnSorting="facultyGrid_Sorting"
                CssClass="footable"
                EmptyDataText="NO DATA FOUND">
                <Columns>
                    <asp:TemplateField HeaderStyle-Width="11%">
                        <ItemTemplate>
                            <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" CssClass="one" />
                            <asp:Button ID="btn_Delete" runat="server" Text="Delete" CommandName="Delete" CssClass="one margin-top" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update" CssClass="one" />
                            <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="one margin-top" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Faculty Id" Visible="false" HeaderStyle-CssClass="header font" SortExpression="Faculty_Id">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Name" runat="server" CssClass="one" Text='<%# Bind("Faculty_Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Faculty Name" HeaderStyle-CssClass="header font" SortExpression="Faculty_Name">
                        <ItemTemplate>
                            <asp:Label ID="lbl_City" runat="server" CssClass="one" Text='<%# Bind("Faculty_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_City" runat="server" CssClass="one" Text='<%# Bind("Faculty_Name") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </fieldset>
</asp:Content>
