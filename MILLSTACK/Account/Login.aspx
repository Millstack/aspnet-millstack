<%@ Page Language="C#" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>

    <!-- Boottrap CSS -->
    <link href="../assests/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assests/css/bootstrap1.min.css" rel="stylesheet" />

    <!-- Bootstrap JS -->
    <script src="../assests/js/bootstrap.bundle.min.js"></script>
    <script src="../assests/js/bootstrap1.min.js"></script>

    <!-- Popper.js -->
    <script src="../assests/js/popper.min.js"></script>
    <script src="../assests/js/popper1.min.js"></script>

    <!-- jQuery -->
    <script src="../assests/js/jquery-3.6.0.min.js"></script>
    <script src="../assests/js/jquery.min.js"></script>
    <script src="../assests/js/jquery-3.3.1.slim.min.js"></script>

    <!-- Select2 library CSS and JS -->
    <link href="../assests/select2/select2.min.css" rel="stylesheet" />
    <script src="../assests/select2/select2.min.js"></script>

    <!-- Sweet Alert CSS and JS -->
    <link href="../assests/sweertalert/sweetalert2.min.css" rel="stylesheet" />
    <script src="../assests/sweertalert/sweetalert2.all.min.js"></script>

    <!-- Sumo Select CSS and JS -->
    <link href="../assests/sumoselect/sumoselect.min.css" rel="stylesheet" />
    <script src="../assests/sumoselect/jquery.sumoselect.min.js"></script>

    <!-- DataTables CSS & JS -->
    <link href="../assests/DataTables/datatables.min.css" rel="stylesheet" />
    <script src="../assests/DataTables/datatables.min.js"></script>

    <script src="login.js"></script>
    <link rel="stylesheet" href="login.css" />


</head>
<body>
    <form id="form1" runat="server" class="bg-light">

        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>


        <!-- top div Starts -->
        <div id="divTopSearch" runat="server" visible="true" class="vh-100">
            <div class="col-md-12 mx-auto">

                <!-- Heading -->
                <div class="col-md-12 mx-auto fw-normal fs-3 fw-bold ps-0 pb-2 text-dark-emphasis pt-4 mb-4 text-center">
                    <asp:Literal Text="Welcome to Honey badger ~ CMS" runat="server"></asp:Literal>
                </div>

                <!-- Header UI Starts -->
                <div class="card col-md-6 mx-auto mt-1 py-1 shadow-lg rounded-1 border-light-subtle">
                    <div class="card-body">


                        <!-- Heading 1 -->
                        <div class="fs-5 fw-semibold text-body-tertiary border-bottom pb-2 mb-4">
                            <asp:Literal Text="User Login" runat="server"></asp:Literal>
                        </div>

                        <!-- 1st row Starts -->
                        <div class="row mb-2">

                            <!-- User Email -->
                            <div class="col-md-12 align-self-end">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal5" Text="" runat="server">Email ID:</asp:Literal>
                                    <div>
                                        <asp:RequiredFieldValidator ID="rr1" ControlToValidate="LoginEmail" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="kindly enter email id" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:TextBox ID="LoginEmail" type="email" placeholder="eg: xyz@gmail.com" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" runat="server"></asp:TextBox>
                            </div>

                        </div>
                        <!-- 1st row Ends -->

                        <!-- 2nd row Starts -->
                        <div class="row mb-2">

                            <!-- User Email -->
                            <div class="col-md-12 align-self-end">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal1" Text="" runat="server">Password:</asp:Literal>
                                    <div>
                                        <asp:RequiredFieldValidator ID="rr2" ControlToValidate="LoginPassword" ValidationGroup="finalSubmit" CssClass="invalid-feedback" InitialValue="" runat="server" ErrorMessage="kindly enter password" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:TextBox ID="LoginPassword" type="password" steps="0.01" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" runat="server"></asp:TextBox>
                            </div>

                        </div>
                        <!-- 2nd row Ends -->

                        <div class="fw-lighter fs-6 text-danger font-italic">
                            <asp:Literal ID="LoginFailedLiteral" Text="" runat="server"></asp:Literal>
                        </div>

                        <!-- Submit Button UI Starts -->
                        <div class="">
                            <div class="row mt-4 mb-0">
                                <div class="col-md-6 text-start">

                                </div>
                                <div class="col-md-6 text-end">
                                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" ValidationGroup="finalSubmit" CssClass="btn btn-custom text-white shadow " />
                                </div>
                            </div>
                        </div>
                        <!-- Submit Button UI Ends -->


                    </div>
                </div>
                <!-- Header UI Ends -->

            </div>
        </div>
        <!-- top div Ends -->


    </form>
</body>
</html>
