using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSOR
{
    public partial class NuevaVenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Id"] != null)
            {
                List<Venta> lv = new List<Venta>();
                var javaScriptSerializer = new JavaScriptSerializer();
                string jsonVentas = "";

                Ventas serv = new Ventas();
                serv.Url = new Juddi().getServiceUrl("Ventas");
                jsonVentas = serv.getVentasActivas((int)Session["id"]);
                lv = javaScriptSerializer.Deserialize<List<Venta>>(jsonVentas);

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[8] { 
                        new DataColumn("id", typeof(int)),  
                                        new DataColumn("negociado",typeof(int)),
                                        new DataColumn("tipo", typeof(string)),  
                                        new DataColumn("fechafin",typeof(string)),
                                        new DataColumn("pujamax",typeof(int)),
                                        new DataColumn("vender",typeof(string)),
                                        new DataColumn("cancelar",typeof(string)),
                                        new DataColumn("editar",typeof(string))
                    });

                for (int i = 0; i < lv.Count; i++)
                {
                    string fecha = lv[i].fecha_F;
                    if (Convert.ToDateTime(fecha) < DateTime.UtcNow)
                    {
                        dt.Rows.Add(lv[i].id, lv[i].negociado, lv[i].tipo, "FIN", lv[i].precio, "Vender", "Cancelar", "Editar");
                    }
                    else
                    {
                        DateTime utcDate = DateTime.SpecifyKind(Convert.ToDateTime(lv[i].fecha_F), DateTimeKind.Utc);
                        var localTime = utcDate.ToLocalTime();
                        dt.Rows.Add(lv[i].id, lv[i].negociado, lv[i].tipo, localTime, lv[i].precio, "Vender", "Cancelar", "Editar");
                    }
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                Response.Redirect("/");
            }
        }

        protected void btEnviar_Click(object sender, EventArgs e)
        {
            bool validData = true;
            Venta venta = new Venta();
            Auxiliar aux = new Auxiliar();
            string jsonVenta = "";

            //Comprobar que las horas y minutos son valores correctos
            int i = 0;
            bool success = int.TryParse(tbHora.Text, out i);
            if (!success)
            {
                validData = false;
                lbMessage.ForeColor = Color.Red;
                lbMessage.Text = "El campo 'Hora' debe contener un valor numérico";
            }
            else
            {
                if (int.Parse(tbHora.Text) < 0 || int.Parse(tbHora.Text) > 23)
                {
                    validData = false;
                    lbMessage.ForeColor = Color.Red;
                    lbMessage.Text = "El campo 'Hora' debe estar comprendido entre 0 y 23";
                }
            }

            success = int.TryParse(tbMin.Text, out i);
            if (!success)
            {
                validData = false;
                lbMessage.ForeColor = Color.Red;
                lbMessage.Text = "El campo 'Minutos' debe contener un valor numérico";
            }
            else
            {
                if (int.Parse(tbMin.Text) < 0 || int.Parse(tbMin.Text) >= 60)
                {
                    validData = false;
                    lbMessage.ForeColor = Color.Red;
                    lbMessage.Text = "El campo 'Minutos' debe estar comprendido entre 0 y 59";
                }
            }

            DateTime date = Calendar1.SelectedDate.Date;
            if (validData)
            {
                date = date.AddHours(int.Parse(tbHora.Text)).AddMinutes(int.Parse(tbMin.Text));
            }

            Ventas serv = new Ventas();
            serv.Url = new Juddi().getServiceUrl("Ventas");
            var javaScriptSerializer = new JavaScriptSerializer();

            //Comprobación de campos
            //Año numérico
            success = int.TryParse(tbAnyo.Text, out i);
            if (!success)
            {
                validData = false;
                lbMessage.ForeColor = Color.Red;
                lbMessage.Text = "El campo 'Año' debe contener un valor numérico";
            }

            //Fecha posterior a la actual
            int result = DateTime.Compare(date, DateTime.Now);
            if (result < 0)
            {
                validData = false;
                lbMessage.ForeColor = Color.Red;
                lbMessage.Text = "La fecha indicada ya ha pasado";
            }

            //Precio numérico
            success = int.TryParse(tbPrecio.Text, out i);
            if (!success)
            {
                validData = false;
                lbMessage.ForeColor = Color.Red;
                lbMessage.Text = "El campo 'Precio' debe contener un valor numérico";
            }

            if (validData)
            {
                int negociado = 0;
                if (ddNegociado.Text == "Automático")
                {
                    negociado = 1;
                }
                else
                {
                    negociado = 2;
                }

                venta.vendedor = (int)Session["Id"];
                venta.tipo = tbTipo.Text;
                venta.autor = tbAutor.Text;
                venta.año = int.Parse(tbAnyo.Text);
                venta.estado = tbEstado.Text;
                venta.fecha_F = date.ToString();
                venta.precio = int.Parse(tbPrecio.Text);
                venta.negociado = negociado;
                venta.idComprador = 0;

                jsonVenta = javaScriptSerializer.Serialize(venta);
                serv.nuevaVenta(jsonVenta);
                lbMessage.ForeColor = Color.Green;
                lbMessage.Text = "Venta añadida correctamente";
                Server.TransferRequest(Request.Url.AbsolutePath, false);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Vender_click")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                vender(int.Parse(row.Cells[0].Text), int.Parse(row.Cells[1].Text));
            }
            else if (e.CommandName == "Cancelar_click")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                cancelar(int.Parse(row.Cells[0].Text));

            }
            else if (e.CommandName == "Editar_click")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                editar(int.Parse(row.Cells[0].Text));
            }
        }

        public void vender(int id, int negociado)
        {
            Ventas serv = new Ventas();
            serv.Url = new Juddi().getServiceUrl("Ventas");
            if (negociado == 2) //Negociado Manual
            {
                Response.Redirect("PujasVenta.aspx?id="+id);
            }
            else
            {
                serv.Url = new Juddi().getServiceUrl("Ventas");
                serv.finalizarVenta(id, 1, 0);
            }
            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }

        public void cancelar(int id)
        {
            Ventas serv = new Ventas();
            serv.Url = new Juddi().getServiceUrl("Ventas");
            serv.finalizarVenta(id, 2, 0);
            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }


        public void editar(int id)
        {
            Response.Redirect("Edicion.aspx?id=" + id);
        }
    }
}