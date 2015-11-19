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
    /// Lógica de interacción para frmLotes.xaml
    /// </summary>
    public partial class frmLotes : MetroWindow
    {
        // variable para controlar que los campos esten llenos
        bool validacion = false;

        public frmLotes()
        {
            InitializeComponent();
        }

        // mensaje de Error
        private async void mensajeError(string mensaje)
        {
            await this.ShowMessageAsync("Error", mensaje);
        }

        // limpiar Controles
        private void LimpiarControlesLote()
        {
            txtNombre.Text = string.Empty;
            txtCuadras.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
        }

        // limpiar Controles
        private void LimpiarControlesArbol()
        {
            cmbTipoArbol.SelectedIndex = 0;
            txtCantidad.Text = string.Empty;
            dtdFecha.SelectedDate = null;
        }

        //mostrar
        private void Mostrar()
        {
           
        }


    }
}
