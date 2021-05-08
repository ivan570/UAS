<%@ Page Title="" Language="C#" MasterPageFile="~/SubAdmin/SubAdmin.Master" AutoEventWireup="true" CodeBehind="CheckReports.aspx.cs" Inherits="UAS_MSU.SubAdmin.CheckReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<style>
		.mar {
			margin-top: 10px;
			margin-bottom: 10px;
		}
		.mar2 {
			margin-top: 20px;
		}
	</style>
	<div class="form-group row mar2">
		<asp:Label ID="lbl_prn" class="col-sm-2 col-form-label" runat="server" Text="PRN"></asp:Label>
		<asp:TextBox ID="textBox_prn" class="form-control" runat="server" TextMode="Number"></asp:TextBox>
	</div>
	<asp:Button ID="check" runat="server" CssClass="btn btn-primary btn-lg mar" Text="Check" OnClick="check_Click" />
	<asp:Button ID="export" runat="server" CssClass="btn btn-primary btn-lg mar" Text="Export" OnClick="export_Click" />
	<asp:GridView ID="student_attendance"
		runat="server"
		AutoGenerateColumns="False"
		CssClass="footable"
		EmptyDataText="NO DATA FOUND">
		<Columns>

			<asp:TemplateField HeaderText="Attendance Id" HeaderStyle-CssClass="header font">
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
