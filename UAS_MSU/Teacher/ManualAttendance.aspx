<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teacher.Master" AutoEventWireup="true" CodeBehind="ManualAttendance.aspx.cs" Inherits="UAS_MSU.Teacher.ManualAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <form class="form-inline">
        <br />
        <asp:Button runat="server" type="submit" class="btn btn-primary mb-2" Text="Take Attendance" ID="bt_take_attendance" OnClick="bt_take_attendance_Click"  />
        <asp:Button runat="server" type="reset" class="btn btn-primary mb-2" Text="Back" ID="bt_reset" OnClick="bt_reset_Click" />
        <br />
        <br />
    </form>
    <fieldset>
        <legend>View Faculty</legend>
        <div>
            <asp:GridView ID="takeAttendanceGrid" runat="server"
                AutoGenerateColumns="False"
                CssClass="footable"
                EmptyDataText="NO DATA FOUND">
                <Columns>
                    <asp:TemplateField HeaderText="Student Id" Visible="false" HeaderStyle-CssClass="header font">
                        <ItemTemplate>
                            <asp:Label ID="lbl_id" runat="server" CssClass="one" Text='<%#Eval("Student_Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student PRN" HeaderStyle-CssClass="header font">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Prn" runat="server" CssClass="one" Text='<%#Eval("Prn") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Name" HeaderStyle-CssClass="header font">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Name" runat="server" CssClass="one" Text='<%#Eval("Student_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Present" HeaderStyle-Width="11%">
                        
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox_Present" runat="server"  Checked='<% #IsTrue(Eval("isPresent")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </fieldset>

</asp:Content>

