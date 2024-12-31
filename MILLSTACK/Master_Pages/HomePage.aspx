<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="Master_Pages_HomePage" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <!-- Put any server-side scripts or code blocks here if needed -->
    </telerik:RadCodeBlock>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">


    <h1>Welcome to EMTS</h1> <br />
    <h4>Election Monitoring And Tracking System</h4>



</asp:Content>

