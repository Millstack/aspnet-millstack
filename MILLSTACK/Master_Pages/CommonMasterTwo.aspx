<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="CommonMasterTwo.aspx.cs" Inherits="Master_Pages_CommonMasterTwo" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">


    <asp:Literal ID="Main_Column_1_Text" runat="server"></asp:Literal>
    <asp:Literal ID="Main_Column_2_Text" runat="server"></asp:Literal>
    <asp:Literal ID="Main_Column_3_Text" runat="server"></asp:Literal>
    <asp:Literal ID="Foreign_Key_Text" runat="server"></asp:Literal>


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



        </div>
    </div>
    <!-- UI Ends -->

</asp:Content>

