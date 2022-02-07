<%@ Page Title="" Language="C#" MasterPageFile="~/public.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="RWA._default" UnobtrusiveValidationMode="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <section class="container content-wrapper">
        <div class="form-wrapper">
            <div class="login">
                <h3 class="login_title">Prijava korisnika</h3>

                <div class="form-group">
                    <asp:RequiredFieldValidator ID="RequiredFieldUserName" runat="server" ErrorMessage="Ovo polje je obavezno!" CssClass="error" ControlToValidate="txtUserName" Font-Size="10"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtUserName" runat="server" placeholder="Username" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:RequiredFieldValidator ID="RequiredFieldPassword" runat="server" ErrorMessage="Ovo polje je obavezno!" CssClass="error" ControlToValidate="txtPassword" Font-Size="10"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>    

                <div class="form-group">
                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary btnLogin" Text="Prijava" OnClick="btnLogin_Click" />
                </div>
                <asp:Label ID="lblLoginInfo" runat="server" CssClass="error" ClientIDMode="Static" Font-Size="10"></asp:Label>
            </div>

            <small class="small-center">Ako nemate račun, registrirajte se!  
                <asp:HyperLink NavigateUrl="registration.aspx" Text="Registracija" runat="server"/>
            </small>
        </div>
    </section>

</asp:Content>
