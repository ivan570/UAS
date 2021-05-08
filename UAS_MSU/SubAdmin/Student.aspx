<%@ Page Title="" Language="C#" MasterPageFile="~/SubAdmin/SubAdmin.Master" AutoEventWireup="true" CodeBehind="Student.aspx.cs" Inherits="UAS_MSU.SubAdmin.Student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form class="form-inline">
        <br />
        <fieldset>
            <legend>Select Course</legend>
            <asp:DropDownList runat="server" AutoPostBack="true" ID="selectDropDownListCourse"
                CssClass="form-control" Width="100%"
                OnSelectedIndexChanged="selectDropDownListCourse_SelectedIndexChanged">
            </asp:DropDownList>
        </fieldset>
        <br />
        <fieldset>
            <legend>Select Class</legend>
            <asp:DropDownList runat="server" AutoPostBack="true" ID="selectDropDownListClass"
                CssClass="form-control" Width="100%"
                OnSelectedIndexChanged="selectDropDownListClass_SelectedIndexChanged">
            </asp:DropDownList>
        </fieldset>
        <br />
        <fieldset>
            <legend>Add Student</legend>
            <div class="form-group row">
                <asp:Label ID="label_name" class="col-sm-2 col-form-label" runat="server" Text="Student Name"></asp:Label>
                <asp:TextBox ID="textBox_name" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group row">
                <asp:Label ID="label_Prn" class="col-sm-2 col-form-label" runat="server" Text="Student PRN"></asp:Label>
                <asp:TextBox ID="textBox_Prn" class="form-control" TextMode="Number" runat="server"></asp:TextBox>
            </div>
            <div class="form-group row">
                <asp:Label ID="label_email" class="col-sm-2 col-form-label" runat="server" Text="Student Email"></asp:Label>
                <asp:TextBox ID="textBox_email" TextMode="Email" class="form-control" runat="server"></asp:TextBox>
            </div>
            <asp:Button runat="server" type="submit" class="btn btn-primary mb-2" Text="Add student" ID="bt_add" OnClick="bt_add_Click" />
            <asp:Button runat="server" type="reset" class="btn btn-primary mb-2" Text="Reset" ID="bt_reset" />
        </fieldset>
        <br />
        <fieldset>
            <legend>Add Student using Excel </legend>
            <a target="_blank" href="https://drive.google.com/file/d/1pRoPE4yABeQVIH6BYFXUlBiNFoWW78uN/view?usp=sharing">Download</a>
            <asp:FileUpload runat="server" class="form-control " ID="fileupload" />
            <asp:Button runat="server" class="btn btn-primary" Style="margin-top: 0.41%;"
                ID="btn_upload" Text="Upload" OnClick="btnUpload_Click" />
        </fieldset>
        <br />
    </form>

    <fieldset>
        <legend>View Student</legend>
        <table class="table">
            <asp:GridView ID="studentGrid" runat="server" AutoGenerateColumns="False"
                OnRowCancelingEdit="studentGrid_RowCancelingEdit"
                OnRowEditing="studentGrid_RowEditing"
                OnRowUpdating="studentGrid_RowUpdating"
                OnRowDeleting="studentGrid_RowDeleting"
                AllowSorting="True"
                OnSorting="studentGrid_Sorting"
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

                    <asp:TemplateField HeaderText="Student Id" Visible="false" HeaderStyle-CssClass="header font" SortExpression="Student_Id">
                        <ItemTemplate>
                            <asp:Label ID="sid" runat="server"  CssClass="one" Text='<%#Bind("Student_Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Name" HeaderStyle-CssClass="header font" SortExpression="Student_Name">
                        <ItemTemplate>
                            <asp:Label ID="label_name" runat="server" CssClass="one" Text='<%#Bind("Student_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="textBox_name" runat="server" CssClass="one" Text='<%#Bind("Student_Name") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student PRN" HeaderStyle-CssClass="header font" SortExpression="Prn" >
                        <ItemTemplate>
                            <asp:Label ID="label_prn" runat="server" CssClass="one" Text='<%#Bind("Prn") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="textBox_prn" runat="server" CssClass="one" Text='<%#Bind("Prn") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Email" HeaderStyle-CssClass="header font" SortExpression="Email" >
                        <ItemTemplate>
                            <asp:Label ID="email" runat="server" CssClass="one" Text='<%#Bind("Email") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </table>
    </fieldset>
</asp:Content>
