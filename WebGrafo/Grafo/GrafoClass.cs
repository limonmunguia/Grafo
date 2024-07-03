using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Grafo
{
    public class GrafoClass
    {
        public List<Vertice> ListaAdyacencia = new List<Vertice>();


        public int BuscarPorCiudad(string NombreCity, ref string msg)
        {
            for (int i = 0; i < ListaAdyacencia.Count; i++)
            {
                if (ListaAdyacencia[i].city.nomciudad == NombreCity)
                {
                    msg = "Ciudad encontrada";
                    return i; // Regresa el índice de la ciudad encontrada
                }
            }

            msg = "Vertice no encontrado";
            return -1; // Regresa -1 si la ciudad no se encuentra
        }

        public string AgregarVertice(Ciudad info)
        {
            ListaAdyacencia.Add(new Vertice(info));
            return "Nuevo Vertice creado";
        }

        public string BorrarVertice(string nomcd)
        {
            string msg = "";
            int vertice = BuscarPorCiudad(nomcd, ref msg);
            // Buscar el vértice en ListaAdyacencia basado en la información de la ciudad

            if (vertice >=0)
            {
                ListaAdyacencia.RemoveAt(vertice);
                msg = $"Se ha borrado el vértice asociado a la ciudad {nomcd}.";
            }
            else msg = $"No se encontró ningún vértice asociado a la ciudad {nomcd} en la lista.";
           
            return msg;
        }

        public string AgregarArista(int VertOrig, int VertDestino, float cost3)
        {
            string msg = "";
            //Hay que verificar que vorigen y vdestino esten en el rango de los objetos de list<> 
            //hay que verificar que numV3 este en el arngo de los objetos del list
            if (VertOrig >= 0 && VertOrig <= (ListaAdyacencia.Count - 1))
            {
                if (VertDestino >= 0 && VertDestino <= (ListaAdyacencia.Count - 1))
                {
                    ListaAdyacencia[VertOrig].AgregarLista(VertDestino, cost3);
                    msg = "Arista Agregada";
                }
                else msg = "La posición del vertice destino no existe en la lista";
            }
            else
            {
                msg = "La posición del vertice origen no existe en la lista";
            }
            return msg;
        }

        public string BorrarArista(int VertOrig, int VertDestino)
        {
            string msg = "";
            //Hay que verificar que vorigen y vdestino esten en el rango de los objetos de list<> 
            //hay que verificar que numV3 este en el arngo de los objetos del list
            if (VertOrig >= 0 && VertOrig <= (ListaAdyacencia.Count - 1))
            {
                if (VertDestino >= 0 && VertDestino <= (ListaAdyacencia.Count - 1))
                {
                    ListaAdyacencia[VertOrig].BorrarLista(VertDestino);
                    msg = "Arista Borrada";
                }
                else msg = "La posición del vertice destino no existe en la lista";
            }
            else
            {
                msg = "La posición del vertice origen no existe en la lista";
            }
            return msg;
        }

        public string[] MostarAristasVertice(int posVertice, ref string msg)
        {
            string[] salida = null;

            if (posVertice >= 0 && posVertice <= (ListaAdyacencia.Count - 1))
            {
                salida = ListaAdyacencia[posVertice].MuestraAristas() ;
                msg = "Correcto";
            }
            else
            {
                msg = "La posición del vértice no existe en la lista";
            }
            return salida; 
        }

        public int[] MostarPosiciones(int posVertice, ref string msg)
        {
            int[] salida = null;

            if (posVertice >= 0 && posVertice <= (ListaAdyacencia.Count - 1))
            {
                salida = ListaAdyacencia[posVertice].ObtenerPos();
                msg = "Correcto";
            }
            else
            {
                msg = "La posición del vértice no existe en la lista";
            }
            return salida;
        }

        public string[] CiudadesVert(int posv, ref string msg)
        {
            string[] salida3 = null;
            if (posv >= 0 && posv < ListaAdyacencia.Count)
            {
                // Obtener la lista de adyacencia del vértice dado
                int[] adyacentes = ListaAdyacencia[posv].ObtenerPos();

                // Inicializar el arreglo de salida con el tamaño de la lista de adyacencia
                salida3 = new string[adyacentes.Length-1];

                // Llenar el arreglo de salida con los nombres de las ciudades (o vértices adyacentes)
                for (int i = 0; i < adyacentes.Length-1; i++)
                {
                    salida3[i] = ListaAdyacencia[adyacentes[i]].city.nomciudad; // Suponiendo que existe un método NombreCiudad() para obtener el nombre del vértice
                }

                msg = "Correcto";
            }
            else
            {
                msg = "La posición del vértice no existe en la lista";
            }
            return salida3;
        }


        public string[] MostrarVertices()
        {
            string[] vertices = new string[ListaAdyacencia.Count];

            for (int i = 0; i < ListaAdyacencia.Count; i++)
            {
                vertices[i] = ListaAdyacencia[i].city.nomciudad; // Suponiendo que nomciudad es el nombre de la ciudad en la clase Ciudad
            }

            return vertices;
        }



        public string[] RecorridoProfundidadPreOrden(string NomC, ref string msg)
        {
            string[] resultado = null;
            int vertIni = BuscarPorCiudad(NomC, ref msg);

            if (vertIni >= 0)
            {
                bool[] visitado = new bool[ListaAdyacencia.Count];
                List<string> visitados = new List<string>();

                Stack<int> pila = new Stack<int>();
                pila.Push(vertIni);
                visitado[vertIni] = true;

                while (pila.Count > 0)
                {
                    int v = pila.Pop();
                    visitados.Add(ListaAdyacencia[v].infoCiudad());

                    int[] adyacentes = ListaAdyacencia[v].ObtenerPos();
                    foreach (int adyacente in adyacentes)
                    {
                        if (!visitado[adyacente])
                        {
                            visitado[adyacente] = true;
                            pila.Push(adyacente);
                        }
                    }
                }

                // Si la primera ciudad visitada es la misma que NomC, eliminarla
                if (visitados.Count > 0 && visitados[0] == NomC)
                {
                    visitados.RemoveAt(0);
                }

                resultado = visitados.ToArray();
            }
            else
            {
                msg = "Vertice no encontrado";
                resultado = new string[] { "No se ha encontrado nada" };
            }

            return resultado;
        }

        public string[] RecorridoAmplitud(int vertIni)
        {
            bool[] visitado = new bool[ListaAdyacencia.Count];
            Queue<int> cola = new Queue<int>();
            string[] visitados = new string[ListaAdyacencia.Count];
            int index = 0;

            cola.Enqueue(vertIni);
            visitado[vertIni] = true;

            while (cola.Count > 0)
            {
                int v = cola.Dequeue();
                visitados[index++] = ListaAdyacencia[v].infoCiudad();
                Console.WriteLine($"Visitando: {ListaAdyacencia[v].infoCiudad()}"); // Mensaje de depuración

                List<NodoLista> adyacentes = ListaAdyacencia[v].ListaEnlaces.ObtenerNodos();
                Console.WriteLine($"Adyacentes de {ListaAdyacencia[v].infoCiudad()}: {string.Join(", ", adyacentes.Select(a => a.nvertice))}"); // Mensaje de depuración
                foreach (NodoLista adyacente in adyacentes)
                {
                    if (!visitado[adyacente.nvertice])
                    {
                        cola.Enqueue(adyacente.nvertice);
                        visitado[adyacente.nvertice] = true;
                    }
                }
            }

            string[] resultado = new string[index];
            Array.Copy(visitados, resultado, index);
            return resultado;
        }


        public float ObtenerPesoAr(int posiAris,int verOr)
        {
            return ListaAdyacencia[verOr].ObtenerCosto(posiAris);
        }

        public string[] Dijkstra(int origen, int destino)
        {
            int n = ListaAdyacencia.Count;
            float[] distancias = new float[n];
            bool[] visitado = new bool[n];
            int[] previo = new int[n];

            for (int i = 0; i < n; i++)
            {
                distancias[i] = float.MaxValue;
                visitado[i] = false;
                previo[i] = -1;
            }

            distancias[origen] = 0;
            List<int> cola = new List<int> { origen };

            while (cola.Count > 0)
            {
                int u = cola[0];
                int minIndex = 0;
                for (int i = 1; i < cola.Count; i++)
                {
                    if (distancias[cola[i]] < distancias[u])
                    {
                        u = cola[i];
                        minIndex = i;
                    }
                }
                cola.RemoveAt(minIndex);

                if (visitado[u])
                    continue;

                visitado[u] = true;

                var adyacentes = ListaAdyacencia[u].ObtenerPos();
                foreach (var v in adyacentes)
                {
                    float peso = ListaAdyacencia[u].ListaEnlaces.RetornarPeso(v);
                    if (distancias[u] + peso < distancias[v])
                    {
                        distancias[v] = distancias[u] + peso;
                        previo[v] = u;
                        if (!visitado[v] && !cola.Contains(v))
                        {
                            cola.Add(v);
                        }
                    }
                }
            }

            if (distancias[destino] == float.MaxValue)
            {
                // No hay camino desde el origen al destino
                return new string[] { "No hay camino desde el origen al destino." };
            }

            // Reconstruir el camino en términos de nombres (o representaciones) de los nodos
            Stack<int> camino = new Stack<int>();
            for (int at = destino; at != -1; at = previo[at])
            {
                camino.Push(at);
            }

            List<string> recorrido = new List<string>();
            while (camino.Count > 0)
            {
                int nodo = camino.Pop();
                recorrido.Add(ListaAdyacencia[nodo].infoCiudad()); // Asumiendo que infoCiudad() devuelve el nombre de la ciudad
            }

            return recorrido.ToArray();
        }


        public string[] BusquedaTopologicaKahn()
        {
            int[] gradosEntrada = new int[ListaAdyacencia.Count];
            Queue<int> cola = new Queue<int>();
            List<string> ordenTopologico = new List<string>();

            // Calcular grados de entrada para todos los vértices
            foreach (var vertice in ListaAdyacencia)
            {
                int[] adyacentes = vertice.ObtenerPos();
                foreach (int v in adyacentes)
                {
                    gradosEntrada[v]++;
                }
            }

            // Inicializar la cola con los vértices que tienen grado de entrada cero
            for (int i = 0; i < ListaAdyacencia.Count; i++)
            {
                if (gradosEntrada[i] == 0)
                {
                    cola.Enqueue(i);
                }
            }

            while (cola.Count > 0)
            {
                int u = cola.Dequeue();
                ordenTopologico.Add(ListaAdyacencia[u].infoCiudad()); // Agregar el vértice al orden topológico

                int[] adyacentes = ListaAdyacencia[u].ObtenerPos();
                foreach (int v in adyacentes)
                {
                    gradosEntrada[v]--;
                    if (gradosEntrada[v] == 0)
                    {
                        cola.Enqueue(v);
                    }
                }
            }

            // Si el tamaño del orden topológico es menor que el número de vértices, hay un ciclo
            if (ordenTopologico.Count != ListaAdyacencia.Count)
            {
                return new string[] { "El grafo contiene ciclos. No se puede realizar una búsqueda topológica." };
            }

            return ordenTopologico.ToArray();
        }


    }
}
