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
    public partial class Vendedor : Form
    {
        public MySqlConnection myCon = null;
        string idEliminar = "";
        string nomEliminar = "";

        public Vendedor()
        {
            InitializeComponent();
            gbInsertar.Hide();
            gbModificar.Hide();
            conectar();
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            
        }
        public Vendedor(MySqlConnection conex)
        {
            InitializeComponent();
            this.myCon = conex;
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
           //     MySqlConnection myCon = Connection.conex();
             //   myCon.Open();

                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo aislamiento
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = myCon;
                comando.CommandText = ("SELECT * FROM vendedor WHERE eliminada = 0;");

                comando.Transaction = transaccion;          // comando transaccion
                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns[5].Visible = false;

                transaccion.Commit();       // confirmar transaccion
               // myCon.Close();
            }
            catch (Exception error)
            {
                transaccion.Rollback();  // deshacer transaccion
                MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
            }
        }

        private void eliminarVendedor()
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
                comando.CommandText = "Update megatubos.vendedor SET Eliminada = 1 WHERE (idVendedor = '" + idEliminar + "'  and Eliminada = 0);";

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
                nomEliminar = "";
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
            MySqlTransaction transaccion = null;
            try
            {
               // MySqlConnection myCon = Connection.conex();
                //myCon.Open();

                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo aislamiento
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = myCon;
                string fecha = dateTimePicker1.Value.ToString("yyyy/MM/dd");
                comando.CommandText = "INSERT INTO megatubos.vendedor(Nombre, Cant_Vendida, Telefono, Fecha, Eliminada) values ('" +
                    tbNombreInsertar.Text + "','" + tbCantInsertar.Text + "','" + tbTelInsertar.Text + "','" + fecha + "', '0');";

                comando.Transaction = transaccion;           // comando transaccion
                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Refresh();

                transaccion.Commit();           // confirmar transaccion
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
           
            if (radioButton1.Checked)
            {
                MySqlTransaction transaccion = null;
                try
                {
                   // MySqlConnection myCon = Connection.conex();
                    //myCon.Open();

                    transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo aislamiento
                    MySqlCommand comando = new MySqlCommand();
                    comando.Connection = myCon;
                    comando.CommandText = "Select * from vendedor WHERE (idVendedor = '" + tbBuscar.Text + "' and Eliminada = 0)";

                    comando.Transaction = transaccion;          // comando transaccion
                    MySqlDataAdapter adaptador = new MySqlDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dataGridView1.DataSource = tabla;
                    string fecha = dateTimePicker3.Value.ToString("yyyy/MM/dd");
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        // if (Convert.ToString(row.Cells["idCliente"].Value) == tbBuscar.Text && tbBuscar.Text != "")
                        // 
                        idEliminar = Convert.ToString(row.Cells["idVendedor"].Value);
                        nomEliminar = Convert.ToString(row.Cells["Nombre"].Value);

                        tbidModificar.Text = Convert.ToString(row.Cells["idVendedor"].Value);
                        tbNomModificar.Text = Convert.ToString(row.Cells["Nombre"].Value);
                        tbCantModificar.Text = Convert.ToString(row.Cells["Cant_Vendida"].Value);
                        tbTelModificar.Text = Convert.ToString(row.Cells["Telefono"].Value);
                        dateTimePicker3.Text = Convert.ToString(row.Cells["Fecha"].Value);
                        // }
                        break;
                    }
                    dataGridView1.Refresh();
                    transaccion.Commit();           // confirmar transaccion
                    //myCon.Close();
                    //conectar();
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
                   // MySqlConnection myCon = Connection.conex();
                    //myCon.Open();

                    transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo aislamiento
                    MySqlCommand comando = new MySqlCommand();
                    comando.Connection = myCon;
                    comando.CommandText = "Select * from vendedor WHERE (Nombre = '" + tbBuscar.Text + "' and Eliminada = 0)";

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
                        idEliminar = Convert.ToString(row.Cells["idVendedor"].Value);
                        nomEliminar = Convert.ToString(row.Cells["Nombre"].Value);

                        tbidModificar.Text = Convert.ToString(row.Cells["idVendedor"].Value);
                        tbNomModificar.Text = Convert.ToString(row.Cells["Nombre"].Value);
                        tbCantModificar.Text = Convert.ToString(row.Cells["Cant_Vendida"].Value);
                        tbTelModificar.Text = Convert.ToString(row.Cells["Telefono"].Value);
                        dateTimePicker3.Text = Convert.ToString(row.Cells["Fecha"].Value);
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
                    comando.CommandText = "Select * from vendedor WHERE (Fecha = '" + tbBuscar.Text + "' and Eliminada = 0)";

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
                        idEliminar = Convert.ToString(row.Cells["idVendedor"].Value);
                        nomEliminar = Convert.ToString(row.Cells["Nombre"].Value);

                        tbidModificar.Text = Convert.ToString(row.Cells["idVendedor"].Value);
                        tbNomModificar.Text = Convert.ToString(row.Cells["Nombre"].Value);
                        tbCantModificar.Text = Convert.ToString(row.Cells["Cant_Vendida"].Value);
                        tbTelModificar.Text = Convert.ToString(row.Cells["Telefono"].Value);
                        dateTimePicker3.Text = Convert.ToString(row.Cells["Fecha"].Value);
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


            if (!string.IsNullOrWhiteSpace(tbBuscar.Text))
            {
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
            }
            else
            {
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
            }

            ///
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            conectar();
            tbBuscar.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // messagebox
            string message = "Desea eliminar a: " + nomEliminar + " con el Id: " + idEliminar;
            string caption = "Eliminacion de vendedor";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                // Closes the parent form.
                eliminarVendedor();
            }
        }

        private void btnGuardarModificar_Click(object sender, EventArgs e)
        {
            string fecha = dateTimePicker3.Value.ToString("yyyy/MM/dd");
            //Modificiar
            MySqlTransaction transaccion = null;
            try
            {
                //MySqlConnection myCon = Connection.conex();
                //myCon.Open();
                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo aislamiento
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = myCon;
                comando.CommandText = "UPDATE vendedor SET Nombre = '" +
                  tbNomModificar.Text + "',Telefono = '" + tbTelModificar.Text + "',Cant_Vendida = '" + tbCantModificar.Text + "' ,Fecha = '" + fecha + "'" +
                  " WHERE (idVendedor = '" + tbidModificar.Text + "' and Eliminada = 0);";

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
                tbNomModificar.Clear();
                tbCantModificar.Clear();
                tbTelModificar.Clear();


                btnModificar.Enabled = true;
                btnInsertar.Enabled = true;
                btnEliminar.Enabled = true;
                btnBuscar.Enabled = true;
                btnUpdate.Enabled = true;
                btnRegresar.Enabled = true;

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

        private void btnCancelarModificar_Click(object sender, EventArgs e)
        {
            gbModificar.Hide();
            tbidModificar.Clear();
            tbNomModificar.Clear();
            tbCantModificar.Clear();
            tbTelModificar.Clear();
          

            btnModificar.Enabled = true;
            btnInsertar.Enabled = true;
            btnEliminar.Enabled = true;
            btnBuscar.Enabled = true;
            btnUpdate.Enabled = true;
            btnRegresar.Enabled = true;

            groupBox1.Visible = true;
            groupBox2.Visible = true;
            dataGridView1.Visible = true;
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
            tbNombreInsertar.Clear();
            tbCantInsertar.Clear();
            tbTelInsertar.Clear();

            groupBox1.Visible = true;
            groupBox2.Visible = true;
            dataGridView1.Visible = true;

        }
    }
}
