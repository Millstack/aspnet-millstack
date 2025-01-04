<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Customer_Attendance.aspx.cs" Inherits="Transaction_Pages_Customer_Attendance" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">


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
                        content: 'Absent';
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
                    content: 'Present';
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
        <asp:Literal ID="Page_Heading" Text="Booth Details" runat="server"></asp:Literal>
    </div>

    <!-- Attendance Details Starts -->
    <div id="Div_UI" runat="server" class="card col-sm-10 mx-auto py-0 px-0 shadow rounded-3">
        <div class="card-body row">

            <!-- TetxtBox: Customer Name -->
            <div class="col-md-6 mb-3 align-self-end">
                <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                    <asp:Literal ID="TxtID1" runat="server" Text="">Customer Name
                    </asp:Literal>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                        ControlToValidate="Txt_Customer_Name" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                    </asp:RequiredFieldValidator>
                </div>
                <asp:TextBox runat="server" ID="Txt_Customer_Name" type="text" Enabled="false" min="0" MaxLength="500"
                    CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">
                </asp:TextBox>
                <ajax:FilteredTextBoxExtender
                    ID="FilteredTextBoxExtender1"
                    runat="server"
                    TargetControlID="Txt_Customer_Name"
                    FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                    ValidChars=" " />
            </div>

            <!-- TetxtBox: WRN Number -->
            <div class="col-md-6 mb-3 align-self-end">
                <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                    <asp:Literal ID="Literal4" runat="server">WRN Number
                    </asp:Literal>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                        ControlToValidate="Txt_WRN_No" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                    </asp:RequiredFieldValidator>
                </div>
                <asp:TextBox runat="server" ID="Txt_WRN_No" TextMode="Number" Enabled="false" min="0" MaxLength="500"
                    CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">
                </asp:TextBox>
                <ajax:FilteredTextBoxExtender
                    ID="FilteredTextBoxExtender5"
                    runat="server"
                    TargetControlID="Txt_WRN_No"
                    FilterType="Numbers"
                    ValidChars=" " />
            </div>

            <!-- TetxtBox: List Number -->
            <div class="col-md-6 mb-3 align-self-end">
                <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                    <asp:Literal ID="Literal2" runat="server">List Number
                    </asp:Literal>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                        ControlToValidate="Txt_List_No" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                    </asp:RequiredFieldValidator>
                </div>
                <asp:TextBox runat="server" ID="Txt_List_No" TextMode="Number" Enabled="false" min="0" MaxLength="500"
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
            <div class="col-md-6 mb-3 align-self-end">
                <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                    <asp:Literal ID="Literal3" runat="server">Serial Number
                    </asp:Literal>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                        ControlToValidate="Txt_Serial_No" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                    </asp:RequiredFieldValidator>
                </div>
                <asp:TextBox runat="server" ID="Txt_Serial_No" TextMode="Number" Enabled="false" min="0" MaxLength="500"
                    CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">
                </asp:TextBox>
                <ajax:FilteredTextBoxExtender
                    ID="FilteredTextBoxExtender4"
                    runat="server"
                    TargetControlID="Txt_Serial_No"
                    FilterType="Numbers"
                    ValidChars=" " />
            </div>

            <!-- TetxtBox: Voting Booth -->
            <div class="col-md-6 mb-3 align-self-end">
                <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                    <asp:Literal ID="Literal7" runat="server" Text="">Voting Booth
                    </asp:Literal>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                        ControlToValidate="Txt_Voting_Booth" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                    </asp:RequiredFieldValidator>
                </div>
                <asp:TextBox runat="server" ID="Txt_Voting_Booth" TextMode="Number" Enabled="true" min="0" MaxLength="500"
                    CssClass="form-control border border-secondary-subtle bg-light text-secondary rounded-1 fs-6 fw-bold py-1 shadow-sm">
                </asp:TextBox>
                <ajax:FilteredTextBoxExtender
                    ID="FilteredTextBoxExtender7"
                    runat="server"
                    TargetControlID="Txt_Voting_Booth"
                    FilterType="Numbers"
                    ValidChars="" />
            </div>

            <!-- TetxtBox: Voting Room -->
            <div class="col-md-6 mb-3 align-self-end">
                <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                    <asp:Literal ID="Literal6" runat="server" Text="">Voting Room
                    </asp:Literal>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                        ControlToValidate="Txt_Voting_Room" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                    </asp:RequiredFieldValidator>
                </div>
                <asp:TextBox runat="server" ID="Txt_Voting_Room" TextMode="Number" Enabled="true" min="0" MaxLength="500"
                    CssClass="form-control border border-secondary-subtle bg-light text-secondary rounded-1 fs-6 fw-bold py-1 shadow-sm">
                </asp:TextBox>
                <ajax:FilteredTextBoxExtender
                    ID="FilteredTextBoxExtender6"
                    runat="server"
                    TargetControlID="Txt_Voting_Room"
                    FilterType="Numbers"
                    ValidChars="" />
            </div>

            <!-- TetxtBox: Data Entry Mode -->
            <!-- From Uiverse.io by mobinkakei -->
            <div class="col-md-6 mb-3 align-self-end">
                <div class="switch-holder border border-dark-subtle">
                    <div class="switch-label text-dark-emphasis">
                        <i class="fa fa-bluetooth-b"></i><span>Customer Attendance Status</span>
                    </div>
                    <div class="switch-toggle">
                        <%--<input type="checkbox" id="bluetooth">--%>
                        <input type="checkbox" id="bluetooth" runat="server" clientidmode="Static" />
                        <label for="bluetooth"></label>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <!-- Attendance Details Ends -->


    <!-- Submit Button UI Starts -->
    <div class="col-md-11 mx-auto row mt-5 mb-2 align-self-end justify-content-center">
        <asp:Button
            ID="Btn_Back"
            runat="server"
            Visible="true"
            Text="Back"
            OnClick="Btn_Back_Click"
            CssClass="btn col-md-1 text-white shadow rounded-0"
            Style="background: #0f3f6f; color: #fff" />
        <asp:Button
            ID="Btn_Submit"
            runat="server"
            Text="Update"
            OnClick="Btn_Submit_Click"
            ValidationGroup="finalSubmit"
            CssClass="btn col-md-1 text-white shadow rounded-0 ms-2"
            Style="background: #0f3f6f; color: #fff" />
    </div>
    <!-- Submit Button UI Ends -->


</asp:Content>

