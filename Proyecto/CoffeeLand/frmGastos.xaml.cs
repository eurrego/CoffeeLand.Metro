using System;
using System.Collections.Generic;
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
using System.Data;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmGastos.xaml
    /// </summary>
    public partial class frmGastos : MetroWindow
    {
        public static DataTable dt ;
        int index = -1;

        // variable para controlar que los campos esten llenos
        bool validacion = false;

        public frmGastos()
        {
            InitializeComponent();
           
        }

        public void crearTabla()
        {
            dt = new DataTable();
            dt.Columns.Add("idConcepto");
            dt.Columns.Add("Concepto");
            dt.Columns.Add("Descripcion");
            dt.Columns.Add("Fecha", typeof(DateTime));
            dt.Columns.Add("Valor");
            dt.Columns.Add("Pago");
            dt.Columns.Add("EstadoCuenta");
        }

        // mensaje de Error
        private async void mensajeError(string mensaje)
        {
            await this.ShowMessageAsync("Error", mensaje);
        }

      

        // Validación de campos
        private bool validarCampos()
        {

            if (cmbTipoPago.SelectedIndex == 0 || txtDescripcionGasto.Text == string.Empty || txtValor.Text == string.Empty || cmbConcepto.SelectedIndex == 0)
            {
                mensajeError("Debe Ingresar todos los Campos");
                validacion = false;
            }
            else
            {
                validacion = true;
            }

            return validacion;
        }

        private void llenarCmbTipoPago()
        {
            List<string> data = new List<string>();
            data.Add("Seleccione un tipo de Pago");
            data.Add("Efectivo");
            data.Add("Crédito");

            cmbTipoPago.ItemsSource = data;
        }

        private void groupArboles_Loaded(object sender, RoutedEventArgs e)
        {
            cmbConcepto.ItemsSource = MGastos.GetInstance().ConsultarConcepto();
            llenarCmbTipoPago();


        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Con este Procedimiento se envia una tabla completa a la 
                //base de datos, enviandole un DataTable como parametro 
                var db = new DBFincaEntities();
                dt.Columns.Remove("Concepto");
                dt.Columns.Remove("Pago");
                db.SP_InsertMultiplesGastos(dt);
                dt.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }




        private void btnAgregarGasto_Click(object sender, RoutedEventArgs e)
        {
            if (validarCampos())
            {

                crearTabla();

                if (cmbTipoPago.Text.Equals("Efectivo"))
                {
                    dt.Rows.Add(cmbConcepto.SelectedValue, cmbConcepto.Text, txtDescripcionGasto.Text, Convert.ToDateTime(dtdFechaGasto.SelectedDate), txtValor.Text, cmbTipoPago.Text, "P");

                }
                else
                {
                    dt.Rows.Add(cmbConcepto.SelectedValue, cmbConcepto.Text, txtDescripcionGasto.Text, Convert.ToDateTime(dtdFechaGasto.SelectedDate), txtValor.Text, cmbTipoPago.Text, "D");

                }

                tblGastos.ItemsSource = dt.DefaultView;
            }
            limpiarCampos();
        }

    

        private void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            index = tblGastos.SelectedIndex;
            dt.Rows[index].Delete();
            index = -1;
        }

        public void limpiarCampos()
        {
            cmbConcepto.SelectedIndex = 0;
            txtValor.Text = string.Empty;
            cmbTipoPago.SelectedIndex = 0;
            dtdFechaGasto.Text = string.Empty;
            txtDescripcionGasto.Text = string.Empty;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            index = tblGastos.SelectedIndex;
            cmbConcepto.SelectedValue = dt.Rows[index].ItemArray[0];
            txtValor.Text = dt.Rows[index].ItemArray[4].ToString();
            cmbTipoPago.SelectedValue = dt.Rows[index].ItemArray[5];
            dtdFechaGasto.SelectedDate= Convert.ToDateTime(dt.Rows[index].ItemArray[3]);
            txtDescripcionGasto.Text= dt.Rows[index].ItemArray[2].ToString();
           
            dt.Clear();
        }

        private void btnCrearConcepto_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            frmConceptos frm = new frmConceptos();
            frm.ShowDialog();
           
        }
    }
}
