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

            if (txtNombre.Text == string.Empty || txtDescripcion.Text == string.Empty || validarGroupBox() == false)
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
            
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string rpta = "";

            if (txtId.Text == string.Empty)
            {
                if (IsValid(txtNombre) && IsValid(txtDescripcion))
                {
                    rpta = MLabores.GetInstance().GestionLabor( txtNombre.Text,Convert.ToBoolean(rbtnArbolesSi.IsChecked), Convert.ToBoolean(rbtnInsumoSi.IsChecked), txtDescripcion.Text ,0, 1);
                    this.ShowMessageAsync("Mensaje", rpta);
                    Limpiar();
                    tabConsultar.Focus();
                }
            }
            else
            {
                if (IsValid(txtNombre) && IsValid(txtDescripcion) )
                {
                    rpta = MLabores.GetInstance().GestionLabor(txtNombre.Text, Convert.ToBoolean(rbtnArbolesSi.IsChecked), Convert.ToBoolean(rbtnInsumoSi.IsChecked), txtDescripcion.Text, Convert.ToInt32(txtId.Text), 2);
                    this.ShowMessageAsync("Mensaje", rpta);
                    Limpiar();
                    lblEstado.Content = "REGISTRAR INSUMOS";
                    btnGuardar.Margin = new Thickness(520, 26, 0, 0);
                    btnCancelar.Visibility = Visibility.Collapsed;
                    tabConsultar.IsEnabled = true;
                    tabConsultar.Focus();
                }
            }
            Mostrar();
        }

        

        private void txtBuscarNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            BuscarNombre();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Labor item = tblLabores.SelectedItem as Labor;

            txtId.Text = item.idLabor.ToString();
            txtNombre.Text = item.NombreLabor;
            txtDescripcion.Text = item.Descripcion;
           
         
            if (item.ModificaArboles == true)
            {
                rbtnArbolesSi.IsChecked = true;
                rbtnArbolesNo.IsChecked = false;
            }
            else
            {
                rbtnArbolesSi.IsChecked = false;
                rbtnArbolesNo.IsChecked = true;
            }

            if (item.RequiereInsumo == true)
            {
                rbtnInsumoSi.IsChecked = true;
                rbtnInsumoNo.IsChecked = false;
            }
            else
            {
                rbtnInsumoSi.IsChecked = false;
                rbtnInsumoNo.IsChecked = true;
            }

            lblEstado.Content = "MODIFICAR INSUMOS";
            btnGuardar.Margin = new Thickness(402, 26, 0, 0);
            btnCancelar.Visibility = Visibility.Visible;
            tabConsultar.IsEnabled = false;
            tabRegistrar.Focus();
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
            Labor item = tblLabores.SelectedItem as Labor;

            int id = item.idLabor;
            string nombre = item.NombreLabor;
            string descripcion = item.Descripcion;
            bool insumo = item.RequiereInsumo;
            bool modifica = item.ModificaArboles;
            

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

                rpta = MLabores.GetInstance().GestionLabor( nombre, insumo, modifica, descripcion, id, 3).ToString();
                await this.ShowMessageAsync("CoffeeLand", rpta);
                Mostrar();
            }
        }

        public static bool IsValid(DependencyObject parent)
        {
            if (Validation.GetHasError(parent))
            {
                return false;
            }

            return true;
        }
    }
}
