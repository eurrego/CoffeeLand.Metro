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
    /// Lógica de interacción para frmLabores.xaml
    /// </summary>
    public partial class frmLabores : MetroWindow
    {
        bool validacion = false;

        public frmLabores()
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
            lblTotal.Content = tblLabores.Items.Count.ToString(); ;
        }

        // Validación de campos
        private bool validarCampos()
        {

            if (cmbTipoPago.SelectedIndex == 0 || txtNombre.Text == string.Empty || txtDescripcion.Text == string.Empty || validarGroupBox() == false)
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


        // validar group box 
        private bool validarGroupBox()
        {
            bool pase1 = false;
            bool pase2 = false;

            if (rbtnArbolesSi.IsChecked == true || rbtnArbolesNo.IsChecked == true)
            {
                pase1 = true;
            }

            if (rbtnInsumoSi.IsChecked == true || rbtnInsumoNo.IsChecked == true)
            {
                pase2 = true;
            }

            if (pase1 == true && pase2 == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // limpiar Controles
        private void Limpiar()
        {
            cmbTipoPago.SelectedIndex = 0;
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtId.Text = string.Empty;
        }

        //mostrar
        private void Mostrar()
        {
            tblLabores.ItemsSource = MLabores.GetInstance().consultarLabor();
            EstilosCeldas();
        }


        //Método para Buscar por nombre
        private void BuscarNombre()
        {
            tblLabores.ItemsSource = MLabores.GetInstance().buscarLabor(txtBuscarNombre.Text);
            EstilosCeldas();
        }

        private void frmLabores1_Loaded(object sender, RoutedEventArgs e)
        {
            Mostrar();
            cmbTipoPago.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string rpta = "";

            if (txtId.Text == string.Empty)
            {
                if (validarCampos())
                {
                    rpta = MLabores.GetInstance().GestionLabor(Convert.ToString(cmbTipoPago.SelectedItem), txtNombre.Text,Convert.ToBoolean(rbtnArbolesSi.IsChecked), Convert.ToBoolean(rbtnInsumoSi.IsChecked), txtDescripcion.Text ,0, 1);
                    this.ShowMessageAsync("Mensaje", rpta);
                    Limpiar();
                    tabConsultar.Focus();
                }
            }
            else
            {
                rpta = MLabores.GetInstance().GestionLabor(Convert.ToString(cmbTipoPago.SelectedItem), txtNombre.Text, Convert.ToBoolean(rbtnArbolesSi.IsChecked), Convert.ToBoolean(rbtnInsumoSi.IsChecked), txtDescripcion.Text, Convert.ToInt32(txtId.Text), 2); 
                this.ShowMessageAsync("Mensaje", rpta);
                Limpiar();
                lblEstado.Content = "REGISTRAR INSUMOS";
                btnGuardar.Margin = new Thickness(520, 26, 0, 0);
                btnCancelar.Visibility = Visibility.Collapsed;
                tabConsultar.IsEnabled = true;
                tabConsultar.Focus();
            }
            Mostrar();
        }

        private void cmbTipoPago_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("Seleccione un tipo de Pago...");
            data.Add("Productividad");
            data.Add("Jornal");

            cmbTipoPago.ItemsSource = data;
        }

        private void txtBuscarNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            BuscarNombre();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            DataGridRow dgRow = (DataGridRow)(tblLabores.ItemContainerGenerator.ContainerFromItem(tblLabores.SelectedItem));

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

            CheckBox text4 = (CheckBox)template.FindName("chkRequiereInsumo", dgdPresenter);
            if (text4.IsChecked == true)
            {
                rbtnArbolesSi.IsChecked = true;
                rbtnArbolesNo.IsChecked = false;
            }
            else
            {
                rbtnArbolesSi.IsChecked = false;
                rbtnArbolesNo.IsChecked = true;
            }


            CheckBox text5 = (CheckBox)template.FindName("chkModificaArboles", dgdPresenter);
            if (text5.IsChecked == true)
            {
                rbtnInsumoSi.IsChecked = true;
                rbtnInsumoNo.IsChecked = false;
            }
            else
            {
                rbtnInsumoSi.IsChecked = false;
                rbtnInsumoNo.IsChecked = true;
            }

            TextBox text6 = (TextBox)template.FindName("txtTipoPago", dgdPresenter);
            string tipoPago = text6.Text;
            cmbTipoPago.SelectedItem = tipoPago;


            lblEstado.Content = "MODIFICAR INSUMOS";
            btnGuardar.Margin = new Thickness(402, 26, 0, 0);
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
            btnGuardar.Margin = new Thickness(520, 26, 0, 0);
            btnCancelar.Visibility = Visibility.Collapsed;
            tabConsultar.IsEnabled = true;
            tabConsultar.Focus();
        }

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            DataGridRow dgRow = (DataGridRow)(tblLabores.ItemContainerGenerator.ContainerFromItem(tblLabores.SelectedItem));

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

            CheckBox text4 = (CheckBox)template.FindName("chkRequiereInsumo", dgdPresenter);
            bool insumo = Convert.ToBoolean(text4.IsChecked);

            CheckBox text5 = (CheckBox)template.FindName("chkModificaArboles", dgdPresenter);
            bool modifica = Convert.ToBoolean(text5.IsChecked);

            TextBox text6 = (TextBox)template.FindName("txtTipoPago", dgdPresenter);
            string tipoPago = text6.Text;


            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",
                ColorScheme = MetroDialogOptions.ColorScheme
            };

            MessageDialogResult result = await this.ShowMessageAsync("CoffeeLand", "¿Realmente desea Inhabilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result != MessageDialogResult.Negative)
            {

                int idInsumo;
                string rpta = "";

                idInsumo = Convert.ToInt32(id);

                rpta = MLabores.GetInstance().GestionLabor(tipoPago, nombre, insumo, modifica, descripcion, idInsumo, 3).ToString();
                await this.ShowMessageAsync("CoffeeLand", rpta);
                Mostrar();
            }
        }
    }
}
