using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeProduccion
{
    public class FuerzaLaboralNivelada
    {
        private readonly PAddedModel _pAddedModel;
        private readonly int _demandaPromedio;

        public FuerzaLaboralNivelada(int demandaPromedio, PAddedModel pAddedModel)
        {
            _pAddedModel = pAddedModel;
            _demandaPromedio = demandaPromedio;
        }

        public double HorasDisponiblesPorTrabajador
        {
            get
            {
                var horasMensualxTrabajador = _pAddedModel.DiasHabiles * _pAddedModel.HorasPorDia;
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

        private int TrabajadoresRequeridos
        {
            get
            {
                return _demandaPromedio / UnidadesPorTrabajadorMensual;
            }
        }

        public int ProduccionReal
        {
            get
            {
                return TrabajadoresRequeridos * UnidadesPorTrabajadorMensual;
            }
        }

        public int Demanda { get { return _pAddedModel.Demanda; } }

        public int InventarioInicial { get { return _pAddedModel.InventarioInicial; } }

        public int InventarioFinal
        {
            get
            {
                return ProduccionReal + InventarioInicial - _demandaPromedio;
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
                return 0;//needs implementation
            }
        }

        public double CostoFijo
        {
            get
            {
                return 0;//needs implementation
            }
        }

        public double Outsourcing { get { return 0; } }

        private double Contratado
        {
            get
            {
                return 0;//needs implementation
            }
        }

        public double ContratadosCosto
        {
            get
            {
                return 0;//needs implementation
            }
        }

        private double Despido
        {
            get
            {
                return 0;//needs implementation
            }
        }

        public double DespidosCosto
        {
            get
            {
                return 0;//needs implementation
            }
        }


    }
}
