using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace U2_W5_D5_BACK.Models
{
    public class TotaleTrasgressore
    {
        public int TotaleVerbali { get; set; }
        public string Profilo { get; set; }
        public static List<TotaleTrasgressore> totaleVerbali = new List<TotaleTrasgressore>();
    }
}