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
    /// Lógica de interacción para frmArboles.xaml
    /// </summary>
    public partial class frmArboles : MetroWindow
    {
        bool validacion = false;
        int lote = 0;

        public frmArboles(int lote)
        {
            InitializeComponent();
            this.lote = lote;
        }

        // mensaje de Error
        private void mensajeError(string mensaje)
        {
            this.ShowMessageAsync("Error", mensaje);
        }

        // Define el estilo de las celdas 
        private void EstilosCeldas()
        {
            lblTotal.Content = tblArboles.Items.Count.ToString(); ;
        }

        // Validación de campos
        private bool validarCampos()
        {

            if (cmbLote.SelectedIndex == 0 || cmbLote.SelectedIndex == 0 || txtCantidad.Text == string.Empty || dtdFecha.SelectedDate == null)
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
            cmbLote.SelectedIndex = 0;
            cmbTipoArbol.SelectedIndex = 0;
            txtCantidad.Text = string.Empty;
            dtdFecha.SelectedDate = null;
        }

        //mostrar
        private void Mostrar()
        {
            cmbLote.ItemsSource = MArbol.GetInstance().ConsultarLote();
            cmbTipoArbol.ItemsSource = MArbol.GetInstance().ConsultarTipoArbol();
            EstilosCeldas();
        }

        private void frmArboles1_Loaded(object sender, RoutedEventArgs e)
        {
            Mostrar();
            cmbLote.SelectedValue = lote;
            cmbTipoArbol.SelectedValue = 0;
        }
    }
}
