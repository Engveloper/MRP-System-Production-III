using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeProduccion
{
    public class PAddedModel
    {
        public int Demanda { get; set; }
        public int DiasHabiles { get; set; }
        public double MateriaPrima { get; set; }
        public double H { get; set; }//Costo mantenimiento unitario / * mensual/trimestral/etc... * 
        public double CostoDeFaltante { get; set; }
        public double Outsourcing { get; set; }
        public double CostoCapacitar { get; set; }
        public double CostoDespedir { get; set; }
        public double HoraNormal { get; set; }
        public double HoraExtra { get; set; }
        public int InventarioInicial { get; set; }
        public int InventarioDeSeguridad { get; set; }
        public int FuerzaLaboralInicial { get; set; }
        public double HorasRequeridaParaUnidad { get; set; }
        public double HorasPorDia { get { return 8; } }

    }
}
