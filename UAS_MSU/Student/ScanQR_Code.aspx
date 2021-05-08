<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.Master" AutoEventWireup="true" CodeBehind="ScanQR_Code.aspx.cs" Inherits="UAS_MSU.Student.ScanQR_Code" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
	<script src="https://rawgit.com/schmich/instascan-builds/master/instascan.min.js"></script>

	<div class="row">
		<div class="col-md-offset-3" >
			<video style="margin: 1em; border: 1px solid black;" id="preview"></video>
		</div>
	</div>

	<script type="text/javascript">
		let scanner = new Instascan.Scanner({ video: document.getElementById('preview') });
		scanner.addListener('scan', function (content) {
			// console.log(content);
			// alert(content);
			var pageId = '<%=  Page.ClientID %>';
			__doPostBack(pageId, content);
		});
		Instascan.Camera.getCameras().then(function (cameras) {
			if (cameras.length > 0) {
				scanner.start(cameras[0]);
			} else {
				console.error('No cameras found.');
			}
		}).catch(function (e) {
			console.log("this is the start of error in catch function ");
			console.error(e);
		});
	</script>

</asp:Content>
