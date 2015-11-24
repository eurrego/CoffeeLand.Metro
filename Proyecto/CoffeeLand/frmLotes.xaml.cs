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
    /// Lógica de interacción para frmLotes.xaml
    /// </summary>
    public partial class frmLotes : MetroWindow
    {
        int index = -1;

        public static DataTable dt = new DataTable();
       
        // variable para controlar que los campos esten llenos
        bool validacion = false;

        public frmLotes()
        {
            InitializeComponent();


            dt.Columns.Add("idTipoArbol");
            dt.Columns.Add("NombreTipoArbol");
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("Fecha", typeof(DateTime));

        }

        // mensaje de Error
        private async void mensajeError(string mensaje)
        {
            await this.ShowMessageAsync("Error", mensaje);
        }

        // limpiar Controles lote
        private void LimpiarControlesLote()
        {
            txtNombre.Text = string.Empty;
            txtCuadras.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
        }

        // limpiar Controles arboles
        private void LimpiarControlesArbol()
        {
            cmbTipoArbol.SelectedIndex = 0;
            txtCantidad.Text = string.Empty;
            dtdFecha.SelectedDate = null;
        }

        //mostrar
        private void Mostrar()
        {
            cmbTipoArbol.ItemsSource = MLote.GetInstance().ConsultarTipoArbol();
        }

        // Validación de campos
        private bool validarCampos()
        {

            if (cmbTipoArbol.SelectedIndex == 0 || txtCantidad.Text == string.Empty || txtCantidad.Text == 0.ToString() || dtdFecha.SelectedDate == null)
            {
                mensajeError("Debe Ingresar todos los Campos, el campo Cantidad no debe ser 0");
                validacion = false;
            }
            else
            {
                validacion = true;
            }

            return validacion;
        }

        // define el total de arboles
        private decimal cantidadTotal()
        {
            decimal total = 0;

            foreach (DataRowView item in tblArboles.ItemsSource)
            {
                total += Convert.ToDecimal(item.Row.ItemArray[2]);
            }

            lblCantidad.Text = total.ToString();

            return total;
        }


        private void frmLotes1_Loaded(object sender, RoutedEventArgs e)
        {
            Mostrar();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {

            if (validarCampos())
            {
                if (index < 0)
                {
                    dt.Rows.Add(Convert.ToByte(cmbTipoArbol.SelectedValue), cmbTipoArbol.Text, Convert.ToInt32(txtCantidad.Text), Convert.ToDateTime(dtdFecha.SelectedDate));
                    tblArboles.ItemsSource = dt.DefaultView;

                    lblTotal.Content = tblArboles.Items.Count.ToString();
                    cantidadTotal();
                    LimpiarControlesArbol();
                }
                else
                {
                    dt.Rows[index].Delete();
                    dt.Rows.Add(Convert.ToByte(cmbTipoArbol.SelectedValue), cmbTipoArbol.Text, Convert.ToInt32(txtCantidad.Text), Convert.ToDateTime(dtdFecha.SelectedDate));
                    tblArboles.IsEnabled = true;
                    btnCancelarEdicion.Visibility = Visibility.Collapsed;
                    btnAgregar.Margin = new Thickness(387, 71, 0, 0);
                    index = -1;
                    cantidadTotal();
                    LimpiarControlesArbol();
                }
             
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            index = tblArboles.SelectedIndex;

            cmbTipoArbol.SelectedValue = dt.Rows[index].ItemArray[0];
            txtCantidad.Text = dt.Rows[index].ItemArray[2].ToString();
            dtdFecha.SelectedDate = Convert.ToDateTime(dt.Rows[index].ItemArray[3]);

            btnAgregar.Margin = new Thickness(272, 71, 0, 0);
            btnCancelarEdicion.Visibility = Visibility.Visible;

            tblArboles.IsEnabled = false;
          
        }

        private void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            index = tblArboles.SelectedIndex;
            dt.Rows[index].Delete();
            index = -1;
            cantidadTotal();
        }

        private void btnCancelarEdicion_Click(object sender, RoutedEventArgs e)
        {
            index = -1;
            btnCancelarEdicion.Visibility = Visibility.Collapsed;
            btnAgregar.Margin = new Thickness(387, 71, 0, 0);
            tblArboles.IsEnabled = true;

            LimpiarControlesArbol();
        }
    }
}
