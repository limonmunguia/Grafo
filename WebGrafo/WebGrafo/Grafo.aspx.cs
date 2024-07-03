using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Grafo;
using Newtonsoft.Json;

namespace WebGrafo
{
    public partial class Grafo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Grafo"] == null)
                {
                    Session["Grafo"] = new GrafoClass();
                }
                LlenarDrop();
            }
        }

        protected GrafoClass gf1
        {
            get
            {
                return (GrafoClass)Session["Grafo"];
            }
        }

        protected void btnAddCity_Click(object sender, EventArgs e)
        {
            Ciudad cd1 = new Ciudad();
            cd1.nomciudad = txtCiudad.Text;
            cd1.superficiekm = float.Parse(txtSuperficie.Text);
            cd1.totalhab = Convert.ToInt32(txtTHabit.Text);

            gf1.AgregarVertice(cd1);
            LlenarDrop();
            txtCiudad.Text = "";
            txtSuperficie.Text = "";
            txtTHabit.Text = "";
        }

        protected void LlenarDrop()
        {
            ddlOrig.Items.Clear();
            ddlDest.Items.Clear();
            ddlCiudadIIni.Items.Clear();
            ddlCityCaminos.Items.Clear();
            ddlDijkstraOri.Items.Clear();
            ddlDijkstraDest.Items.Clear();
            ddlOrig.Items.Add(new ListItem("-- Select City --", "")); 
            ddlDest.Items.Add(new ListItem("-- Select City --", "")); 
            ddlCityCaminos.Items.Add(new ListItem("-- Select City --", "")); 
            ddlCiudadIIni.Items.Add(new ListItem("-- Select City --", "")); 
            ddlDijkstraDest.Items.Add(new ListItem("-- Select City --", ""));
            ddlDijkstraOri.Items.Add(new ListItem("-- Select City --", ""));

            foreach (var vertice in gf1.MostrarVertices())
            {
                ddlOrig.Items.Add(new ListItem(vertice));
                ddlCityCaminos.Items.Add(new ListItem(vertice));
                ddlDest.Items.Add(new ListItem(vertice));
                ddlCiudadIIni.Items.Add(new ListItem(vertice));
                ddlDijkstraDest.Items.Add(new ListItem(vertice));
                ddlDijkstraOri.Items.Add(new ListItem(vertice));

            }
        }

        

        protected void btnAddCamino_Click(object sender, EventArgs e)
        {
            
            if(ddlOrig.SelectedIndex == ddlDest.SelectedIndex)
            {
                
            }
            else
            {
                int vertO = ddlOrig.SelectedIndex-1;
                int VerD = ddlDest.SelectedIndex-1;
                float costo = float.Parse(txtPeso.Text);

                gf1.AgregarArista(vertO, VerD, costo);
                
            }
            ddlOrig.Items.Clear();
            ddlDest.Items.Clear();
            txtPeso.Text = "";
            LlenarDrop();

        }

        protected void ddlOrig_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void btnMostrarArista_Click(object sender, EventArgs e)
        {
            string msg = "Agregando Arista";

            ddlAristas.Items.Clear();

            // Obtener las posiciones de las aristas
            var posiciones = gf1.MostarPosiciones(ddlCityCaminos.SelectedIndex - 1, ref msg);

            // Verificar si no se encontraron aristas
            if (posiciones != null && posiciones.Length == 0)
            {
                ddlAristas.Items.Add(new ListItem("No hay caminos disponibles."));
            }
            else if (posiciones != null)
            {
                string CiudadOrg = gf1.ListaAdyacencia[ddlCityCaminos.SelectedIndex - 1].city.nomciudad;

                // Mostrar las aristas encontradas
                foreach (var posicion in posiciones)
                {
                    string ciudad = gf1.ListaAdyacencia[posicion].infoCiudad();
                    ddlAristas.Items.Add(new ListItem($"Ciudad origen: {CiudadOrg} --> Ciudad destino: {ciudad}"));
                }
            }
        }

        protected void btnDFS_Click(object sender, EventArgs e)
        {
            string msg = "Recorrido Profundidad";

            // Obtener la ciudad seleccionada del DropDownList
            string ciudadInicial = ddlCiudadIIni.SelectedItem.Text;

            // Llamar al método RecorridoProfundidad del grafo
            string[] recorrido = gf1.RecorridoProfundidadPreOrden(ciudadInicial,ref msg);

            // Mostrar resultados en un GridView
            if (recorrido != null)
            {
                // Configurar el GridView
                gv1.DataSource = recorrido.Select((ciudad, index) => new { IndiceRecorridoDFS = index , Ciudad = ciudad });
                gv1.DataBind();
            }
            else
            {
                // Mostrar mensaje de error si no se encontró el vértice
               
                gv1.DataSource = null;
                gv1.DataBind();
            }

        }

        protected void btnBFS_Click(object sender, EventArgs e)
        {
            gv1.DataSource = null;

            // Llamar al método RecorridoProfundidad del grafo
            string[] recorrido = gf1.RecorridoAmplitud(ddlCiudadIIni.SelectedIndex-1);

            // Mostrar resultados en un GridView
            if (recorrido != null)
            {
                // Configurar el GridView
                gv1.DataSource = recorrido.Select((ciudad, index) => new { IndiceRecorridoBFS = index , Ciudad = ciudad });
                gv1.DataBind();
            }
            else
            {
                // Mostrar mensaje de error si no se encontró el vértice

                gv1.DataSource = null;
                gv1.DataBind();
            }

        }

        protected void btnDijkstra_Click(object sender, EventArgs e)
        {
            gvDijkstra.DataSource = null;
            string[] caminoMasCorto = gf1.Dijkstra(ddlDijkstraOri.SelectedIndex-1, ddlDijkstraDest.SelectedIndex-1);

            // Mostrar resultados en el GridView
            if (caminoMasCorto != null)
            {
                // Configurar el GridView
                gvDijkstra.DataSource = caminoMasCorto.Select((ciudad, index) => new { Indice = index + 1, Ciudad = ciudad });
                gvDijkstra.DataBind();
            }
            else
            {
                // Mostrar mensaje de error si no se encontró un camino
                gvDijkstra.DataSource = null;
                gvDijkstra.DataBind();
            }

        }

        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            gvbusquedatopo.DataSource = null;
            string[] busqueda = gf1.BusquedaTopologicaKahn();

            // Mostrar resultados en el GridView
            if (busqueda != null && busqueda.Length > 0)
            {
                // Configurar el GridView con los resultados obtenidos
                gvbusquedatopo.DataSource = busqueda;
                gvbusquedatopo.DataBind();
            }
            else
            {
                // Mostrar mensaje de error si no se encontró un orden topológico válido
                gvbusquedatopo.DataSource = new string[] { "El grafo contiene ciclos. No se puede realizar una búsqueda topológica." };
                gvbusquedatopo.DataBind();
            }
        }

        protected void btnGraficar_Click(object sender, EventArgs e)
        {
            var nodes = new List<object>();
            var edges = new List<object>();

            foreach (var vertice in gf1.ListaAdyacencia)
            {
                int verticeId = gf1.ListaAdyacencia.IndexOf(vertice);
                nodes.Add(new
                {
                    id = verticeId,
                    label = vertice.infoCiudad()
                });

                foreach (var arista in vertice.ListaEnlaces.ObtenerNodos())
                {
                    edges.Add(new
                    {
                        from = verticeId,
                        to = arista.nvertice,
                        label = arista.distancia.ToString()
                    });
                }
            }

            var json = JsonConvert.SerializeObject(new { nodes, edges });
            ClientScript.RegisterStartupScript(this.GetType(), "renderGraph", $"renderGraph({json});", true);
        }
    }
}