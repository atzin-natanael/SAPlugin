using System.Windows.Forms;
using System.Xml.Linq;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SapPlugin
{
    public partial class Principal : Form
    {
        List<List<string>> ListaMain = new List<List<string>>();
        List<List<string>> ListaNotas = new List<List<string>>();
        List<List<string>> ListaClientes = new List<List<string>>();

        public Principal()
        {
            InitializeComponent();
            Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            CbConceptos.Visible = false;
            CargarConceptos();
            TxtFolio.Visible = false;
            Texto2.Visible = false;
            Buscar.Visible = false;
            Grid.Visible = false;
            ChReferencia.Visible = false;
            LbCantidad.Visible = false;
            TextoSaldo.Visible = false;
            ChPeriodo.Visible = false;
            DateInicio.Visible = false;
            DateFin.Visible = false;
            Exportar.Visible = false;
        }
        public void CargarConceptos()
        {
            CbConceptos.Items.Add("Cliente");
            CbConceptos.Items.Add("Asignación");
            CbConceptos.Items.Add("Clase");
            CbConceptos.Items.Add("No Documento");
            CbConceptos.Items.Add("Factura");
            CbConceptos.Items.Add("Fecha de Documento");
            CbConceptos.Items.Add("Fecha de Vencimiento");
            CbConceptos.Items.Add("Importe");
            CbConceptos.Items.Add("Referencia");
            CbConceptos.Items.Add("Condición de Pago");
        }
        private void BtnCargar_Click(object sender, EventArgs e)
        {
            string FileName = "C:\\Users\\NPACHECO\\Music\\CARTERA.XLSX";
            try
            {
                // Abre el archivo Excel

                using (XLWorkbook ExcelMain = new XLWorkbook(FileName))
                {
                    var HojaMain = ExcelMain.Worksheet(1);
                    int Filas = HojaMain.RowsUsed().Count();
                    for (int i = 2; i <= Filas; i++)
                    {
                        if (!string.IsNullOrEmpty(HojaMain.Cell("A" + i).Value.ToString()))
                        {
                            List<string> Datos = new List<string>();
                            string Cliente = HojaMain.Cell("A" + i).Value.ToString();
                            string Asignacion = HojaMain.Cell("B" + i).Value.ToString();
                            string ClaseDocumento = HojaMain.Cell("E" + i).Value.ToString();
                            string NoDocumento = HojaMain.Cell("D" + i).Value.ToString();
                            string Factura = HojaMain.Cell("H" + i).Value.ToString();
                            string FechaDocumento = HojaMain.Cell("I" + i).Value.ToString();
                            string FechaVencimiento = HojaMain.Cell("J" + i).Value.ToString();
                            string Importe = HojaMain.Cell("K" + i).Value.ToString();
                            string Referencia = HojaMain.Cell("M" + i).Value.ToString();
                            string CondicionPago = HojaMain.Cell("Q" + i).Value.ToString();
                            string FD = "";
                            string FV = "";
                            if (FechaDocumento != string.Empty && FechaVencimiento != string.Empty)
                            {
                                FD = FechaDocumento.Substring(0, 10);
                                FV = FechaVencimiento.Substring(0, 10);
                            }
                            Datos.Add(Cliente);
                            Datos.Add(Asignacion);
                            Datos.Add(ClaseDocumento);
                            Datos.Add(NoDocumento);
                            Datos.Add(Factura);
                            Datos.Add(FD);
                            Datos.Add(FV);
                            Datos.Add(Importe);
                            Datos.Add(Referencia);
                            Datos.Add(CondicionPago);
                            ListaMain.Add(Datos);

                        }
                    }
                }
                }catch (Exception ex)
            {
                // Captura cualquier otro tipo de error
                MessageBox.Show("Error al procesar el archivo Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string FileNameNotas = "C:\\Users\\NPACHECO\\Music\\Relacion de Notas.xlsx";
                try
                {
                    // Abre el archivo Excel
                    HashSet<string> DocumentosUnicos = new HashSet<string>();
                    using (XLWorkbook ExcelMainNotas = new XLWorkbook(FileNameNotas))
                    {
                        var HojaMainNotas = ExcelMainNotas.Worksheet(1);
                        int Filas = HojaMainNotas.RowsUsed().Count();
                        for (int i = 2; i <= Filas; i++)
                        {
                            string Documento = HojaMainNotas.Cell("A" + i).Value.ToString();

                            // Verificamos si el número de documento ya está en el HashSet
                            if (!string.IsNullOrEmpty(HojaMainNotas.Cell("P" + i).Value.ToString()) && !DocumentosUnicos.Contains(Documento))
                            {
                                string Factura = HojaMainNotas.Cell("P" + i).Value.ToString();

                                // Si no está repetido, agregamos el número de documento al HashSet
                                DocumentosUnicos.Add(Documento);

                                // Agregamos los datos a la lista
                                List<string> DatosN = new List<string> { Documento, Factura };
                                ListaNotas.Add(DatosN);
                            }
                        }
                    }
                    LbTitulo.Text = Path.GetFileName(FileName);
                    BtnCargar.Visible = false;
                    Texto1.Visible = false;
                    CbConceptos.Visible = true;
                    TxtFolio.Visible = true;
                    Texto2.Visible = true;
                    Buscar.Visible = true;
                    Grid.Visible = true;
                    ChReferencia.Visible = true;
                    Exportar.Visible = true;
                    CbConceptos.SelectedIndex = 0;
                    CbConceptos.Focus();
                }
                catch (Exception ex)
                {
                    // Captura cualquier otro tipo de error
                    MessageBox.Show("Error al procesar el archivo Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            string FileNameClientes = "C:\\Users\\NPACHECO\\Music\\Copia de Project UNO Book of Record V21.xlsx";
            try
            {
                // Abre el archivo Excel
                HashSet<string> NumCliente = new HashSet<string>();
                using (XLWorkbook ExcelMainClientes = new XLWorkbook(FileNameClientes))
                {
                    var HojaMainClientes = ExcelMainClientes.Worksheet(1);
                    int Filas = HojaMainClientes.RowsUsed().Count();
                    for (int i = 2; i <= Filas; i++)
                    {
                        string ClaveCliente = HojaMainClientes.Cell("AH" + i).Value.ToString();

                        // Verificamos si el número de documento ya está en el HashSet
                        if (!string.IsNullOrEmpty(HojaMainClientes.Cell("AH" + i).Value.ToString()) && !NumCliente.Contains(ClaveCliente))
                        {
                            string Nombre = HojaMainClientes.Cell("AJ" + i).Value.ToString();

                            // Si no está repetido, agregamos el número de documento al HashSet
                            NumCliente.Add(ClaveCliente);

                            // Agregamos los datos a la lista
                            List<string> DatosC = new List<string> { ClaveCliente, Nombre };
                            ListaClientes.Add(DatosC);
                        }
                    }
                }
                LbTitulo.Text = Path.GetFileName(FileName);
                BtnCargar.Visible = false;
                Texto1.Visible = false;
                CbConceptos.Visible = true;
                TxtFolio.Visible = true;
                Texto2.Visible = true;
                Buscar.Visible = true;
                Grid.Visible = true;
                ChReferencia.Visible = true;
                Exportar.Visible = true;
                CbConceptos.SelectedIndex = 0;
                CbConceptos.Focus();
            }
            catch (Exception ex)
            {
                // Captura cualquier otro tipo de error
                MessageBox.Show("Error al procesar el archivo Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public string Referencia(string Factura)
        {
            foreach (var sublista in ListaMain)
            {
                // Verificar si la sublista contiene el valor que buscas
                if (sublista[4].ToString() == Factura && sublista[2] == "CI")
                {
                    return sublista[8].ToString();
                }
            }
            return null;

        }
        private void Buscar_Click(object sender, EventArgs e)
        {
            Grid.Rows.Clear();
            bool encontrado = false;
            decimal saldo = 0;
            if (CbConceptos.Text != string.Empty && TxtFolio.Text != string.Empty)
            {
                switch (CbConceptos.Text)
                {
                    case "Cliente":
                        if (ChPeriodo.Checked)
                        {
                            foreach (var sublista in ListaMain)
                            {
                                // Verificar si la sublista contiene el valor que buscas
                                if (sublista[0].ToString() == TxtFolio.Text && DateTime.Parse(sublista[5].ToString()) <= DateTime.Parse(DateFin.Text) && DateTime.Parse(sublista[5].ToString()) >= DateTime.Parse(DateInicio.Text))
                                {
                                    Grid.Rows.Add(sublista[0], sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[5].Style.BackColor = System.Drawing.Color.Gold;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[0].Style.BackColor = System.Drawing.Color.Gold;
                                    encontrado = true;
                                }
                            }
                        }
                        else
                        {
                            foreach (var sublista in ListaMain)
                            {
                                // Verificar si la sublista contiene el valor que buscas
                                if (sublista[0].ToString() == TxtFolio.Text)
                                {
                                    Grid.Rows.Add(sublista[0], sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    if (sublista[2] == "CI")
                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.CadetBlue;
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[0].Style.BackColor = System.Drawing.Color.Gold;
                                    encontrado = true;
                                }
                            }

                        }
                        Grid.ClearSelection();
                        if (!encontrado)
                        {
                            MessageBox.Show("No se encontraron resultados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case "Asignación":
                        if (ChPeriodo.Checked)
                        {
                            foreach (var sublista in ListaMain)
                            {
                                // Verificar si la sublista contiene el valor que buscas
                                if (sublista[1].ToString() == TxtFolio.Text && DateTime.Parse(sublista[5].ToString()) <= DateTime.Parse(DateFin.Text) && DateTime.Parse(sublista[5].ToString()) >= DateTime.Parse(DateInicio.Text))
                                {
                                    Grid.Rows.Add(sublista[0], sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[5].Style.BackColor = System.Drawing.Color.Gold;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[1].Style.BackColor = System.Drawing.Color.Gold;
                                    encontrado = true;
                                }
                            }
                        }
                        else
                        {
                            foreach (var sublista in ListaMain)
                            {
                                // Verificar si la sublista contiene el valor que buscas
                                if (sublista[1].ToString() == TxtFolio.Text)
                                {
                                    Grid.Rows.Add(sublista[0], sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    if (sublista[2] == "CI")
                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.CadetBlue;
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[1].Style.BackColor = System.Drawing.Color.Gold;
                                    encontrado = true;
                                }
                            }

                        }
                        Grid.ClearSelection();
                        if (!encontrado)
                        {
                            MessageBox.Show("No se encontraron resultados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case "Clase":
                        encontrado = false;
                        if (ChPeriodo.Checked)
                        {
                            foreach (var sublista in ListaMain)
                            {
                                // Verificar si la sublista contiene el valor que buscas
                                if (sublista[2].ToString() == TxtFolio.Text && DateTime.Parse(sublista[5].ToString()) <= DateTime.Parse(DateFin.Text) && DateTime.Parse(sublista[5].ToString()) >= DateTime.Parse(DateInicio.Text))
                                {
                                    Grid.Rows.Add(sublista[0], sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[5].Style.BackColor = System.Drawing.Color.Gold;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[2].Style.BackColor = System.Drawing.Color.Gold;
                                    encontrado = true;
                                }
                            }
                        }
                        else
                        {
                            foreach (var sublista in ListaMain)
                            {
                                // Verificar si la sublista contiene el valor que buscas
                                if (sublista[2].ToString() == TxtFolio.Text)
                                {
                                    Grid.Rows.Add(sublista[0], sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[2].Style.BackColor = System.Drawing.Color.Gold;
                                    encontrado = true;
                                }
                            }

                        }
                        Grid.ClearSelection();
                        if (!encontrado)
                        {
                            MessageBox.Show("No se encontraron resultados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case "No Documento":
                        encontrado = false;
                        if (ChPeriodo.Checked)
                        {
                            foreach (var sublista in ListaMain)
                            {
                                // Verificar si la sublista contiene el valor que buscas
                                if (sublista[3].ToString() == TxtFolio.Text && DateTime.Parse(sublista[5].ToString()) <= DateTime.Parse(DateFin.Text) && DateTime.Parse(sublista[5].ToString()) >= DateTime.Parse(DateInicio.Text))
                                {
                                    Grid.Rows.Add(sublista[0], sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[5].Style.BackColor = System.Drawing.Color.Gold;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[3].Style.BackColor = System.Drawing.Color.Gold;
                                    encontrado = true;
                                }
                            }
                        }
                        else
                        {
                            foreach (var sublista in ListaMain)
                            {
                                // Verificar si la sublista contiene el valor que buscas
                                if (sublista[3].ToString() == TxtFolio.Text)
                                {
                                    Grid.Rows.Add(sublista[0], sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    if (sublista[2] == "CI")
                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.CadetBlue;
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[3].Style.BackColor = System.Drawing.Color.Gold;
                                    encontrado = true;
                                }
                            }

                        }
                        Grid.ClearSelection();
                        if (!encontrado)
                        {
                            MessageBox.Show("No se encontraron resultados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case "Factura":
                        encontrado = false;
                        string referencia = "";
                        if (ChReferencia.Checked)
                        {
                            LbCantidad.Visible = true;
                            TextoSaldo.Visible = true;
                            referencia = Referencia(TxtFolio.Text);
                        }
                        else
                        {
                            LbCantidad.Visible = false;
                            TextoSaldo.Visible = false;
                        }
                        foreach (var sublista in ListaMain)
                        {
                            if (ChReferencia.Checked)
                            {
                                if (sublista[4].ToString() == TxtFolio.Text || sublista[8].ToString() == referencia)
                                {
                                    Grid.Rows.Add(sublista[0], sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    if (sublista[2] == "CI")
                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.CadetBlue;
                                    if (sublista[8].ToString() == referencia)
                                    {
                                        Grid.Rows[Grid.Rows.Count - 1].Cells[8].Style.BackColor = System.Drawing.Color.Gold;
                                    }
                                    if (sublista[4].ToString() == TxtFolio.Text)
                                    {
                                        Grid.Rows[Grid.Rows.Count - 1].Cells[4].Style.BackColor = System.Drawing.Color.Gold;
                                    }
                                    //if (sublista[2] == "CD")
                                    //    saldo -= decimal.Parse(sublista[7].ToString());
                                    //else
                                    saldo += decimal.Parse(sublista[7].ToString());
                                    LbCantidad.Text = saldo.ToString();
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    encontrado = true;
                                }

                            }
                            else
                            {
                                if (sublista[4].ToString() == TxtFolio.Text)
                                {
                                    Grid.Rows.Add(sublista[0], sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    if (sublista[2] == "CI")
                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.CadetBlue;
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[4].Style.BackColor = System.Drawing.Color.Gold;
                                    encontrado = true;
                                }
                            }
                            // Verificar si la sublista contiene el valor que buscas
                        }
                        Grid.ClearSelection();
                        if (!encontrado)
                        {
                            MessageBox.Show("No se encontraron resultados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case "Fecha de Documento":
                        encontrado = false;
                        foreach (var sublista in ListaMain)
                        {
                            // Verificar si la sublista contiene el valor que buscas
                            if (sublista[5].ToString() == TxtFolio.Text)
                            {
                                Grid.Rows.Add(sublista[0], sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                if (sublista[2] == "CI")
                                    Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.CadetBlue;
                                Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                Grid.Rows[Grid.Rows.Count - 1].Cells[5].Style.BackColor = System.Drawing.Color.Gold;
                                encontrado = true;
                            }
                        }
                        Grid.ClearSelection();
                        if (!encontrado)
                        {
                            MessageBox.Show("No se encontraron resultados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case "Fecha de Vencimiento":
                        encontrado = false;
                        foreach (var sublista in ListaMain)
                        {
                            // Verificar si la sublista contiene el valor que buscas
                            if (sublista[6].ToString() == TxtFolio.Text)
                            {
                                Grid.Rows.Add(sublista[0], sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                if (sublista[2] == "CI")
                                    Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.CadetBlue;
                                Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                Grid.Rows[Grid.Rows.Count - 1].Cells[6].Style.BackColor = System.Drawing.Color.Gold;
                                encontrado = true;
                            }
                        }
                        Grid.ClearSelection();
                        if (!encontrado)
                        {
                            MessageBox.Show("No se encontraron resultados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case "Importe":
                        encontrado = false;
                        try
                        {
                            foreach (var sublista in ListaMain)
                            {
                                // Verificar si la sublista contiene el valor que buscas

                                if (decimal.Parse(sublista[7].ToString()) == decimal.Parse(TxtFolio.Text))
                                {
                                    Grid.Rows.Add(sublista[0], sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    if (sublista[2] == "CI")
                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.CadetBlue;
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[7].Style.BackColor = System.Drawing.Color.Gold;
                                    encontrado = true;
                                }

                            }
                        }
                        catch
                        {
                            MessageBox.Show("Agrega un valor valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        Grid.ClearSelection();
                        if (!encontrado)
                        {
                            MessageBox.Show("No se encontraron resultados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case "Referencia":
                        encontrado = false;
                        if (ChPeriodo.Checked)
                        {
                            foreach (var sublista in ListaMain)
                            {
                                // Verificar si la sublista contiene el valor que buscas
                                if (sublista[8].ToString() == TxtFolio.Text && DateTime.Parse(sublista[5].ToString()) <= DateTime.Parse(DateFin.Text) && DateTime.Parse(sublista[5].ToString()) >= DateTime.Parse(DateInicio.Text))
                                {
                                    Grid.Rows.Add(sublista[0], sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[5].Style.BackColor = System.Drawing.Color.Gold;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[8].Style.BackColor = System.Drawing.Color.Gold;
                                    encontrado = true;
                                }
                            }
                        }
                        else
                        {
                            foreach (var sublista in ListaMain)
                            {
                                // Verificar si la sublista contiene el valor que buscas
                                if (sublista[8].ToString() == TxtFolio.Text)
                                {
                                    Grid.Rows.Add(sublista[0], sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    if (sublista[2] == "CI")
                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.CadetBlue;
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[8].Style.BackColor = System.Drawing.Color.Gold;
                                    encontrado = true;
                                }
                            }

                        }
                        Grid.ClearSelection();
                        if (!encontrado)
                        {
                            MessageBox.Show("No se encontraron resultados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case "Condición de Pago":
                        encontrado = false;
                        if (ChPeriodo.Checked)
                        {
                            foreach (var sublista in ListaMain)
                            {
                                // Verificar si la sublista contiene el valor que buscas
                                if (sublista[9].ToString() == TxtFolio.Text && DateTime.Parse(sublista[5].ToString()) <= DateTime.Parse(DateFin.Text) && DateTime.Parse(sublista[5].ToString()) >= DateTime.Parse(DateInicio.Text))
                                {
                                    Grid.Rows.Add(sublista[0], sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[5].Style.BackColor = System.Drawing.Color.Gold;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[9].Style.BackColor = System.Drawing.Color.Gold;
                                    encontrado = true;
                                }
                            }
                        }
                        else
                        {
                            foreach (var sublista in ListaMain)
                            {
                                // Verificar si la sublista contiene el valor que buscas
                                if (sublista[9].ToString() == TxtFolio.Text)
                                {
                                    Grid.Rows.Add(sublista[0], sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    if (sublista[2] == "CI")
                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.CadetBlue;
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[9].Style.BackColor = System.Drawing.Color.Gold;
                                    encontrado = true;
                                }
                            }

                        }
                        Grid.ClearSelection();
                        if (!encontrado)
                        {
                            MessageBox.Show("No se encontraron resultados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            Grid.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;  // Color más oscuro para las filas alternas
            Grid.AlternatingRowsDefaultCellStyle.ForeColor = System.Drawing.Color.Black;      // Color del texto de las filas alternas

            // Configurar las filas no alternas (filas impares)
            Grid.DefaultCellStyle.BackColor = System.Drawing.Color.White;  // Color de fondo para las filas impares
            Grid.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;   // Color del texto para las filas impares (opcional)
        }

        private void CbConceptos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbConceptos.Text == "Factura")
            {
                ChReferencia.Visible = true;
            }
            else
            {
                ChReferencia.Visible = false;
                LbCantidad.Visible = false;
                TextoSaldo.Visible = false;
            }
            if (CbConceptos.Text == "Clase" || CbConceptos.Text == "Cliente" || CbConceptos.Text == "Asignación" || CbConceptos.Text == "No. Documento" || CbConceptos.Text == "Referencia" || CbConceptos.Text == "Condición de Pago")
            {
                ChPeriodo.Visible = true;

            }
            else
            {
                ChPeriodo.Visible = false;
                DateInicio.Visible = false;
                DateFin.Visible = false;

            }
        }
        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(LbCantidad.Text);
        }

        private void ChPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            DateInicio.Visible = ChPeriodo.Checked;
            DateFin.Visible = ChPeriodo.Checked;
        }

        private void Exportar_Click(object sender, EventArgs e)
        {
        }
    }
}
