<%@ Page Title="" Language="C#" MasterPageFile="~/SubAdmin/SubAdmin.Master" AutoEventWireup="true" CodeBehind="Course.aspx.cs" Inherits="UAS_MSU.SubAdmin.Course" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form class="form-inline">
        <br />
        <fieldset id="add_course" runat="server">
            <legend>Add Course</legend>
            <div class="form-group row">
                <asp:Label ID="label_course_name" class="col-sm-2 col-form-label" runat="server" Text="Course Name"></asp:Label>
                <asp:TextBox ID="textBox_course_name" class="form-control" runat="server"></asp:TextBox>
            </div>
            <asp:Button runat="server" type="submit" class="btn btn-primary mb-2" Text="Add Course" ID="bt_add" OnClick="bt_add_Click" />
            <asp:Button runat="server" type="reset" class="btn btn-primary mb-2" Text="Reset" ID="bt_reset" />
        </fieldset>
        <br />
        <fieldset id="view_course" runat="server">
            <legend>View Course</legend>

            <asp:GridView ID="courseGrid" runat="server" AutoGenerateColumns="False"
                OnRowCancelingEdit="courseGrid_RowCancelingEdit"
                OnRowEditing="courseGrid_RowEditing"
                OnRowUpdating="courseGrid_RowUpdating"
                OnRowDeleting="courseGrid_RowDeleting"
                AllowSorting="True"
                OnSorting="courseGrid_Sorting"
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

                    <asp:TemplateField HeaderText="Course Id" Visible="false" HeaderStyle-CssClass="header font" SortExpression="Course_Id">
                        <ItemTemplate>
                            <asp:Label ID="course_id"  runat="server" CssClass="one" Text='<%#Eval("Course_Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course Name" HeaderStyle-CssClass="header font" SortExpression="Course_Name">
                        <ItemTemplate>
                            <asp:Label ID="course_name" runat="server" CssClass="one" Text='<%#Eval("Course_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="course_name" runat="server" CssClass="one" Text='<%#Eval("Course_Name") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </form>
</asp:Content>
