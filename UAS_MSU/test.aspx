<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="UAS_MSU.test" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search and Filter a DropDownList</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"
        type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=DDL]');
            var $items = $('select[id$=DDL] option');

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                    );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Enter Text in the TextBox to Filter the DropDownList </h2>
            <br />
            <asp:TextBox ID="txtNew" runat="server"
                ToolTip="Enter Text Here"></asp:TextBox><br />
            <asp:DropDownList ID="DDL" runat="server">
            </asp:DropDownList>
            <br />
            <p id="para"></p>
        </div>

    </form>
</body>
</html>
