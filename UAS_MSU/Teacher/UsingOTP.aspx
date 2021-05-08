<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teacher.Master" AutoEventWireup="true" CodeBehind="UsingOTP.aspx.cs" Inherits="UAS_MSU.Teacher.UsingOTP" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<script type="text/javascript">
		function countdown() {
			seconds = document.getElementById("MainContent_timerLabel").innerHTML;
			if (seconds > 0) {
				document.getElementById("MainContent_timerLabel").innerHTML = seconds - 1;
				setTimeout("countdown()", 1000);
			}
			else {
				box = document.getElementById("MainContent_div_otp")
				box.hidden = true;
				var pageId = '<%=  Page.ClientID %>';
				__doPostBack(pageId, "argumentString");
			}
		}
		setTimeout("countdown()", 1000);
	</script>


	<div id="BACK" runat="server" style="margin-top:0.5%;">
		<asp:Button runat="server" class="btn btn-primary"
			Text="BACK" Enabled="true" ID="BACK_BTN" OnClick="BACK_BTN_Click" />
		
	</div>

	<div id="otp" runat="server" style="margin-top:0.5%;">
		<asp:Button runat="server" class="btn btn-primary btn-lg btn-block"
			Text="Generate OTP" Enabled="true" ID="bt_otp" OnClick="bt_otp_Click" />
		<div class="alert alert-warning form-group" style="width: 20em; margin-top: 10px" role="alert" visible="false" id="div_otp" runat="server">
			<strong id="st_otp" runat="server"></strong>
			<span style="position: relative; left: 65%;">
				<span id="timerLabel" runat="server">10</span> sec</span>
		</div>
	</div>
	<br />

	<div id="Div1" runat="server" style="margin-top:0.5%;">
		<asp:Button runat="server" class="btn btn-primary btn-lg btn-block"
			Text="Generate QR Code" Enabled="true" ID="bt_qr" OnClick="bt_qr_click" />

		<asp:PlaceHolder ID="PlaceHolder_Qr" runat="server"></asp:PlaceHolder>
	</div>

</asp:Content>
