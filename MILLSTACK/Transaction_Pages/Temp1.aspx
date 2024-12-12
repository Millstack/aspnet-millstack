<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Temp1.aspx.cs" Inherits="Transaction_Pages_Temp1" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">

    <!-- Heading -->
    <div class="col-md-11 mx-auto fw-normal fs-3 fw-bold ps-0 pb-2 text-dark-emphasis mt-1 mb-1 text-center">
        <asp:Literal ID="Page_Heading" Text="Customer Creation" runat="server"></asp:Literal>
    </div>

    <!-- Customer Details Starts -->
    <div id="Div_UI" runat="server" class="card col-md-11 mx-auto mt-1 py-2 shadow rounded-3">
        <div class="card-body">

            <!-- Heading - BG -->
            <div class="fs-5 fw-medium text-white border border-dark-subtle shadow rounded-2 text-left py-2 px-3 mb-3" style="background-color: #0f3f6f !important;">
                <asp:Literal ID="Main_Heading_1" Text="Customer Details" runat="server"></asp:Literal>
            </div>

            <!-- Row 1 Starts -->
            <div class="row mb-2" style="border-style: solid none none none; border-width: 1px; border-color: #d6d5d5; padding-top: 10px; padding-bottom: 10px; margin-top: 10px; margin-bottom: 10px;">

                <!-- DropDown: Customer Type -->
                <div class="col-md-4 mb-3 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Lit_UserName" runat="server" Text="Customer Type"></asp:Literal>
                    </div>
                    <asp:DropDownList ID="DD_Customer_Type" runat="server" Width="100%" CssClass="form-control chosen-dropdown">
                    </asp:DropDownList>
                </div>

                <!-- DropDown: Customer Gender -->
                <div class="col-md-4 mb-3 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Literal1" runat="server" Text="Customer Gender"></asp:Literal>
                    </div>
                    <asp:DropDownList ID="DD_Gender" runat="server" Width="100%" CssClass="form-control chosen-dropdown">
                    </asp:DropDownList>
                </div>

                <!-- DropDown: Assembly -->
                <div class="col-md-4 mb-3 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Literal2" runat="server" Text="Assembly"></asp:Literal>
                    </div>
                    <asp:DropDownList ID="DD_Assembly" runat="server" Width="100%" CssClass="form-control chosen-dropdown">
                    </asp:DropDownList>
                </div>

            </div>

        </div>
    </div>

</asp:Content>

