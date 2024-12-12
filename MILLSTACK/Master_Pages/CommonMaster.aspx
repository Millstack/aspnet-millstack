<%@ Page Title="" Language="C#" Async="true" UnobtrusiveValidationMode="None" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="CommonMaster.aspx.cs" Inherits="Master_Pages_CommonMaster" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">


    <!-- Heading -->
    <div class="col-md-11 mx-auto fw-normal fs-3 fw-bold ps-0 pb-2 text-dark-emphasis mt-1 mb-1 text-center">
        <asp:Literal ID="Page_Heading" Text="" runat="server"></asp:Literal>
    </div>

    <!-- Control Starts -->
    <div id="Div_Control" runat="server" class="card col-md-11 mx-auto mt-1 py-2 shadow rounded-3 text-dark-emphasis">
        <div class="card-body">

            <!-- Heading - BG -->
            <div class="fs-5 fw-medium text-white border border-dark-subtle shadow rounded-2 text-left py-2 px-3 mb-3" style="background-color: #0f3f6f !important;">
                <asp:Literal ID="Main_Heading_1" Text="" runat="server"></asp:Literal>
            </div>

            <!-- Control Foriegn Section Starts -->
            <div class="row mb-2" style="border-style: solid none none none; border-width: 1px; border-color: #d6d5d5; padding-top: 10px; padding-bottom: 10px; margin-top: 10px; margin-bottom: 10px;">

                <!-- DD: Foreign Table 1 Column Name -->
                <div id="Div_Foriegn_DD_1" runat="server" class="col-md-4 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Foreign_Table_1_Key_Text" runat="server" Text=""><em style="color: red">*</em></asp:Literal>
                        <div>
                            <asp:RequiredFieldValidator ID="RFV_DD_Foreign_Column_1" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                ControlToValidate="DD_Foriegn_Column_1" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <asp:DropDownList ID="DD_Foriegn_Column_1" runat="server" Width="100%" CssClass="form-control chosen-dropdown"
                        OnSelectedIndexChanged="DD_Foriegn_Column_1_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>

                <!-- DD: Foreign Table 2 Column Name -->
                <div id="Div_Foriegn_DD_2" runat="server" class="col-md-4 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Foreign_Table_2_Key_Text" runat="server" Text=""><em style="color: red">*</em></asp:Literal>
                        <div>
                            <asp:RequiredFieldValidator ID="RFV_DD_Foreign_Column_2" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                ControlToValidate="DD_Foriegn_Column_2" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <asp:DropDownList ID="DD_Foriegn_Column_2" runat="server" Width="100%" CssClass="form-control chosen-dropdown"
                        OnSelectedIndexChanged="DD_Foriegn_Column_2_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>

                <!-- DD: Foreign Table 3 Column Name -->
                <div id="Div_Foriegn_DD_3" runat="server" class="col-md-4 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Foreign_Table_3_Key_Text" runat="server" Text=""><em style="color: red">*</em></asp:Literal>
                        <div>
                            <asp:RequiredFieldValidator ID="RFV_DD_Foreign_Column_3" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                ControlToValidate="DD_Foriegn_Column_3" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <asp:DropDownList ID="DD_Foriegn_Column_3" runat="server" Width="100%" CssClass="form-control chosen-dropdown"
                        OnSelectedIndexChanged="DD_Foriegn_Column_3_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>

            </div>
            <!-- Control Foriegn Section Ends -->


            <!-- Control Main Section Starts -->
            <div class="row mb-2">

                <!-- Tetxbox: Main Column 1 Input -->
                <div id="Div_Main_Table_Column_1" runat="server" class="col-md-4 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Main_Column_1_Text" runat="server" Text=""><em style="color: red">*</em></asp:Literal>
                        <asp:RequiredFieldValidator ID="RFV_Main_Table_Column_1" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Input_Main_Column_1" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Input_Main_Column_1" type="text" Enabled="true" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="Input_Main_Column_1"
                        FilterType="Numbers, UppercaseLetters, LowercaseLetters, Custom" ValidChars=". " />
                </div>

                <!-- Tetxbox: Main Column 2 Input -->
                <div id="Div_Main_Table_Column_2" runat="server" class="col-md-4 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Main_Column_2_Text" runat="server" Text=""><em style="color: red">*</em></asp:Literal>
                        <asp:RequiredFieldValidator ID="RFV_Main_Table_Column_2" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Input_Main_Column_2" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Input_Main_Column_2" type="text" Enabled="true" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"
                        oninput="validateInput(this)"></asp:TextBox>
                </div>

                <!-- Tetxbox: Main Column 3 Input -->
                <div id="Div_Main_Table_Column_3" runat="server" class="col-md-4 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Main_Column_3_Text" runat="server" Text=""><em style="color: red">*</em></asp:Literal>
                        <asp:RequiredFieldValidator ID="RFV_Main_Table_Column_3" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Input_Main_Column_3" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Input_Main_Column_3" type="text" Enabled="true" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="Input_Main_Column_3"
                        FilterType="Numbers, UppercaseLetters, LowercaseLetters, Custom" ValidChars=". " />
                </div>

            </div>
            <!-- Control Main Section Ends -->

            <!-- Submit Button UI Starts -->
            <div class="row mt-5 mb-2 align-self-end">
                <div class="col-md-2 mb-1 fw-semibold fs-6 align-middle text-center">
                    <%--<asp:Literal ID="Search_Text" runat="server" Text="Search Division Name"></asp:Literal>--%>
                </div>
                <div class="col-md-6 text-start">
                    <%--<asp:TextBox runat="server" ID="Input_Search_" type="text" Enabled="true"
                        CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"
                        placeholder="" oninput=""></asp:TextBox>--%>
                </div>
                <div class="col-md-2 text-end">
                    <asp:Button ID="Btn_Reset" runat="server" Text="Reset" OnClick="Btn_Reset_Click"
                        CssClass="btn col-md-8 text-white shadow rounded-0" Style="background: #0f3f6f; color: #fff" />
                </div>
                <div class="col-md-2 text-start">
                    <asp:Button ID="Btn_Submit" runat="server" Text="Save" OnClick="Btn_Submit_Click" ValidationGroup="finalSubmit"
                        CssClass="btn col-md-8 text-white shadow rounded-0" Style="background: #0f3f6f; color: #fff" />
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
                <asp:Literal ID="Main_Heading_2" Text="" runat="server"></asp:Literal>
            </div>

            <!-- Scrollable Div For Gridview -->
            <div id="Div_Grid_Search" visible="false" runat="server" style="position: relative; overflow: hidden; width: auto; height: 580px;">
                <div class="card-body border border-dark-subtle bg-light p-2 rounded-2 shadow" data-height="580" style="overflow: auto; width: auto; height: 580px;">
                </div>
            </div>

            <div class="p-3 border border-dark-subtle shadow rounded-2 bg-light">
                <asp:GridView ID="Grid_Search" runat="server" ShowHeaderWhenEmpty="false" AutoGenerateColumns="false" Width="100%" SelectedRowStyle-BackColor="#F3F3F3"
                    DataKeyNames="ID" OnSelectedIndexChanged="Grid_Search_SelectedIndexChanged" OnRowDeleting="Grid_Search_RowDeleting"
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

                        <asp:BoundField DataField="ID" Visible="false" HeaderText="ID">
                            <HeaderStyle Width="50px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="fw-light" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Main_Column_1" Visible="true" HeaderText="Main_Column_1">
                            <%--<HeaderStyle Width="50px" />--%>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="fw-light" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Main_Column_2" Visible="true" HeaderText="Main_Column_2">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="fw-light" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Main_Column_3" Visible="true" HeaderText="Main_Column_3">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="fw-light" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Foreign_Column_1" Visible="true" HeaderText="Foreign_Column_1">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="fw-bold text-body-secondary text-bg-light" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Foreign_Column_2" Visible="true" HeaderText="Foreign_Column_2">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="fw-bold text-body-secondary text-bg-light" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Foreign_Column_3" Visible="true" HeaderText="Foreign_Column_3">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="fw-bold text-body-secondary text-bg-light" />
                        </asp:BoundField>

                        <asp:BoundField DataField="DummyColumn" Visible="false" HeaderText="DummyColumn">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="fw-light" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Edit" ShowHeader="true" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:LinkButton ID="Link_Btn_Edit" runat="server" CausesValidation="False" CommandName="Select" ForeColor="#0f3f6f">
                                    <asp:Image ID="IMG_Edit" runat="server" ImageUrl="~/assets/image/edit/pencil-square.svg" AlternateText="Edit" ToolTip="Edit" Style="width: 25px; height: 25px;" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemStyle CssClass="align-middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Delete" ShowHeader="true" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:LinkButton ID="Link_Btn_Delete" runat="server" CausesValidation="False"
                                    CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>'
                                    OnClientClick="javascript:return confirm ('Are you sure to Delete this record permanently ? ')" ForeColor="Red">
                                    <asp:Image ID="IMG_Delete" runat="server" ImageUrl="~/assets/image/delete-cut/delete.png" AlternateText="Edit" ToolTip="Delete" Style="width: 30px; height: 30px;" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="80px"></HeaderStyle>
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


            <asp:HiddenField ID="hfDeleteId" runat="server" />

        </div>
    </div>
    <!-- Grid UI Ends -->


    <script type="text/javascript">
        function validateInput(input) {
            var value = input.value;
            var regex = /^[\u0900-\u097F\w\s.]*$/; // \u0900-\u097F is the Unicode range for Devanagari script (Marathi)
            if (!regex.test(value)) {
                input.value = value.slice(0, -1); // Remove the last invalid character
            }
        }
    </script>

</asp:Content>

