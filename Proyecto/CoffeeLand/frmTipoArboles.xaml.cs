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
    /// Lógica de interacción para frmTipoArboles.xaml
    /// </summary>
    public partial class frmTipoArboles : MetroWindow
    {
        bool validacion;
        byte idTipoArbol = 0;

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
            tblTipoArbol.Columns[1].Header = "Nombre";
            tblTipoArbol.Columns[2].Header= "Descripción";
            lblTotal.Content = tblTipoArbol.Items.Count.ToString(); ;
        }

        //Método para ocultar columnas
        private void OcultarColumnas()
        {
            tblTipoArbol.Columns[0].Visibility = Visibility.Hidden;
            tblTipoArbol.Columns[3].Visibility = Visibility.Hidden;
            tblTipoArbol.Columns[4].Visibility = Visibility.Hidden;

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
            idTipoArbol = 0;
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

            if (idTipoArbol == 0)
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
                rpta = MTipoArbol.GetInstance().registrarTipoArbol(txtNombre.Text, txtDescripcion.Text, idTipoArbol, 2).ToString();
                this.ShowMessageAsync("Mensaje", rpta);
                Limpiar();
            }
            Mostrar();
        }

        private void chkInhabilitar_Checked(object sender, RoutedEventArgs e)
        {

            tblTipoArbol.Columns[0].Visibility = Visibility.Visible;
        }

        private void chkInhabilitar_Unchecked(object sender, RoutedEventArgs e)
        {
            tblTipoArbol.Columns[0].Visibility = Visibility.Hidden;
        }

        private void txtBuscarNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            BuscarNombre();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            BuscarNombre();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
         
        }



      
    }
}
