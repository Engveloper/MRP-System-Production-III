using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoDeProduccion
{
    /// <summary>
    /// Lógica de interacción para MQDescuento.xaml
    /// </summary>
    public partial class MQDescuento : UserControl
    {
        public MQDescuento()
        {
            InitializeComponent();
        }

       

        

        public static void soloNumeros(KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void txtCostoProducto_KeyDown(object sender, KeyEventArgs e)
        {
            soloNumeros(e);
        }
    }
}
