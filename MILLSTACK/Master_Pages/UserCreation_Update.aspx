<%@ Page Title="" Language="C#" Async="true" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="UserCreation_Update.aspx.cs" Inherits="Master_Pages_UserCreation_Update" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">


    <!-- Heading -->
    <div class="col-md-11 mx-auto fw-normal fs-3 fw-bold ps-0 pb-2 text-dark-emphasis mt-1 mb-1 text-center">
        <asp:Literal ID="Page_Heading" Text="User Master" runat="server"></asp:Literal>
    </div>

    <!-- Control Starts -->
    <div id="Div_Control" runat="server" class="card col-md-11 mx-auto mt-1 py-2 shadow rounded-3 text-dark-emphasis">
        <div class="card-body">

            <!-- Control Section Starts -->
            <div class="row mb-2">

                <!-- DropDown: User ID & Full Name -->
                <div class="col-md-6 mb-3 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Lit_UserName" runat="server" Text="">User Full Name</asp:Literal>
                    </div>
                    <asp:DropDownList ID="DD_User_ID_FullName" runat="server" Width="100%" CssClass="form-control chosen-dropdown">
                    </asp:DropDownList>
                </div>

                <!-- DropDown: UserName -->
                <div class="col-md-6 mb-3 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Literal1" runat="server" Text="">UserName</asp:Literal>
                    </div>
                    <asp:DropDownList ID="DD_UserName" runat="server" Width="100%" CssClass="form-control chosen-dropdown">
                    </asp:DropDownList>
                </div>

                <!-- DropDown: Designation -->
                <div class="col-md-6 mb-3 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Literal2" runat="server" Text="">User Designation</asp:Literal>
                    </div>
                    <asp:DropDownList ID="DD_Designation" runat="server" Width="100%" CssClass="form-control chosen-dropdown">
                    </asp:DropDownList>
                </div>

            </div>
            <!-- Control Section Ends -->

            <!-- Submit Button UI Starts -->
            <div class="row mt-5 mb-2 align-self-end">
                <div class="col-md-8 mb-1 fw-semibold fs-6 align-middle text-center"></div>
                <div class="col-md-2 text-end">
                    <asp:Button ID="Btn_Reset" runat="server" Text="Reset" OnClick="Btn_Reset_Click"
                        CssClass="btn btn-dark col-md-8 text-white shadow rounded-0" />
                </div>
                <div class="col-md-2 text-start">
                    <asp:Button ID="Btn_Search" runat="server" Text="Search" OnClick="Btn_Search_Click"
                        CssClass="btn btn-dark col-md-8 text-white shadow rounded-0" />
                </div>
            </div>
            <!-- Submit Button UI Ends -->

        </div>
    </div>
    <!-- Control UI Ends -->

</asp:Content>

