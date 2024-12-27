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

</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>

        <!-- Heading -->
        <div class="col-md-11 mx-auto fw-normal fs-3 fw-bold ps-0 pb-2 text-dark-emphasis mt-1 mb-1 text-center">
            <asp:Literal ID="Page_Heading" Text="Booth Details" runat="server"></asp:Literal>
        </div>

        <!-- Booth Details Starts -->
        <div id="Div_UI" runat="server" class="card col-sm-10 mx-auto py-0 px-0 shadow rounded-3">
            <div class="card-body row">

                <!-- TetxtBox: Customer Name -->
                <div class="col-md-10 mb-3 align-self-end">
                    <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                        <asp:Literal ID="TxtID1" runat="server" Text="">Customer Name
                        </asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Txt_Customer_Name" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Txt_Customer_Name" type="text" Enabled="false" min="0" MaxLength="500"
                        CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">
                    </asp:TextBox>
                    <ajax:FilteredTextBoxExtender
                        ID="FilteredTextBoxExtender1"
                        runat="server"
                        TargetControlID="Txt_Customer_Name"
                        FilterType="UppercaseLetters, LowercaseLetters, Numbers, Custom"
                        ValidChars=" " />
                </div>

                <!-- TetxtBox: WRN Number -->
                <div class="col-md-10 mb-3 align-self-end">
                    <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                        <asp:Literal ID="Literal4" runat="server">WRN Number
                        </asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Txt_WRN_No" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Txt_WRN_No" TextMode="Number" Enabled="false" min="0" MaxLength="500"
                        CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">
                    </asp:TextBox>
                    <ajax:FilteredTextBoxExtender
                        ID="FilteredTextBoxExtender5"
                        runat="server"
                        TargetControlID="Txt_WRN_No"
                        FilterType="Numbers"
                        ValidChars=" " />
                </div>

                <!-- TetxtBox: List Number -->
                <div class="col-md-10 mb-3 align-self-end">
                    <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                        <asp:Literal ID="Literal2" runat="server">List Number
                        </asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Txt_List_No" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Txt_List_No" TextMode="Number" Enabled="false" min="0" MaxLength="500"
                        CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">
                    </asp:TextBox>
                    <ajax:FilteredTextBoxExtender
                        ID="FilteredTextBoxExtender3"
                        runat="server"
                        TargetControlID="Txt_List_No"
                        FilterType="Numbers"
                        ValidChars=" " />
                </div>

                <!-- TetxtBox: Serial Number -->
                <div class="col-md-10 mb-3 align-self-end">
                    <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                        <asp:Literal ID="Literal3" runat="server">Serial Number
                        </asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Txt_Serial_No" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Txt_Serial_No" TextMode="Number" Enabled="false" min="0" MaxLength="500"
                        CssClass="form-control border border-secondary-subtle bg-light rounded-1 fs-6 fw-light py-1">
                    </asp:TextBox>
                    <ajax:FilteredTextBoxExtender
                        ID="FilteredTextBoxExtender4"
                        runat="server"
                        TargetControlID="Txt_Serial_No"
                        FilterType="Numbers"
                        ValidChars=" " />
                </div>

                <!-- TetxtBox: Voting Booth -->
                <div class="col-md-10 mb-3 align-self-end">
                    <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                        <asp:Literal ID="Literal7" runat="server" Text="">Voting Booth
                        </asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Txt_Voting_Booth" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Txt_Voting_Booth" TextMode="Number" Enabled="true" min="0" MaxLength="500"
                        CssClass="form-control border border-secondary-subtle bg-light text-secondary rounded-1 fs-6 fw-bold py-1 shadow-sm">
                    </asp:TextBox>
                    <ajax:FilteredTextBoxExtender
                        ID="FilteredTextBoxExtender7"
                        runat="server"
                        TargetControlID="Txt_Voting_Booth"
                        FilterType="Numbers"
                        ValidChars="" />
                </div>

                <!-- TetxtBox: Voting Room -->
                <div class="col-md-10 mb-3 align-self-end">
                    <div class="mb-1 fw-normal fs-6 text-dark-emphasis">
                        <asp:Literal ID="Literal6" runat="server" Text="">Voting Room
                        </asp:Literal>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="invalid-feedback" SetFocusOnError="True" Display="Dynamic" ToolTip="Required"
                            ControlToValidate="Txt_Voting_Room" InitialValue="" ValidationGroup="finalSubmit" ErrorMessage="required field" EnableClientScript="true">
                        </asp:RequiredFieldValidator>
                    </div>
                    <asp:TextBox runat="server" ID="Txt_Voting_Room" TextMode="Number" Enabled="true" min="0" MaxLength="500"
                        CssClass="form-control border border-secondary-subtle bg-light text-secondary rounded-1 fs-6 fw-bold py-1 shadow-sm">
                    </asp:TextBox>
                    <ajax:FilteredTextBoxExtender
                        ID="FilteredTextBoxExtender6"
                        runat="server"
                        TargetControlID="Txt_Voting_Room"
                        FilterType="Numbers"
                        ValidChars="" />
                </div>

            </div>
        </div>
        <!-- Booth Details Ends -->

    </form>
</body>
</html>
