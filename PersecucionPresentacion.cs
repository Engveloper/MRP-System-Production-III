namespace LoDeProduccion
{
    public class PersecucionPresentacion
    {
        public PersecucionPresentacion(EstrategiaPersecucion ep)
        {
            HorasRequeridas = ep.HorasRequeridas;
            HorasDisponiblePorTrabajador = ep.HorasDisponiblePorTrabajador;
            NumeroDeTrabajadoresNecesitados = ep.TrabajadoresNecesitados;
            MateriaPrima = ep.MateriaPrima;
            H = ep.H;
            CostoFaltante = ep.CostoFaltanteTotal;
            Outsourcing = ep.Outsourcing;
            Contratados = ep.ContratadosCosto;
            Despidos = ep.DespedidosCosto;
            CostoHorasNormales = ep.CostoHorasNormales;
            Total = ep.Total;
        }

        public double HorasRequeridas { get; set; }
        public double HorasDisponiblePorTrabajador { get; set; }
        public int NumeroDeTrabajadoresNecesitados { get; set; }
        public double MateriaPrima { get; set; }
        public double H { get; set; }
        public double CostoFaltante { get; set; }
        public double Outsourcing { get; set; }
        public double Contratados { get; set; }
        public double Despidos { get; set; }
        public double CostoHorasNormales { get; set; }
        public double Total { get; set; }
    }
}
