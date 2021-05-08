<%@ Page Title="" Language="C#" MasterPageFile="~/SubAdmin/SubAdmin.Master" AutoEventWireup="true" CodeBehind="TeacherSubject.aspx.cs" Inherits="UAS_MSU.SubAdmin.TeacherSubject2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron complete_padding">
        <form class="form-inline">
            
            <hr class="my-4">

            <div class="row">
                <div class="col-lg-6">
                    <fieldset>
                        <legend>Select Course</legend>
                        
                        <asp:DropDownList runat="server" AutoPostBack="true"
                            ID="selectDropDownListCourse"
                            CssClass="form-control" Width="100%"
                            OnSelectedIndexChanged="selectDropDownListCourse_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </fieldset>
                </div>
                <div class="col-lg-6">
                    <fieldset>
                        <legend>Select Semester & Year</legend>
                        <asp:DropDownList runat="server" AutoPostBack="true"
                            ID="selectDropDownListClass"
                            CssClass="form-control" Width="100%"
                            OnSelectedIndexChanged="selectDropDownListClass_SelectedIndexChanged">
                        </asp:DropDownList>
                    </fieldset>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-6">
                    <fieldset>
                        <legend>Select Subject</legend>
                        <asp:DropDownList runat="server" AutoPostBack="true"
                            ID="selectDropDownListSubject"
                            CssClass="form-control" Width="100%"
                            OnSelectedIndexChanged="selectDropDownListSubject_SelectedIndexChanged">
                        </asp:DropDownList>
                    </fieldset>
                </div>
                <div class="col-lg-6">
                    <fieldset>
                        <legend>Select Teacher</legend>
                        <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" OnTextChanged="Search" AutoPostBack="true"></asp:TextBox>
                    </fieldset>
                </div>
                
            </div>
        </form>

        <br />
        
    </div>
        <hr />
        <asp:GridView ID="teacher" runat="server"
            AutoGenerateColumns="false"
            AllowPaging="true"
            EmptyDataText="NO DATA FOUND"
            CssClass="footable"
            OnPageIndexChanging="OnPaging"
            PageSize="3">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="11%">
                    <ItemTemplate>
                        <asp:Button ID="btn_Edit" runat="server" Text="Assign" 
                            CssClass="one" OnClick="OnAssignClick" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField Visible="false" DataField="Teacher_Id" HeaderStyle-CssClass="header font" ItemStyle-CssClass="one" HeaderText="Teacher Id" ItemStyle-Width="150" />
                <asp:BoundField DataField="Email" HeaderStyle-CssClass="header font" ItemStyle-CssClass="one" HeaderText="Teacher Email" ItemStyle-Width="150" />
                <asp:BoundField DataField="Teacher_Name" HeaderStyle-CssClass="header font" ItemStyle-CssClass="one" HeaderText="Teacher Name" ItemStyle-Width="150" />
                
            </Columns>
        </asp:GridView>
    <br />
    <fieldset>
            <legend>View Assigned Teacher</legend>
            <div>
                <asp:GridView ID="teacherGrid" runat="server"
                    AutoGenerateColumns="False"
                    OnRowDeleting="teacherGrid_RowDeleting"
                    CssClass="footable"
                    EmptyDataText="NO DATA FOUND">
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="11%">
                            <ItemTemplate>
                                <asp:Button ID="btn_Delete" runat="server" Text="Delete" CommandName="Delete" CssClass="one" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Teacher subject id" HeaderStyle-CssClass="header font" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="Teacher_Subject_Id" runat="server" CssClass="one" Text='<%#Eval("Teacher_Subject_Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Teacher Id" Visible="false" HeaderStyle-CssClass="header font" >
                            <ItemTemplate>
                                <asp:Label ID="Teacher_Id" runat="server" CssClass="one" Text='<%#Eval("Teacher_Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Teacher Email" HeaderStyle-CssClass="header font" >
                            <ItemTemplate>
                                <asp:Label ID="Email" runat="server" CssClass="one" Text='<%#Eval("Email") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Teacher Name" HeaderStyle-CssClass="header font">
                            <ItemTemplate>
                                <asp:Label ID="Teacher_Name" runat="server" CssClass="one" Text='<%#Eval("Teacher_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>

</asp:Content>

