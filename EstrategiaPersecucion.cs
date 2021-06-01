using System;

namespace LoDeProduccion
{
    public class EstrategiaPersecucion
    {
        public PAddedModel _pAddedModel { get; set; }
        private int _demanda;
        private int _dias;

        public EstrategiaPersecucion(PAddedModel pAddedModel, PAddedVariance variance)
        {
            _pAddedModel = pAddedModel;
            _demanda = variance.demanda;
            _dias = variance.dias;
        }

        public double HorasRequeridas { 
            get 
            {
                var unidades = _demanda + _pAddedModel.InventarioDeSeguridad - _pAddedModel.InventarioInicial;
                return unidades * _pAddedModel.HorasRequeridaParaUnidad;
            } 
        }

        public double HorasDisponiblePorTrabajador
        {
            get
            {
                var horasMensualxTrabajador = _dias * _pAddedModel.HorasPorDia;
                return horasMensualxTrabajador;
            }
        }

        public int TrabajadoresNecesitados
        {
            get
            {
                double trabajadoresRAW = HorasRequeridas / HorasDisponiblePorTrabajador;
                return Convert.ToInt32(trabajadoresRAW);
            }
        }

        //  Costos

        public double MateriaPrima
        {
            get
            {
                var total = _pAddedModel.MateriaPrima * (HorasRequeridas/_pAddedModel.HorasRequeridaParaUnidad);
                return total;
            }
        }

        public double H = 0;
        public double Outsourcing = 0;

        private int Contratados
        {
            get
            {
                if (TrabajadoresNecesitados > _pAddedModel.FuerzaLaboralInicial) return TrabajadoresNecesitados - _pAddedModel.FuerzaLaboralInicial;
                return 0;
            }
        }

        public double ContratadosCosto
        {
            get
            {
                return Contratados * _pAddedModel.CostoCapacitar;
            }
        }

        private int Despedidos
        {
            get
            {
                if (TrabajadoresNecesitados < _pAddedModel.FuerzaLaboralInicial) return _pAddedModel.FuerzaLaboralInicial - TrabajadoresNecesitados;
                return 0;
            }
        }

        public double DespedidosCosto
        {
            get
            {
                return Despedidos * _pAddedModel.CostoDespedir;
            }
        }

        public double CostoHorasNormales
        {
            get
            {
                return HorasRequeridas * _pAddedModel.HoraNormal;
            }
        }

        public double CostoFaltanteTotal
        {
            get
            {
                return 0;
            }
        }

        public double Total
        {
            get
            {
                return MateriaPrima + ContratadosCosto + DespedidosCosto + CostoHorasNormales;
            }
        }

    }
}
