<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Department.aspx.cs" Inherits="UAS_MSU.Admin.Department" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<form class="form-inline">
		<br />
		<fieldset>
			<legend>Select Faculty</legend>

			<asp:DropDownList ID="selectDropDownList" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%"
				OnSelectedIndexChanged="selectDropDownList_SelectedIndexChanged" EnableViewState="true">
				 
			</asp:DropDownList>

		</fieldset>
		<br />
		<fieldset>
			<legend>Add Department</legend>
			<div class="form-group row">
				<asp:Label ID="label_department_name" class="col-sm-2 col-form-label" runat="server" Text="Department Name"></asp:Label>
				<asp:TextBox ID="textBox_department_name" class="form-control" runat="server"></asp:TextBox>
			</div>
			<div class="form-group row">
				<asp:Label ID="label_department_hod" class="col-sm-2 col-form-label" runat="server" Text="HOD Name"></asp:Label>
				<asp:TextBox ID="textBox_department_hod" class="form-control" runat="server"></asp:TextBox>
			</div>
			<div class="form-group row">
				<asp:Label ID="label_hod_username" class="col-sm-2 col-form-label" runat="server" Text="HOD username"></asp:Label>
				<asp:TextBox ID="textBox_hod_username" class="form-control" runat="server"></asp:TextBox>
			</div>
			<div class="form-group row">
				<asp:Label ID="label_hod_password" class="col-sm-2 col-form-label" runat="server" Text="HOD Password"></asp:Label>
				<asp:TextBox ID="textBox_hod_password" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
			</div>
			<asp:Button runat="server" type="submit" class="btn btn-primary mb-2" Text="Add Department" ID="bt_add" OnClick="bt_add_Click" />
			<asp:Button runat="server" type="reset" class="btn btn-primary mb-2" Text="Reset" ID="bt_reset" />
		</fieldset>
		<br />
		<br />
		<fieldset>
			<legend>View Department</legend>
			<div>
				<asp:GridView ID="departmentGrid" runat="server"
					AutoGenerateColumns="False"
					OnRowCancelingEdit="departmentGrid_RowCancelingEdit"
					OnRowEditing="departmentGrid_RowEditing"
					OnRowUpdating="departmentGrid_RowUpdating"
					OnRowDeleting="departmentGrid_RowDeleting"
					AllowSorting="True"
					OnSorting="departmentGrid_Sorting"
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

						<asp:TemplateField HeaderText="Department Id" Visible="false" HeaderStyle-CssClass="header font" SortExpression="Department_Id">
							<ItemTemplate>
								<asp:Label ID="Department_Id" runat="server" CssClass="one" Text='<%#Bind("Department_Id") %>'></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Department Name" HeaderStyle-CssClass="header font" SortExpression="Department_Name">
							<ItemTemplate>
								<asp:Label ID="Department_Name" runat="server" CssClass="one" Text='<%#Bind("Department_Name") %>'></asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox ID="Department_Name" runat="server" CssClass="one" Text='<%#Bind("Department_Name") %>'></asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="HOD Name" HeaderStyle-CssClass="header font" SortExpression="Hod_Name">
							<ItemTemplate>
								<asp:Label ID="Hod_Name" runat="server" CssClass="one" Text='<%#Bind("Hod_Name") %>'></asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox ID="Hod_Name" runat="server" CssClass="one" Text='<%#Bind("Hod_Name") %>'></asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="HOD Email" HeaderStyle-CssClass="header font" SortExpression="Hod_Username">
							<ItemTemplate>
								<asp:Label ID="Hod_Username" runat="server" CssClass="one" Text='<%#Bind("Hod_Username") %>'></asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox ID="Hod_Username" TextMode="Email" CssClass="one" runat="server" Text='<%#Bind("Hod_Username") %>'></asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateField>
					</Columns>
				</asp:GridView>
			</div>
		</fieldset>
	</form>
</asp:Content>
