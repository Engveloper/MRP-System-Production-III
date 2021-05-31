using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeProduccion
{
    public class EstrategiaPersecucion
    {
        private readonly PAddedModel _pAddedModel;

        public EstrategiaPersecucion(PAddedModel pAddedModel)
        {
            _pAddedModel = pAddedModel;
        }

        public double HorasRequeridas { 
            get 
            {
                var unidades = _pAddedModel.Demanda + _pAddedModel.InventarioDeSeguridad - _pAddedModel.InventarioInicial;
                return unidades * _pAddedModel.HorasRequeridaParaUnidad;
            } 
        }

        public double HorasDisponiblePorTrabajador
        {
            get
            {
                var horasMensualxTrabajador = _pAddedModel.DiasHabiles * _pAddedModel.HorasPorDia;
                return horasMensualxTrabajador;
            }
        }

        public int TrabajadoresNecesitados
        {
            get
            {
                double trabajadoresRAW = HorasRequeridas / HorasDisponiblePorTrabajador;
                return (int)Math.Ceiling(trabajadoresRAW);
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
        public double CostoFijo = 0;
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
                return Contratados * _pAddedModel.HoraNormal;
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

        public double Total
        {
            get
            {
                return MateriaPrima + ContratadosCosto + DespedidosCosto + CostoHorasNormales;
            }
        }

    }
}
