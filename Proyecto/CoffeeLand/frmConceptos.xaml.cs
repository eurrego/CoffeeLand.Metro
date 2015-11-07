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
    /// Lógica de interacción para frmConceptos.xaml
    /// </summary>
    public partial class frmConceptos : MetroWindow
    {
        bool validacion;

        public frmConceptos()
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
            lblTotal.Content = tblConceptos.Items.Count.ToString(); ;
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
            tblConceptos.ItemsSource = MConcepto.GetInstance().ConsultarConcepto();
            EstilosCeldas();
        }


        //Método para Buscar por nombre
        private void BuscarNombre()
        {
            tblConceptos.ItemsSource = MConcepto.GetInstance().ConsultarParametroConcepto(txtBuscarNombre.Text);
            EstilosCeldas();
        }

        private void frmConcepto_Loaded(object sender, RoutedEventArgs e)
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
                    rpta = MConcepto.GetInstance().GestionConcepto(txtNombre.Text, txtDescripcion.Text, 0, 1).ToString();
                    this.ShowMessageAsync("Mensaje", rpta);
                    Limpiar();
                }
            }
            else if (validarCampos())
            {
                rpta = MConcepto.GetInstance().GestionConcepto(txtNombre.Text, txtDescripcion.Text, Convert.ToByte(txtId.Text), 2).ToString();
                this.ShowMessageAsync("Mensaje", rpta);
                Limpiar();
                lblEstado.Content = "REGISTRAR CONCEPTOS";
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
            Concepto item = tblConceptos.SelectedItem as Concepto;

            txtNombre.Text = item.NombreConcepto;
            txtDescripcion.Text = item.Descripcion;
            txtId.Text = item.idConcepto.ToString();

            lblEstado.Content = "MODIFICAR CONCEPTOS";
            btnGuardar.Margin = new Thickness(556, 36, 0, 0);
            btnCancelar.Visibility = Visibility.Visible;
            gridConsultar.IsEnabled = false;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            lblEstado.Content = "REGISTRAR CONCEPTOS";
            btnGuardar.Margin = new Thickness(667, 36, 0, 0);
            btnCancelar.Visibility = Visibility.Hidden;
            gridConsultar.IsEnabled = true;
        }

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            Concepto item = tblConceptos.SelectedItem as Concepto;

            string nombre = item.NombreConcepto;
            string descripcion = item.Descripcion;
            byte id = Convert.ToByte( item.idConcepto);

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

                rpta = MConcepto.GetInstance().GestionConcepto(nombre, descripcion, id, 3).ToString();
                await this.ShowMessageAsync("CoffeeLand", rpta);
                Mostrar();
            }
        }
    }
}
