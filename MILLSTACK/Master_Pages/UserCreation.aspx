<%@ Page Title="" Language="C#" Async="true" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="UserCreation.aspx.cs" Inherits="Master_Pages_UserCreation" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">

    <!-- Heading -->
    <div class="col-md-11 mx-auto fw-normal fs-3 fw-bold ps-0 pb-2 text-dark-emphasis mt-1 mb-1 text-center">
        <asp:Literal ID="Page_Heading" Text="User Creation" runat="server"></asp:Literal>
    </div>

    <!-- User Details Starts -->
    <div id="Div_Control" runat="server" class="card col-md-11 mx-auto mt-1 py-2 shadow rounded-3">
        <div class="card-body">

            <!-- Heading - BG -->
            <div class="fs-5 fw-medium text-white border border-dark-subtle shadow rounded-2 text-left py-2 px-3 mb-3" style="background-color: #0f3f6f !important;">
                <asp:Literal ID="Main_Heading_1" Text="User Details" runat="server"></asp:Literal>
            </div>

            <!-- Row 1 Starts -->
            <div class="row mb-2" style="border-style: solid none none none; border-width: 1px; border-color: #d6d5d5; padding-top: 10px; padding-bottom: 10px; margin-top: 10px; margin-bottom: 10px;">

                <!-- TetxtBox: First Name -->
                <div class="col-md-4 mb-3 align-self-end">
                    <div class="mb-1 fw-normal fs-6">
                        <asp:Literal ID="TxtID1" runat="server" Text="">First Name
                            <em style="color: red">*</em>
                        </asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Txt_First_Name" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Txt_First_Name" type="text" Enabled="true" min="0" MaxLength="500"
                        CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="Txt_First_Name"
                        FilterType="Numbers, UppercaseLetters, LowercaseLetters, Custom" ValidChars=". " />
                </div>

                <!-- TetxtBox: Middle Name -->
                <div class="col-md-4 mb-3 align-self-end">
                    <div class="mb-1 fw-normal fs-6">
                        <asp:Literal ID="Literal1" runat="server" Text="">Middle Name
                            <%--<em style="color: red">*</em>--%>
                        </asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Txt_Middle_Name" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Txt_Middle_Name" type="text" Enabled="true" min="0" MaxLength="500"
                        CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="Txt_Middle_Name"
                        FilterType="Numbers, UppercaseLetters, LowercaseLetters, Custom" ValidChars=". " />
                </div>

                <!-- TetxtBox: Last Name -->
                <div class="col-md-4 mb-3 align-self-end">
                    <div class="mb-1 fw-normal fs-6">
                        <asp:Literal ID="Literal2" runat="server" Text="">Last Name
                             <em style="color: red">*</em>
                        </asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Txt_Laste_Name" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Txt_Laste_Name" type="text" Enabled="true" min="0" MaxLength="500"
                        CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="Txt_Laste_Name"
                        FilterType="Numbers, UppercaseLetters, LowercaseLetters, Custom" ValidChars=". " />
                </div>

                <!-- DropDown: Gender -->
                <div class="col-md-4 mb-3 align-self-end">
                    <div class="mb-1 fw-normal fs-6">
                        <asp:Literal ID="Foreign_Table_2_Key_Text" runat="server" Text="">Gender
                            <em style="color: red">*</em>
                        </asp:Literal>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                ControlToValidate="DD_Gender" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <asp:DropDownList ID="DD_Gender" runat="server" Width="100%" CssClass="form-control chosen-dropdown"
                        AutoPostBack="false">
                    </asp:DropDownList>
                </div>

                <!-- TetxtBox: Phone Number -->
                <div class="col-md-4 mb-3 align-self-end">
                    <div class="mb-1 fw-normal fs-6">
                        <asp:Literal ID="Literal3" runat="server" Text="">Phone Number
                            <em style="color: red">*</em>
                        </asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Txt_Phone_Number" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="invalid-feedback" ErrorMessage="Invalid mobile number"
                            runat="server" SetFocusOnError="True" Display="Dynamic" ToolTip="" ValidationExpression="^\d{10}$" ControlToValidate="Txt_Phone_Number">
                        </asp:RegularExpressionValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Txt_Phone_Number" type="number" Enabled="true" min="0" MaxLength="10"
                        CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="Txt_Phone_Number"
                        FilterType="Numbers" />
                </div>

                <!-- TetxtBox: Email -->
                <div class="col-md-4 mb-3 align-self-end">
                    <div class="mb-1 fw-normal fs-6">
                        <asp:Literal ID="Literal4" runat="server" Text="">Email
                            <em style="color: red">*</em>
                        </asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Txt_Email" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Txt_Email" type="email" Enabled="true" min="0" MaxLength="500"
                        CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="Txt_Email"
                        FilterType="Numbers, UppercaseLetters, LowercaseLetters, Custom" ValidChars=".  @" />
                </div>

                <!-- TetxtArea: Address -->
                <div class="col-md-12 mb-3 align-self-end">
                    <div class="mb-1 fw-normal fs-6">
                        <asp:Literal ID="Literal5" runat="server" Text="">Address
                            <em style="color: red"></em>
                        </asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="TA_Address" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                        </asp:RequiredFieldValidator>
                    </div>
                    <textarea id="TA_Address" runat="server" rows="3" cols="50"
                        class="form-control focus-input border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1 shadow-sm"></textarea>
                </div>

                <!-- TetxtBox: Username -->
                <div class="col-md-4 mb-3 align-self-end">
                    <div class="mb-1 fw-normal fs-6">
                        <asp:Literal ID="Literal6" runat="server" Text="">Username
                            <em style="color: red">*</em>
                        </asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Txt_User_Name" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Txt_User_Name" type="text" Enabled="true" min="0" MaxLength="500"
                        CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="Txt_User_Name"
                        FilterType="Numbers, LowercaseLetters, Custom" ValidChars="." />
                </div>

                <!-- TetxtBox: Password -->
                <div class="col-md-4 mb-3 align-self-end">
                    <div class="mb-1 fw-normal fs-6">
                        <asp:Literal ID="Literal7" runat="server" Text="">Password
                            <em style="color: red">*</em>
                        </asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Txt_Password" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Txt_Password" type="text" Enabled="true" min="0" MaxLength="500"
                        CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" oninput="validatePassword()"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="Txt_Password"
                        FilterType="Numbers, UppercaseLetters, LowercaseLetters, Custom" ValidChars=".#$@_" />
                </div>

                <!-- TetxtBox: Confirm Password -->
                <div class="col-md-4 mb-3 align-self-end">
                    <div class="mb-1 fw-normal fs-6">
                        <asp:Literal ID="Literal8" runat="server">Confirm Password
                            <em style="color: red">*</em>
                            <span id="PasswordMessage" class="text-danger" style="font-style: italic;"></span>
                        </asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Txt_Confirm_Password" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                        </asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidator1" ClientValidationFunction="validatePasswordCustom" ValidationGroup="finalSubmit"
                            CssClass="invalid-feedback" runat="server" ErrorMessage="password did not match !!" ControlToValidate="Txt_Confirm_Password">
                        </asp:CustomValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Txt_Confirm_Password" type="password" Enabled="true" min="0" MaxLength="500"
                        CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1" oninput="validatePassword()"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="Txt_Confirm_Password"
                        FilterType="Numbers, UppercaseLetters, LowercaseLetters, Custom" ValidChars=".#$@_" />
                </div>

            </div>
            <!-- Row 1 Ends -->

        </div>
    </div>
    <!-- User Details Ends -->

    <!-- Work Area Allocation Starts -->
    <div id="Div1" runat="server" class="card col-md-11 mx-auto mt-1 py-2 mt-5 mb-5 shadow rounded-3">
        <div class="card-body">

            <!-- Heading - BG -->
            <div class="fs-5 fw-medium text-white border border-dark-subtle shadow rounded-2 text-left py-2 px-3 mb-3" style="background-color: #0f3f6f !important;">
                <asp:Literal ID="Literal9" Text="Work Area Details" runat="server"></asp:Literal>
            </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <!-- Row 1 Starts -->
                    <div class="row mb-2" style="border-style: solid none none none; border-width: 1px; border-color: #d6d5d5; padding-top: 10px; padding-bottom: 10px; margin-top: 10px; margin-bottom: 10px;">

                        <!-- DropDown: Hierarchy -->
                        <div class="col-md-4 mb-3 align-self-end">
                            <div class="mb-1 fw-normal fs-6">
                                <asp:Literal ID="Literal10" runat="server" Text="">Hierarchy
                            <em style="color: red">*</em>
                                </asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                        ControlToValidate="DD_Hierarchy" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:DropDownList ID="DD_Hierarchy" runat="server" Width="100%" CssClass="form-control chosen-dropdown"
                                OnSelectedIndexChanged="DD_Hierarchy_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <!-- ListBox: Role -->
                        <div class="col-md-4 mb-3 align-self-end">
                            <div class="mb-1 fw-normal fs-6">
                                <asp:Literal ID="Literal11" runat="server" Text="">Role
                            <em style="color: red">*</em>
                                </asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                        ControlToValidate="MCDD_Role" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:ListBox ID="MCDD_Role" runat="server" SelectionMode="Multiple" Width="100%" CssClass="form-control listbox"
                                AutoPostBack="false"></asp:ListBox>
                        </div>

                        <!-- DropDown: Status -->
                        <div class="col-md-4 mb-3 align-self-end">
                            <div class="mb-1 fw-normal fs-6">
                                <asp:Literal ID="Literal12" runat="server" Text="">Status
                            <em style="color: red">*</em>
                                </asp:Literal>
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                        ControlToValidate="DD_Status" InitialValue="-1" ValidationGroup="finalSubmit" ErrorMessage="required field">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <asp:DropDownList ID="DD_Status" runat="server" Width="100%" CssClass="form-control chosen-dropdown"
                                AutoPostBack="false">
                                <asp:ListItem Text="" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                <asp:ListItem Text="In-Active" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>
                    <!-- Row 1 Ends -->



                    <!-- Row 2 Starts -->
                    <div class="row mb-3">

                        <!-- CheckBoxList: Division -->
                        <div id="Div_Division" runat="server" visible="false" class="form-group col-md-4 m-0 shadow">
                            <div class="mb-1 fw-normal fs-6">
                                <asp:Literal ID="Literal13" runat="server" Text="">Division
                            <em style="color: red">*</em>
                                </asp:Literal>
                            </div>
                            <div class="col-md-12 bg-light mb-2" style="height: 300px; overflow-x: hidden; overflow-y: auto;">
                                <div class="row">
                                    <div class="col-md-6 form-group">
                                        <asp:TextBox ID="Txt_CheckBoxList_Division" placeholder="Search..." CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 form-group">
                                        <asp:CheckBox ID="Check_All_Division" CssClass="form-control" Text="Select All" runat="server" AutoPostBack="True" 
                                            OnCheckedChanged="Check_All_Division_CheckedChanged" />
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-12 m-0">
                                        <asp:CheckBoxList ID="CheckBoxList_Division" runat="server" OnSelectedIndexChanged="CheckBoxList_Division_SelectedIndexChanged" AutoPostBack="true"></asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- CheckBoxList: District -->
                        <div id="Div_District" runat="server" visible="false" class="form-group col-md-4 m-0 shadow">
                            <div class="mb-1 fw-normal fs-6">
                                <asp:Literal ID="Literal14" runat="server" Text="">District
                                    <em style="color: red">*</em>
                                </asp:Literal>
                            </div>
                            <div class="col-md-12 bg-light mb-2" style="height: 300px; overflow-x: hidden; overflow-y: auto;">
                                <div class="row">
                                    <div class="col-md-6 form-group">
                                        <asp:TextBox ID="Txt_CheckBoxList_District" placeholder="Search..." CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 form-group">
                                        <asp:CheckBox ID="Check_All_District" CssClass="form-control" Text="Select All" runat="server" AutoPostBack="True" 
                                            OnCheckedChanged="Check_All_District_CheckedChanged" />
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-12 m-0">
                                        <asp:CheckBoxList ID="CheckBoxList_District" runat="server" OnSelectedIndexChanged="CheckBoxList_District_SelectedIndexChanged" AutoPostBack="true"></asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- CheckBoxList: Taluka -->
                        <div id="Div_Taluka" runat="server" visible="false" class="form-group col-md-4 m-0 shadow">
                            <div class="mb-1 fw-normal fs-6">
                                <asp:Literal ID="Literal15" runat="server" Text="">Taluka
                                    <em style="color: red">*</em>
                                </asp:Literal>
                            </div>
                            <div class="col-md-12 bg-light mb-2" style="height: 300px; overflow-x: hidden; overflow-y: auto;">
                                <div class="row">
                                    <div class="col-md-6 form-group">
                                        <asp:TextBox ID="Txt_CheckBoxList_Taluka" placeholder="Search..." CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 form-group">
                                        <asp:CheckBox ID="Check_All_Taluka" CssClass="form-control" Text="Select All" runat="server" AutoPostBack="True" 
                                            OnCheckedChanged="Check_All_Taluka_CheckedChanged" />
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-12 m-0">
                                        <asp:CheckBoxList ID="CheckBoxList_Taluka" runat="server" AutoPostBack="false"></asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <!-- Row 2 Ends -->

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
    <!-- Work Area Allocation Ends -->

    <!-- Submit Button UI Starts -->
    <div class="col-md-11 mx-auto row mt-5 mb-2 align-self-end">
        <div class="col-md-6 text-start">
            <asp:Button ID="Btn_Back" runat="server" Text="Back" OnClick="Btn_Back_Click"
                CssClass="btn col-md-2 text-white shadow rounded-0" Style="background: #0f3f6f; color: #fff" />
        </div>
        <div class="col-md-6 text-end">
            <asp:Button ID="Btn_Submit" runat="server" Text="Save" OnClick="Btn_Submit_Click" ValidationGroup="finalSubmit"
                CssClass="btn col-md-2 text-white shadow rounded-0" Style="background: #0f3f6f; color: #fff" />
        </div>
    </div>
    <!-- Submit Button UI Ends -->



    <script type="text/javascript">
        function validatePassword() {
            var password = document.getElementById('<%= Txt_Password.ClientID %>').value;
            var confirmPassword = document.getElementById('<%= Txt_Confirm_Password.ClientID %>').value;
            var passwordMessage = document.getElementById('PasswordMessage');
            var isValid = true;

            // Check if the password field is empty
            if (password === "") {
                passwordMessage.textContent = "Password is required.";
                passwordMessage.classList.remove('text-success');
                passwordMessage.classList.add('text-danger');
                passwordMessage.style.fontStyle = 'italic';
                document.getElementById('<%= Txt_Confirm_Password.ClientID %>').classList.add('is-invalid');
                isValid = false;
            }
            else if (confirmPassword === password) {
                passwordMessage.textContent = "Password matched";
                passwordMessage.classList.remove('text-danger');
                passwordMessage.classList.add('text-success');
                passwordMessage.style.fontStyle = 'italic';
                document.getElementById('<%= Txt_Confirm_Password.ClientID %>').classList.remove('is-invalid');
            }
            else {
                passwordMessage.textContent = "Password did not match!";
                passwordMessage.classList.remove('text-success');
                passwordMessage.classList.add('text-danger');
                passwordMessage.style.fontStyle = 'italic';
                document.getElementById('<%= Txt_Confirm_Password.ClientID %>').classList.add('is-invalid');
                isValid = false;
            }

            return isValid;
        }


        function validatePasswordCustom(source, args) {
            args.IsValid = validatePassword();
        }

    </script>

</asp:Content>

