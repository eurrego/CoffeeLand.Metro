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

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmProveedor.xaml
    /// </summary>
    public partial class frmProveedor : MetroWindow
    {
        bool validacion = false;

        public frmProveedor()
        {
            InitializeComponent();
        }

        // mensaje de Error
        private async void mensajeError(string mensaje)
        {
            await this.ShowMessageAsync("Error", mensaje);
        }

        // Define el estilo de las celdas 
        private void EstilosCeldas()
        {
            lblTotal.Content = tblProveedor.Items.Count.ToString(); ;
        }

        // Validación de campos
        private bool validarCampos()
        {

            if ( cmbTipoDocumento.SelectedIndex == 0 || txtNombre.Text == string.Empty || txtDocumento.Text == string.Empty || txtTelefono.Text == string.Empty || txtDireccion.Text == string.Empty)
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

        // limpiar Controles
        private void Limpiar()
        {

            cmbTipoDocumento.SelectedIndex = 0;
            txtNombre.Text = string.Empty;
            txtDocumento.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtId.Text = string.Empty;
        }

        //mostrar
        private void Mostrar()
        {
            tblProveedor.ItemsSource = MProveedor.GetInstance().ConsultarProveedor();
            cargarCmbTipoDocumento();
            EstilosCeldas();
        }

        //Método para Buscar por nombre
        private void BuscarNombre()
        {
            tblProveedor.ItemsSource = MProveedor.GetInstance().ConsultarParametroProveedor(txtBuscarNombre.Text);
            EstilosCeldas();
        }

        private void frmProveedor1_Loaded(object sender, RoutedEventArgs e)
        {
            Mostrar();
            tabConsultar.Focus();
        }

        private void txtBuscarNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            BuscarNombre();
        }

        private void cargarCmbTipoDocumento()
        {
            List<string> data = new List<string>();
            data.Add("Seleccione un Tipo de Documento...");
            data.Add("CC");
            data.Add("TI");
            data.Add("NIT");
            cmbTipoDocumento.ItemsSource = data;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string rpta = "";

            if (txtId.Text == string.Empty)
            {
                if (validarCampos())
                {
                    rpta = MProveedor.GetInstance().GestionProveedor( txtDocumento.Text, txtNombre.Text,  txtTelefono.Text, txtDireccion.Text,Convert.ToString(cmbTipoDocumento.SelectedItem),1);
                    this.ShowMessageAsync("Mensaje", rpta);
                    Limpiar();
                    tabConsultar.Focus();
                }
            }
            else
            {
                rpta = MProveedor.GetInstance().GestionProveedor(txtId.Text, txtNombre.Text, txtTelefono.Text, txtDireccion.Text, Convert.ToString(cmbTipoDocumento.SelectedItem), 2);
                this.ShowMessageAsync("Mensaje", rpta);
                Limpiar();
                lblEstado.Content = "REGISTRAR PROVEEDORES";
                btnGuardar.Margin = new Thickness(520, 48, 0, 0);
                btnCancelar.Visibility = Visibility.Collapsed;
                tabConsultar.IsEnabled = true;
                txtDocumento.IsEnabled = true;
                cmbTipoDocumento.IsEnabled = true;
                tabConsultar.Focus();
            }
            Mostrar();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Proveedor item = tblProveedor.SelectedItem as Proveedor;

            txtId.Text = item.Nit;
            txtNombre.Text = item.NombreProveedor;
            txtDocumento.Text = item.Nit;
            txtTelefono.Text = item.Telefono;
            txtDireccion.Text = item.Direccion;
            cmbTipoDocumento.SelectedItem = item.TipoDocumento;
           

            lblEstado.Content = "MODIFICAR PROVEEDOR";
            btnGuardar.Margin = new Thickness(402, 48, 0, 0);
            btnCancelar.Visibility = Visibility.Visible;
            tabConsultar.IsEnabled = false;
            txtDocumento.IsEnabled = false;
            cmbTipoDocumento.IsEnabled = false;
            tabRegistrar.Focus();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            lblEstado.Content = "REGISTRAR PROVEEDOR";
            btnGuardar.Margin = new Thickness(520, 26, 0, 0);
            btnCancelar.Visibility = Visibility.Collapsed;
            tabConsultar.IsEnabled = true;
            txtDocumento.IsEnabled = true;
            cmbTipoDocumento.IsEnabled = true;
            tabConsultar.Focus();
        }

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            Proveedor item = tblProveedor.SelectedItem as Proveedor;

            
            string nombre = item.NombreProveedor;
            string id = item.Nit;
            string telefono = item.Telefono;
            string direccion= item.Direccion;
            string tipoDocumento = item.TipoDocumento;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",
                ColorScheme = MetroDialogOptions.ColorScheme
            };

            MessageDialogResult result = await this.ShowMessageAsync("CoffeeLand", "¿Realmente desea Inhabilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result != MessageDialogResult.Negative)
            {
                string rpta = "";

                rpta = MProveedor.GetInstance().GestionProveedor(id, nombre, telefono, direccion, tipoDocumento, 3);
                await this.ShowMessageAsync("CoffeeLand", rpta);
                Mostrar();
            }
        }
    }
}
