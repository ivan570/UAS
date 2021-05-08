<%@ Page Title="" Language="C#" MasterPageFile="~/SubAdmin/SubAdmin.Master" AutoEventWireup="true" CodeBehind="Test11.aspx.cs" Inherits="UAS_MSU.Test11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div>

		<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">

			<Columns>

				<asp:TemplateField>

					<ItemTemplate>

						<asp:LinkButton runat="server" CommandArgument='<%# Eval("Faculty_id") %>' OnCommand="LinkButton_Click" Text="View Details"> </asp:LinkButton>

					</ItemTemplate>

				</asp:TemplateField>

			</Columns>

		</asp:GridView>

	</div>

</asp:Content>
