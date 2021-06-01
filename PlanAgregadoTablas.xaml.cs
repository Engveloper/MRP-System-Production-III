using System;
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

        public PlanAgregadoTablas(PAddedModel pamodel, ObservableCollection<PAddedVariance> variance, double? percentage)
        {
            InitializeComponent();
            persecucionlist = new ObservableCollection<PersecucionPresentacion>();
            dgPersecucion.ItemsSource = persecucionlist;
            for (int i = 0; i < variance.Count; i++)
            {
                if (percentage is not null)
                {
                    pamodel.InventarioDeSeguridad = (int)Math.Ceiling(Convert.ToDecimal(variance[i].demanda * percentage));
                }
                if (i == 0)
                {
                    var estrategia = new EstrategiaPersecucion(pamodel, variance[i]);
                    var presentacion = new PersecucionPresentacion(estrategia);
                    persecucionlist.Add(presentacion);
                }
                else
                {
                    pamodel.FuerzaLaboralInicial = new EstrategiaPersecucion(pamodel, variance[i-1]).TrabajadoresNecesitados;
                    persecucionlist.Add(new PersecucionPresentacion(new EstrategiaPersecucion(pamodel, variance[i])));
                }
            }
            niveladalist = new ObservableCollection<FuerzaLaboralNivelada>();
            dgNivelada.ItemsSource = niveladalist;
        }


    }
}
