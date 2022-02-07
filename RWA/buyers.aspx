<%@ Page Title="" Language="C#" MasterPageFile="~/private.Master" AutoEventWireup="true" CodeBehind="buyers.aspx.cs" Inherits="RWA.buyers" UnobtrusiveValidationMode="None" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.addEventListener('DOMContentLoaded', () => {
            const inputs = document.querySelectorAll('input[type=text]');
            inputs.forEach(input => input.classList.add('form-control'));
        });
    </script>
    <style type="text/css">
        .auto-style1 {
            --bs-table-bg: transparent;
            --bs-table-accent-bg: transparent;
            --bs-table-striped-color: #212529;
            --bs-table-striped-bg: rgba(0, 0, 0, 0.05);
            --bs-table-active-color: #212529;
            --bs-table-active-bg: rgba(0, 0, 0, 0.1);
            --bs-table-hover-color: #212529;
            --bs-table-hover-bg: rgba(0, 0, 0, 0.075);
            width: 100%;
            margin-bottom: 1rem;
            color: #212529;
            vertical-align: top;
            border-color: #dee2e6;
            margin-right: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">

        <div class="col-12 jumboton">
            <div class="cookie-holder">
                <span class="badge bg-success cookie-name">Pozdrav: "<asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>"</span>
                <span class="badge bg-primary">Vrijeme prijave: <asp:Label ID="lblTime" runat="server" Text=""></asp:Label></span>
            </div>
            <h2>Dohvat Kupaca</h2>
        </div>

        <div class="row">

            <div class="col-4">
                <div class="card bg-light mb-3" style="max-width: 100%;">
                    <div class="card-header">Odaberite državu i grad</div>
                    <div class="card-body">

                        <div class="form-buyers">
                            <asp:DropDownList ID="ddlDrzava" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDrzava_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="form-buyers">
                            <asp:DropDownList ID="ddlGrad" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlGrad_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <hr />

                        <div class="form-buyers">
                            <asp:Label Text="Unesi željeni broj kupaca" runat="server" CssClass="buyer-label" />
                            <asp:TextBox runat="server" ID="txtNumberOfBuyers" CssClass="form-control buyerInput" placeholder="Unesi broj kupaca"></asp:TextBox>

                            <div class="validation-holder">
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Vrijednost između 1-50" CssClass="error" Font-Size="10"
                                    ControlToValidate="txtNumberOfBuyers" MaximumValue="50" MinimumValue="1" Display="Dynamic" Type="Integer"></asp:RangeValidator>
                                <asp:RequiredFieldValidator ID="RequiredNumberOfBuyers" runat="server" ErrorMessage="Unesite broj kupaca za prikaz"
                                    CssClass="error" ControlToValidate="txtNumberOfBuyers" Font-Size="10" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div>
                            <asp:Button ID="btnNumberOfBuyers" runat="server" CssClass="btn btn-primary" Text="Dohvati kupce" OnClick="btnNumberOfBuyers_Click" AutoPostBack="true" />
                        </div>

                    </div>
                    <!-- End of Card-body -->
                </div>
                <!-- End of card bg-light mb-3 -->
            </div>
            <!-- End of col-4 -->

            <div class="col-4">
                <asp:Label ID="lblError" runat="server" Text="test"></asp:Label>
            </div> <!-- End of col-4 -->

        </div> <!-- End of row -->
    </div><!-- End of container -->




    <div class="container">
        <asp:GridView 
            ID="GridView1" 
            runat="server" 
            CssClass="auto-style1" 
            AutoGenerateColumns="False" 
            BackColor="White" 
            BorderColor="#DEDFDE" 
            BorderStyle="None" 
            BorderWidth="1px" 
            CellPadding="4" 
            ForeColor="Black" 
            GridLines="Vertical" 
            AllowPaging="True" 
            AllowSorting="True"
            DataSourceID="SqlDataSource1" >

            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="IdKupac" HeaderText="IdKupac" Visible="False" />
                <asp:BoundField DataField="Ime" HeaderText="Ime" SortExpression="Ime" />
                <asp:BoundField DataField="Prezime" HeaderText="Prezime" SortExpression="Prezime" />
                <asp:BoundField DataField="Email" HeaderText="E-mail" />
                <asp:BoundField DataField="Telefon" HeaderText="Telefon" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="BtnEdit"
                                    runat="server"
                                    Text="Uredi"
                                    CssClass="btn btn-sm btn-dark"
                                    CommandArgument='<%# Eval("IDKupac") %>'
                                    OnCommand="BtnEdit_Command" />
                        &nbsp;
                        <asp:Button ID="btnDetails"
                                    runat="server"
                                    Text="Detalji"
                                    CssClass="btn btn-sm btn-success"
                                    CommandArgument='<%# Eval("IDKupac") %>' 
                                    OnCommand="btnDetails_Command" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#68D3BE" Font-Bold="True" ForeColor="#ffffff" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SelectedRowStyle BackColor="#F8C8C2" ForeColor="#B82616" />
            <PagerSettings LastPageText="Zadnja" Mode="NumericFirstLast" NextPageText="Sljedeća" PreviousPageText="Previous" FirstPageText="Prva" Position="Bottom" />
            <PagerStyle Height="30px" VerticalAlign="Bottom" HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:AdventureWorksOBPConnectionString %>" SelectCommand="GetTopBuyersByCityByCountry" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtNumberOfBuyers" Name="numOfBuyers" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="ddlDrzava" Name="drzavaId" PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="ddlGrad" Name="gradId" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    <!-- End of container -->

</asp:Content>
