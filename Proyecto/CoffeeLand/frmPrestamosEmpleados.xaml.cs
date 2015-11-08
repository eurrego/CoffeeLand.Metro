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
        public frmPrestamosEmpleados()
        {
            InitializeComponent();
        }

        // mensaje de Error
        private async void mensajeError(string mensaje)
        {
            await this.ShowMessageAsync("Error", mensaje);
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
            }
        }

        private void frmPrestamosEmpleados1_Loaded(object sender, RoutedEventArgs e)
        {
            cmbEmpleado.ItemsSource = MPrestamosEmpleados.GetInstance().ConsultarEmpleado();
        }

        private void cmbEmpleado_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cmbEmpleado.SelectedIndex == 0)
            {
                lblEmpleado.Text = "Seleccione un Empleado"; 
            }
            else
            {
                Persona item = cmbEmpleado.SelectedItem as Persona;
                lblEmpleado.Text = item.NombrePersona;
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
                }
                else
                {
                    Persona item = list.SelectedItem as Persona;
                    lblEmpleado.Text = item.NombrePersona;
                    list.ItemsSource = null;
                }
            }
            
        }

        private void expTipoBusqueda_MouseLeave(object sender, MouseEventArgs e)
        {
            expTipoBusqueda.IsExpanded = false;
        }
    }
}
