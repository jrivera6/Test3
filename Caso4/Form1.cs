using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caso4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString);

        public void ListaAños()
        {
            using (SqlCommand cmd = new SqlCommand("usp_listaanios", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "pedidos");
                        cbAños.DataSource = df.Tables["pedidos"];
                        cbAños.DisplayMember = "anios";
                        cbAños.ValueMember = "anios";
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListaAños();
        }

        private void cbAños_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand("usp_lista_pedidos_mes", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@anio", cbAños.SelectedValue);

                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "pedidos");
                        cbMes.DataSource = df.Tables["pedidos"];
                        cbMes.DisplayMember = "meses";
                        cbMes.ValueMember = "meses";
                    }
                }
            }
        }

        private void cbMes_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand("usp_lista_pedidosxmeses", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@anio", cbAños.SelectedValue);
                    da.SelectCommand.Parameters.AddWithValue("@mes", cbMes.SelectedValue);

                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "Pedidos");
                        dgPedidos.DataSource = df.Tables["Pedidos"];
                        //lblPedido.Text = df.Tables["Pedidos"].Rows.Count.ToString();
                    }
                }
            }
        }
    }
}
