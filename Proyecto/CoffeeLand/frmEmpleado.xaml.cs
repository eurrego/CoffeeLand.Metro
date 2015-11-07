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
using System.Collections;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmEmpleado.xaml
    /// </summary>
    public partial class frmEmpleado : MetroWindow
    {

        bool validacion = false;

        public frmEmpleado()
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
            lblTotal.Content = tblEmpleado.Items.Count.ToString(); ;
        }

        // Validación de campos
        private bool validarCampos()
        {

            if ( cmbTipoDocumento.SelectedIndex == 0 || cmbTipoContrato.SelectedIndex == 0 || cmbGenero.SelectedIndex == 0 || txtNombre.Text == string.Empty || txtDocumento.Text == string.Empty || txtTelefono.Text == string.Empty || dtdFechaNacimiento.SelectedDate == null)
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
            cmbGenero.SelectedIndex = 0;
            cmbTipoContrato.SelectedIndex = 0;
            cmbTipoDocumento.SelectedIndex = 0;
            dtdFechaNacimiento.SelectedDate = null;
            txtNombre.Text = string.Empty;
            txtDocumento.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtId.Text = string.Empty;
        }

        //mostrar
        private void Mostrar()
        {
            tblEmpleado.ItemsSource = MPersona.GetInstance().ConsultarPersona();
            cmbTipoDocumento.ItemsSource = MPersona.GetInstance().ConsultarTipoDocumento();
            cmbTipoContrato.ItemsSource = MPersona.GetInstance().ConsultarTipoContrato();
            EstilosCeldas();
        }

        //Método para Buscar por nombre
        private void BuscarNombre()
        {
            tblEmpleado.ItemsSource = MPersona.GetInstance().ConsultarParametroPersona(txtBuscarNombre.Text);
            EstilosCeldas();
        }

        private void frmEmpleado1_Loaded(object sender, RoutedEventArgs e)
        {
            Mostrar();
            tabConsultar.Focus();
        }

        private void txtBuscarNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            BuscarNombre();
        }

        private void cmbGenero_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("Seleccione un Género...");
            data.Add("M");
            data.Add("F");
            cmbGenero.ItemsSource = data;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string rpta = "";

            if (txtId.Text == string.Empty)
            {
                if (validarCampos())
                {
                    rpta = MPersona.GetInstance().GestionPersona(txtNombre.Text, Convert.ToString(cmbGenero.SelectedItem), txtTelefono.Text, Convert.ToDateTime(dtdFechaNacimiento.SelectedDate), Convert.ToInt32(txtDocumento.Text), 1, Convert.ToByte(cmbTipoDocumento.SelectedValue), Convert.ToByte(cmbTipoContrato.SelectedValue));
                    this.ShowMessageAsync("Mensaje", rpta);
                    Limpiar();
                    tabConsultar.Focus();
                }
            }
            else
            {
                rpta = MPersona.GetInstance().GestionPersona(txtNombre.Text, Convert.ToString(cmbGenero.SelectedItem), txtTelefono.Text, Convert.ToDateTime(dtdFechaNacimiento.SelectedDate), Convert.ToInt32(txtId.Text), 2, Convert.ToByte(cmbTipoDocumento.SelectedValue), Convert.ToByte(cmbTipoContrato.SelectedValue));
                this.ShowMessageAsync("Mensaje", rpta);
                Limpiar();
                lblEstado.Content = "REGISTRAR EMPLEADOS";
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
            Persona item = tblEmpleado.SelectedItem as Persona;

            txtId.Text = item.DocumentoPersona;
            txtNombre.Text = item.NombrePersona;
            txtDocumento.Text = item.DocumentoPersona;
            txtTelefono.Text = item.Telefono;
            cmbGenero.SelectedItem = item.Genero;
            cmbTipoContrato.SelectedValue = item.idTipoContratoPersona;
            cmbTipoDocumento.SelectedValue = item.idTipoDocumento;
            dtdFechaNacimiento.SelectedDate = item.FechaNacimineto;

            lblEstado.Content = "MODIFICAR EMPLEADOS";
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
            lblEstado.Content = "REGISTRAR EMPLEADOS";
            btnGuardar.Margin = new Thickness(520, 26, 0, 0);
            btnCancelar.Visibility = Visibility.Collapsed;
            tabConsultar.IsEnabled = true;
            tabConsultar.Focus();
        }

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            Persona item = tblEmpleado.SelectedItem as Persona;

            int id = Convert.ToInt32(item.DocumentoPersona);
            string nombre = item.NombrePersona;
            string telefono = item.Telefono;
            string genero = item.Genero;
            byte tipoContrato = item.idTipoContratoPersona;
            byte tipoDocumento = item.idTipoDocumento;
            DateTime fecha = item.FechaNacimineto;

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

                rpta = MPersona.GetInstance().GestionPersona(nombre, genero, telefono, fecha, id, 3, tipoDocumento, tipoContrato).ToString();
                await this.ShowMessageAsync("CoffeeLand", rpta);
                Mostrar();
            }
        }
    }
}
