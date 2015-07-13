<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="WebSOR.Perfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <h1>Perfil</h1>
        <div class="row">
            <div class="col-md-6">
                <h4>Datos</h4>
                <p>
                    Nombre: 
                    <asp:TextBox ID="tbNombre" runat="server" Width="196px"></asp:TextBox>
                </p>
                <p>
                    e-Mail:&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="tbMail" runat="server" Width="192px"></asp:TextBox>
                </p>

            </div>
            
            <div class="col-md-6">
                <h4>Cambiar password</h4>
                <p>
                    Nuevo password 1: 
                    <asp:TextBox ID="tbPass1"  TextMode="Password" runat="server" Width="190px"></asp:TextBox>
                </p>
                <p>
                    Nuevo password 2: 
                    <asp:TextBox ID="tbPass2"  TextMode="Password" runat="server" Width="187px"></asp:TextBox>
                </p>
            </div>
        </div>
        <asp:Label ID="lbMessage" runat="server"></asp:Label>
        <br />
        <asp:Button ID="btConfirm" runat="server" Text="Confirmar" OnClick="btConfirm_Click" />
    </div>
</asp:Content>
