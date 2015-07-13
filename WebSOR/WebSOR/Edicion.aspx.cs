using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSOR
{
    public partial class Edicion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Id"] == null)
            {
                Response.Redirect("/");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bool validData = true;
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
                if (int.Parse(tbMin.Text) < 0 || int.Parse(tbMin.Text) > 60)
                {
                    validData = false;
                    lbMessage.ForeColor = Color.Red;
                    lbMessage.Text = "El campo 'Minutos' debe estar comprendido entre 0 y 60";
                }
            }

            DateTime date = Calendar1.SelectedDate.Date;
            if (validData)
            {
                date = date.AddHours(int.Parse(tbHora.Text)).AddMinutes(int.Parse(tbMin.Text));
                int result = DateTime.Compare(date, DateTime.Now);
                if (result >= 0)
                {
                    int idVenta = Int32.Parse(Request["id"]);
                    Ventas serv = new Ventas();
                    serv.Url = new Juddi().getServiceUrl("Ventas");
                    serv.editarVenta(idVenta, date);
                    Response.Redirect("NuevaVenta.aspx");
                }
            }
        }
    }
}