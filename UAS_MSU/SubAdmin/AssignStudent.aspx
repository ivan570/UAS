<%@ Page Title="" Language="C#" MasterPageFile="~/SubAdmin/SubAdmin.Master" AutoEventWireup="true" CodeBehind="AssignStudent.aspx.cs" Inherits="UAS_MSU.SubAdmin.AssignStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"
        type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=DDL]');
            var $items = $('select[id$=DDL] option');

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                    );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }
            }
        });
    </script>
    <form class="form-inline">
        <br />
        <div class="row">
            <div class="col-lg-6">
                <fieldset>
                    <legend>Select Course</legend>
                    <asp:DropDownList runat="server" AutoPostBack="true" ID="selectDropDownListCourse"
                        CssClass="form-control" Width="100%"
                        OnSelectedIndexChanged="selectDropDownListCourse_SelectedIndexChanged">
                    </asp:DropDownList>
                </fieldset>
            </div>
            <div class="col-lg-6">
                <fieldset>
                    <legend>Select Class</legend>
                    <asp:DropDownList runat="server" AutoPostBack="true" ID="selectDropDownListClass"
                        CssClass="form-control" Width="100%"
                        OnSelectedIndexChanged="selectDropDownListClass_SelectedIndexChanged">
                    </asp:DropDownList>
                </fieldset>
            </div>
        </div>
        <br />
        <br />
        <div class="row">
            <div class="col-lg-6">
                <fieldset>
                    <legend>Select Elective Subject</legend>
                    <asp:DropDownList runat="server" AutoPostBack="true" ID="selectDropDownListSubject"
                        CssClass="form-control" Width="100%"
                        OnSelectedIndexChanged="selectDropDownListSubject_SelectedIndexChanged">
                    </asp:DropDownList>
                </fieldset>
            </div>
            <div class="col-lg-6">
                <fieldset>
                    <legend>Search Student</legend>
                    <asp:TextBox ID="txtNew" placeholder="Search Student" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:DropDownList ID="DDL" AutoPostBack="true" CssClass="form-control margin-top-ddl" runat="server">
                    </asp:DropDownList>

                    <p id="para"></p>
                </fieldset>
                <div class="d-flex flex-row-reverse">
                    <asp:Button runat="server" type="submit" class="btn btn-primary"
                        Text="Assign" ID="bt_assign" OnClick="bt_Assign_click" />
                </div>
            </div>
        </div>
    </form>

    <fieldset>
        <legend>Add Student using Excel </legend>
        <a target="_blank" href="https://drive.google.com/file/d/1yGNGAcxR6P1b26LhBR-523fehwc8Moi7/view?usp=sharing">Download</a>
        <asp:FileUpload runat="server" class="form-control " ID="fileupload" />
        <asp:Button runat="server" class="btn btn-primary" Style="margin-top: 0.41%;"
            ID="btn_upload" Text="Upload" OnClick="btnUpload_Click" />
    </fieldset>
    <br />

    <fieldset>
        <legend>View Student</legend>
        <div>
            <asp:GridView ID="studentGrid" runat="server"
                AutoGenerateColumns="False"
                OnRowDeleting="studentGrid_RowDeleting"
                CssClass="footable"
                AllowSorting="True"
                OnSorting="studentGrid_Sorting"
                EmptyDataText="NO DATA FOUND">
                <Columns>
                    <asp:TemplateField HeaderStyle-Width="11%">
                        <ItemTemplate>
                            <asp:Button ID="btn_Delete" runat="server" Text="Delete" CommandName="Delete" CssClass="one" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student_Subject Id" Visible="false" HeaderStyle-CssClass="header font" SortExpression="Student_Subject_Id">
                        <ItemTemplate>
                            <asp:Label ID="Student_Subject_Id" runat="server" CssClass="one" Text='<%#Bind("Student_Subject_Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Name" HeaderStyle-CssClass="header font" SortExpression="Student_Name">
                        <ItemTemplate>
                            <asp:Label ID="Student_Name" runat="server" CssClass="one" Text='<%#Bind("Student_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PRN" HeaderStyle-CssClass="header font" SortExpression="Prn">
                        <ItemTemplate>
                            <asp:Label ID="prn" runat="server" CssClass="one" Text='<%#Bind("Prn") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email" HeaderStyle-CssClass="header font" SortExpression="Email">
                        <ItemTemplate>
                            <asp:Label ID="email" runat="server" CssClass="one" Text='<%#Bind("Email") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </fieldset>
</asp:Content>

