<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Customer_Master.aspx.cs" Inherits="Transaction_Pages_Customer_Master" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">


    <!-- JavaScript -->
    <script src="<%= ResolveUrl("../assets/components/chosen_v1.8.7/chosen.jquery.min.js") %>"></script>
    <script src="<%= ResolveUrl("../assets/components/sumo-select/jquery.sumoselect.min_v3.0.3.js") %>"></script>

    <!-- DataTables JS -->
    <script src="<%= ResolveUrl("../assets/components/datatables/datatables.min.js") %>"></script>
    <script src="<%= ResolveUrl("../assets/components/datatables/Buttons-2.4.2/js/dataTables.buttons.min.js") %>"></script>
    <script src="<%= ResolveUrl("../assets/components/datatables/buttons.flash.min.js") %>"></script>
    <script src="<%= ResolveUrl("../assets/components/datatables/JSZip-3.10.1/jszip.min.js") %>"></script>
    <script src="<%= ResolveUrl("../assets/components/datatables/Buttons-2.4.2/js/buttons.html5.min.js") %>"></script>
    <script src="<%= ResolveUrl("../assets/components/datatables/Buttons-2.4.2/js/buttons.print.min.js") %>"></script>

    <style>
        /* From Uiverse.io by mobinkakei */
        .switch-holder {
            display: flex;
            padding: 2px 10px;
            border-radius: 6px;
            /*box-shadow: -8px -8px 15px rgba(255, 255, 255, .7), 10px 10px 10px rgba(0, 0, 0, .2), inset 8px 8px 15px rgba(255, 255, 255, .7), inset 10px 10px 10px rgba(0, 0, 0, .2);*/
            justify-content: space-between;
            align-items: center;
        }

        .switch-label {
            padding: 0 20px 0 10px
        }

            .switch-label i {
                margin-right: 5px;
            }

        .switch-toggle {
            height: 40px;
        }

            .switch-toggle input[type="checkbox"] {
                position: absolute;
                opacity: 0;
                z-index: -2;
            }

                .switch-toggle input[type="checkbox"] + label {
                    position: relative;
                    display: inline-block;
                    width: 100px;
                    height: 40px;
                    border-radius: 20px;
                    margin: 0;
                    cursor: pointer;
                    box-shadow: inset -8px -8px 15px rgba(255, 255, 255, .6), inset 10px 10px 10px rgba(0, 0, 0, .25);
                }

                    .switch-toggle input[type="checkbox"] + label::before {
                        position: absolute;
                        content: 'NO';
                        color: #fff;
                        font-size: 13px;
                        text-align: center;
                        line-height: 25px;
                        top: 8px;
                        left: 8px;
                        width: 45px;
                        height: 25px;
                        border-radius: 20px;
                        /*background-color: #eeeeee;*/
                        background-color: #e76363;
                        box-shadow: -3px -3px 5px rgba(255, 255, 255, .5), 3px 3px 5px rgba(0, 0, 0, .25);
                        transition: .3s ease-in-out;
                    }

                .switch-toggle input[type="checkbox"]:checked + label::before {
                    left: 50%;
                    content: 'YES';
                    color: #fff;
                    background-color: #00b33c;
                    box-shadow: -3px -3px 5px rgba(255, 255, 255, .5), 3px 3px 5px #00b33c;
                }

        /* Ensure that the ASP.NET CheckBox is styled correctly */
        .custom-checkbox {
            position: absolute;
            opacity: 0;
            z-index: -2;
        }
    </style>

    <!-- Heading -->
    <div class="col-md-11 mx-auto fw-normal fs-3 fw-bold ps-0 pb-2 text-dark-emphasis mt-1 mb-1 text-center">
        <asp:Literal ID="Page_Heading" Text="Customer Creation" runat="server"></asp:Literal>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>


            <!-- Customer Details Starts -->
            <div id="Div_UI" runat="server" class="card col-md-11 mx-auto mt-1 py-2 shadow rounded-3">
                <div class="card-body">

                    <!-- Heading - BG -->
                    <div class="fs-5 fw-medium text-white border border-dark-subtle shadow rounded-2 text-left py-2 px-3 mb-3" style="background-color: #0f3f6f !important;">
                        <asp:Literal ID="Main_Heading_1" Text="Customer Details" runat="server"></asp:Literal>
                    </div>

                    <!-- Row 1 Starts -->
                    <div class="row mb-2" style="border-style: solid none none none; border-width: 1px; border-color: #d6d5d5; padding-top: 10px; padding-bottom: 10px; margin-top: 10px; margin-bottom: 10px;">

                        <!-- TetxtBox: Customer Name -->
                        <div class="col-md-6 mb-3 align-self-end">
                            <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                                <asp:Literal ID="TxtID1" runat="server" Text="">Customer Name<em style="color: red">*</em>
                                </asp:Literal>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                    ControlToValidate="Txt_Customer_Name" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                                </asp:RequiredFieldValidator>
                            </div>
                            <asp:TextBox runat="server" ID="Txt_Customer_Name" type="text" Enabled="true" min="0" MaxLength="500"
                                CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">
                            </asp:TextBox>
                            <ajax:FilteredTextBoxExtender
                                ID="FilteredTextBoxExtender1"
                                runat="server"
                                TargetControlID="Txt_Customer_Name"
                                FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                                ValidChars=" " />
                        </div>

                        <!-- TetxtBox: Customer Mobile No -->
                        <div class="col-md-3 mb-3 align-self-end">
                            <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                                <asp:Literal ID="Literal1" runat="server">Customer Mobile No<em style="color: red">*</em>
                                </asp:Literal>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                    ControlToValidate="Txt_Customer_Mobile" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator
                                    ControlToValidate="Txt_Customer_Mobile"
                                    ValidationExpression="^\d{10}$"
                                    ErrorMessage="Please enter a valid 10-digit mobile number."
                                    Display="Dynamic"
                                    ForeColor="Red"
                                    runat="server"
                                    EnableClientScript="false" />
                            </div>
                            <asp:TextBox runat="server" ID="Txt_Customer_Mobile" TextMode="Number" min="0" step="1" MaxLength="10" Enabled="true"
                                CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">
                            </asp:TextBox>
                            <ajax:FilteredTextBoxExtender
                                ID="FilteredTextBoxExtender2"
                                runat="server"
                                TargetControlID="Txt_Customer_Mobile"
                                FilterType="Numbers"
                                ValidChars="" />

                        </div>

                        <!-- DropDown: Gender -->
                        <div class="col-md-3 mb-3 align-self-end">
                            <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                                <asp:Literal ID="Foreign_Table_2_Key_Text" runat="server" Text="">Gender<em style="color: red">*</em>
                                </asp:Literal>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                    ControlToValidate="DD_Gender" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                                </asp:RequiredFieldValidator>
                            </div>
                            <asp:DropDownList ID="DD_Gender" runat="server" Width="100%" CssClass="form-control chosen-dropdown"
                                AutoPostBack="false">
                            </asp:DropDownList>
                        </div>

                        <!-- TetxtBox: List Number -->
                        <div class="col-md-3 mb-3 align-self-end">
                            <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                                <asp:Literal ID="Literal2" runat="server">List Number<em style="color: red">*</em>
                                </asp:Literal>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                    ControlToValidate="Txt_List_No" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                                </asp:RequiredFieldValidator>
                            </div>
                            <asp:TextBox runat="server" ID="Txt_List_No" TextMode="Number" Enabled="true" min="0" MaxLength="500"
                                CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">
                            </asp:TextBox>
                            <ajax:FilteredTextBoxExtender
                                ID="FilteredTextBoxExtender3"
                                runat="server"
                                TargetControlID="Txt_List_No"
                                FilterType="Numbers"
                                ValidChars=" " />
                        </div>

                        <!-- TetxtBox: Serial Number -->
                        <div class="col-md-3 mb-3 align-self-end">
                            <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                                <asp:Literal ID="Literal3" runat="server">Serial Number<em style="color: red">*</em>
                                </asp:Literal>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                    ControlToValidate="Txt_Serial_No" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                                </asp:RequiredFieldValidator>
                            </div>
                            <asp:TextBox runat="server" ID="Txt_Serial_No" TextMode="Number" Enabled="true" min="0" MaxLength="500"
                                CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">
                            </asp:TextBox>
                            <ajax:FilteredTextBoxExtender
                                ID="FilteredTextBoxExtender4"
                                runat="server"
                                TargetControlID="Txt_Serial_No"
                                FilterType="Numbers"
                                ValidChars=" " />
                        </div>

                        <!-- TetxtBox: WRN Number -->
                        <div class="col-md-3 mb-3 align-self-end">
                            <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                                <asp:Literal ID="Literal4" runat="server">WRN Number<em style="color: red">*</em>
                                </asp:Literal>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                    ControlToValidate="Txt_WRN_No" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                                </asp:RequiredFieldValidator>
                            </div>
                            <asp:TextBox runat="server" ID="Txt_WRN_No" TextMode="Number" Enabled="true" min="0" MaxLength="500"
                                CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">
                            </asp:TextBox>
                            <ajax:FilteredTextBoxExtender
                                ID="FilteredTextBoxExtender5"
                                runat="server"
                                TargetControlID="Txt_WRN_No"
                                FilterType="Numbers"
                                ValidChars=" " />
                        </div>

                        <!-- DropDown: Customer Type -->
                        <div class="col-md-3 mb-3 align-self-end">
                            <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                                <asp:Literal ID="Literal5" runat="server" Text="">Customer Type<em style="color: red">*</em>
                                </asp:Literal>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                    ControlToValidate="DD_Customer_Type" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                                </asp:RequiredFieldValidator>
                            </div>
                            <asp:DropDownList ID="DD_Customer_Type" runat="server" Width="100%" CssClass="form-control chosen-dropdown"
                                AutoPostBack="false">
                            </asp:DropDownList>
                        </div>

                        <!-- TetxtBox: Voting Booth -->
                        <div class="col-md-3 mb-3 align-self-end">
                            <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                                <asp:Literal ID="Literal7" runat="server" Text="">Voting Booth<em style="color: red">*</em>
                                </asp:Literal>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                    ControlToValidate="Txt_Voting_Booth" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                                </asp:RequiredFieldValidator>
                            </div>
                            <asp:TextBox runat="server" ID="Txt_Voting_Booth" TextMode="Number" Enabled="true" min="0" MaxLength="500"
                                CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">
                            </asp:TextBox>
                            <ajax:FilteredTextBoxExtender
                                ID="FilteredTextBoxExtender7"
                                runat="server"
                                TargetControlID="Txt_Voting_Booth"
                                FilterType="Numbers"
                                ValidChars="" />
                        </div>

                        <!-- TetxtBox: Voting Room -->
                        <div class="col-md-3 mb-3 align-self-end">
                            <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                                <asp:Literal ID="Literal6" runat="server" Text="">Voting Room<em style="color: red">*</em>
                                </asp:Literal>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                    ControlToValidate="Txt_Voting_Room" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                                </asp:RequiredFieldValidator>
                            </div>
                            <asp:TextBox runat="server" ID="Txt_Voting_Room" TextMode="Number" Enabled="true" min="0" MaxLength="500"
                                CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">
                            </asp:TextBox>
                            <ajax:FilteredTextBoxExtender
                                ID="FilteredTextBoxExtender6"
                                runat="server"
                                TargetControlID="Txt_Voting_Room"
                                FilterType="Numbers"
                                ValidChars="" />
                        </div>

                        <!-- TetxtBox: Data Entry Mode -->
                        <div class="col-md-3 mb-3 align-self-end">
                            <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                                <asp:Literal ID="Literal8" runat="server" Text="">Data Entry Mode<em style="color: red">*</em>
                                </asp:Literal>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                    ControlToValidate="Txt_Data_Entry_Mode" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                                </asp:RequiredFieldValidator>
                            </div>
                            <asp:TextBox runat="server" ID="Txt_Data_Entry_Mode" Enabled="false" min="0" MaxLength="500"
                                CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1 text-secondary">
                            </asp:TextBox>
                            <ajax:FilteredTextBoxExtender
                                ID="FilteredTextBoxExtender8"
                                runat="server"
                                TargetControlID="Txt_Data_Entry_Mode"
                                FilterType="UppercaseLetters, LowercaseLetters"
                                ValidChars="" />
                        </div>

                        <!-- TetxtBox: Data Entry Mode -->
                        <!-- From Uiverse.io by mobinkakei -->
                        <div class="col-md-3 mb-3 align-self-end">
                            <div class="switch-holder border border-dark-subtle">
                                <div class="switch-label text-dark-emphasis">
                                    <i class="fa fa-bluetooth-b"></i><span>Customer Done</span>
                                </div>
                                <div class="switch-toggle">
                                    <%--<input type="checkbox" id="bluetooth">--%>
                                    <input type="checkbox" id="bluetooth" runat="server" clientidmode="Static" />
                                    <label for="bluetooth"></label>
                                </div>
                            </div>
                        </div>

                    </div>
                    <!-- Row 1 Ends -->

                </div>
            </div>
            <!-- Customer Details Ends -->

            <!-- Customer Location Starts -->
            <div id="Div1" runat="server" class="card col-md-11 mx-auto mt-4 py-2 shadow rounded-3">
                <div class="card-body">

                    <!-- Heading - BG -->
                    <div class="fs-5 fw-medium text-white border border-dark-subtle shadow rounded-2 text-left py-2 px-3 mb-3" style="background-color: #0f3f6f !important;">
                        <asp:Literal ID="Literal9" Text="Customer Location Details" runat="server"></asp:Literal>
                    </div>

                    <!-- Row 1 Starts -->
                    <div class="row mb-2" style="border-style: solid none none none; border-width: 1px; border-color: #d6d5d5; padding-top: 10px; padding-bottom: 10px; margin-top: 10px; margin-bottom: 10px;">

                        <!-- DropDown: Assembly -->
                        <div class="col-md-4 mb-3 align-self-end">
                            <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                                <asp:Literal ID="Literal10" runat="server" Text="">Assembly
                                <em style="color: red">*</em>
                                </asp:Literal>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                        ControlToValidate="DD_Assembly" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                                    </asp:RequiredFieldValidator>
                            </div>
                            <asp:DropDownList ID="DD_Assembly" runat="server" Width="100%" CssClass="form-control chosen-dropdown"
                                OnSelectedIndexChanged="DD_Assembly_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <!-- DropDown: Ward -->
                        <div class="col-md-4 mb-3 align-self-end">
                            <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                                <asp:Literal ID="Literal11" runat="server" Text="">Ward
                            <em style="color: red">*</em>
                                </asp:Literal>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                        ControlToValidate="DD_Ward" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                                    </asp:RequiredFieldValidator>
                            </div>
                            <asp:DropDownList ID="DD_Ward" runat="server" Width="100%" CssClass="form-control chosen-dropdown"
                                OnSelectedIndexChanged="DD_Ward_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <!-- DropDown: Sector -->
                        <div class="col-md-4 mb-3 align-self-end">
                            <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                                <asp:Literal ID="Literal12" runat="server" Text="">Sector
                            <em style="color: red">*</em>
                                </asp:Literal>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                        ControlToValidate="DD_Sector" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                                    </asp:RequiredFieldValidator>
                            </div>
                            <asp:DropDownList ID="DD_Sector" runat="server" Width="100%" CssClass="form-control chosen-dropdown"
                                OnSelectedIndexChanged="DD_Sector_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <!-- DropDown: Society -->
                        <div class="col-md-12 mb-3 align-self-end">
                            <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                                <asp:Literal ID="Literal13" runat="server" Text="">Society
                            <em style="color: red">*</em>
                                </asp:Literal>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                        ControlToValidate="DD_Society" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                                    </asp:RequiredFieldValidator>
                            </div>
                            <asp:DropDownList ID="DD_Society" runat="server" Width="100%" CssClass="form-control chosen-dropdown"
                                AutoPostBack="false">
                            </asp:DropDownList>
                        </div>

                    </div>
                    <!-- Row 1 Ends -->

                </div>
            </div>
            <!-- Customer Location Ends -->

            <!-- Submit Button UI Starts -->
            <div class="col-md-11 mx-auto row mt-5 mb-2 align-self-end">
                <div class="col-md-6 text-start">
                    <asp:Button
                        ID="Btn_Back"
                        runat="server"
                        Text="Back"
                        OnClick="Btn_Back_Click"
                        CssClass="btn col-md-2 text-white shadow rounded-0"
                        Style="background: #0f3f6f; color: #fff" />
                </div>
                <div class="col-md-6 text-end">
                    <asp:Button
                        ID="Btn_Submit"
                        runat="server"
                        Text="Save"
                        OnClick="Btn_Submit_Click"
                        ValidationGroup="finalSubmit"
                        CssClass="btn col-md-2 text-white shadow rounded-0"
                        Style="background: #0f3f6f; color: #fff" />
                </div>
            </div>
            <!-- Submit Button UI Ends -->

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

