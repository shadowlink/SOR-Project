using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSOR
{
    public partial class PujasVenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Id"] != null)
            {
                List<Puja> lv = new List<Puja>();
                var javaScriptSerializer = new JavaScriptSerializer();
                string jsonVentas = "";
                int idVenta = Int32.Parse(Request["id"]);

                Ventas serv = new Ventas();
                serv.Url = new Juddi().getServiceUrl("Ventas");
                jsonVentas = serv.getPujasVenta(idVenta);
                lv = javaScriptSerializer.Deserialize<List<Puja>>(jsonVentas);

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[4] { 
                        new DataColumn("idventa", typeof(int)),  
                                        new DataColumn("idpujador",typeof(int)),
                                        new DataColumn("cantidad", typeof(string)),  
                                        new DataColumn("Vender",typeof(string))
                    });

                for (int i = 0; i < lv.Count; i++)
                {
                    dt.Rows.Add(lv[i].ventaId, lv[i].pujadorId, lv[i].cantidad, "Vender");
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                Response.Redirect("/");
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
        }


        public void vender(int id, int pujadorId)
        {
            Ventas serv = new Ventas();

            serv.Url = new Juddi().getServiceUrl("Ventas");
            serv.finalizarVenta(id, 4, pujadorId);

            Response.Redirect("NuevaVenta.aspx");
        }
    }
}