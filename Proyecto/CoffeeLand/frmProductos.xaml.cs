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
using System.Windows.Controls.Primitives;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmProductos.xaml
    /// </summary>
    public partial class frmProductos : MetroWindow
    {
        bool validacion;

        public frmProductos()
        {
            InitializeComponent();
        }

        // mensaje de Error
        private void mensajeError(string mensaje)
        {
            this.ShowMessageAsync("Error", mensaje);
        }

        // Define el estilo de las celdas 
        private void EstilosCeldas()
        {
            lblTotal.Content = tblProductos.Items.Count.ToString(); ;
        }

        // Validación de campos
        private bool validarCampos()
        {

            if (txtNombre.Text == string.Empty || txtDescripcion.Text == string.Empty)
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
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtId.Text = string.Empty;
        }

        //mostrar
        private void Mostrar()
        {
            tblProductos.ItemsSource = MProducto.GetInstance().ConsultarProducto();
            EstilosCeldas();
        }


        //Método para Buscar por nombre
        private void BuscarNombre()
        {
            tblProductos.ItemsSource = MProducto.GetInstance().ConsultarParametroProducto(txtBuscarNombre.Text);
            EstilosCeldas();
        }

        private void frmProducto_Loaded(object sender, RoutedEventArgs e)
        {
            Mostrar();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string rpta = "";

            if (txtId.Text == string.Empty)
            {
                if (validarCampos())
                {
                    rpta = MProducto.GetInstance().GestionProducto(txtNombre.Text, txtDescripcion.Text, 0, 1).ToString();
                    this.ShowMessageAsync("Mensaje", rpta);
                    Limpiar();
                }
            }
            else if (validarCampos())
            {
                rpta = MProducto.GetInstance().GestionProducto(txtNombre.Text, txtDescripcion.Text, Convert.ToByte(txtId.Text), 2).ToString();
                this.ShowMessageAsync("Mensaje", rpta);
                Limpiar();
                lblEstado.Content = "REGISTRAR PRODUCTOS";
                btnGuardar.Margin = new Thickness(667, 36, 0, 0);
                btnCancelar.Visibility = Visibility.Hidden;
                gridConsultar.IsEnabled = true;
            }
            Mostrar();
        }

        private void txtBuscarNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            BuscarNombre();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Producto item = tblProductos.SelectedItem as Producto;

            txtId.Text = item.idProducto.ToString();
            txtNombre.Text = item.NombreProducto;
            txtDescripcion.Text = item.Descripcion;

            lblEstado.Content = "MODIFICAR PRODUCTOS";
            btnGuardar.Margin = new Thickness(556, 36, 0, 0);
            btnCancelar.Visibility = Visibility.Visible;
            gridConsultar.IsEnabled = false;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            lblEstado.Content = "REGISTRAR PRODUCTOS";
            btnGuardar.Margin = new Thickness(667, 36, 0, 0);
            btnCancelar.Visibility = Visibility.Hidden;
            gridConsultar.IsEnabled = true;
        }

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            Producto item = tblProductos.SelectedItem as Producto;

            byte id = item.idProducto;
            string nombre = item.NombreProducto;
            string descripcion = item.Descripcion;

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

                rpta = MProducto.GetInstance().GestionProducto(nombre, descripcion, id, 3).ToString();
                await this.ShowMessageAsync("CoffeeLand", rpta);
                Mostrar();
            }
        }
    }
}
