using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace Repo.App.clases
{
    public partial class princ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String cad1 = Request.Form["fini"].ToString();
            string anio1 = cad1.Substring(0, 4);
            string mes1 = cad1.Substring(5, 2);
            string dia1 = cad1.Substring(8, 2);
            string fini = anio1 + mes1 + dia1;

            String cad2 = Request.Form["ffin"].ToString();
            string anio2 = cad2.Substring(0, 4);
            string mes2 = cad2.Substring(5, 2);
            string dia2 = cad2.Substring(8, 2);
            string ffin = anio2 + mes2 + dia2;

            string centro = Request.Form["cen"].ToString();

            string cadena = "";


            DataTable tb = new DataTable();


            try
            {

                SqlConnection myConnection = new SqlConnection("Data Source=10.10.7.3;Initial Catalog=PRO; Uid=sa; Pwd=MDm1210sPx; Persist Security Info=True");
                myConnection.Open();
                SqlCommand comando = new SqlCommand("[dbo].[spa_pvsrcentro]", myConnection);

                comando.Parameters.AddWithValue("@fechaini", fini);
                comando.Parameters.AddWithValue("@fechafin", ffin);
                comando.Parameters.AddWithValue("@centro", centro);




                SqlDataAdapter da = new SqlDataAdapter();
                comando.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = comando;

                da.Fill(tb);

                myConnection.Close();



            }
            catch (Exception ex)
            {
                throw;
            }


            cadena += ConvertDataTableToHTML(tb);
            Response.Write(cadena);

        }


        [System.Web.Services.WebMethod]
        public static string JqueryAjaxCall (string feini, string fefin, string centro, string linea){

            String cad1 = feini;
            string anio1 = cad1.Substring(0, 4);
            string mes1 = cad1.Substring(5, 2);
            string dia1 = cad1.Substring(8, 2);
            string fini = anio1 + mes1 + dia1;

            String cad2 = fefin;
            string anio2 = cad2.Substring(0, 4);
            string mes2 = cad2.Substring(5, 2);
            string dia2 = cad2.Substring(8, 2);
            string ffin = anio2 + mes2 + dia2;
            

            string cadena = "";


            DataTable tb = new DataTable();


            try
            {

                SqlConnection myConnection = new SqlConnection("Data Source=10.10.7.3;Initial Catalog=PRO; Uid=sa; Pwd=MDm1210sPx; Persist Security Info=True");
                myConnection.Open();
                SqlCommand comando = new SqlCommand("[dbo].[spa_pvsrgeneral]", myConnection);

                comando.Parameters.AddWithValue("@fechaini", fini);
                comando.Parameters.AddWithValue("@fechafin", ffin);
                comando.Parameters.AddWithValue("@centro", centro);
                comando.Parameters.AddWithValue("@linea", linea);




                SqlDataAdapter da = new SqlDataAdapter();
                comando.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = comando;

                da.Fill(tb);

                myConnection.Close();



            }
            catch (Exception ex)
            {
                throw;
            }


            cadena += ConvertDataTableToHTML(tb);

            return cadena;

        }

        [System.Web.Services.WebMethod]
        public static string FilOrdFab(string ofa){
            string cadena = "";

            DataTable tb = new DataTable();

            try
            {

                SqlConnection myConnection = new SqlConnection("Data Source=10.10.7.3;Initial Catalog=PRO; Uid=sa; Pwd=MDm1210sPx; Persist Security Info=True");
                myConnection.Open();
                SqlCommand comando = new SqlCommand("[dbo].[spa_pvsrorden]", myConnection);

                comando.Parameters.AddWithValue("@ofa", ofa);




                SqlDataAdapter da = new SqlDataAdapter();
                comando.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = comando;

                da.Fill(tb);

                myConnection.Close();



            }
            catch (Exception ex)
            {
                throw;
            }

            cadena += ConvertDataTableToHTML(tb);
            return cadena;

        }


        [System.Web.Services.WebMethod]
        public static string Filtipo(string ofa, string tipo, decimal costost)
        {
            string cadena = "";

            DataTable tb = new DataTable();

            try
            {

                SqlConnection myConnection = new SqlConnection("Data Source=10.10.7.3;Initial Catalog=PRO; Uid=sa; Pwd=MDm1210sPx; Persist Security Info=True");
                myConnection.Open();
                SqlCommand comando = new SqlCommand("[dbo].[spa_pvsrtipo]", myConnection);

                comando.Parameters.AddWithValue("@ofa", ofa);
                comando.Parameters.AddWithValue("@tipo", tipo);
                comando.Parameters.AddWithValue("@costost", costost);


                SqlDataAdapter da = new SqlDataAdapter();
                comando.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = comando;

                da.Fill(tb);

                myConnection.Close();



            }
            catch (Exception ex)
            {
                throw;
            }

            cadena += ConvertDataTableToHTML(tb);
            return cadena;

        }


        public static string ConvertDataTableToHTML(DataTable dt)
        {
            //string html = "<table>";
            StringBuilder html = new StringBuilder();
            //add header row
            html.Append("<table class=\"table table-hover princ\" cellspacing=\"0\" rules=\"all\" border=\"1\" id=\"tabla1\" style=\"border-collapse:collapse; \">");

            for (int i = 0; i < dt.Columns.Count; i++)
                if (i == 0)
                {
                    html.Append("<thead style=\"font-size: 12px\"><tr>");
                    html.Append("<th scope=\"col\">" + dt.Columns[i].ColumnName + "</th>");
                }
                else
                {
                    html.Append("<th scope=\"col\">" + dt.Columns[i].ColumnName + "</th>");
                }

            html.Append("</tr></thead>");
            //add rows
            html.Append("<tbody style=\"font-size: 12px\" class=\"table table - bordered\">");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html.Append("<tr>");
                for (int j = 0; j < dt.Columns.Count; j++)
                    switch (j)
                    {
                        case 0:
                            html.Append("<td>" + dt.Rows[i][j].ToString() + "</td>");
                            break;
                        default:
                            html.Append("<td>" + dt.Rows[i][j].ToString() + "</td>");
                            break;
                    }
                    
                        



                html.Append("</tr>");
            }
            html.Append("</tbody>");
            html.Append("</table>");
            return html.ToString();
        }








    }
}
