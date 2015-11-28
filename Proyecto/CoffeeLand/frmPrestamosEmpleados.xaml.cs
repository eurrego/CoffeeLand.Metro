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
    /// Lógica de interacción para frmPrestamosEmpleados.xaml
    /// </summary>
    public partial class frmPrestamosEmpleados : MetroWindow
    {
        // variable para controlar que los campos esten llenos
        bool validacion = false;

        public frmPrestamosEmpleados()
        {
            InitializeComponent();

        }

        // mensaje de Error
        private async void mensajeError(string mensaje)
        {
            await this.ShowMessageAsync("Error", mensaje);
        }

        // limpiar Controles
        private void Limpiar()
        {
            dtdFecha.SelectedDate = null;
            txtValor.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtAbono.Text = string.Empty;
            tblPrestamos.ItemsSource = null;
            lblTotal.Text = string.Empty;
        }

        // define la deuda total
        private decimal deudaTotal()
        {
            decimal total = 0;

            foreach (DeudaPersona item in tblPrestamos.ItemsSource)
            {
                total += item.Valor;
            }

            lblTotal.Text = string.Format("{0:c}", total);

            return total;
        }

        //mostrar
        private void Mostrar()
        {
            if (cmbEmpleado.IsVisible)
            {
                tblPrestamos.ItemsSource = MPrestamosEmpleados.GetInstance().ConsultarDeudaEmpleado(cmbEmpleado.SelectedValue.ToString());
            }
            else
            {
                tblPrestamos.ItemsSource = MPrestamosEmpleados.GetInstance().ConsultarDeudaEmpleado(txtBuscarDocumento.Text);
            }
            deudaTotal();
        }

        private void frmPrestamosEmpleados1_Loaded(object sender, RoutedEventArgs e)
        {
            cmbEmpleado.ItemsSource = MPrestamosEmpleados.GetInstance().ConsultarEmpleado();
            btnAbonos.IsEnabled = false;
            btnGuardar.IsEnabled = false;
        }

        private void cmbEmpleado_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cmbEmpleado.SelectedIndex == 0)
            {
                lblEmpleado.Text = "Seleccione un Empleado";
                btnAbonos.IsEnabled = false;
                btnGuardar.IsEnabled = false;
            }
            else
            {
                Persona item = cmbEmpleado.SelectedItem as Persona;
                lblEmpleado.Text = item.NombrePersona;
                Mostrar();

                if (tblPrestamos.Items.Count == 0)
                {
                    btnAbonos.IsEnabled = false;
                }
                else
                {
                    btnAbonos.IsEnabled = true;
                }

                btnGuardar.IsEnabled = true;

            }
        }

        private void btnBuscarDocumento_Click(object sender, RoutedEventArgs e)
        {

            if (txtBuscarDocumento.Text == string.Empty)
            {
                mensajeError("Debe ingresar un Documento");
            }
            else
            {
                DataGrid list = new DataGrid();
                list.ItemsSource = MPrestamosEmpleados.GetInstance().ConsultarParametroPersona(txtBuscarDocumento.Text);
                list.SelectedIndex = 0;

                if (list.Items.Count == 1)
                {
                    mensajeError("No existe ningún Empleado con este Documento ");
                    list.ItemsSource = null;
                    btnAbonos.IsEnabled = false;
                    btnGuardar.IsEnabled = false;
                }
                else
                {
                    Persona item = list.SelectedItem as Persona;
                    lblEmpleado.Text = item.NombrePersona;
                    list.ItemsSource = null;
                    Mostrar();

                    if (tblPrestamos.Items.Count == 0)
                    {
                        btnAbonos.IsEnabled = false;
                    }
                    else
                    {
                        btnAbonos.IsEnabled = true;
                    }

                    btnGuardar.IsEnabled = true;
                }
            }
        }

        private void btnAbonos_Click(object sender, RoutedEventArgs e)
        {
            FlyoutAbonos.IsOpen = true;
            txtAbono.Text = string.Empty;
        }

        private bool validarCampos()
        {

            if (txtValor.Text == string.Empty || txtDescripcion.Text == string.Empty || dtdFecha.SelectedDate == null)
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

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            string rpta = "";

            if (validarCampos())
            {
                if (cmbEmpleado.IsVisible)
                {
                    rpta = MPrestamosEmpleados.GetInstance().insercionDeudaEmpleado(cmbEmpleado.SelectedValue.ToString(), Convert.ToDecimal(txtValor.Text), Convert.ToDateTime(dtdFecha.SelectedDate), txtDescripcion.Text).ToString();
                    this.ShowMessageAsync("Mensaje", rpta);
                    Limpiar();
                    btnAbonos.IsEnabled = true;
                }
                else
                {
                    rpta = MPrestamosEmpleados.GetInstance().insercionDeudaEmpleado(txtBuscarDocumento.Text, Convert.ToDecimal(txtValor.Text), Convert.ToDateTime(dtdFecha.SelectedDate), txtDescripcion.Text).ToString();
                    this.ShowMessageAsync("Mensaje", rpta);
                    Limpiar();
                    btnAbonos.IsEnabled = true;
                }

            }

            Mostrar();
        }

        private void btnCrearEmpleado_Click(object sender, RoutedEventArgs e)
        {
            Close();
            frmEmpleado miEmpleado = new frmEmpleado();
            miEmpleado.Show();
        }

        private void btnBusqueda1_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = FindResource("cmButton") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }

 

        private void btnNombre_Click(object sender, RoutedEventArgs e)
        {
            txtBuscarDocumento.Visibility = Visibility.Collapsed;
            btnBuscarDocumento.Visibility = Visibility.Collapsed;
            cmbEmpleado.Visibility = Visibility.Visible;
            cmbEmpleado.SelectedIndex = 0;
            txtBuscarDocumento.Text = string.Empty;
            lblEmpleado.Text = "Seleccione un Empleado";
            btnAbonos.IsEnabled = false;
            btnGuardar.IsEnabled = false;
            Limpiar();
        }

        private void btnDocumento_Click(object sender, RoutedEventArgs e)
        {
            txtBuscarDocumento.Visibility = Visibility.Visible;
            btnBuscarDocumento.Visibility = Visibility.Visible;
            cmbEmpleado.Visibility = Visibility.Collapsed;
            cmbEmpleado.SelectedIndex = 0;
            txtBuscarDocumento.Text = string.Empty;
            lblEmpleado.Text = "Seleccione un Empleado";
            btnAbonos.IsEnabled = false;
            btnGuardar.IsEnabled = false;
            Limpiar();
        }

        private void btnConfirmarAbono_Click(object sender, RoutedEventArgs e)
        {
            string rpta = "";

            if (txtAbono.Text != string.Empty)
            {
                decimal valor = Convert.ToDecimal(txtAbono.Text);

                if (Convert.ToInt32(deudaTotal()) < Convert.ToInt32(valor))
                {
                    mensajeError("El valor del Abono no debe superar el total del Prestamo");
                    txtAbono.Text = string.Empty;
                }
                else
                {
                    while (valor != 0)
                    {
                        DateTime newDate = DateTime.Now;
                        int idDeuda = 0;
                        decimal valorTotal = 0;

                        for (int i = 0; i < tblPrestamos.Items.Count; i++)
                        {
                            tblPrestamos.SelectedIndex = i;
                            DeudaPersona item = tblPrestamos.SelectedItem as DeudaPersona;

                            var fecha = item.Fecha;

                            TimeSpan ts = newDate - fecha;

                            if (ts.Days > 0)
                            {
                                newDate = fecha;
                                idDeuda = item.idDeudaPersona;
                                valorTotal = item.Valor;
                            }
                            else if (ts.Days == 0)
                            {
                                newDate = fecha;
                                idDeuda = item.idDeudaPersona;
                                valorTotal = item.Valor;
                            }
                        }

                        if (valor == valorTotal)
                        {
                            rpta = MPrestamosEmpleados.GetInstance().insercionAbonoDeuda(valor, DateTime.Now, idDeuda, 0, 1);
                            valor = 0;
                        }
                        else if (valorTotal > valor)
                        {
                            decimal newValor = valorTotal - valor;
                            rpta = MPrestamosEmpleados.GetInstance().insercionAbonoDeuda(valor, DateTime.Now, idDeuda, newValor, 2);
                            valor = 0;
                        }
                        else if (valorTotal < valor)
                        {
                            rpta = MPrestamosEmpleados.GetInstance().insercionAbonoDeuda(valorTotal, DateTime.Now, idDeuda, 0, 3);
                            valor = valor - valorTotal;
                        }

                        Limpiar();
                        Mostrar();
                    }
                    this.ShowMessageAsync("Mensaje", rpta);

                    if (tblPrestamos.Items.Count == 0)
                    {
                        btnAbonos.IsEnabled = false;
                    }
                }
            }
            else
            {
                mensajeError("Debe ingresar el valor del Abono");
            }
        }
    }
}
