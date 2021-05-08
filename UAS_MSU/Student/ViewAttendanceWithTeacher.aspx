<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.Master" AutoEventWireup="true" CodeBehind="ViewAttendanceWithTeacher.aspx.cs" Inherits="UAS_MSU.Student.ViewAttendanceWithTeacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<style>
		.mar {
			margin-top: 10px;
			margin-bottom: 10px;
		}
		
	</style>
	<asp:Button ID="export" runat="server" CssClass="btn btn-primary btn-lg mar" Text="Export" OnClick="export_Click" />
	<asp:GridView ID="student_attendance"
		runat="server"
		AutoGenerateColumns="False"
		CssClass="footable"
		ShowFooter="true"
		EmptyDataText="NO DATA FOUND">
		<Columns>
			<asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="header font">
				<ItemTemplate>
					<asp:Label ID="subject" runat="server" CssClass="one"
						Text='<%#Eval("subject") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:TemplateField HeaderText="Present" HeaderStyle-CssClass="header font">
				<ItemTemplate>
					<asp:Label ID="present" runat="server" CssClass="one"
						Text='<%#Eval("present") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:TemplateField HeaderText="Total" HeaderStyle-CssClass="header font">
				<ItemTemplate>
					<asp:Label ID="total" runat="server" CssClass="one" Text='<%#Eval("total") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:TemplateField HeaderText="Percentage" HeaderStyle-CssClass="header font">
				<ItemTemplate>
					<asp:Label ID="Time" runat="server" CssClass="one" Text='<%#Eval("percentage") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>

		</Columns>
	</asp:GridView>

</asp:Content>
