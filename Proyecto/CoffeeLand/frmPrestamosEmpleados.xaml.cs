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
    /// Lógica de interacción para frmPrestamosEmpleados.xaml
    /// </summary>
    public partial class frmPrestamosEmpleados : MetroWindow
    {
        // variable para controlar que los campos esten llenos
        bool validacion = false;

        public frmPrestamosEmpleados()
        {
            InitializeComponent();
        }

        // mensaje de Error
        private async void mensajeError(string mensaje)
        {
            await this.ShowMessageAsync("Error", mensaje);
        }

        // limpiar Controles
        private void Limpiar()
        {
            dtdFecha.SelectedDate = null;
            txtValor.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtAbono.Text = string.Empty;
            tblPrestamos.ItemsSource = null;
            lblTotal.Text = string.Empty;
        }

        // define la deuda total
        private void deudaTotal()
        {
            decimal total = 0;

            foreach (DeudaPersona item in tblPrestamos.ItemsSource)
            {
               total += item.Valor;
            }
            
            lblTotal.Text = string.Format("{0:c}", total);
        }

        //mostrar
        private void Mostrar()
        {
            if (cmbEmpleado.IsVisible)
            {
                tblPrestamos.ItemsSource = MPrestamosEmpleados.GetInstance().ConsultarDeudaEmpleado(cmbEmpleado.SelectedValue.ToString());
            }
            else
            {
                tblPrestamos.ItemsSource = MPrestamosEmpleados.GetInstance().ConsultarDeudaEmpleado(txtBuscarDocumento.Text);
            }

            deudaTotal();
        }

        private void btnBusqueda_Click(object sender, RoutedEventArgs e)
        {
            if (btnBusqueda.IsChecked == true)
            {
                lblBusqueda.Content = "Documento";
                txtBuscarDocumento.Visibility = Visibility.Visible;
                btnBuscarDocumento.Visibility = Visibility.Visible;
                cmbEmpleado.Visibility = Visibility.Collapsed;
                cmbEmpleado.SelectedIndex = 0;
                txtBuscarDocumento.Text = string.Empty;
                lblEmpleado.Text = "Seleccione un Empleado";
                groupRegistrar.IsEnabled = false;
                groupDeudas.IsEnabled = false;
                Limpiar();
            }
            else
            {
                lblBusqueda.Content = "Nombre";
                txtBuscarDocumento.Visibility = Visibility.Collapsed;
                btnBuscarDocumento.Visibility = Visibility.Collapsed;
                cmbEmpleado.Visibility = Visibility.Visible;
                cmbEmpleado.SelectedIndex = 0;
                txtBuscarDocumento.Text = string.Empty;
                lblEmpleado.Text = "Seleccione un Empleado";
                groupRegistrar.IsEnabled = false;
                groupDeudas.IsEnabled = false;
                Limpiar();
            }
        }

        private void frmPrestamosEmpleados1_Loaded(object sender, RoutedEventArgs e)
        {
            cmbEmpleado.ItemsSource = MPrestamosEmpleados.GetInstance().ConsultarEmpleado();
            groupRegistrar.IsEnabled = false;
            groupDeudas.IsEnabled = false;
        }

        private void cmbEmpleado_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cmbEmpleado.SelectedIndex == 0)
            {
                lblEmpleado.Text = "Seleccione un Empleado";
                groupRegistrar.IsEnabled = false;
                groupDeudas.IsEnabled = false;
            }
            else
            {
                Persona item = cmbEmpleado.SelectedItem as Persona;
                lblEmpleado.Text = item.NombrePersona;
                Mostrar();
                groupRegistrar.IsEnabled = true;
                groupDeudas.IsEnabled = true;

                DateTime fechaReciente = MPrestamosEmpleados.GetInstance().ConsultarFecha();
                lblUltimafecha.Text = string.Format("{0:D}", fechaReciente);
            }
        }

        private void btnBuscarDocumento_Click(object sender, RoutedEventArgs e)
        {

            if (txtBuscarDocumento.Text == string.Empty)
            {
                mensajeError("Debe ingresar un Documento");
            }
            else
            {
                DataGrid list = new DataGrid();
                list.ItemsSource = MPrestamosEmpleados.GetInstance().ConsultarParametroPersona(txtBuscarDocumento.Text);
                list.SelectedIndex = 0;

                if (list.Items.Count == 1)
                {
                    mensajeError("No existe ningún Empleado con este Documento ");
                    list.ItemsSource = null;
                    groupRegistrar.IsEnabled = false;
                    groupDeudas.IsEnabled = false;
                }
                else
                {
                    Persona item = list.SelectedItem as Persona;
                    lblEmpleado.Text = item.NombrePersona;
                    list.ItemsSource = null;
                    groupRegistrar.IsEnabled = true;
                    groupDeudas.IsEnabled = true;
                    Mostrar();

                    DateTime fechaReciente = MPrestamosEmpleados.GetInstance().ConsultarFecha();
                    lblUltimafecha.Text = string.Format("{0:D}", fechaReciente);

                }
            }
            
        }

        private void expTipoBusqueda_MouseLeave(object sender, MouseEventArgs e)
        {
            expTipoBusqueda.IsExpanded = false;
        }

        private void btnAbonos_Click(object sender, RoutedEventArgs e)
        {
            FlyoutAbonos.IsOpen = true;
            txtAbono.Text = string.Empty;
        }

        private bool validarCampos()
        {

            if (txtValor.Text == string.Empty || txtDescripcion.Text == string.Empty || dtdFecha.SelectedDate == null)
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

            string rpta = "";

                if (validarCampos())
                {
                    if (cmbEmpleado.IsVisible)
                    {
                        rpta = MPrestamosEmpleados.GetInstance().insercionDeudaEmpleado(cmbEmpleado.SelectedValue.ToString(),Convert.ToDecimal(txtValor.Text),Convert.ToDateTime(dtdFecha.SelectedDate),txtDescripcion.Text).ToString();
                        this.ShowMessageAsync("Mensaje", rpta);
                        Limpiar();
                    }
                    else
                    {
                        rpta = MPrestamosEmpleados.GetInstance().insercionDeudaEmpleado(txtBuscarDocumento.Text, Convert.ToDecimal(txtValor.Text), Convert.ToDateTime(dtdFecha.SelectedDate), txtDescripcion.Text).ToString();
                        this.ShowMessageAsync("Mensaje", rpta);
                        Limpiar();
                    }

                }
         
            Mostrar();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            dtdFecha.SelectedDate = null;
            txtValor.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtAbono.Text = string.Empty;
        }
    }
}
