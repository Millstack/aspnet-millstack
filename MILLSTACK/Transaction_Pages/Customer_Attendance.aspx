<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Customer_Attendance.aspx.cs" Inherits="Transaction_Pages_Customer_Attendance" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">

    <!-- Heading -->
    <div class="row d-flex justify-content-between col-md-11 mx-auto fw-normal fs-3 fw-bold ps-0 pb-2 text-dark-emphasis mt-1 mb-1">
        <div class="col-md-6">
            <asp:Literal ID="Page_Heading" Text="Customer Attendance" runat="server"></asp:Literal>
        </div>
        <div id="Div_New_Btn" runat="server" visible="false" class="col-md-6 text-end">
            <asp:Button
                ID="Btn_New_Record"
                runat="server"
                Text="New Customer +"
                OnClick="Btn_New_Record_Click"
                CssClass="btn btn-dark col-md-3 border border-dark-subtle shadow rounded-0" />
        </div>
    </div>

    <!-- Control Starts -->
    <div id="Div_Control" runat="server" class="card col-md-11 mx-auto mt-1 py-2 shadow rounded-3 text-dark-emphasis">
        <div class="card-body">

            <!-- Control Section Starts -->
            <div class="row mb-2">

                <!-- DropDown: List Number -->
                <div class="col-md-4 mb-3 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Lit_UserName" runat="server" Text="List Number"></asp:Literal>
                    </div>
                    <asp:DropDownList ID="DD_List_No" runat="server" Width="100%" CssClass="form-control chosen-dropdown">
                    </asp:DropDownList>
                </div>

                <!-- DropDown: Serial Number -->
                <div class="col-md-4 mb-3 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Literal1" runat="server" Text="Serial Number"></asp:Literal>
                    </div>
                    <asp:DropDownList ID="DD_Serial_No" runat="server" Width="100%" CssClass="form-control chosen-dropdown">
                    </asp:DropDownList>
                </div>

                <!-- DropDown: Customer Name -->
                <div class="col-md-4 mb-3 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Literal2" runat="server" Text="Customer Name"></asp:Literal>
                    </div>
                    <asp:DropDownList ID="DD_Customer_Name" runat="server" Width="100%" CssClass="form-control chosen-dropdown">
                    </asp:DropDownList>
                </div>

                <!-- DropDown: WRN Number -->
                <div class="col-md-4 mb-3 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Literal3" runat="server" Text="WRN Number"></asp:Literal>
                    </div>
                    <asp:DropDownList ID="DD_WRN_No" runat="server" Width="100%" CssClass="form-control chosen-dropdown">
                    </asp:DropDownList>
                </div>

                <!-- DropDown: Voting Booth -->
                <div class="col-md-4 mb-3 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Literal4" runat="server" Text="Voting Booth"></asp:Literal>
                    </div>
                    <asp:DropDownList ID="DD_Voting_Booth" runat="server" Width="100%" CssClass="form-control chosen-dropdown">
                    </asp:DropDownList>
                </div>

                <!-- DropDown: Voting Room -->
                <div class="col-md-4 mb-3 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Literal5" runat="server" Text="Voting Room"></asp:Literal>
                    </div>
                    <asp:DropDownList ID="DD_Voting_Room" runat="server" Width="100%" CssClass="form-control chosen-dropdown">
                    </asp:DropDownList>
                </div>

            </div>
            <!-- Control Section Ends -->

            <!-- Submit Button UI Starts -->
            <div class="d-flex justify-content-end align-self-end">
                <asp:Button
                    ID="Btn_Reset"
                    runat="server"
                    Text="Reset"
                    OnClick="Btn_Reset_Click"
                    CssClass="btn btn-dark col-md-1 text-white shadow rounded-0" />
                <asp:Button
                    ID="Btn_Search"
                    runat="server" Text="Search"
                    OnClick="Btn_Search_Click"
                    CssClass="btn btn-dark col-md-1 text-white shadow rounded-0 ms-3" />
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
                <asp:Literal ID="Main_Heading" Text="Customer Attendance Records" runat="server"></asp:Literal>
            </div>

            <div class="p-3 border border-dark-subtle shadow rounded-2 bg-light">
                <asp:GridView ID="Grid_Search" runat="server" ShowHeaderWhenEmpty="false" AutoGenerateColumns="false" Width="100%" SelectedRowStyle-BackColor="#F3F3F3"
                    DataKeyNames="Customer_ID" OnSelectedIndexChanged="Grid_Search_SelectedIndexChanged" OnRowDeleting="Grid_Search_RowDeleting"
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

                        <asp:BoundField DataField="Customer_ID" Visible="false" HeaderText="Customer ID" HeaderStyle-CssClass="text-start" ItemStyle-CssClass="fw-light text-center" />
                        <asp:BoundField DataField="List_No" Visible="true" HeaderText="List No" HeaderStyle-CssClass="text-start" ItemStyle-CssClass="fw-light text-center" />
                        <asp:BoundField DataField="Serial_No" Visible="true" HeaderText="Serial No" HeaderStyle-CssClass="text-start" ItemStyle-CssClass="fw-light text-center" />
                        <asp:BoundField DataField="WRN_No" Visible="true" HeaderText="WRN No" HeaderStyle-CssClass="text-start" ItemStyle-CssClass="fw-light text-center" />
                        <asp:BoundField DataField="Customer_Name" Visible="true" HeaderText="Customer Name" HeaderStyle-CssClass="text-start" ItemStyle-CssClass="fw-light text-center" />
                        <asp:BoundField DataField="Customer_MobileNo" Visible="true" HeaderText="Mobile No" HeaderStyle-CssClass="text-start" ItemStyle-CssClass="fw-light text-center" />
                        <asp:BoundField DataField="GenderName" Visible="true" HeaderText="Gender" HeaderStyle-CssClass="text-start" ItemStyle-CssClass="fw-light text-center" />
                        <asp:BoundField DataField="WardName" Visible="false" HeaderText="Ward" HeaderStyle-CssClass="text-start" ItemStyle-CssClass="fw-light text-center" />
                        <asp:BoundField DataField="SectorName" Visible="false" HeaderText="Sector" HeaderStyle-CssClass="text-start" ItemStyle-CssClass="fw-light text-center" />

                        <asp:BoundField DataField="Voting_Booth" Visible="true" HeaderText="Voting Booth" HeaderStyle-CssClass="text-start"
                            ItemStyle-CssClass="fw-bold text-body-secondary text-bg-light text-center" />
                        <asp:BoundField DataField="Voting_Room" Visible="true" HeaderText="Voting Room" HeaderStyle-CssClass="text-start"
                            ItemStyle-CssClass="fw-bold text-body-secondary text-bg-light text-center" />

                        <asp:TemplateField HeaderText="Customer Type" ShowHeader="true" Visible="false">
                            <ItemTemplate>
                                <asp:Literal
                                    ID="Literal_CustomerType"
                                    runat="server"
                                    Text='<%# Eval("CustomerType_ID") %>'
                                    EnableViewState="false"
                                    Mode="PassThrough" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="align-middle"></HeaderStyle>
                            <ItemStyle CssClass="text-center align-middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Customer Status" ShowHeader="true" Visible="false">
                            <ItemTemplate>
                                <asp:Literal
                                    ID="Literal_CustomerStatus"
                                    runat="server"
                                    Text='<%# Eval("Customer_Done") %>'
                                    EnableViewState="false"
                                    Mode="PassThrough" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="align-middle"></HeaderStyle>
                            <ItemStyle CssClass="text-center align-middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="View" ShowHeader="true" Visible="true" HeaderStyle-Width="50px">
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
                                        ImageUrl="~/assets/icons/general-icons/ballot-2.png"
                                        AlternateText="Edit"
                                        ToolTip="View Booth Details"
                                        Style="width: 25px; height: 25px;" />
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

