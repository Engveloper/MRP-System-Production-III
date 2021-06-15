using System;
using System.Collections.ObjectModel;

using System.Linq;

using System.Windows;

namespace LoDeProduccion
{
    /// <summary>
    /// Lógica de interacción para PlanAgregadoTablas.xaml
    /// </summary>
    public partial class PlanAgregadoTablas : Window
    {

        private ObservableCollection<PersecucionPresentacion> persecucionlist;

        private ObservableCollection<NiveladaPresentacion> niveladalist;


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


            /* ---------NIVELADA--------------- */

            niveladalist = new ObservableCollection<NiveladaPresentacion>();
            dgNivelada.ItemsSource = niveladalist;
            var demandaProm = (double)variance.Sum(x => x.demanda)/variance.Count();
            for (int i = 0; i < variance.Count; i++)
            {
                if(percentage is not null)
                {
                    pamodel.InventarioDeSeguridad = (int)Math.Ceiling(Convert.ToDecimal(variance[i].demanda * percentage));
                }
                if(i == 0)
                {
                    var estrategia = new FuerzaLaboralNivelada(demandaProm, variance[i], pamodel);
                    niveladalist.Add(new NiveladaPresentacion(estrategia));
                }
                else
                {
                    pamodel.InventarioInicial = niveladalist[i - 1].InventarioFinal;
                    pamodel.FuerzaLaboralInicial = new FuerzaLaboralNivelada(demandaProm, variance[i-1], pamodel).TrabajadoresRequeridos;
                    var estrategia = new FuerzaLaboralNivelada(demandaProm, variance[i], pamodel);
                    niveladalist.Add(new NiveladaPresentacion(estrategia));

                }
            }


        }


    }
}
