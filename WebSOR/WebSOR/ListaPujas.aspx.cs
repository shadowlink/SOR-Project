using Apache.NMS;
using Apache.NMS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSOR
{


    public partial class ListaPujas : System.Web.UI.Page
    {

        //ActiveMQ Colas 
        Uri connecturi = new Uri("activemq:tcp://localhost:61616");
        IConnectionFactory factory;
        const string BROKER = "tcp://localhost:61616";

        protected void Page_Load(object sender, EventArgs e)
        {
            factory = new NMSConnectionFactory(connecturi);
            loadVentas();

            //Topic Pujas
            string CLIENT_ID2 = Guid.NewGuid().ToString();
            string CONSUMER_ID2 = Guid.NewGuid().ToString();
            TopicSubscriber subscriber2 = new TopicSubscriber("Pujas", BROKER, CLIENT_ID2, CONSUMER_ID2);
            subscriber2.OnMessageReceived += new MessageReceivedDelegate(topicPujasMessage);
        }

        public void loadVentas()
        {
            List<Venta> lv = new List<Venta>();
            var javaScriptSerializer = new JavaScriptSerializer();
            string jsonVentas = "";

            Ventas serv = new Ventas();
            serv.Url = new Juddi().getServiceUrl("Ventas");
            jsonVentas = serv.getVentas((int)Session["Id"]);
            lv = javaScriptSerializer.Deserialize<List<Venta>>(jsonVentas);
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[7] { 
                        new DataColumn("id", typeof(int)),  
                                        new DataColumn("tipo", typeof(string)),  
                                        new DataColumn("autor",typeof(string)),
                                        new DataColumn("estado",typeof(string)),
                                        new DataColumn("fechafin",typeof(string)),
                                        new DataColumn("pujamax",typeof(int)),
                                        new DataColumn("pujar",typeof(string))
                    });
            for (int i = 0; i < lv.Count; i++)
            {
                dt.Rows.Add(lv[i].id, lv[i].tipo, lv[i].autor, lv[i].estado, lv[i].fecha_F, lv[i].precio, "Pujar");
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();  
        }

        public void topicPujasMessage(string message)
        {
            Server.Transfer("ListaPujas.aspx", true);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Pujar_click")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                setPuja(int.Parse(row.Cells[0].Text));
            }  
        }

        public void setPuja(int id)
        {
            string jsonPuja = "";
            Auxiliar aux = new Auxiliar();
            var javaScriptSerializer = new JavaScriptSerializer();

            if (tbPuja.Text != "")
            {
                int i = 0;
                bool success = int.TryParse(tbPuja.Text, out i);
                if (success)
                {
                    Puja p = new Puja();
                    p.pujadorId = (int)Session["Id"];
                    p.ventaId = id;
                    p.cantidad = int.Parse(tbPuja.Text);

                    jsonPuja = javaScriptSerializer.Serialize(p);

                    using (IConnection connection = factory.CreateConnection())
                    using (ISession session = connection.CreateSession())
                    {
                        IDestination destination = SessionUtil.GetDestination(session, "queue://Pujas");
                        using (IMessageProducer producer = session.CreateProducer(destination))
                        {
                            connection.Start();
                            ITextMessage request = session.CreateTextMessage(jsonPuja);
                            producer.Send(request);
                            connection.Close();
                        }
                    }
                }
            }

            tbPuja.Text="";
            Response.Redirect("ListaPujas.aspx");
        }
    }
}