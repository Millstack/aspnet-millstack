<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Booth_Master_Modal.aspx.cs" Inherits="Transaction_Pages_Modal_Booth_Master_Modal" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Booth Details</title>

    <!-- CSS -->
    <link href="<%= ResolveUrl("../../assets/components/bootstrap/css/bootstrap.min_v5.3.3.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("../../assets/components/sweet-alert/sweetalert2.min_v11.11.css") %>" rel="stylesheet" />

    <!-- JavaScript -->
    <script src="<%= ResolveUrl("../../assets/components/bootstrap/js/bootstrap.bundle.min_v5.3.3.js") %>"></script>
    <script src="<%= ResolveUrl("../../assets/components/bootstrap/js/bootstrap.min_v5.3.3.js") %>"></script>
    <script src="<%= ResolveUrl("../../assets/components/bootstrap/js/popper.min_v2.11.8.js") %>"></script>
    <script src="<%= ResolveUrl("../../assets/components/sweet-alert/sweetalert2_v11.11.js") %>"></script>
    <script src="<%= ResolveUrl("../../assets/components/select2/select2.min_v4.0.13.js") %>"></script>
    <script src="<%= ResolveUrl("../../assets/components/chosen_v1.8.7/chosen.jquery.min.js") %>"></script>
    <script src="<%= ResolveUrl("../../assets/components/sumo-select/jquery.sumoselect.min_v3.0.3.js") %>"></script>

    <!-- DataTables JS -->
    <script src="<%= ResolveUrl("../../assets/components/datatables/datatables.min.js") %>"></script>
    <script src="<%= ResolveUrl("../../assets/components/datatables/Buttons-2.4.2/js/dataTables.buttons.min.js") %>"></script>
    <script src="<%= ResolveUrl("../../assets/components/datatables/buttons.flash.min.js") %>"></script>
    <script src="<%= ResolveUrl("../../assets/components/datatables/JSZip-3.10.1/jszip.min.js") %>"></script>
    <script src="<%= ResolveUrl("../../assets/components/datatables/Buttons-2.4.2/js/buttons.html5.min.js") %>"></script>
    <script src="<%= ResolveUrl("../../assets/components/datatables/Buttons-2.4.2/js/buttons.print.min.js") %>"></script>

</head>
<body>
    <form id="form1" runat="server">

        <!-- Heading -->
        <div class="col-md-11 mx-auto fw-normal fs-3 fw-bold ps-0 pb-2 text-dark-emphasis mt-1 mb-1 text-center">
            <asp:Literal ID="Page_Heading" Text="Booth Details" runat="server"></asp:Literal>
        </div>

    </form>
</body>
</html>
