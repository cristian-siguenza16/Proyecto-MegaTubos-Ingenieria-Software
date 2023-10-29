using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Proyecto_MegaTubos
{
    public partial class Login : Form
    {
        public MySqlConnection myCon = Connection.conex();
      
        public Login()
        {
            Abrir();
            InitializeComponent();
            conectar();
            this.CenterToScreen();
        }
        private void Abrir()
        {
            myCon.Close();
            myCon.Open();
        }
        private void conectar()
        {

          
            MySqlCommand comando = new MySqlCommand();
            MySqlTransaction transaccion = null;            // inicializamos variable transaccion
            try
            {
               // MySqlConnection myCon = Connection.conex();
                //myCon.Open();
                //string aislamiento = "IsolationLevel.ReadCommitted";
                // al iniciar la transaccion, se escribe el tipo de aislamiento
                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable); 

                comando.Connection = myCon;
                comando.CommandText = ("select * from usuario;");

                comando.Transaction = transaccion;              // mandamos el comando de tipo transaccion

                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView1.DataSource = tabla;

                // tabla 2 a leer
                comando.CommandText = ("select * from tipo_empleado;");

                comando.Transaction = transaccion;          // mandamos el comando 2 de tipo transaccion

                adaptador.SelectCommand = comando;
                DataTable tabla2 = new DataTable();
                adaptador.Fill(tabla2);
                dataGridView2.DataSource = tabla2;


                transaccion.Commit();                       // confirmamos la transaccion

            }
            catch (Exception error)
            {
                transaccion.Rollback();  // deshacer transaccion
                MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
            }
        }

        private void btn_Ingresar_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                
                if (Convert.ToString(row.Cells["idUsuario"].Value) == tbUsuario.Text && tbUsuario.Text != "")
                {
                    if (Convert.ToString(row.Cells["Password"].Value) == tbPassword.Text && tbPassword.Text != "")
                    {
                        string posicion = " ";
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (Convert.ToString(row2.Cells["idTipo_Empleado"].Value) == Convert.ToString(row.Cells["Tipo_Empleado_idTipo_Empleado"].Value))
                            {
                                posicion = Convert.ToString(row2.Cells["Tipo"].Value);
                            }
                        }

                        btnIngresar.Enabled = false;
                        btnIngresar.Visible = false;
                        Principal frm = new Principal();
                        frm.FormClosed += Logout;
                        frm.ingresa(posicion, Convert.ToString(row.Cells["Nombre"].Value), Convert.ToString(row.Cells["idUsuario"].Value), myCon);
                        frm.Show();
                        this.Hide();
                    }
                }
            }
            tbUsuario.Text = "USUARIO";
            tbPassword.UseSystemPasswordChar = false;
            tbPassword.Text = "CONTRASEÑA";
        }

        private void Logout(Object sender, FormClosedEventArgs e)
        {
            this.Show();
            tbUsuario.Focus();
            btnIngresar.Enabled = true;
            btnIngresar.Visible = true;
        }

        private void tbUsuario_Enter(object sender, EventArgs e)
        {
            if(tbUsuario.Text == "USUARIO")
            {
                tbUsuario.Text = "";
                tbUsuario.ForeColor = Color.LightGray;
            }
        }

        private void tbUsuario_Leave(object sender, EventArgs e)
        {
            if (tbUsuario.Text == "")
            {
                tbUsuario.Text = "USUARIO";
                tbUsuario.ForeColor = Color.DimGray;
            }
        }

        private void tbPassword_Enter(object sender, EventArgs e)
        {
            if (tbPassword.Text == "CONTRASEÑA")
            {
                tbPassword.Text = "";
                tbPassword.ForeColor = Color.LightGray;
                tbPassword.UseSystemPasswordChar = true;
            }
        }

        private void tbPassword_Leave(object sender, EventArgs e)
        {
            if (tbPassword.Text == "")
            {
                tbPassword.Text = "CONTRASEÑA";
                tbPassword.ForeColor = Color.DimGray;
                tbPassword.UseSystemPasswordChar = false;
            }
        }

        private void tbUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                tbPassword.Focus();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (Convert.ToString(row.Cells["idUsuario"].Value) == tbUsuario.Text && tbUsuario.Text != "")
                {
                    if (Convert.ToString(row.Cells["Password"].Value) == tbPassword.Text && tbPassword.Text != "")
                    {
                        string posicion = " ";
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (Convert.ToString(row2.Cells["idTipo_Empleado"].Value) == Convert.ToString(row.Cells["Tipo_Empleado_idTipo_Empleado"].Value))
                            {
                                if(Convert.ToString(row2.Cells["Tipo"].Value) == "Gerente" || Convert.ToString(row2.Cells["Tipo"].Value) == "Admin")
                                {
                                    Usuarios form = new Usuarios();
                                    form.FormClosed += Logout;
                                    form.Show();
                                    this.Hide();
                                }
                            }
                        }
                    }
                }
            }
            tbUsuario.Text = "USUARIO";
            tbPassword.UseSystemPasswordChar = false;
            tbPassword.Text = "CONTRASEÑA";
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.ForeColor = Color.SkyBlue;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.White;
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
