using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PracaDyplomowa.Models
{
    public class Announcement : DbConnection
    {
        public Announcement() { }
        public string Id { get; set; }
        public string JednostkaId { get; set; }
        public string Tytul { get; set; }
        public string Tresc { get; set; }
        public string TloHex { get; set; }
        public string CzcionkaHex { get; set; }
        public string Data { get; set; }
        public string Autor { get; set; }
        public string DokumentNazwa { get; set; }




        public static List<Announcement> GetAllAnnouncements(string jednostkaId)
        {
            List<Announcement> Li = new List<Announcement>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = "select * from osp_ogloszenie WHERE jednostka_id = :val1 ";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();

                    Console.WriteLine("@Announcement @GetAllAnnouncements()  Connected status: " + oracleConn.State +
                      $" [Approach: {7 - numberOfAttempts}]");

                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Announcement @GetAllAnnouncements(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Announcement tempAction = new Announcement();
                        tempAction.Id = (dataReader["id"].ToString());
                        tempAction.Tytul = (dataReader["tytul"].ToString());
                        tempAction.Tresc = (dataReader["tresc"].ToString());
                        tempAction.TloHex = (dataReader["tlo_kolor_hex"].ToString());
                        tempAction.CzcionkaHex = (dataReader["czcionka_kolor_hex"].ToString());
                        tempAction.Data = (dataReader["czas_rozpoczecia"].ToString());
                        tempAction.Autor = (dataReader["autor"].ToString());
                        tempAction.DokumentNazwa = (dataReader["dokument_nazwa"].ToString());

                        Li.Add(tempAction);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Announcement @GetAllAnnouncements() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Announcement @GetAllAnnouncements():  Success");
            return Li;
        }


        public static List<Announcement> GetAnnouncementsByMinDate(string jednostkaId, string minDate)
        {
            List<Announcement> Li = new List<Announcement>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = "select * from osp_ogloszenie WHERE jednostka_id = :val1 " +
                "AND czas_rozpoczecia >= TO_TIMESTAMP(:val2, 'DD-MM-YYYY HH24:MI:SS')";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();

                    Console.WriteLine("@Announcement @GetAnnouncementsByMinDate()  Connected status: " + oracleConn.State +
                      $" [Approach: {7 - numberOfAttempts}]");

                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    cmd.Parameters.Add("val2", minDate);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Announcement @GetAnnouncementsByMinDate(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Announcement tempAction = new Announcement();
                        tempAction.Id = (dataReader["id"].ToString());
                        tempAction.Tytul = (dataReader["tytul"].ToString());
                        tempAction.Tresc = (dataReader["tresc"].ToString());
                        tempAction.TloHex = (dataReader["tlo_kolor_hex"].ToString());
                        tempAction.CzcionkaHex = (dataReader["czcionka_kolor_hex"].ToString());
                        tempAction.Data = (dataReader["czas_rozpoczecia"].ToString());
                        tempAction.Autor = (dataReader["autor"].ToString());
                        tempAction.DokumentNazwa = (dataReader["dokument_nazwa"].ToString());

                        Li.Add(tempAction);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Announcement @GetAnnouncementsByMinDate() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Announcement @GetAnnouncementsByMinDate():  Success");
            return Li;
        }



        public static List<Announcement> Get2FirstAnnouncements(string jednostkaId)
        {
            List<Announcement> Li = new List<Announcement>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = "select * from osp_ogloszenie WHERE jednostka_id = :val1 " +
                "ORDER BY czas_rozpoczecia DESC FETCH FIRST 2 ROWS ONLY  ";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();

                    Console.WriteLine("@Announcement @GetAllAnnouncements()  Connected status: " + oracleConn.State +
                      $" [Approach: {7 - numberOfAttempts}]");

                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Announcement @GetAllAnnouncements(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Announcement tempAction = new Announcement();
                        tempAction.Id = (dataReader["id"].ToString());
                        tempAction.Tytul = (dataReader["tytul"].ToString());
                        tempAction.Tresc = (dataReader["tresc"].ToString());
                        tempAction.TloHex = (dataReader["tlo_kolor_hex"].ToString());
                        tempAction.CzcionkaHex = (dataReader["czcionka_kolor_hex"].ToString());
                        tempAction.Data = (dataReader["czas_rozpoczecia"].ToString());
                        tempAction.Autor = (dataReader["autor"].ToString());

                        Li.Add(tempAction);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Announcement @GetAllAnnouncements() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Announcement @GetAllAnnouncements():  Success");
            return Li;
        }


        public static string InsertAnnouncement(Announcement obj)
        {
            var success = false;
            var numberOfAttempts = 6;
          
            var sqlQuery = "INSERT INTO osp_ogloszenie(jednostka_id, tytul, tresc, " +
                "czcionka_kolor_hex, tlo_kolor_hex , autor , dokument_nazwa) " +
                "VALUES (:val1 , :val2, :val3, :val4, :val5, :val6 , :val7 ) ";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Announcement @InsertAnnouncement()  Connected status: " + oracleConn.State +
                            $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", obj.JednostkaId);
                    cmd.Parameters.Add("val2", obj.Tytul);
                    cmd.Parameters.Add("val3", obj.Tresc);
                    cmd.Parameters.Add("val4", obj.CzcionkaHex);
                    cmd.Parameters.Add("val5", obj.TloHex);
                    cmd.Parameters.Add("val6", obj.Autor);
                    cmd.Parameters.Add("val7", obj.DokumentNazwa);


                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@Announcement @InsertAnnouncement() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
         

            Console.WriteLine("@Announcement @InsertAnnouncement(): Success");
            oracleConn.Close();
            return "(✓) Dodano Ogloszenie";
        }


        public static string DeleteAnnouncement(string id)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "DELETE FROM osp_ogloszenie WHERE id = :val1";


            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Announcement @DeleteAnnouncement()  Connected status: " + oracleConn.State +
                       $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", int.Parse(id));


                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@Announcement @DeleteAnnouncement() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@Announcement @DeleteAnnouncement(): Success");
            oracleConn.Close();
            return "(✓) Usunięto";
        }


        public static string GetNumberOfUnreadAnnouncements(string unitId, string lastVisitDate)
        {
            var success = false;
            var numberOfAttempts = 6;
            string count = "";
            string sqlsqlQuery = "Select COUNT(*) FROM osp_ogloszenie " +
                "Where jednostka_id = :val1 AND czas_rozpoczecia > TO_TIMESTAMP(:val2, 'YYYY//MM/DD HH24:MI:SS,FF9') ";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();
                    Console.WriteLine("@User @GetNumberOfUnreadAnnouncements()  Connected status: " + oracleConn.State +
                       $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", unitId);
                    cmd.Parameters.Add("val2", lastVisitDate);
                    count = cmd.ExecuteScalar().ToString();

                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@User @GetNumberOfUnreadAnnouncements() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@User @GetNumberOfUnreadAnnouncements(): Success");
            return count;
        }

    }
}
