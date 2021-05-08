<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.Master" AutoEventWireup="true" CodeBehind="viewAttendance.aspx.cs" Inherits="UAS_MSU.Student.viewAttendance" %>

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
		EmptyDataText="NO DATA FOUND">
		<Columns>
			<asp:TemplateField HeaderText="Attendance Id" Visible="false" HeaderStyle-CssClass="header font">
				<ItemTemplate>
					<asp:Label ID="Attendance_Id" runat="server" CssClass="one"
						Text='<%#Eval("Attendance_Id") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:TemplateField HeaderText="Subject" HeaderStyle-CssClass="header font">
				<ItemTemplate>
					<asp:Label ID="Subject" runat="server" CssClass="one"
						Text='<%#Eval("Subject") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="header font">
				<ItemTemplate>
					<asp:Label ID="date" runat="server" CssClass="one" Text='<%#Eval("Date") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:TemplateField HeaderText="Duration" HeaderStyle-CssClass="header font">
				<ItemTemplate>
					<asp:Label ID="Time" runat="server" CssClass="one" Text='<%#Eval("Time") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:TemplateField HeaderText="Present or not ???" HeaderStyle-CssClass="header font">
				<ItemTemplate>
					<asp:Label ID="isPresent" runat="server" CssClass="one" Text='<%#Eval("isPresent") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>

		</Columns>
	</asp:GridView>

</asp:Content>
