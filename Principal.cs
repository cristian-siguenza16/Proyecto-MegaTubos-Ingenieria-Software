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
    public partial class Principal : Form
    {
        public MySqlConnection myCon = null;
        private string tipo;
        public Principal()
        {
            InitializeComponent();
            this.CenterToScreen();
            seguridad();
           // abrir();
        }
        public void ingresa(string posicion, string nombre, string user, MySqlConnection conexion)
        {
            tipo = posicion;
            labelPosicion.Text = posicion;
            labelNombre.Text = nombre;
            labelUser.Text = user;
            myCon = conexion;
            seguridad();
        }
        /*

        public void abrir(MySqlConnection conexion)
        {
            myCon = conexion;

        }*/
        private void seguridad()
        {
            btnAgregarVenta.Enabled = false;
            btnCliente.Enabled = false;
            btnProducto.Enabled = false;
            btnVendedor.Enabled = false;
            btnHistorialVentas.Enabled = false;
            btnGraficas.Enabled = false;


            if(tipo == "Vendedor")
            {
                btnAgregarVenta.Enabled = true;
                btnCliente.Enabled = true;
                btnVendedor.Enabled = true;
            }
            else if (labelPosicion.Text == "Admin" || labelPosicion.Text == "Gerente")
            {
                btnAgregarVenta.Enabled = true;
                btnCliente.Enabled = true;
                btnProducto.Enabled = true;
                btnVendedor.Enabled = true;
                btnHistorialVentas.Enabled = true;
                btnGraficas.Enabled = true;
            }

        }
        private void abrirFormulario<MiForm>(String nombre) where MiForm : Form, new()
        {
            Form formulario;
            formulario = panel2.Controls.OfType<MiForm>().FirstOrDefault();
            if(formulario == null)
            {
                
                if (nombre == "Cliente")
                {
                    formulario = new Clientes(myCon);  
                }
                else if(nombre == "Vendedor")
                {
                    formulario = new Vendedor(myCon);
                }
                else if(nombre =="Producto")
                {
                    formulario = new Producto(myCon);
                }
                else if(nombre == "Ventas")
                {
                    formulario = new Ventas(myCon);
                }
                else if(nombre == "Historial")
                {
                    formulario = new Historial(myCon);
                }
                else if (nombre == "Graficas")
                {
                    formulario = new Graficas(myCon);
                }
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                panel2.Controls.Add(formulario);
                panel2.Tag = formulario;
               

                formulario.Show();

                formulario.BringToFront();
                formulario.FormClosed += new FormClosedEventHandler(cerrarFormularios);
                
            }
            else
            {
                formulario.BringToFront();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // btn producto
            abrirFormulario<Producto>("Producto");
            btnProducto.BackColor = Color.FromArgb(80, 80, 80);
        }

        private void cerrarFormularios(Object sender, FormClosedEventArgs e)
        {
            if(Application.OpenForms["Producto"] == null)
            {
                btnProducto.BackColor = Color.FromArgb(40, 40, 40);
            }
            if (Application.OpenForms["Clientes"] == null)
            {
                btnCliente.BackColor = Color.FromArgb(40, 40, 40);
            }
            if (Application.OpenForms["Vendedor"] == null)
            {
                btnVendedor.BackColor = Color.FromArgb(40, 40, 40);
            }
            if (Application.OpenForms["Ventas"] == null)
            {
                btnAgregarVenta.BackColor = Color.FromArgb(40, 40, 40);
            }
            if (Application.OpenForms["Historial"] == null)
            {
                btnHistorialVentas.BackColor = Color.FromArgb(40, 40, 40);
            }
            if (Application.OpenForms["Graficas"] == null)
            {
                btnGraficas.BackColor = Color.FromArgb(40, 40, 40);
            }
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            abrirFormulario<Clientes>("Cliente");
            btnCliente.BackColor = Color.FromArgb(80, 80, 80);
        }

        private void btnVendedor_Click(object sender, EventArgs e)
        {
            abrirFormulario<Vendedor>("Vendedor");
            btnVendedor.BackColor = Color.FromArgb(80, 80, 80);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // btn agregar ventas
            abrirFormulario<Ventas>("Ventas");
            btnAgregarVenta.BackColor = Color.FromArgb(80, 80, 80);
        }

        private void btnHistorialVentas_Click(object sender, EventArgs e)
        {
            // historial ventas
            abrirFormulario<Historial>("Historial");
            btnHistorialVentas.BackColor = Color.FromArgb(80, 80, 80);
        }

        private void Principal_Load(object sender, EventArgs e)
        {

        }

        private void btnGraficas_Click(object sender, EventArgs e)
        {
            // historial ventas
            abrirFormulario<Graficas>("Graficas");
            btnGraficas.BackColor = Color.FromArgb(80, 80, 80);
        }
    }
}
