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
    public partial class Historial : Form
    {
        public MySqlConnection myCon = null;
        public Historial()
        {
            InitializeComponent();
            conectar();
        }
        public Historial(MySqlConnection conex)
        {
            InitializeComponent();
            this.myCon = conex;
            conectar();
        }
        private void conectar()
        {
            MySqlTransaction transaccion = null;
            try
            {
            //    MySqlConnection myCon = Connection.conex();
               // myCon.Open();
                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable); // nivel de aislamiento
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = myCon;
                comando.CommandText = ("SELECT ID, Descripcion, salidas.Fecha, Producto_Codigo, CantidadVendida, Sub_Total, Total, Ferreteria, vendedor.nombre as Vendedor, cliente.Nombre as Cliente  FROM megatubos.detalle_de_salida " +
                    "INNER JOIN megatubos.salidas " +
                    "ON megatubos.detalle_de_salida.Salidas_ID = megatubos.salidas.ID " +
                    "INNER JOIN megatubos.cliente " +
                    "ON megatubos.salidas.Cliente_idCliente = megatubos.cliente.idCliente " +
                    "INNER JOIN megatubos.vendedor " +
                    "ON megatubos.salidas.Vendedor_idVendedor = megatubos.vendedor.idVendedor " +
                    "WHERE salidas.eliminada = 0; ");

                comando.Transaction = transaccion;      // comando tipo transaccion

                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns[4].Visible = false;
                transaccion.Commit();
             //   myCon.Close();
            }
            catch (Exception error)
            {
                transaccion.Rollback();  // deshacer transaccion
                MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            conectar();
            tbBuscar.Clear();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                MySqlTransaction transaccion = null;
                try
                {
               //     MySqlConnection myCon = Connection.conex();
                 //   myCon.Open();
                    transaccion = myCon.BeginTransaction(IsolationLevel.Serializable); // nivel de aislamiento
                    MySqlCommand comando = new MySqlCommand();
                    comando.Connection = myCon;
                    comando.CommandText = ("SELECT ID, Descripcion, Fecha, Producto_Codigo, CantidadVendida, Sub_Total, Total, Ferreteria, vendedor.Nombre as Vendedor, cliente.Nombre as Cliente  FROM megatubos.detalle_de_salida " +
                    "INNER JOIN megatubos.salidas " +
                    "ON megatubos.detalle_de_salida.Salidas_ID = megatubos.salidas.ID " +
                    "INNER JOIN megatubos.cliente " +
                    "ON megatubos.salidas.Cliente_idCliente = megatubos.cliente.idCliente " +
                    "INNER JOIN megatubos.vendedor " +
                    "ON megatubos.salidas.Vendedor_idVendedor = megatubos.vendedor.idVendedor" +
                    "WHERE (salidas.eliminada = 0 and ID = '" + tbBuscar.Text + "');");

                    comando.Transaction = transaccion; // comando tipo transaccion
                    MySqlDataAdapter adaptador = new MySqlDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dataGridView1.DataSource = tabla;
                    dataGridView1.Refresh();

                    transaccion.Commit(); // confirmar transaccion 
                   // myCon.Close();
                }
                catch (Exception error)
                {
                    transaccion.Rollback();  // deshacer transaccion
                    MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
                }
            }
            else if (radioButton2.Checked)
            {
                MySqlTransaction transaccion = null;
                try
                {
                    //MySqlConnection myCon = Connection.conex();
                    //myCon.Open();
                    transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // nivel de aislamiento
                    MySqlCommand comando = new MySqlCommand();
                    comando.Connection = myCon;
                    comando.CommandText = ("SELECT ID, Descripcion, Fecha, Producto_Codigo, CantidadVendida, Sub_Total, Total, Ferreteria, vendedor.Nombre as Vendedor, cliente.Nombre as Cliente  FROM megatubos.detalle_de_salida " +
                    "INNER JOIN megatubos.salidas " +
                    "ON megatubos.detalle_de_salida.Salidas_ID = megatubos.salidas.ID " +
                    "INNER JOIN megatubos.cliente " +
                    "ON megatubos.salidas.Cliente_idCliente = megatubos.cliente.idCliente " +
                    "INNER JOIN megatubos.vendedor " +
                    "ON megatubos.salidas.Vendedor_idVendedor = megatubos.vendedor.idVendedor" +
                    "WHERE (salidas.eliminada = 0 and Fecha = '" + tbBuscar.Text + "');");

                    comando.Transaction = transaccion;          // comando tipo transaccion
                    MySqlDataAdapter adaptador = new MySqlDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dataGridView1.DataSource = tabla;
                    dataGridView1.Refresh();

                    transaccion.Commit();               // confirmar transaccion
                    //myCon.Close();
                }
                catch (Exception error)
                {
                    transaccion.Rollback();  // deshacer transaccion
                    MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
                }
            }
            else if (radioButton3.Checked)
            {
                MySqlTransaction transaccion = null;
                try
                {
                    //MySqlConnection myCon = Connection.conex();
                    //myCon.Open();
                    transaccion = myCon.BeginTransaction(IsolationLevel.Serializable); // nivel de aislamiento
                    MySqlCommand comando = new MySqlCommand();
                    comando.Connection = myCon;
                    comando.CommandText = ("SELECT ID, Descripcion, Fecha, Producto_Codigo, CantidadVendida, Sub_Total, Total, Ferreteria, vendedor.Nombre as Vendedor, cliente.Nombre as Cliente  FROM megatubos.detalle_de_salida " +
                    "INNER JOIN megatubos.salidas " +
                    "ON megatubos.detalle_de_salida.Salidas_ID = megatubos.salidas.ID " +
                    "INNER JOIN megatubos.cliente " +
                    "ON megatubos.salidas.Cliente_idCliente = megatubos.cliente.idCliente " +
                    "INNER JOIN megatubos.vendedor " +
                    "ON megatubos.salidas.Vendedor_idVendedor = megatubos.vendedor.idVendedor" +
                    "WHERE (salidas.eliminada = 0 and Ferreteria = '" + tbBuscar.Text + "');");

                    comando.Transaction = transaccion;          // comando tipo transaccion
                    MySqlDataAdapter adaptador = new MySqlDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dataGridView1.DataSource = tabla;
                    dataGridView1.Refresh();

                    transaccion.Commit();       // confirmar transaccion
                    //myCon.Close();
                }
                catch (Exception error)
                {
                    transaccion.Rollback();  // deshacer transaccion
                    MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
                }

            }
        }

        private void Historial_Load(object sender, EventArgs e)
        {

        }
    }
}
