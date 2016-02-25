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
using MahApps.Metro.Controls.Dialogs;
using Modelo;
using MahApps.Metro.Controls;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmRecuperarContrasena.xaml
    /// </summary>
    public partial class frmRecuperarContrasena : MetroWindow
    {
        private IEnumerable<Usuario> usuario { get; set; }

        public frmRecuperarContrasena()
        {

            InitializeComponent();
        }

        public frmRecuperarContrasena(IEnumerable<Usuario> usu)
        {
            InitializeComponent();
            usuario = usu;


        }

        private void frmRecuperarContrasena_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> datosPreguntaSeguridad = new List<string>();
            datosPreguntaSeguridad.Add("Seleccione una pregunta de seguridad");
            datosPreguntaSeguridad.Add("¿Cuál es la ciudad de nacimiento de su mamá?");
            datosPreguntaSeguridad.Add("¿Cuál es la fecha de expedición de la cédula?");
            datosPreguntaSeguridad.Add("¿Cuál es el nombre de su primer mascota?");
            datosPreguntaSeguridad.Add("¿Cuál es la marca de su primer auto?");
            datosPreguntaSeguridad.Add("¿Cuál es el nombre de su primer jefe?");
            cmbPreguntaSeguridad.ItemsSource = datosPreguntaSeguridad;

        }

        private void btnRecuperar_Click(object sender, RoutedEventArgs e)
        {

            foreach (var item in usuario)
            {
                if (item.PreguntaSeguridad.Equals(cmbPreguntaSeguridad.SelectedItem.ToString()))
                {
                    if (item.Respuesta.Equals(txtRepuestaSeguridad.Text))
                    {
                        if (txtContraseña.Password.ToString().Equals(txtConfirmarContrasena.Password.ToString()))
                        {

                            mensajeError(MUsuario.GetInstance().GestionUsuario(item.idUsuario, string.Empty, string.Empty, Encriptar(txtContraseña.Password.ToString()), string.Empty, string.Empty, 5));
                            this.Close();

                            MainWindow main = new MainWindow();
                            main.ShowDialog();
                        }
                        else
                        {
                            mensajeError("Las contraseñas no coinciden");
                        }
                    }
                    else
                    {
                        mensajeError("Pregunta y/o Respuest de seugridad incorrectas");
                    }
                }
                else
                {
                    mensajeError("Pregunta y/o Respuest de seugridad incorrectas");
                }

            }

        }

        private async void mensajeError(string mensaje)
        {
            await this.ShowMessageAsync("Error", mensaje);
        }

        public string Encriptar(string cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }




    }
}
