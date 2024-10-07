<%@ Page Title="" Language="C#" Async="true" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="UserCreation_Update.aspx.cs" Inherits="Master_Pages_UserCreation_Update" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">


    <!-- Heading -->
    <div class="row d-flex justify-content-between col-md-11 mx-auto fw-normal fs-3 fw-bold ps-0 pb-2 text-dark-emphasis mt-1 mb-1">
        <div class="col-md-6">
            <asp:Literal ID="Page_Heading" Text="User Master" runat="server"></asp:Literal>
        </div>
        <div class="col-md-6 text-end">
            <asp:Button 
                ID="Btn_UserCreation"
                runat="server"
                Text="New User +"
                OnClick="Btn_UserCreation_Click"
                CssClass="btn btn-dark col-md-3 border border-dark-subtle shadow rounded-0"/>
        </div>
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
                    <asp:Button 
                        ID="Btn_Reset"
                        runat="server" 
                        Text="Reset" 
                        OnClick="Btn_Reset_Click"
                        CssClass="btn btn-dark col-md-8 text-white shadow rounded-0" />
                </div>
                <div class="col-md-2 text-start">
                    <asp:Button 
                        ID="Btn_Search" 
                        runat="server" Text="Search" 
                        OnClick="Btn_Search_Click"
                        CssClass="btn btn-dark col-md-8 text-white shadow rounded-0" />
                </div>
            </div>
            <!-- Submit Button UI Ends -->

        </div>
    </div>
    <!-- Control UI Ends -->


    <!-- Grid UI Starts -->
    <div id="Div_Grid" runat="server" class="card col-md-11 mx-auto my-5 py-2 shadow rounded-3">
        <div class="card-body">

            <!-- Heading - BG -->
            <div class="fs-5 fw-medium text-white border border-dark-subtle bg-primary shadow rounded-2 text-left py-2 px-3 mb-3" style="background-color: #0f3f6f !important;">
                <asp:Literal ID="Main_Heading" Text="User Master Records" runat="server"></asp:Literal>
            </div>

            <!-- Scrollable Div For Gridview -->
            <div id="Div_Grid_Search" visible="false" runat="server" style="position: relative; overflow: hidden; width: auto; height: 580px;">
                <div class="card-body border border-dark-subtle bg-light p-2 rounded-2 shadow" data-height="580" style="overflow: auto; width: auto; height: 580px;">
                </div>
            </div>

            <div class="p-3 border border-dark-subtle shadow rounded-2 bg-light">
                <asp:GridView ID="Grid_Search" runat="server" ShowHeaderWhenEmpty="false" AutoGenerateColumns="false" Width="100%" SelectedRowStyle-BackColor="#F3F3F3"
                    DataKeyNames="User_ID" OnSelectedIndexChanged="Grid_Search_SelectedIndexChanged" OnRowDeleting="Grid_Search_RowDeleting"
                    AllowPaging="false" OnPageIndexChanging="Grid_Search_PageIndexChanging" PageSize="10"
                    CssClass="datatables table table-bordered table-hover border border-1 border-dark-subtle shadow text-center grid-custom">
                    <HeaderStyle CssClass="" />
                    <Columns>

                        <asp:TemplateField ControlStyle-CssClass="col-md-1" HeaderText="Sr.No">
                            <ItemTemplate>
                                <asp:HiddenField ID="id" runat="server" Value="id" />
                                <span><%#Container.DataItemIndex + 1%></span>
                            </ItemTemplate>
                            <ItemStyle CssClass="align-middle" Width="30px" />
                        </asp:TemplateField>


                        <asp:BoundField DataField="User_ID" Visible="true" HeaderText="User ID" HeaderStyle-CssClass="text-start" ItemStyle-CssClass="fw-light text-center" />
                        <asp:BoundField DataField="DesignationName" Visible="false" HeaderText="Designation Name" HeaderStyle-CssClass="text-start" ItemStyle-CssClass="fw-light text-center" />
                        <asp:BoundField DataField="UserFullName" Visible="true" HeaderText="Name" HeaderStyle-CssClass="text-start" ItemStyle-CssClass="fw-light text-center" />
                        <asp:BoundField DataField="UserName" Visible="true" HeaderText="UserName" HeaderStyle-CssClass="text-start" ItemStyle-CssClass="fw-light text-center" />
                        <asp:BoundField DataField="GenderName" Visible="true" HeaderText="Gender" HeaderStyle-CssClass="text-start" ItemStyle-CssClass="fw-light text-center" />
                        <asp:BoundField DataField="UserPhoneNo" Visible="true" HeaderText="Phone No" HeaderStyle-CssClass="text-start" ItemStyle-CssClass="fw-light text-center" />
                        <asp:BoundField DataField="UserEmail" Visible="true" HeaderText="Email" HeaderStyle-CssClass="text-start" ItemStyle-CssClass="fw-light text-center" />


                        <asp:TemplateField HeaderText="Edit" ShowHeader="true" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:LinkButton
                                    ID="Link_Btn_Edit"
                                    runat="server"
                                    CausesValidation="False"
                                    CommandName="Select"
                                    ForeColor="#0f3f6f">
                                    <asp:Image
                                        ID="IMG_Edit"
                                        runat="server"
                                        ImageUrl="~/assets/image/edit/pencil-square.svg"
                                        AlternateText="Edit"
                                        ToolTip="Edit"
                                        Style="width: 25px; height: 25px;" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="50px"></HeaderStyle>
                            <ItemStyle CssClass="align-middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Delete" ShowHeader="true" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:LinkButton
                                    ID="Link_Btn_Delete"
                                    runat="server"
                                    CausesValidation="False"
                                    CommandName="Delete"
                                    CommandArgument='<%# Container.DataItemIndex %>'
                                    OnClientClick="javascript:return confirm ('Are you sure to Delete this record permanently ? ')"
                                    ForeColor="Red">
                                    <asp:Image
                                        ID="IMG_Delete"
                                        runat="server"
                                        ImageUrl="~/assets/image/delete-cut/delete.png"
                                        AlternateText="Edit"
                                        ToolTip="Delete"
                                        Style="width: 30px; height: 30px;" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="50px"></HeaderStyle>
                            <ItemStyle CssClass="align-middle" />
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        <tr>
                            <td colspan="4" class="text-center">
                                <div class="alert alert-info" role="alert">
                                    No Data Available To Display.
                                </div>
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <FooterStyle CssClass="" />
                    <PagerStyle CssClass="grid-pager" />
                </asp:GridView>
            </div>

        </div>
    </div>
    <!-- Grid UI Ends -->



</asp:Content>

