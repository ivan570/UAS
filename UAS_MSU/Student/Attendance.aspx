<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.Master" AutoEventWireup="true" CodeBehind="Attendance.aspx.cs" Inherits="UAS_MSU.Student.Attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<form class="form-inline">
		<br />
		<fieldset id="f_att_id" runat="server">
			<legend>Attendance</legend>
			<div class="form-group row">
				<asp:Label ID="label_otp" class="col-sm-2 col-form-label" runat="server" Text="Enter OTP"></asp:Label>
				<asp:TextBox ID="textBox_otp" class="form-control" runat="server"></asp:TextBox>
			</div>
			<asp:Button runat="server" type="submit" class="btn btn-primary mb-2" Text="Mark Attendance" ID="bt_attendace" OnClick="bt_attendace_Click" />
			<asp:Button runat="server" type="reset" class="btn btn-primary mb-2" Text="Reset" ID="bt_reset" />
		</fieldset>
		<br />
		<fieldset id="f_QR_id" runat="server">
			<legend>Scan QR code</legend>
			<asp:Button runat="server" type="reset" class="btn btn-primary mb-2"
				Text="Scan QR code" ID="scan_qr" OnClick="scan_qr_Click" />
		</fieldset>
	</form>

</asp:Content>
