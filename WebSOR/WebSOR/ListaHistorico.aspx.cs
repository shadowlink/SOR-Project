using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSOR
{
    public partial class ListaHistorico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Id"] != null)
            {
                Auxiliar aux = new Auxiliar();
                HistoricoVentas hv = new HistoricoVentas();
                string jsonVentas = "";
                Historico serv = new Historico();
                serv.Url = new Juddi().getServiceUrl("Historico");
                jsonVentas = serv.devolverTodo((int)Session["Id"]);
                var javaScriptSerializer = new JavaScriptSerializer();
                hv = javaScriptSerializer.Deserialize<HistoricoVentas>(jsonVentas);
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[4] { 
                        new DataColumn("id", typeof(int)),  
                                        new DataColumn("tipo", typeof(string)),  
                                        new DataColumn("estado",typeof(string)),
                                        new DataColumn("precioventa",typeof(int))
                    });
                for (int i = 0; i < hv.ListaVentas.Count; i++)
                {
                    dt.Rows.Add(hv.ListaVentas[i].id, hv.ListaVentas[i].tipo, hv.ListaVentas[i].estado, hv.ListaVentas[i].precio);
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                Response.Redirect("/");
            }
        }
    }
}