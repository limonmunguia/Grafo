using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafo
{
    public class Ciudad
    {
        //Esta clase, será la que quede almacenada en el vertice
        public string nomciudad { get; set; }
        public int totalhab { get; set; }
        public float superficiekm { get; set; }

        public Ciudad() { }

        public Ciudad(string nomc, int totlhab, float sup)
        {
            nomciudad = nomc;
            totalhab = totlhab;
            superficiekm = sup;
        }

    }
}
