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
    /// Lógica de interacción para frmInsumo.xaml
    /// </summary>
    public partial class frmInsumo : MetroWindow
    {
        bool validacion = false;

        public frmInsumo()
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
            lblTotal.Content = tblInsumo.Items.Count.ToString(); ;
        }

        // Validación de campos
        private bool validarCampos()
        {

            if (cmbTipoInsumo.SelectedIndex == 0 || txtNombre.Text == string.Empty || txtMarca.Text == string.Empty || txtUnidadMedida.Text == string.Empty || txtDescripcion.Text == string.Empty)
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
            cmbTipoInsumo.SelectedIndex = 0;
            txtNombre.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtUnidadMedida.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtId.Text = string.Empty;
        }

        //mostrar
        private void Mostrar()
        { 
            tblInsumo.ItemsSource = MInsumo.GetInstance().ConsultarInsumo();
            cmbTipoInsumo.ItemsSource = MInsumo.GetInstance().ConsultarTipoInsumo();
            EstilosCeldas();
        }


        //Método para Buscar por nombre
        private void BuscarNombre()
        {
            tblInsumo.ItemsSource = MInsumo.GetInstance().ConsultarParametroInsumo(txtBuscarNombre.Text);
            EstilosCeldas();
        }

        private void frmInsumo1_Loaded(object sender, RoutedEventArgs e)
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
                    rpta = MInsumo.GetInstance().GestionInsumo(Convert.ToByte(cmbTipoInsumo.SelectedValue), txtNombre.Text, txtDescripcion.Text, txtMarca.Text, txtUnidadMedida.Text, 0, 1);
                    this.ShowMessageAsync("Mensaje", rpta);
                    Limpiar();
                    tabConsultar.Focus();
                }
            }
            else
            {
                rpta = MInsumo.GetInstance().GestionInsumo(Convert.ToByte(cmbTipoInsumo.SelectedValue), txtNombre.Text, txtDescripcion.Text, txtMarca.Text, txtUnidadMedida.Text, Convert.ToInt32(txtId.Text), 2);
                this.ShowMessageAsync("Mensaje", rpta);
                Limpiar();
                lblEstado.Content = "REGISTRAR INSUMOS";
                btnGuardar.Margin = new Thickness(520, 48, 0, 0);
                btnCancelar.Visibility = Visibility.Collapsed;
                tabConsultar.IsEnabled = true;
                tabConsultar.Focus();
            }
            Mostrar();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            DataGridRow dgRow = (DataGridRow)(tblInsumo.ItemContainerGenerator.ContainerFromItem(tblInsumo.SelectedItem));

            if (dgRow == null)
            {
                return;
            }

            DataGridDetailsPresenter dgdPresenter = FindVisualChild<DataGridDetailsPresenter>(dgRow);
            DataTemplate template = dgdPresenter.ContentTemplate;

            TextBox text = (TextBox)template.FindName("txtNombre", dgdPresenter);
            string nombre = text.Text;
            txtNombre.Text = nombre;

            TextBox text2 = (TextBox)template.FindName("txtDescripcion", dgdPresenter);
            string descripcion = text2.Text;
            txtDescripcion.Text = descripcion;

            TextBox text3 = (TextBox)template.FindName("txtId", dgdPresenter);
            string id = text3.Text;
            txtId.Text = id;

            TextBox text4 = (TextBox)template.FindName("txtMarca", dgdPresenter);
            string marca = text4.Text;
            txtMarca.Text = marca;

            TextBox text5 = (TextBox)template.FindName("txtUnidadMedida", dgdPresenter);
            string unidadMedida = text5.Text;
            txtUnidadMedida.Text = unidadMedida;

            TextBox text6 = (TextBox)template.FindName("txtTipoInsumo", dgdPresenter);
            string idTipoInsumo = text6.Text;
            cmbTipoInsumo.SelectedValue = idTipoInsumo;


            lblEstado.Content = "MODIFICAR INSUMOS";
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

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            lblEstado.Content = "REGISTRAR INSUMOS";
            btnGuardar.Margin = new Thickness(520, 48, 0, 0);
            btnCancelar.Visibility = Visibility.Collapsed;
            tabConsultar.IsEnabled = true;
            tabConsultar.Focus();
        }

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            DataGridRow dgRow = (DataGridRow)(tblInsumo.ItemContainerGenerator.ContainerFromItem(tblInsumo.SelectedItem));

            if (dgRow == null)
            {
                return;
            }

            DataGridDetailsPresenter dgdPresenter = FindVisualChild<DataGridDetailsPresenter>(dgRow);
            DataTemplate template = dgdPresenter.ContentTemplate;


            TextBox text = (TextBox)template.FindName("txtNombre", dgdPresenter);
            string nombre = text.Text;
            
            TextBox text2 = (TextBox)template.FindName("txtDescripcion", dgdPresenter);
            string descripcion = text2.Text;
        
            TextBox text3 = (TextBox)template.FindName("txtId", dgdPresenter);
            string id = text3.Text;

            TextBox text4 = (TextBox)template.FindName("txtMarca", dgdPresenter);
            string marca = text4.Text;

            TextBox text5 = (TextBox)template.FindName("txtUnidadMedida", dgdPresenter);
            string unidadMedida = text5.Text;

            TextBox text6 = (TextBox)template.FindName("txtTipoInsumo", dgdPresenter);
            string idTipoInsumo = text6.Text;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",
                ColorScheme = MetroDialogOptions.ColorScheme
            };

            MessageDialogResult result = await this.ShowMessageAsync("CoffeeLand", "¿Realmente desea Inhabilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result != MessageDialogResult.Negative)
            {
                byte idTipoIn;
                int idInsumo;
                string rpta = "";

                idTipoIn = Convert.ToByte(idTipoInsumo);
                idInsumo = Convert.ToInt32(id);

                rpta = MInsumo.GetInstance().GestionInsumo(idTipoIn, nombre, descripcion, marca, unidadMedida, idInsumo, 3).ToString();
                await this.ShowMessageAsync("CoffeeLand", rpta);
                Mostrar();
            }
        }
    }
}
