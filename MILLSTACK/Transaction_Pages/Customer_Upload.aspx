<%@ Page Title="" Language="C#" Async="true" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Customer_Upload.aspx.cs" Inherits="Transaction_Pages_Customer_Upload" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">


    <!-- Login Div Starts -->
    <div id="TopDiv" runat="server" visible="true" class="vh-100 mb-5">
        <div class="col-md-12 mx-auto mb-5">

            <!-- Heading -->
            <div class="col-md-12 mx-auto fw-bold fs-3 fw-medium ps-0 pb-2 text-dark-emphasis mb-4 text-center">
                <asp:Literal Text="Upload Customers" runat="server"></asp:Literal>
            </div>

            <!-- UI Starts -->
            <div class="card col-md-12 mx-auto mt-2 mb-5 rounded-3 shadow">
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
                                            ErrorMessage="enter sheet name" InitialValue="" ControlToValidate="SheetName" ValidationGroup="DocumentUpload">
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
                                <asp:LinkButton ID="Reference_Excel" runat="server" CssClass="link-body-secondary text-decoration-none fw-normal">download excel format</asp:LinkButton>

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

                        <asp:GridView ShowHeaderWhenEmpty="true" ID="GridCustomer" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridCustomer_RowDataBound"
                            CssClass="table table-bordered table-hover border border-1 border-dark-subtle text-center grid-custom mb-3">
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

                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                <asp:BoundField DataField="Gender" HeaderText="Gender" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                <asp:BoundField DataField="CustomerNo" HeaderText="Customer No" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                <asp:BoundField DataField="WardNo" HeaderText="Ward No" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                <asp:BoundField DataField="Society" HeaderText="Society" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                <asp:BoundField DataField="SectorArea" HeaderText="Sector / Area" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />

                                <asp:TemplateField HeaderText="Customer Type">
                                    <ItemTemplate>
                                        <span id="GridCustType" runat="server"></span>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="align-middle" Width="50px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remove">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>'>
                                            <asp:Image runat="server" ImageUrl="~/assests/img/modern-cross-fill.svg" AlternateText="Edit" style="width: 28px; height: 28px;" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" CssClass="align-middle" Width="30px" />
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                    </div>
                    <!-- Item GridView Ends -->


                    <!-- Error GridView Starts -->
                    <div id="ErrorGridDiv" runat="server" visible="false" class="mt-5 border border-dark-subtle bg-light rounded-3 shadow p-3">

                        <div class="fw-semibold fs-4 text-light-emphasis mb-3 text-start">
                            <asp:Literal ID="literal2" runat="server" Text="Excel Error Records"></asp:Literal>
                        </div>

                        <asp:GridView ShowHeaderWhenEmpty="true" ID="GridErrors" runat="server" AutoGenerateColumns="false"
                            CssClass="table table-bordered table-hover border border-1 border-dark-subtle text-center grid-custom mb-3">
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

                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                <asp:BoundField DataField="CustomerNo" HeaderText="Customer Number" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-light" />
                                <asp:BoundField DataField="ErrorReason" HeaderText="Error Reason" ItemStyle-Font-Size="15px" ItemStyle-CssClass="align-middle text-start fw-normal text-danger" />

                            </Columns>
                        </asp:GridView>

                    </div>
                    <!-- Error GridView Ends -->



                    <!-- Submit Button UI Starts -->
                    <div id="SubmitCancelButtonDiv" runat="server" visible="false" class="">

                        <div class="row mt-5 mb-2">
                            <div class="col-md-6 text-start">
                                <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn btn-custom text-white shadow mb-5" />
                            </div>
                            <div class="col-md-6 text-end">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="finalSubmit" CssClass="btn btn-custom text-white shadow mb-5" />
                            </div>
                        </div>
                    </div>
                    <!-- Submit Button UI Ends -->



                </div>
            </div>
            <!-- UI Starts -->



        </div>
    </div>
    <!-- Login Div Ends -->



</asp:Content>

