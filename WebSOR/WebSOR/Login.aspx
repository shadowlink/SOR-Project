<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebSOR.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Login</h1>

    <p>
        Mail:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="tbMail" runat="server" Width="209px"></asp:TextBox>
    </p>
    <p>
        Password:
        <asp:TextBox ID="tbPass" runat="server" Width="208px" TextMode="Password"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="lbMessage" runat="server"></asp:Label>
    </p>
    <p>
        <asp:Button ID="btEntrar" runat="server" OnClick="btEntrar_Click" Text="Entrar" />
    </p>
    
</asp:Content>
