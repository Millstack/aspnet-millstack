<%@ Page Title="" Language="C#" Async="true" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Master_Pages_Dashboard" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">

    <!-- Heading -->
    <div class="col-md-11 mx-auto fw-normal fs-3 fw-bold ps-0 pb-2 text-dark-emphasis mt-1 mb-1 text-center">
        <asp:Literal ID="Page_Heading" Text="Dashbaord" runat="server"></asp:Literal>
    </div>

    <!-- UI Starts -->
    <div id="Div_Control" runat="server" class="card col-md-11 mx-auto mt-1 py-2 shadow rounded-3">
        <div class="card-body">

            <!-- Row 1 Starts -->
            <div class="row mb-2">

                <!-- User Master -->
                <div class="col-md-4 mb-3 align-self-end rounded-3">
                    <div class="mb-1 fw-normal fs-6">
                        <p>Users Count</p>
                        <h3 class="text-white1"><%#DataBinder.Eval(Container,"DataItem.TotalProject")%></h3>
                        <hr class="bg-white" />
                        <h3 class="text-white1">&#8377 : <%#DataBinder.Eval(Container,"DataItem.TotalProjectAmt")%></h3>
                    </div>
                </div>
                <!-- Row 1 Ends -->

            </div>
        </div>
    </div>
    <!-- UI Ends -->
</asp:Content>

