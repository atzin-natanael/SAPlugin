using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.CustomUI;
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
            LbCantidad.Visible = false;
            TextoSaldo.Visible = false;
            ChPeriodo.Visible = false;
            DateInicio.Visible = false;
            DateFin.Visible = false;
            Exportar.Visible = false;
            RadioAB.Visible = false;
            RadioCI.Visible = false;
            CargarExcel();
        }
        public void CargarConceptos()
        {
            CbConceptos.Items.Add("Cliente");
            CbConceptos.Items.Add("Relación AB");
            CbConceptos.Items.Add("Asignación");
            CbConceptos.Items.Add("Clase");
            CbConceptos.Items.Add("No Documento");
            CbConceptos.Items.Add("Factura");
            CbConceptos.Items.Add("Fecha de Documento");
            CbConceptos.Items.Add("Fecha de Vencimiento");
            CbConceptos.Items.Add("Importe");
            //CbConceptos.Items.Add("Referencia");
            CbConceptos.Items.Add("Condición de Pago");
        }
        public void CargarExcel()
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
                            string Referencia = HojaMain.Cell("N" + i).Value.ToString();
                            string CondicionPago = HojaMain.Cell("Q" + i).Value.ToString();
                            string Referencia2 = HojaMain.Cell("O" + i).Value.ToString();
                            string CG = HojaMain.Cell("G" + i).Value.ToString();
                            string FD = "";
                            string FV = "";
                            if (FechaDocumento != string.Empty && FechaVencimiento != string.Empty)
                            {
                                FD = FechaDocumento.Substring(0, 10);
                                FV = FechaVencimiento.Substring(0, 10);
                            }
                            decimal importe = decimal.Parse(Importe);
                            Datos.Add(Cliente);
                            Datos.Add(Asignacion);
                            Datos.Add(ClaseDocumento);
                            Datos.Add(NoDocumento);
                            Datos.Add(Factura);
                            Datos.Add(FD);
                            Datos.Add(FV);
                            Datos.Add(importe.ToString("N"));
                            Datos.Add(Referencia);
                            Datos.Add(CondicionPago);
                            Datos.Add(Referencia2);
                            Datos.Add(CG);
                            ListaMain.Add(Datos);

                        }
                    }
                }
            }
            catch (Exception ex)
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
                CbConceptos.Visible = true;
                TxtFolio.Visible = true;
                Texto2.Visible = true;
                Buscar.Visible = true;
                Grid.Visible = true;
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
                CbConceptos.Visible = true;
                TxtFolio.Visible = true;
                Texto2.Visible = true;
                Buscar.Visible = true;
                Grid.Visible = true;
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
            string ClienteNombre = string.Empty;
            int id = 0;
            Grid.Columns[10].Visible = true;
            bool toggleColor = false; // Variable para alternar entre los colores de fondo
            ListaMain = ListaMain.OrderByDescending(datos => DateTime.Parse(datos[5].ToString())).ToList();
            if (CbConceptos.Text != string.Empty && TxtFolio.Text != string.Empty)
            {
                switch (CbConceptos.Text)
                {
                    case "Relación AB":
                        LbCantidad.Visible = false;
                        TextoSaldo.Visible = false;
                        ClienteNombre = string.Empty;
                        int fila2 = 0;
                        foreach (var sublista in ListaMain)
                        {
                            decimal saldos2 = 0;
                            bool bandera2 = false;
                            if ((sublista[8] == TxtFolio.Text) || sublista[10] == TxtFolio.Text)
                            {
                                id++;
                                toggleColor = !toggleColor;

                                foreach (var Cliente in ListaClientes)
                                {
                                    if (Cliente[0] == sublista[0])
                                    {
                                        ClienteNombre = Cliente[1];
                                    }
                                }
                                Grid.Columns[10].HeaderText = "Saldo";
                                // Añadir la fila al DataGridView
                                Grid.Rows.Add(id, sublista[0], ClienteNombre, sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], "", sublista[9]);
                                fila2 = Grid.Rows.Count;
                                // Cambiar el color de fondo alternado
                                Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = toggleColor ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
                                // Cambiar la altura de la fila
                                Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                saldos2 += decimal.Parse(sublista[7].ToString());
                                bandera2 = true;
                                // Alternar el valor del color para la siguiente fila
                                //toggleColor = !toggleColor;
                                encontrado = true;

                                string referenciaFact = sublista[8].ToString();
                                string referenciaFact2 = sublista[10].ToString();
                                bool ReferenciaEncontrada = false;
                                foreach (var Buc in ListaMain)
                                {
                                    if (Buc[2] == "DZ" && (Buc[11].ToString() == referenciaFact && referenciaFact != string.Empty || Buc[11].ToString() == referenciaFact2 && referenciaFact2 != string.Empty))
                                    {
                                        Grid.Rows.Add(id, Buc[0], ClienteNombre, Buc[1], Buc[2], Buc[3], Buc[4], Buc[5], Buc[6], Buc[7], Buc[8], Buc[9]);
                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = toggleColor ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
                                        Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                        bandera2 = true;
                                        saldos2 += decimal.Parse(Buc[7].ToString());
                                    }
                                }
                                foreach (var sublista2 in ListaNotas)
                                {
                                    if (sublista2[1].ToString() == referenciaFact)
                                    {
                                        ReferenciaEncontrada = true;
                                        foreach (var Buscardocto in ListaMain)
                                        {
                                            if (Buscardocto[3] == sublista2[0])
                                            {
                                                Grid.Rows.Add(id, Buscardocto[0], ClienteNombre, Buscardocto[1], Buscardocto[2], Buscardocto[3], Buscardocto[4], Buscardocto[5], Buscardocto[6], Buscardocto[7], "", Buscardocto[9]);
                                                Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = toggleColor ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
                                                Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                                saldos2 += decimal.Parse(Buscardocto[7].ToString());
                                                bandera2 = true;
                                                //toggleColor = !toggleColor; // Alternar el color para la siguiente fila
                                            }
                                        }
                                    }
                                }

                                if (ReferenciaEncontrada == false)
                                {
                                    foreach (var sublista2 in ListaNotas)
                                    {
                                        if (sublista2[1].ToString() == referenciaFact2)
                                        {
                                            foreach (var Buscardocto in ListaMain)
                                            {
                                                if (Buscardocto[3] == sublista2[0])
                                                {
                                                    Grid.Rows.Add(id, Buscardocto[0], ClienteNombre, Buscardocto[1], Buscardocto[2], Buscardocto[3], Buscardocto[4], Buscardocto[5], Buscardocto[6], Buscardocto[7], "", Buscardocto[9]);
                                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                                    Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = toggleColor ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
                                                    saldos2 += decimal.Parse(Buscardocto[7].ToString());
                                                    bandera2 = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (bandera2)
                            {
                                Grid.Rows[fila2 - 1].Cells[10].Value = saldos2.ToString("N");
                            }
                        }
                        Grid.ClearSelection();
                        if (!encontrado)
                        {
                            MessageBox.Show("No se encontraron resultados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case "Cliente":
                        LbCantidad.Visible = false;
                        TextoSaldo.Visible = false;
                        int fila = 0;
                        int fila3 = 0;
                        Grid.Columns[10].Visible = true;
                        Grid.Columns[10].HeaderText = "Saldo";
                        decimal saldos = 0;
                        decimal saldos4 = 0;
                        if (RadioCI.Checked)
                        {
                            foreach (var sublista in ListaMain)
                            {
                                bool bandera = false;
                                if (ChPeriodo.Checked)
                                {
                                    if (sublista[2] == "CI" && sublista[0] == TxtFolio.Text && DateTime.Parse(sublista[5].ToString()) <= DateTime.Parse(DateFin.Text) && DateTime.Parse(sublista[5].ToString()) >= DateTime.Parse(DateInicio.Text))
                                    {

                                        foreach (var Cliente in ListaClientes)
                                        {
                                            if (Cliente[0] == sublista[0])
                                            {
                                                ClienteNombre = Cliente[1];
                                            }
                                        }
                                        toggleColor = !toggleColor;
                                        id++;
                                        encontrado = true;
                                        Grid.Rows.Add(id, sublista[0], ClienteNombre, sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], "Saldo", sublista[9]);
                                        fila = Grid.Rows.Count;
                                        bandera = true;
                                        saldos = 0;
                                        saldos += decimal.Parse(sublista[7].ToString());
                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = toggleColor ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
                                        Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                        foreach (var revisar in ListaMain)
                                        {
                                            if (revisar[2] != "CI" && revisar[4] == sublista[4])
                                            {
                                                Grid.Rows.Add(id, revisar[0], ClienteNombre, revisar[1], revisar[2], revisar[3], revisar[4], revisar[5], revisar[6], revisar[7], "", revisar[9]);
                                                Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = toggleColor ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
                                                Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                                saldos += decimal.Parse(revisar[7].ToString());
                                                bandera = true;
                                            }
                                        }
                                        foreach (var notas in ListaNotas)
                                        {
                                            if (notas[1] == sublista[4])
                                            {
                                                foreach (var Buscardocto in ListaMain)
                                                {
                                                    if (Buscardocto[3] == notas[0])
                                                    {
                                                        Grid.Rows.Add(id, Buscardocto[0], ClienteNombre, Buscardocto[1], Buscardocto[2], Buscardocto[3], Buscardocto[4], Buscardocto[5], Buscardocto[6], Buscardocto[7], "", Buscardocto[9]);
                                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = toggleColor ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
                                                        Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                                        saldos += decimal.Parse(Buscardocto[7].ToString());
                                                        bandera = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (sublista[2] == "CI" && sublista[0] == TxtFolio.Text)
                                    {
                                        foreach (var Cliente in ListaClientes)
                                        {
                                            if (Cliente[0] == sublista[0])
                                            {
                                                ClienteNombre = Cliente[1];
                                            }
                                        }
                                        toggleColor = !toggleColor;
                                        id++;
                                        encontrado = true;
                                        Grid.Rows.Add(id, sublista[0], ClienteNombre, sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], "saldo", sublista[9]);
                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = toggleColor ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
                                        Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                        fila = Grid.Rows.Count;
                                        bandera = true;
                                        saldos = 0;
                                        saldos += decimal.Parse(sublista[7].ToString());
                                        foreach (var revisar in ListaMain)
                                        {
                                            if (revisar[2] != "CI" && revisar[4] == sublista[4])
                                            {
                                                Grid.Rows.Add(id, revisar[0], ClienteNombre, revisar[1], revisar[2], revisar[3], revisar[4], revisar[5], revisar[6], revisar[7], "", revisar[9]);
                                                Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = toggleColor ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
                                                Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                                bandera = true;
                                                saldos += decimal.Parse(revisar[7].ToString());
                                            }
                                        }
                                        foreach (var notas in ListaNotas)
                                        {
                                            if (notas[1] == sublista[4])
                                            {
                                                foreach (var Buscardocto in ListaMain)
                                                {
                                                    if (Buscardocto[3] == notas[0])
                                                    {
                                                        Grid.Rows.Add(id, Buscardocto[0], ClienteNombre, Buscardocto[1], Buscardocto[2], Buscardocto[3], Buscardocto[4], Buscardocto[5], Buscardocto[6], Buscardocto[7], "", Buscardocto[9]);
                                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = toggleColor ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
                                                        Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                                        bandera = true;
                                                        saldos += decimal.Parse(Buscardocto[7].ToString());
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (bandera)
                                {
                                    Grid.Rows[fila - 1].Cells[10].Value = saldos.ToString("N");
                                }
                            }
                        }
                        if (RadioAB.Checked)
                        {
                            foreach (var sublista in ListaMain)
                            {
                                bool bandera3 = false;
                                if (ChPeriodo.Checked)
                                {
                                    if (sublista[2] == "AB" && sublista[0] == TxtFolio.Text && DateTime.Parse(sublista[5].ToString()) <= DateTime.Parse(DateFin.Text) && DateTime.Parse(sublista[5].ToString()) >= DateTime.Parse(DateInicio.Text))
                                    {
                                        string referenciaFact = sublista[8].ToString();
                                        string referenciaFact2 = sublista[10].ToString();
                                        bool ReferenciaEncontrada = false;
                                        foreach (var Cliente in ListaClientes)
                                        {
                                            if (Cliente[0] == sublista[0])
                                            {
                                                ClienteNombre = Cliente[1];
                                            }
                                        }
                                        id++;
                                        toggleColor = !toggleColor;
                                        encontrado = true;
                                        Grid.Rows.Add(id, sublista[0], ClienteNombre, sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], "", sublista[9]);
                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = toggleColor ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
                                        Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                        fila3 = Grid.Rows.Count;
                                        bandera3 = true;
                                        saldos4 = 0;
                                        saldos4 += decimal.Parse(sublista[7].ToString());
                                        foreach (var Buc in ListaMain)
                                        {
                                            if (Buc[2] == "DZ" && (Buc[11].ToString() == referenciaFact && referenciaFact != string.Empty || Buc[11].ToString() == referenciaFact2 && referenciaFact2 != string.Empty))
                                            {
                                                Grid.Rows.Add(id, Buc[0], ClienteNombre, Buc[1], Buc[2], Buc[3], Buc[4], Buc[5], Buc[6], Buc[7], "", Buc[9]);
                                                Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = toggleColor ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
                                                Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                                saldos4 += decimal.Parse(Buc[7].ToString());
                                                bandera3 = true;

                                            }
                                        }
                                        foreach (var sublista2 in ListaNotas)
                                        {
                                            if (sublista2[1].ToString() == referenciaFact)
                                            {
                                                ReferenciaEncontrada = true;
                                                foreach (var Buscardocto in ListaMain)
                                                {
                                                    if (Buscardocto[3] == sublista2[0])
                                                    {
                                                        Grid.Rows.Add(id, Buscardocto[0], ClienteNombre, Buscardocto[1], Buscardocto[2], Buscardocto[3], Buscardocto[4], Buscardocto[5], Buscardocto[6], Buscardocto[7],"", Buscardocto[9]);
                                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = toggleColor ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
                                                        Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                                        bandera3 = true;
                                                        saldos4 += decimal.Parse(Buscardocto[7].ToString());
                                                        //toggleColor = !toggleColor; // Alternar el color para la siguiente fila
                                                    }
                                                }
                                            }
                                        }

                                        if (ReferenciaEncontrada == false)
                                        {
                                            foreach (var sublista2 in ListaNotas)
                                            {
                                                if (sublista2[1].ToString() == referenciaFact2)
                                                {
                                                    foreach (var Buscardocto in ListaMain)
                                                    {
                                                        if (Buscardocto[3] == sublista2[0])
                                                        {
                                                            Grid.Rows.Add(id, Buscardocto[0], ClienteNombre, Buscardocto[1], Buscardocto[2], Buscardocto[3], Buscardocto[4], Buscardocto[5], Buscardocto[6], Buscardocto[7], "", Buscardocto[9]);
                                                            Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                                            saldos4 += decimal.Parse(Buscardocto[7].ToString());
                                                            bandera3 = true;
                                                            Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = toggleColor ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
                                                            //toggleColor = !toggleColor; // Alternar el color para la siguiente fila
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (sublista[2] == "AB" && sublista[0] == TxtFolio.Text)
                                    {
                                        string referenciaFact = sublista[8].ToString();
                                        string referenciaFact2 = sublista[10].ToString();
                                        bool ReferenciaEncontrada = false;
                                        foreach (var Cliente in ListaClientes)
                                        {
                                            if (Cliente[0] == sublista[0])
                                            {
                                                ClienteNombre = Cliente[1];
                                            }
                                        }
                                        id++;
                                        encontrado = true;
                                        toggleColor = !toggleColor;
                                        saldos4 = 0;
                                        Grid.Rows.Add(id, sublista[0], ClienteNombre, sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], "", sublista[9]);
                                        fila3 = Grid.Rows.Count;
                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = toggleColor ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
                                        Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                        saldos4 += decimal.Parse(sublista[7].ToString());
                                        bandera3 = true;
                                        foreach (var Buc in ListaMain)
                                        {
                                            if (Buc[2] == "DZ" && (Buc[11].ToString() == referenciaFact && referenciaFact != string.Empty || Buc[11].ToString() == referenciaFact2 && referenciaFact2 != string.Empty))
                                            {
                                                Grid.Rows.Add(id, Buc[0], ClienteNombre, Buc[1], Buc[2], Buc[3], Buc[4], Buc[5], Buc[6], Buc[7], "", Buc[9]);
                                                Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = toggleColor ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
                                                Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                                saldos4 += decimal.Parse(Buc[7].ToString());
                                                bandera3 = true;
                                            }
                                        }
                                        foreach (var sublista2 in ListaNotas)
                                        {
                                            if (sublista2[1].ToString() == referenciaFact)
                                            {
                                                ReferenciaEncontrada = true;
                                                foreach (var Buscardocto in ListaMain)
                                                {
                                                    if (Buscardocto[3] == sublista2[0])
                                                    {
                                                        Grid.Rows.Add(id, Buscardocto[0], ClienteNombre, Buscardocto[1], Buscardocto[2], Buscardocto[3], Buscardocto[4], Buscardocto[5], Buscardocto[6], Buscardocto[7], "", Buscardocto[9]);
                                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = toggleColor ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
                                                        Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                                        bandera3 = true;
                                                        saldos4 += decimal.Parse(Buscardocto[7].ToString());
                                                        //toggleColor = !toggleColor; // Alternar el color para la siguiente fila
                                                    }
                                                }
                                            }
                                        }

                                        if (ReferenciaEncontrada == false)
                                        {
                                            foreach (var sublista2 in ListaNotas)
                                            {
                                                if (sublista2[1].ToString() == referenciaFact2)
                                                {
                                                    foreach (var Buscardocto in ListaMain)
                                                    {
                                                        if (Buscardocto[3] == sublista2[0])
                                                        {
                                                            Grid.Rows.Add(id, Buscardocto[0], ClienteNombre, Buscardocto[1], Buscardocto[2], Buscardocto[3], Buscardocto[4], Buscardocto[5], Buscardocto[6], Buscardocto[7], "", Buscardocto[9]);
                                                            Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                                            saldos4 += decimal.Parse(Buscardocto[7].ToString());
                                                            bandera3 = true;
                                                            Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = toggleColor ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
                                                            //toggleColor = !toggleColor; // Alternar el color para la siguiente fila
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }


                                }
                                if (bandera3)
                                {
                                    Grid.Rows[fila3 - 1].Cells[10].Value = saldos4.ToString("N");
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
                                    Grid.Columns[10].Visible = false;
                                    foreach (var Cliente in ListaClientes)
                                    {
                                        if (Cliente[0] == sublista[0])
                                        {
                                            ClienteNombre = Cliente[1];
                                        }
                                    }
                                    id++;
                                    Grid.Columns[10].Visible = false;
                                    Grid.Rows.Add(id, sublista[0], ClienteNombre, sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[7].Style.BackColor = System.Drawing.Color.Gold;
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
                                if (sublista[1].ToString() == TxtFolio.Text)
                                {
                                    foreach (var Cliente in ListaClientes)
                                    {
                                        if (Cliente[0] == sublista[0])
                                        {
                                            ClienteNombre = Cliente[1];
                                        }
                                    }
                                    id++;
                                    Grid.Columns[10].Visible = false;
                                    Grid.Rows.Add(id, sublista[0], ClienteNombre, sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
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
                    case "Clase":
                        encontrado = false;
                        if (ChPeriodo.Checked)
                        {
                            foreach (var sublista in ListaMain)
                            {
                                // Verificar si la sublista contiene el valor que buscas
                                if (sublista[2].ToString() == TxtFolio.Text && DateTime.Parse(sublista[5].ToString()) <= DateTime.Parse(DateFin.Text) && DateTime.Parse(sublista[5].ToString()) >= DateTime.Parse(DateInicio.Text))
                                {
                                    Grid.Columns[10].Visible = false;
                                    foreach (var Cliente in ListaClientes)
                                    {
                                        if (Cliente[0] == sublista[0])
                                        {
                                            ClienteNombre = Cliente[1];
                                        }
                                    }
                                    id++;
                                    Grid.Columns[10].Visible = false;
                                    Grid.Rows.Add(id, sublista[0], ClienteNombre, sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[7].Style.BackColor = System.Drawing.Color.Gold;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[4].Style.BackColor = System.Drawing.Color.Gold;
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
                                    foreach (var Cliente in ListaClientes)
                                    {
                                        if (Cliente[0] == sublista[0])
                                        {
                                            ClienteNombre = Cliente[1];
                                        }
                                    }
                                    id++;
                                    Grid.Columns[10].Visible = false;
                                    Grid.Rows.Add(id, sublista[0], ClienteNombre, sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[4].Style.BackColor = System.Drawing.Color.Gold;
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
                                Grid.Columns[10].Visible = false;
                                // Verificar si la sublista contiene el valor que buscas
                                if (sublista[3].ToString() == TxtFolio.Text && DateTime.Parse(sublista[5].ToString()) <= DateTime.Parse(DateFin.Text) && DateTime.Parse(sublista[5].ToString()) >= DateTime.Parse(DateInicio.Text))
                                {
                                    foreach (var Cliente in ListaClientes)
                                    {
                                        if (Cliente[0] == sublista[0])
                                        {
                                            ClienteNombre = Cliente[1];
                                        }
                                    }
                                    id++;
                                    Grid.Columns[10].Visible = false;
                                    Grid.Rows.Add(id, sublista[0], ClienteNombre, sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[5].Style.BackColor = System.Drawing.Color.Gold;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[7].Style.BackColor = System.Drawing.Color.Gold;
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
                                    foreach (var Cliente in ListaClientes)
                                    {
                                        if (Cliente[0] == sublista[0])
                                        {
                                            ClienteNombre = Cliente[1];
                                        }
                                    }
                                    id++;
                                    Grid.Columns[10].Visible = false;
                                    Grid.Rows.Add(id, sublista[0], ClienteNombre, sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    if (sublista[2] == "CI")
                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.CadetBlue;
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[5].Style.BackColor = System.Drawing.Color.Gold;
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
                        Grid.Columns[10].Visible = true;
                        Grid.Columns[10].HeaderText = "Referencia";
                        string referencia = "";
                        LbCantidad.Visible = true;
                        TextoSaldo.Visible = true;
                        referencia = Referencia(TxtFolio.Text);
                        ClienteNombre = string.Empty;
                        foreach (var sublista in ListaMain)
                        {
                            if (sublista[4].ToString() == TxtFolio.Text || (sublista[8].ToString() == referencia && (sublista[2] != "CD" && sublista[2] != "CC" && sublista[2] != "CI")) && referencia != "")
                            {
                                foreach (var Cliente in ListaClientes)
                                {
                                    if (Cliente[0] == sublista[0])
                                    {
                                        ClienteNombre = Cliente[1];
                                    }
                                }
                                id++;
                                Grid.Rows.Add(id, sublista[0], ClienteNombre, sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                saldo += decimal.Parse(sublista[7].ToString());
                                if (sublista[4].ToString() == TxtFolio.Text)
                                {
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[6].Style.BackColor = System.Drawing.Color.Gold;
                                }
                                if (sublista[2] == "CI" || sublista[2] == "CC" || sublista[2] == "AB")
                                {
                                    Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.CadetBlue;
                                    foreach (var notas in ListaNotas)
                                    {
                                        if (notas[1] == sublista[4])
                                        {
                                            foreach (var Buscardocto in ListaMain)
                                            {
                                                if (Buscardocto[3] == notas[0])
                                                {
                                                    Grid.Rows.Add(id, Buscardocto[0], ClienteNombre, Buscardocto[1], Buscardocto[2], Buscardocto[3], Buscardocto[4], Buscardocto[5], Buscardocto[6], Buscardocto[7], Buscardocto[8], Buscardocto[9]);
                                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                                    saldo += decimal.Parse(Buscardocto[7].ToString());
                                                }
                                            }
                                        }
                                    }
                                }
                                //if (sublista[2] == "CD")
                                //    saldo -= decimal.Parse(sublista[7].ToString());
                                //else
                                LbCantidad.Text = saldo.ToString("N");
                                Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                encontrado = true;
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
                        LbCantidad.Visible = false;
                        TextoSaldo.Visible = false;
                        foreach (var sublista in ListaMain)
                        {
                            // Verificar si la sublista contiene el valor que buscas
                            if (sublista[5].ToString() == TxtFolio.Text)
                            {
                                foreach (var Cliente in ListaClientes)
                                {
                                    if (Cliente[0] == sublista[0])
                                    {
                                        ClienteNombre = Cliente[1];
                                    }
                                }
                                id++;
                                Grid.Columns[10].Visible = false;
                                Grid.Rows.Add(id, sublista[0], ClienteNombre, sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                if (sublista[2] == "CI")
                                    Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.CadetBlue;
                                Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                Grid.Rows[Grid.Rows.Count - 1].Cells[7].Style.BackColor = System.Drawing.Color.Gold;
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
                        LbCantidad.Visible = false;
                        TextoSaldo.Visible = false;
                        foreach (var sublista in ListaMain)
                        {
                            // Verificar si la sublista contiene el valor que buscas
                            if (sublista[6].ToString() == TxtFolio.Text)
                            {
                                foreach (var Cliente in ListaClientes)
                                {
                                    if (Cliente[0] == sublista[0])
                                    {
                                        ClienteNombre = Cliente[1];
                                    }
                                }
                                Grid.Columns[10].Visible = false;
                                id++;
                                Grid.Rows.Add(id, sublista[0], ClienteNombre, sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                if (sublista[2] == "CI")
                                    Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.CadetBlue;
                                Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                Grid.Rows[Grid.Rows.Count - 1].Cells[8].Style.BackColor = System.Drawing.Color.Gold;
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
                                    foreach (var Cliente in ListaClientes)
                                    {
                                        if (Cliente[0] == sublista[0])
                                        {
                                            ClienteNombre = Cliente[1];
                                        }
                                    }
                                    id++;
                                    LbCantidad.Visible = false;
                                    TextoSaldo.Visible = false;
                                    Grid.Columns[10].Visible = false;
                                    Grid.Rows.Add(id, sublista[0], ClienteNombre, sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    if (sublista[2] == "CI")
                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.CadetBlue;
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[9].Style.BackColor = System.Drawing.Color.Gold;
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
                                    foreach (var Cliente in ListaClientes)
                                    {
                                        if (Cliente[0] == sublista[0])
                                        {
                                            ClienteNombre = Cliente[1];
                                        }
                                    }
                                    id++;
                                    Grid.Rows.Add(id, sublista[0], ClienteNombre, sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[7].Style.BackColor = System.Drawing.Color.Gold;
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
                                if (sublista[8].ToString() == TxtFolio.Text)
                                {
                                    foreach (var Cliente in ListaClientes)
                                    {
                                        if (Cliente[0] == sublista[0])
                                        {
                                            ClienteNombre = Cliente[1];
                                        }
                                    }
                                    id++;
                                    Grid.Rows.Add(id, sublista[0], ClienteNombre, sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    if (sublista[2] == "CI")
                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.CadetBlue;
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[10].Style.BackColor = System.Drawing.Color.Gold;
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
                                    foreach (var Cliente in ListaClientes)
                                    {
                                        if (Cliente[0] == sublista[0])
                                        {
                                            ClienteNombre = Cliente[1];
                                        }
                                    }
                                    id++;
                                    Grid.Columns[10].Visible = false;
                                    Grid.Rows.Add(id, sublista[0], ClienteNombre, sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[7].Style.BackColor = System.Drawing.Color.Gold;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[11].Style.BackColor = System.Drawing.Color.Gold;
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
                                    foreach (var Cliente in ListaClientes)
                                    {
                                        if (Cliente[0] == sublista[0])
                                        {
                                            ClienteNombre = Cliente[1];
                                        }
                                    }
                                    id++;
                                    Grid.Rows.Add(id, sublista[0], ClienteNombre, sublista[1], sublista[2], sublista[3], sublista[4], sublista[5], sublista[6], sublista[7], sublista[8], sublista[9]);
                                    if (sublista[2] == "CI")
                                        Grid.Rows[Grid.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.CadetBlue;
                                    Grid.Rows[Grid.Rows.Count - 1].Height = 50;
                                    Grid.Rows[Grid.Rows.Count - 1].Cells[11].Style.BackColor = System.Drawing.Color.Gold;
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
            //Grid.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;  // Color más oscuro para las filas alternas
            //Grid.AlternatingRowsDefaultCellStyle.ForeColor = System.Drawing.Color.Black;      // Color del texto de las filas alternas

            //// Configurar las filas no alternas (filas impares)
            //Grid.DefaultCellStyle.BackColor = System.Drawing.Color.White;  // Color de fondo para las filas impares
            //Grid.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;   // Color del texto para las filas impares (opcional)
        }

        private void CbConceptos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbConceptos.Text == "Cliente")
                TxtFolio.PlaceholderText = "Número Cliente";
            if (CbConceptos.Text == "Factura")
                TxtFolio.PlaceholderText = "Folio Factura";
            if (CbConceptos.Text == "Fecha de Documento")
                TxtFolio.PlaceholderText = "DD/MM/YYYY";
            if (CbConceptos.Text == "Fecha de Vencimiento")
                TxtFolio.PlaceholderText = "DD/MM/YYYY";
            if (CbConceptos.Text == "Importe")
                TxtFolio.PlaceholderText = "Importe";
            if (CbConceptos.Text == "Condición de Pago")
                TxtFolio.PlaceholderText = "Condición";
            if (CbConceptos.Text == "Clase")
                TxtFolio.PlaceholderText = "Clase";
            if (CbConceptos.Text == "No Documento")
                TxtFolio.PlaceholderText = "No. Documento";
            if (CbConceptos.Text == "Asignación")
                TxtFolio.PlaceholderText = "Asignación";
            if (CbConceptos.Text == "Relación AB")
                TxtFolio.PlaceholderText = "No. Documento";

            if (CbConceptos.Text == "Clase" || CbConceptos.Text == "Asignación" || CbConceptos.Text == "No Documento" || CbConceptos.Text == "Referencia" || CbConceptos.Text == "Condición de Pago")
            {
                ChPeriodo.Visible = true;
                ChPeriodo.Checked = false;
                RadioAB.Visible = false;
                RadioCI.Visible = false;
                LbCantidad.Visible = false;
                TextoSaldo.Visible = false;
            }
            else if (CbConceptos.Text == "Cliente")
            {
                LbCantidad.Visible = false;
                TextoSaldo.Visible = false;
                RadioAB.Visible = true;
                RadioCI.Visible = true;
                RadioCI.Checked = true;
                ChPeriodo.Visible = true;
                ChPeriodo.Checked = false;
                DateInicio.Visible = false;
                DateFin.Visible = false;
                ChPeriodo.Checked = false;
            }
            else
            {
                RadioAB.Visible = false;
                RadioCI.Visible = false;
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
            if (Grid.Rows.Count > 0)
            {
                using (var Libro = new XLWorkbook())
                {
                    var Hoja = Libro.AddWorksheet("Reporte"); // Crea una nueva hoja de trabajo

                    // Copiar los encabezados de columna
                    for (int j = 0; j < Grid.Columns.Count; j++)
                    {
                        Hoja.Cell(1, j + 1).Value = Grid.Columns[j].HeaderText; // Escribe el nombre de la columna en la primera fila
                    }
                    Hoja.Column(1).Width = 5;
                    Hoja.Column(2).Width = 10;
                    Hoja.Column(3).Width = 40;
                    Hoja.Column(4).Width = 15;
                    Hoja.Column(5).Width = 15;
                    Hoja.Column(6).Width = 15;
                    Hoja.Column(7).Width = 15;
                    Hoja.Column(8).Width = 15;
                    Hoja.Column(9).Width = 15;
                    Hoja.Column(10).Width = 15;
                    Hoja.Column(11).Width = 15;
                    Hoja.Column(12).Width = 15;
                    // Copiar los datos de las filas
                    for (int i = 0; i < Grid.Rows.Count; i++)
                    {
                        for (int j = 0; j < Grid.Columns.Count; j++)
                        {
                            if (!Grid.Rows[i].IsNewRow) // Asegúrate de no exportar la fila vacía
                            {
                                Hoja.Cell(i + 2, j + 1).Value = Grid.Rows[i].Cells[j].Value?.ToString(); // Escribe los valores de las celdas
                            }
                        }
                    }

                    GuardarArchivo.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";
                    GuardarArchivo.FileName = "Exportado.xlsx";

                    if (GuardarArchivo.ShowDialog() == DialogResult.OK)
                    {
                        Libro.SaveAs(GuardarArchivo.FileName); // Guarda el archivo en la ubicación seleccionada
                        MessageBox.Show("Datos exportados exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start(new ProcessStartInfo(GuardarArchivo.FileName) { UseShellExecute = true });
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay datos para exportar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtFolio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Buscar.Focus();

            }
        }

        private void DateFin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Buscar.Focus();

            }
        }

        private void CbConceptos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtFolio.Focus();

            }
        }
    }
}
