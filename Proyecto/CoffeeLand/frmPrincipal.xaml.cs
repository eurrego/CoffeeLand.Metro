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

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmPrincipal.xaml
    /// </summary>
    public partial class frmPrincipal : MetroWindow
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }


        private void btnArboles_Click(object sender, RoutedEventArgs e)
        {
            frmTipoArboles miTipoArboles = new frmTipoArboles();
            miTipoArboles.ShowDialog();
        }

        private void btnGestionTerrenos_Click(object sender, RoutedEventArgs e)
        {
            GestionTerrenos.IsOpen = true;
        }

        private void btnAdministracionEmpleados_Click(object sender, RoutedEventArgs e)
        {
            administracionEmpleados.IsOpen = true;
        }

        private void btnAdministracionCostos_Click(object sender, RoutedEventArgs e)
        {
            administracionCostos.IsOpen = true;
        }

        private void btnAdministracionVentas_Click(object sender, RoutedEventArgs e)
        {
            administracionVentas.IsOpen = true;
        }

        private void btnConceptos_Click(object sender, RoutedEventArgs e)
        {
            frmConceptos miConcepto = new frmConceptos();
            miConcepto.ShowDialog();
        }

        private void btnTipoInsumos_Click(object sender, RoutedEventArgs e)
        {
            frmTipoInsumo MiTipoInsumo = new frmTipoInsumo();
            MiTipoInsumo.ShowDialog();
        }

        private void btnProductos_Click(object sender, RoutedEventArgs e)
        {
            frmProductos miProducto = new frmProductos();
            miProducto.ShowDialog();
        }

        private void btnInsumos_Click(object sender, RoutedEventArgs e)
        {
            frmInsumo miInsumo = new frmInsumo();
            miInsumo.ShowDialog();
        }

        private void btnLabores_Click(object sender, RoutedEventArgs e)
        {
            frmLabores misLabores = new frmLabores();
            misLabores.ShowDialog();
        }

        private void btnEmpleados_Click(object sender, RoutedEventArgs e)
        {
            frmEmpleado miEmpleado = new frmEmpleado();
            miEmpleado.ShowDialog();
        }

        private void btnProveedores_Click(object sender, RoutedEventArgs e)
        {
            frmProveedor miProveedor = new frmProveedor();
            miProveedor.ShowDialog();
        }

        private void btnPrestamos_Click(object sender, RoutedEventArgs e)
        {
            frmPrestamosEmpleados misPrestamos = new frmPrestamosEmpleados();
            misPrestamos.ShowDialog();
        }

        private void btnLotes_Click(object sender, RoutedEventArgs e)
        {
            frmLotes misLotes = new frmLotes();
            misLotes.ShowDialog();
        }
    }
}
