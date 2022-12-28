using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace U2_W5_D5_BACK.Models
{
    public class Anagrafica
    {
        public int IDAnagrafica { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string CAP { get; set; }
        public string CodiceFiscale { get; set; }

        public static List<Anagrafica> listaAnagrafica = new List<Anagrafica>();

    }
}