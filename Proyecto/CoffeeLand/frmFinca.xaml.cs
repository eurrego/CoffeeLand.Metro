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

using Modelo;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmFinca.xaml
    /// </summary>
    public partial class frmFinca : MetroWindow
    {
        public frmFinca()
        {
            InitializeComponent();
        }

        private void frmFinca1_Loaded(object sender, RoutedEventArgs e)
        {

            cmbDepartamento.ItemsSource = MFinca.GetInstance().ConsultarDepartamento();
            var municipios = MFinca.GetInstance().ConsultarMunicipios();

            

            var finca = MFinca.GetInstance().ConsultarFinca();

            foreach (var item in finca)
            {
                txtNombre.Text = item.NombreFinca.ToString();
                txtPropietario.Text = item.Propietario.ToString();
                txtTelefono.Text = item.Telefono.ToString();
                txtVereda.Text = item.Vereda.ToString();
                txtHectareas.Text = item.Hectareas.ToString();
                cmbMunicipio.SelectedValue = item.idMunicipio;

                //foreach (var item2 in municipios)
                //     {
                //         if (int.Parse(item2.idMunicipio.ToString()) == int.Parse(item.idMunicipio.ToString()))
                //         {
                //             cmbDepartamento.SelectedValue = item2.idDepartamento;
                //             break;

                //         }
                //     }

                
            }





            }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {


            if (txtHectareas.Text == string.Empty || txtNombre.Text == string.Empty || txtPropietario.Text == string.Empty ||txtTelefono.Text == string.Empty ||txtVereda.Text == string.Empty || cmbDepartamento.SelectedIndex == 0 || cmbMunicipio.SelectedIndex == 0)
            {


                mensajeError("Debe digitar todos los campos");

            }

            else
            {

                try
                {
                    MFinca.GetInstance().modificarFinca(txtNombre.Text, txtPropietario.Text, int.Parse(cmbMunicipio.SelectedValue.ToString()), txtVereda.Text, txtTelefono.Text, txtHectareas.Text);


                }
                catch (Exception)
                {


                    throw;
                }

            }



        }

        private void cmbDepartamento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


             cmbMunicipio.ItemsSource = MFinca.GetInstance().ConsultarMunicipios(int.Parse(cmbDepartamento.SelectedValue.ToString()));
        }


        // mensaje de Error
        private async void mensajeError(string mensaje)
        {
            await this.ShowMessageAsync("Error", mensaje);
        }
    }
}
