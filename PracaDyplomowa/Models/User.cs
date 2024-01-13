using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PracaDyplomowa.Models
{
    public class User : DbConnection
    {
        public class UserType
        {
            public const string Admin  = "Admin";
            public const string Standard  = "Standard";
        }
        public class StandbyStatus
        {
            public const string Dostepny = "Dostępny";
            public const string Niedostepny = "Niedostępny";
        }
        public class LastVisit
        {
            public const string Chat = "chat_wizyta";
            public const string Ogloszenia = "ogloszenia_wizyta";
        }
        public User() 
        {
        }
        public User(string id , string column = "email")
        {
            GetUserDataBy(id , column);
        }
        public User(string id, string jednostkaId, string column )
        {
            GetUserData(id, jednostkaId, column);
        }
     
        public string UzytkownikId { get; set; }
        public string Jednostka_id { get; set; } //foreign key
        public string TypKonta { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Ulica { get; set; }
        public string Miejscowosc { get; set; }
        public string KodPocztowy { get; set; }
        public string CzasPracy { get; set; }
        public string IloscAkcji { get; set; }
        public string Email { get; set; }
        public string Haslo { get; set; }
        public string Telefon { get; set; }
        public string Funkcja { get; set; }
        public string DataWstapienia { get; set; }
        public string WaznoscBadan { get; set; }
        public string Zatwierdzony { get; set; }
        public string Status { get; set; }
        public string ImgNazwa { get; set; }
        public string PowiazanyEventId { get; set; } //termin badan
        public string ChatWizyta { get; set; } //termin badan
        public string OgloszeniaWizyta { get; set; } //termin badan
        public string IloscSprzetu { get; set; } //termin badan
        public string KursId { get; set; }
        public string Kurs { get; set; }
        public string KursDataUkonczenia { get; set; }
        public string KursDataWaznosci { get; set; }





        private void GetUserData(string email, string jednostkaId, string column)
        {
            var success = false;
            var numberOfAttempts = 6;
            string sqlsqlQuery = "Select osp_uzytkownik.* , osp_kalendarz.id from osp_uzytkownik " +
                 "join osp_kalendarz on osp_uzytkownik.uzytkownik_id =osp_kalendarz.uzytkownik_id " +
                $"Where osp_uzytkownik.{column} = :val1 AND osp_uzytkownik.jednostka_id = :val2";
            OracleConnection oracleConn = new OracleConnection(ConnString);
            while(!success && numberOfAttempts>1)
            {
                try
                {
                    success = true;
                    
                    oracleConn.Open();
                    Console.WriteLine("@User @GetUserData()  Connected status: " + oracleConn.State +
                       $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", email);
                    cmd.Parameters.Add("val2", jednostkaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("No data found");
                        oracleConn.Close();
                        GetUserDataAlternative( email,  jednostkaId,  column); //if there is no event related to user
                        return;
                    }

                    while (dataReader.Read())
                    {
                        UzytkownikId = (dataReader["uzytkownik_id"].ToString());
                        Jednostka_id = (dataReader["jednostka_id"].ToString());
                        TypKonta = (dataReader["typ_konta"].ToString());
                        Imie = (dataReader["imie"].ToString());
                        Nazwisko = (dataReader["nazwisko"].ToString());
                        Ulica = (dataReader["ulica"].ToString());
                        Miejscowosc = (dataReader["miejscowosc"].ToString());
                        KodPocztowy = (dataReader["kod_pocztowy"].ToString());
                        CzasPracy = (dataReader["czas_pracy"].ToString());
                        Email = (dataReader["email"].ToString());
                        Haslo = (dataReader["haslo"].ToString());
                        Telefon = (dataReader["telefon"].ToString());
                        Funkcja = (dataReader["funkcja"].ToString());
                        DataWstapienia = (dataReader["data_wstapienia"].ToString());

                        if (!string.IsNullOrEmpty(DataWstapienia)) DataWstapienia =
                              Convert.ToDateTime(DataWstapienia).ToString("yyyy-MM-dd");

                        Zatwierdzony = (dataReader["zatwierdzony"].ToString());
                        Status = (dataReader["status"].ToString());
                        WaznoscBadan = (dataReader["waznosc_badan"].ToString());

                        if (!string.IsNullOrEmpty(WaznoscBadan)) WaznoscBadan =
                              Convert.ToDateTime(WaznoscBadan).ToString("yyyy-MM-dd");

                        PowiazanyEventId = (dataReader["id"].ToString());
                        ImgNazwa = (dataReader["img_nazwa"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;
                    
                    if(numberOfAttempts==1)
                        Console.WriteLine("@User @GetUserData() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@User @GetUserData(): Success");
        }

        // (bez join) na wypadek kiedy nie ma relacji z kalendarzem czyt nie ma daty badan
        private void GetUserDataAlternative(string email, string jednostkaId, string column) 
        {
            var success = false;
            var numberOfAttempts = 6;
            string sqlsqlQuery = $"select * from osp_uzytkownik Where {column} = :val1 AND jednostka_id = :val2";
            OracleConnection oracleConn = new OracleConnection(ConnString);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();
                    Console.WriteLine("@User @GetUserData()  Connected status: " + oracleConn.State +
                       $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", email);
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
                        UzytkownikId = (dataReader["uzytkownik_id"].ToString());
                        Jednostka_id = (dataReader["jednostka_id"].ToString());
                        TypKonta = (dataReader["typ_konta"].ToString());
                        Imie = (dataReader["imie"].ToString());
                        Nazwisko = (dataReader["nazwisko"].ToString());
                        Ulica = (dataReader["ulica"].ToString());
                        Miejscowosc = (dataReader["miejscowosc"].ToString());
                        KodPocztowy = (dataReader["kod_pocztowy"].ToString());
                        CzasPracy = (dataReader["czas_pracy"].ToString());
                        Email = (dataReader["email"].ToString());
                        Haslo = (dataReader["haslo"].ToString());
                        Telefon = (dataReader["telefon"].ToString());
                        Funkcja = (dataReader["funkcja"].ToString());
                        DataWstapienia = (dataReader["data_wstapienia"].ToString());

                        if (!string.IsNullOrEmpty(DataWstapienia)) DataWstapienia =
                              Convert.ToDateTime(DataWstapienia).ToString("yyyy-MM-dd");

                        Zatwierdzony = (dataReader["zatwierdzony"].ToString());
                        Status = (dataReader["status"].ToString());
                        WaznoscBadan = (dataReader["waznosc_badan"].ToString());

                        if (!string.IsNullOrEmpty(WaznoscBadan)) WaznoscBadan =
                              Convert.ToDateTime(WaznoscBadan).ToString("yyyy-MM-dd");

                        ImgNazwa = (dataReader["img_nazwa"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@User @GetUserData() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@User @GetUserData(): Success");
        }

        public void GetUserDataBy(string id, string column = "email")
        {
            var success = false;
            var numberOfAttempts = 6;
            string sqlsqlQuery = $"select * from osp_uzytkownik Where {column} = :val1";
            OracleConnection oracleConn = new OracleConnection(ConnString);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@User @GetUserDataBy()  Connected status: " + oracleConn.State +
                        $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", id);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@User @GetUserDataBy(): No data found");
                        oracleConn.Close();
                        return;
                    }

                    while (dataReader.Read())
                    {
                        UzytkownikId = (dataReader["uzytkownik_id"].ToString());
                        Jednostka_id = (dataReader["jednostka_id"].ToString());
                        TypKonta = (dataReader["typ_konta"].ToString());
                        Imie = (dataReader["imie"].ToString());
                        Nazwisko = (dataReader["nazwisko"].ToString());
                        Ulica = (dataReader["ulica"].ToString());
                        Miejscowosc = (dataReader["miejscowosc"].ToString());
                        KodPocztowy = (dataReader["kod_pocztowy"].ToString());
                        CzasPracy = (dataReader["czas_pracy"].ToString());
                        Email = (dataReader["email"].ToString());
                        Haslo = (dataReader["haslo"].ToString());
                        Telefon = (dataReader["telefon"].ToString());
                        Funkcja = (dataReader["funkcja"].ToString());
                        DataWstapienia = (dataReader["data_wstapienia"].ToString());

                        if (!string.IsNullOrEmpty(DataWstapienia)) DataWstapienia =
                              Convert.ToDateTime(DataWstapienia).ToString("yyyy-MM-dd");

                        Zatwierdzony = (dataReader["zatwierdzony"].ToString());
                        Status = (dataReader["status"].ToString());
                        WaznoscBadan = (dataReader["waznosc_badan"].ToString());

                        if (!string.IsNullOrEmpty(WaznoscBadan)) WaznoscBadan =
                               Convert.ToDateTime(WaznoscBadan).ToString("yyyy-MM-dd");

                        ImgNazwa = (dataReader["img_nazwa"].ToString());
                        ChatWizyta = (dataReader["chat_wizyta"].ToString());

                        if (!string.IsNullOrEmpty(ChatWizyta)) ChatWizyta =
                               Convert.ToDateTime(ChatWizyta).ToString("yyyy-MM-dd HH:mm:ss.fff");



                        OgloszeniaWizyta = (dataReader["ogloszenia_wizyta"].ToString());

                        if (!string.IsNullOrEmpty(OgloszeniaWizyta)) OgloszeniaWizyta =
                               Convert.ToDateTime(OgloszeniaWizyta).ToString("yyyy-MM-dd HH:mm:ss.fff");
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@User @GetUserDataBy()  Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@User @GetUserDataBy(): Success");
        }

       

        public static List<User> GetListOfAllUser(string jednostkaId)
        {
            List<User> Li = new List<User>();
            var success = false;
            var numberOfAttempts = 6;
            string sqlsqlQuery = $"select * from osp_uzytkownik WHERE jednostka_id = :val1";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@User @GetListOfAllUser()  Connected status: " + oracleConn.State +
                        $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@User @GetListOfAllUser(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        User tempUser = new User();
                        tempUser.UzytkownikId = (dataReader["uzytkownik_id"].ToString());
                        tempUser.Jednostka_id = (dataReader["jednostka_id"].ToString());
                        tempUser.TypKonta = (dataReader["typ_konta"].ToString());
                        tempUser.Imie = (dataReader["imie"].ToString());
                        tempUser.Nazwisko = (dataReader["nazwisko"].ToString());
                        tempUser.Ulica = (dataReader["ulica"].ToString());
                        tempUser.Miejscowosc = (dataReader["miejscowosc"].ToString());
                        tempUser.KodPocztowy = (dataReader["kod_pocztowy"].ToString());
                        tempUser.CzasPracy = (dataReader["czas_pracy"].ToString());
                        tempUser.Email = (dataReader["email"].ToString());
                        tempUser.Haslo = (dataReader["haslo"].ToString());
                        tempUser.Telefon = (dataReader["telefon"].ToString());
                        tempUser.Funkcja = (dataReader["funkcja"].ToString());
                        tempUser.DataWstapienia = (dataReader["data_wstapienia"].ToString());

                        if (!string.IsNullOrEmpty(tempUser.DataWstapienia)) tempUser.DataWstapienia =
                              Convert.ToDateTime(tempUser.DataWstapienia).ToString("yyyy-MM-dd");

                        tempUser.Zatwierdzony = (dataReader["zatwierdzony"].ToString());
                        tempUser.Status = (dataReader["status"].ToString());
                        tempUser.WaznoscBadan = (dataReader["waznosc_badan"].ToString());

                        if (!string.IsNullOrEmpty(tempUser.WaznoscBadan)) tempUser.WaznoscBadan =
                             Convert.ToDateTime(tempUser.WaznoscBadan).ToString("yyyy-MM-dd");

                        tempUser.ImgNazwa = (dataReader["img_nazwa"].ToString());

                        Li.Add(tempUser);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@User @GetListOfAllUser()  Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@User @GetListOfAllUser(): Success");
            return Li;    
        }

        public static List<User> GetListOfAllAdministration(string jednostkaId)
        {
            List<User> Li = new List<User>();
            var success = false;
            var numberOfAttempts = 6;
            string sqlsqlQuery = $"select * from osp_uzytkownik WHERE jednostka_id = :val1 " +
                $"AND funkcja IS NOT NULL";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@User @GetListOfAllAdministration()  Connected status: " + oracleConn.State +
                           $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@User @GetListOfAllAdministration(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        User tempUser = new User();
                        tempUser.UzytkownikId = (dataReader["uzytkownik_id"].ToString());
                        tempUser.Jednostka_id = (dataReader["jednostka_id"].ToString());
                        tempUser.TypKonta = (dataReader["typ_konta"].ToString());
                        tempUser.Imie = (dataReader["imie"].ToString());
                        tempUser.Nazwisko = (dataReader["nazwisko"].ToString());
                        tempUser.Ulica = (dataReader["ulica"].ToString());
                        tempUser.Miejscowosc = (dataReader["miejscowosc"].ToString());
                        tempUser.KodPocztowy = (dataReader["kod_pocztowy"].ToString());
                        tempUser.CzasPracy = (dataReader["czas_pracy"].ToString());
                        tempUser.Email = (dataReader["email"].ToString());
                        tempUser.Haslo = (dataReader["haslo"].ToString());
                        tempUser.Telefon = (dataReader["telefon"].ToString());
                        tempUser.Funkcja = (dataReader["funkcja"].ToString());
                        tempUser.DataWstapienia = (dataReader["data_wstapienia"].ToString());

                        if (!string.IsNullOrEmpty(tempUser.DataWstapienia)) tempUser.DataWstapienia =
                             Convert.ToDateTime(tempUser.DataWstapienia).ToString("yyyy-MM-dd");

                        tempUser.Zatwierdzony = (dataReader["zatwierdzony"].ToString());
                        tempUser.Status = (dataReader["status"].ToString());
                        tempUser.WaznoscBadan = (dataReader["waznosc_badan"].ToString());

                        if (!string.IsNullOrEmpty(tempUser.WaznoscBadan)) tempUser.WaznoscBadan =
                         Convert.ToDateTime(tempUser.WaznoscBadan).ToString("yyyy-MM-dd");

                        tempUser.ImgNazwa = (dataReader["img_nazwa"].ToString());
                        Li.Add(tempUser);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@User @GetListOfAllAdministration()  Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@User @GetListOfAllAdministration(): Success");
            return Li;
        }


        public static string GetUserStatus(string userId)
        {
            var currentStatus = "";
            var success = false;
            var numberOfAttempts = 6;
            string sqlsqlQuery = "select status from osp_uzytkownik Where uzytkownik_id = :val1";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();
                    Console.WriteLine("@User @GetUserStatus()  Connected status: " + oracleConn.State +
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
                       currentStatus  = (dataReader["status"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@User @GetUserStatus() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@User @GetUserStatus(): Success");
            return currentStatus;
        }


        public static (string result, string insertedId) InsertUser(User obj , string jednostkaId)
        {
            var success = false;
            var numberOfAttempts = 6;
            var returnedID = "";

            var sqlQuery = "INSERT INTO osp_uzytkownik(jednostka_id, imie, nazwisko, email, telefon,  " +
                "funkcja, data_wstapienia, ulica, miejscowosc , kod_pocztowy, status , typ_konta, " +
                "zatwierdzony, haslo ,waznosc_badan, img_nazwa ) " +
                "VALUES (:val0, :val1 , :val2, :val3, :val4, :val5, TO_DATE( :val6 , 'YYYY-MM-DD'), " +
                ":val7, :val8, :val9, :val10, :val11, :val12, :val13, " +
                "TO_DATE( :val14 , 'YYYY-MM-DD'), :val15 ) " +
                "RETURNING uzytkownik_id INTO :returnInsertedRowId ";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@User @InsertUser()  Connected status: " + oracleConn.State +
                       $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val0", jednostkaId);
                    cmd.Parameters.Add("val1", obj.Imie);
                    cmd.Parameters.Add("val2", obj.Nazwisko);
                    cmd.Parameters.Add("val3", obj.Email);
                    cmd.Parameters.Add("val4", obj.Telefon);
                    cmd.Parameters.Add("val5", obj.Funkcja);
                    cmd.Parameters.Add("val6", obj.DataWstapienia);
                    cmd.Parameters.Add("val7", obj.Ulica);
                    cmd.Parameters.Add("val8", obj.Miejscowosc);
                    cmd.Parameters.Add("val9", obj.KodPocztowy);
                    cmd.Parameters.Add("val10", obj.Status);
                    cmd.Parameters.Add("val11", obj.TypKonta);
                    cmd.Parameters.Add("val12", obj.Zatwierdzony);
                    cmd.Parameters.Add("val13", obj.Haslo);
                    cmd.Parameters.Add("val14", obj.WaznoscBadan);
                    cmd.Parameters.Add("val15", obj.ImgNazwa);
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
                        Console.WriteLine("@User @InsertUser() Error: " + ex.Message);
                        oracleConn.Close();
                        return ("(🗙) Wystąpił błąd" , null);
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }

            Console.WriteLine("@User @InsertUser(): Success");
            oracleConn.Close();
            return ("(✓) Dodano użytkownika", returnedID);
        }


        public static (string result, bool success) updateMainData(User obj, string userIdentifier) //no pass
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "UPDATE osp_uzytkownik SET " +
            "  imie = :val1 , nazwisko = :val2 " +
            " ,email = :val3 , telefon  = :val4 , funkcja = :val5 " +
            " ,data_wstapienia = TO_DATE( :val6 , 'YYYY-MM-DD') " +
            " ,ulica = :val7 , miejscowosc = :val8 , kod_pocztowy = :val9 "+
            " ,status = :val10, waznosc_badan = TO_DATE( :val11 , 'YYYY-MM-DD'), typ_konta = :val12 " +
            " ,img_nazwa = :val13 WHERE uzytkownik_id = :val14";

            var oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@User @updateMainData()  Connected status: " + oracleConn.State +
                             $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", obj.Imie);
                    cmd.Parameters.Add("val2", obj.Nazwisko);
                    cmd.Parameters.Add("val3", obj.Email);
                    cmd.Parameters.Add("val4", obj.Telefon);
                    cmd.Parameters.Add("val5", obj.Funkcja);
                    cmd.Parameters.Add("val6", obj.DataWstapienia);
                    cmd.Parameters.Add("val7", obj.Ulica);
                    cmd.Parameters.Add("val8", obj.Miejscowosc);
                    cmd.Parameters.Add("val9", obj.KodPocztowy);
                    cmd.Parameters.Add("val10", obj.Status);
                    cmd.Parameters.Add("val11", obj.WaznoscBadan);
                    cmd.Parameters.Add("val12", obj.TypKonta);
                    cmd.Parameters.Add("val13", obj.ImgNazwa);
                    cmd.Parameters.Add("val14", userIdentifier);


                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        oracleConn.Close();
                        Console.WriteLine("-@User @updateMainData()  Error: " + ex.Message);
                        return ("(🗙) Wystąpił błąd", false);
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@User @updateMainData(): Success");
            oracleConn.Close();
            return ("(✓) Powodzenie", true);
        }


        public static string DeleteUser(string id)
        {
            Notification.RemoveNewUserStatusByMemberId(id);
            Action.RemoveActionParticipantByMemberId(id);
            Reminder.RemoveReminderRecipientsByMemberId(id);
            Competition.RemoveCompetitionParticipantByMemberId(id);
            CalendarEvent.RemoveEventByUserIdRelation(id);
            Equipment.RemoveAssignedEquipmentByMemberId(id);
            RemoveCoursesByMemberId(id);

            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "DELETE FROM osp_uzytkownik WHERE uzytkownik_id = :val1";


            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@User @DeleteUser()  Connected status: " + oracleConn.State +
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
                        Console.WriteLine("@User @DeleteUser() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@User @DeleteUser(): Success");
            oracleConn.Close();
            return "(✓) Usunięto";
        }


        public static string updatePassword(string password, string userId)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = $"UPDATE osp_uzytkownik SET haslo = :val1 " +
                $"WHERE uzytkownik_id = :val2";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@User @updatePassword()  Connected status: " + oracleConn.State +
                       $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", password);
                    cmd.Parameters.Add("val2", userId);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@User @updatePassword  Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@User @updatePassword: Success");
            oracleConn.Close();
            return "(✓) Powodzenie";
        }

        public static (string result, bool success) updateStatus(string status, string userId)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = $"UPDATE osp_uzytkownik SET status = :val1 " +
                $"WHERE uzytkownik_id = :val2";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@User @updateStatus()  Connected status: " + oracleConn.State +
                       $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", status);
                    cmd.Parameters.Add("val2", userId);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@User @updateStatus  Error: " + ex.Message);
                        oracleConn.Close();
                        return ("(🗙) Wystąpił błąd",false);
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@User @updateStatus: Success");
            oracleConn.Close();
            return ("(✓) Powodzenie" , true);
        }



        public static (int totalTime, int actionCount) getTimeWorked(string uzytkownikId) //inMinutes
        {
            int totalTime = 0;
            int actionCount = 0;
            var success = false;
            var numberOfAttempts = 6;
            string sqlsqlQuery = "select osp_akcja_uczestnik.akcja_id , osp_akcje.czas_zdarzenia, " +
                "osp_akcje.czas_zakonczenia FROM osp_akcja_uczestnik join osp_akcje " +
                "on osp_akcja_uczestnik.akcja_id = osp_akcje.id " +
                "where osp_akcja_uczestnik.uzytkownik_id = :val1";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@User @getTimeWorked()  Connected status: " + oracleConn.State +
                          $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", uzytkownikId);
                 
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@User @getTimeWorked(): No data found");
                        oracleConn.Close();
                        return (0,0);
                    }

                    while (dataReader.Read())
                    {
                        var start = Convert.ToDateTime((dataReader["czas_zdarzenia"]));
                        var end = Convert.ToDateTime((dataReader["czas_zakonczenia"]));
                        totalTime += (int)end.Subtract(start).TotalMinutes;
                        actionCount++;
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@User @getTimeWorked() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@User @getTimeWorked(): Success");
            return (totalTime , actionCount);
        }

        //pobiera imie ,nazwisko ,mail, czas pracy, ilosc, akcji
        public static List<User> getTimeWorkedAllMembers(string jednostkaId , string minDate ,string maxDate) //inMinutes
        {
            Console.WriteLine("sasasasasas: "+ maxDate);
            List<User> Li = new List<User>();
           
            var success = false;
            var numberOfAttempts = 6;
            string sqlsqlQuery = "select osp_akcja_uczestnik.akcja_id , osp_akcje.czas_zdarzenia, " +
                "osp_akcje.czas_zakonczenia ,osp_uzytkownik.uzytkownik_id, osp_uzytkownik.imie, " +
                "osp_uzytkownik.nazwisko , osp_uzytkownik.email FROM osp_akcja_uczestnik " +
                "join osp_akcje on osp_akcja_uczestnik.akcja_id = osp_akcje.id " +
                "join osp_uzytkownik on osp_akcja_uczestnik.uzytkownik_id = osp_uzytkownik.uzytkownik_id " +
                "where osp_uzytkownik.jednostka_id = :val1 " +
                "and czas_zdarzenia >= TO_TIMESTAMP(:val2, 'YYYY/MM/DD') " +
                "and czas_zdarzenia <= TO_TIMESTAMP(:val3, 'YYYY/MM/DD HH24:MI')";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@User @getTimeWorkedAllMembers()  Connected status: " + oracleConn.State +
                        $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    cmd.Parameters.Add("val2", minDate);
                    cmd.Parameters.Add("val3", maxDate);

                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@User @getTimeWorkedAllMembers(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        User tempUser = new User();
                        var start = Convert.ToDateTime((dataReader["czas_zdarzenia"]));
                        var end = Convert.ToDateTime((dataReader["czas_zakonczenia"]));
                        var time = (int)end.Subtract(start).TotalMinutes;
                        tempUser.CzasPracy = time.ToString();
                        tempUser.UzytkownikId = (dataReader["uzytkownik_id"].ToString());
                        tempUser.Imie = (dataReader["imie"].ToString());
                        tempUser.Nazwisko = (dataReader["nazwisko"].ToString());
                        tempUser.Email = (dataReader["email"].ToString());
                        Li.Add(tempUser);

                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@User @getTimeWorkedAllMembers() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@User @getTimeWorkedAllMembers(): Success");
            return Li;
        }


        public static void UpdateVisitDate(string subsite, string userId)
        {
            var success = false;
            var numberOfAttempts = 6;
            //chat_wizyta
            //ogloszenia_wizyta

            var sqlQuery = $"UPDATE osp_uzytkownik SET {subsite} = TO_TIMESTAMP( :val1 , 'DD/MM/YYYY HH24:MI:SS,FF9') " +
                "WHERE uzytkownik_id = :val2";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@User @updateVisitDate()  Connected status: " + oracleConn.State +
                       $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", DateTime.Now.AddSeconds(1).ToString("dd-MM-yyyy HH:mm:ss.fff")); //1sec because cant select milisec from db
                    cmd.Parameters.Add("val2", userId);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@User @updateVisitDate  Error: " + ex.Message);
                        oracleConn.Close();
                        return ;
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@User @updateVisitDate: Success");
            oracleConn.Close();
            return;
        }


        public static string InsertCours(List<User> obj, string jednostkaId)
        {
            var success = false;
            var numberOfAttempts = 6;
           

            var sqlQuery = "INSERT INTO osp_uzytkownik_kursy(jednostka_id, " +
                "uzytkownik_id, kurs, DATA_UKONCZENIA, WAZNOSC_KURSU) " +
                "VALUES (:val0, :val1 , :val2, " +
                "TO_DATE(:val4, 'YYYY-MM-DD'), TO_DATE(:val5, 'YYYY-MM-DD'))";
              

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@User @InsertCours()  Connected status: " + oracleConn.State +
                       $" [Approach: {7 - numberOfAttempts}]");
                    foreach(var item in obj)
                    {
                        OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                        cmd.Parameters.Add("val0", jednostkaId);
                        cmd.Parameters.Add("val1", item.UzytkownikId);
                        cmd.Parameters.Add("val2", item.Kurs);
                        cmd.Parameters.Add("val3", item.KursDataUkonczenia);
                        cmd.Parameters.Add("val4", item.KursDataWaznosci);

                        cmd.ExecuteNonQuery();

                    }

                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@User @InsertCours() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }

            Console.WriteLine("@User @InsertCours(): Success");
            oracleConn.Close();
            return "(✓) Powodzenie";
        }

        public static List<User> GetCourses(string jednostkaId, string uzytkownikId)
        {
            List<User> Li = new List<User>();
            var success = false;
            var numberOfAttempts = 6;
            string sqlsqlQuery = $"select * from osp_uzytkownik_kursy WHERE jednostka_id = :val1 AND uzytkownik_id = :val2";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@User @GetCourses()  Connected status: " + oracleConn.State +
                        $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    cmd.Parameters.Add("val2", uzytkownikId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@User @GetCourses(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        User tempUser = new User();
                        tempUser.UzytkownikId = (dataReader["uzytkownik_id"].ToString());
                        tempUser.KursId = (dataReader["id"].ToString());
                        tempUser.Kurs = (dataReader["kurs"].ToString());

                        tempUser.KursDataUkonczenia = (dataReader["data_ukonczenia"].ToString());
                        if (!string.IsNullOrEmpty(tempUser.KursDataUkonczenia)) tempUser.KursDataUkonczenia =
                             Convert.ToDateTime(tempUser.KursDataUkonczenia).ToString("yyyy-MM-dd");

                        tempUser.KursDataWaznosci = (dataReader["waznosc_kursu"].ToString());
                        if (!string.IsNullOrEmpty(tempUser.KursDataWaznosci)) tempUser.KursDataWaznosci =
                             Convert.ToDateTime(tempUser.KursDataWaznosci).ToString("yyyy-MM-dd");

                        Li.Add(tempUser);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@User @GetCourses()  Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@User @GetCourses(): Success");
            return Li;
        }


        public static string RemoveCoursesByMemberId(string id)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "DELETE FROM osp_uzytkownik_kursy WHERE uzytkownik_id = :val1";

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
                        Console.WriteLine("@Notification @RemoveCoursesByMemberId() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@Notification @RemoveCoursesByMemberId(): Success");
            oracleConn.Close();
            return "(✓) Usunięto";
        }

        public static string RemoveCoursesByCourseId(string id)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "DELETE FROM osp_uzytkownik_kursy WHERE id = :val1";

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
                        Console.WriteLine("@Notification @RemoveCoursesByCourseId() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@Notification @RemoveCoursesByCourseId(): Success");
            oracleConn.Close();
            return "(✓) Usunięto";
        }

        public static List<User> SearchUser(string jednostkaId  , string searchData)
        {
            searchData = searchData.ToLower();
            List<User> Li = new List<User>();
            var success = false;
            var numberOfAttempts = 6;
            string sqlsqlQuery = $"SELECT * FROM osp_uzytkownik WHERE jednostka_id = :val1 AND (LOWER(imie) LIKE '%{searchData}%' OR  LOWER(nazwisko) LIKE '%{searchData}%')";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@User @SearchUser()  Connected status: " + oracleConn.State +
                        $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@User @GetListOfAllUser(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        User tempUser = new User();
                        tempUser.UzytkownikId = (dataReader["uzytkownik_id"].ToString());
                        tempUser.Jednostka_id = (dataReader["jednostka_id"].ToString());
                        tempUser.TypKonta = (dataReader["typ_konta"].ToString());
                        tempUser.Imie = (dataReader["imie"].ToString());
                        tempUser.Nazwisko = (dataReader["nazwisko"].ToString());
                        tempUser.Ulica = (dataReader["ulica"].ToString());
                        tempUser.Miejscowosc = (dataReader["miejscowosc"].ToString());
                        tempUser.KodPocztowy = (dataReader["kod_pocztowy"].ToString());
                        tempUser.CzasPracy = (dataReader["czas_pracy"].ToString());
                        tempUser.Email = (dataReader["email"].ToString());
                        tempUser.Haslo = (dataReader["haslo"].ToString());
                        tempUser.Telefon = (dataReader["telefon"].ToString());
                        tempUser.Funkcja = (dataReader["funkcja"].ToString());
                        tempUser.DataWstapienia = (dataReader["data_wstapienia"].ToString());

                        if (!string.IsNullOrEmpty(tempUser.DataWstapienia)) tempUser.DataWstapienia =
                              Convert.ToDateTime(tempUser.DataWstapienia).ToString("yyyy-MM-dd");

                        tempUser.Zatwierdzony = (dataReader["zatwierdzony"].ToString());
                        tempUser.Status = (dataReader["status"].ToString());
                        tempUser.WaznoscBadan = (dataReader["waznosc_badan"].ToString());

                        if (!string.IsNullOrEmpty(tempUser.WaznoscBadan)) tempUser.WaznoscBadan =
                             Convert.ToDateTime(tempUser.WaznoscBadan).ToString("yyyy-MM-dd");

                        tempUser.ImgNazwa = (dataReader["img_nazwa"].ToString());

                        Li.Add(tempUser);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@User @SearchUser()  Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@User @SearchUser(): Success");
            return Li;
        }

    }
}
