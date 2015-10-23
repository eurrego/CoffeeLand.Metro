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

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            menu.IsOpen = true;
        }
    }
}
