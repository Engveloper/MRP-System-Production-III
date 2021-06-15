using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeProduccion
{
    public class FuerzaLaboralNivelada
    {

        public PAddedModel _pAddedModel;
        private readonly double _demandaPromedio;
        private int _demanda;
        private int _dias;

        public FuerzaLaboralNivelada(double demandaPromedio, PAddedVariance variance, PAddedModel pAddedModel)
        {
            _pAddedModel = pAddedModel;
            _demandaPromedio = demandaPromedio;
            _demanda = variance.demanda;
            _dias = variance.dias;
        }

        public double HorasDisponiblesPorTrabajador
        {
            get
            {
                var horasMensualxTrabajador = _dias * _pAddedModel.HorasPorDia;
                return horasMensualxTrabajador;
            }
        }

        private int UnidadesPorTrabajadorMensual
        {
            get
            {
                var unidades = (int)Math.Ceiling(HorasDisponiblesPorTrabajador / _pAddedModel.HorasRequeridaParaUnidad);
                return unidades;
            }
        }


        public int TrabajadoresRequeridos
        {
            get
            {
                return (int)_demandaPromedio / UnidadesPorTrabajadorMensual;
            }
        }

        public int ProduccionReal
        {
            get
            {
                return TrabajadoresRequeridos * UnidadesPorTrabajadorMensual;
            }
        }

        public int Demanda { get { return _demanda; } }

        public int InventarioInicial { get { return _pAddedModel.InventarioInicial; } }

        public int InventarioFinal
        {
            get
            {
                return (int)(ProduccionReal + InventarioInicial - _demandaPromedio);

            }
        }

        public int InventarioDeSeguridad { get { return _pAddedModel.InventarioDeSeguridad; } }

        // Costos

        public double MateriaPrima
        {
            get
            {
                return ProduccionReal * _pAddedModel.MateriaPrima;
            }
        }

        public double H
        {
            get
            {

                return InventarioFinal;
            }
        }

        public double CostoFaltante
        {
            get
            {
                if(InventarioFinal < 0)
                {
                    return _pAddedModel.
   
            }
        }

       
        }

        public double Outsourcing { get { return 0; } }

        private double Contratado
        {
            get
            {

                if (TrabajadoresRequeridos < _pAddedModel.FuerzaLaboralInicial)
                {
                    return _pAddedModel.FuerzaLaboralInicial - TrabajadoresRequeridos;
                }
                return 0;

            }
        }

        public double ContratadosCosto
        {
            get
            {

                return Contratado * _pAddedModel.CostoCapacitar;

            }
        }

        private double Despido
        {
            get
            {

                if (TrabajadoresRequeridos > _pAddedModel.FuerzaLaboralInicial)
                {
                    return TrabajadoresRequeridos - _pAddedModel.FuerzaLaboralInicial;
                }
                return 0;

            }
        }

        public double DespidosCosto
        {
            get
            {

                return Despido * _pAddedModel.CostoDespedir;
            }
        }

        public double HorasRequeridas
        {
            get
            {
                var unidades = _demanda + _pAddedModel.InventarioDeSeguridad - _pAddedModel.InventarioInicial;
                return unidades * _pAddedModel.HorasRequeridaParaUnidad;
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
                return MateriaPrima + ContratadosCosto + DespidosCosto + CostoHorasNormales + CostoFaltante;
            }
        }


    }
}
