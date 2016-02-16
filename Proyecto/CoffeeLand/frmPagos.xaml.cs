using System;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

using Modelo;
using System.Collections.Generic;

using System.Data;
using System.ComponentModel;
using System.Collections;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmPagos.xaml
    /// </summary>
    public partial class frmPagos : MetroWindow
    {


        DataTable auxiliar = new DataTable();
        public frmPagos()
        {
            InitializeComponent();
        }

        private void btnDetallePago_Click(object sender, RoutedEventArgs e)
        {
            if (cmbTipoEmpleado.SelectedIndex.Equals(2))
            {
                tabDetallePago.Visibility = Visibility.Visible;
                tabDetallePago.Focus();

                tabDetallePago.Visibility = Visibility.Collapsed;

                PagosPersona_Result1 item = tblPagos.SelectedItem as PagosPersona_Result1;
                tblDetallePago.ItemsSource = MPagos.GetInstance().DetalleSalario(int.Parse(item.DocumentoPersona)) as IEnumerable;



                lblNombreEmpleado.Text = item.Nombre.ToString();
                lblCedulaEmpleado.Text = item.DocumentoPersona.ToString();
            }
            else if (cmbTipoEmpleado.SelectedIndex.Equals(1))
            {
                FlyoutPagos.IsOpen = true;
                txtPago.Text = string.Empty;
            }


        }

        private void tblPagos_Loaded(object sender, RoutedEventArgs e)
        {


        }

        private void frmPagos_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void cmbTipoEmpleado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTipoEmpleado.SelectedIndex == 0)
            {
                tblPagos.ItemsSource=null;
                tblPagos.Items.Refresh();
                //btnAbonos.IsEnabled = false;
                //btnGuardar.IsEnabled = false;
            }
            else if (cmbTipoEmpleado.SelectedIndex == 1)//consultar persona permanente
            {
                auxiliar.Reset();
                auxiliar.Columns.Add("DocumentoPersona");
                auxiliar.Columns.Add("Nombre");
                auxiliar.Columns.Add("Valor_a_pagar");
                auxiliar.Columns.Add("Valor_Deuda");


                tblPagos.ItemsSource = MPagos.GetInstance().ConsultarSalario(2) as IEnumerable;


            }
            else if (cmbTipoEmpleado.SelectedIndex == 2)//consultar persona temporal
            {

                tblPagos.ItemsSource = MPagos.GetInstance().ConsultarSalario(1) as IEnumerable;


            }
        }

        public static DataTable DataGridtoDataTable(DataGrid dg)
        {


            dg.SelectAllCells();
            dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dg);
            dg.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            string[] Lines = result.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string[] Fields;
            Fields = Lines[0].Split(new char[] { ',' });
            int Cols = Fields.GetLength(0);

            DataTable dt = new DataTable();
            for (int i = 0; i < Cols; i++)
                dt.Columns.Add(Fields[i].ToUpper(), typeof(string));
            DataRow Row;
            for (int i = 1; i < Lines.GetLength(0) - 1; i++)
            {
                Fields = Lines[i].Split(new char[] { ',' });
                Row = dt.NewRow();
                for (int f = 0; f < Cols; f++)
                {
                    Row[f] = Fields[f];
                }
                dt.Rows.Add(Row);
            }
            return dt;

        }

        private void btnConfirmarPago_Click(object sender, RoutedEventArgs e)
        {
            PagosPersona_Result1 item = tblPagos.SelectedItem as PagosPersona_Result1;

            item.Valor_a_pagar = decimal.Parse(txtPago.Text);
            tblPagos.Items.Refresh();

           

            FlyoutPagos.IsOpen = false;


        }

  
        private async void mensajeError(string mensaje)
        {
            await this.ShowMessageAsync("", mensaje);
        }

        private void btnPagar_Click(object sender, RoutedEventArgs e)
        {
            auxiliar = DataGridtoDataTable(tblPagos);
            string val = "0";

            if (cmbTipoEmpleado.SelectedIndex.Equals(1))
            {

                for (int i = 0; i < auxiliar.Rows.Count; i++)
                {
                    val = auxiliar.Rows[i].ItemArray[2].ToString();
                    if (val == "0")
                    {
                        mensajeError("Error Debe asignar un pago!");

                        break;
                    }

                }

                if (val != "0")
                {

                    auxiliar.Columns.Remove("NOMBRE");
                    auxiliar.Columns.Remove("VALOR DEUDA");
                    auxiliar.Columns.Remove("DETALLE PAGO");
                    MPagos.GetInstance().insertarMultiplesSalarios(auxiliar,1);

                    tblPagos.ItemsSource = null;
                    tblPagos.Items.Refresh();
                    cmbTipoEmpleado.SelectedIndex = 0;

                    mensajeError("Registro de pago exitoso.");
                }
            }
            else if (cmbTipoEmpleado.SelectedIndex.Equals(2))
            {
                auxiliar.Columns.Remove("NOMBRE");
                auxiliar.Columns.Remove("VALOR A pAGAR");
                auxiliar.Columns.Remove("VALOR DEUDA");
                auxiliar.Columns.Remove("DETALLE PAGO");
                MPagos.GetInstance().insertarMultiplesSalarios(auxiliar,2);

                tblPagos.ItemsSource = null;
                tblPagos.Items.Refresh();
                cmbTipoEmpleado.SelectedIndex = 0;

                mensajeError("Registro de pago exitoso.");
            }

            }
    }
}
