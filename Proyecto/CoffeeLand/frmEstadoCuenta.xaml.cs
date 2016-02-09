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
using System.Collections;
using System.ComponentModel;
using System.Data;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmEstadoCuenta.xaml
    /// </summary>
    public partial class frmEstadoCuenta : MetroWindow
    {

        int total;
        int idCompra;
        public frmEstadoCuenta()
        {
            InitializeComponent();
        }

        private void frmEstadoCuenta1_Loaded(object sender, RoutedEventArgs e)
        {
            cmbProveedor.ItemsSource = MEstadoCuenta.GetInstance().ConsultarProveedor();
        }



        private void cmbProveedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbProveedor.SelectedIndex == 0)
            {
                lblProveedor.Text = "Seleccione un proveedor";

                tblCompras.ItemsSource = null;
                lblTotal.Text = "0";


            }
            else
            {
                Proveedor item = cmbProveedor.SelectedItem as Proveedor;
                lblProveedor.Text = item.NombreProveedor;

                var data = MEstadoCuenta.GetInstance().ConsultaCompraProveedor(item.Nit) as IEnumerable;
                tblCompras.ItemsSource = MEstadoCuenta.GetInstance().ConsultaCompraProveedor(item.Nit) as IEnumerable; ;

                TotalAdeuda(data);



            }
        }

        private void btnDetalleCompra_Click(object sender, RoutedEventArgs e)
        {




            ComprasProveedor_Result1 item = tblCompras.SelectedItem as ComprasProveedor_Result1;


            lblFacturaDetalleCompra.Text = item.NumeroFactura.ToString();
            lblFechaDetalleCompra.Text = item.Fecha.ToString();

            lblProveedorDetalleCompra.Text = cmbProveedor.Text.ToString();

            string total = item.total.ToString();
            lblTotalDetalleCompra.Text = quitarDecimales(total);
            tblDetalleCompra.ItemsSource = MEstadoCuenta.GetInstance().ConsultaDetalleCompra(item.idCompra) as IEnumerable;
            tabDetalleCompra.Visibility = Visibility.Visible;
            tabDetalleCompra.Focus();
            tabDetalleCompra.Visibility = Visibility.Collapsed;


        }

        private void btnAbonar_Click(object sender, RoutedEventArgs e)
        {
            FlyoutAbonos.IsOpen = true;
            txtAbono.Text = string.Empty;

            ComprasProveedor_Result1 item = tblCompras.SelectedItem as ComprasProveedor_Result1;

            idCompra = int.Parse(item.idCompra.ToString());

            total = int.Parse(quitarDecimales(item.adeuda.ToString()));


        }



        private async void mensajeError(string mensaje)
        {
            await this.ShowMessageAsync("Error", mensaje);
        }

        private async void mensaje(string mensaje)
        {
            await this.ShowMessageAsync(mensaje, "");
        }

        private void btnConfirmarAbono_Click(object sender, RoutedEventArgs e)
        {

            if (txtAbono.Text != string.Empty)
            {

                if (int.Parse(txtAbono.Text) <= total)
                {

                    string rpta = MEstadoCuenta.GetInstance().RegistroAbono(idCompra, decimal.Parse(txtAbono.Text), Convert.ToDateTime(dtdFechaAbono.SelectedDate), total);

                    Proveedor item = cmbProveedor.SelectedItem as Proveedor;


                    var data = MEstadoCuenta.GetInstance().ConsultaCompraProveedor(item.Nit) as IEnumerable;
                    tblCompras.ItemsSource = data;

                    FlyoutAbonos.IsOpen = false;
                    mensaje(rpta);

                    TotalAdeuda(data);

                }

                else
                {
                    mensajeError("Debe ingresar un valor que no sea superior a lo que se adeuda");
                }

            }
            else
            {
                mensajeError("Debe ingresar un valor abonar");
            }

        }

        public string quitarDecimales(string valor)
        {

            int dondeestalacoma = valor.IndexOf(',');

            string valorRecortado = valor.Substring(0, dondeestalacoma);

            return valorRecortado;

        }

        public void TotalAdeuda(IEnumerable data)
        {
            int total = 0;
            foreach (ComprasProveedor_Result1 valor in data)
            {
                total += int.Parse(quitarDecimales(valor.adeuda.ToString()));
            }
            lblTotal.Text = total.ToString();
        }

    }
}
