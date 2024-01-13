using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PracaDyplomowa.Models
{
    public class Reminder : DbConnection
    {
        public Reminder()
        {
        }

        public Reminder(string id, string jednostkaId)
        {
            GetReminder(id, jednostkaId);
        }
        public string Id { get; set; }
        public string JednostkaId { get; set; }
        public string Tytul { get; set; }
        public string Tresc { get; set; }
        public string DataRozpoczecia { get; set; }
        public string CzasRozpoczecia { get; set; }
        public string PowiadomienieEmail { get; set; }
        public string PowiadomienieTelefon { get; set; }
        public string TloHex { get; set; }
        public string CzcionkaHex { get; set; }




        private void GetReminder(string id, string jednostkaId)
        {
            var success = false;
            var numberOfAttempts = 6;
            
            string sqlsqlQuery = $"select * from osp_kalendarz Where id = :val1 AND jednostka_id = :val2";
            OracleConnection oracleConn = new OracleConnection(ConnString);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                  
                    oracleConn.Open();
                    Console.WriteLine("@Reminder @GetReminder()  Connected status: " + oracleConn.State +
                     $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", id);
                    cmd.Parameters.Add("val2", jednostkaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Reminder @GetReminder(): No data found");
                        oracleConn.Close();
                        return;
                    }

                    while (dataReader.Read())
                    {
                        Id = (dataReader["id"].ToString());
                        Tytul = (dataReader["tytul"].ToString());
                        Tresc = (dataReader["tresc"].ToString());
                        DataRozpoczecia = (dataReader["data_rozpoczecia"].ToString().Split(' ')[0]);
                        CzasRozpoczecia = (dataReader["czas_rozpoczecia"].ToString().Split(' ')[1]);
                        PowiadomienieEmail = (dataReader["powiadomienie_email"].ToString());
                        PowiadomienieTelefon = (dataReader["powiadomienie_telefon"].ToString());
                        TloHex = (dataReader["tlo_kolor_hex"].ToString());
                        CzcionkaHex = (dataReader["czcionka_kolor_hex"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("--->@GetReminder()" + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                }
            }
            oracleConn.Close();
            Console.WriteLine("Connection status: " + oracleConn.State);
        }




        public static List<Reminder> GetAllReminders(string jednostkaId)
        {
            List<Reminder> Li = new List<Reminder>();
            var success = false;
            var numberOfAttempts = 6;
           
            string sqlsqlQuery = "select * from osp_kalendarz WHERE jednostka_id = :val1 " +
                "AND powiadomienie_email='t' OR powiadomienie_telefon = 't'";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                  
                    oracleConn.Open();
                    Console.WriteLine("@Reminder @GetAllReminders()  Connected status: " + oracleConn.State +
                       $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Reminder @GetAllReminders(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Reminder tempReminder = new Reminder();
                        tempReminder.Id = (dataReader["id"].ToString());
                        tempReminder.Tytul = (dataReader["tytul"].ToString());
                        tempReminder.Tresc = (dataReader["tresc"].ToString());
                        tempReminder.DataRozpoczecia = (dataReader["data_rozpoczecia"].ToString());
                        tempReminder.CzasRozpoczecia = (dataReader["czas_rozpoczecia"].ToString());
                        tempReminder.PowiadomienieEmail = (dataReader["powiadomienie_email"].ToString());
                        tempReminder.PowiadomienieTelefon = (dataReader["powiadomienie_telefon"].ToString());
                        tempReminder.TloHex = (dataReader["tlo_kolor_hex"].ToString());
                        tempReminder.CzcionkaHex = (dataReader["czcionka_kolor_hex"].ToString());
                        Li.Add(tempReminder);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Reminder @GetAllReminders() Error:" + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("Connection status: " + oracleConn.State);
            return Li;
        }


        public static List<Reminder> GetAllCurrentReminders(string jednostkaId = null)
        {
            List<Reminder> Li = new List<Reminder>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = jednostkaId != null ? "Select * from osp_kalendarz WHERE jednostka_id = :val1 " +
               "AND  czas_rozpoczecia > sysdate " +
               "AND (powiadomienie_email='t' OR powiadomienie_telefon = 't')" 
               : "Select * from osp_kalendarz WHERE czas_rozpoczecia > sysdate " +
               "AND (powiadomienie_email='t' OR powiadomienie_telefon = 't')";

           
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    
                    oracleConn.Open();
                    Console.WriteLine("@Reminder @GetAllCurrentReminders()  Connected status: " + oracleConn.State +
                     $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    if(jednostkaId !=null)
                        cmd.Parameters.Add("val1", jednostkaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Reminder @GetAllCurrentReminders(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Reminder tempUser = new Reminder();
                        tempUser.Id = (dataReader["id"].ToString());
                        tempUser.Tytul = (dataReader["tytul"].ToString());
                        tempUser.Tresc = (dataReader["tresc"].ToString());
                        tempUser.DataRozpoczecia = (dataReader["data_rozpoczecia"].ToString());
                        tempUser.CzasRozpoczecia = (dataReader["czas_rozpoczecia"].ToString());
                        tempUser.PowiadomienieEmail = (dataReader["powiadomienie_email"].ToString());
                        tempUser.PowiadomienieTelefon = (dataReader["powiadomienie_telefon"].ToString());
                        tempUser.TloHex = (dataReader["tlo_kolor_hex"].ToString());
                        tempUser.CzcionkaHex = (dataReader["czcionka_kolor_hex"].ToString());
                        Li.Add(tempUser);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Reminder @GetAllCurrentReminders() Error" + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("Connection status: " + oracleConn.State);
            return Li;
        }

        public static (List<string> id, List<string> email )getReminderRecipients(string reminder_id)
        {
            List<string> idNumberLi = new List<string>();
            List<string> emailLi = new List<string>();
            var success = false;
            var numberOfAttempts = 6;
            
            string sqlsqlQuery = "select osp_przypomnienie_adresat.uzytkownik_id, osp_uzytkownik.email " +
                "from osp_przypomnienie_adresat join osp_uzytkownik " +
                "on osp_przypomnienie_adresat.uzytkownik_id = osp_uzytkownik.uzytkownik_id " +
                "WHERE przypomnienie_id = :val1";
                
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                   
                    oracleConn.Open();
                    Console.WriteLine("@Reminder @getReminderRecipients()  Connected status: " + oracleConn.State +
                     $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", reminder_id);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Reminder @getReminderRecipients(): No data found");
                        oracleConn.Close();
                        return (idNumberLi , emailLi);
                    }

                    while (dataReader.Read())
                    {
                        idNumberLi.Add(dataReader["uzytkownik_id"].ToString());
                        emailLi.Add(dataReader["email"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Reminder @getReminderRecipients()" + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("Connection status: " + oracleConn.State);
            return (idNumberLi, emailLi);
        }

        public static string UpdateReminder(Reminder obj, List<string> listOfRecipients)
        {
            var success = false;
            var numberOfAttempts = 6;
            var returnedID = "";

            var sqlQuery = "UPDATE OSP_KALENDARZ SET tytul = :val1 , tresc = :val2 " +
                ", data_rozpoczecia = TO_DATE( :val3 , 'YYYY-MM-DD'), czas_rozpoczecia = TO_TIMESTAMP( :val4 , 'YYYY-MM-DD HH24:MI:SS') " +
                ", powiadomienie_email = :val5, powiadomienie_telefon = :val6 " +
                " WHERE id = :val7 RETURNING id INTO: returnInsertedRowId ";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Reminder @UpdateReminder()  Connected status: " + oracleConn.State +
                     $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    //await Task.Run(() => cmd.ExecuteNonQuery());

                    cmd.Parameters.Add("val1", obj.Tytul);
                    cmd.Parameters.Add("val2", obj.Tresc);
                    cmd.Parameters.Add("val3", obj.DataRozpoczecia);
                    cmd.Parameters.Add("val4", obj.CzasRozpoczecia);
                    cmd.Parameters.Add("val5", obj.PowiadomienieEmail);
                    cmd.Parameters.Add("val6", obj.PowiadomienieTelefon);
                    cmd.Parameters.Add("val7", obj.Id);// int.Parse(id));
                    cmd.Parameters.Add(":returnInsertedRowId", OracleDbType.Decimal, ParameterDirection.Output);


                    cmd.ExecuteNonQuery();
                    returnedID = (cmd.Parameters[":returnInsertedRowId"].Value).ToString();
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@Reminder @UpdateReminder() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }

            RemoveReminderRecipients(returnedID);
            CalendarEvent.InsertRecipients(returnedID, listOfRecipients);
            Console.WriteLine("@Reminder @UpdateReminder(): Success");
            oracleConn.Close();
            return "(✓) Powodzenie";
        }

        public static string hideReminder(string id)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "UPDATE OSP_KALENDARZ SET powiadomienie_email = :val1, powiadomienie_telefon = :val2 " +
                " WHERE id = :val3 ";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Reminder @hideReminder()  Connected status: " + oracleConn.State +
                     $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    //await Task.Run(() => cmd.ExecuteNonQuery());

                    cmd.Parameters.Add("val1", "n");
                    cmd.Parameters.Add("val2", "n");
                    cmd.Parameters.Add("val3", int.Parse(id));

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@Reminder @hideReminder() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            RemoveReminderRecipients(id);
            Console.WriteLine("@Reminder @hideReminder(): Success");
            oracleConn.Close();
            return "(✓) Usunięto";
        }

        public static string RemoveReminderRecipients(string id)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "DELETE FROM osp_przypomnienie_adresat WHERE przypomnienie_id = :val1";


            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Reminder @RemoveReminderRecipients()  Connected status: " + oracleConn.State +
                     $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    //await Task.Run(() => cmd.ExecuteNonQuery());
                    cmd.Parameters.Add("val1", id);


                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@Reminder @RemoveReminderRecipients() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@Reminder @RemoveReminderRecipients(): Success");
            oracleConn.Close();
            return "(✓) Usunięto";
        }

        public static string RemoveReminderRecipientsByMemberId(string id)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "DELETE FROM osp_przypomnienie_adresat WHERE uzytkownik_id = :val1";


            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Reminder @RemoveReminderRecipientsByMemberId()  Connected status: " + oracleConn.State +
                     $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    //await Task.Run(() => cmd.ExecuteNonQuery());
                    cmd.Parameters.Add("val1", id);


                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@Reminder @RemoveReminderRecipientsByMemberId() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@Reminder @RemoveReminderRecipientsByMemberId(): Success");
            oracleConn.Close();
            return "(✓) Usunięto";
        }

    }
}
