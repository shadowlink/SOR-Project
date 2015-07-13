<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevaVenta.aspx.cs" Inherits="WebSOR.NuevaVenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
        </div>
        <div class="row">
            <div class="col-md-6">
                <h1>Nueva Venta</h1>
                <p>
                    Tipo:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="tbTipo" runat="server" Width="209px"></asp:TextBox>
                </p>
                <p>
                    Autor:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="tbAutor" runat="server" Width="212px" ></asp:TextBox>
                </p>
                <p>
                    Año:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="tbAnyo" runat="server" Width="212px" ></asp:TextBox>
                </p>
                <p>
                    Estado: <asp:TextBox ID="tbEstado" runat="server" Width="212px"></asp:TextBox>
                </p>
                <p>
                    Fecha Fin: <asp:Calendar ID="Calendar1" runat="server" Height="119px" Width="291px"></asp:Calendar>
                </p>
                <p>
                    Hora:&nbsp;&nbsp;&nbsp; 
                    <asp:TextBox ID="tbHora" runat="server" Width="34px" ></asp:TextBox> : 
                    <asp:TextBox ID="tbMin" runat="server" Width="50px" ></asp:TextBox>
                </p>
                <p>
                    Precio: <asp:TextBox ID="tbPrecio" runat="server" Width="121px"></asp:TextBox> &nbsp;€
                </p>
                    Negociado: <asp:DropDownList ID="ddNegociado" runat="server">
                                    <asp:ListItem Text="Automático" Value="Automático" Selected="true"></asp:ListItem>
                                    <asp:ListItem Text="Manual" Value="Manual" Selected="false"></asp:ListItem>
                               </asp:DropDownList>
                <p>
                    <asp:Label ID="lbMessage" runat="server"></asp:Label>
                </p>
                <p>
                    <asp:Button ID="btEnviar" runat="server" Text="Enviar" OnClick="btEnviar_Click" Width="78px" />
                </p>
            </div>
            <div class="col-md-6">
                <h1>Ventas en curso</h1>
                <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"  Width="651px" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="id"></asp:BoundField>
                        <asp:BoundField DataField="negociado" HeaderText="negociado"></asp:BoundField>
                        <asp:BoundField DataField="tipo" HeaderText="Tipo"></asp:BoundField>
                        <asp:BoundField DataField="fechafin" HeaderText="Fecha Fin"></asp:BoundField>
                        <asp:BoundField DataField="pujamax" HeaderText="Puja Max."></asp:BoundField>
                        <asp:ButtonField ButtonType="Button" HeaderText="Vender" Text="Vender" CommandName="Vender_click" />
                        <asp:ButtonField ButtonType="Button"  CommandName="Cancelar_click" HeaderText="Cancelar" Text="Cancelar" />
                        <asp:ButtonField ButtonType="Button"  CommandName="Editar_click" HeaderText="Editar" Text="Editar" />
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
            </div>
        </div>
    </div>
</asp:Content>
