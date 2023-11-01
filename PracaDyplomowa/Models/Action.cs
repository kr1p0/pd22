using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PracaDyplomowa.Models
{
    public class ActionType
    {
        private ActionType() { }
        public static readonly string Pozar = "Pożar";
        public static readonly string ZdarzenieDrogowe = "Zdarzenie drogowe";
        public static readonly string MiejsceZagrozenia = "Miejsce zagrożenia";
        public static readonly string FalszywyAlarm = "Fałszywy alarm";
    }


    public class Action : DbConnection
    {
        public Action() { }

        public Action(string CzasZdarzeniaPelny)
        {
            this.CzasZdarzeniaPelny = CzasZdarzeniaPelny;
        }


        public string JednostkaId { get; set; }
        public string Id { get; set; }
        public string TypZdarzenia { get; set; }
        //public string DataZdarzenia { get; set; }
        public string CzasZdarzenia { get; set; }
        public string CzasZdarzeniaPelny { get; set; }
        //public string DataZakonczenia { get; set; }
        public string CzasZakonczenia { get; set; }
        public string IloscUczestnikow { get; set; }
        public string Lokalizacja { get; set; }
        public List<string> ListaIdUczestnikow { get; set; }
        public class Participants
        {
            public string Akcja_id { get; set; }
            public string Uzytkownik_id { get; set; }
        }

        public void GetAction(string id)
        {
            var success = false;
            var numberOfAttempts = 6;
            string sqlsqlQuery = $"select * from osp_akcje Where id = :val1";
            OracleConnection oracleConn = new OracleConnection(ConnString);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();
                    Console.WriteLine("@Action @GetAction()  Connected status: " + oracleConn.State +
                       $" [Approach: {7-numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", id);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Action @GetAction(): No data found");
                        oracleConn.Close();
                        return;
                    }

                    while (dataReader.Read())
                    {
                        Id = (dataReader["id"].ToString());
                        TypZdarzenia = (dataReader["typ_zdarzenia"].ToString());
                        //DataZdarzenia = (dataReader["data_zdarzenia"].ToString());
                        CzasZdarzenia = (dataReader["czas_zdarzenia"].ToString());
                        //DataZakonczenia = (dataReader["data_zakonczenia"].ToString());
                        CzasZakonczenia = (dataReader["czas_zakonczenia"].ToString());
                        IloscUczestnikow = (dataReader["ilosc_uczestnikow"].ToString());
                        Lokalizacja = (dataReader["lokalizacja"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Action @GetAction() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Action @GetAction(): Success");
        }

        public static List<Action> GetAllActions(string jednostkaId)
        {
            List<Action> Li = new List<Action>();
            var success = false;
            var numberOfAttempts = 6;
           
            string sqlsqlQuery = "select * from osp_akcje WHERE jednostka_id = :val1 ";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
               
                    oracleConn.Open();

                    Console.WriteLine("@Action @GetAllActions()  Connected status: " + oracleConn.State +
                      $" [Approach: {7-numberOfAttempts}]");

                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Action @GetAllActions(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Action tempAction = new Action();
                        tempAction.Id = (dataReader["id"].ToString());
                        tempAction.TypZdarzenia = (dataReader["typ_zdarzenia"].ToString());
                        //tempAction.DataZdarzenia = (dataReader["data_zdarzenia"].ToString());
                        tempAction.CzasZdarzenia = (dataReader["czas_zdarzenia"].ToString());
                        //tempAction.DataZakonczenia = (dataReader["data_zakonczenia"].ToString());
                        tempAction.CzasZakonczenia = (dataReader["czas_zakonczenia"].ToString());
                        tempAction.IloscUczestnikow = (dataReader["ilosc_uczestnikow"].ToString());
                        tempAction.Lokalizacja = (dataReader["lokalizacja"].ToString());
                        Li.Add(tempAction);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Action @GetAllActions() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Action @GetAllActions():  Success");
            return Li;
        }

        public static List<Action> GetActionsbyType(string jednostkaId, string typZdarzenia)
        {
            List<Action> Li = new List<Action>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = "select * from osp_akcje WHERE jednostka_id = :val1 AND LOWER(typ_zdarzenia) = :val2";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();

                    Console.WriteLine("@Action @GetActionsbyType()  Connected status: " + oracleConn.State +
                      $" [Approach: {7-numberOfAttempts}]");

                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    cmd.Parameters.Add("val2", typZdarzenia.ToLower());

                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Action @GetActionsbyType(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Action tempAction = new Action();
                        tempAction.Id = (dataReader["id"].ToString());
                        tempAction.TypZdarzenia = (dataReader["typ_zdarzenia"].ToString());
                        //tempAction.DataZdarzenia = (dataReader["data_zdarzenia"].ToString());
                        tempAction.CzasZdarzenia = (dataReader["czas_zdarzenia"].ToString());
                        //tempAction.DataZakonczenia = (dataReader["data_zakonczenia"].ToString());
                        tempAction.CzasZakonczenia = (dataReader["czas_zakonczenia"].ToString());
                        tempAction.IloscUczestnikow = (dataReader["ilosc_uczestnikow"].ToString());
                        tempAction.Lokalizacja = (dataReader["lokalizacja"].ToString());
                        Li.Add(tempAction);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Action @GetActionsbyType() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Action @GetActionsbyType():  Success");
            return Li;
        }



        public static List<Action> GetActionsbyMember(string userId)
        {
            List<Action> Li = new List<Action>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = "select osp_akcje.czas_zdarzenia, osp_akcje.czas_zakonczenia,  osp_akcje.typ_zdarzenia ,osp_akcje.id " +
                "from osp_akcje join osp_akcja_uczestnik on osp_akcje.id = osp_akcja_uczestnik.akcja_id  " +
                "WHERE osp_akcja_uczestnik.uzytkownik_id = :val1";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();

                    Console.WriteLine("@Action @GetActionsbyMember()  Connected status: " + oracleConn.State +
                      $" [Approach: {7 - numberOfAttempts}]");

                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    
                    cmd.Parameters.Add("val1", userId);

                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Action @GetActionsbyMember(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Action tempAction = new Action();
                        tempAction.Id = (dataReader["id"].ToString());
                        tempAction.TypZdarzenia = (dataReader["typ_zdarzenia"].ToString());
                        //tempAction.DataZdarzenia = (dataReader["data_zdarzenia"].ToString());
                        tempAction.CzasZdarzenia = (dataReader["czas_zdarzenia"].ToString());
                        //tempAction.DataZakonczenia = (dataReader["data_zakonczenia"].ToString());
                        tempAction.CzasZakonczenia = (dataReader["czas_zakonczenia"].ToString());
                        Li.Add(tempAction);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Action @GetActionsbyMember() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Action @GetActionsbyMember():  Success");
            return Li;
        }



        public static List<Action> GetActionsByDate(string jednostkaId , string minDate ,string maxDate, bool getParticipants = false)
        {
            List<Action> Li = new List<Action>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = "select * from osp_akcje WHERE jednostka_id = :val1 " +
                "AND czas_zdarzenia >= TO_TIMESTAMP(:val2, 'YYYY/MM/DD') " +
                "AND czas_zdarzenia <= TO_TIMESTAMP(:val3, 'YYYY/MM/DD HH24:MI')";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();

                    Console.WriteLine("@Action @GetActionsByDate()  Connected status: " + oracleConn.State +
                      $" [Approach: {7-numberOfAttempts}]");

                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    cmd.Parameters.Add("val2", minDate);
                    cmd.Parameters.Add("val2", maxDate);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Action @GetActionsByDate(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Action tempAction = new Action();
                        tempAction.Id = (dataReader["id"].ToString());
                        tempAction.TypZdarzenia = (dataReader["typ_zdarzenia"].ToString());
                        //tempAction.DataZdarzenia = (dataReader["data_zdarzenia"].ToString());
                        tempAction.CzasZdarzeniaPelny = (dataReader["czas_zdarzenia"].ToString());
                        tempAction.CzasZdarzenia = (dataReader["czas_zdarzenia"].ToString());
                        tempAction.CzasZdarzenia = Convert.ToDateTime(tempAction.CzasZdarzenia).Year.ToString();
                        //tempAction.DataZakonczenia = (dataReader["data_zakonczenia"].ToString());
                        tempAction.CzasZakonczenia = (dataReader["czas_zakonczenia"].ToString());
                        tempAction.IloscUczestnikow = (dataReader["ilosc_uczestnikow"].ToString());
                        tempAction.Lokalizacja = (dataReader["lokalizacja"].ToString());
                        tempAction.ListaIdUczestnikow = getParticipants ? GetParticipants(tempAction.Id) : new List<string>() ;
                        Li.Add(tempAction);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Action @GetActionsByDate() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Action @GetActionsByDate():  Success");
            return Li;
        }




        public static string InsertAction(Action obj, List<string> listOfParticipants)
        {
            var success = false;
            var numberOfAttempts = 6;
            var returnedID = "";

            var sqlQuery = "INSERT INTO osp_akcje(jednostka_id, typ_zdarzenia, czas_zdarzenia, czas_zakonczenia, ilosc_uczestnikow, lokalizacja) " +
                "VALUES (:val1 , :val2, TO_TIMESTAMP(:val3, 'YYYY-MM-DD HH24:MI:SS') , TO_TIMESTAMP(:val4 , 'YYYY-MM-DD HH24:MI:SS') , :val5 , :val6 ) RETURNING id INTO :returnInsertedRowId ";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Action @InsertAction()  Connected status: " + oracleConn.State +
                            $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", obj.JednostkaId);
                    cmd.Parameters.Add("val2", obj.TypZdarzenia);
                    cmd.Parameters.Add("val3", obj.CzasZdarzenia);
                    cmd.Parameters.Add("val4", obj.CzasZakonczenia);
                    cmd.Parameters.Add("val5", obj.IloscUczestnikow);
                    cmd.Parameters.Add("val6", obj.Lokalizacja);
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
                        Console.WriteLine("@Action @InsertAction() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }

           
            InsertParticipants(returnedID, listOfParticipants);

            Console.WriteLine("@Action @InsertAction(): Success");
            oracleConn.Close();
            return "(✓) Dodano Akcje";
        }


        public static string InsertParticipants(string akcja_id, List<string> listOfParticipants)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "INSERT INTO osp_akcja_uczestnik(akcja_id, uzytkownik_id) VALUES " +
            "(:val1 , :val2 )";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Action @InsertParticipants()  Connected status: " + oracleConn.State +
                      $" [Approach: {7 - numberOfAttempts}]");
                    foreach (var participant in listOfParticipants)
                    {
                        OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                        cmd.Parameters.Add("val1", akcja_id);
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
                        Console.WriteLine("@Action @InsertParticipants() " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@Action @InsertParticipants(): Success");
            oracleConn.Close();
            return "(✓) Dodano uczestnika";
        }



        public static string AssignActionsToMember(string uzytkownikId, List<string> listOfassignedActions)
        {
            RemoveActionParticipantByMemberId(uzytkownikId);
           
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "INSERT INTO osp_akcja_uczestnik(akcja_id, uzytkownik_id) VALUES " +
                "(:val1 , :val2 )";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Action @AssignActionsToMember()  Connected status: " + oracleConn.State +
                      $" [Approach: {7 - numberOfAttempts}]");


                    foreach (var item in listOfassignedActions)
                    {
                        OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                        cmd.Parameters.Add("val1", item);
                        cmd.Parameters.Add("val2", uzytkownikId);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;
                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@Action @AssignActionsToMember()  Error: " + ex.Message);
                        return "(🗙) Wystąpił błąd";
                    }

                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Action @AssignActionsToMember() :  Success");
            return "Powodzenie";
        }



        public static List<string> GetParticipants(string akcjaId)
        {
            List<string> Li = new List<string>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = "select * from osp_akcja_uczestnik WHERE akcja_id = :val1 ";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();

                    Console.WriteLine("@Action @GetParticipants()  Connected status: " + oracleConn.State +
                      $" [Approach: {7-numberOfAttempts}]");

                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", akcjaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Action @GetParticipants(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Li.Add((dataReader["uzytkownik_id"].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Action @GetParticipants() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Action @GetParticipants():  Success");
            return Li;
        }


        public static string EditAction(Action obj, List<string> listOfParticipants)
        {
            var success = false;
            var numberOfAttempts = 6;
            var returnedID = "";

            var sqlQuery = "UPDATE osp_akcje SET typ_zdarzenia = :val1, " +
                "czas_zdarzenia = TO_TIMESTAMP(:val2, 'YYYY-MM-DD HH24:MI:SS'), " +
                "czas_zakonczenia = TO_TIMESTAMP(:val3 , 'YYYY-MM-DD HH24:MI:SS'), " +
                "ilosc_uczestnikow = :val4, lokalizacja = :val5 where id = :val6 " +
                " RETURNING id INTO :returnInsertedRowId";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Action @EditAction()  Connected status: " + oracleConn.State +
                      $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", obj.TypZdarzenia);
                    cmd.Parameters.Add("val2", obj.CzasZdarzenia);
                    cmd.Parameters.Add("val3", obj.CzasZakonczenia);
                    cmd.Parameters.Add("val4", obj.IloscUczestnikow);
                    cmd.Parameters.Add("val5", obj.Lokalizacja);
                    cmd.Parameters.Add("val6", obj.Id);
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
                        Console.WriteLine("@Action @EditAction(): " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }

            RemoveActionParticipants(returnedID);
            InsertParticipants(returnedID, listOfParticipants);


            Console.WriteLine("@Action @EditAction(): Success");
            oracleConn.Close();
            return "(✓) Powodzenie";
        }

        public static string RemoveActionParticipants(string id)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "DELETE FROM osp_akcja_uczestnik WHERE akcja_id = :val1";


            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Action @RemoveActionParticipants()  Connected status: " + oracleConn.State +
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
                        Console.WriteLine("@Action @RemoveActionParticipants(): " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@Action @RemoveActionParticipants(): Success");
            oracleConn.Close();
            return "(✓) Usunięto";
        }

        public static string RemoveActionParticipantByMemberId(string id)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "DELETE FROM osp_akcja_uczestnik WHERE uzytkownik_id = :val1";


            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Action @RemoveActionParticipantByMemberId()  Connected status: " + oracleConn.State +
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
                        Console.WriteLine("@Action @RemoveActionParticipantByMemberId(): " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@Action @RemoveActionParticipantByMemberId(): Success");
            oracleConn.Close();
            return "(✓) Usunięto";
        }

        public static string RemoveAction(string id)
        {
            RemoveActionParticipants(id);

            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "DELETE FROM osp_akcje WHERE id = :val1";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Action @RemoveAction()  Connected status: " + oracleConn.State +
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
                        Console.WriteLine("@Action @RemoveAction(): " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@Action @RemoveAction(): Success");
            oracleConn.Close();
            return "(✓) Usunięto";
        }


        public static List<Action> SearchActions(string jednostkaId, string searchData)
        {
            List<Action> Li = new List<Action>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = $"SELECT * FROM osp_akcje WHERE jednostka_id = :val1 AND (LOWER(typ_zdarzenia) LIKE '%{searchData}%' OR  LOWER(czas_zdarzenia) LIKE '%{searchData}%')";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();

                    Console.WriteLine("@Action @GetAllActions()  Connected status: " + oracleConn.State +
                      $" [Approach: {7 - numberOfAttempts}]");

                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Action @GetAllActions(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Action tempAction = new Action();
                        tempAction.Id = (dataReader["id"].ToString());
                        tempAction.TypZdarzenia = (dataReader["typ_zdarzenia"].ToString());
                        //tempAction.DataZdarzenia = (dataReader["data_zdarzenia"].ToString());
                        tempAction.CzasZdarzenia = (dataReader["czas_zdarzenia"].ToString());
                        //tempAction.DataZakonczenia = (dataReader["data_zakonczenia"].ToString());
                        tempAction.CzasZakonczenia = (dataReader["czas_zakonczenia"].ToString());
                        tempAction.IloscUczestnikow = (dataReader["ilosc_uczestnikow"].ToString());
                        tempAction.Lokalizacja = (dataReader["lokalizacja"].ToString());
                        Li.Add(tempAction);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Action @GetAllActions() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Action @GetAllActions():  Success");
            return Li;
        }
    }
}
