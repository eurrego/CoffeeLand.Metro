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
    /// Lógica de interacción para frmLotes.xaml
    /// </summary>
    public partial class frmLotes : MetroWindow
    {
        // variable para controlar que los campos esten llenos
        bool validacion = false;

        public frmLotes()
        {
            InitializeComponent();
        }

        // Define el estilo de las celdas 
        private void EstilosCeldas()
        {
            lblTotal.Content = tblLotes.Items.Count.ToString(); ;
        }

        // mensaje de Error
        private async void mensajeError(string mensaje)
        {
            await this.ShowMessageAsync("Error", mensaje);
        }

        // Validación de campos
        private bool validarCampos()
        {

            if (txtNombre.Text == string.Empty || txtCuadras.Text == 0.ToString() || txtDescripcion.Text == null)
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

        // limpiar Controles lote
        private void LimpiarControlesLote()
        {
            txtNombre.Text = string.Empty;
            txtCuadras.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
        }

        //mostrar
        private void Mostrar()
        {
            tblLotes.ItemsSource = MLote.GetInstance().ConsultarLotes();
            EstilosCeldas();
        }

        //Método para Buscar por nombre
        private void BuscarNombre()
        {
            tblLotes.ItemsSource = MLote.GetInstance().ConsultarParametroLote(txtBuscarNombre.Text);
            EstilosCeldas();
        }

        private void frmLotes1_Loaded(object sender, RoutedEventArgs e)
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
                    rpta = MLote.GetInstance().GestionLote(txtNombre.Text, txtDescripcion.Text, txtCuadras.Text,  0, 1).ToString();
                    this.ShowMessageAsync("Mensaje", rpta);
                    LimpiarControlesLote();
                }
            }
            else if (validarCampos())
            {
                rpta = MLote.GetInstance().GestionLote(txtNombre.Text, txtDescripcion.Text, txtCuadras.Text, Convert.ToInt32(txtId.Text), 2).ToString();
                this.ShowMessageAsync("Mensaje", rpta);
                LimpiarControlesLote();
                lblEstado.Content = "REGISTRAR LOTES";
                btnGuardar.Margin = new Thickness(668, 31, 0, 0);
                btnCancelar.Visibility = Visibility.Collapsed;
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
            Lote item = tblLotes.SelectedItem as Lote;

            txtNombre.Text = item.NombreLote;
            txtDescripcion.Text = item.Observaciones;
            txtCuadras.Text = item.Cuadras;
            txtId.Text = item.idLote.ToString();

            lblEstado.Content = "MODIFICAR LOTES";
            btnGuardar.Margin = new Thickness(556, 31, 0, 0);
            btnCancelar.Visibility = Visibility.Visible;
            gridConsultar.IsEnabled = false;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarControlesLote();
            lblEstado.Content = "REGISTRAR LOTES";
            btnGuardar.Margin = new Thickness(688, 31, 0, 0);
            btnCancelar.Visibility = Visibility.Hidden;
            gridConsultar.IsEnabled = true;
        }

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            Lote item = tblLotes.SelectedItem as Lote;

            string nombre = item.NombreLote;
            string descripcion = item.Observaciones;
            string cuadras = item.Cuadras;
            byte id = Convert.ToByte(item.idLote);

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

                rpta = MLote.GetInstance().GestionLote(nombre, descripcion, cuadras, id, 3).ToString();
                await this.ShowMessageAsync("CoffeeLand", rpta);
                Mostrar();
            }
        }
    }
}
