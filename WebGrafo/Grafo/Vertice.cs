using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafo
{
    public class Vertice
    {
        public Ciudad city = null;
        public ListaArista ListaEnlaces = new ListaArista();

        public Vertice(Ciudad datos)
        {
            city = datos;
        }

        public string AgregarLista(int numv,float distancia)
        {
            return ListaEnlaces.Insertar(numv, distancia);
        }

        public string[] MuestraAristas()
        {
            return ListaEnlaces.MostrarColeccion();
        }

        public int[] ObtenerPos()
        {
            return ListaEnlaces.ObtenerPosiciones();
        }

        public float ObtenerCosto(int posAr)
        {
            return ListaEnlaces.RetornarPeso(posAr);
        }
        public string BorrarLista(int numVD)
        {
            return ListaEnlaces.Borrar(numVD);
        }

        public string infoCiudad()
        {
            return $"{city.nomciudad}";
        }
    }
}
