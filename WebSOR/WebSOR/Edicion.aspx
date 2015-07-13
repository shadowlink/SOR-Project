<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edicion.aspx.cs" Inherits="WebSOR.Edicion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Edicion</h1>

    <p>
        Fecha Fin: <asp:Calendar ID="Calendar1" runat="server" Height="119px" Width="291px"></asp:Calendar>
    </p>

    <p>
        Hora:&nbsp;&nbsp;&nbsp; 
        <asp:TextBox ID="tbHora" runat="server" Width="34px" ></asp:TextBox> : 
        <asp:TextBox ID="tbMin" runat="server" Width="50px" ></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="lbMessage" runat="server" Text=""></asp:Label>
    </p>
    <p>
        <asp:Button ID="Button1" runat="server" Text="Guardar" OnClick="Button1_Click" />
    </p>
</asp:Content>
