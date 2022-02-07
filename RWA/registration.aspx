<%@ Page Title="" Language="C#" MasterPageFile="~/public.Master" AutoEventWireup="true" CodeBehind="registration.aspx.cs" Inherits="RWA.registration" UnobtrusiveValidationMode="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="container content-wrapper">
        <div class="form-wrapper">
            <div class="login">
                <h3 class="login_title">Registracija korisnika</h3>

                <div class="form-group">
                    <asp:RequiredFieldValidator ID="RequiredFieldRegistredUserName" runat="server" ErrorMessage="Ovo polje je obavezno!" CssClass="error" ControlToValidate="txtRegistredUserName" Font-Size="10"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtRegistredUserName" runat="server" placeholder="Username" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:RequiredFieldValidator ID="RequiredFieldRegistredEmail" runat="server" ErrorMessage="Ovo polje je obavezno!" CssClass="error" ControlToValidate="txtRegistredEmail" Font-Size="10"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionRegistredEmail" runat="server" ErrorMessage="Unesite ispravnu e-mail adresu!" CssClass="error" Display="Dynamic" ControlToValidate="txtRegistredEmail" Font-Size="10" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="txtRegistredEmail" runat="server" placeholder="E-mail" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:RequiredFieldValidator ID="RequiredFieldRegistredPassword" runat="server" ErrorMessage="Ovo polje je obavezno!" CssClass="error" ControlToValidate="txtRegistredPassword" Font-Size="10"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtRegistredPassword" runat="server" placeholder="Password" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>                    
                

                <div class="form-group">
                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary btnLogin" Text="Registriraj se"  OnClick="btnLogin_Click"/>
                </div>
                <asp:Label ID="lblRegistrationInfo" runat="server" CssClass="error" ClientIDMode="Static" Font-Size="10"></asp:Label>
            </div>

            <small class="small-center">Imate već račun?  
                <asp:HyperLink NavigateUrl="default.aspx" Text="Prijava" runat="server"/>
            </small>
        </div>
    </section>

</asp:Content>

