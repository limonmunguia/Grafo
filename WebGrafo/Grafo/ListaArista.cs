using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafo
{
    public class ListaArista
    {
        private NodoLista inicio = null;
        private int contador = 0;

        public string Insertar(int numV, float distancia)
        {
            string msg = "";
            NodoLista nuevo = new NodoLista();
            nuevo.nvertice = numV;
            nuevo.distancia = distancia;

            if(inicio == null)
            {
                //No hay elementos
                inicio = nuevo;
                contador++;
                msg = "Primer elemento de la colección";
            }

            else
            {
                NodoLista temp = null;
                temp = inicio;
                while(temp.next != null)
                {
                    temp = temp.next;
                }

                temp.next = nuevo;
                contador++;
                msg = $"Es el elemento num: {contador}";
            }
            return msg;
        }

        public string[] MostrarColeccion()
        {
            string[] cadena = new string[contador];
            int numAr = 0;
            NodoLista z = null;
            z = inicio;
            int w = 0;
            while(z != null)
            {
                cadena[w] = $"Posición enlace a: [{z.nvertice}] " ;
                w++;
                z = z.next;
                numAr++;
            }

            return cadena;
        }

        public float RetornarPeso(int nmAr)
        {
            NodoLista temp = inicio;

            while (temp != null)
            {
                if (temp.nvertice == nmAr)  // Compara con el número de vértice del nodo de la lista
                {
                    return temp.distancia; // Retorna el peso (distancia) del nodo encontrado
                }
                temp = temp.next;
            }

            // Manejar el caso donde no se encuentra el nodo con el vértice especificado
            throw new ArgumentException("El nodo especificado no existe en la lista.");
        }

        public int[] ObtenerPosiciones()
        {
            int[] cadena = new int[contador];
            int numAr = 0;
            NodoLista z = null;
            z = inicio;
            int w = 0;
            while (z != null)
            {
                cadena[w] = z.nvertice;
                w++;
                z = z.next;
                numAr++;
            }

            return cadena;
        }

        public List<NodoLista> ObtenerNodos()
        {
            List<NodoLista> nodos = new List<NodoLista>();
            NodoLista temp = inicio;
            while (temp != null)
            {
                nodos.Add(temp);
                temp = temp.next;
            }
            return nodos;
        }


        public string Borrar(int numVD)
        {
            string msg="";
            if(inicio == null)
            {
                msg = "La lista está vacía. No se puede borrar ningún elemento.";
                return msg;
            }

            // Caso especial: Borrar el primer elemento
            if (inicio.nvertice == numVD)
            {
                inicio = inicio.next;
                contador--;
                msg = $"Se ha borrado el vértice con número {numVD}.";
                return msg;
            }

            NodoLista temp = inicio;
            while (temp.next != null)
            {
                if (temp.next.nvertice == numVD)
                {
                    temp.next = temp.next.next; // Saltar el nodo que queremos borrar
                    contador--;
                    msg = $"Se ha borrado el vértice con número {numVD}.";
                    return msg;
                }
                temp = temp.next;
            }

            // Si no se encontró el vértice a borrar
            msg = $"No se encontró ningún vértice con número {numVD} en la lista.";
            return msg;
        }

        




    }
}
