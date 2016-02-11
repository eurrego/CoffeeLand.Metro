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
using Modelo;
using MahApps.Metro.Controls.Dialogs;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmVenta.xaml
    /// </summary>
    public partial class frmVenta : MetroWindow
    {
        bool validacion = false;

        public frmVenta()
        {
            InitializeComponent();
        }

        private void frmVenta1_Loaded(object sender, RoutedEventArgs e)
        {
            cmbProveedor.ItemsSource = MVentas.GetInstance().ConsultarProveedor();
            cmbProducto.ItemsSource = MVentas.GetInstance().ConsultarProducto();

            lblcargas.Text = MVentas.GetInstance().ConsultarProduccion().ToString();

        }

        private async void mensajeError(string mensaje)
        {
            await this.ShowMessageAsync("Error", mensaje);
        }

        private async void mensajeExito(string mensaje)
        {
            await this.ShowMessageAsync("", mensaje);
        }


        public void limpiarCampos()
        {
            cmbProducto.SelectedIndex = 0;
            cmbProveedor.SelectedIndex = 0;
            txtCantidadCarga.Text = string.Empty;
            txtNumeroFactura.Text = string.Empty;
            txtValorBeneficio.Text = string.Empty;
            txtValorCarga.Text = string.Empty;
        }

        public bool validarCampos()
        {
            if (cmbProducto.SelectedIndex == 0 || cmbProveedor.SelectedIndex == 0 || txtValorCarga.Text == string.Empty || txtValorBeneficio.Text == string.Empty || txtNumeroFactura.Text == string.Empty || txtCantidadCarga.Text == string.Empty)
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


        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (validarCampos())
            {
                if (decimal.Parse(txtCantidadCarga.Text.ToString()) > MVentas.GetInstance().ConsultarProduccion())
                {
                    mensajeError("La cantidad de cargas es superior a la existencia");
                }
                else
                {
                    decimal precioBeneficio = decimal.Parse(txtValorBeneficio.Text) * decimal.Parse(txtCantidadCarga.Text);
                    MVentas.GetInstance().GestionVenta(int.Parse(cmbProveedor.SelectedValue.ToString()), Convert.ToDateTime(dtdFecha.SelectedDate), int.Parse(txtNumeroFactura.Text), int.Parse(cmbProducto.SelectedValue.ToString()), decimal.Parse(txtValorCarga.Text), decimal.Parse(txtCantidadCarga.Text), precioBeneficio);
                    mensajeExito( MVentas.GetInstance().ventaProduccion(decimal.Parse(txtCantidadCarga.Text)));
                    limpiarCampos();
                    lblcargas.Text = MVentas.GetInstance().ConsultarProduccion().ToString();

                }
            }
        }
    }
}
