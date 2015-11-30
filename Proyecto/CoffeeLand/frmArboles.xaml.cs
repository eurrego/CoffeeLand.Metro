﻿using System;
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


        // Validación de campos
        private bool validarCampos()
        {

            if (cmbLote.SelectedIndex == 0 || txtCantidad.Text == string.Empty || dtdFecha.SelectedDate == null)
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
            cmbTipoArbol.SelectedIndex = 0;
            txtCantidad.Text = string.Empty;
            dtdFecha.SelectedDate = null;
        }

        //mostrar
        private void Mostrar()
        {
            tblArboles.ItemsSource = MArbol.GetInstance().ConsultarArboles(Convert.ToInt32(cmbLote.SelectedValue)) as IEnumerable;
            cmbTipoArbol.SelectedValue = 0;
        }

        // define el total de arboles
        private void cantidadTotal()
        {
            lblTotal.Text = MArbol.GetInstance().ConsultarCantidad(Convert.ToInt32(cmbLote.SelectedValue)).ToString();
        }


        private void frmArboles1_Loaded(object sender, RoutedEventArgs e)
        {
            cmbTipoArbol.ItemsSource = MArbol.GetInstance().ConsultarTipoArbol();
            cmbLote.ItemsSource = MArbol.GetInstance().ConsultarLote();
            cmbLote.SelectedValue = lote;
            Mostrar();
        }

        private void cmbLote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbLote.SelectedIndex == 0)
            {
                lblLote.Text = "Seleccione un Lote";
            }
            else
            {
                Lote item = cmbLote.SelectedItem as Lote;
                lblLote.Text = item.NombreLote;
                Mostrar();
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string rpta = "";

            if (validarCampos())
            {
                    rpta = MArbol.GetInstance().gestionArboles(Convert.ToInt16(cmbLote.SelectedValue),Convert.ToByte(cmbTipoArbol.SelectedValue), Convert.ToInt32(txtCantidad.Text) , Convert.ToDateTime(dtdFecha.SelectedDate)).ToString();
                    this.ShowMessageAsync("Mensaje", rpta);
                    Limpiar();
                    
            }

            Mostrar();
        }

        private void btnDetalle_Click(object sender, RoutedEventArgs e)
        {
 
 

            lblLoteConsultar.Text = lblLote.Text;
            //lblArbol.Text = item.Cantidad.ToString();

            //tblMovimientosArboles.ItemsSource = MArbol.GetInstance().ConsultarMovimiento();
            gridConsultar.Focus();

        }
    }
}
