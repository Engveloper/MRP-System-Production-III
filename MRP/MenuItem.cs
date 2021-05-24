using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace LoDeProduccion.MRP
{
    class MenuItem
    {
        public ObservableCollection<MenuItem> Elementos { get; set; }

        private string nombre;
        public string Nombre { get => nombre; set => nombre = value; }

        private string padre = "";
        public string Padre { get => padre; set => padre = value; }

        private string tiempoEspera;
        public string TiempoEspera { get => tiempoEspera; set => tiempoEspera = value; }

        private int cantidadComponentes;
        public int CantidadComponentes { get => cantidadComponentes; set => cantidadComponentes = value; }

        public MenuItem(string pNombre, string pTiempoEspera, int pCantidadComponentes)
        {
            Elementos = new ObservableCollection<MenuItem>();
            nombre = pNombre;
            tiempoEspera = pTiempoEspera;
            cantidadComponentes = pCantidadComponentes;
        }

        public MenuItem(string pNombre, string pPadre, string pTiempoEspera, int pCantidadComponentes)
        {
            Elementos = new ObservableCollection<MenuItem>();
            nombre = pNombre;
            padre = pPadre;
            tiempoEspera = pTiempoEspera;
            cantidadComponentes = pCantidadComponentes;
        }
    }
}
