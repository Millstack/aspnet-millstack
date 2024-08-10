<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="CommonMasterTwo.aspx.cs" Inherits="Master_Pages_CommonMasterTwo" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">


    <!-- Heading -->
    <div class="col-md-11 mx-auto fw-normal fs-3 fw-bold ps-0 pb-2 text-dark-emphasis mt-1 mb-1 text-center">
        <asp:Literal ID="Page_Heading" Text="" runat="server"></asp:Literal>
    </div>

    <!-- UI Starts -->
    <div class="card col-md-11 mx-auto mt-1 py-2 shadow rounded-3">
        <div class="card-body">

            <!-- Heading - BG -->
            <div class="fs-5 fw-medium text-white border border-dark-subtle bg-primary shadow rounded-2 text-center py-2 mb-3">
                <asp:Literal Text="" runat="server"></asp:Literal>
            </div>

            <!-- Control Section Starts -->
            <div class="row mb-2">

                <!-- DD: Foreign Table Column Name -->
                <div class="col-md-12 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Foreign_Key_Text" runat="server" Text=""><em style="color: red">*</em></asp:Literal>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                ControlToValidate="DD_Foriegn_Column" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <asp:DropDownList ID="DD_Foriegn_Column" runat="server" Width="100%" CssClass="form-control rounded-0"
                        OnSelectedIndexChanged="" AutoPostBack="false">
                    </asp:DropDownList>
                </div>

                <!-- Tetxbox: Main Column 1 Input -->
                <div class="col-md-4 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Main_Column_1_Text" runat="server" Text=""><em style="color: red">*</em></asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Input_Main_Column_1" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Input_Main_Column_1" type="text" Enabled="false" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="Input_Main_Column_1"
                        FilterType="Numbers, UppercaseLetters, LowercaseLetters" ValidChars=" " />
                </div>

                <!-- Tetxbox: Main Column 2 Input -->
                <div class="col-md-4 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Main_Column_2_Text" runat="server" Text=""><em style="color: red">*</em></asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Input_Main_Column_2" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Input_Main_Column_2" type="text" Enabled="false" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="Input_Main_Column_1"
                        FilterType="Numbers, UppercaseLetters, LowercaseLetters" ValidChars=" " />
                </div>

                <!-- Tetxbox: Main Column 3 Input -->
                <div class="col-md-4 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Main_Column_3_Text" runat="server" Text=""><em style="color: red">*</em></asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Input_Main_Column_3" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Input_Main_Column_3" type="text" Enabled="false" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="Input_Main_Column_1"
                        FilterType="Numbers, UppercaseLetters, LowercaseLetters" ValidChars=" " />
                </div>

            </div>
            <!-- Control Section Ends -->

            <!-- Submit Button UI Starts -->
            <div class="row mt-5 mb-2">
                <div class="col-md-3 text-start"></div>
                <div class="col-md-3 text-start"></div>
                <div class="col-md-3 text-start">
                    <asp:Button ID="Btn_Back" runat="server" Text="Back" OnClick="Btn_Back_Click"
                        CssClass="btn col-md-3 text-white shadow rounded-0" Style="background: #4800ff; color: #fff" />
                </div>
                <div class="col-md-3 text-end">
                    <asp:Button ID="Btn_Submit" runat="server" Text="Save" OnClick="Btn_Submit_Click" ValidationGroup="finalSubmit"
                        CssClass="btn col-md-3 text-white shadow rounded-0" Style="background: #4800ff; color: #fff" />
                </div>
            </div>
            <!-- Submit Button UI Ends -->

        </div>
    </div>
    <!-- UI Ends -->

</asp:Content>

