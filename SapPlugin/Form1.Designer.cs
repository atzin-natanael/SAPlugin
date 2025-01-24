namespace SapPlugin
{
    partial class Principal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            Texto1 = new Label();
            BtnCargar = new Button();
            AbrirArchivo = new OpenFileDialog();
            TxtFolio = new TextBox();
            Texto2 = new Label();
            CbConceptos = new ComboBox();
            Buscar = new Button();
            Grid = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column10 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column9 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            ChReferencia = new CheckBox();
            TextoSaldo = new Label();
            LbCantidad = new Label();
            MenuSaldo = new ContextMenuStrip(components);
            copiarToolStripMenuItem = new ToolStripMenuItem();
            ChPeriodo = new CheckBox();
            DateInicio = new DateTimePicker();
            DateFin = new DateTimePicker();
            LbTitulo = new Label();
            ((System.ComponentModel.ISupportInitialize)Grid).BeginInit();
            MenuSaldo.SuspendLayout();
            SuspendLayout();
            // 
            // Texto1
            // 
            Texto1.Anchor = AnchorStyles.Top;
            Texto1.AutoSize = true;
            Texto1.Font = new Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Texto1.ForeColor = SystemColors.ControlLight;
            Texto1.Location = new Point(395, 112);
            Texto1.Name = "Texto1";
            Texto1.Size = new Size(212, 25);
            Texto1.TabIndex = 0;
            Texto1.Text = "Cargar archivo main:";
            Texto1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BtnCargar
            // 
            BtnCargar.Anchor = AnchorStyles.Top;
            BtnCargar.BackColor = Color.FromArgb(224, 224, 224);
            BtnCargar.Cursor = Cursors.Hand;
            BtnCargar.FlatAppearance.MouseDownBackColor = Color.Gray;
            BtnCargar.FlatStyle = FlatStyle.Flat;
            BtnCargar.ForeColor = SystemColors.ActiveCaptionText;
            BtnCargar.Image = (Image)resources.GetObject("BtnCargar.Image");
            BtnCargar.ImageAlign = ContentAlignment.MiddleLeft;
            BtnCargar.Location = new Point(407, 156);
            BtnCargar.Name = "BtnCargar";
            BtnCargar.Size = new Size(180, 64);
            BtnCargar.TabIndex = 1;
            BtnCargar.Text = "Cargar Excel";
            BtnCargar.TextAlign = ContentAlignment.MiddleRight;
            BtnCargar.UseVisualStyleBackColor = false;
            BtnCargar.Click += BtnCargar_Click;
            // 
            // TxtFolio
            // 
            TxtFolio.Anchor = AnchorStyles.Top;
            TxtFolio.CharacterCasing = CharacterCasing.Upper;
            TxtFolio.Location = new Point(264, 89);
            TxtFolio.Name = "TxtFolio";
            TxtFolio.Size = new Size(165, 29);
            TxtFolio.TabIndex = 2;
            // 
            // Texto2
            // 
            Texto2.Anchor = AnchorStyles.Top;
            Texto2.AutoSize = true;
            Texto2.Font = new Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Texto2.ForeColor = SystemColors.ControlLight;
            Texto2.Location = new Point(264, 47);
            Texto2.Name = "Texto2";
            Texto2.Size = new Size(123, 25);
            Texto2.TabIndex = 3;
            Texto2.Text = "Buscar por:";
            Texto2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CbConceptos
            // 
            CbConceptos.Anchor = AnchorStyles.Top;
            CbConceptos.FormattingEnabled = true;
            CbConceptos.Location = new Point(408, 45);
            CbConceptos.Name = "CbConceptos";
            CbConceptos.Size = new Size(201, 30);
            CbConceptos.TabIndex = 4;
            CbConceptos.SelectedIndexChanged += CbConceptos_SelectedIndexChanged;
            // 
            // Buscar
            // 
            Buscar.Anchor = AnchorStyles.Top;
            Buscar.BackColor = Color.FromArgb(224, 224, 224);
            Buscar.Cursor = Cursors.Hand;
            Buscar.FlatStyle = FlatStyle.Flat;
            Buscar.ForeColor = SystemColors.ActiveCaptionText;
            Buscar.ImageAlign = ContentAlignment.MiddleLeft;
            Buscar.Location = new Point(675, 84);
            Buscar.Name = "Buscar";
            Buscar.Size = new Size(149, 38);
            Buscar.TabIndex = 5;
            Buscar.Text = "Buscar";
            Buscar.UseVisualStyleBackColor = false;
            Buscar.Click += Buscar_Click;
            // 
            // Grid
            // 
            Grid.AllowUserToAddRows = false;
            Grid.AllowUserToDeleteRows = false;
            Grid.AllowUserToOrderColumns = true;
            Grid.AllowUserToResizeRows = false;
            Grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Grid.BackgroundColor = Color.FromArgb(50, 50, 50);
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Arial", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Grid.Columns.AddRange(new DataGridViewColumn[] { Column1, Column10, Column2, Column3, Column9, Column4, Column5, Column6, Column7, Column8 });
            Grid.Cursor = Cursors.Hand;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Arial", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(192, 192, 255);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.Desktop;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            Grid.DefaultCellStyle = dataGridViewCellStyle2;
            Grid.Location = new Point(12, 224);
            Grid.Name = "Grid";
            Grid.ReadOnly = true;
            Grid.RowHeadersVisible = false;
            Grid.Size = new Size(957, 493);
            Grid.TabIndex = 6;
            // 
            // Column1
            // 
            Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column1.HeaderText = "Cliente";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // Column10
            // 
            Column10.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column10.HeaderText = "Asignación";
            Column10.Name = "Column10";
            Column10.ReadOnly = true;
            // 
            // Column2
            // 
            Column2.HeaderText = "Clase";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 60;
            // 
            // Column3
            // 
            Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column3.HeaderText = "No. Documento";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            // 
            // Column9
            // 
            Column9.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column9.HeaderText = "Factura";
            Column9.Name = "Column9";
            Column9.ReadOnly = true;
            // 
            // Column4
            // 
            Column4.HeaderText = "Fecha Documento";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Width = 110;
            // 
            // Column5
            // 
            Column5.HeaderText = "Fecha Vencimiento";
            Column5.MinimumWidth = 100;
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.Width = 110;
            // 
            // Column6
            // 
            Column6.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column6.HeaderText = "Importe";
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            // 
            // Column7
            // 
            Column7.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column7.HeaderText = "Referencia";
            Column7.Name = "Column7";
            Column7.ReadOnly = true;
            // 
            // Column8
            // 
            Column8.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column8.HeaderText = "Condición de Pago";
            Column8.Name = "Column8";
            Column8.ReadOnly = true;
            // 
            // ChReferencia
            // 
            ChReferencia.Anchor = AnchorStyles.Top;
            ChReferencia.AutoSize = true;
            ChReferencia.ForeColor = SystemColors.Control;
            ChReferencia.Location = new Point(637, 47);
            ChReferencia.Name = "ChReferencia";
            ChReferencia.Size = new Size(122, 26);
            ChReferencia.TabIndex = 7;
            ChReferencia.Text = "Referencia";
            ChReferencia.UseVisualStyleBackColor = true;
            // 
            // TextoSaldo
            // 
            TextoSaldo.AutoSize = true;
            TextoSaldo.Font = new Font("Georgia", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TextoSaldo.ForeColor = SystemColors.ControlLight;
            TextoSaldo.Location = new Point(12, 176);
            TextoSaldo.Name = "TextoSaldo";
            TextoSaldo.Size = new Size(102, 31);
            TextoSaldo.TabIndex = 8;
            TextoSaldo.Text = "Saldo:";
            TextoSaldo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LbCantidad
            // 
            LbCantidad.AutoSize = true;
            LbCantidad.ContextMenuStrip = MenuSaldo;
            LbCantidad.Cursor = Cursors.Hand;
            LbCantidad.Font = new Font("Arial Rounded MT Bold", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LbCantidad.ForeColor = SystemColors.ControlLight;
            LbCantidad.Location = new Point(114, 176);
            LbCantidad.Name = "LbCantidad";
            LbCantidad.Size = new Size(0, 32);
            LbCantidad.TabIndex = 9;
            LbCantidad.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MenuSaldo
            // 
            MenuSaldo.Items.AddRange(new ToolStripItem[] { copiarToolStripMenuItem });
            MenuSaldo.Name = "MenuSaldo";
            MenuSaldo.Size = new Size(110, 26);
            MenuSaldo.Text = "Copiar";
            // 
            // copiarToolStripMenuItem
            // 
            copiarToolStripMenuItem.Name = "copiarToolStripMenuItem";
            copiarToolStripMenuItem.Size = new Size(109, 22);
            copiarToolStripMenuItem.Text = "Copiar";
            copiarToolStripMenuItem.Click += copiarToolStripMenuItem_Click;
            // 
            // ChPeriodo
            // 
            ChPeriodo.AutoSize = true;
            ChPeriodo.Cursor = Cursors.Hand;
            ChPeriodo.ForeColor = SystemColors.Control;
            ChPeriodo.Location = new Point(181, 15);
            ChPeriodo.Name = "ChPeriodo";
            ChPeriodo.Size = new Size(96, 26);
            ChPeriodo.TabIndex = 10;
            ChPeriodo.Text = "Periodo";
            ChPeriodo.UseVisualStyleBackColor = true;
            ChPeriodo.CheckedChanged += ChPeriodo_CheckedChanged;
            // 
            // DateInicio
            // 
            DateInicio.CalendarFont = new Font("Arial", 18F);
            DateInicio.Format = DateTimePickerFormat.Short;
            DateInicio.Location = new Point(150, 47);
            DateInicio.Name = "DateInicio";
            DateInicio.Size = new Size(157, 29);
            DateInicio.TabIndex = 11;
            // 
            // DateFin
            // 
            DateFin.CalendarFont = new Font("Arial", 18F);
            DateFin.Format = DateTimePickerFormat.Short;
            DateFin.Location = new Point(150, 97);
            DateFin.Name = "DateFin";
            DateFin.Size = new Size(157, 29);
            DateFin.TabIndex = 12;
            DateFin.Value = new DateTime(2025, 1, 24, 0, 0, 0, 0);
            // 
            // LbTitulo
            // 
            LbTitulo.Anchor = AnchorStyles.Top;
            LbTitulo.AutoSize = true;
            LbTitulo.Font = new Font("Georgia", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LbTitulo.ForeColor = Color.Gold;
            LbTitulo.Location = new Point(407, 9);
            LbTitulo.Name = "LbTitulo";
            LbTitulo.Size = new Size(0, 31);
            LbTitulo.TabIndex = 13;
            LbTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Principal
            // 
            AutoScaleDimensions = new SizeF(11F, 22F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(50, 50, 50);
            ClientSize = new Size(1008, 729);
            Controls.Add(LbTitulo);
            Controls.Add(DateFin);
            Controls.Add(DateInicio);
            Controls.Add(ChPeriodo);
            Controls.Add(LbCantidad);
            Controls.Add(TextoSaldo);
            Controls.Add(ChReferencia);
            Controls.Add(Grid);
            Controls.Add(Buscar);
            Controls.Add(CbConceptos);
            Controls.Add(Texto2);
            Controls.Add(TxtFolio);
            Controls.Add(BtnCargar);
            Controls.Add(Texto1);
            Font = new Font("Arial", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            Name = "Principal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SAPlugin";
            WindowState = FormWindowState.Maximized;
            Load += Principal_Load;
            ((System.ComponentModel.ISupportInitialize)Grid).EndInit();
            MenuSaldo.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Texto1;
        private Button BtnCargar;
        private OpenFileDialog AbrirArchivo;
        private TextBox TxtFolio;
        private Label Texto2;
        private ComboBox CbConceptos;
        private Button Buscar;
        private DataGridView Grid;
        private CheckBox ChReferencia;
        private Label TextoSaldo;
        private Label LbCantidad;
        private ContextMenuStrip MenuSaldo;
        private ToolStripMenuItem copiarToolStripMenuItem;
        private CheckBox ChPeriodo;
        private DateTimePicker DateInicio;
        private DateTimePicker DateFin;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private Label LbTitulo;
    }
}
