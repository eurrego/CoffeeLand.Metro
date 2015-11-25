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
    /// Lógica de interacción para frmTerrenos.xaml
    /// </summary>
    public partial class frmTerrenos : MetroWindow
    {

        public static DataTable dt;
       
        bool validacion = false;

        public frmTerrenos()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            cmbLotes.ItemsSource = MTerrenos.GetInstance().ConsultarLote();
            cmbLabores.ItemsSource = MTerrenos.GetInstance().ConsultarLabor();
            cmbInsumo.ItemsSource = MTerrenos.GetInstance().ConsultarInsumo();
            llenarCmbTipoPago();
        }


        private void llenarCmbTipoPago()
        {
            List<string> data = new List<string>();
            data.Add("Seleccione un tipo de Pago");
            data.Add("Contrato");
            data.Add("Jornal");

            cmbTipoPago.ItemsSource = data;
        }

        public void crearTabla()
        {
            dt = new DataTable();
            dt.Columns.Add("Insumo");
            dt.Columns.Add("Cantidad");

        }

        private void cmbLabores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //txtTipoPagoLabor = cmbLabores.("TipoPagoLabor");

        }
        private async void mensajeError(string mensaje)
        {
            await this.ShowMessageAsync("Error", mensaje);
        }

        public void limpiarCampos()
        {
            cmbInsumo.SelectedIndex = 0;
            cmbTipoPago.SelectedIndex = 0;
            txtCantidadInsumo.Text = string.Empty;
            
        }

        private bool validarCampos()
        {

            if (cmbTipoPago.SelectedIndex == 0 || cmbInsumo.Text == string.Empty || dtdFechaLabor.Text == string.Empty)
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

        private void btnAgregarInsumos_Click(object sender, RoutedEventArgs e)
        {
            if (validarCampos())
            {

                dt.Rows.Add(cmbInsumo.Text, cmbTipoPago.Text, txtCantidadInsumo.Text);

                tblInsumos.ItemsSource = dt.DefaultView;
            }
            limpiarCampos();
        }
    }
}

