using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Run_Login(object sender, StartupEventArgs e)
        {

            // evitamos que la aplicación se cierre luego de validar el usuario.
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            // cargamos ventana de inicia y validamos el usuario
            MainWindow inicio = new MainWindow();
            Nullable<bool> resultDialog = inicio.ShowDialog(); // ojo con este codigo.

            if (true == resultDialog.HasValue && true == resultDialog.Value) // si se logra valida se procede a cargar la ventana principal.
            {
                frmPrincipal principal = new frmPrincipal();

                // aqui cambio el procedimiento para cerrar la aplicación al cerrar el MainWindow.
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                principal.Show(); // inicia la aplicación.
            }
        }


    }
}
