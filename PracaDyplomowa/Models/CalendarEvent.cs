using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PracaDyplomowa.Models
{
    public class CalendarEvent : DbConnection
    {
        public CalendarEvent()
        {
        }

        public CalendarEvent(string id, string jednostkaId)
        {
            GetEvent(id, jednostkaId);
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
        public string UzytkownikId { get; set; }
        public string SprzetId { get; set; }

        private void GetEvent(string id, string jednostkaId)
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
                    Console.WriteLine("@CalendarEvent @GetEvent()  Connected status: " + oracleConn.State +
                           $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", id);
                    cmd.Parameters.Add("val2", jednostkaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("No data found");
                        oracleConn.Close();
                        return;
                    }

                    while (dataReader.Read())
                    {
                        Id = (dataReader["id"].ToString());
                        Tytul = (dataReader["tytul"].ToString());
                        Tresc = (dataReader["tresc"].ToString());
                        DataRozpoczecia = (dataReader["data_rozpoczecia"].ToString());
                        CzasRozpoczecia = (dataReader["czas_rozpoczecia"].ToString());
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
                        Console.WriteLine("--->@GetEvent()"+ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                }
            }
            oracleConn.Close();
        }


        private static string GetEventIdByUserId(string userId)
        {
            var success = false;
            var numberOfAttempts = 6;
            string eventId="";
            string sqlsqlQuery = $"select id from osp_kalendarz Where uzytkownik_id = :val1";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();
                    Console.WriteLine("@CalendarEvent @GetEventIdByUserId()  Connected status: " + oracleConn.State +
                           $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", userId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("No data found");
                        oracleConn.Close();
                        return null;
                    }

                    while (dataReader.Read())
                    {
                        eventId = (dataReader["id"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@CalendarEvent @GetEventIdByUserId() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                }
            }
            oracleConn.Close();
            return eventId;
        }



        public static string RemoveEvent(string id)
        {
            Reminder.RemoveReminderRecipients(id);

            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "DELETE FROM osp_kalendarz WHERE id = :val1";


            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@CalendarEvent @RemoveEvent()  Connected status: " + oracleConn.State +
                           $" [Approach: {7 - numberOfAttempts}]");
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
                        Console.WriteLine("@CalendarEvent @RemoveEvent() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
        
            Console.WriteLine("@CalendarEvent @RemoveEvent(): Success");
            oracleConn.Close();
            return "(✓) Usunięto";
        }


        public static string RemoveEventByUserIdRelation(string id)
        {

            Reminder.RemoveReminderRecipients(GetEventIdByUserId(id));

            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "DELETE FROM osp_kalendarz WHERE uzytkownik_id = :val1";


            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@CalendarEvent @RemoveEventByUserIdRelation()  Connected status: " + oracleConn.State +
                           $" [Approach: {7 - numberOfAttempts}]");
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
                        Console.WriteLine("@CalendarEvent @RemoveEventByUserIdRelation() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }


            Console.WriteLine("@CalendarEvent @RemoveEventByUserIdRelation(): Success");
            oracleConn.Close();
            return "(✓) Usunięto";
        }



        public static string RemoveEventByEquipmentIdRelation(string id)
        {
            Reminder.RemoveReminderRecipients(id);

            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "DELETE FROM osp_kalendarz WHERE sprzet_id = :val1";


            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@CalendarEvent @RemoveEventByEquipmentIdRelation()  Connected status: " + oracleConn.State +
                           $" [Approach: {7 - numberOfAttempts}]");
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
                        Console.WriteLine("@CalendarEvent @RemoveEventByEquipmentIdRelation() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }


            Console.WriteLine("@CalendarEvent @RemoveEventByEquipmentIdRelation(): Success");
            oracleConn.Close();
            return "(✓) Usunięto";
        }


        public static string insertEvent(CalendarEvent obj , List<string> listOfRecipients)
        {
            var success = false;
            var numberOfAttempts = 6;
            var returnedID = "";

            var sqlQuery = "INSERT INTO OSP_KALENDARZ (jednostka_id, tytul , tresc, data_rozpoczecia, czas_rozpoczecia,  " +
                "powiadomienie_email, powiadomienie_telefon, tlo_kolor_hex, czcionka_kolor_hex , uzytkownik_id, sprzet_id)" +
                "values( :val1 , :val2 , :val3 , TO_DATE( :val4 , 'YYYY-MM-DD'), " +
                "TO_TIMESTAMP( :val5 , 'YYYY-MM-DD HH24:MI'), :val6 , :val7 , :val8 , :val9, :val10, :val11 )"+
                " RETURNING id INTO :returnInsertedRowId ";


            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@CalendarEvent @insertEvent()  Connected status: " + oracleConn.State +
                           $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    
                    cmd.Parameters.Add("val1", obj.JednostkaId);
                    cmd.Parameters.Add("val2", obj.Tytul);
                    cmd.Parameters.Add("val3", obj.Tresc);
                    cmd.Parameters.Add("val4", obj.DataRozpoczecia);
                    cmd.Parameters.Add("val5", obj.CzasRozpoczecia);
                    cmd.Parameters.Add("val6", obj.PowiadomienieEmail);
                    cmd.Parameters.Add("val7", obj.PowiadomienieTelefon);
                    cmd.Parameters.Add("val8", obj.TloHex);
                    cmd.Parameters.Add("val9", obj.CzcionkaHex);
                    cmd.Parameters.Add("val10", obj.UzytkownikId);
                    cmd.Parameters.Add("val11", obj.SprzetId);
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
                        Console.WriteLine("@CalendarEvent @insertEvent() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }

            InsertRecipients(returnedID, listOfRecipients);

            Console.WriteLine("@CalendarEvent @insertEvent(): Success");
            oracleConn.Close();
            return "(✓) Dodano";
        }


        public static string InsertRecipients(string reminder_id, List<string> listOfRecipients)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "INSERT INTO osp_przypomnienie_adresat(przypomnienie_id, uzytkownik_id) VALUES " +
            "(:val1 , :val2 )";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@CalendarEvent @InsertRecipients()  Connected status: " + oracleConn.State +
                           $" [Approach: {7 - numberOfAttempts}]");
                    foreach (var participant in listOfRecipients)
                    {
                        OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                        cmd.Parameters.Add("val1", reminder_id);
                        cmd.Parameters.Add("val2", participant);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@CalendarEvent @InsertRecipients() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@CalendarEvent @InsertRecipients(): Success");
            oracleConn.Close();
            return "(✓) Dodano uczestnika";
        }


   






        //Nested Class
        public class AllCalendarEvent : DbConnection
        {
            public AllCalendarEvent()
            {
            }
            public AllCalendarEvent(string jednostkaId)
            {
                GetAllEvents(jednostkaId);
            }
            public AllCalendarEvent(string jednostkaId, string dateX)
            {
                GetAllEventsByDate(jednostkaId, dateX);
            }
            public AllCalendarEvent(string jednostkaId, string dateElement , string partDate)
            {
                GetAllEventsByPartDate(jednostkaId , dateElement , partDate);
            }

            public List<CalendarEvent> Li { get; set; } = new List<CalendarEvent>();

            private void GetAllEvents(string jednostkaId)
            {
                var success = false;
                var numberOfAttempts = 6;
             
                string sqlsqlQuery = "select * from osp_kalendarz WHERE jednostka_id = :val1";
                OracleConnection oracleConn = new OracleConnection(ConnString);
                while (!success && numberOfAttempts > 1)
                {
                    try
                    {
                        success = true;
                       
                        oracleConn.Open();
                        Console.WriteLine("@CalendarEvent @GetAllEvents()  Connected status: " + oracleConn.State +
                           $" [Approach: {7 - numberOfAttempts}]");
                        OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                        cmd.Parameters.Add("val1", jednostkaId);
                        OracleDataReader dataReader = cmd.ExecuteReader();
                        if (!dataReader.HasRows)
                        {
                            Console.WriteLine("No data found");
                            oracleConn.Close();
                            return;
                        }

                        while (dataReader.Read())
                        {
                            CalendarEvent tempUser = new CalendarEvent();
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
                            Console.WriteLine("@CalendarEvent @GetAllEvents() Error: " + ex.Message);
                        else
                            Thread.Sleep(10000 / numberOfAttempts);
                        oracleConn.Close();
                    }
                }
                oracleConn.Close();
            }

            private void GetAllCurrentEvents(string jednostkaId)
            {
                var success = false;
                var numberOfAttempts = 6;
               
                string sqlsqlQuery = "SELECT * FROM osp_kalendarz WHERE jednostka_id = :val1 " +
                    "AND  czas_rozpoczecia > sysdate";
                OracleConnection oracleConn = new OracleConnection(ConnString);
                while (!success && numberOfAttempts > 1)
                {
                    try
                    {
                        success = true;
                        
                        oracleConn.Open();
                        Console.WriteLine("@CalendarEvent @GetAllCurrentEvents()  Connected status: " + oracleConn.State +
                           $" [Approach: {7 - numberOfAttempts}]");
                        OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                        cmd.Parameters.Add("val1", jednostkaId);

                        OracleDataReader dataReader = cmd.ExecuteReader();
                        if (!dataReader.HasRows)
                        {
                            Console.WriteLine("No data found");
                            oracleConn.Close();
                            return;
                        }

                        while (dataReader.Read())
                        {
                            CalendarEvent tempUser = new CalendarEvent();
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
                            Console.WriteLine("@CalendarEvent @GetAllCurrentEvents() Error:" + ex.Message);
                        else
                            Thread.Sleep(10000 / numberOfAttempts);
                        oracleConn.Close();
                    }
                }
                oracleConn.Close();
            }

            private void GetAllEventsByPartDate(string jednostkaId, string dateElement, string partDate)
            {
                var success = false;
                var numberOfAttempts = 6;
               
                string sqlsqlQuery = "select * from osp_kalendarz WHERE jednostka_id = :val1 AND " +
                   $" EXTRACT ( {dateElement} FROM data_rozpoczecia ) = :val2 ";
                OracleConnection oracleConn = new OracleConnection(ConnString);
                while (!success && numberOfAttempts > 1)
                {
                    try
                    {
                        success = true;
                       
                        oracleConn.Open();
                        Console.WriteLine("@CalendarEvent @GetAllEventsByPartDate()  Connected status: " + oracleConn.State +
                            $" [Approach: {7 - numberOfAttempts}]");
                        OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                        cmd.Parameters.Add("val1", jednostkaId);
                        cmd.Parameters.Add("val2", partDate);
                        OracleDataReader dataReader = cmd.ExecuteReader();
                        if (!dataReader.HasRows)
                        {
                            Console.WriteLine("No data found");
                            oracleConn.Close();
                            return;
                        }

                        while (dataReader.Read())
                        {
                            CalendarEvent tempUser = new CalendarEvent();
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
                            Console.WriteLine("@CalendarEvent @GetAllEventsByPartDate() Error: " + ex.Message);
                        else
                            Thread.Sleep(10000 / numberOfAttempts);
                        oracleConn.Close();
                    }
                }
                oracleConn.Close();
            }

            private void GetAllEventsByDate(string jednostkaId, string dateX)
            {
                var success = false;
                var numberOfAttempts = 6;
               
                string sqlsqlQuery = "select * from osp_kalendarz WHERE jednostka_id = :val1 AND " +
                   " data_rozpoczecia = TO_Date(:val2, 'DD-MM-YYYY')";
                OracleConnection oracleConn = new OracleConnection(ConnString);
                while (!success && numberOfAttempts > 1)
                {
                    try
                    {
                        success = true;
                      
                        oracleConn.Open();
                        Console.WriteLine("@CalendarEvent @GetAllEventsByDate()  Connected status: " + oracleConn.State +
                            $" [Approach: {7 - numberOfAttempts}]");
                        OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                        cmd.Parameters.Add("val1", jednostkaId);
                        cmd.Parameters.Add("val2", dateX);
                        OracleDataReader dataReader = cmd.ExecuteReader();
                        if (!dataReader.HasRows)
                        {
                            Console.WriteLine("No data found");
                            oracleConn.Close();
                            return;
                        }

                        while (dataReader.Read())
                        {
                            CalendarEvent tempUser = new CalendarEvent();
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
                            Console.WriteLine("@CalendarEvent @GetAllEventsByDate() Error:" + ex.Message);
                        else
                            Thread.Sleep(10000 / numberOfAttempts);
                        oracleConn.Close();
                    }
                }
                oracleConn.Close();
            }

            public static string GetAllEvents4Today(string jednostkaId)
            {
                var success = false;
                var numberOfAttempts = 6;
                var count = "0";
                string sqlsqlQuery = "Select COUNT(*) FROM osp_kalendarz WHERE jednostka_id = :val1 AND " +
                   " data_rozpoczecia = TO_Date(:val2, 'DD-MM-YYYY')";
                OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
                while (!success && numberOfAttempts > 1)
                {
                    try
                    {
                        success = true;

                        oracleConn.Open();
                        Console.WriteLine("@CalendarEvent @GetAllEvents4Today()  Connected status: " + oracleConn.State +
                            $" [Approach: {7 - numberOfAttempts}]");
                        OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                        cmd.Parameters.Add("val1", jednostkaId);
                        cmd.Parameters.Add("val2", DateTime.Now.ToString("dd-MM-yyyy"));
                        OracleDataReader dataReader = cmd.ExecuteReader();
                        if (!dataReader.HasRows)
                        {
                            Console.WriteLine("@GetAllEvents4Today: No data found");
                            oracleConn.Close();
                            return count;
                        }

                        while (dataReader.Read())
                        {
                            count = cmd.ExecuteScalar().ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        success = false;
                        numberOfAttempts--;

                        if (numberOfAttempts == 1)
                            Console.WriteLine("@CalendarEvent @GetAllEvents4Today() Error:" + ex.Message);
                        else
                            Thread.Sleep(10000 / numberOfAttempts);
                        oracleConn.Close();
                    }
                }
                oracleConn.Close();
                return count;
            }



        }
    }
}
