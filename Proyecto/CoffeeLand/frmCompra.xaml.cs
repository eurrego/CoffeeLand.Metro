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
    /// Lógica de interacción para frmCompra.xaml
    /// </summary>
    public partial class frmCompra : MetroWindow
    {

        DataTable dtDetalleCompra = new DataTable();
        int init = 0;
        int index = -1;

        public frmCompra()
        {
            InitializeComponent();
            cmbTipoInsumo.IsEnabled = false;
            cmbInsumo.IsEnabled = false;
        }







        private void frmCompra1_Loaded(object sender, RoutedEventArgs e)
        {
            cmbProveedor.ItemsSource = MCompra.GetInstance().ConsultarProveedor();
            cmbTipoInsumo.ItemsSource = MCompra.GetInstance().ConsultarTipoInsumo();
        }





        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {

            if (validarCampos(1))
            {

                if (init == 0)
                {
                    dtDetalleCompra.Columns.Add("Nombre", typeof(string));
                    dtDetalleCompra.Columns.Add("Cantidad", typeof(string));
                    dtDetalleCompra.Columns.Add("Precio", typeof(string));
                    dtDetalleCompra.Columns.Add("Subtotal", typeof(string));
                    dtDetalleCompra.Columns.Add("idInsumo", typeof(string));
                    dtDetalleCompra.Columns.Add("idCompra", typeof(string));
                    dtDetalleCompra.Columns.Add("TipoInsumo", typeof(string));
                    init++;
                }

                Insumo item = cmbInsumo.SelectedItem as Insumo;

                DataRow fila = dtDetalleCompra.NewRow();
                fila["idInsumo"] = item.idInsumo.ToString();
                fila["Nombre"] = item.NombreInsumo.ToString();
                fila["Cantidad"] = txtCantidad.Text;
                fila["Precio"] = txtValor.Text;
                fila["Subtotal"] = (int.Parse(txtValor.Text) * int.Parse(txtCantidad.Text)).ToString();
                fila["TipoInsumo"] = cmbTipoInsumo.Text;
                dtDetalleCompra.Rows.Add(fila);


                tblDetalleCompra.ItemsSource = dtDetalleCompra.DefaultView;
                limpiarCampos();
                TotalCompra();
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {


            if (validarCampos(2))
            {
                if (validarCampos(3))
                {




                    string compra = "";

                    compra = MCompra.GetInstance().RegistroCompra(cmbProveedor.SelectedValue.ToString(), Convert.ToDateTime(dtdFecha.SelectedDate), int.Parse(txtNumeroFactura.Text)).ToString();


                    if (!compra.Equals("0"))
                    {


                        foreach (DataRow dr in dtDetalleCompra.Rows)
                        {
                            dr["idCompra"] = compra;
                        }

                        dtDetalleCompra.Columns.Remove("TipoInsumo");
                        MCompra.GetInstance().RegistroDetalleCompra(dtDetalleCompra);

                        dtDetalleCompra.Clear();
                        limpiarCamposCompra();
                        dtDetalleCompra.Columns.Add("TipoInsumo");

                    }

                    else
                    {
                        mensajeError("Este número de factura ya existe");
                    }






                }
            }
        }

        private void btnInhabilitar_Click(object sender, RoutedEventArgs e)
        {
            index = tblDetalleCompra.SelectedIndex;
            dtDetalleCompra.Rows[index].Delete();
            index = -1;
            TotalCompra();

        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            index = tblDetalleCompra.SelectedIndex;
            cmbTipoInsumo.Text = dtDetalleCompra.Rows[index].ItemArray[6].ToString();

            TipoInsumo item = cmbTipoInsumo.SelectedItem as TipoInsumo;
            cmbInsumo.ItemsSource = MCompra.GetInstance().ConsultarInsumo(item.idTipoInsumo);

            cmbInsumo.Text = dtDetalleCompra.Rows[index].ItemArray[0].ToString();
            txtCantidad.Text = dtDetalleCompra.Rows[index].ItemArray[1].ToString();
            txtValor.Text = dtDetalleCompra.Rows[index].ItemArray[2].ToString();
            dtDetalleCompra.Rows[index].Delete();
            TotalCompra();


        }


        public void limpiarCampos()
        {
            cmbTipoInsumo.SelectedIndex = 0;
            cmbInsumo.SelectedIndex = 0;
            txtValor.Text = string.Empty;
            txtCantidad.Text = string.Empty;


        }

        public void limpiarCamposCompra()
        {
            cmbProveedor.SelectedIndex = 0;
            txtNumeroFactura.Text = string.Empty;
            dtdFecha.Text = string.Empty;
            lblTotal.Text = "0";


        }

        public void TotalCompra()
        {
            int total = 0;

            foreach (DataRow row in dtDetalleCompra.Rows)
            {
                total += int.Parse(row["Subtotal"].ToString());

            }

            lblTotal.Text = total.ToString();


        }

        private bool validarCampos(int campos)
        {
            bool validacion = false;

            if (campos == 1)
            {


                if (txtCantidad.Text == string.Empty || txtValor.Text == string.Empty || cmbInsumo.SelectedIndex == 0 || cmbTipoInsumo.SelectedIndex == 0)
                {
                    mensajeError("Debe Ingresar todos los Campos");
                    validacion = false;
                }
                else
                {
                    validacion = true;
                }
            }
            else if (campos == 2)
            {

                if (txtNumeroFactura.Text == string.Empty || dtdFecha.Text == string.Empty || cmbProveedor.SelectedIndex == 0)
                {
                    mensajeError("Debe Ingresar todos los Campos");
                    validacion = false;
                }
                else
                {
                    validacion = true;
                }

            }
            else if (campos == 3)
            {
                if (dtDetalleCompra.Rows.Count == 0)
                {
                    mensajeError("Debe agregar por lo menos un insumo");
                    validacion = false;
                }
                else
                {
                    validacion = true;
                }

            }

            return validacion;
        }

        // mensaje de Error
        private async void mensajeError(string mensaje)
        {
            await this.ShowMessageAsync("Error", mensaje);
        }

        private void cmbProveedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cmbProveedor.SelectedIndex == 0)
            {
                lblProveedor.Text = "Seleccione un proveedor";
                cmbTipoInsumo.IsEnabled = false;

            }
            else
            {
                Proveedor item = cmbProveedor.SelectedItem as Proveedor;
                lblProveedor.Text = item.NombreProveedor;
                cmbTipoInsumo.IsEnabled = true;

            }
        }

        private void cmbTipoInsumo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cmbTipoInsumo.SelectedIndex == 0)
            {

                cmbInsumo.IsEnabled = false;

            }
            else
            {
                TipoInsumo item = cmbTipoInsumo.SelectedItem as TipoInsumo;

                cmbInsumo.ItemsSource = MCompra.GetInstance().ConsultarInsumo(item.idTipoInsumo);
                cmbInsumo.IsEnabled = true;


            }
        }

        private void btnCrearProveedor_Click(object sender, RoutedEventArgs e)
        {

            Close();
            frmProveedor miProveedor = new frmProveedor();
            miProveedor.Show();

        }
    }
}
