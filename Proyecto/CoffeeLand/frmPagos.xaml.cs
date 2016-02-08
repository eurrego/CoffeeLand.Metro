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
using System.Collections;
using System.Data;

namespace CoffeeLand
{
    /// <summary>
    /// Lógica de interacción para frmPagos.xaml
    /// </summary>
    public partial class frmPagos : MetroWindow
    {



        public frmPagos()
        {
            InitializeComponent();
        }

        private void btnDetallePago_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tblPagos_Loaded(object sender, RoutedEventArgs e)
        {
            tblPagos.ItemsSource = MPagos.GetInstance().ConsultarSalario() as IEnumerable;

        }

        private void frmPagos_Loaded(object sender, RoutedEventArgs e)
        {

        }

      

    }
}
