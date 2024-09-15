<%@ Page Title="" Language="C#" Async="true" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Master_Pages_Dashboard" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">

    <style>
        body {
            background-color: #333 !important;
            color: #3D7EAE !important;
        }

        .navbar, .navbar a {
            color: #D3D3D3 !important; /* Ensure the navbar text is gray */
        }


        /* .dark-theme .text-dark,
        .dark-theme .text-dark-emphasis,
        .dark-theme .text-body-secondary,
        .dark-theme .text-body-tertiary {
            background-color: #333 !important;
            color: #D3D3D3 !important;
        }*/
    </style>

    <!-- Heading -->
    <div class="col-md-11 mx-auto fw-normal fs-3 fw-bold ps-0 pb-2 text-dark-emphasis mt-1 mb-1 text-center">
        <asp:Literal ID="Page_Heading" Text="Dashboard" runat="server"></asp:Literal>
    </div>

    <!-- UI Starts -->
    <div class="card col-md-12 shadow rounded-3">
        <div class="card-body">

            <%--<div class="row">
                <asp:Repeater ID="Repeater_Dashboard" runat="server">
                    <ItemTemplate>

                        <!-- KPI: Division -->
                        <div class="col-md-4 mb-3 align-self-end rounded-3 bg-dark-subtle ">
                            <div class="fw-normal fs-6">
                                <p><%# Eval("DivisionName") %></p>
                                <hr class="bg-white" />
                                <h3 class="text-white1">District Count: <%# Eval("DistrictCount") %></h3>
                            </div>
                        </div>

                    </ItemTemplate>
                </asp:Repeater>
            </div>--%>


            <div class="row ">
            </div>

        </div>
    </div>
    <!-- UI Ends -->


</asp:Content>

