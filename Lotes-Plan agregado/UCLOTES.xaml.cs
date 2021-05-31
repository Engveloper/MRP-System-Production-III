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
    /// Lógica de interacción para UC_LOTES.xaml
    /// </summary>
    public partial class UCLOTES : UserControl
    {
        public UCLOTES()
        {
            InitializeComponent();
        }
        



        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (txtnn.Text.Equals(""))
            {
                MessageBox.Show( "Debe ingresar la cantidad", "MENSAJE DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                try
                {
                    int id = dgNecesidades.Items.Count;
                    int cantidad = Convert.ToInt32(txtnn.Text);
                    var nece = new NECESIDADES { Id = id + 1, Necesidades = cantidad };

                    dgNecesidades.Items.Add(nece);

                    txtnn.Text = "";
                    txtnn.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show( "Debe ingresar un numero", "MENSAJE DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnCalcular_Click(object sender, RoutedEventArgs e)
        {
            if (txtcm.Text.Equals(""))
            {
                MessageBox.Show( "Debe ingresar el costo del producto", "MENSAJE DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (txtcp.Text.Equals(""))
            {
                MessageBox.Show("Debe ingresar el costo de pedir", "MENSAJE DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (txtcma.Text.Equals(""))
            {
                MessageBox.Show( "Debe ingresar el costo de pedir", "MENSAJE DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (dgNecesidades.Items.Count == 0)
            {
                MessageBox.Show("Debe ingresar al menos una cantidad", "MENSAJE DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (Validar())
            {
                //stkpTablas.Children.Clear();
                l4l.ItemsSource = null;
                EOQ.ItemsSource = null;
                LTC.ItemsSource = null;
                LUC.ItemsSource = null;

                CalcularL4L();
                CalcularOtros();
               AgregarTablas();
            }
        }

        private void CalcularL4L()
        {

            decimal S = Convert.ToDecimal(txtcp.Text);

            List<CT> material = new List<CT>();
            decimal ct = 0;
            foreach (NECESIDADES det in dgNecesidades.Items)
            {

                int neces = det.Necesidades;
                int prod = neces;
                int Inv = 0;
                int H = 0;
                ct = ct + S;

                var cot = new CT
                {
                    Semana = det.Id,
                    Necesidades = neces,
                    ProduccionRequerida = prod,
                    InventarioFinal = Inv,
                    CostoMantener = H,
                    CostoPedir = S,
                    CostoTotal = ct
                };

                material.Add(cot);
            }

         
           l4l.ItemsSource = material;

        }

        private void CalcularOtros()
        {

            decimal H = CalcularCostoMantenerAnual();
            decimal C = Convert.ToDecimal(txtcm.Text);
            decimal S = Convert.ToDecimal(txtcp.Text);

            //Calcular EOQ

            decimal D = 0;

            foreach (NECESIDADES det in dgNecesidades.Items)
            {
                D = D + det.Necesidades;
            }
            D = Math.Round(D / dgNecesidades.Items.Count) * 52;
            int eoq = (int)Math.Round(Math.Sqrt((Double)(2 * S * D) / (Double)H));
            EOQ.ItemsSource = Calcular(H, C, S, eoq);

            //Calcular LTC
            decimal cm = H / 52;
         //   MessageBox.Show("H" + H + " Y CM" + cm);
            LTC.ItemsSource = Calcular(H, C, S, CalcularProd(0, S, cm));
            //Calcular LUC
            LUC.ItemsSource = Calcular(H, C, S, CalcularProd(1, S, cm));

        }
        private List<CT> Calcular(Decimal H, Decimal C, Decimal S, int QO)
        {
            List<CT> material = new List<CT>();

            decimal ct = 0;
            int i = 0;
            int prod = 0;
            int Inv = 0;
            decimal Cman = 0;
            decimal Cped = 0;
            int invI = 0;
            //int IIT = 0;

            if (!txtii.Text.Equals(""))
            {
                invI = Convert.ToInt32(txtii.Text);
            }
            foreach (NECESIDADES det in dgNecesidades.Items)
            {
                int neces = det.Necesidades;

                if (i == 0)
                {
                    prod = QO;
                    Inv = invI + QO - neces;
                    Cped = S;
                }
                else
                {
                    if (Inv - neces >= 0)
                    {
                        prod = 0;
                        Inv = Inv - neces;
                        Cped = 0;
                    }
                    else
                    {
                        prod = QO;
                        Inv = Inv + prod - neces;
                        Cped = S;
                    }
                }

                ///Ojo
                ///
                if (Inv < 0)
                {
                    Cman = 0;
                }
                else { 
                Cman = Math.Round(Inv * (H / 52), 2);
                }

                ct = ct + Cman + Cped;
                i++;

                var cot = new CT
                {
                    Semana = det.Id,
                    Necesidades = neces,
                    InventarioInicial = invI,
                    ProduccionRequerida = prod,
                    InventarioFinal = Inv,
                    CostoMantener = Cman,
                    CostoPedir = Cped,
                    CostoTotal = ct
                };

                invI = Inv;
                material.Add(cot);
            }


            return material;
        }


        private decimal CalcularCostoMantenerAnual()
        {
            decimal c = 0;
            c = Convert.ToDecimal(txtcma.Text);
            decimal costop = Convert.ToDecimal(txtcm.Text);

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

                    c = (c / 100) * 52 * costop;
                    break;
            }
            return c;

        }

        private int CalcularProd(int a, decimal S, decimal cm)
        {
            int eoq = 0;
            decimal luc = 0;
            //int i = 0;
            int prod = 0;
            decimal Cman = 0;
            decimal Cped = 0;


            List<TExtra> material = new List<TExtra>();

            foreach (NECESIDADES nece in dgNecesidades.Items)
            {
                prod = nece.Necesidades + prod;
                Cman = (nece.Necesidades * cm * (nece.Id - 1)) + Cman;
                Cped = S;
                luc = (Cped + Cman) / prod;

                var cot = new TExtra
                {
                    LUC = luc,
                    Necesidades = prod,
                    CostoMantener = Cman,
                    CostoPedir = Cped,
                };

                material.Add(cot);
            }

            int i = 0;
            decimal diferencia = 0;
            foreach (TExtra extra in material)
            {

                if (a == 0)
                {
                    if (i == 0)
                    {
                        diferencia = Math.Abs(extra.CostoPedir - extra.CostoMantener);
                        eoq = extra.Necesidades;
                    }
                    else if (Math.Abs(extra.CostoPedir - extra.CostoMantener) < diferencia)
                    {
                        eoq = extra.Necesidades;
                        diferencia = Math.Abs(extra.CostoPedir - extra.CostoMantener);

                    }
                }
                else if (a == 1)
                {
                    if (i == 0)
                    {
                        diferencia = extra.LUC;
                        eoq = extra.Necesidades;
                    }
                    else if (extra.LUC < diferencia)
                    {
                        eoq = extra.Necesidades;
                        diferencia = extra.LUC;
                    }
                }

                i++;
            }

            return eoq;
        }
        private Boolean Validar()
        {
            Boolean bandera = false;
            try
            {
                Convert.ToDecimal(txtcma.Text);
                Convert.ToDecimal(txtcm.Text);
                Convert.ToDecimal(txtcp.Text);
                bandera = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show( "El costos deben ser un número", "MENSAJE DEL SISTEMA", MessageBoxButton.OK, MessageBoxImage.Warning);
                return bandera;
            }


            return bandera;
        }

        public static void soloNumeros(KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9
                || e.Key == Key.OemComma || e.Key == Key.OemPeriod)
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }
        private void AgregarTablas()
        {
            /*Label l = new Label();
            l.FontSize = 40;
            l.Content = "L4L";
            l.Height = 50;
            l.Width = 100;

            l4l.Height = 100;
            l4l.Width = 600;
            // l4l.AutoGenerateColumns = false;
            l4l.CanUserReorderColumns = false;
            l4l.CanUserResizeColumns = false;
            l4l.CanUserSortColumns = false;
            l4l.IsReadOnly = true;
            l4l.CanUserAddRows = false;
            l4l.RowHeight = 40;
            l4l.CanUserDeleteRows = false;
            l4l.CanUserResizeRows = false;

            Label l2 = new Label();
            l2.FontSize = 40;
            l2.Content = "EOQ";
            l2.Height = 50;
            l2.Width = 100;

            EOQ.Height = 100;
            EOQ.Width = 600;
            EOQ.CanUserReorderColumns = false;
            EOQ.IsReadOnly = true;
            EOQ.CanUserResizeColumns = false;
            EOQ.CanUserAddRows = false;
            EOQ.RowHeight = 40;
            EOQ.CanUserDeleteRows = false;
            EOQ.CanUserResizeRows = false;
            EOQ.CanUserSortColumns = false;

            Label l3 = new Label();
            l3.FontSize = 40;
            l3.Content = "LTC";
            l3.Height = 50;
            l3.Width = 100;

            LTC.Height = 100;
            LTC.Width = 600;
            LTC.CanUserReorderColumns = false;
            LTC.IsReadOnly = true;
            LTC.CanUserResizeColumns = false;
            LTC.CanUserAddRows = false;
            LTC.RowHeight = 40;
            LTC.CanUserDeleteRows = false;
            LTC.CanUserResizeRows = false;
            LTC.CanUserSortColumns = false;

            Label l4 = new Label();
            l4.FontSize = 40;
            l4.Content = "LUC";
            l4.Height = 50;
            l4.Width = 100;

            LUC.Height = 100;
            LUC.Width = 600;
            LUC.CanUserReorderColumns = false;
            LUC.IsReadOnly = true;
            LUC.CanUserResizeColumns = false;
            LUC.CanUserAddRows = false;
            LUC.RowHeight = 40;
            LUC.CanUserDeleteRows = false;
            LUC.CanUserResizeRows = false;
            LUC.CanUserSortColumns = false;



            stkpTablas.Children.Add(l);
            stkpTablas.Children.Add(l4l);
            stkpTablas.Children.Add(l2);
            stkpTablas.Children.Add(EOQ);
            stkpTablas.Children.Add(l3);
            stkpTablas.Children.Add(LTC);
            stkpTablas.Children.Add(l4);
            stkpTablas.Children.Add(LUC);*/

            lblEOQ.Visibility = Visibility.Visible;
            lblL4L.Visibility = Visibility.Visible;
            lblLUC.Visibility = Visibility.Visible;
            lclLTC.Visibility = Visibility.Visible;
            l4l.Visibility = Visibility.Visible;
            EOQ.Visibility = Visibility.Visible;
            LTC.Visibility = Visibility.Visible;
            LUC.Visibility = Visibility.Visible;
        }

        private void txtcm_KeyDown(object sender, KeyEventArgs e)
        {
            soloNumeros(e);
        }

        private void txtcp_KeyDown(object sender, KeyEventArgs e)
        {
            soloNumeros(e);
        }

        private void txtcma_KeyDown(object sender, KeyEventArgs e)
        {
            soloNumeros(e);
        }

        private void txtnn_KeyDown(object sender, KeyEventArgs e)
        {
            soloNumeros(e);
        }

        private void txtii_KeyDown(object sender, KeyEventArgs e)
        {
            soloNumeros(e);
        }
    }

    public class CT
    {
        public int Semana { get; set; }
        public int Necesidades { get; set; }
        public int InventarioInicial { get; set; }
        public int ProduccionRequerida { get; set; }
        public int InventarioFinal { get; set; }
        public Decimal CostoMantener { get; set; }
        public Decimal CostoPedir { get; set; }
        public Decimal CostoTotal { get; set; }
    }

    public class NECESIDADES
    {
        public int Id { get; set; }
        public int Necesidades { get; set; }
    }

    public class TExtra
    {
        public int Necesidades { get; set; }
        public Decimal CostoMantener { get; set; }
        public Decimal CostoPedir { get; set; }
        public Decimal LUC { get; set; }
    }
}
