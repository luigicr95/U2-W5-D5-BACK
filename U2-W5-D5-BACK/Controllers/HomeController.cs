using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using U2_W5_D5_BACK.Models;

namespace U2_W5_D5_BACK.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListaAnagrafe()
        {
            Anagrafica.listaAnagrafica.Clear();
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToPoliziaDB"].ToString();
                con.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM ANAGRAFICA";
                command.Connection = con;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Anagrafica profilo = new Anagrafica();
                    profilo.IDAnagrafica = Convert.ToInt32(reader["IDAnagrafica"]);
                    profilo.Cognome = reader["Cognome"].ToString();
                    profilo.Nome = reader["Nome"].ToString();
                    profilo.Indirizzo = reader["Indirizzo"].ToString();
                    profilo.Citta = reader["Citta"].ToString();
                    profilo.CAP = reader["CAP"].ToString();
                    profilo.CodiceFiscale = reader["CodiceFiscale"].ToString();
                    Anagrafica.listaAnagrafica.Add(profilo);
                }

            }catch (Exception ex)
            {

            }

            con.Close();

            return View(Anagrafica.listaAnagrafica);
        }

        public ActionResult CreateAnagrafica()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAnagrafica(Anagrafica profilo)
        {
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToPoliziaDB"].ToString();
                con.Open();

                SqlCommand command = new SqlCommand();
                command.Parameters.AddWithValue("@Cognome", profilo.Cognome);
                command.Parameters.AddWithValue("@Nome", profilo.Nome);
                command.Parameters.AddWithValue("@Indirizzo", profilo.Indirizzo);
                command.Parameters.AddWithValue("@Citta", profilo.Citta);
                command.Parameters.AddWithValue("@CAP", profilo.CAP);
                command.Parameters.AddWithValue("@CodiceFiscale", profilo.CodiceFiscale);

                command.CommandText = "INSERT INTO ANAGRAFICA VALUES (@Cognome, @Nome, @Indirizzo, @Citta, @CAP, @CodiceFiscale)";
                command.Connection = con;
                command.ExecuteNonQuery();
            }catch (Exception ex)
            {

            }

            con.Close();

            return RedirectToAction("ListaAnagrafe");
        }

        public ActionResult ListaViolazioni()
        {
            Violazione.listaViolazioni.Clear();
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToPoliziaDB"].ToString();
                con.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM TIPOVIOLAZIONE";
                command.Connection = con;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Violazione violazione = new Violazione();
                    violazione.IDViolazione = Convert.ToInt32(reader["IDViolazione"]);
                    violazione.DescrizioneViolazione = reader["Descrizione"].ToString();
                    Violazione.listaViolazioni.Add(violazione);
                    
                }

            }
            catch (Exception ex)
            {

            }

            con.Close();

            return View(Violazione.listaViolazioni);
        }

        public ActionResult ListaVerbali()
        {
            Verbale.listaVerbali.Clear();
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToPoliziaDB"].ToString();
                con.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM VERBALE INNER JOIN TIPOVIOLAZIONE ON VERBALE.IDViolazione = TIPOVIOLAZIONE.IDViolazione INNER JOIN ANAGRAFICA ON VERBALE.IDAnagrafica = ANAGRAFICA.IDAnagrafica";
                command.Connection = con;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Verbale verbale = new Verbale();
                    verbale.IDVerbale = Convert.ToInt32(reader["IDVerbale"]);
                    verbale.DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                    verbale.IndirizzoViolazione = reader["IndirizzoViolazione"].ToString();
                    verbale.NominativoAgente = reader["NominativoAgente"].ToString();
                    //verbale.DataTrascrizione = Convert.ToDateTime(reader["DataTrascrizione"]);
                    verbale.Importo = Convert.ToDecimal(reader["Importo"]);
                    verbale.Decurtamento = Convert.ToInt32(reader["DecurtamentoPunti"]);
                    verbale.Violazione = reader["Descrizione"].ToString();
                    verbale.Profilo = reader["Cognome"].ToString() + " " + reader["Nome"].ToString();
                    Verbale.listaVerbali.Add(verbale);
                }
                    
            }catch(Exception ex)
            {

            }

            return View(Verbale.listaVerbali);
        }

        public ActionResult CreateVerbale()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateVerbale(Verbale verbale)
        {
            return RedirectToAction("ListaVerbali");
        }

        public ActionResult _VerbaliPerTrasgressore()
        {
            TotaleTrasgressore.totaleVerbali.Clear();
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToPoliziaDB"].ToString();
                con.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT COUNT(*) AS TotaleVerbali, Nome, Cognome  FROM VERBALE INNER JOIN ANAGRAFICA ON VERBALE.IDAnagrafica = ANAGRAFICA.IDAnagrafica GROUP BY Nome, Cognome";
                command.Connection = con;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TotaleTrasgressore verbale = new TotaleTrasgressore();
                    verbale.TotaleVerbali = Convert.ToInt32(reader["TotaleVerbali"]);
                    verbale.Profilo = reader["Cognome"].ToString() + " " + reader["Nome"].ToString();
                    TotaleTrasgressore.totaleVerbali.Add(verbale);
                }

            }
            catch (Exception ex)
            {

            }

            return PartialView(TotaleTrasgressore.totaleVerbali);
        }

        public ActionResult _TotalePuntiPerTrasgressore()
        {
            Verbale.listaVerbali.Clear();
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToPoliziaDB"].ToString();
                con.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT SUM(DecurtamentoPunti) AS TotalePunti, Nome, Cognome  FROM VERBALE INNER JOIN ANAGRAFICA ON VERBALE.IDAnagrafica = ANAGRAFICA.IDAnagrafica GROUP BY Nome, Cognome";
                command.Connection = con;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Verbale verbale = new Verbale();
                    verbale.Decurtamento = Convert.ToInt32(reader["TotalePunti"]);
                    verbale.Profilo = reader["Cognome"].ToString() + " " + reader["Nome"].ToString();
                    Verbale.listaVerbali.Add(verbale);
                }

            }
            catch (Exception ex)
            {

            }

            return PartialView(Verbale.listaVerbali);

        }

        public ActionResult _Violazioni10Punti()
        {
            Verbale.listaVerbali.Clear();
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToPoliziaDB"].ToString();
                con.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT Nome, Cognome, DataViolazione, Importo, DecurtamentoPunti  FROM VERBALE INNER JOIN ANAGRAFICA ON VERBALE.IDAnagrafica = ANAGRAFICA.IDAnagrafica WHERE DecurtamentoPunti >= 10 GROUP BY Nome, Cognome, DecurtamentoPunti, DataViolazione, Importo";
                command.Connection = con;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Verbale verbale = new Verbale();
                    verbale.Decurtamento = Convert.ToInt32(reader["DecurtamentoPunti"]);
                    verbale.Importo = Convert.ToDecimal(reader["Importo"]);
                    verbale.DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                    verbale.Profilo = reader["Cognome"].ToString() + " " + reader["Nome"].ToString();
                    Verbale.listaVerbali.Add(verbale);
                }

            }
            catch (Exception ex)
            {

            }

            return PartialView(Verbale.listaVerbali);
        }

        public ActionResult _Violazioni400Euro()
        {
            Verbale.listaVerbali.Clear();
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConToPoliziaDB"].ToString();
                con.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT Nome, Cognome, DataViolazione, Importo, DecurtamentoPunti  FROM VERBALE INNER JOIN ANAGRAFICA ON VERBALE.IDAnagrafica = ANAGRAFICA.IDAnagrafica WHERE Importo >= 400 GROUP BY Nome, Cognome, DecurtamentoPunti, DataViolazione, Importo";
                command.Connection = con;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Verbale verbale = new Verbale();
                    verbale.Decurtamento = Convert.ToInt32(reader["DecurtamentoPunti"]);
                    verbale.Importo = Convert.ToDecimal(reader["Importo"]);
                    verbale.DataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                    verbale.Profilo = reader["Cognome"].ToString() + " " + reader["Nome"].ToString();
                    Verbale.listaVerbali.Add(verbale);
                }

            }
            catch (Exception ex)
            {

            }

            return PartialView(Verbale.listaVerbali);
        }
    }
}