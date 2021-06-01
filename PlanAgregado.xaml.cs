using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace LoDeProduccion
{
    /// <summary>
    /// Interaction logic for PlanAgregado.xaml
    /// </summary>
    public partial class PlanAgregado : UserControl
    {
        public PAddedModel pamodel;
        public ObservableCollection<PAddedVariance> demandasDias;
        public bool boolss { get; set; }
        public PlanAgregado()
        {
            InitializeComponent();
            demandasDias = new ObservableCollection<PAddedVariance>();
            dgDemandasDias.ItemsSource = demandasDias;
        }

        private void AgregarMes_Click(object sender, RoutedEventArgs e)
        {
            pamodel = new();
            pamodel.MateriaPrima = double.Parse(ingMateriaPrima.Text);
            pamodel.H = double.Parse(txtH.Text);
            pamodel.CostoDeFaltante = double.Parse(txtCostoDeFaltante.Text);
            pamodel.Outsourcing = 0;
            pamodel.CostoCapacitar = double.Parse(ingContratar.Text);
            pamodel.CostoDespedir = double.Parse(ingDespido.Text);
            pamodel.HoraExtra = double.Parse(ingHoraExtra.Text);
            pamodel.InventarioInicial = int.Parse(ingInventarioInicial.Text);
            if (txtInventarioDeSeguridad.Text == string.Empty)
            {
                pamodel.InventarioDeSeguridad = 0;
            }
            else pamodel.InventarioDeSeguridad = int.Parse(txtInventarioDeSeguridad.Text);
            pamodel.FuerzaLaboralInicial = int.Parse(ingFuerzaLaboralInicial.Text);
            pamodel.HorasRequeridaParaUnidad = double.Parse(txtHorasParaUnidad.Text);
            pamodel.HorasPorDia = double.Parse(ingHorasJornada.Text);
            pamodel.HoraNormal = double.Parse(ingHoraNormal.Text);
            

            double? percentage = null;
            if(boolss)percentage = double.Parse(txtSSPercentage.Text);

            PlanAgregadoTablas tablas = new(pamodel, demandasDias, percentage);
            tablas.Show();
        }


        private void btnAgregarFila_Click(object sender, RoutedEventArgs e)
        {
            demandasDias.Add(new PAddedVariance());
        }

        private void chkSS_Checked(object sender, RoutedEventArgs e)
        {
            boolss = true;
            txtInventarioDeSeguridad.IsEnabled = false;
        }

        private void chkSS_Unchecked(object sender, RoutedEventArgs e)
        {
            boolss = true;
            txtInventarioDeSeguridad.IsEnabled = true;
        }
    }
}

