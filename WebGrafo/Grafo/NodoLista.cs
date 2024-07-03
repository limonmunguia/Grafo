using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafo
{
    public class NodoLista
    {
        public int nvertice = -1; //no se pone cero pq existe el vertice en la posicion [0]

        //costo para llegar a ese vertice
        public float distancia { get; set; }
        //enlace lista ligada
        public NodoLista next = null;

        public float ObtenerPeso()
        {
            return distancia;
        }

    }
}
