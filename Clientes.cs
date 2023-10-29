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
    
    public partial class Clientes : Form
    {
        string idEliminar = "";
        string nomEliminar = "";
        string idEliminarFerr = "";
        string nomEliminarFerr = "";
        string idFerrEliminarFerr = "";
        string nomFerrEliminarFerr = "";
        public MySqlConnection myCon = null;
        
       

        public Clientes()
        {
            InitializeComponent();
            gbInsertar.Hide();
            gbModificar.Hide();
            gbInsertarFerr.Hide();
            gbModificarFerr.Hide();
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            btnInsertarFerr.Enabled = false;
            btnModificarFerr.Enabled = false;
            btnEliminarFerr.Enabled = false;
            conectar();
        }
        public Clientes(MySqlConnection conex)
        {
            InitializeComponent();
            this.myCon = conex;
            gbInsertar.Hide();
            gbModificar.Hide();
            gbInsertarFerr.Hide();
            gbModificarFerr.Hide();
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            btnInsertarFerr.Enabled = false;
            btnModificarFerr.Enabled = false;
            btnEliminarFerr.Enabled = false;
            conectar();
        }
        public void abrir(MySqlConnection conexion)

        {
            myCon = conexion;
        }

        private void conectar()
        {
            MySqlTransaction transaccion = null;
            try
            {
               
                MySqlCommand comando = new MySqlCommand();
                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable); // tipo de aislamiento
                comando.Connection = myCon;
                comando.CommandText = ("SELECT idFerreteria, ferreteria.Nombre as Nombre_Ferreteria, Direccion, idCliente, cliente.Nombre, Telefono, NIT, Eliminada FROM megatubos.ferreteria "
                                      + "INNER JOIN megatubos.cliente "
                                      + "ON megatubos.cliente.idCliente = megatubos.ferreteria.Cliente_idCliente "
                                      + "WHERE cliente.eliminada = 0 "
                                      + "GROUP BY idFerreteria; ");

                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                comando.Transaction = transaccion; // comando tipo transaccion

                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns[7].Visible = false;

                transaccion.Commit();
            
            }
            catch (Exception error)
            {
                transaccion.Rollback();  // deshacer transaccion
                MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
            }
        }
        private void eliminarCliente()
        {
            ///Eliminar
            MySqlTransaction transaccion = null;
            try
            {
                
                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable); // nivel de aislamiento
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = myCon;
                comando.CommandText = "Update megatubos.cliente SET Eliminada = 1 WHERE (idCliente = '" + idEliminar + "'  and Eliminada = 0);";

                comando.Transaction = transaccion; // comando 1 tipo transaccion
                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comando; 
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView2.DataSource = tabla;
                dataGridView2.Refresh();
                //myCon.Close();

                //myCon.Open();
                comando.Connection = myCon;
                comando.CommandText = "Delete From megatubos.Ferreteria WHERE (Cliente_idCliente = '" + idEliminar + "');";

                comando.Transaction = transaccion; // comando 2 tipo transaccion

                adaptador.SelectCommand = comando;
                adaptador.Fill(tabla);
                dataGridView2.DataSource = tabla;
                dataGridView2.Refresh();

                transaccion.Commit();

              

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

        private void eliminarFerreteria()
        {
            ///Eliminar
            MySqlTransaction transaccion = null;
            try
            {
              
                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable); // nivel de aislamiento
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = myCon;
                comando.CommandText = "Delete From megatubos.Ferreteria WHERE (idFerreteria= '" + idFerrEliminarFerr + "');";

                comando.Transaction = transaccion; // comando tipo transaccion
                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Refresh();
                transaccion.Commit(); // confirmamos la transaccion
            
                conectar();

                idEliminarFerr = "";
                nomEliminarFerr = "";
            }
            catch (Exception error)
            {
                transaccion.Rollback();  // deshacer transaccion
                MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
            }
        }

        private void insertarFerreteria(string id, string buscar, string nit, string telefono)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (Convert.ToString(row.Cells["Nombre"].Value) == buscar && buscar != "" && buscar != "0")
                {
                    if (Convert.ToString(row.Cells["NIT"].Value) == nit && nit != "")
                    {
                        if (Convert.ToString(row.Cells["Telefono"].Value) == telefono && telefono != "")
                        {
                            MySqlTransaction transaccion = null;
                            try
                            {
                                
                                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable); // tipo de aislamiento
                                MySqlCommand comando = new MySqlCommand();
                                comando.Connection = myCon;
                                comando.CommandText = "Insert into ferreteria(Nombre, Direccion, Cliente_idCliente) values('" +
                                      tbFerrInsertar.Text + "','" + tbDirecInsertar.Text + "','" + Convert.ToString(row.Cells["idCliente"].Value) + "');";
                                comando.Transaction = transaccion; // comando tipo transaccion
                                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                                adaptador.SelectCommand = comando;
                                DataTable tabla = new DataTable();
                                adaptador.Fill(tabla);
                                dataGridView1.DataSource = tabla;
                                dataGridView1.Refresh();
                                transaccion.Commit(); // confirmamos la transaccion
                             
                            }
                            catch (Exception error)
                            {
                                transaccion.Rollback();  // deshacer transaccion
                                MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
                            }
                            break;
                        }
                    }
                }
                if (Convert.ToString(row.Cells["idCliente"].Value) == id && id != "" && id!="0")
                {
                    MySqlTransaction transaccion = null;
                    try
                    {
                        
                        transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);
                        MySqlCommand comando = new MySqlCommand();
                        comando.Connection = myCon;
                        comando.CommandText = "Insert into ferreteria(Nombre, Direccion, Cliente_idCliente) values('" +
                              tbFerrInsertar.Text + "','" + tbDirecInsertar.Text + "','" + Convert.ToString(row.Cells["idCliente"].Value) + "', '0');";

                        comando.Transaction = transaccion;
                        MySqlDataAdapter adaptador = new MySqlDataAdapter();
                        adaptador.SelectCommand = comando;
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);
                        dataGridView1.DataSource = tabla;
                        dataGridView1.Refresh();
                        transaccion.Commit(); //confirmamos la transaccion
                   
                    }
                    catch (Exception error)
                    {
                        transaccion.Rollback();  // deshacer transaccion
                        MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
                    }
                    break;
                }
            }
        }


        private void conectarCliente()
        {
            MySqlTransaction transaccion = null;
            try
            {
                
                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable); // inicializamos nivel de aislamiento
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = myCon;
                comando.CommandText = "SELECT * FROM megatubos.cliente WHERE (Eliminada = 0);";

                comando.Transaction = transaccion; // comando tipo transaccion
                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView2.DataSource = tabla;
                dataGridView2.Refresh();

                transaccion.Commit(); // confirma la transaccion
                
                conectar();
            }
            catch (Exception error)
            {
                transaccion.Rollback();  // deshacer transaccion
                MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //message box

            string message = "Desea eliminar a: " + nomEliminarFerr + " con el Id: " + idEliminar
                                +" \n Tambien se eliminaran todas las ferreterias conectadas a ese cliente";
            string caption = "Eliminacion de cliente";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                // Closes the parent form.
                eliminarCliente();
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            gbModificar.Show();
            gbModificar.BringToFront();
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            dataGridView1.Visible = false;
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                MySqlTransaction transaccion = null;
                try
                {
                    
                    transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);
                    MySqlCommand comando = new MySqlCommand();
                    comando.Connection = myCon;
                    comando.CommandText = ("SELECT idFerreteria, ferreteria.Nombre as Nombre_Ferreteria, Direccion, idCliente, cliente.Nombre, Telefono, NIT, Eliminada FROM megatubos.ferreteria "
                                      + "INNER JOIN megatubos.cliente "
                                      + "ON megatubos.cliente.idCliente = megatubos.ferreteria.Cliente_idCliente "
                                      + "WHERE (idCliente = '" + tbBuscar.Text + "' AND eliminada = 0)"
                                      + "GROUP BY idFerreteria; ");

                    comando.Transaction = transaccion;
                    MySqlDataAdapter adaptador = new MySqlDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dataGridView1.DataSource = tabla;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        // if (Convert.ToString(row.Cells["idCliente"].Value) == tbBuscar.Text && tbBuscar.Text != "")
                        // {
                        idEliminar = Convert.ToString(row.Cells["idCliente"].Value);
                        nomEliminar = Convert.ToString(row.Cells["Nombre"].Value);

                        idEliminarFerr = Convert.ToString(row.Cells["idCliente"].Value);
                        nomEliminarFerr = Convert.ToString(row.Cells["Nombre"].Value);

                        tbIdClienteInsertarFerr.Text = Convert.ToString(row.Cells["idCliente"].Value);
                        tbNomInsertarFerr.Text = Convert.ToString(row.Cells["Nombre"].Value);

                        tbIdClienteModificarFerr.Text = Convert.ToString(row.Cells["idCliente"].Value);
                        tbNombreModificarFerr.Text = Convert.ToString(row.Cells["Nombre"].Value);

                        tbidModificar.Text = Convert.ToString(row.Cells["idCliente"].Value);
                        tbNomModificar.Text = Convert.ToString(row.Cells["Nombre"].Value);
                        tbNitModificar.Text = Convert.ToString(row.Cells["NIT"].Value);
                        tbTelModificar.Text = Convert.ToString(row.Cells["Telefono"].Value);
                        // tbDireModificar.Text = Convert.ToString(row.Cells["Direccion"].Value);
                        // tbFerrModificar.Text = Convert.ToString(row.Cells["Ferreteria"].Value);
                        // }
                        break;
                    }
                    dataGridView1.Refresh();
                    transaccion.Commit();
                   
                }
                catch (Exception error)
                {
                    transaccion.Rollback();  // deshacer transaccion
                    MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
                }
                //conectar();
            }
            else if (radioButton2.Checked)
            {
                MySqlTransaction transaccion = null;
                try
                {
                    
                    transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);
                    MySqlCommand comando = new MySqlCommand();
                    comando.Connection = myCon;
                    comando.CommandText = ("SELECT idFerreteria, ferreteria.Nombre as Nombre_Ferreteria, Direccion, idCliente, cliente.Nombre, Telefono, NIT, Eliminada FROM megatubos.ferreteria "
                                      + "INNER JOIN megatubos.cliente "
                                      + "ON megatubos.cliente.idCliente = megatubos.ferreteria.Cliente_idCliente "
                                      + "WHERE (cliente.Nombre = '" + tbBuscar.Text + "' AND eliminada = 0)"
                                      + "GROUP BY idFerreteria; ");

                    comando.Transaction = transaccion;
                    MySqlDataAdapter adaptador = new MySqlDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dataGridView1.DataSource = tabla;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        // if (Convert.ToString(row.Cells["idCliente"].Value) == tbBuscar.Text && tbBuscar.Text != "")
                        // {
                        idEliminar = Convert.ToString(row.Cells["idCliente"].Value);
                        nomEliminar = Convert.ToString(row.Cells["Nombre"].Value);

                        idEliminarFerr = Convert.ToString(row.Cells["idCliente"].Value);
                        nomEliminarFerr = Convert.ToString(row.Cells["Nombre"].Value);

                        tbIdClienteInsertarFerr.Text = Convert.ToString(row.Cells["idCliente"].Value);
                        tbNomInsertarFerr.Text = Convert.ToString(row.Cells["Nombre"].Value);

                        tbIdClienteModificarFerr.Text = Convert.ToString(row.Cells["idCliente"].Value);
                        tbNombreModificarFerr.Text = Convert.ToString(row.Cells["Nombre"].Value);

                        tbidModificar.Text = Convert.ToString(row.Cells["idCliente"].Value);
                        tbNomModificar.Text = Convert.ToString(row.Cells["Nombre"].Value);
                        tbNitModificar.Text = Convert.ToString(row.Cells["NIT"].Value);
                        tbTelModificar.Text = Convert.ToString(row.Cells["Telefono"].Value);
                        //  tbDireModificar.Text = Convert.ToString(row.Cells["Direccion"].Value);
                        //  tbFerrModificar.Text = Convert.ToString(row.Cells["Ferreteria"].Value);

                        // }
                        break;

                    }
                    dataGridView1.Refresh();
                    transaccion.Commit();
                
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
                    
                    transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);
                    MySqlCommand comando = new MySqlCommand();
                    comando.Connection = myCon;
                    comando.CommandText = ("SELECT idFerreteria, ferreteria.Nombre as Nombre_Ferreteria, Direccion, idCliente, cliente.Nombre, Telefono, NIT, Eliminada FROM megatubos.ferreteria "
                                      + "INNER JOIN megatubos.cliente "
                                      + "ON megatubos.cliente.idCliente = megatubos.ferreteria.Cliente_idCliente "
                                      + "WHERE (ferreteria.idFerreteria = '" + tbBuscar.Text + "' AND cliente.eliminada = 0)"
                                      + "GROUP BY idFerreteria; ");

                    comando.Transaction = transaccion;
                    MySqlDataAdapter adaptador = new MySqlDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dataGridView1.DataSource = tabla;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        // if (Convert.ToString(row.Cells["idCliente"].Value) == tbBuscar.Text && tbBuscar.Text != "")
                        // {
                        idEliminar = Convert.ToString(row.Cells["idCliente"].Value);
                        nomEliminar = Convert.ToString(row.Cells["Nombre"].Value);

                        idEliminarFerr = Convert.ToString(row.Cells["idCliente"].Value);
                        nomEliminarFerr = Convert.ToString(row.Cells["Nombre"].Value);
                        idFerrEliminarFerr = Convert.ToString(row.Cells["idFerreteria"].Value);
                        nomFerrEliminarFerr = Convert.ToString(row.Cells["Nombre_Ferreteria"].Value);

                        tbIdClienteInsertarFerr.Text = Convert.ToString(row.Cells["idCliente"].Value);
                        tbNomInsertarFerr.Text = Convert.ToString(row.Cells["Nombre"].Value);

                        tbIdClienteModificarFerr.Text = Convert.ToString(row.Cells["idCliente"].Value);
                        tbNombreModificarFerr.Text = Convert.ToString(row.Cells["Nombre"].Value);
                        tbidFerrModificarFerr.Text = Convert.ToString(row.Cells["idFerreteria"].Value);
                        tbDirecModificarFerr.Text = Convert.ToString(row.Cells["Direccion"].Value);
                        tbFerrModificarFerr.Text = Convert.ToString(row.Cells["Nombre_Ferreteria"].Value);

                        tbidModificar.Text = Convert.ToString(row.Cells["idCliente"].Value);
                        tbNomModificar.Text = Convert.ToString(row.Cells["Nombre"].Value);
                        tbNitModificar.Text = Convert.ToString(row.Cells["NIT"].Value);
                        tbTelModificar.Text = Convert.ToString(row.Cells["Telefono"].Value);
                        // tbDireModificar.Text = Convert.ToString(row.Cells["Direccion"].Value);
                        //tbFerrModificar.Text = Convert.ToString(row.Cells["Ferreteria"].Value);

                        // }
                        break;

                    }
                    dataGridView1.Refresh();
                    transaccion.Commit();
                  //  myCon.Close();
                    btnModificarFerr.Enabled = true;
                    btnEliminarFerr.Enabled = true;
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
                btnInsertarFerr.Enabled = true;
            }
            else
            {
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
                btnInsertarFerr.Enabled = false;
                btnModificarFerr.Enabled = false;
                btnEliminarFerr.Enabled = false;
            }
        }

        private void btnCancelar1_Click(object sender, EventArgs e)
        {
            gbInsertar.Hide();
            groupBox1.Visible = true;
            groupBox2.Visible = true;
            dataGridView1.Visible = true;
        }

        private void btnGuardar1_Click(object sender, EventArgs e)
        {
            MySqlTransaction transaccion = null;
            try
            {
               // MySqlConnection myCon = Connection.conex();
                //myCon.Open();
                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = myCon;
                comando.CommandText = "Insert into cliente(Nombre, Telefono, NIT, Eliminada) values('" +
                    tbNombreInsertar.Text + "','" + tbTelInsertar.Text + "','" + tbNitInsertar.Text + "', '0');";
                comando.Transaction = transaccion;
                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Refresh();
                // insertamos en la tabla ferreteria
                transaccion.Commit();
                //myCon.Close();
                conectarCliente();
                insertarFerreteria("0", tbNombreInsertar.Text, tbNitInsertar.Text, tbTelInsertar.Text);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            conectar();
            tbBuscar.Clear();
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            btnInsertarFerr.Enabled = false;
            btnModificarFerr.Enabled = false;
            btnEliminarFerr.Enabled = false;
        }

        private void tbBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardarModificar_Click(object sender, EventArgs e)
        {
            //Modificiar
            MySqlTransaction transaccion = null;
            try
            {
                //MySqlConnection myCon = Connection.conex();
                //myCon.Open();
                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = myCon;
                comando.CommandText = "UPDATE cliente SET Nombre = '" +
                  tbNomModificar.Text + "',Telefono = '" + tbTelModificar.Text + "',NIT = '" + tbNitModificar.Text + "'" +
                  " WHERE (idCliente = '" + tbidModificar.Text + "'  AND eliminada = 0);";

                comando.Transaction = transaccion;// comando tipo transaccion
                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Refresh();
                transaccion.Commit();   // confirmar transaccion
                //myCon.Close();
                conectar();

                gbModificar.Hide();
                tbidModificar.Clear();
                tbNomModificar.Clear();
                tbNitModificar.Clear();
                tbTelModificar.Clear();

                btnModificar.Enabled = true;
                btnInsertar.Enabled = true;
                btnEliminar.Enabled = true;
                btnBuscar.Enabled = true;
                btnUpdate.Enabled = true;
                btnBack.Enabled = true;

                gbInsertar.Hide();
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
            tbNitModificar.Clear();
            tbTelModificar.Clear();

            btnModificar.Enabled = true;
            btnInsertar.Enabled = true;
            btnEliminar.Enabled = true;
            btnBuscar.Enabled = true;
            btnUpdate.Enabled = true;
            btnBack.Enabled = true;

            gbInsertar.Hide();
            groupBox1.Visible = true;
            groupBox2.Visible = true;
            dataGridView1.Visible = true;
        }

        private void btnInsertarFerr_Click(object sender, EventArgs e)
        {
            gbInsertarFerr.Show();
            gbInsertarFerr.BringToFront();
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            dataGridView1.Visible = false;
        }

        private void btnCancelarInsertarFerr_Click(object sender, EventArgs e)
        {
            gbInsertarFerr.Hide();
           
            tbIdClienteInsertarFerr.Clear();
            tbNomInsertarFerr.Clear();
            tbDirecInsertarFerr.Clear();
            tbFerrInsertarFerr.Clear();

            btnModificar.Enabled = true;
            btnInsertar.Enabled = true;
            btnEliminar.Enabled = true;
            btnBuscar.Enabled = true;
            btnUpdate.Enabled = true;
            btnBack.Enabled = true;

            gbInsertar.Hide();
            groupBox1.Visible = true;
            groupBox2.Visible = true;
            dataGridView1.Visible = true;
        }

        private void btnGuardarInsertarFerr_Click(object sender, EventArgs e)
        {
            tbIdClienteInsertarFerr.Text = idEliminar;
            tbNomInsertarFerr.Text = nomEliminar;

            MySqlTransaction transaccion = null;
            try
            {
                //MySqlConnection myCon = Connection.conex();
                //myCon.Open();
                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);     // tipo aislamiento
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = myCon;
                comando.CommandText = "Insert into ferreteria(Nombre, Direccion, Cliente_idCliente) values('" +
                              tbFerrInsertarFerr.Text + "','" + tbDirecInsertarFerr.Text + "','" + tbIdClienteInsertarFerr.Text + "');";

                comando.Transaction = transaccion;      // comando tipo transaccion
                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Refresh();

                transaccion.Commit();  // confirmar transaccion
                //myCon.Close();
                conectar();

                gbInsertarFerr.Hide();
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

        private void btnModificarFerr_Click(object sender, EventArgs e)
        {
            gbModificarFerr.Show();
            gbModificarFerr.BringToFront();
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            dataGridView1.Visible = false;

            btnModificarFerr.Enabled = false;
            btnEliminarFerr.Enabled = false;
        }

        private void btnGuardarModificarFerr_Click(object sender, EventArgs e)
        {
            tbIdClienteInsertarFerr.Text = idEliminar;
            tbNomInsertarFerr.Text = nomEliminar;

            MySqlTransaction transaccion = null;
            try
            {
                //MySqlConnection myCon = Connection.conex();
                //myCon.Open();

                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo de aislamiento
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = myCon;
                comando.CommandText = "UPDATE ferreteria SET Nombre = '" +
                  tbFerrModificarFerr.Text + "', Direccion = '" + tbDirecModificarFerr.Text + "'" +
                  " WHERE (idFerreteria = '" + tbidFerrModificarFerr.Text + "' AND Cliente_idCliente = '" + tbIdClienteModificarFerr.Text + "');";

                comando.Transaction = transaccion;          // comando tipo transaccion
                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;
                dataGridView1.Refresh();

                transaccion.Commit();       // confirmar transaccion
               // myCon.Close();
                conectar();

                gbModificarFerr.Hide();
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

        private void btnCancelarModificarFerr_Click(object sender, EventArgs e)
        {
            gbModificarFerr.Hide();

            tbIdClienteModificarFerr.Clear();
            tbNombreModificarFerr.Clear();
            tbidFerrModificarFerr.Clear();
            tbDirecModificarFerr.Clear();
            tbFerrModificarFerr.Clear();

            btnModificar.Enabled = true;
            btnInsertar.Enabled = true;
            btnEliminar.Enabled = true;
            btnBuscar.Enabled = true;
            btnUpdate.Enabled = true;
            btnBack.Enabled = true;

            gbInsertar.Hide();
            groupBox1.Visible = true;
            groupBox2.Visible = true;
            dataGridView1.Visible = true;
        }

        private void btnEliminarFerr_Click(object sender, EventArgs e)
        {
            //message box

            string message = "Desea eliminar a: " + nomEliminarFerr + " con el Id: " + idEliminarFerr
                                + " \n Se borrara permanentemente";
            string caption = "Eliminacion de Ferreteria";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                // Closes the parent form.
                eliminarFerreteria();
            }

            btnModificarFerr.Enabled = false;
            btnEliminarFerr.Enabled = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
