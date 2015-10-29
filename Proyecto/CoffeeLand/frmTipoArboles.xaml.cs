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
    /// Lógica de interacción para frmTipoArboles.xaml
    /// </summary>
    public partial class frmTipoArboles : MetroWindow
    {
        bool validacion;

        public frmTipoArboles()
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
            //tblTipoArbol.Columns[1].Header = "Nombre";
            //tblTipoArbol.Columns[2].Header= "Descripción";
            lblTotal.Content = tblTipoArbol.Items.Count.ToString(); ;
        }

        //Método para ocultar columnas
        private void OcultarColumnas()
        {
            //tblTipoArbol.Columns[0].Visibility = Visibility.Hidden;
            //tblTipoArbol.Columns[3].Visibility = Visibility.Hidden;
            //tblTipoArbol.Columns[4].Visibility = Visibility.Hidden;

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
            tblTipoArbol.ItemsSource = MTipoArbol.GetInstance().consultarTipoArbol();
            OcultarColumnas();
            EstilosCeldas();
        }


        //Método para Buscar por nombre
        private void BuscarNombre()
        {
            tblTipoArbol.ItemsSource = MTipoArbol.GetInstance().buscarTipoArbol(txtBuscarNombre.Text);
            OcultarColumnas();
            EstilosCeldas();
        }

        private void frmTipoArboles1_Loaded(object sender, RoutedEventArgs e)
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
                    rpta = MTipoArbol.GetInstance().registrarTipoArbol(txtNombre.Text, txtDescripcion.Text, 0, 1).ToString();
                    this.ShowMessageAsync("Mensaje", rpta);

                    Limpiar();
                }
            }
            else if (validarCampos())
            {
                rpta = MTipoArbol.GetInstance().registrarTipoArbol(txtNombre.Text, txtDescripcion.Text, Convert.ToInt32(txtId.Text), 2).ToString();
                this.ShowMessageAsync("Mensaje", rpta);
                Limpiar();
            }
            Mostrar();
        }


        private void txtBuscarNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            BuscarNombre();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            DataGridRow dgRow = (DataGridRow)(tblTipoArbol.ItemContainerGenerator.ContainerFromItem(tblTipoArbol.SelectedItem));

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
                { T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }

            return null;
        }


    }
}
