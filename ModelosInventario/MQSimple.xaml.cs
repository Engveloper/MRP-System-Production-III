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
    /// Lógica de interacción para MQSimple.xaml
    /// </summary>
    public partial class MQSimple : UserControl
    {
        public MQSimple()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, RoutedEventArgs e)
        {
            lblR.Visibility = Visibility.Visible;
            txtResultados.Visibility = Visibility.Visible;
            txtResultados.Text = "La cantidad optima a pedir es de " + QOPT() + "\n \nEl número de pedidos ha hacerse al año sería de "
                + NPedidos() + "\n \nEl costo de anual sería de " + CostoTotal() + "\n \nSe ordenará cada " + Math.Truncate( 365 / NPedidos()) + 
                " días\n\nO cuando solo se tengan " +ROP()+" unidades en el inventario";
        }

        private int QOPT()
        {
            int eoq = (int)Math.Round(Math.Sqrt((Double)(2 * CalcularDemanda() * Convert.ToDecimal(txtCostoPedir.Text)) / (Double)CalcularCostoMantenerAnual()));
            return eoq;
        }

        private decimal NPedidos()
        {

            decimal pedidos = CalcularDemanda() / QOPT();
                pedidos = Math.Truncate( pedidos);
            return pedidos;
        }

        private decimal CostoTotal()
        {
            decimal ct;
            int q = QOPT();
            int D = CalcularDemanda();
            decimal h = CalcularCostoMantenerAnual();
            decimal s = Convert.ToDecimal(txtCostoPedir.Text);
            decimal c = Convert.ToDecimal(txtCostoProducto.Text);

            ct = Math.Round( (D * s / q) + (q * h / 2) + (D * c),2);
            return ct;
        }

        private decimal ROP()
        {
            decimal a = Convert.ToDecimal( CalcularDemanda()) ;
            decimal b = Convert.ToDecimal(txtPlazo.Text);
            decimal ROP = ( a/365) *b ;
           
            ROP =  Math.Round( ROP);
            return ROP;
        }
        private decimal CalcularCostoMantenerAnual()
        {
            decimal c = 0;
            c = Convert.ToDecimal(txtCostoMan.Text);
            decimal costop = Convert.ToDecimal(txtCostoProducto.Text);

            switch (cmbCMant.SelectedIndex)
            {
                case 0:
                    break;

                case 1:
                    c = c * 12;
                    break;

                case 2:
                    c = c * 52;
                    break;

                case 3:
                    c = (c / 100) * costop;
                    break;
            }
            return c;
        }

        private int CalcularDemanda()
        {
            int d = Convert.ToInt32(txtDemanda.Text);
            switch (cmbDemanda.SelectedIndex)
            {
                case 0:
                    break;

                case 1:
                    d = d * 12;
                    break;

                case 2:
                    d = d * 52;
                    break;

                case 3:
                    d = d * 365;
                    break;
            }

            return d;
        }
    }
}
