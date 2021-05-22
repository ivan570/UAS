<%@ Page Title="" Language="C#" MasterPageFile="~/SubAdmin/SubAdmin.Master" AutoEventWireup="true" CodeBehind="Class.aspx.cs" Inherits="UAS_MSU.SubAdmin.Class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form class="form-inline">
        <br />
        <fieldset>
            <legend>Select Course</legend>
            <asp:DropDownList runat="server" AutoPostBack="true" ID="selectDropDownListCourse"
                CssClass="form-control" Width="100%" OnSelectedIndexChanged="selectDropDownListCourse_SelectedIndexChanged">
            </asp:DropDownList>
        </fieldset>
        <br />
        <fieldset id="add_class" runat="server">
            <legend>Add Class</legend>

            <div class="form-group row">
                <asp:Label ID="label_class_semister" class="col-sm-2 col-form-label" runat="server" Text="Semester"></asp:Label>
                <asp:TextBox ID="textBox_class_semister" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group row">
                <asp:Label ID="label_class_year" class="col-sm-2 col-form-label" runat="server" Text="Year"></asp:Label>
                <asp:TextBox ID="textBox_class_year" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
            </div>
            <asp:Button runat="server" type="submit" class="btn btn-primary mb-2" Text="Add Class" ID="bt_add" OnClick="bt_add_Click" />
            <asp:Button runat="server" type="reset" class="btn btn-primary mb-2" Text="Reset" ID="bt_reset" />
        </fieldset>
        <br />

        <fieldset id="view_course" runat="server">
            <legend>View Class</legend>
            <table class="table">
                <asp:GridView ID="classGrid" runat="server" AutoGenerateColumns="False"
                    OnRowCancelingEdit="classGrid_RowCancelingEdit"
                    OnRowEditing="classGrid_RowEditing"
                    OnRowUpdating="classGrid_RowUpdating"
                    OnRowDeleting="classGrid_RowDeleting"
                    AllowSorting="True"
                    OnSorting="classGrid_Sorting"
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

                        <asp:TemplateField HeaderText="Class Id" Visible="false" HeaderStyle-CssClass="header font" SortExpression="Class_Id">
                            <ItemTemplate>
                                <asp:Label ID="class_id" runat="server" CssClass="one" Text='<%#Bind("Class_Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Semister" HeaderStyle-CssClass="header font" SortExpression="Semister">
                            <ItemTemplate>
                                <asp:Label ID="semister" TextMode="Number" CssClass="one" runat="server" Text='<%#Bind("Semister") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="semister" TextMode="Number" CssClass="one" runat="server" Text='<%#Bind("Semister") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Year" HeaderStyle-CssClass="header font" SortExpression="Year">
                            <ItemTemplate>
                                <asp:Label ID="year" TextMode="Number" CssClass="one" runat="server" Text='<%#Bind("Year") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="year" TextMode="Number" CssClass="one" runat="server" Text='<%#Bind("Year") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </table>

        </fieldset>
    </form>
</asp:Content>
