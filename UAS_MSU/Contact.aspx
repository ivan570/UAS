<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="UAS_MSU.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <form class="form-inline">
        <fieldset>
            <h1 class="col-6">Contact</h1>
            <br />
            <div class="form-group row">
                <asp:Label ID="label_name" class="col-sm-2 col-form-label" runat="server" Text="Name"></asp:Label>
                <asp:TextBox ID="textBox_name" class="form-control" runat="server"></asp:TextBox>
            </div>
             <div class="form-group row">
                <asp:Label ID="label_email" class="col-sm-2 col-form-label" runat="server" Text="Email"></asp:Label>
                <asp:TextBox ID="textBox_email" class="form-control" runat="server"></asp:TextBox>
            </div>
             <div class="form-group row">
                <asp:Label ID="label_area" class="col-sm-2 col-form-label" runat="server" Text="Details"></asp:Label>
                <asp:TextBox ID="textBox_area" class="form-control" runat="server" TextMode="MultiLine" Rows="10"></asp:TextBox>
            </div>
            <asp:Button runat="server" type="submit" class="btn btn-primary mb-2" Text="Send" ID="bt_add" OnClick="bt_add_Click"/>
            <asp:Button runat="server" type="reset" class="btn btn-primary mb-2" Text="Reset" ID="bt_reset" />
        </fieldset>
    </form>
</asp:Content>
