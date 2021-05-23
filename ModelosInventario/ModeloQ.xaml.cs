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
    /// Lógica de interacción para ModeloQ.xaml
    /// </summary>
    public partial class ModeloQ : UserControl
    {
        public ModeloQ()
        {
            InitializeComponent();
        }
        UserControl usc = null;
        UserControl usc2 = null;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Contenedor.Children.Clear();
            usc = new MQSimple();
            Contenedor.Children.Add(usc);

            Contendor2.Children.Clear();
            usc2 = new MQDescuento();
            Contendor2.Children.Add(usc2);
        }
    }

}
