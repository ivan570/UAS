<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teacher.Master" AutoEventWireup="true" CodeBehind="TakeAttendance.aspx.cs" Inherits="UAS_MSU.Teacher.TakeAttendance" %>


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
							OnSelectedIndexChanged="selectDropDownListCourse_SelectedIndexChanged">
						</asp:DropDownList>
					</fieldset>
				</div>
				<div class="col-lg-6">
					<fieldset>
						<legend>Select Class</legend>
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
						<legend>Date</legend>
						<asp:TextBox ID="current_date" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
					</fieldset>
				</div>
			</div>
			<br />
			<div class="row">
				<div class="col-lg-6">
					<fieldset>
						<legend>Duration</legend>
						<div class="row">
							<div class="col-lg-3 col-sm-3 col-md-3">
								<input id="duration_hour" runat="server" class="form-control"
									placeholder="Hour" type="number" min="0" max="23" />
							</div>
							<div class="col-lg-3 col-sm-3 col-md-3">
								<input id="duration_min" runat="server" class="form-control"
									placeholder="Minutes" type="number" min="0" max="59" />
							</div>
						</div>
					</fieldset>
				</div>
				<div class="col-lg-6">
					<fieldset>
						<legend></legend>

						<div class="col-lg-6">
							<asp:RadioButton ID="rad_manualy" runat="server" Text=" Manualy" GroupName="xx" Checked="true" />
							<asp:RadioButton ID="rad_usingOTP" runat="server" Text=" Using OTP" GroupName="xx" />
						</div>

						<div class="row">
							<div class="col-lg-4">
								<asp:Button runat="server" type="submit" class="btn btn-primary mb-2" Text="Take Attendance" ID="bt_add" OnClick="bt_take_attendance_Click" />
							</div>
							<div class="col-lg-2">
								<asp:Button runat="server" type="reset" class="btn btn-primary mb-2" Text="Reset" ID="bt_reset" />
							</div>
						</div>
					</fieldset>
				</div>
			</div>
		</form>
	</div>

	<fieldset id="view_course" runat="server">
		<legend>Attendance Report</legend>

		<asp:GridView ID="attendanceGrid" runat="server"
			AutoGenerateColumns="False"
			OnRowDeleting="attendanceGrid_RowDeleting"
			AllowSorting="True"
			OnSorting="attendanceGrid_Sorting"
			CssClass="footable"
			EmptyDataText="NO DATA FOUND">

			<Columns>
				<asp:TemplateField HeaderStyle-Width="16%">
					<ItemTemplate>
						<asp:Button ID="btn_Edit" CssClass="btn btn-default margin-top" runat="server" Text="Edit" CommandArgument='<%# Bind("Attendance_Id") %>'
							OnCommand="OnEditClick"></asp:Button>
						<asp:Button ID="btn_Delete" runat="server" Text="Delete" CommandName="Delete"
							CssClass="btn btn-default margin-top" />
						<asp:Button ID="btn_download" OnCommand="OnDownloadClick" runat="server"
							Text="Download" CssClass="btn btn-default margin-top" CommandArgument='<%# Bind("Attendance_Id") %>' />
					</ItemTemplate>
				</asp:TemplateField>

				<asp:TemplateField HeaderText="Attendance Id" Visible="false" HeaderStyle-CssClass="header font" SortExpression="Attendance_Id">
					<ItemTemplate>
						<asp:Label ID="Attendance_Id" runat="server" CssClass="one" Text='<%#Bind("Attendance_Id") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="header font" SortExpression="Date">
					<ItemTemplate>
						<asp:Label ID="Date" runat="server" CssClass="one" Text='<%#Bind("Date") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Duration" HeaderStyle-CssClass="header font" SortExpression="Duration">
					<ItemTemplate>
						<asp:Label ID="Cur_Time" runat="server" CssClass="one" Text='<%#Bind("Duration") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Present" HeaderStyle-CssClass="header font" SortExpression="Present">
					<ItemTemplate>
						<asp:Label ID="Present" runat="server" CssClass="one" Text='<%#Bind("Present") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Total" HeaderStyle-CssClass="header font" SortExpression="Total">
					<ItemTemplate>
						<asp:Label ID="Total" runat="server" CssClass="one" Text='<%#Bind("Total") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
	</fieldset>
	<script>

</script>
</asp:Content>
