<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test2.aspx.cs" Inherits="UAS_MSU.Test2" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title></title>

    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 400px;
            height: 350px;
        }

        .lbl {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }
    </style>

</head>

<body>

    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:Button ID="Button1" runat="server" Text="Fill Form in Popup" />

        <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="Button1"
            CancelControlID="Button2" BackgroundCssClass="Background">
        </cc1:ModalPopupExtender>

        <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none">
            <table>

                <tr>

                    <td>

                        <asp:Label runat="server" CssClass="lbl" Text="First Name"></asp:Label>

                    </td>

                    <td>

                        <asp:TextBox runat="server" Font-Size="14px"></asp:TextBox>

                    </td>

                </tr>

                <tr>

                    <td>

                        <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="Middle Name"></asp:Label>

                    </td>

                    <td>

                        <asp:TextBox ID="TextBox1" runat="server" Font-Size="14px"></asp:TextBox>

                    </td>

                </tr>

                <tr>

                    <td>

                        <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="Last Name"></asp:Label>

                    </td>

                    <td>

                        <asp:TextBox ID="TextBox2" runat="server" Font-Size="14px"></asp:TextBox>

                    </td>

                </tr>

                <tr>

                    <td>

                        <asp:Label ID="Label3" runat="server" CssClass="lbl" Text="Gender"></asp:Label>

                    </td>

                    <td>

                        <asp:TextBox ID="TextBox3" runat="server" Font-Size="14px"></asp:TextBox>

                    </td>

                </tr>

                <tr>

                    <td>

                        <asp:Label ID="Label4" runat="server" CssClass="lbl" Text="Age"></asp:Label>

                    </td>

                    <td>

                        <asp:TextBox ID="TextBox4" runat="server" Font-Size="14px"></asp:TextBox>

                    </td>

                </tr>

                <tr>

                    <td>

                        <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="City"></asp:Label>

                    </td>

                    <td>

                        <asp:TextBox ID="TextBox5" runat="server" Font-Size="14px"></asp:TextBox>

                    </td>

                </tr>

                <tr>

                    <td>

                        <asp:Label ID="Label6" runat="server" CssClass="lbl" Text="State"></asp:Label>

                    </td>

                    <td>

                        <asp:TextBox ID="TextBox6" runat="server" Font-Size="14px"></asp:TextBox>

                    </td>

                </tr>

            </table>

            <br />

            <asp:Button ID="Button2" runat="server" Text="Close" />

        </asp:Panel>

    </form>

</body>

</html>
