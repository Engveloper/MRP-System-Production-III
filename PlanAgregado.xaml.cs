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
        public PlanAgregado()
        {
            InitializeComponent();
            demandasDias = new ObservableCollection<PAddedVariance>();
            dgDemandasDias.ItemsSource = demandasDias;
        }

        private void AgregarMes_Click(object sender, RoutedEventArgs e)
        {
            pamodel = new() 
            {
                DiasHabiles = int.Parse(ingDiasLaborales.Text),
                MateriaPrima = double.Parse(ingMateriaPrima.Text),
                H = double.Parse(txtH.Text),
                CostoDeFaltante = double.Parse(txtCostoDeFaltante.Text),
                Outsourcing = 0,
                CostoCapacitar = double.Parse(ingContratar.Text),
                CostoDespedir = double.Parse(ingDespido.Text),
                HoraExtra = double.Parse(ingHoraExtra.Text),
                InventarioInicial = int.Parse(ingInventarioInicial.Text),
                InventarioDeSeguridad = int.Parse(txtInventarioDeSeguridad.Text),
                FuerzaLaboralInicial = int.Parse(ingFuerzaLaboralInicial.Text),
                HorasRequeridaParaUnidad = double.Parse(txtHorasParaUnidad.Text)
            };

            PlanAgregadoTablas tablas = new(pamodel, demandasDias);
            tablas.Show();
        }


        private void btnAgregarFila_Click(object sender, RoutedEventArgs e)
        {
            demandasDias.Add(new PAddedVariance());
        }
    }
}

