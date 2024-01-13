using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PracaDyplomowa.Models
{

    public class NotificationType
    {
        private NotificationType() { }
        public static readonly string AwaitingApproval = "Oczekuje zatwierdzenia";
        public static readonly string UpdatedStatus = "Zmiana statusu";
        public static readonly string OverdueUserDeadline = "Zaległe badania";
        public static readonly string FewDaysLeftUserDeadline = " Zbliżają się badania"; //3 dni
        public static readonly string OverdueEquipmentDeadline = "Zaległy przegląd";
        public static readonly string FewDaysLeftEquipmentDeadline = " Zbliża się przegląd"; //3 dni
    }

    public class StandbyStatus
    {
        private StandbyStatus() { }
        public static readonly string Dostepny = "Dostępny";
        public static readonly string Niedostepny = "Niedostępny";
    }

    public class Notification : DbConnection
    {
        public string UzytkownikId { get; set; }
        public string Jednostka_id { get; set; } //foreign key
        public string TypKonta { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public string Haslo { get; set; }
        public string Telefon { get; set; }
        public string Funkcja { get; set; }
        public string DataWstapienia { get; set; }
        public string Zatwierdzony { get; set; }
        public string DataUtworzenia { get; set; }
        public string Dotyczy { get; set; }
        public string NowyStatus { get; set; }
        public string NowyStatusId { get; set; }


        public static List<Notification> getUsersToApprove(string jednostkaId)
        {
            List<Notification> Li = new List<Notification>();
            var success = false;
            var numberOfAttempts = 6;
            Console.WriteLine("--->@getUsersToApprove()");
            string sqlsqlQuery = "SELECT * from osp_uzytkownik WHERE jednostka_id = :val1 " +
                "AND zatwierdzony = 'n'";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    
                    oracleConn.Open();
                    Console.WriteLine("@Notification @getUsersToApprove()  Connected status: " + oracleConn.State +
                           $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Notification tempNotification = new Notification();
                        tempNotification.UzytkownikId = (dataReader["uzytkownik_id"].ToString());
                        tempNotification.Jednostka_id = (dataReader["jednostka_id"].ToString());
                        //tempNotification.TypKonta = (dataReader["typ_konta"].ToString());
                        tempNotification.Imie = (dataReader["imie"].ToString());
                        tempNotification.Nazwisko = (dataReader["nazwisko"].ToString());
                        tempNotification.Email = (dataReader["email"].ToString());
                        tempNotification.DataUtworzenia = (dataReader["data_utworzenia"].ToString());
                        //tempNotification.Haslo = (dataReader["haslo"].ToString());
                        //tempNotification.Telefon = (dataReader["telefon"].ToString());
                        //tempNotification.Funkcja = (dataReader["funkcja"].ToString());
                        //tempNotification.DataWstapienia = (dataReader["data_wstapienia"].ToString());
                        //tempNotification.Zatwierdzony = (dataReader["zatwierdzony"].ToString());
                        tempNotification.Dotyczy = NotificationType.AwaitingApproval;
                        Li.Add(tempNotification);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Notification @getUsersToApprove()" + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("Connection status: " + oracleConn.State);
            return Li;
        }

        public static string approveUser(string userId)
        {
            var success = false;
            var numberOfAttempts = 6;

            string sqlQuery = " UPDATE osp_uzytkownik SET zatwierdzony = 't' WHERE uzytkownik_id = :val1";
            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", userId);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@Notification @approveUser(): " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@Notification @approveUser(): Success");
            oracleConn.Close();
            return "(✓) Zatwierdzono użytkownika";
        }

        public static List<Notification> getUserStatusUpdates(string jednostkaId)
        {
            List<Notification> Li = new List<Notification>();
            var success = false;
            var numberOfAttempts = 6;
            Console.WriteLine("--->@getUsersToApprove()");
            string sqlsqlQuery = "SELECT * from osp_status_update INNER JOIN osp_uzytkownik " +
                "ON osp_status_update.uzytkownik_id = osp_uzytkownik.uzytkownik_id " +
                "WHERE osp_status_update.jednostka_id = :val1 ";
              
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Notification @getUserStatusUpdates()  Connected status: " + oracleConn.State +
                           $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Notification tempNotification = new Notification();
                        tempNotification.NowyStatusId = (dataReader["id"].ToString());
                        tempNotification.UzytkownikId = (dataReader["uzytkownik_id"].ToString());
                        tempNotification.Jednostka_id = (dataReader["jednostka_id"].ToString());
                        tempNotification.Imie = (dataReader["imie"].ToString());
                        tempNotification.Nazwisko = (dataReader["nazwisko"].ToString());
                        tempNotification.Email = (dataReader["email"].ToString());
                        tempNotification.NowyStatus = (dataReader["nowy_status"].ToString().ToLower());
                        if (tempNotification.NowyStatus == "niedostepny" || tempNotification.NowyStatus == "niedostępny")
                            tempNotification.NowyStatus = StandbyStatus.Niedostepny;
                        else if (tempNotification.NowyStatus == "dostepny" || tempNotification.NowyStatus == "dostępny")
                            tempNotification.NowyStatus = StandbyStatus.Dostepny;
                        else
                            tempNotification.NowyStatus = "Nienznay";
                        tempNotification.DataUtworzenia = (dataReader["data_zmiany"].ToString());
                        tempNotification.Dotyczy = NotificationType.UpdatedStatus;
                        Li.Add(tempNotification);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Notification @getUserStatusUpdates() Error:" + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            
            return Li;
        }

        public static List<Notification> GetOverdueEquipmentDeadlines(string jednostkaId)
        {
            List<Notification> Li = new List<Notification>();
            var success = false;
            var numberOfAttempts = 6;
            string sqlsqlQuery = $"select * from osp_inwentarz WHERE jednostka_id = :val1 " +
                $"AND badania_koniec <  CURRENT_TIMESTAMP";

            string sqlsqlQuery2 = $"select * from osp_inwentarz WHERE jednostka_id = :val1 " +
            $"AND badania_koniec <   CURRENT_TIMESTAMP + interval '3' day AND badania_koniec >  CURRENT_TIMESTAMP";


            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Notification @GetOverdueEquipmentDeadlines()  Connected status: " + oracleConn.State +
                        $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            Notification tempNotification = new Notification();
                            tempNotification.UzytkownikId = (dataReader["id"].ToString());
                            tempNotification.Jednostka_id = (dataReader["jednostka_id"].ToString());
                            tempNotification.Imie = (dataReader["producent"].ToString());
                            tempNotification.Nazwisko = (dataReader["nazwa_modelu"].ToString());
                            tempNotification.Email = (dataReader["sn"].ToString());
                            tempNotification.DataUtworzenia = Convert.ToDateTime(dataReader["badania_koniec"]).ToString("dd.MM.yyyy");
                            tempNotification.Dotyczy = NotificationType.OverdueEquipmentDeadline;
                            Li.Add(tempNotification);
                        }
                    }


                    cmd = new OracleCommand(sqlsqlQuery2, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            Notification tempNotification = new Notification();
                            tempNotification.UzytkownikId = (dataReader["id"].ToString());
                            tempNotification.Jednostka_id = (dataReader["jednostka_id"].ToString());
                            tempNotification.Imie = (dataReader["producent"].ToString());
                            tempNotification.Nazwisko = (dataReader["nazwa_modelu"].ToString());
                            tempNotification.Email = (dataReader["sn"].ToString());
                            tempNotification.DataUtworzenia = Convert.ToDateTime(dataReader["badania_koniec"]).ToString("dd.MM.yyyy");
                            tempNotification.Dotyczy = NotificationType.FewDaysLeftEquipmentDeadline;
                            Li.Add(tempNotification);
                        }
                    }

                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Notification @GetOverdueEquipmentDeadlines()  Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Notification @GetOverdueEquipmentDeadlines(): Success");
            return Li;
        }

        public static List<Notification> GetOverdueUserDeadlines(string jednostkaId)
        {
            List<Notification> Li = new List<Notification>();
            var success = false;
            var numberOfAttempts = 6;
            string sqlsqlQuery = $"select * from osp_uzytkownik WHERE jednostka_id = :val1 " +
                $"AND waznosc_badan < CURRENT_TIMESTAMP";

            string sqlsqlQuery2 = $"select * from osp_uzytkownik WHERE jednostka_id = :val1 " +
               $"AND waznosc_badan < CURRENT_TIMESTAMP + interval '3' day AND waznosc_badan >  CURRENT_TIMESTAMP";

            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Notification @GetOverdueUserDeadlines()  Connected status: " + oracleConn.State +
                        $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {

                        while (dataReader.Read())
                        {
                            Notification tempNotification = new Notification();
                            tempNotification.UzytkownikId = (dataReader["uzytkownik_id"].ToString());
                            tempNotification.Jednostka_id = (dataReader["jednostka_id"].ToString());
                            tempNotification.Imie = (dataReader["imie"].ToString());
                            tempNotification.Nazwisko = (dataReader["nazwisko"].ToString());
                            tempNotification.Email = (dataReader["email"].ToString());
                            tempNotification.DataUtworzenia = Convert.ToDateTime(dataReader["waznosc_badan"]).ToString("dd.MM.yyyy");
                            tempNotification.Dotyczy = NotificationType.OverdueUserDeadline;
                            Li.Add(tempNotification);
                        }
                    }

                    cmd = new OracleCommand(sqlsqlQuery2, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {

                        while (dataReader.Read())
                        {
                            Notification tempNotification = new Notification();
                            tempNotification.UzytkownikId = (dataReader["uzytkownik_id"].ToString());
                            tempNotification.Jednostka_id = (dataReader["jednostka_id"].ToString());
                            tempNotification.Imie = (dataReader["imie"].ToString());
                            tempNotification.Nazwisko = (dataReader["nazwisko"].ToString());
                            tempNotification.Email = (dataReader["email"].ToString());
                            tempNotification.DataUtworzenia = Convert.ToDateTime(dataReader["waznosc_badan"]).ToString("dd.MM.yyyy");
                            tempNotification.Dotyczy = NotificationType.FewDaysLeftUserDeadline;
                            Li.Add(tempNotification);
                        }
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Notification @GetOverdueUserDeadlines()  Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Notification @GetOverdueUserDeadlines(): Success");
            return Li;
        }


        public static string RemoveNewUserStatus(string id)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "DELETE FROM osp_status_update WHERE id = :val1";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", id);


                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@Notification @RemoveNewUserStatus() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@Notification @RemoveNewUserStatus(): Success");
            oracleConn.Close();
            return "(✓) Usunięto";
        }

        public static string RemoveNewUserStatusByMemberId(string id)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "DELETE FROM osp_status_update WHERE uzytkownik_id = :val1";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", id);


                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@Notification @RemoveNewUserStatusByMemberId() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@Notification @RemoveNewUserStatusByMemberId(): Success");
            oracleConn.Close();
            return "(✓) Usunięto";
        }

    }
}
