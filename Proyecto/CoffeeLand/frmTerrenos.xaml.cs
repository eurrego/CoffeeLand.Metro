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
using System.Collections;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmTerrenos.xaml
    /// </summary>
    public partial class frmTerrenos : MetroWindow
    {

        public static DataTable dt;
        public static DataTable dt1;

        int init = 0;
        int init1 = 0;
        int opc = 0;
        int opc2 = 0;
        int index = -1;
        int cantidadArboles = 0;

        bool validacion = false;


        public frmTerrenos()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            cmbLotes.ItemsSource = MTerrenos.GetInstance().ConsultarLote();
            cmbLabores.ItemsSource = MTerrenos.GetInstance().ConsultarLabor();
            cmbInsumo.ItemsSource = MTerrenos.GetInstance().ConsultarInsumo();
            cmbEmpleado.ItemsSource = MTerrenos.GetInstance().ConsultarEmpleado();
            llenarCmbTipoPago();
        }


        private void llenarCmbTipoPago()
        {
            List<string> data = new List<string>();
            data.Add("Seleccione un tipo de Pago");
            data.Add("Contrato");
            data.Add("Jornal");

            cmbTipoPago.ItemsSource = data;
        }

        public void crearTabla(int opc2)
        {

            switch (opc2)
            {
                case 1:

                    if (init == 0)
                    {
                        dt = new DataTable();
                        dt.Columns.Add("idLabor_Lote");
                        dt.Columns.Add("IdInsumo");
                        dt.Columns.Add("Cantidad");
                        dt.Columns.Add("Precio");
                        dt.Columns.Add("NombreInsumo");
                        init++;
                    }
                    break;

                case 2:

                    if (init1 == 0)
                    {
                        dt1 = new DataTable();
                        dt1.Columns.Add("DocumentoPersona");
                        dt1.Columns.Add("idLabor_Lote");
                        dt1.Columns.Add("Cantidad");
                        dt1.Columns.Add("Valor");
                        dt1.Columns.Add("NombrePersona");

                        init1++;
                    }
                    break;
            }
        }

        private void cmbLabores_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

            Labor labor = cmbLabores.SelectedItem as Labor;

            if (labor.RequiereInsumo)
            {
                groupInsumos.IsEnabled = true;
            }
            else
            {
                groupInsumos.IsEnabled = false;
            }

        }

        private async void mensajeError(string mensaje)
        {
            await this.ShowMessageAsync("Error", mensaje);
        }

        public void limpiarCampos(int opc)
        {
            if (opc == 1)
            {
                cmbInsumo.SelectedIndex = 0;
                txtCantidadInsumo.Text = string.Empty;
            }
            else if (opc == 2)
            {
                if (cmbTipoPago.SelectedItem.Equals("Jornal"))
                {
                    cmbEmpleado.SelectedIndex = 0;
                    txtValorProductividad.Text = string.Empty;
                }
                else
                {
                    cmbEmpleado.SelectedIndex = 0;
                    txtCantidadProductividad.Text = string.Empty;
                    txtValorProductividad.Text = string.Empty;
                }

            }
            else
            {
                cmbLabores.SelectedIndex = 0;
                cmbTipoPago.SelectedIndex = 0;
                dtdFechaLabor.Text = string.Empty;
                cmbLotes.SelectedIndex = 0;
            }



        }

        private bool validarCampos(int opc)
        {
            switch (opc)
            {
                case 1:
                    if (cmbInsumo.Text == string.Empty || txtCantidadInsumo.Text == string.Empty)
                    {
                        mensajeError("Debe Ingresar todos los Campos");
                        validacion = false;
                    }
                    else
                    {
                        validacion = true;
                    }

                    break;

                case 2:

                    if (cmbEmpleado.SelectedIndex == 0 || txtCantidadProductividad.Text == string.Empty || txtValorProductividad.Text == string.Empty)
                    {
                        mensajeError("Debe Ingresar todos los Campos");
                        validacion = false;
                    }
                    else
                    {
                        validacion = true;
                    }

                    break;
            }

            return validacion;
        }

        private void btnAgregarInsumos_Click(object sender, RoutedEventArgs e)
        {
            Insumo insumo = cmbInsumo.SelectedItem as Insumo;


            if (validarCampos(1))
            {
                crearTabla(1);

                if (insumo.CantidadExistente >= int.Parse(txtCantidadInsumo.Text))
                {
                    dt.Rows.Add(null, cmbInsumo.SelectedValue, txtCantidadInsumo.Text, insumo.PrecioPromedio, cmbInsumo.Text);

                    tblInsumos.ItemsSource = dt.DefaultView;
                }
                else
                {
                    mensajeError("La cantidad del insumo es menor a la ingresada");
                }


            }
            limpiarCampos(1);

        }

        private void btnAgregarProductividad_Click(object sender, RoutedEventArgs e)
        {
            if (validarCampos(2))
            {
                crearTabla(2);


                dt1.Rows.Add(cmbEmpleado.SelectedValue, null, int.Parse(txtCantidadProductividad.Text), int.Parse(txtValorProductividad.Text), cmbEmpleado.Text);

                tblProductividad.ItemsSource = dt1.DefaultView;


            }
            limpiarCampos(2);
        }

        private void btnModificarInsumo_Click(object sender, RoutedEventArgs e)
        {
            index = tblInsumos.SelectedIndex;
            cmbInsumo.Text = dt.Rows[index].ItemArray[0].ToString();
            txtCantidadInsumo.Text = dt.Rows[index].ItemArray[1].ToString();

            dt.Rows[index].Delete();
        }

        private void btnInhabilitarInsumo_Click(object sender, RoutedEventArgs e)
        {
            index = tblInsumos.SelectedIndex;
            dt.Rows[index].Delete();
            index = -1;
        }

        private void btnInhabilitarProductividadEmpleado_Click(object sender, RoutedEventArgs e)
        {
            index = tblProductividad.SelectedIndex;
            dt1.Rows[index].Delete();
            index = -1;

        }
        private void btnModificarProductividadEmpleado_Click(object sender, RoutedEventArgs e)
        {
            CantidadProductividad();
            index = tblProductividad.SelectedIndex;
            cmbEmpleado.SelectedItem = dt1.Rows[index].ItemArray[4].ToString();
            txtCantidadProductividad.Text = dt1.Rows[index].ItemArray[2].ToString();
            txtValorProductividad.Text = dt1.Rows[index].ItemArray[3].ToString();

            dt1.Rows[index].Delete();
        }



        public void CantidadProductividad()
        {
            if (cmbTipoPago.SelectedItem.Equals("Jornal"))
            {
                txtCantidadProductividad.Text = "1";
                txtCantidadProductividad.IsEnabled = false;
            }
            else
            {
                txtCantidadProductividad.Text = string.Empty;
                txtCantidadProductividad.IsEnabled = true;
            }
        }

        private void cmbLotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Lote item = cmbLotes.SelectedItem as Lote;

            if (item.idLote != 0)
            {
                lblNombreLote.Content = item.NombreLote;

            }

        }

        private void cmbInsumo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Insumo insumo = cmbInsumo.SelectedItem as Insumo;

            txtUnidadMedida.Text = insumo.UnidadMedida;
            lblCantidad.Content = insumo.CantidadExistente;
            var precioPromedio = insumo.PrecioPromedio;
        }

        public void GuardarDatos()
        {
            string Idlabor_lote = MTerrenos.GetInstance().asociarLaborLote(int.Parse((cmbLabores.SelectedValue).ToString()), int.Parse((cmbLotes.SelectedValue).ToString()), Convert.ToDateTime(dtdFechaLabor.SelectedDate)).ToString();

            foreach (DataRow itemEmpleado in dt1.Rows)
            {
                itemEmpleado["IdLabor_Lote"] = int.Parse(Idlabor_lote);
            }

            if (dt != null)
            {
                foreach (DataRow itemInsumo in dt.Rows)
                {
                    itemInsumo["IdLabor_Lote"] = int.Parse(Idlabor_lote);
                }

                dt.Columns.Remove("NombreInsumo");
                MTerrenos.GetInstance().asociarInsumoLaborLote(dt);
                dt.Clear();
                dt.Columns.Add("NombreInsumo");
            }


            dt1.Columns.Remove("NombrePersona");

            MTerrenos.GetInstance().salarioEmpleado(dt1);


            if (cmbLabores.Text.Equals("Produccion"))
            {
                int cantidad = 0;
                foreach (DataRow itemCantidad in dt1.Rows)
                {
                    cantidad += int.Parse(itemCantidad["Cantidad"].ToString());
                }

                MTerrenos.GetInstance().registrarProduccion(int.Parse(cmbLotes.SelectedValue.ToString()), Convert.ToDateTime(dtdFechaLabor.ToString()), cantidad);
            }
            dt1.Columns.Add("NombrePersona");

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dt1 == null)
                {

                    mensajeError("Debe Ingresar todos los Campos");
                }
                else
                {

                    Labor labor = cmbLabores.SelectedItem as Labor;

                    if (labor.ModificaArboles)
                    {
                        FlyoutAbonos.IsOpen = true;

                        cmbTipoArbolLote.ItemsSource = MTerrenos.GetInstance().LaborModificaArbol(int.Parse(cmbLotes.SelectedValue.ToString())) as IEnumerable;
                        cmbTipoArbolModificar.ItemsSource = MTerrenos.GetInstance().ConsultarTipoArboles();

                        foreach (DataRow itemCantidad in dt1.Rows)
                        {
                            cantidadArboles += int.Parse(itemCantidad["Cantidad"].ToString());
                        }

                        txtArbolesModicacion.Text = (cantidadArboles).ToString();

                    }
                    else
                    {
                        GuardarDatos();
                    }

                    if (!FlyoutAbonos.IsOpen)
                    {
                        limpiarCampos(0);
                        dt1.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbTipoArbolLote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTipoArbolLote.SelectedIndex != 0)
            {

                var cantidad = MTerrenos.GetInstance().cantidadArbolesLote(int.Parse(cmbTipoArbolLote.SelectedValue.ToString()), int.Parse(cmbLotes.SelectedValue.ToString()));

                txtCantidadArbolesLotes.Text = cantidad.ToString();
            }
        }

        private void btnConfirmarModificacionArbol_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                MTerrenos.GetInstance().MovimientoArboles(short.Parse(cmbLotes.SelectedValue.ToString()), byte.Parse(cmbTipoArbolLote.SelectedValue.ToString()), cantidadArboles, Convert.ToDateTime(dtdFechaLabor.SelectedDate), 0, "Salida", 1);
                MTerrenos.GetInstance().MovimientoArboles(short.Parse(cmbLotes.SelectedValue.ToString()), byte.Parse(cmbTipoArbolModificar.SelectedValue.ToString()), cantidadArboles, Convert.ToDateTime(dtdFechaLabor.SelectedDate), 0, "Entrada", 1);

                GuardarDatos();
                FlyoutAbonos.IsOpen = false;
                limpiarCampos(0);
                dt1.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbTipoPago_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CantidadProductividad();
        }


    }

}

