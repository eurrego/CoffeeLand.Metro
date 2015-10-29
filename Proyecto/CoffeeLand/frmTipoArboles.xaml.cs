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
            lblTotal.Content = tblTipoArbol.Items.Count.ToString(); ;
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
            EstilosCeldas();
        }


        //Método para Buscar por nombre
        private void BuscarNombre()
        {
            tblTipoArbol.ItemsSource = MTipoArbol.GetInstance().buscarTipoArbol(txtBuscarNombre.Text);
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
                lblEstado.Content = "REGISTRAR TIPOS DE ARBOLES";
                btnGuardar.Margin = new Thickness(667, 36, 0, 0);
                btnCancelar.Visibility = Visibility.Hidden;
                gridConsultar.IsEnabled = true;
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

            lblEstado.Content = "MODIFICAR TIPOS DE ARBOLES";
            btnGuardar.Margin =new Thickness(556,36,0,0);
            btnCancelar.Visibility = Visibility.Visible;
            gridConsultar.IsEnabled = false;
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

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            lblEstado.Content = "REGISTRAR TIPOS DE ARBOLES";
            btnGuardar.Margin = new Thickness(667, 36, 0, 0);
            btnCancelar.Visibility = Visibility.Hidden;
            gridConsultar.IsEnabled = true;
        }

        private async void btnInhabilitar_Click(object sender, RoutedEventArgs e)
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
            

            TextBox text2 = (TextBox)template.FindName("txtDescripcion", dgdPresenter);
            string descripcion = text2.Text;
           

            TextBox text3 = (TextBox)template.FindName("txtId", dgdPresenter);
            string id = text3.Text;
        

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                NegativeButtonText = "Cancelar",
                ColorScheme = MetroDialogOptions.ColorScheme
            };

            MessageDialogResult result = await this.ShowMessageAsync("CoffeeLand", "¿Realmente desea Inhabilitar el Registro?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result != MessageDialogResult.Negative)
            {
                byte idTipoArbol;
                string rpta = "";

                idTipoArbol = Convert.ToByte(id);

                rpta = MTipoArbol.GetInstance().registrarTipoArbol(nombre, descripcion, idTipoArbol, 3).ToString();
                await this.ShowMessageAsync("CoffeeLand",rpta);
                Mostrar();
            }
                
            }
        }
    }
