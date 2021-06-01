namespace LoDeProduccion
{
    public class PAddedVariance
    {
        public PAddedVariance(int Demanda, int Dias)
        {
            demanda = Demanda;
            dias = Dias;
        }

        public PAddedVariance() { }

        public int demanda { get; set; }
        public int dias { get; set; }
    }
}
