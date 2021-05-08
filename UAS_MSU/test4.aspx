<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test4.aspx.cs" Inherits="UAS_MSU.test4" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
    <style type="text/css">
        .auto-style1 {
            position: relative;
            width: 92%;
            -ms-flex: 0 0 50%;
            flex: 0 0 50%;
            max-width: 50%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <br>
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-body">

                            <div class="row">
                                <div class="col">
                                    <center><img width="100px" src="images/member1.jpg" /></center>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <hr>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4 mx-auto">
                                    <center>
                           <label>Contact no.</label>
                           <div class="form-group">
                           <div class="input-group">
                              <asp:TextBox CssClass="form-control" ID="TextBox9" runat="server" placeholder="Mobile No."></asp:TextBox>
                              <asp:LinkButton class="btn btn-primary" ID="LinkButton4" runat="server" ><i class="fas fa-check-circle"></i></asp:LinkButton>
                           </div>
                           </div>
                            </center>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label>Full Name</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox10" runat="server" placeholder="Full Name" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <lable>Email ID</lable>
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" ID="TextBox1" runat="server" placeholder="Email ID" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-8 mx-auto">
                                    <center>
                              <div class="form-group">
                                <asp:Button class="btn btn-lg btn-block btn-danger" ID="Button1" runat="server" Text="Delete User"/>
                              </div>
                              </center>
                                </div>
                            </div>
                            <br>
                        </div>
                    </div>
                </div>

                <div class="auto-style1">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col">
                                    <center>
                           <h4>Member List</h4>
                        </center>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <hr>
                                </div>
                            </div>
                            <div class="row">
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ivankshu\source\repos\UAS_MSU\UAS_MSU\App_Data\Database.mdf;Integrated Security=True" 
                                    SelectCommand="SELECT * FROM Teacher"></asp:SqlDataSource>
                                <div class="col">
                                    <asp:GridView class="table table-striped table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False"
                                        DataSourceID="SqlDataSource1">
                                        <Columns>
                                            <asp:BoundField DataField="Teacher_id" HeaderText="Name" ItemStyle-Width="150" />
                                            <asp:BoundField DataField="Teacher_Name" HeaderText="City" ItemStyle-Width="150" />
                                            <asp:BoundField DataField="Email" HeaderText="Country" ItemStyle-Width="150" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
