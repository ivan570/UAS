<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UAS_MSU.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="col-6">Login</h1>
    <br />
    <div class="form-group">
        <div class="form-group row">
            <asp:Label ID="label_username" class="col-sm-2 col-form-label" runat="server" Text="Username"></asp:Label>
            <asp:TextBox ID="textBox_username" class="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="form-group row">
            <asp:Label ID="label_password" class="col-sm-2 col-form-label" runat="server" Text="Password"></asp:Label>
            <asp:TextBox ID="textBox_password" class="form-control" TextMode="Password" runat="server"></asp:TextBox>
        </div>
        <div class="form-group row">
            <asp:HyperLink ID="forgot_password" class="col-sm-2 col-form-label" runat="server" NavigateUrl="~/ForgotPassword.aspx" ForeColor="Red">Forgot Password?</asp:HyperLink>
        </div>
        <asp:Button runat="server" type="submit" class="btn btn-primary mb-2" Text="Login" ID="bt_login" OnClick="bt_login_Click" />
        <asp:Button runat="server" type="reset" class="btn btn-primary mb-2" Text="Reset" ID="reset" />
    </div>
</asp:Content>
