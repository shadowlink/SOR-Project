<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaPujas.aspx.cs" Inherits="WebSOR.ListaPujas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%if(Session["Id"]!=null){ %>

    <h1>Listado de Ventas</h1>
    <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"  Width="1362px" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id"></asp:BoundField>
            <asp:BoundField DataField="tipo" HeaderText="Tipo"></asp:BoundField>
            <asp:BoundField DataField="autor" HeaderText="Autor"></asp:BoundField>
            <asp:BoundField DataField="estado" HeaderText="Estado"></asp:BoundField>
            <asp:BoundField DataField="fechafin" HeaderText="Fecha Fin"></asp:BoundField>
            <asp:BoundField DataField="pujamax" HeaderText="Puja Max."></asp:BoundField>
            <asp:ButtonField ButtonType="Button" HeaderText="Pujar" Text="Pujar" CommandName="Pujar_click" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>

    <p>Nueva Puja<asp:TextBox ID="tbPuja" runat="server"></asp:TextBox>

    </p>

    <%} %>
</asp:Content>
