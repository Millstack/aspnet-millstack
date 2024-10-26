<%@ Page Title="EMTS - Customer Upload" Language="C#" Async="true" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Customer_Upload.aspx.cs" Inherits="Transaction_Pages_Customer_Upload" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">


    <style type="text/css">
        .scrollable-grid-wrapper {
            overflow-x: auto; /* Enables horizontal scrolling */
            white-space: nowrap; /* Prevents inner content from wrapping */
            width: 100%; /* Ensure full width is used */
        }
    </style>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <!-- Heading -->
            <div class="col-md-11 mx-auto fw-normal fs-3 fw-bold ps-0 pb-2 text-dark-emphasis mt-1 mb-1 text-center">
                <asp:Literal ID="Page_Heading" Text="Customer Upload" runat="server"></asp:Literal>
            </div>

            <!-- UI Starts -->
            <div id="Div_Control" runat="server" class="card col-md-12 mx-auto mt-1 py-2 shadow rounded-3">
                <div class="card-body">

                    <!-- Excel Upload UI Starts -->
                    <div id="ExcelUploadDiv" runat="server" visible="true" class="mb-3 mt-3 py-3">
                        <div id="docUpload" runat="server" visible="true" class="row mt-4">

                            <!-- Excel Sheet Name -->
                            <div class="col-md-4 align-self-end">
                                <div class="mb-1 text-body-tertiary fw-semibold fs-6">
                                    <asp:Literal ID="Literal1" Text="" runat="server">Sheet Name<em style="color: red">*</em></asp:Literal>
                                    <div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                            ErrorMessage="enter sheet name" InitialValue="" ControlToValidate="Txt_Sheet_Name" ValidationGroup="DocumentUpload">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <asp:TextBox runat="server" ID="Txt_Sheet_Name" type="text" step="any" min="-Infinity" max="Infinity" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                            </div>

                            <!-- Excel Upload UI Starts -->
                            <div class="col-md-8 align-self-start">

                                <!-- User Information -->
                                <h6 class="fw-lighter fs-6 text-body-tertiary">User can upload excel with format .xlsx or .xls</h6>

                                <!-- Reference Excel Format -->
                                <asp:LinkButton 
                                    ID="Reference_Excel" 
                                    runat="server" 
                                    CssClass="link- text-decoration-none fw-normal"
                                    Text="download reference excel sheet"
                                    OnClick="Reference_Excel_Click">
                                </asp:LinkButton>

                                <div>
                                    <asp:RequiredFieldValidator ID="RFV_Upload" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                        ErrorMessage="choose excel file to upload" InitialValue="" ControlToValidate="File_Upload" ValidationGroup="DocumentUpload">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <!-- File Browse UI -->
                                <div class="input-group">
                                    <asp:FileUpload ID="File_Upload" runat="server" CssClass="form-control" aria-describedby="inputGroupPrepend" />
                                    <asp:Button ID="Btn_Upload" runat="server" Text="Upload" OnClick="Btn_Upload_Click" AutoPost="true"
                                        CssClass="btn btn-dark text-white btn-outline-secondary" ValidationGroup="DocumentUpload" />
                                </div>

                            </div>
                            <!-- Excel Upload UI Ends -->

                        </div>
                    </div>
                    <!-- Excel Upload UI Ends -->




                    <!-- Item GridView Starts -->
                    <div id="CustomerDiv" runat="server" visible="false" class="mt-3 border border-dark-subtle bg-light rounded-3 shadow p-3">

                        <div class="fw-semibold fs-4 text-light-emphasis mb-3 text-start">
                            <asp:Literal ID="literalCustomer" runat="server" Text="Uploaded Customer Records"></asp:Literal>
                        </div>

                        <div class="col-md-12">
                            <asp:GridView ShowHeaderWhenEmpty="true" ID="Grid_Customer" runat="server" AutoGenerateColumns="false"
                                OnRowDataBound="GridCustomer_RowDataBound" OnRowDeleting="Grid_Customer_RowDeleting"
                                CssClass="datatables table table-bordered table-hover border border-1 border-dark-subtle text-center grid-custom mb-3">
                                <HeaderStyle CssClass="align-middle" />
                                <Columns>

                                    <asp:TemplateField ControlStyle-CssClass="col-md-1" HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="id" runat="server" Value="id" />
                                            <span>
                                                <%# Container.DataItemIndex + 1 %>
                                            </span>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="align-middle" Width="30px" />
                                    </asp:TemplateField>

                                    <asp:BoundField Visible="true" DataField="List_No" HeaderText="List No." HeaderStyle-CssClass="text-center" ItemStyle-CssClass="fw-light text-start" />
                                    <asp:BoundField Visible="true" DataField="Serial_No" HeaderText="Sr. No." HeaderStyle-CssClass="text-center" ItemStyle-CssClass="fw-light text-start" />
                                    <asp:BoundField Visible="true" DataField="Customer_Name" HeaderText="Customer Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="fw-light text-start" />
                                    <asp:BoundField Visible="true" DataField="Customer_MobileNo" HeaderText="Mobile No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="fw-light text-start" />

                                    <asp:TemplateField HeaderText="Gender">
                                        <ItemTemplate>
                                            <div id="Div_Gender_Wrapper" runat="server" class="">
                                                <asp:DropDownList ID="DD_Gender" runat="server" Width="100%"
                                                    CssClass="form-control chosen-dropdown" AutoPostBack="false">
                                                </asp:DropDownList>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="align-middle" Width="30px" />
                                    </asp:TemplateField>

                                    <asp:BoundField Visible="true" DataField="WRN_No" HeaderText="WRN No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="fw-light text-start" />

                                    <asp:TemplateField HeaderText="Customer Type">
                                        <ItemTemplate>
                                            <div id="DivWrapper" runat="server" class="">
                                                <asp:DropDownList ID="DD_Customer_Type" runat="server" Width="100%"
                                                    CssClass="form-control chosen-dropdown" AutoPostBack="true"
                                                    OnSelectedIndexChanged="DD_Customer_Type_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="align-middle" Width="30px" />
                                    </asp:TemplateField>

                                    <asp:BoundField Visible="true" DataField="Voting_Booth" HeaderText="Voting Booth" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="fw-light text-start" />
                                    <asp:BoundField Visible="true" DataField="Voting_Room" HeaderText="Voting Room" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="fw-light text-start" />
                                    <asp:BoundField Visible="true" DataField="Ward_ID" HeaderText="Ward" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="fw-light text-start" />
                                    <asp:BoundField Visible="true" DataField="Sector_ID" HeaderText="Sector" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="fw-light text-start" />
                                    <asp:BoundField Visible="true" DataField="Society_ID" HeaderText="Society" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="fw-light text-start" />

                                    <asp:TemplateField HeaderText="Remove">
                                        <ItemTemplate>
                                            <asp:LinkButton 
                                                ID="lnkDelete" 
                                                runat="server" 
                                                CommandName="Delete" 
                                                ToolTip="Delete"
                                                CommandArgument='<%# Container.DataItemIndex %>'
                                                OnClientClick="return confirm('Are you sure to Delete this record ?');">
                                                <asp:Image runat="server" 
                                                    ImageUrl="~/assets/image/delete-cut/delete-5.png" 
                                                    AlternateText="Edit" 
                                                    style="width: 40px; height: 40px;"
                                                    CssClass="shadow"/>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="align-middle" Width="30px" />
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
                    <!-- Item GridView Ends -->


                    <!-- Error GridView Starts -->
                    <div id="ErrorGridDiv" runat="server" visible="false" class="mt-5 border border-dark-subtle bg-light rounded-3 shadow p-3">

                        <div class="fw-semibold fs-4 text-light-emphasis mb-3 text-start">
                            <asp:Literal ID="literal2" runat="server" Text="Excel Error Records"></asp:Literal>
                        </div>

                        <asp:GridView ShowHeaderWhenEmpty="true" ID="GridErrors" runat="server" AutoGenerateColumns="false"
                            CssClass="datatables table table-bordered table-hover border border-1 border-dark-subtle text-center grid-custom mb-3">
                            <HeaderStyle CssClass="align-middle" />
                            <Columns>

                                <asp:TemplateField ControlStyle-CssClass="col-md-1" HeaderText="Sr.No">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="id" runat="server" Value="id" />
                                        <span>
                                            <%# Container.DataItemIndex + 1 %>
                                        </span>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="align-middle" Width="30px" />
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
                    <!-- Error GridView Ends -->



                    <!-- Submit Button UI Starts -->
                    <div id="SubmitCancelButtonDiv" runat="server" visible="false" class="">

                        <div class="row mt-5 mb-2">
                            <div class="col-md-6 text-start">
                                <asp:Button 
                                    ID="btnBack"
                                    runat="server" 
                                    Text="Back" 
                                    OnClick="btnBack_Click" 
                                    CssClass="btn btn-custom text-white shadow mb-5"
                                    Style="background: #0f3f6f; color: #fff"/>
                            </div>
                            <div class="col-md-6 text-end">
                                <asp:Button 
                                    ID="btnSubmit" 
                                    runat="server" 
                                    Text="Submit" 
                                    OnClick="btnSubmit_Click" 
                                    ValidationGroup="finalSubmit" 
                                    CssClass="btn btn-custom text-white shadow mb-5"
                                    Style="background: #0f3f6f; color: #fff"/>
                            </div>
                        </div>
                    </div>
                    <!-- Submit Button UI Ends -->



                </div>
            </div>
            <!-- UI Starts -->


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Reference_Excel" />
            <asp:PostBackTrigger ControlID="Btn_Upload" />
        </Triggers>
    </asp:UpdatePanel>



</asp:Content>

