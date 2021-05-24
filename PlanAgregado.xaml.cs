using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Treeview
{
    /// <summary>
    /// Interaction logic for PlanAgregado.xaml
    /// </summary>
    public partial class PlanAgregado : UserControl
    {
        private List<PlanAgregadoModel> planList = new List<PlanAgregadoModel>();
        private int index = 0;
        public PlanAgregado()
        {
            InitializeComponent();
        }

        private void AgregarMes_Click(object sender, RoutedEventArgs e)
        {
            PlanAgregadoModel temp = new PlanAgregadoModel();
            temp.MateriaPrima = float.Parse(ingMateriaPrima.Text);
            temp.CostoFijo = float.Parse(ingCostoFijo.Text);
            temp.Mantener = float.Parse(ingMantener.Text);
            temp.Contratar = float.Parse(ingContratar.Text);
            temp.Despido = float.Parse(ingDespido.Text);
            temp.HoraNormal = float.Parse(ingHoraNormal.Text);
            temp.HoraExtra = float.Parse(ingHoraExtra.Text);

            temp.Mes = index;
            temp.DiasLaborales = int.Parse(ingDiasLaborales.Text);
            temp.HorasLaborales = int.Parse(ingHorasJornada.Text);
            temp.ProduccionTrabajador = int.Parse(ingProduccionTrabajador.Text);
            temp.Demanda = int.Parse(ingDemanda.Text);

            if (index == 0)
            {
                temp.TrabajadoresActuales = int.Parse(ingFuerzaLaboralInicial.Text); 
            }
            else
            {
                temp.TrabajadoresActuales = planList.ElementAt(index-1).TrabajadoresRequeridos;
            }
            index++;
        }

        private void Finalizar_Click(object sender, RoutedEventArgs e)
        {
            TablaPlan.ItemsSource = planList;
            TablaPlan.Visibility = Visibility.Visible;
        }
    }
}

