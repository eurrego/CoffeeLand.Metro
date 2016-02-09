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

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmVenta.xaml
    /// </summary>
    public partial class frmVenta : MetroWindow
    {
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

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            decimal precioBeneficio = decimal.Parse(txtValorBeneficio.Text) * decimal.Parse(txtCantidadCarga.Text);
            MVentas.GetInstance().GestionVenta(int.Parse(cmbProveedor.SelectedValue.ToString()), Convert.ToDateTime(dtdFecha.SelectedDate),int.Parse(txtNumeroFactura.Text), int.Parse(cmbProducto.SelectedValue.ToString()),decimal.Parse(txtValorCarga.Text),decimal.Parse(txtCantidadCarga.Text),precioBeneficio);
        }
    }
}
