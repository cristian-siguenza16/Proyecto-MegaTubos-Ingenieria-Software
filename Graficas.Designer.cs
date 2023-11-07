namespace Proyecto_MegaTubos
{
    partial class Graficas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title5 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.btnBack = new System.Windows.Forms.Button();
            this.chartProducto = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cbMes = new System.Windows.Forms.ComboBox();
            this.chartClientes = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartProducto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartClientes)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.White;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Image = global::Proyecto_MegaTubos.Properties.Resources.Regresar;
            this.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBack.Location = new System.Drawing.Point(919, 32);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(180, 71);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "Regresar";
            this.btnBack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // chartProducto
            // 
            chartArea5.Name = "ChartArea1";
            this.chartProducto.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chartProducto.Legends.Add(legend5);
            this.chartProducto.Location = new System.Drawing.Point(12, 12);
            this.chartProducto.Name = "chartProducto";
            this.chartProducto.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series5.ChartArea = "ChartArea1";
            series5.Color = System.Drawing.Color.LightSeaGreen;
            series5.Legend = "Legend1";
            series5.Name = "Productos";
            this.chartProducto.Series.Add(series5);
            this.chartProducto.Size = new System.Drawing.Size(883, 315);
            this.chartProducto.TabIndex = 2;
            this.chartProducto.Text = "chart1";
            title5.Name = "MejorProducto";
            title5.Text = "Mejores productos en el mes";
            this.chartProducto.Titles.Add(title5);
            // 
            // cbMes
            // 
            this.cbMes.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbMes.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMes.FormattingEnabled = true;
            this.cbMes.Items.AddRange(new object[] {
            "Enero",
            "Febrero",
            "Marzo",
            "Abril",
            "Mayo",
            "Junio",
            "Julio",
            "Agosto",
            "Septiembre",
            "Octubre",
            "Noviembre",
            "Diciembre"});
            this.cbMes.Location = new System.Drawing.Point(919, 214);
            this.cbMes.Name = "cbMes";
            this.cbMes.Size = new System.Drawing.Size(180, 30);
            this.cbMes.TabIndex = 3;
            this.cbMes.SelectedValueChanged += new System.EventHandler(this.cbMes_SelectedValueChanged);
            // 
            // chartClientes
            // 
            chartArea6.Name = "ChartArea1";
            this.chartClientes.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chartClientes.Legends.Add(legend6);
            this.chartClientes.Location = new System.Drawing.Point(12, 351);
            this.chartClientes.Name = "chartClientes";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Clientes";
            this.chartClientes.Series.Add(series6);
            this.chartClientes.Size = new System.Drawing.Size(883, 315);
            this.chartClientes.TabIndex = 4;
            this.chartClientes.Text = "chart1";
            title6.Name = "MejorProducto";
            title6.Text = "Mejores clientes en el mes";
            this.chartClientes.Titles.Add(title6);
            // 
            // Graficas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 691);
            this.Controls.Add(this.chartClientes);
            this.Controls.Add(this.cbMes);
            this.Controls.Add(this.chartProducto);
            this.Controls.Add(this.btnBack);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Graficas";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Graficas";
            this.Load += new System.EventHandler(this.Graficas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartProducto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartClientes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartProducto;
        private System.Windows.Forms.ComboBox cbMes;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartClientes;
    }
}