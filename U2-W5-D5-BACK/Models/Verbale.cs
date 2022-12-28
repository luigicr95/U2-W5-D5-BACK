﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace U2_W5_D5_BACK.Models
{
    public class Verbale
    {
        public int IDVerbale { get; set; }
        public DateTime DataViolazione { get; set; }
        public string IndirizzoViolazione { get; set; }
        public string NominativoAgente { get; set; }
        public DateTime DataTrascrizione { get; set; }
        public decimal Importo { get; set; }
        public int Decurtamento { get; set; }
        public string Violazione { get; set; }
        public string Profilo { get; set; }

        public static List<Verbale> listaVerbali = new List<Verbale>(); 

    }
}