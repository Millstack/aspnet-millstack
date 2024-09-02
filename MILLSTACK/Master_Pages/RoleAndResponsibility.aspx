<%@ Page Title="" Language="C#" Async="true" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="RoleAndResponsibility.aspx.cs" Inherits="Master_Pages_RoleAndResponsibility" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">

    <style type="text/css">
        .btn-custom {
            background-color: #4f64cc;
            color: white;
            font-size: 14px;
            font-family: Arial, sans-serif;
            font-weight: normal;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= TreeView1.ClientID %> input[type="checkbox"]').change(function () {
                var isChecked = $(this).is(':checked');
                var tableRow = $(this).closest('tr');

                // If a child node is checked, check its parents
                if (isChecked) {
                    checkParents(tableRow);
                }
            });

            function checkParents(tableRow) {
                // Find the previous table that contains the parent node
                var parentTable = tableRow.closest('div').prevAll('table').first();

                // Check if there is a parent node
                if (parentTable.length > 0) {
                    var parentCheckbox = parentTable.find('input[type="checkbox"]').first();
                    parentCheckbox.prop('checked', true);

                    // Recursively check all parent nodes
                    checkParents(parentTable.closest('tr'));
                }
            }
        });
    </script>

    <!-- Heading -->
    <div class="col-md-11 mx-auto fw-normal fs-3 fw-bold ps-0 pb-2 text-dark mt-1 mb-1 text-center">
        <asp:Literal ID="heading_Project" Text="Role & Responsibilities" runat="server"></asp:Literal>
    </div>


    <!-- Header UI Starts -->
    <div class="card col-md-11 mx-auto mt-1 py-2 shadow rounded-3">
        <div class="card-body">

            <!-- Heading 1 -->
            <div class="fw-normal fs-5 fw-medium text-body-secondary border-bottom pb-2 mb-4">
                <asp:Literal Text="Role Details" runat="server"></asp:Literal>
            </div>

            <!-- 1st row Starts -->
            <div class="row mb-4">

                <!-- DD Roles -->
                <div class="col-md-6 text-body-tertiary align-self-end">
                    <div class="mb-1  fw-semibold fs-6">
                        <asp:Literal ID="Literal15" Text="Role" runat="server"></asp:Literal>
                    </div>
                    <asp:DropDownList ID="DD_User_Roles" Width="100%" OnSelectedIndexChanged="DD_User_Roles_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                </div>

            </div>
            <!-- 1st row Ends -->




            <!-- From Uiverse.io by guilhermeyohan -->
            <!-- https://uiverse.io/guilhermeyohan/great-snake-42 -->
            <div class="checkbox-apple-wrapper">
                <span class="checkbox-text text-body-tertiary">Select All</span>
                <div class="checkbox-apple">
                    <input class="yep" id="check-apple" type="checkbox">
                    <label for="check-apple"></label>
                </div>
            </div>



            <asp:TreeView ID="TreeView1" runat="server" Width="100%" Font-Size="12pt" ShowCheckBoxes="All" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged"
                ExpandDepth="FullyExpand" ShowExpandCollapse="true" ShowLines="True" NodeIndent="30" NodeStyle-Height="35px" CssClass="custom-treeview"
                Font-Names="Helvetica">
                <ParentNodeStyle Font-Bold="True" />
            </asp:TreeView>

            <hr />


            <!-- Save & Reset Buttons -->
            <div class="row mt-5 mb-2">
                <div class="col-md-6 text-start">
                    <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnEventClick_btnReset" CssClass="btn btn-custom text-white shadow mb-5" />
                </div>
                <div class="col-md-6 text-end">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="finalSubmit" CssClass="btn btn-custom text-white shadow mb-5" />
                </div>
            </div>

        </div>
    </div>


    <script type="text/javascript">
        $(document).ready(function () {
            $('#check-apple').change(function () {
                var isChecked = $(this).is(':checked');

                // Find all checkboxes within the TreeView and set their checked property
                $('#<%= TreeView1.ClientID %> input[type="checkbox"]').prop('checked', isChecked);
            });
        });
    </script>



</asp:Content>

