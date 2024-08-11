<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="CommonMasterTwo.aspx.cs" Inherits="Master_Pages_CommonMasterTwo" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">


    <!-- Heading -->
    <div class="col-md-11 mx-auto fw-normal fs-3 fw-bold ps-0 pb-2 text-dark-emphasis mt-1 mb-1 text-center">
        <asp:Literal ID="Page_Heading" Text="" runat="server"></asp:Literal>
    </div>

    <!-- Control Starts -->
    <div class="card col-md-11 mx-auto mt-1 py-2 shadow rounded-3">
        <div class="card-body">

            <!-- Heading - BG -->
            <div class="fs-5 fw-medium text-white border border-dark-subtle bg-primary shadow rounded-2 text-center py-2 mb-3">
                <asp:Literal Text="" runat="server"></asp:Literal>
            </div>

            <!-- Control Foriegn Section Starts -->
            <div class="row mb-2">

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
                    <asp:DropDownList ID="DD_Foriegn_Column_1" runat="server" Width="100%" CssClass="form-control"
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
                    <asp:DropDownList ID="DD_Foriegn_Column_2" runat="server" Width="100%" CssClass="form-control"
                        OnSelectedIndexChanged="DD_Foriegn_Column_2_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>

                <!-- DD: Foreign Table 3 Column Name -->
                <div id="Div1" runat="server" class="col-md-4 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Foreign_Table_3_Key_Text" runat="server" Text=""><em style="color: red">*</em></asp:Literal>
                        <div>
                            <asp:RequiredFieldValidator ID="RFV_DD_Foreign_Column_3" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                                ControlToValidate="DD_Foriegn_Column_3" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <asp:DropDownList ID="DD_Foriegn_Column_3" runat="server" Width="100%" CssClass="form-control"
                        OnSelectedIndexChanged="DD_Foriegn_Column_3_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>

            </div>
            <!-- Control Foriegn Section Ends -->


            <!-- Control Main Section Starts -->
            <div class="row mb-2">

                <!-- Tetxbox: Main Column 1 Input -->
                <div class="col-md-4 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Main_Column_1_Text" runat="server" Text=""><em style="color: red">*</em></asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Input_Main_Column_1" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox ID="Input_Main_Column_1" runat="server" type="text" Enabled="false" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="Input_Main_Column_1"
                        FilterType="Numbers, UppercaseLetters, LowercaseLetters" ValidChars=" " />
                </div>

                <!-- Tetxbox: Main Column 2 Input -->
                <div class="col-md-4 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Main_Column_2_Text" runat="server" Text=""><em style="color: red">*</em></asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Input_Main_Column_2" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Input_Main_Column_2" type="text" Enabled="false" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="Input_Main_Column_1"
                        FilterType="Numbers, UppercaseLetters, LowercaseLetters" ValidChars=" " />
                </div>

                <!-- Tetxbox: Main Column 3 Input -->
                <div class="col-md-4 align-self-end">
                    <div class="mb-1 fw-semibold fs-6">
                        <asp:Literal ID="Main_Column_3_Text" runat="server" Text=""><em style="color: red">*</em></asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Input_Main_Column_3" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Input_Main_Column_3" type="text" Enabled="false" CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="Input_Main_Column_1"
                        FilterType="Numbers, UppercaseLetters, LowercaseLetters" ValidChars=" " />
                </div>

            </div>
            <!-- Control Main Section Ends -->

            <!-- Submit Button UI Starts -->
            <div class="row mt-5 mb-2">
                <div class="col-md-3 text-start"></div>
                <div class="col-md-3 text-start"></div>
                <div class="col-md-3 text-start">
                    <asp:Button ID="Btn_Back" runat="server" Text="Back" OnClick="Btn_Back_Click"
                        CssClass="btn col-md-3 text-white shadow rounded-0" Style="background: #4800ff; color: #fff" />
                </div>
                <div class="col-md-3 text-end">
                    <asp:Button ID="Btn_Submit" runat="server" Text="Save" OnClick="Btn_Submit_Click" ValidationGroup="finalSubmit"
                        CssClass="btn col-md-3 text-white shadow rounded-0" Style="background: #4800ff; color: #fff" />
                </div>
            </div>
            <!-- Submit Button UI Ends -->

        </div>
    </div>
    <!-- Control UI Ends -->


    <!-- Grid UI Starts -->
    <div class="card col-md-11 mx-auto mt-1 py-2 shadow rounded-3">
        <div class="card-body">

            <!-- Heading - BG -->
            <div class="fs-5 fw-medium text-white border border-dark-subtle bg-primary shadow rounded-2 text-center py-2 mb-3">
                <asp:Literal Text="" runat="server"></asp:Literal>
            </div>

            <div id="Div_Grid_Search" visible="true" runat="server" style="position: relative; overflow: hidden; width: auto; height: 580px;">
                <div class="card-body pt-0 slimScroll" data-height="580" style="overflow: auto; width: auto; height: 580px;">

                    <asp:GridView ID="Grid_Search" runat="server" ShowHeaderWhenEmpty="false" AutoGenerateColumns="false" Width="100%" SelectedRowStyle-BackColor="#F3F3F3"
                        DataKeyNames="ID" OnSelectedIndexChanged="Grid_Search_SelectedIndexChanged" OnRowDeleting="Grid_Search_RowDeleting"
                        AllowPaging="true" OnPageIndexChanging="Grid_Search_PageIndexChanging" PageSize="10"
                        CssClass="datatables table table-bordered table-hover border border-1 border-dark-subtle text-center grid-custom" Style="">
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
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="fw-light" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Main_Column_1" Visible="true" HeaderText="Main_Column_1">
                                <%--<HeaderStyle Width="50px" />--%>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="fw-light" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Main_Column_2" Visible="true" HeaderText="Main_Column_2">
                                <%--<HeaderStyle Width="50px" />--%>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="fw-light" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Main_Column_3" Visible="true" HeaderText="Main_Column_3">
                                <%--<HeaderStyle Width="50px" />--%>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="fw-light" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Foreign_Column_1" Visible="true" HeaderText="Foreign_Column_1">
                                <%--<HeaderStyle Width="50px" />--%>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="fw-light" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Foreign_Column_2" Visible="true" HeaderText="Foreign_Column_2">
                                <%--<HeaderStyle Width="50px" />--%>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="fw-light" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Foreign_Column_3" Visible="true" HeaderText="Foreign_Column_3">
                                <%--<HeaderStyle Width="50px" />--%>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="fw-light" />
                            </asp:BoundField>

                            <asp:BoundField DataField="DummyColumn" Visible="false" HeaderText="DummyColumn">
                                <%--<HeaderStyle Width="50px" />--%>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="fw-light" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Edit" ShowHeader="true" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Link_Btn_Edit" runat="server" CausesValidation="False" CommandName="Select"
                                        Text="<i class='XlFont feather icon-edit'></i>">
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="50px"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Delete" ShowHeader="true" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Link_Btn_Delete" runat="server" CausesValidation="False" CommandName="Delete" 
                                        OnClientClick="javascript:return confirm ('Are you sure to Delete this record permanently ? ')"
                                        ForeColor="Red" Text="<i class='XlFont feather icon-trash'></i>">
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="50px"></HeaderStyle>
                            </asp:TemplateField>

                        </Columns>

                        <FooterStyle CssClass="" />
                        <PagerStyle Font-Bold="True" />

                    </asp:GridView>
                </div>
                <div class="slimScrollBar" style="background: rgba(0, 0, 0, 0.95); width: 5px; position: absolute; top: 0px; opacity: 0.4; display: none; border-radius: 7px; z-index: 99; right: 1px; height: 73.2899px;"></div>
                <div class="slimScrollRail" style="width: 5px; height: 100%; position: absolute; top: 0px; display: none; border-radius: 7px; background: rgb(51, 51, 51); opacity: 0.2; z-index: 90; right: 1px;"></div>
            </div>

        </div>
    </div>
    <!-- Grid UI Ends -->

</asp:Content>

