<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Teacher.aspx.cs" Inherits="UAS_MSU.Admin.Teacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form class="form-inline">
        <br />
        <fieldset>
            <legend>Add Teacher</legend>
            <div class="form-group row">
                <asp:Label ID="label_teacher_fname" class="col-sm-2 col-form-label" runat="server" Text="Teacher First Name"></asp:Label>
                <asp:TextBox ID="textBox_teacher_fname" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group row">
                <asp:Label ID="label_teacher_lname" class="col-sm-2 col-form-label" runat="server" Text="Teacher Last Name"></asp:Label>
                <asp:TextBox ID="textBox_teacher_lname" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group row">
                <asp:Label ID="label_email" class="col-sm-2 col-form-label" runat="server" Text="Teacher Email"></asp:Label>
                <asp:TextBox ID="textBox_email" TextMode="Email" class="form-control" runat="server"></asp:TextBox>
            </div>
            <asp:Button runat="server" type="submit" class="btn btn-primary mb-2" Text="Add teacher" ID="bt_add" OnClick="bt_add_Click" />
            <asp:Button runat="server" type="reset" class="btn btn-primary mb-2" Text="Reset" ID="bt_reset" />
        </fieldset>
    </form>
    <br />
    <fieldset>
        <legend>Add Teacher using Excel </legend>
        <a target="_blank" href="https://drive.google.com/file/d/1nL5usIaxVDO9tvc54jcUaGFNca62SHDi/view?usp=sharing">Download</a>
        <asp:FileUpload runat="server" class="form-control " ID="fileupload" />
        <asp:Button runat="server" class="btn btn-primary" Style="margin-top: 0.41%;" ID="btn_upload" Text="Upload" OnClick="btnUpload_Click" />
    </fieldset>
    <br />
    <fieldset>
        <legend>View Teacher</legend>
        <asp:GridView ID="teacherGrid" runat="server" AutoGenerateColumns="False"
            OnRowCancelingEdit="teacherGrid_RowCancelingEdit"
            OnRowEditing="teacherGrid_RowEditing"
            OnRowUpdating="teacherGrid_RowUpdating"
            OnRowDeleting="teacherGrid_RowDeleting"
            AllowSorting="True"
            OnSorting="teacherGrid_Sorting"
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

                <asp:TemplateField HeaderText="Teacher Id" Visible="false" HeaderStyle-CssClass="header font" SortExpression="Teacher_Id">
                    <ItemTemplate>
                        <asp:Label ID="tid" runat="server" CssClass="one" Text='<%#Bind("Teacher_Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Teacher Email" HeaderStyle-CssClass="header font" SortExpression="Email">
                    <ItemTemplate>
                        <asp:Label ID="email" runat="server" CssClass="one" Text='<%#Bind("Email") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Teacher First Name" HeaderStyle-CssClass="header font" SortExpression="fName">
                    <ItemTemplate>
                        <asp:Label ID="label_fname" runat="server" CssClass="one" Text='<%#Bind("fName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="textBox_fname" runat="server" CssClass="one" Text='<%#Bind("fName") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Teacher Last Name" HeaderStyle-CssClass="header font" SortExpression="lName">
                    <ItemTemplate>
                        <asp:Label ID="label_lname" runat="server" CssClass="one" Text='<%#Bind("lName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="textBox_lname" runat="server" CssClass="one" Text='<%#Bind("lName") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </fieldset>
</asp:Content>
