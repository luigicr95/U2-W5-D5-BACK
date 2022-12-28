using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace U2_W5_D5_BACK.Models
{
    public class Violazione
    {
        public int IDViolazione { get; set; }
        public string DescrizioneViolazione { get; set; }
       public static List<Violazione> listaViolazioni = new List<Violazione>();
    }
}