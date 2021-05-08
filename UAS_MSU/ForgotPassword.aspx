<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="UAS_MSU.ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<style>
		legend {
			color: #FFF !important;
			font-size: 25px;
		}
	</style>
	<h1>Forgot Password</h1>
	<form class="form-inline">
		<br />
		<fieldset>
			<legend>Credential</legend>
			<div class="form-group row">
				<asp:Label ID="label_username" class="col-sm-2 col-form-label" runat="server" Text="Username"></asp:Label>
				<asp:TextBox ID="textBox_username" class="form-control" TextMode="Email" runat="server"></asp:TextBox>
			</div>
			<asp:Button runat="server" type="submit" class="btn btn-primary mb-2" Text="Get OTP" ID="bt_get_otp" OnClick="bt_get_otp_Click" />
			<asp:Button runat="server" type="reset" class="btn btn-primary mb-2" Text="Reset" ID="bt_reset" OnClick="bt_reset_Click" />
		</fieldset>
		<br />
		<fieldset>
			<legend>OTP Verification</legend>
			<div class="form-group row">
				<asp:Label ID="label_otp" class="col-sm-2 col-form-label" runat="server" Text="OTP"></asp:Label>
				<asp:TextBox ID="textBox_otp" class="form-control" runat="server" Enabled="false"></asp:TextBox>
			</div>
			<asp:Button runat="server" type="submit" class="btn btn-primary mb-2" Text="Verify" ID="bt_verify" OnClick="bt_verify_Click" Enabled="false" />
			<asp:Button runat="server" type="submit" class="btn btn-primary mb-2" Text="Resend OTP" ID="bt_resend_otp" OnClick="bt_resend_otp_Click" Enabled="false" />
		</fieldset>
		<br />
		<fieldset>
			<legend>Set Password</legend>
			<div class="form-group row">
				<asp:Label ID="label_set_password" class="col-sm-2 col-form-label" runat="server" Text="Set Password"></asp:Label>
				<asp:TextBox ID="textBox_set_password" TextMode="Password" class="form-control" runat="server" Enabled="false"></asp:TextBox>
			</div>
			<div class="form-group row">
				<asp:Label ID="label_confirm_password" class="col-sm-2 col-form-label" runat="server" Text="Confirm Password"></asp:Label>
				<asp:TextBox ID="textBox_confirm_password" TextMode="Password" class="form-control" runat="server" Enabled="false"></asp:TextBox>
			</div>
			<asp:Button runat="server" type="submit" class="btn btn-primary mb-2" Text="Forgot" ID="bt_forgot" OnClick="bt_forgot_Click" Enabled="false" />
			<asp:Button runat="server" type="reset" class="btn btn-primary mb-2" Text="Home" ID="bt_home" OnClick="bt_home_Click" />
		</fieldset>
		<br />
	</form>


</asp:Content>
