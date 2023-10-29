using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Proyecto_MegaTubos
{
    public partial class Usuarios : Form
    {
        public MySqlConnection myCon = Connection.conex();
        string idEliminar = "";
        string nomEliminar = "";
        public Usuarios()
        {
            InitializeComponent();
            myCon.Open();
            this.CenterToScreen();
            gbInsertar.Hide();
            gbModificar.Hide();
            conectar();
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            tbContraseniaModificar.UseSystemPasswordChar = true;
            tbContraseniaInsertar.UseSystemPasswordChar = true;
        }
        private void conectar()
        {
            MySqlTransaction transaccion = null;
            try
            {
              //  MySqlConnection myCon = Connection.conex();
               // myCon.Open();

                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo aislamiento
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = myCon;
                comando.CommandText = ("SELECT idUsuario as Usuario, Nombre, password(Password) as Password, Tipo as Cargo FROM megatubos.usuario " +
                                       "INNER JOIN megatubos.tipo_empleado " + 
                                       "ON megatubos.usuario.Tipo_Empleado_idTipo_Empleado = megatubos.tipo_empleado.idTipo_Empleado; ");

                comando.Transaction = transaccion;          // comando transaccion
                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;

                transaccion.Commit();       // confirmar transaccion
                //myCon.Close();
            }
            catch (Exception error)
            {
                transaccion.Rollback();  // deshacer transaccion
                MessageBox.Show("No se pudo ejecutar " + error.Message + "\n Vuelva a intentarlo");
            }
        }
        private void eliminarUsuario()
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
                comando.CommandText = "Delete from usuario WHERE (idUsuario = '" + idEliminar + "');";

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
                MessageBox.Show("No se pudo ejecutar " + error.Message + "\n Vuelva a intentarlo");
            }
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
            int valor;
            valor = cbTipoUsuario.SelectedIndex + 1;

            MySqlTransaction transaccion = null;
            try
            {
               // MySqlConnection myCon = Connection.conex();
               // myCon.Open();

                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo aislamiento
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = myCon;
                comando.CommandText = "INSERT INTO megatubos.Usuario(idUsuario, Nombre, Password, Tipo_Empleado_idTipo_Empleado) values ('" +
                    tbUsuarioInsertar.Text + "','" + tbNombreInsertar.Text + "','" + tbContraseniaInsertar.Text + "','" + valor + "');";

                comando.Transaction = transaccion;          // comando transaccion
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
                MessageBox.Show("No se pudo ejecutar " + error.Message + "\n Vuelva a intentarlo");
            }
        }

        private void btnCancelar1_Click(object sender, EventArgs e)
        {
            gbInsertar.Hide();
            tbNombreInsertar.Clear();
            tbUsuarioInsertar.Clear();
            tbContraseniaInsertar.Clear();
            cbTipoUsuario.Text = "";

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

            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnGuardarModificar_Click(object sender, EventArgs e)
        {
            int valor;
            valor = cbIdTipoModificar.SelectedIndex;
            valor = valor + 1;
            Console.WriteLine(valor);
            //Modificiar
            MySqlTransaction transaccion = null;
            try
            {
               // MySqlConnection myCon = Connection.conex();
               // myCon.Open();

                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo aislamiento
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = myCon;
                comando.CommandText = "UPDATE usuario SET Nombre = '" +
                  tbNomModificar.Text + "',Password = '" + tbContraseniaModificar.Text + "',Tipo_Empleado_idTipo_Empleado = '" + valor.ToString() + "'" +
                  " WHERE (idUsuario = '" + tbidUsuarioModificar.Text + "');";

                comando.Transaction = transaccion;      // comando transaccion
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
                tbidUsuarioModificar.Clear();
                tbNomModificar.Clear();
                tbContraseniaModificar.Clear();
                cbIdTipoModificar.Text = "";


                btnModificar.Enabled = true;
                btnInsertar.Enabled = true;
                btnEliminar.Enabled = true;
                btnBuscar.Enabled = true;
                btnUpdate.Enabled = true;

                groupBox1.Visible = true;
                groupBox2.Visible = true;
                dataGridView1.Visible = true;

            }
            catch (Exception error)
            {
                transaccion.Rollback();  // deshacer transaccion
                MessageBox.Show("No se pudo ejecutar " + error.Message + "\n Vuelva a intentarlo");
            }
        }

        private void btnCancelarModificar_Click(object sender, EventArgs e)
        {
            gbModificar.Hide();
            tbidUsuarioModificar.Clear();
            tbNomModificar.Clear();
            tbContraseniaModificar.Clear();
            cbIdTipoModificar.Text = "";


            btnModificar.Enabled = true;
            btnInsertar.Enabled = true;
            btnEliminar.Enabled = true;
            btnBuscar.Enabled = true;
            btnUpdate.Enabled = true;


            groupBox1.Visible = true;
            groupBox2.Visible = true;
            dataGridView1.Visible = true;

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
                eliminarUsuario();
            }

            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
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
                    comando.CommandText = "SELECT idUsuario as Usuario, Nombre, Password, Tipo as Cargo FROM megatubos.usuario " +
                                          "INNER JOIN megatubos.tipo_empleado " +
                                          "ON megatubos.usuario.Tipo_Empleado_idTipo_Empleado = megatubos.tipo_empleado.idTipo_Empleado " +
                                          "WHERE (idUsuario = '" + tbBuscar.Text + "' );";

                    comando.Transaction = transaccion;          // comando transaccion
                    MySqlDataAdapter adaptador = new MySqlDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dataGridView1.DataSource = tabla;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        
                        idEliminar = Convert.ToString(row.Cells["Usuario"].Value);
                        nomEliminar = Convert.ToString(row.Cells["Nombre"].Value);

                        tbidUsuarioModificar.Text = Convert.ToString(row.Cells["Usuario"].Value);
                        tbNomModificar.Text = Convert.ToString(row.Cells["Nombre"].Value);
                        tbContraseniaModificar.Text = Convert.ToString(row.Cells["Password"].Value);
                        //cbIdTipoModificar.SelectedIndex = Convert.ToInt32(row.Cells["Tipo_Empleado_idTipo_Empleado"].Value) - 1;
                        cbIdTipoModificar.SelectedItem = Convert.ToString(row.Cells["Cargo"].Value);
                        break;
                    }
                    dataGridView1.Refresh();

                    transaccion.Commit();           // confirmar transaccion
                    //myCon.Close();
                }
                catch (Exception error)
                {
                    transaccion.Rollback();  // deshacer transaccion
                    MessageBox.Show("No se pudo ejecutar " + error.Message + "\n Vuelva a intentarlo");
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
                    comando.CommandText = "SELECT idUsuario as Usuario, Nombre, Password, Tipo as Cargo FROM megatubos.usuario " +
                                          "INNER JOIN megatubos.tipo_empleado " +
                                          "ON megatubos.usuario.Tipo_Empleado_idTipo_Empleado = megatubos.tipo_empleado.idTipo_Empleado " +
                                          "WHERE (Nombre = '" + tbBuscar.Text + "')";

                    comando.Transaction = transaccion;      // comando transaccion
                    MySqlDataAdapter adaptador = new MySqlDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dataGridView1.DataSource = tabla;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {

                        idEliminar = Convert.ToString(row.Cells["Usuario"].Value);
                        nomEliminar = Convert.ToString(row.Cells["Nombre"].Value);

                        tbidUsuarioModificar.Text = Convert.ToString(row.Cells["Usuario"].Value);
                        tbNomModificar.Text = Convert.ToString(row.Cells["Nombre"].Value);
                        tbContraseniaModificar.Text = Convert.ToString(row.Cells["Password"].Value);
                        //cbIdTipoModificar.SelectedIndex = Convert.ToInt32(row.Cells["Tipo_Empleado_idTipo_Empleado"].Value) - 1;
                        cbIdTipoModificar.SelectedItem = Convert.ToString(row.Cells["Cargo"].Value);
                        break;
                    }

                    dataGridView1.Refresh();

                    transaccion.Commit();           // confirmar transaccion
                    //myCon.Close();
                }
                catch (Exception error)
                {
                    transaccion.Rollback();  // deshacer transaccion
                    MessageBox.Show("No se pudo ejecutar " + error.Message + "\n Vuelva a intentarlo");
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
                    comando.CommandText = "SELECT idUsuario as Usuario, Nombre, Password, Tipo as Cargo FROM megatubos.usuario " +
                                          "INNER JOIN megatubos.tipo_empleado " +
                                          "ON megatubos.usuario.Tipo_Empleado_idTipo_Empleado = megatubos.tipo_empleado.idTipo_Empleado " +
                                          "WHERE (Tipo = '" + tbBuscar.Text + "')";

                    comando.Transaction = transaccion;          // comando transaccion
                    MySqlDataAdapter adaptador = new MySqlDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dataGridView1.DataSource = tabla;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
  
                        idEliminar = Convert.ToString(row.Cells["Usuario"].Value);
                        nomEliminar = Convert.ToString(row.Cells["Nombre"].Value);

                        tbidUsuarioModificar.Text = Convert.ToString(row.Cells["Usuario"].Value);
                        tbNomModificar.Text = Convert.ToString(row.Cells["Nombre"].Value);
                        tbContraseniaModificar.Text = Convert.ToString(row.Cells["Password"].Value);
                        //cbIdTipoModificar.SelectedIndex = Convert.ToInt32(row.Cells["Tipo_Empleado_idTipo_Empleado"].Value) - 1;
                        //cbIdTipoModificar.Select(Convert.ToString(row.Cells["Nombre"].Value));
                        cbIdTipoModificar.SelectedItem = Convert.ToString(row.Cells["Cargo"].Value);
                        break;

                    }

                    dataGridView1.Refresh();

                    transaccion.Commit();           // confirmar transaccion
                    //myCon.Close();
                }
                catch (Exception error)
                {
                    transaccion.Rollback();  // deshacer transaccion
                    MessageBox.Show("No se pudo ejecutar " + error.Message + "\n Vuelva a intentarlo");
                }
            }


            if (!string.IsNullOrWhiteSpace(tbBuscar.Text) && radioButton1.Checked == true || 
                !string.IsNullOrWhiteSpace(tbBuscar.Text) && radioButton2.Checked == true ||
                !string.IsNullOrWhiteSpace(tbBuscar.Text) && radioButton3.Checked == true)
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
    }
}
