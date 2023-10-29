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
    public partial class Producto : Form
    {
        public MySqlConnection myCon = null;
        private MySqlTransaction prueba = null;
        string idEliminar = "";
        string descripcionEliminar = "";

        public Producto()
        {
            InitializeComponent();
            gbInsertar.Hide();
            gbModificar.Hide();
            conectar();

            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }
        public Producto(MySqlConnection conex)
        {
            this.myCon = conex;
            InitializeComponent();
            gbInsertar.Hide();
            gbModificar.Hide();
            conectar();

            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }
        private void conectar()
        {
            MySqlTransaction transaccion = null;
            try
            {
                //MySqlConnection myCon = Connection.conex();
                //myCon.Open();
                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo aislamiento
                MySqlCommand comando = new MySqlCommand();
                //prueba = transaccion;
                comando.Connection = myCon;
                comando.CommandText = ("SELECT * FROM producto WHERE eliminado = 0;");

                comando.Transaction = transaccion;      // comando transaccion
                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns[4].Visible = false;

                transaccion.Commit();           // confirmar transaccion
                //myCon.Close();
            }
            catch (Exception error)
            {
                transaccion.Rollback();  // deshacer transaccion
                MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
            }
        }
         

        private bool verificarCodigo(string texto)
        {
            conectar();
            bool salir = false;
            Console.WriteLine(texto);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                Console.WriteLine(Convert.ToString(row.Cells["Codigo"].Value));
                if (Convert.ToString(row.Cells["Codigo"].Value) != texto && !string.IsNullOrWhiteSpace(texto))
                {
                    salir = true;
                }
                else
                {
                    salir = false;
                    break;
                }
            }
         
            return salir;
        }

        private void eliminarProducto()
        {
            ///Eliminar
            MySqlTransaction transaccion = null;
            try
            {
                //MySqlConnection myCon = Connection.conex();
                //myCon.Open();
                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo aislamiento
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = myCon;
                comando.CommandText = "Update megatubos.producto SET Eliminado = 1 WHERE (codigo = '" + idEliminar + "'  and Eliminado = 0);";

                comando.Transaction = transaccion;          // comando transaccion
                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Refresh();

                transaccion.Commit();           // confirmar transaccion
                //myCon.Close();
                conectar();

                idEliminar = "";
                descripcionEliminar = "";
            }
            catch (Exception error)
            {
                transaccion.Rollback();  // deshacer transaccion
                MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            //prueba.Rollback();
            //MessageBox.Show("Hizo un rollback de prueba");
            this.Close();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            gbInsertar.Show();
            gbInsertar.BringToFront();

            groupBox1.Visible = false;
            groupBox2.Visible = false;
            dataGridView1.Visible = false;
        }

        private void btnGuardar1_Click(object sender, EventArgs e)
        {
            if (verificarCodigo(tbCodigoInsertar.Text))
            {
                MySqlTransaction transaccion = null;
                try
                {
                  //  MySqlConnection myCon = Connection.conex();
                    //myCon.Open();
                    transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo aislamiento
                    //prueba = transaccion;
                    MySqlCommand comando = new MySqlCommand();
                    comando.Connection = myCon;
                    comando.CommandText = "Insert into producto (Codigo, Descripcion, Stock, PrecioCosto, Eliminado) values('" + tbCodigoInsertar.Text
                        + "','" + tbDescripcionInsertar.Text + "','" + tbStockInsertar.Text + "','" + tbPrecioInsertar.Text + "', '0')";

                    comando.Transaction = transaccion;          // comando transaccion
                    MySqlDataAdapter adaptador = new MySqlDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dataGridView1.DataSource = tabla;
                    dataGridView1.Refresh();

                    transaccion.Commit();       // confirmar transaccion
                    //myCon.Close();

                    gbInsertar.Hide();
                    conectar();

                    groupBox1.Visible = true;
                    groupBox2.Visible = true;
                    dataGridView1.Visible = true;
                }
                catch (Exception error)
                {
                    transaccion.Rollback();  // deshacer transaccion
                    MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
                }
            }
            else
            {
                MessageBox.Show("Verifique ingresar un codigo valido, y no repetido");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                MySqlTransaction transaccion = null;
                try
                {
                    //MySqlConnection myCon = Connection.conex();
                    //myCon.Open();

                    transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo aislamiento
                    MySqlCommand comando = new MySqlCommand();
                    comando.Connection = myCon;
                    comando.CommandText = "Select * from producto WHERE (codigo = '" + tbBuscar.Text + "' and Eliminado = 0);";

                    comando.Transaction = transaccion;          // comando transaccion
                    MySqlDataAdapter adaptador = new MySqlDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dataGridView1.DataSource = tabla;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        // if (Convert.ToString(row.Cells["idCliente"].Value) == tbBuscar.Text && tbBuscar.Text != "")
                        // {
                        idEliminar = Convert.ToString(row.Cells["Codigo"].Value);
                        descripcionEliminar = Convert.ToString(row.Cells["Descripcion"].Value);

                        tbidModificar.Text = Convert.ToString(row.Cells["Codigo"].Value);
                        tbDescriModificar.Text = Convert.ToString(row.Cells["Descripcion"].Value);
                        tbStockModificar.Text = Convert.ToString(row.Cells["Stock"].Value);
                        tbPrecioModificar.Text = Convert.ToString(row.Cells["PrecioCosto"].Value);
                        // }
                        break;
                    }
                    dataGridView1.Refresh();

                    transaccion.Commit();           // confirmar transaccion
                    //myCon.Close();
                }
                catch (Exception error)
                {
                    transaccion.Rollback();  // deshacer transaccion
                    MessageBox.Show("No se pudo ejecutar" + error.Message);
                }
            }
            else if (radioButton2.Checked)
            {
                MySqlTransaction transaccion = null;
                try
                {
                    //MySqlConnection myCon = Connection.conex();
                    //myCon.Open();

                    transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo aislamiento
                    MySqlCommand comando = new MySqlCommand();
                    comando.Connection = myCon;
                    comando.CommandText = "Select * from producto WHERE (descripcion = '" + tbBuscar.Text + "' and Eliminado = 0);";

                    comando.Transaction = transaccion;          // comando transaccion
                    MySqlDataAdapter adaptador = new MySqlDataAdapter(); 
                    adaptador.SelectCommand = comando;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dataGridView1.DataSource = tabla;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        // if (Convert.ToString(row.Cells["idCliente"].Value) == tbBuscar.Text && tbBuscar.Text != "")
                        // {
                        idEliminar = Convert.ToString(row.Cells["Codigo"].Value);
                        descripcionEliminar = Convert.ToString(row.Cells["Descripcion"].Value);

                        tbidModificar.Text = Convert.ToString(row.Cells["Codigo"].Value);
                        tbDescriModificar.Text = Convert.ToString(row.Cells["Descripcion"].Value);
                        tbStockModificar.Text = Convert.ToString(row.Cells["Stock"].Value);
                        tbPrecioModificar.Text = Convert.ToString(row.Cells["PrecioCosto"].Value);
                        // }
                        break;

                    }
                    dataGridView1.Refresh();

                    transaccion.Commit();           // confirmar transaccion
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

                    transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo aislamiento
                    MySqlCommand comando = new MySqlCommand();
                    comando.Connection = myCon;
                    comando.CommandText = "Select * from producto WHERE (stock = '" + tbBuscar.Text + "' and Eliminado = 0);";

                    comando.Transaction = transaccion;          // comando transaccion
                    MySqlDataAdapter adaptador = new MySqlDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dataGridView1.DataSource = tabla;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        // if (Convert.ToString(row.Cells["idCliente"].Value) == tbBuscar.Text && tbBuscar.Text != "")
                        // {
                        idEliminar = Convert.ToString(row.Cells["Codigo"].Value);
                        descripcionEliminar = Convert.ToString(row.Cells["Descripcion"].Value);

                        tbidModificar.Text = Convert.ToString(row.Cells["Codigo"].Value);
                        tbDescriModificar.Text = Convert.ToString(row.Cells["Descripcion"].Value);
                        tbStockModificar.Text = Convert.ToString(row.Cells["Stock"].Value);
                        tbPrecioModificar.Text = Convert.ToString(row.Cells["PrecioCosto"].Value);
                        // }
                        break;

                    }
                    dataGridView1.Refresh();

                    transaccion.Commit();           // confirmar transaccion
                    //myCon.Close();
                }
                catch (Exception error)
                {
                    transaccion.Rollback();  // deshacer transaccion
                    MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
                }
            }

            if(!string.IsNullOrWhiteSpace(tbBuscar.Text))
            {
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
            }
            else
            {
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            conectar();
            tbBuscar.Clear();

            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // messagebox
            string message = "Desea eliminar a el producto con el codigo: " + idEliminar + "\n y la descripcion: " + descripcionEliminar;
            string caption = "Eliminacion de producto";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                // Closes the parent form.
                eliminarProducto();
            }

            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnGuardarModificar_Click(object sender, EventArgs e)
        {
            //Modificiar
            MySqlTransaction transaccion = null;
            try
            {
                //MySqlConnection myCon = Connection.conex();
                //myCon.Open();

                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo aislamiento
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = myCon;
                comando.CommandText = "UPDATE megatubos.producto SET Descripcion = '" +
                  tbDescriModificar.Text + "',Stock = '" + tbStockModificar.Text + "',PrecioCosto = '" + tbPrecioModificar.Text + "'" +
                  " WHERE (Codigo = '" + tbidModificar.Text + "' and Eliminado = 0);";

                comando.Transaction = transaccion;          // comando transaccion
                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Refresh();

                transaccion.Commit();           // confirmar transaccion
                //myCon.Close();
                conectar();

                gbModificar.Hide();
                tbidModificar.Clear();
                tbDescriModificar.Clear();
                tbStockModificar.Clear();
                tbPrecioModificar.Clear();


                btnModificar.Enabled = true;
                btnInsertar.Enabled = true;
                btnEliminar.Enabled = true;
                btnBuscar.Enabled = true;
                btnUpdate.Enabled = true;
                btnRegresar.Enabled = true;

                groupBox1.Visible = true;
                groupBox2.Visible = true;
                dataGridView1.Visible = true;

                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
            }
            catch (Exception error)
            {
                transaccion.Rollback();  // deshacer transaccion
                MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
            }
        }

        private void btnCancelarModificar_Click(object sender, EventArgs e)
        {
            gbModificar.Hide();
            tbidModificar.Clear();
            tbDescriModificar.Clear();
            tbStockModificar.Clear();
            tbPrecioModificar.Clear();


            btnModificar.Enabled = true;
            btnInsertar.Enabled = true;
            btnEliminar.Enabled = true;
            btnBuscar.Enabled = true;
            btnUpdate.Enabled = true;
            btnRegresar.Enabled = true;

            groupBox1.Visible = true;
            groupBox2.Visible = true;
            dataGridView1.Visible = true;

            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            gbModificar.Show();
            gbModificar.BringToFront();

            groupBox1.Visible = false;
            groupBox2.Visible = false;
            dataGridView1.Visible = false;
        }

        private void btnCancelar1_Click(object sender, EventArgs e)
        {
            gbInsertar.Hide();
            tbDescripcionInsertar.Clear();
            tbStockInsertar.Clear();
            tbPrecioInsertar.Clear();

            groupBox1.Visible = true;
            groupBox2.Visible = true;
            dataGridView1.Visible = true;
        }
    }
}
