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
using Modelo;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmVenta.xaml
    /// </summary>
    public partial class frmVenta : MetroWindow
    {
        public frmVenta()
        {
            InitializeComponent();
        }

        private void frmVenta1_Loaded(object sender, RoutedEventArgs e)
        {
            cmbProveedor.ItemsSource = MVentas.GetInstance().ConsultarProveedor();
            cmbProducto.ItemsSource = MVentas.GetInstance().ConsultarProducto();

        }
    }
}
