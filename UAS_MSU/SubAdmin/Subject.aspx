<%@ Page Title="" Language="C#" MasterPageFile="~/SubAdmin/SubAdmin.Master" AutoEventWireup="true" CodeBehind="Subject.aspx.cs" Inherits="UAS_MSU.SubAdmin.Subject" %>

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
        <div id="showNotice" runat="server">
            <h4 class="text-muted text-center"> Please Add Class first </h4>
        </div>
        <div id="showClass" runat="server">
            <fieldset>
                <legend>Select Semester & Year</legend>
                <asp:DropDownList runat="server" AutoPostBack="true" ID="selectDropDownListClass"
                    CssClass="form-control" Width="100%" OnSelectedIndexChanged="selectDropDownListClass_SelectedIndexChanged">
                </asp:DropDownList>
            </fieldset>
            <br />

            <div class="form-group row">
                <asp:Label ID="label_subject" class="col-sm-2 col-form-label" runat="server" Text="Subject"></asp:Label>
                <asp:TextBox ID="textBox_subject" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="custom-control custom-checkbox">
                <asp:CheckBox CssClass="custom-control-input" ID="checkbox_type" runat="server" />
                <label class="custom-control-label" for="customCheck1">Subject is Elective type</label>
            </div>
            <asp:Button runat="server" type="submit" class="btn btn-primary mb-2"
                Text="Add Subject"
                ID="bt_add" OnClick="bt_add_Click" />
        </div>
    </form>

    <fieldset id="view_subject">
        <br />
        
        <asp:GridView ID="teacherGrid" runat="server" AutoGenerateColumns="False"
            OnRowDeleting="teacherGrid_RowDeleting"
            CssClass="footable"
            EmptyDataText="NO DATA FOUND">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="11%">
                    <ItemTemplate>
                        <asp:Button ID="btn_Delete" runat="server" Text="Delete" CommandName="Delete" CssClass="one" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="11%" Visible="false" HeaderText="Subject Id" HeaderStyle-CssClass="header font">
                    <ItemTemplate>
                        <asp:Label ID="subject_id" runat="server" CssClass="one" Text='<%#Eval("Subject_Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="11%" HeaderStyle-CssClass="header font" HeaderText="Subject Name">
                    <ItemTemplate>
                        <asp:Label ID="subject_name" TextMode="Number" CssClass="one" runat="server" Text='<%#Eval("Subject_Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                 <asp:TemplateField HeaderStyle-Width="11%" HeaderStyle-CssClass="header font" HeaderText="Subject is Elective Type">
                    <ItemTemplate>
                        <asp:Label ID="course_e_type" TextMode="Number" CssClass="one" runat="server" Text='<%#Eval("E_type") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </fieldset>
</asp:Content>
