<%@ Page Title="" Language="C#" MasterPageFile="~/private.Master" AutoEventWireup="true" CodeBehind="update.aspx.cs" Inherits="RWA.update" UnobtrusiveValidationMode="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="container">
            <div class="col-12 jumboton">
                <h2>Ažuriranje Kupca</h2>
            </div>
        <div class="edit-wrapper">
            <div class="row edit-buyer">
                <div class="card bg-light mb-3" style="max-width: 100%;">
                    <div class="card-header">Uredite podatke za odabranog kupca: id = <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label></div>
                    <div class="card-body">

                        <div class="form-buyers">
                            <asp:Label Text="Ime kupca" runat="server" CssClass="buyer-label" />
                            <asp:TextBox runat="server" ID="txtBuyerName" CssClass="form-control buyerInput"></asp:TextBox>
                        </div>

                        <div class="form-buyers">
                            <asp:Label Text="Prezime kupca" runat="server" CssClass="buyer-label" />
                            <asp:TextBox runat="server" ID="txtBuyerSurName" CssClass="form-control buyerInput"></asp:TextBox>
                        </div>

                        <div class="form-buyers">
                            <asp:Label Text="E-mail kupca" runat="server" CssClass="buyer-label" />
                            <asp:TextBox runat="server" ID="txtBuyerEmail" CssClass="form-control buyerInput"></asp:TextBox>
                        </div>

                        <div class="form-buyers">
                            <asp:Label Text="Telefon kupca" runat="server" CssClass="buyer-label" />
                            <asp:TextBox runat="server" ID="txtBuyerPhone" CssClass="form-control buyerInput"></asp:TextBox>
                        </div>


                        <div class="form-buyers">
                            <asp:Label Text="Država kupca" runat="server" CssClass="buyer-label" />
                            <asp:DropDownList ID="ddlDrzava" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDrzava_SelectedIndexChanged" ></asp:DropDownList>
                        </div>

                        <div class="form-buyers">
                            <asp:Label Text="Grad kupca" runat="server" CssClass="buyer-label" />
                            <asp:DropDownList ID="ddlGrad" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlGrad_SelectedIndexChanged" ></asp:DropDownList>
                        </div>

                        <hr />

                        <div>
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Ažuriraj kupca" OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" OnClick="btnCancel_Click" Text="Povratak" />
                        </div>
                         <asp:Label ID="lblUpdateInfo" runat="server" CssClass="error" ClientIDMode="Static" Font-Size="10"></asp:Label>

                    </div>
                    <!-- End of Card-body -->
                </div>
                <!-- End of card bg-light mb-3 -->
            </div>
            <!-- End of edit-buyer -->

        </div><!-- End of edit-wrapper -->
        <div class="col-4">
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </div>
        <!-- End of col-4 -->
    </div>
    <!-- End of container -->

</asp:Content>

