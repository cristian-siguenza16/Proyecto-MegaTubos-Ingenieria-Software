using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_MegaTubos
{
    public partial class Graficas : Form
    {
        public MySqlConnection myCon = null;
        MySqlDataReader reader = null;
        private int mes_select = 11;

        public Graficas()
        {
            InitializeComponent();
        }

        public Graficas(MySqlConnection conex)
        {
            InitializeComponent();
            this.myCon = conex;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void query_grafica_productos()
        {
            chartProducto.Series["Productos"].Points.Clear();
            MySqlCommand comando = new MySqlCommand();
            comando.Connection = myCon;
            comando.CommandText = ("SELECT sum(CantidadVendida), producto.Descripcion FROM megatubos.salidas " + 
                "INNER JOIN megatubos.detalle_de_salida " +
                "ON detalle_de_salida.salidas_ID = salidas.ID " +
                "INNER JOIN megatubos.producto " +
                "ON detalle_de_salida.Producto_Codigo = producto.Codigo " +
                "WHERE year(salidas.Fecha) = 2023 AND month(salidas.Fecha) =" + mes_select.ToString() + " AND salidas.Eliminada=0 " +
                " group by detalle_de_salida.Producto_Codigo LIMIT 5;");

            reader = comando.ExecuteReader();

            while (reader.Read())
            {
                int num = reader.GetInt16(0);
                string desc = reader.GetString(1);
                chartProducto.Series["Productos"].Points.AddXY(desc, num);

            }
            myCon.Close();
            myCon.Open();
        }

        private void query_grafica_clientes()
        {
            chartClientes.Series["Clientes"].Points.Clear();
            MySqlCommand comando = new MySqlCommand();
            comando.Connection = myCon;
            comando.CommandText = ("SELECT Nombre, sum(total) FROM megatubos.salidas " +
                "INNER JOIN megatubos.cliente " +
                "ON cliente.idCliente = salidas.Cliente_idCliente " +
                "WHERE year(salidas.Fecha) = 2023 AND month(salidas.Fecha) =" + mes_select.ToString() + " AND salidas.Eliminada=0 " +
                " group by cliente.idCliente LIMIT 5;");

            reader = comando.ExecuteReader();

            while (reader.Read())
            {
                int num = reader.GetInt16(1);
                string desc = reader.GetString(0);
                chartClientes.Series["Clientes"].Points.AddXY(desc, num);
            }
            myCon.Close();
            myCon.Open();
        }

        private void cbMes_SelectedValueChanged(object sender, EventArgs e)
        {
            mes_select = cbMes.SelectedIndex + 1;
            query_grafica_productos();
            query_grafica_clientes();
        }

        private void Graficas_Load(object sender, EventArgs e)
        {
            cbMes.SelectedIndex = 10;
            query_grafica_productos();
            query_grafica_clientes();
        }
    }
}
