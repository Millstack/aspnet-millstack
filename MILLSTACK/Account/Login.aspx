<%@ Page Language="C#" Async="true" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login_Login" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>

    <!-- CSS -->
    <link href="<%= ResolveUrl("../assets/components/bootstrap/css/bootstrap.min_v5.3.3.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("../assets/components/select2/select2.min_v4.0.13.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("../assets/components/sweet-alert/sweetalert2.min_v11.11.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("../assets/components/chosen_v1.8.7/chosen.min.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("../assets/components/sumo-select/sumoselect.min_v3.0.3.css") %>" rel="stylesheet" />

    <!-- JavaScript -->
    <script src="<%= ResolveUrl("../assets/components/bootstrap/js/bootstrap.bundle.min_v5.3.3.js") %>"></script>
    <script src="<%= ResolveUrl("../assets/components/bootstrap/js/bootstrap.min_v5.3.3.js") %>"></script>
    <script src="<%= ResolveUrl("../assets/components/bootstrap/js/popper.min_v2.11.8.js") %>"></script>
    <script src="<%= ResolveUrl("../assets/components/jquery/jquery_3.7.1.js") %>"></script>
    <script src="<%= ResolveUrl("../assets/components/jquery/jquery_3.7.1.min.js") %>"></script>
    <script src="<%= ResolveUrl("../assets/components/sweet-alert/sweetalert2_v11.11.js") %>"></script>
    <script src="<%= ResolveUrl("../assets/components/select2/select2.min_v4.0.13.js") %>"></script>
    <script src="<%= ResolveUrl("../assets/components/chosen_v1.8.7/chosen.jquery.min.js") %>"></script>
    <script src="<%= ResolveUrl("../assets/components/sumo-select/jquery.sumoselect.min_v3.0.3.js") %>"></script>

    <!-- DataTables CSS -->
    <link href="<%= ResolveUrl("../assets/components/datatables/datatables.min.css") %>" rel="stylesheet" />
    <!-- DataTables - Button Extension CSS -->
    <link href="<%= ResolveUrl("../assets/components/datatables/Buttons-2.4.2/css/buttons.dataTables.min.css") %>" rel="stylesheet" />
    <!-- DataTables JS -->
    <script src="<%= ResolveUrl("../assets/components/datatables/datatables.min.js") %>"></script>
    <!-- DataTables - Button Extension JS -->
    <script src="<%= ResolveUrl("../assets/components/datatables/Buttons-2.4.2/js/dataTables.buttons.min.js") %>"></script>
    <script src="<%= ResolveUrl("../assets/components/datatables/buttons.flash.min.js") %>"></script>
    <script src="<%= ResolveUrl("../assets/components/datatables/JSZip-3.10.1/jszip.min.js") %>"></script>
    <script src="<%= ResolveUrl("../assets/components/datatables/Buttons-2.4.2/js/buttons.html5.min.js") %>"></script>
    <script src="<%= ResolveUrl("../assets/components/datatables/Buttons-2.4.2/js/buttons.print.min.js") %>"></script>

    <script src="login.js"></script>
    <%--<link rel="stylesheet" href="login.css" />--%>
</head>
<body class="" style="background-color: #E4E9F7;">
    <form id="form1" runat="server">

        <style>
            /* From Uiverse.io by Kabak */
            .custom-button {
                height: 50px;
                margin: 5px;
                width: 120px;
                background: #333;
                -webkit-box-pack: center;
                -ms-flex-pack: center;
                justify-content: center;
                cursor: pointer;
                -webkit-box-align: center;
                -ms-flex-align: center;
                align-items: center;
                font-family: Consolas, Courier New, monospace;
                border: solid #404c5d 1px;
                font-size: 16px;
                color: rgb(161, 161, 161);
                -webkit-transition: 500ms;
                transition: 500ms;
                border-radius: 5px;
                background: linear-gradient(145deg, #2e2d2d, #212121);
                -webkit-box-shadow: -1px -5px 15px #41465b, 5px 5px 15px #41465b, inset 5px 5px 10px #212121, inset -5px -5px 10px #212121;
                box-shadow: -1px -5px 15px #41465b, 5px 5px 15px #41465b, inset 5px 5px 10px #212121, inset -5px -5px 10px #212121;
            }

                .custom-button:hover {
                    -webkit-box-shadow: 1px 1px 13px #20232e, -1px -1px 13px #545b78;
                    box-shadow: 1px 1px 13px #20232e, -1px -1px 13px #545b78;
                    color: #d6d6d6;
                    -webkit-transition: 500ms;
                    transition: 500ms;
                }

                .custom-button:active {
                    -webkit-box-shadow: 1px 1px 13px #20232e, -1px -1px 33px #545b78;
                    box-shadow: 1px 1px 13px #20232e, -1px -1px 33px #545b78;
                    color: #d6d6d6;
                    -webkit-transition: 100ms;
                    transition: 100ms;
                }
        </style>



        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>

        <div class="login-html login-wrap col-md-5 mx-auto mt-5 py-5 shadow-lg rounded-3">
            <ul class="nav nav-tabs">
                <li class="nav-item">
                    <a class="nav-link active bg-dark border border-dark-subtle" id="tab-1" data-bs-toggle="tab" href="#sign-in-tab">Sign In</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" id="tab-2" data-bs-toggle="tab" href="#sign-up-tab">Sign Up</a>
                </li>
            </ul>

            <div class="tab-content pt-4">
                <!-- Sign In Section -->
                <div class="tab-pane fade show active fw-bold text-white" id="sign-in-tab">
                    <div class="mb-3">
                        <label for="txtUsername" class="form-label">Username</label>
                        <asp:TextBox ID="Txt_UserName" runat="server" CssClass="form-control rounded-0 bg-light border border-light-subtle" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="invalid-feedback text-warning" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Txt_UserName" InitialValue="" ValidationGroup="LoginClick" ErrorMessage="enter username">
                        </asp:RequiredFieldValidator>
                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="Txt_UserName"
                            FilterType="Numbers, LowercaseLetters, Custom" ValidChars="." />
                    </div>
                    <div class="mb-3">
                        <label for="txtPassword" class="form-label">Password</label>
                        <asp:TextBox ID="Txt_Passowrd" runat="server" CssClass="form-control rounded-0 bg-light" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="invalid-feedback text-warning" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Txt_Passowrd" InitialValue="" ValidationGroup="LoginClick" ErrorMessage="enter password">
                        </asp:RequiredFieldValidator>
                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="Txt_Passowrd"
                            FilterType="Numbers, UppercaseLetters, LowercaseLetters, Custom" ValidChars=".$#@_ " />
                    </div>
                    <div class="form-check mb-3">
                        <input class="form-check-input" type="checkbox" value="" id="keepSignedIn" checked>
                        <label class="form-check-label" for="keepSignedIn">
                            Keep me Signed in
                        </label>
                    </div>
                    <div class="d-grid col-md-3 mx-auto">
                        <asp:Button ID="btn_Login" runat="server" Text="Log In" OnClick="btn_Login_Click"
                            CssClass="btn btn-dark shadow-lg rounded-0 border border-light" ValidationGroup="LoginClick" />
                    </div>
                    <div class="text-center mt-4">
                        <a class="text-decoration-none" href="#forgot">Forgot Password?</a>
                    </div>
                    <div id="Div_Wrong" runat="server" visible="false" style="font-style: italic;"
                        class="col-md-12 p-3 mt-3 border border-danger bg-light rounded-2 shadow-lg shadow-danger text-danger fs-6 fw-bold">
                        <asp:Literal ID="wrong" runat="server"></asp:Literal>
                    </div>
                </div>

                <!-- Sign Up Section -->
                <div class="tab-pane fade" id="sign-up-tab">
                    <div class="mb-3">
                        <label for="txtNewUsername" class="form-label">Username</label>
                        <asp:TextBox ID="txtNewUsername" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="txtNewPassword" class="form-label">Password</label>
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" />
                    </div>
                    <div class="mb-3">
                        <label for="txtConfirmPassword" class="form-label">Repeat Password</label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" />
                    </div>
                    <div class="mb-3">
                        <label for="txtEmail" class="form-label">Email Address</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                    </div>
                    <div class="d-grid">
                        <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" CssClass="custom-button" />
                    </div>
                    <div class="text-center mt-4">
                        <a href="#tab-1" data-bs-toggle="tab">Already Member?</a>
                    </div>
                </div>
            </div>
        </div>

        <!-- Custom CSS -->
        <style>
            .login-wrap {
                position: relative;
                /*background: url('/Account/programming_1.jpg') no-repeat center;*/
                /*background: url('/Account/computer_1.jpg') no-repeat center;*/
                background: url('/Account/computer_2.jpg') no-repeat center;
                background-size: cover;
                padding: 3rem;
                border-radius: 8px;
                box-shadow: 0 12px 15px 0 rgba(0, 0, 0, .24), 0 17px 50px 0 rgba(0, 0, 0, .19);
                color: #fff;
                z-index: 1;
                overflow: hidden; /* Ensure pseudo-element is contained */
            }

                .login-wrap::before {
                    content: "";
                    position: absolute;
                    top: 0;
                    left: 0;
                    width: 100%;
                    height: 100%;
                    background: rgba(0, 0, 0, 0.4); /* Adjust opacity as needed */
                    z-index: -1;
                    transition: background 0.5s ease-in-out; /* Fading effect */
                }

                .login-wrap:hover::before {
                    background: rgba(0, 0, 0, 0.6); /* Increased fade on hover */
                }


            .login-html {
                background-color: rgba(40, 57, 101, .9);
                padding: 30px;
                border-radius: 8px;
            }

            .nav-tabs .nav-link.active {
                background-color: #1161ee;
                color: white;
            }

            .nav-tabs .nav-link {
                color: white;
            }

            .form-label {
                color: #aaa;
            }
        </style>


        <%-- <div class="login-wrap col-md-5 mx-auto">
            <div class="login-html">
                <input id="tab-1" type="radio" name="tab" class="sign-in" checked><label for="tab-1" class="tab">Sign In</label>
                <input id="tab-2" type="radio" name="tab" class="sign-up"><label for="tab-2" class="tab">Sign Up</label>
                <div class="login-form">
                    <div class="sign-in-htm">
                        <div class="group">
                            <label for="user" class="label">Username</label>
                            <input id="user" type="text" class="input">
                        </div>
                        <div class="group">
                            <label for="pass" class="label">Password</label>
                            <input id="pass" type="password" class="input" data-type="password">
                        </div>
                        <div class="group">
                            <input id="check" type="checkbox" class="check" checked>
                            <label for="check"><span class="icon"></span>Keep me Signed in</label>
                        </div>
                        <div class="group">
                            <input type="submit" class="button" value="Sign In">
                        </div>
                        <div class="hr"></div>
                        <div class="foot-lnk">
                            <a href="#forgot">Forgot Password?</a>
                        </div>
                    </div>
                    <div class="sign-up-htm">
                        <div class="group">
                            <label for="user" class="label">Username</label>
                            <input id="user" type="text" class="input">
                        </div>
                        <div class="group">
                            <label for="pass" class="label">Password</label>
                            <input id="pass" type="password" class="input" data-type="password">
                        </div>
                        <div class="group">
                            <label for="pass" class="label">Repeat Password</label>
                            <input id="pass" type="password" class="input" data-type="password">
                        </div>
                        <div class="group">
                            <label for="pass" class="label">Email Address</label>
                            <input id="pass" type="text" class="input">
                        </div>
                        <div class="group">
                            <input type="submit" class="button" value="Sign Up">
                        </div>
                        <div class="hr"></div>
                        <div class="foot-lnk">
                            <label for="tab-1">
                            Already Member?</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
    </form>
</body>
</html>
