using System.Collections.ObjectModel;
using System.Windows;

namespace LoDeProduccion
{
    /// <summary>
    /// Lógica de interacción para PlanAgregadoTablas.xaml
    /// </summary>
    public partial class PlanAgregadoTablas : Window
    {

        private ObservableCollection<PersecucionPresentacion> persecucionlist;
        private ObservableCollection<FuerzaLaboralNivelada> niveladalist;

        public PlanAgregadoTablas(PAddedModel pamodel, ObservableCollection<PAddedVariance> variance)
        {
            InitializeComponent();
            persecucionlist = new ObservableCollection<PersecucionPresentacion>();
            dgPersecucion.ItemsSource = persecucionlist;
            foreach (var item in variance)
            {
                persecucionlist.Add(new PersecucionPresentacion(new EstrategiaPersecucion(pamodel, item.demanda)));
            }
            niveladalist = new ObservableCollection<FuerzaLaboralNivelada>();
            dgNivelada.ItemsSource = niveladalist;
        }


    }
}
