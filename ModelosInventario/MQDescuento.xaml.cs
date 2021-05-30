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

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if ((txtCosto.Text.Equals("") && CMB.SelectedIndex==0)|| txtU1.Text.Equals("") || txtU2.Text.Equals("")|| txtCostoProducto.Text.Equals(""))
            {
                MessageBox.Show("Debe llenar todos los campos", "MENSAJE DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                try
                {
                    int id = dgProveedores.Items.Count;
                    decimal costo = 0;
                    int u1 = Convert.ToInt32(txtU1.Text);
                    int u2 = Convert.ToInt32(txtU2.Text);
                    decimal desc = 0;

                    switch (CMB.SelectedIndex)
                    {
                        case 0:
                            desc = Convert.ToDecimal(txtCostoProducto.Text);
                            costo = Convert.ToDecimal(txtCosto.Text) * (1-(desc/100));
                            break;
                        case 1:
                            costo = Convert.ToDecimal(txtCostoProducto.Text);
                            
                            break;
                    }
                    var nece = new Proveedores { Id = id + 1, Descuento = desc, Precio = costo, Cantidad1 = u1, Cantidad2 = u2 };

                    dgProveedores.Items.Add(nece);

                    txtCostoProducto.Text = "";
                    txtU1.Text = "";
                    txtU2.Text = "";
                    txtCostoProducto.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Debe ingresar un número", "MENSAJE DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnCalcular_Click(object sender, RoutedEventArgs e)
        {
            if (txtCostoMan.Text.Equals("") || txtCostoPedir.Text.Equals("") || txtDemanda.Text.Equals("") ||dgProveedores.Items.Count==0)
            {
                MessageBox.Show("Debe llenar todos los campos", "MENSAJE DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                int demanda = CalcularDemanda();
                Decimal costopedido = Convert.ToDecimal(txtCostoPedir.Text);
                Decimal tasas = 0;
                Decimal costoman = CalcularCostoMantenerAnual();
                txtResultados.Text = "";
                String resul;

                foreach (Proveedores P in dgProveedores.Items)
                {
                    int cantidad = 0;
                    tasas = tasa(P.Precio);
                    cantidad = CalcularQ(demanda, costopedido, tasas, costoman);
                    P.CantOptima = cantidad;
                    Decimal costoT;
                    resul = "Opción " + P.Id + ": Para el lote de entre " + P.Cantidad1 + " y " + P.Cantidad2 + ", \n con un precio de " +
                        P.Precio + ", \n la cantidad óptima a pedir es " + cantidad;

                    if (P.Cantidad1 == 0)
                    {
                        if ( cantidad > P.Cantidad2)
                        {
                            costoT = CantTotal(demanda, costopedido, P.Cantidad2, costoman, tasas, P.Precio);
                            resul = resul + ", \n esta no cumple la condición del proveedor \n por lo que se probará con la cantidad " +
                                "máxima para este lote " + P.Cantidad2 + "\n con el cual tendría un costo total de " + costoT + "\n\n";
                        }
                        else {
                            costoT = CantTotal(demanda, costopedido, cantidad, costoman, tasas, P.Precio);
                            resul = resul + "\n y tiene un costo total de " + costoT + "\n\n";
                        }
                        
                    }
                    else
                    {

                        if (cantidad < P.Cantidad1|| cantidad>P.Cantidad2)
                        {
                            costoT = CantTotal(demanda, costopedido, P.Cantidad1, costoman, tasas, P.Precio);
                            resul = resul + ", \n esta no cumple la condición del proveedor \n por lo que se probará con la cantidad " +
                                "mínima para este lote " + P.Cantidad1 + "\n con el cual tendría un costo total de " + costoT + "\n\n";
                        }
                        else
                        {
                            costoT = CantTotal(demanda, costopedido, cantidad, costoman, tasas, P.Precio);
                            resul = resul + "\n y tiene un costo total de " + costoT + "\n\n";
                        }
                    }

                    P.CostoTotal = costoT;

                    txtResultados.Text = txtResultados.Text + resul;

                }
            }

            lblR.Visibility = Visibility.Visible;
            txtResultados.Visibility = Visibility.Visible;
        }

        public int CalcularQ(int demanda, Decimal cp, Decimal tasa, Decimal cm)
        {
            int Q=0;
            Q = (int)Math.Round(Math.Sqrt((Double)(2 * demanda * cp )/ (Double)(tasa + cm)));
            return Q;
        }

        public Decimal CantTotal(int D,Decimal s, int q, decimal h, decimal h2, decimal c)
        {
            Decimal ct;

            ct =  Math.Round((D * s / q) + (q * (h + h2) / 2) + (D * c), 2);
            return ct;

        }

        private decimal CalcularCostoMantenerAnual()
        {
            decimal c = 0;
            c = Convert.ToDecimal(txtCostoMan.Text);


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


            }
            return c;
        }

        public decimal tasa(Decimal costop)
        {
            decimal c = 0;
            if (!txtTasa.Text.Equals(""))
            {
                c = Convert.ToDecimal(txtTasa.Text);

                c = (c / 100) * costop;
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
        public static void soloNumeros(KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 
                || e.Key == Key.OemComma|| e.Key == Key.OemPeriod)
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

        private void txtDemanda_KeyDown(object sender, KeyEventArgs e)
        {
            soloNumeros(e);
        }

        private void txtCostoPedir_KeyDown(object sender, KeyEventArgs e)
        {
            soloNumeros(e);
        }

        private void txtCostoMan_KeyDown(object sender, KeyEventArgs e)
        {
            soloNumeros(e);
        }

       

        private void txtU1_KeyDown(object sender, KeyEventArgs e)
        {
            soloNumeros(e);
        }

        private void txtU2_KeyDown(object sender, KeyEventArgs e)
        {
            soloNumeros(e);
        }

        private void txtTasa_KeyDown(object sender, KeyEventArgs e)
        {
            soloNumeros(e);
        }

        private void txtPlazo_KeyDown(object sender, KeyEventArgs e)
        {
            soloNumeros(e);
        }

        private void txtDias_KeyDown(object sender, KeyEventArgs e)
        {
            soloNumeros(e);
        }

        private void txtCosto_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }

    public class Proveedores
    {
        public int Id { get; set; }
        public decimal Descuento { get; set; }
        public decimal Precio{ get; set; }
        public int Cantidad1 { get; set; }
        public int Cantidad2 { get; set; }
        public int CantOptima { get; set; }
        public decimal CostoTotal { get; set; }
    }
}
