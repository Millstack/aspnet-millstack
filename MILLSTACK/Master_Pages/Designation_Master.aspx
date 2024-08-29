<%@ Page Title="" Language="C#" Async="true" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Designation_Master.aspx.cs" Inherits="Master_Pages_Designation_Master" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">


    <!-- Heading -->
    <div class="col-md-11 mx-auto fw-normal fs-3 fw-bold ps-0 pb-2 text-dark-emphasis mt-1 mb-1 text-center">
        <asp:Literal ID="Page_Heading" Text="Designation Master" runat="server"></asp:Literal>
    </div>

    <!-- Hierarchy TreeView Starts -->
    <div id="Div_Control" runat="server" class="card col-md-11 mx-auto mt-1 py-2 shadow rounded-3">
        <div class="card-body">


            <!-- Heading - BG -->
            <div class="fs-5 fw-medium text-white border border-dark-subtle shadow rounded-2 text-left py-2 px-3 mb-3" style="background-color: #0f3f6f !important;">
                <asp:Literal ID="Main_Heading_1" Text="Designation Details" runat="server"></asp:Literal>
            </div>

            <!-- Row 1 Starts -->
            <div class="row mb-2" style="border-style: solid none none none; border-width: 1px; border-color: #d6d5d5; padding-top: 10px; padding-bottom: 10px; margin-top: 10px; margin-bottom: 10px;">

                <!-- DropDown: Parent Designation -->
                <div class="col-md-12 mb-3 align-self-end">
                    <div class="mb-1 fw-normal fs-6">
                        <asp:Literal ID="Lit_Parent_Designation" runat="server" Text="">Parent Designation
                            <%--<em style="color: red">*</em>--%>
                        </asp:Literal>
                        <div>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                ControlToValidate="DD_Parent_Designation" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                            </asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                    <asp:DropDownList ID="DD_Parent_Designation" runat="server" Width="100%" CssClass="form-control chosen-dropdown"
                        AutoPostBack="false">
                    </asp:DropDownList>
                </div>

                <!-- TetxtBox: Designation Name -->
                <div class="col-md-6 mb-3 align-self-end">
                    <div class="mb-1 fw-normal fs-6">
                        <asp:Literal ID="TxtID1" runat="server" Text="">Designation Name
                            <em style="color: red">*</em>
                        </asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Txt_Designation_Name" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Txt_Designation_Name" type="text" Enabled="true" min="0" MaxLength="500"
                        CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="Txt_Designation_Name"
                        FilterType="Numbers, UppercaseLetters, LowercaseLetters, Custom" ValidChars=" ()" />
                </div>

                <!-- TetxtBox: Designation Code -->
                <div class="col-md-6 mb-3 align-self-end">
                    <div class="mb-1 fw-normal fs-6">
                        <asp:Literal ID="Literal1" runat="server" Text="">Designation Code
                            <em style="color: red">*</em>
                        </asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Txt_Designation_Code" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Txt_Designation_Code" type="text" Enabled="true" min="0" MaxLength="500"
                        CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="Txt_Designation_Code"
                        FilterType="Numbers, UppercaseLetters, LowercaseLetters, Custom" ValidChars=" " />
                </div>

                <!-- DropDown: Level Type -->
                <div class="col-md-6 mb-3 align-self-end">
                    <div class="mb-1 fw-normal fs-6">
                        <asp:Literal ID="Literal2" runat="server" Text="">Level Type
                            <em style="color: red">*</em>
                        </asp:Literal>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                ControlToValidate="DD_Level_Type" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <asp:DropDownList ID="DD_Level_Type" runat="server" Width="100%" CssClass="form-control chosen-dropdown"
                        AutoPostBack="false">
                    </asp:DropDownList>
                </div>

                <!-- Submit Button UI Starts -->
                <div class="col-md-6 row mb-3 align-self-end">
                    <div class="col-md-4 text-right">
                        <asp:Button ID="Btn_Submit" runat="server" Text="Save" OnClick="Btn_Submit_Click" ValidationGroup="finalSubmit"
                            CssClass="btn col-md-12 text-white shadow rounded-0" Style="background: #0f3f6f; color: #fff" />
                    </div>
                    <div class="col-md-4 text-center">
                        <asp:Button ID="Btn_Reset" runat="server" Text="Reset" OnClick="Btn_Reset_Click"
                            CssClass="btn col-md-12 text-white shadow rounded-0" Style="background: #0f3f6f; color: #fff" />
                    </div>
                    <div class="col-md-4 text-center">
                        <asp:LinkButton ID="Btn_Delete" runat="server" Text="Delete" OnClick="Btn_Delete_Click"
                            OnClientClick="javascript:return confirm ('Are you sure to Delete this record ? ')"
                            CssClass="btn col-md-12 text-white shadow rounded-0" Style="background: #ea523b; color: #fff">
                            <asp:Image ID="IMG_Delete" runat="server" ImageUrl="~/assets/image/delete-cut/delete.png" AlternateText="Edit" CssClass="ms-2"
                                ToolTip="Delete" Style="width: 25px; height: 25px;" />
                        </asp:LinkButton>
                    </div>
                </div>

            </div>
            <!-- Row 1 Ends -->


            <!-- Hierarchy TreeView Starts -->
            <div class="mt-3">
                <div class="col-md-11 border border-secondary-subtle mx-auto bg-white mt-2 p-4 fw-bold fs-5 py-2 ps-2 text-secondary rounded-top-3">
                    <asp:Literal ID="Literal3" runat="server" Text="">Designation (Hierarchy) Levels</asp:Literal>
                </div>
                <div class="col-md-11 border border-secondary-subtle mx-auto bg-light p-4 rounded-bottom-3">
                    <asp:TreeView ID="Hierarchy" runat="server"
                        BorderWidth="0px" EnableTheming="True" HoverNodeStyle-BorderWidth="0px"
                        ImageSet="Simple" LeafNodeStyle-BorderWidth="0px" NodeStyle-BorderWidth="0px"
                        ParentNodeStyle-BorderWidth="0px" RootNodeStyle-BorderWidth="0px"
                        ShowLines="True" OnSelectedNodeChanged="Hierarchy_SelectedNodeChanged" CssClass="fw-normal fs-6">
                        <LevelStyles>
                            <asp:TreeNodeStyle CssClass="text-body-secondary" />
                        </LevelStyles>
                        <ParentNodeStyle BorderWidth="0px" CssClass="" />
                        <HoverNodeStyle BorderWidth="0px" CssClass="fw-semibold text-dark-emphasis" />
                        <SelectedNodeStyle Font-Bold="True" CssClass="text-dark-emphasis" />
                        <RootNodeStyle BorderWidth="0px" CssClass="" />
                        <NodeStyle BorderWidth="0px" CssClass="text-body-secondary" />
                        <LeafNodeStyle BorderStyle="None" BorderWidth="0px" />
                    </asp:TreeView>
                </div>
                <!-- Hierarchy TreeView Ends -->

            </div>


        </div>
    </div>
    <!-- Hierarchy TreeView Ends -->



</asp:Content>

