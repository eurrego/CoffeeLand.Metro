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
        private void mensajeError(string mensaje)
        {
            this.ShowMessageAsync("Error", mensaje);
        }

        // Define el estilo de las celdas 
        private void EstilosCeldas()
        {
            lblTotal.Content = tblEmpleado.Items.Count.ToString(); ;
        }

        // Validación de campos
        private bool validarCampos()
        {

            if (cmbTipoDocumento.SelectedIndex == 0 || cmbTipoContrato.SelectedIndex == 0 || cmbGenero.SelectedIndex == 0 ||  txtNombre.Text == string.Empty || txtDocumento.Text == string.Empty || txtTelefono.Text == string.Empty)
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

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Mostrar();
            tabConsultar.Focus();
        }

        private void txtBuscarNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            BuscarNombre();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string rpta = "";

            if (txtId.Text == string.Empty)
            {
                if (validarCampos())
                {
                    rpta = MPersona.GetInstance().GestionPersona(txtNombre.Text,Convert.ToString(cmbGenero.SelectedItem),txtTelefono.Text,Convert.ToDateTime(dtdFechaNacimiento.SelectedDate), Convert.ToInt32(txtDocumento.Text), 1, Convert.ToByte(cmbTipoDocumento.SelectedValue), Convert.ToByte(cmbTipoContrato.SelectedValue));
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
                tabConsultar.Focus();
            }
            Mostrar();
        }

        private void cmbGenero_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("Seleccione un Género...");
            data.Add("M");
            data.Add("F");

            cmbGenero.ItemsSource = data;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            DataGridRow dgRow = (DataGridRow)(tblEmpleado.ItemContainerGenerator.ContainerFromItem(tblEmpleado.SelectedItem));

            if (dgRow == null)
            {
                return;
            }

            DataGridDetailsPresenter dgdPresenter = FindVisualChild<DataGridDetailsPresenter>(dgRow);
            DataTemplate template = dgdPresenter.ContentTemplate;

            TextBox text = (TextBox)template.FindName("txtNombrePersona", dgdPresenter);
            string nombre = text.Text;
            txtNombre.Text = nombre;

            TextBox text2 = (TextBox)template.FindName("txtGenero", dgdPresenter);
            string genero = text2.Text;
            cmbGenero.SelectedItem = genero;

            TextBox text3 = (TextBox)template.FindName("txtId", dgdPresenter);
            string id = text3.Text;
            txtId.Text = id;

            TextBox text4 = (TextBox)template.FindName("txtId", dgdPresenter);
            string documento = text4.Text;
            txtDocumento.Text = documento;

            TextBox text5 = (TextBox)template.FindName("txtFecha", dgdPresenter);
            dtdFechaNacimiento.DisplayDate = Convert.ToDateTime(text5.Text);

            TextBox text6 = (TextBox)template.FindName("txtTipoContrato", dgdPresenter);
            string tipoContrato = text6.Text;
            cmbTipoContrato.SelectedValue = tipoContrato;

            TextBox text7 = (TextBox)template.FindName("txtIdTipoDocumento", dgdPresenter);
            string tipoDocumento = text7.Text;
            cmbTipoDocumento.SelectedValue = tipoDocumento;

            TextBox text8 = (TextBox)template.FindName("txtTelefono", dgdPresenter);
            string telefono = text8.Text;
            txtTelefono.Text = telefono;


            lblEstado.Content = "MODIFICAR EMPLEADOS";
            btnGuardar.Margin = new Thickness(402, 48, 0, 0);
            btnCancelar.Visibility = Visibility.Visible;
            tabConsultar.IsEnabled = false;
            tabRegistrar.Focus();
        }

        public static T FindVisualChild<T>(DependencyObject obj)
        where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }

            return null;
        }
    }
}
