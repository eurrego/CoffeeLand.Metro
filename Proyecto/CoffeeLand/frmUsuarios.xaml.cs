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
    /// Lógica de interacción para frmUsuarios.xaml
    /// </summary>
    public partial class frmUsuarios : MetroWindow
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Loaded(object sender, RoutedEventArgs e)
        {

            List<string> datosPreguntaSeguridad = new List<string>();
            datosPreguntaSeguridad.Add("Seleccione una pregunta de seguridad");
            datosPreguntaSeguridad.Add("¿Cuál es la ciudad de nacimiento de su mamá?");
            datosPreguntaSeguridad.Add("¿Cuál es la fecha de expedición de la cédula?");
            datosPreguntaSeguridad.Add("¿Cuál es el nombre de su primer mascota?");
            datosPreguntaSeguridad.Add("¿Cuál es la marca de su primer auto?");
            datosPreguntaSeguridad.Add("¿Cuál es el nombre de su primer jefe?");
            cmbPreguntaSeguridad.ItemsSource = datosPreguntaSeguridad;

            List<string> datosRol = new List<string>();
            datosRol.Add("Seleccione un Rol");
            datosRol.Add("Administrador");
            datosRol.Add("Empleado");
            cmbRol.ItemsSource = datosRol;

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            if (txtNickname.Text != string.Empty && txtContraseña.Password.ToString() != string.Empty && txtConfirmarContrasena.Password.ToString() != string.Empty && txtRepuestaSeguridad.Text != string.Empty && cmbPreguntaSeguridad.SelectedIndex != 0 && cmbRol.SelectedIndex != 0)
            {



                foreach (var item in MUsuario.GetInstance().InciarSesion(txtNickname.Text))
                {

                    if ( item.Nickname ==  string.Empty)
                    {

                        if (txtContraseña.Password.ToString() == txtConfirmarContrasena.Password.ToString())
                        {

                            string mensaje = MUsuario.GetInstance().GestionUsuario(0, txtNickname.Text, cmbRol.SelectedItem.ToString(), Encriptar(txtContraseña.Password.ToString()), cmbPreguntaSeguridad.SelectedItem.ToString(), txtRepuestaSeguridad.Text, 1);
                            mensajeError(mensaje);

                            LimpiarCampos();
                        }
                        else
                        {
                            mensajeError("Las contraseñas no coinsiden");
                        }

                    }
                    else
                    {
                        mensajeError("Este usuario ya se encuentra regitrado");
                    }

                }
            }
            else
            {
                mensajeError("Debe dijitar todos los campos");
            }  

        }

        private async void mensajeError(string mensaje)
        {
            await this.ShowMessageAsync("Error", mensaje);
        }

        public string Encriptar( string cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public void LimpiarCampos()
        {

            txtNickname.Text = string.Empty;
            txtContraseña.Password = string.Empty;
            txtRepuestaSeguridad.Text = string.Empty;
            txtConfirmarContrasena.Password = string.Empty;
            cmbPreguntaSeguridad.SelectedIndex = 0;
            cmbRol.SelectedIndex = 0;

        }

        //public  string DesEncriptar( string cadenaAdesencriptar)
        //{
        //    string result = string.Empty;
        //    byte[] decryted = Convert.FromBase64String(cadenaAdesencriptar);
        //    //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
        //    result = System.Text.Encoding.Unicode.GetString(decryted);
        //    return result;
        //}
    }
}
