namespace LoDeProduccion
{
    public class NiveladaPresentacion
    {
        public NiveladaPresentacion(FuerzaLaboralNivelada fln)
        {
            HorasDisponibles = fln.HorasDisponiblesPorTrabajador;
            ProduccionReal = fln.ProduccionReal;
            Demanda = fln.Demanda;
            InventarioFinal = fln.InventarioFinal;
            SS = fln.InventarioDeSeguridad;
            MateriaPrima = fln.MateriaPrima;
            H = fln.H;
            CostoDeFaltante = fln.CostoFaltante;
            Contratar = fln.ContratadosCosto;
            Despedir = fln.DespidosCosto;
        }

        public double HorasDisponibles { get; set; }
        public double ProduccionReal { get; set; }
        public int Demanda { get; set; }
        public int InventarioFinal { get; set; }
        public int SS { get; set; }
        public double MateriaPrima { get; set; }
        public double H { get; set; }
        public double CostoDeFaltante { get; set; }
        public int Outsourcing { get; set; } = 0;
        public double Contratar { get; set; }
        public double Despedir { get; set; }


    }
}
