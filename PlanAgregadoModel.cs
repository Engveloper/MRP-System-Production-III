using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treeview
{

    class PlanAgregadoModel
    {
        private float materiaPrima;
        private float costoFijo;
        private float mantener;
        private float contratar;
        private float despido;
        private float horaNormal;
        private float horaExtra;
        private float costoTrabajadoresContratados;
        private float costoTrabajadoresDespedidos;
        private float costoManoObra;
        private float costoMantener;
        private float costoHoraExtra;
        private float costoTotal;
        private int mes;
        private int diasLaborales;
        private int horasLaborales;
        private int produccionTrabajador;
        private int unidadesProducidas;
        private int demanda;
        private int inventarioInicial;
        private int inventario;
        private int fuerzaTrabajoInicial;
        private int trabajadoresRequeridos;
        private int trabajadoresActuales;
        private int trabajadoresContratados;
        private int trabajadoresDespedidos;

        public int Mes
        {
            get { return mes; }
            set { mes = value; }
        }

        public int InventarioInicial
        {
            get { return inventarioInicial; }
            set { inventarioInicial = value; }
        }

        public int FuerzaTrabajoInicial
        {
            get { return fuerzaTrabajoInicial; }
            set { fuerzaTrabajoInicial = value; }
        }

        public int DiasLaborales
        {
            get { return diasLaborales; }
            set { diasLaborales = value; }
        }

        public int ProduccionTrabajador
        {
            get { return produccionTrabajador; }
            set { produccionTrabajador = value; }
        }

        public int Demanda
        {
            get { return demanda; }
            set { demanda = value; }
        }

        public int TrabajadoresRequeridos
        {
            get { return trabajadoresRequeridos; }
            set 
            {
                double temp;
                temp = (demanda / produccionTrabajador);
                temp = Math.Round(temp);
                trabajadoresRequeridos = (int)temp;
            }
        }

        public int TrabajadoresActuales
        {
            get { return trabajadoresActuales; }
            set { trabajadoresActuales = value; }
        }

        public int TrabajadoresContratados
        {
            get { return trabajadoresContratados; }
            set 
            {
                int temp = trabajadoresRequeridos - trabajadoresActuales;
                if (temp > 0)
                {
                    trabajadoresContratados = temp;
                }
                else
                {
                    trabajadoresContratados = 0;
                }
            }
        }

        public float Contratar
        {
            get { return contratar; }
            set { contratar = value; }
        }

        public float CostoTrabajadoresContratados
        {
            get { return costoTrabajadoresContratados; }
            set { costoTrabajadoresContratados = trabajadoresContratados*contratar; }
        }

        public int TrabajadoresDespedidos
        {
            get { return trabajadoresDespedidos; }
            set 
            {
                int temp = trabajadoresRequeridos - trabajadoresActuales;
                if (temp >= 0)
                {
                    trabajadoresDespedidos = 0;
                }
                else
                {
                    trabajadoresDespedidos = Math.Abs(temp);
                }
            }
        }

        public float Despido
        {
            get { return despido; }
            set { despido = value; }
        }

        public float CostoTrabajadoresDespedidos
        {
            get { return costoTrabajadoresDespedidos; }
            set { costoTrabajadoresDespedidos = trabajadoresDespedidos * despido; }
        }

        public float HoraNormal
        {
            get { return horaNormal; }
            set { horaNormal = value; }
        }

        public int HorasLaborales
        {
            get { return horasLaborales; }
            set { horasLaborales = value; }
        }

        public float CostoManoObra
        {
            get { return costoManoObra; }   
            set { costoManoObra = horasLaborales * horaNormal * diasLaborales; }
        }

        public int UnidadesProducidas
        {
            get { return unidadesProducidas; }
            set { unidadesProducidas = trabajadoresRequeridos * produccionTrabajador; }
        }

        public int Inventario
        {
            get { return inventario; }
            set 
            {
                int temp = unidadesProducidas - demanda;
                if (temp > 0)
                {
                    inventario = temp;
                }
                else
                {
                    inventario = 0;
                }
            }
        }

        public float Mantener
        {
            get { return mantener; }
            set { mantener = value; }
        }

        public float CostoMantener
        {
            get { return costoMantener; }
            set { costoMantener = mantener * inventario; }
        }

        public float HoraExtra
        {
            get { return horaExtra; }
            set { horaExtra = value; }
        }

        public float CostoHoraExtra
        {
            get { return costoHoraExtra; }
            set 
            {
                int temp = (demanda - unidadesProducidas);
                if (temp <= 0)
                {
                    costoHoraExtra = 0;
                }
                else
                {
                    temp = (demanda - unidadesProducidas) / ((produccionTrabajador * trabajadoresRequeridos) / (horasLaborales));
                    costoHoraExtra = temp * horaExtra;
                }
            }
        }

        public float MateriaPrima
        {
            get { return materiaPrima; }
            set { materiaPrima = value; }
        }

        public float CostoFijo
        {
            get { return costoFijo; }
            set { costoFijo = value; }
        }

        public float CostoTotal
        {
            get { return costoTotal; }
            set { costoTotal = costoHoraExtra + costoManoObra + costoFijo + costoMantener + costoTrabajadoresContratados + costoTrabajadoresDespedidos; }
        }
    }
}

