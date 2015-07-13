<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="WebSOR.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Nuevo usuario</h1>
    
    <p>
        Usuario:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="tbUser" runat="server" Width="212px"></asp:TextBox>
    </p>
    <p>
        Mail:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="tbMail" runat="server" Width="212px"></asp:TextBox>
    </p>
    <p>
        Password: <asp:TextBox ID="tbPass1" runat="server" Width="212px" TextMode="Password"></asp:TextBox>
    </p>
    <p>
        Password: <asp:TextBox ID="tbPass2" runat="server" Width="212px" TextMode="Password"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="lbMessage" runat="server"></asp:Label>
    </p>
    <p>
        <asp:Button ID="btcrear" runat="server" OnClick="btCrear_Click" Text="Crear" Width="63px" />
    </p>
</asp:Content>
