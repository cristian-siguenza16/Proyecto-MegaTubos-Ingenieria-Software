using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Proyecto_MegaTubos
{
    public partial class Ventas : Form
    {
        public MySqlConnection myCon = null;
        // dataGriedView2 = adaptador variable de tablas solo para hacer llenado y comparaciones
        private DataTable tablaVentas;
        private decimal total = 0;
        private string totaltexto = "0";
        private bool cotizar = false;
        public Ventas()
        {
            InitializeComponent();
            crearTabla();
            gbEliminar.Visible = false;
            gbInsertar.Visible = false;
            gbModificar.Visible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        public Ventas(MySqlConnection conex)
        {
            InitializeComponent();
            this.myCon = conex;
            crearTabla();
            actualizar_factura();
            gbEliminar.Visible = false;
            gbInsertar.Visible = false;
            gbModificar.Visible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void crearTabla()
        {
            tablaVentas = new DataTable();
            tablaVentas.Columns.Add("Descripcion de Producto");
            tablaVentas.Columns.Add("Precio Unitario");
            tablaVentas.Columns.Add("Cantidad");
            tablaVentas.Columns.Add("Sub-Total");
            tablaVentas.Columns.Add("idProducto");
            dataGridView1.DataSource = tablaVentas;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.ReadOnly = true;
        }

        private void calcularTotal()
        {
            actualizar_factura();
            total = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                    total =  total + Convert.ToDecimal(row.Cells["Sub-Total"].Value);
            }
            lbTotal.Text = "Total Q " + Convert.ToString(total);
            totaltexto = Convert.ToString(total);
        }
        private void conectarProducto()
        {
            MySqlTransaction transaccion = null;
            try
            {
             //   MySqlConnection myCon = Connection.conex();
             //   myCon.Open();

                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo aislamiento
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = myCon;
                comando.CommandText = ("SELECT * FROM producto WHERE eliminado = 0;");

                comando.Transaction = transaccion;          // comando transaccion
                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView2.DataSource = tabla;
                dataGridView2.Columns[4].Visible = false;

                transaccion.Commit();           // confirmar transaccion
             //   myCon.Close();
            }
            catch (Exception error)
            {
                transaccion.Rollback();  // deshacer transaccion
                MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
            }
        }

        private void conectarClienteyFerreteria()
        {
            MySqlTransaction transaccion = null;

            try
            {
              //  MySqlConnection myCon = Connection.conex();
                //myCon.Open();
                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo aislamiento
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = myCon;
                comando.CommandText = ("SELECT idFerreteria, ferreteria.Nombre as Nombre_Ferreteria, Direccion, idCliente, Cliente_idCliente, cliente.Nombre, Telefono, NIT, Eliminada FROM megatubos.ferreteria "
                                      + "INNER JOIN megatubos.cliente "
                                      + "ON megatubos.cliente.idCliente = megatubos.ferreteria.Cliente_idCliente "
                                      + "WHERE cliente.eliminada = 0 "
                                      + "GROUP BY idFerreteria; ");

                comando.Transaction = transaccion;          // comando transaccion
                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView2.DataSource = tabla;

                transaccion.Commit();           // confirmar transaccion
            //    myCon.Close();
            }
            catch (Exception error)
            {
                transaccion.Rollback();  // deshacer transaccion
                MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
            }
        }

        private void conectarSalida()
        {
            MySqlTransaction transaccion = null;
            try
            {
              //  MySqlConnection myCon = Connection.conex();
                //myCon.Open();
                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo aislamiento
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = myCon;
                comando.CommandText = ("SELECT * FROM megatubos.salidas WHERE Eliminada = 0;");

                comando.Transaction = transaccion;          // comando transaccion
                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataGridView2.DataSource = tabla;
                dataGridView2.Refresh();

                transaccion.Commit();           // confirmar transaccion
            //    myCon.Close();
            }
            catch (Exception error)
            {
                transaccion.Rollback();  // deshacer transaccion
                MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
            }
        }
        private void insertarDetalle_de_Salida(String fecha)
        {
            string id = "";
            string cant, subtotal, idproducto;
            foreach (DataGridViewRow row2 in dataGridView2.Rows)
            {

                if (Convert.ToString(row2.Cells["Descripcion"].Value) == rtbDescripcion.Text && rtbDescripcion.Text != "")
                {
                        if (Convert.ToString(row2.Cells["Total"].Value) == totaltexto && totaltexto != "")
                        {
                            if (Convert.ToString(row2.Cells["Ferreteria"].Value) == cbFerreteria.Text && cbFerreteria.Text != "")
                            {
                                if (Convert.ToString(row2.Cells["Vendedor_idVendedor"].Value) == tbIdVendedor.Text && tbIdVendedor.Text != "")
                                {
                                    if (Convert.ToString(row2.Cells["Cliente_idCliente"].Value) == tbIdCliente.Text && tbIdCliente.Text != "")
                                    {
                                
                                        Console.WriteLine(Convert.ToString(row2.Cells["ID"].Value));
                                    id = Convert.ToString(row2.Cells["ID"].Value);
                                    foreach (DataGridViewRow row in dataGridView1.Rows)
                                        {
                                        cant = Convert.ToString(row.Cells["Cantidad"].Value);
                                        subtotal = Convert.ToString(row.Cells["Sub-Total"].Value);
                                        idproducto = Convert.ToString(row.Cells["idProducto"].Value);
                                        if (!string.IsNullOrWhiteSpace(cant) && !string.IsNullOrWhiteSpace(subtotal))
                                        {
                                            MySqlTransaction transaccion = null;
                                            try
                                            {
                                           //     MySqlConnection myCon = Connection.conex();
                                             //   myCon.Open();

                                                transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo aislamiento
                                                MySqlCommand comando = new MySqlCommand();
                                                comando.Connection = myCon;
                                                comando.CommandText = "Insert into megatubos.detalle_de_salida(CantidadVendida, Sub_Total, Salidas_ID, Producto_Codigo) values('" +
                                                cant + "','" + subtotal + "','" + id + "','" + idproducto + "');";

                                                comando.Transaction = transaccion;          // comando transaccion
                                                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                                                adaptador.SelectCommand = comando;
                                                DataTable tabla = new DataTable();
                                                adaptador.Fill(tabla);
                                                // dataGridView2.DataSource = tabla;

                                                transaccion.Commit();           // confirmar transaccion
                                               // myCon.Close();
                                            }
                                            catch (Exception error)
                                            {
                                                transaccion.Rollback();  // deshacer transaccion
                                                MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
                                            }
                                        }

                                        }
                                        MessageBox.Show("Se inserto todo correctamente");
                                        break;
                                    }
                                }
                            }
                        }
                    
                }
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            actualizar_factura();
            gbInsertar.Show();
            gbInsertar.BringToFront();
            btnInsertar.Enabled = false;
            btnBack.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            actualizar_factura();
            gbModificar.Show();
            gbModificar.BringToFront();
            btnInsertar.Enabled = false;
            btnBack.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            actualizar_factura();
            gbEliminar.Show();
            gbEliminar.BringToFront();
            btnInsertar.Enabled = false;
            btnBack.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
        }

        private void btnCancelarInsertar_Click(object sender, EventArgs e)
        {
            gbInsertar.Visible = false;
            btnInsertar.Enabled = true;
            btnBack.Enabled = true;
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
        }

        private void btnCancelarModificar_Click(object sender, EventArgs e)
        {
            gbModificar.Visible = false;
            btnInsertar.Enabled = true;
            btnBack.Enabled = true;
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
        }

        private void btCancelarEliminar_Click(object sender, EventArgs e)
        {
            gbEliminar.Visible = false;
            btnInsertar.Enabled = true;
            btnBack.Enabled = true;
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
        }

        private void btnGuardarInsertar_Click(object sender, EventArgs e)
        {

            DataRow row = tablaVentas.NewRow();
            //row["Local"] = cBLocales1.Text;
            row["Descripcion de Producto"] = tbDescripcionInsertar.Text;
            row["Cantidad"] = nupCantidadInsertar.Text;
            row["Precio Unitario"] = tbPrecioInsertar.Text;
            row["Sub-Total"] = tbSubtotalInsertar.Text;
            row["idProducto"] = tbProductoInsertar.Text;
            tablaVentas.Rows.Add(row);

            calcularTotal();

            tbProductoInsertar.Clear();
            tbDescripcionEliminar.Clear();
            tbExistenciaInsertar.Clear();
            nupCantidadInsertar.Value = 1;
            tbPrecioInsertar.Clear();
            tbSubtotalInsertar.Clear();
        }

        private void tbProductoInsertar_TextChanged(object sender, EventArgs e)
        {
            conectarProducto();
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (Convert.ToString(row.Cells["Codigo"].Value) == tbProductoInsertar.Text)
                {
                    tbDescripcionInsertar.Text = Convert.ToString(row.Cells["Descripcion"].Value);
                    tbExistenciaInsertar.Text = Convert.ToString(row.Cells["Stock"].Value);
                    tbPrecioInsertar.Text = Convert.ToString(row.Cells["PrecioCosto"].Value);
                }
            }
        }

        private void nupCantidadInsertar_ValueChanged(object sender, EventArgs e)
        {
            Decimal cant, sub, precio;
            if(!string.IsNullOrWhiteSpace(tbPrecioInsertar.Text) && !string.IsNullOrWhiteSpace(nupCantidadInsertar.Text))
            {
                cant = Convert.ToDecimal(nupCantidadInsertar.Text);
                precio = Convert.ToDecimal(tbPrecioInsertar.Text);
                sub = cant * precio;
                tbSubtotalInsertar.Text = Convert.ToString(sub); 
            }
        }

        private void nupCantidadInsertar_Enter(object sender, EventArgs e)
        {
            Decimal cant, sub, precio;
            if (!string.IsNullOrWhiteSpace(tbPrecioInsertar.Text) && !string.IsNullOrWhiteSpace(nupCantidadInsertar.Text))
            {
                cant = Convert.ToDecimal(nupCantidadInsertar.Text);
                precio = Convert.ToDecimal(tbPrecioInsertar.Text);
                sub = cant * precio;
                tbSubtotalInsertar.Text = Convert.ToString(sub);
            }
        }

        // todo lo de modificar de la tabla productos de venta
        private void btnGuardarModificar_Click(object sender, EventArgs e)
        {

            DataRow row = tablaVentas.NewRow();

            foreach (DataGridViewRow row2 in dataGridView1.Rows)
            {
                if (Convert.ToString(row2.Cells["idProducto"].Value) == tbIdModificar.Text && !string.IsNullOrWhiteSpace(tbPrecioModificar.Text))
                {
                    int i = row2.Index;
                    row.Table.Rows.RemoveAt(i);
                    row["Descripcion de Producto"] = tbDescripcionModifcar.Text;
                    row["Cantidad"] = nupCantidadModificar.Text;
                    row["Precio Unitario"] = tbPrecioModificar.Text;
                    row["Sub-Total"] = tbSubtotalModificar.Text;
                    row["idProducto"] = tbIdModificar.Text;
                    tablaVentas.Rows.Add(row);
                }
            }
            if(!string.IsNullOrWhiteSpace(tbPrecioModificar.Text))
            {
                calcularTotal();
            }

            tbIdModificar.Clear();
            tbDescripcionModifcar.Clear();
            tbExistenciaModificar.Clear();
            nupCantidadModificar.Value = 1;
            tbPrecioModificar.Clear();
            tbSubtotalModificar.Clear();

            gbModificar.Visible = false;
            btnInsertar.Enabled = true;
            btnBack.Enabled = true;
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
        }
        // actualizar el sub-total por producto adquiridio
        private void nupCantidadModificar_Enter(object sender, EventArgs e)
        {
            decimal cant, sub, precio;
            if (!string.IsNullOrWhiteSpace(tbPrecioModificar.Text) && !string.IsNullOrWhiteSpace(nupCantidadModificar.Text))
            {
                cant = Convert.ToDecimal(nupCantidadModificar.Text);
                precio = Convert.ToDecimal(tbPrecioModificar.Text);
                sub = cant * precio;

                tbSubtotalModificar.Text = Convert.ToString(sub);
            }
        }

        // cuando cambia de valor el texto llenar los otros campos
        private void tbIdModificar_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToString(row.Cells["idProducto"].Value) == tbIdModificar.Text)
                {
                    tbDescripcionModifcar.Text = Convert.ToString(row.Cells["Descripcion de Producto"].Value);
                    conectarProducto();
                    // obtener la existencia
                    foreach (DataGridViewRow row2 in dataGridView2.Rows)
                    {
                        if (Convert.ToString(row2.Cells["Codigo"].Value) == tbIdModificar.Text)
                        {
                            tbExistenciaModificar.Text = Convert.ToString(row2.Cells["Stock"].Value);
                        }
                    }
                    tbPrecioModificar.Text = Convert.ToString(row.Cells["Precio Unitario"].Value);
                    nupCantidadModificar.Text = Convert.ToString(row.Cells["Cantidad"].Value);
                    tbSubtotalModificar.Text = Convert.ToString(row.Cells["Sub-Total"].Value);
                }
            }
        }

        private void nupCantidadModificar_ValueChanged(object sender, EventArgs e)
        {
            decimal cant, sub, precio;
            if (!string.IsNullOrWhiteSpace(tbPrecioModificar.Text) && !string.IsNullOrWhiteSpace(nupCantidadModificar.Text))
            {
                cant = Convert.ToDecimal(nupCantidadModificar.Text);
                precio = Convert.ToDecimal(tbPrecioModificar.Text);
                sub = cant * precio;

                tbSubtotalModificar.Text = Convert.ToString(sub);
            }
        }

        // todo lo de eliminar de la tabla de productos de ventas
        private void btnGuardarEliminar_Click(object sender, EventArgs e)
        {
            DataRow row = tablaVentas.NewRow();

            foreach (DataGridViewRow row2 in dataGridView1.Rows)
            {
                if (Convert.ToString(row2.Cells["idProducto"].Value) == tbProductoEliminar.Text && !string.IsNullOrEmpty(tbPrecioEliminar.Text))
                {
                    int i = row2.Index;
                    row.Table.Rows.RemoveAt(i);
                }
            }
            if (!string.IsNullOrWhiteSpace(tbPrecioEliminar.Text))
            {
                calcularTotal();
            }

            tbProductoEliminar.Clear();
            tbDescripcionEliminar.Clear();
            tbExistenciaEliminar.Clear();
            nupCantidadEliminar.Value = 1;
            tbPrecioEliminar.Clear();
            tbSubtotalEliminar.Clear();

            gbEliminar.Visible = false;
            btnInsertar.Enabled = true;
            btnBack.Enabled = true;
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
        }

        private void nupCantidadEliminar_Enter(object sender, EventArgs e)
        {
            decimal cant, sub, precio;
            if (!string.IsNullOrWhiteSpace(tbPrecioEliminar.Text) && !string.IsNullOrWhiteSpace(nupCantidadEliminar.Text))
            {
                cant = Convert.ToDecimal(nupCantidadEliminar.Text);
                precio = Convert.ToDecimal(tbPrecioEliminar.Text);
                sub = cant * precio;

                tbSubtotalEliminar.Text = Convert.ToString(sub);
            }
        }

        private void tbProductoEliminar_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToString(row.Cells["idProducto"].Value) == tbProductoEliminar.Text)
                {
                    tbDescripcionEliminar.Text = Convert.ToString(row.Cells["Descripcion de Producto"].Value);
                    conectarProducto();
                    // obtener la existencia
                    foreach (DataGridViewRow row2 in dataGridView2.Rows)
                    {
                        if (Convert.ToString(row2.Cells["Codigo"].Value) == tbProductoEliminar.Text)
                        {
                            tbExistenciaEliminar.Text = Convert.ToString(row2.Cells["Stock"].Value);
                        }
                    }
                    tbPrecioEliminar.Text = Convert.ToString(row.Cells["Precio Unitario"].Value);
                    nupCantidadEliminar.Text = Convert.ToString(row.Cells["Cantidad"].Value);
                    tbSubtotalEliminar.Text = Convert.ToString(row.Cells["Sub-Total"].Value);
                }
            }
        }

        private void tbNombreCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                cbFerreteria.Items.Clear();
                string idCliente = "";
                conectarClienteyFerreteria();
                foreach (DataGridViewRow row2 in dataGridView2.Rows)
                {
                    if (Convert.ToString(row2.Cells["Nombre"].Value) == tbNombreCliente.Text && !string.IsNullOrWhiteSpace(tbNombreCliente.Text))
                    {
                        idCliente = Convert.ToString(row2.Cells["idCliente"].Value);
                        break;
                    }
                }
                conectarClienteyFerreteria();
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (Convert.ToString(row.Cells["Cliente_idCliente"].Value) == idCliente && !string.IsNullOrWhiteSpace(idCliente))
                    {
                        cbFerreteria.Items.Add(Convert.ToString(row.Cells["Nombre_Ferreteria"].Value));
                        cbFerreteria.SelectedIndex = 0;
                    }
                }

                cbFerreteria.Focus();
                tbIdCliente.Text = idCliente;
            }
        }

        private void btnGuardarP_Click(object sender, EventArgs e)
        {
            actualizar_factura();

                string fecha = dtpFecha.Value.ToString("yyyy/MM/dd");
            if (!string.IsNullOrWhiteSpace(rtbDescripcion.Text) && !string.IsNullOrWhiteSpace(cbFerreteria.Text) && !string.IsNullOrWhiteSpace(tbIdVendedor.Text) && tablaVentas.Rows.Count != 0)
            {
                // agregar salida 
                MySqlTransaction transaccion = null;
                try
                {
                 //   MySqlConnection myCon = Connection.conex();
                   // myCon.Open();

                    transaccion = myCon.BeginTransaction(IsolationLevel.Serializable);  // tipo aislamiento
                    MySqlCommand comando = new MySqlCommand();
                    comando.Connection = myCon;
                    comando.CommandText = "Insert into megatubos.salidas(Descripcion, Fecha, Total, Ferreteria, Vendedor_idVendedor, Cliente_idCliente, Eliminada) values('" +
                        rtbDescripcion.Text + "','" + fecha + "','" + totaltexto + "','" + cbFerreteria.Text + "','" +
                        tbIdVendedor.Text + "','" + tbIdCliente.Text + "', '0');";

                    comando.Transaction = transaccion;          // comando transaccion
                    MySqlDataAdapter adaptador = new MySqlDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dataGridView2.DataSource = tabla;
                    dataGridView2.Refresh();
                    // insertamos el void de insertar detalle de factura

                    transaccion.Commit();           // confirmar transaccion
                    //myCon.Close();
                    conectarSalida();
                    insertarDetalle_de_Salida(fecha); 

                    // IMPRIMIR FACTURA O COTIZACION, DEPENDE DEL CASO

                    PrintDocument printDocument1 = new PrintDocument();
                    PrinterSettings ps = new PrinterSettings();
                    printDocument1.PrinterSettings = ps;
                    printDocument1.PrintPage += ImprimirFactura;
                    printDocument1.Print();


                    tablaVentas.Rows.Clear();
                    dataGridView1.DataSource = tablaVentas;
                    lbTotal.Text = "Total Q 0";
                    totaltexto = "0";
                    tbIdVendedor.Clear();
                    tbIdVenta.Clear();
                    tbIdCliente.Clear();
                    tbNombreCliente.Clear();
                    cbFerreteria.Items.Clear();
                    cbFerreteria.Text = "";
                    rtbDescripcion.Clear();
                }
                catch (Exception error)
                {
                    transaccion.Rollback();  // deshacer transaccion
                    MessageBox.Show("No se pudo ejecutar" + error.Message + "\n Vuelva a intentarlo");
                }
            }
            else
            {
                MessageBox.Show("Es obligatorio llenar todos los campos y/o agregar producto");
            }
        }
        private void btnCotizar_Click(object sender, EventArgs e)
        {
            cotizar = true;
            // IMPRIMIR FACTURA O COTIZACION, DEPENDE DEL CASO

            PrintDocument printDocument1 = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            printDocument1.PrinterSettings = ps;
            printDocument1.PrintPage += ImprimirFactura;
            printDocument1.Print();


            tablaVentas.Rows.Clear();
            dataGridView1.DataSource = tablaVentas;
            lbTotal.Text = "Total Q 0";
            totaltexto = "0";
            tbIdVendedor.Clear();
            tbIdVenta.Clear();
            tbIdCliente.Clear();
            tbNombreCliente.Clear();
            cbFerreteria.Items.Clear();
            cbFerreteria.Text = "";
            rtbDescripcion.Clear();
        }

        private void btnCancelarP_Click(object sender, EventArgs e)
        {
            tablaVentas.Rows.Clear();
            dataGridView1.DataSource = tablaVentas;
            lbTotal.Text = "Total Q 0";
            totaltexto = "0";
            tbIdVendedor.Clear();
            tbIdVenta.Clear();
            tbIdCliente.Clear();
            tbNombreCliente.Clear();
            cbFerreteria.Items.Clear();
            cbFerreteria.Text = "";
            rtbDescripcion.Clear();
        }

        private void ImprimirFactura(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Arial", 12);
            string fecha = dtpFecha.Value.ToString("yyyy/MM/dd");
            int ancho = 421;
            int y = 20;
            if (cotizar)
            {
                e.Graphics.DrawString("-------------------------- MEGATUBOS --------------------------", font, Brushes.Black, new Rectangle(0, y += 20, ancho, 20));
                e.Graphics.DrawString("OLINTEPEQUE, QUETZALTENANGO", font, Brushes.Black, new Rectangle(50, y += 40, ancho, 20));
                e.Graphics.DrawString("Fecha: " + fecha, font, Brushes.Black, new Rectangle(2, y += 40, ancho, 20));
                e.Graphics.DrawString("Factura No. COTIZACION", font, Brushes.Black, new Rectangle(2, y += 20, ancho, 20));
                e.Graphics.DrawString("Ferreteria: " + cbFerreteria.Text, font, Brushes.Black, new Rectangle(2, y += 20, ancho, 20));
                e.Graphics.DrawString("Direccion: Ciudad", font, Brushes.Black, new Rectangle(2, y += 20, ancho, 20));
                e.Graphics.DrawString("Nombre: " + tbNombreCliente.Text, font, Brushes.Black, new Rectangle(2, y += 20, ancho, 20));
                e.Graphics.DrawString("NIT: " + tbNIT.Text, font, Brushes.Black, new Rectangle(2, y += 20, ancho, 20));
                e.Graphics.DrawString("-------------------------- PRODUCTOS --------------------------", font, Brushes.Black, new Rectangle(0, y += 20, ancho, 20));
                e.Graphics.DrawString("CANT------", font, Brushes.Black, new Rectangle(0, y += 20, 55, 20));
                e.Graphics.DrawString("DESCRIPCION-----------------------------", font, Brushes.Black, new Rectangle(60, y, 350, 20));
                e.Graphics.DrawString("P/U---------------------", font, Brushes.Black, new Rectangle(351, y, 60, 20));
                foreach (DataRow row in tablaVentas.Rows)
                {
                    e.Graphics.DrawString(row["Cantidad"].ToString(), font, Brushes.Black, new Rectangle(2, y += 20, 55, 20));
                    e.Graphics.DrawString(row["Descripcion de Producto"].ToString(), font, Brushes.Black, new Rectangle(60, y, 350, 20));
                    e.Graphics.DrawString("Q " + row["Precio Unitario"].ToString(), font, Brushes.Black, new Rectangle(351, y, 60, 20));
                }
                e.Graphics.DrawString("TOTAL A PAGAR Q" + totaltexto, font, Brushes.Black, new Rectangle(170, y += 60, ancho, 20));


                e.Graphics.DrawString("Agradecemos su preferencia", font, Brushes.Black, new Rectangle(100, y += 40, ancho, 20));
                cotizar = false;
            }
            else
            {
                e.Graphics.DrawString("----------------------- MEGATUBOS -----------------------", font, Brushes.Black, new Rectangle(0, y += 20, ancho, 20));
                e.Graphics.DrawString("OLINTEPEQUE, QUETZALTENANGO", font, Brushes.Black, new Rectangle(50, y += 40, ancho, 20));
                e.Graphics.DrawString("Fecha: " + fecha, font, Brushes.Black, new Rectangle(2, y += 40, ancho, 20));
                e.Graphics.DrawString("Factura No." + tbIdVenta.Text, font, Brushes.Black, new Rectangle(2, y += 20, ancho, 20));
                e.Graphics.DrawString("Ferreteria: " + cbFerreteria.Text, font, Brushes.Black, new Rectangle(2, y += 20, ancho, 20));
                e.Graphics.DrawString("Direccion: Ciudad", font, Brushes.Black, new Rectangle(2, y += 20, ancho, 20));
                e.Graphics.DrawString("Nombre: " + tbNombreCliente.Text, font, Brushes.Black, new Rectangle(2, y += 20, ancho, 20));
                e.Graphics.DrawString("NIT: " + tbNIT.Text, font, Brushes.Black, new Rectangle(2, y += 20, ancho, 20));
                e.Graphics.DrawString("----------------------- PRODUCTOS -----------------------", font, Brushes.Black, new Rectangle(0, y += 40, ancho, 20));
                e.Graphics.DrawString("CANT", font, Brushes.Black, new Rectangle(1, y += 20, 25, 20));
                e.Graphics.DrawString("DESCRIPCION", font, Brushes.Black, new Rectangle(30, y, 350, 20));
                e.Graphics.DrawString("SUB-TOTAL", font, Brushes.Black, new Rectangle(320, y, 100, 20));
                foreach (DataRow row in tablaVentas.Rows)
                {
                    e.Graphics.DrawString(row["Cantidad"].ToString(), font, Brushes.Black, new Rectangle(2, y += 20, 25, 20));
                    e.Graphics.DrawString(row["Descripcion de Producto"].ToString(), font, Brushes.Black, new Rectangle(30, y, 350, 20));
                    e.Graphics.DrawString("Q " + row["Precio Unitario"].ToString(), font, Brushes.Black, new Rectangle(320, y, 100, 20));
                }
                e.Graphics.DrawString("TOTAL A PAGAR Q" + totaltexto, font, Brushes.Black, new Rectangle(170, y += 60, ancho, 20));


                e.Graphics.DrawString("Agradecemos su preferencia", font, Brushes.Black, new Rectangle(100, y += 40, ancho, 20));
            }
        }

        private void tbNIT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (tbNIT.Text != "CF" || tbNIT.Text != "cf")
                {
                    cbFerreteria.Items.Clear();
                    string idCliente = "";
                    string nombre = "";
                    conectarClienteyFerreteria();
                    foreach (DataGridViewRow row2 in dataGridView2.Rows)
                    {
                        if (Convert.ToString(row2.Cells["NIT"].Value) == tbNIT.Text && !string.IsNullOrWhiteSpace(tbNIT.Text))
                        {   

                            idCliente = Convert.ToString(row2.Cells["idCliente"].Value);
                            nombre = Convert.ToString(row2.Cells["Nombre"].Value);
                            break;
                        }
                    }
                    conectarClienteyFerreteria();
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (Convert.ToString(row.Cells["Cliente_idCliente"].Value) == idCliente && !string.IsNullOrWhiteSpace(idCliente))
                        {
                            cbFerreteria.Items.Add(Convert.ToString(row.Cells["Nombre_Ferreteria"].Value));
                            cbFerreteria.SelectedIndex = 0;
                        }
                    }

                    cbFerreteria.Focus();
                    tbIdCliente.Text = idCliente;
                    tbNombreCliente.Text = nombre;
                }
            }
        }

        private void actualizar_factura()
        {
            MySqlCommand comando = new MySqlCommand();
            comando.Connection = myCon;
            comando.CommandText = ("SELECT ID FROM megatubos.salidas getLastRecord ORDER BY id DESC LIMIT 1;");

            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader.GetString("id")) + 1;
                tbIdVenta.Text = id.ToString();
            }
            myCon.Close();
            myCon.Open();
        }
        //final 
    }
}
