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
            if (txtCostoProducto.Text.Equals("") || txtU1.Text.Equals("") || txtU2.Text.Equals(""))
            {
                MessageBox.Show("Debe llenar todos los campos", "MENSAJE DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                try
                {
                    int id = dgProveedores.Items.Count;
                    decimal costo = Convert.ToDecimal(txtCostoProducto.Text);
                    int u1 = Convert.ToInt32(txtU1.Text);
                    int u2 = Convert.ToInt32(txtU2.Text);
                    var nece = new Proveedores { Id = id + 1, Precio = costo, Cantidad1 = u1, Cantidad2 = u2 };

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
            if (txtCostoMan.Text.Equals("") || txtCostoPedir.Text.Equals("") || txtDemanda.Text.Equals("") || txtPlazo.Text.Equals("") ||dgProveedores.Items.Count==0)
            {
                MessageBox.Show("Debe llenar todos los campos", "MENSAJE DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                txtResultados.Text = "";
            }
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

        private void txtPlazo_KeyDown(object sender, KeyEventArgs e)
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

        
    }

    public class Proveedores
    {
        public int Id { get; set; }
        public decimal Precio{ get; set; }
        public int Cantidad1 { get; set; }
        public int Cantidad2 { get; set; }
    }
}
