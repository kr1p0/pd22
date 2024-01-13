using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PracaDyplomowa.Models
{
    public class Competition : DbConnection
    {
        public Competition() { }

        public class CompetitionParticipants
        {
            public string Id { get; set; }
            public string UczestnikId { get; set; }
            public string Rola { get; set; }
        }

        public string JednostkaId { get; set; }
        public string Id { get; set; }
        public string Szczebel { get; set; }
        public string Kategoria { get; set; }
        public string IloscUczestnikow { get; set; }
        public string ZajeteMiejsce { get; set; }
        public string Data { get; set; }



        public  void GetCompetitionData(string competitionId)
        {
            var success = false;
            var numberOfAttempts = 6;
            string sqlsqlQuery = $"select * from osp_zawody Where id = :val1 ";
            OracleConnection oracleConn = new OracleConnection(ConnString);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();
                    Console.WriteLine("@Competition @GetCompetitionData()  Connected status: " + oracleConn.State +
                           $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", competitionId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Competition @GetCompetitionData(): No data found");
                        oracleConn.Close();
                        return;
                    }

                    while (dataReader.Read())
                    {
                        Id = (dataReader["id"].ToString());
                        Szczebel = (dataReader["szczebel"].ToString());
                        Kategoria = (dataReader["kategoria"].ToString());
                        IloscUczestnikow = (dataReader["ilosc_uczestnikow"].ToString());
                        ZajeteMiejsce = (dataReader["zajete_miejsce"].ToString());
                        Data = (dataReader["data"].ToString());
                        if (!string.IsNullOrEmpty(Data)) Data =
                               Convert.ToDateTime(Data).ToString("yyyy-MM-dd");
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Competition @GetCompetitionData() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Competition @GetCompetitionData(): Success");
        }


        public static List<Competition> GetAllCompetitionData(string jednostkaId)
        {
            List<Competition> Li = new List<Competition>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = $"select * from osp_zawody WHERE jednostka_id = :val1";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Competition @GetAllCompetitionData()  Connected status: " + oracleConn.State +
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
                        Competition tempEquipment = new Competition();
                        tempEquipment.Id = (dataReader["id"].ToString());
                        tempEquipment.Szczebel = (dataReader["szczebel"].ToString());
                        tempEquipment.Kategoria = (dataReader["kategoria"].ToString());
                        tempEquipment.IloscUczestnikow = (dataReader["ilosc_uczestnikow"].ToString());
                        tempEquipment.ZajeteMiejsce = (dataReader["zajete_miejsce"].ToString());
                        tempEquipment.Data = (dataReader["data"].ToString());
                        if (!string.IsNullOrEmpty(tempEquipment.Data)) tempEquipment.Data =
                              Convert.ToDateTime(tempEquipment.Data).ToString("yyyy-MM-dd");
                        Li.Add(tempEquipment);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Competition @GetAllEquipmentData() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Competition @GetAllCompetitionData(): Success");
            return Li;
        }


        public static List<Competition> GetAllCompetitionByDate(string jednostkaId, string minDate ,string maxDate)
        {
            List<Competition> Li = new List<Competition>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = $"select * from osp_zawody WHERE jednostka_id = :val1 " +
                "AND data >= TO_DATE(:val2, 'YYYY/MM/DD') AND data <= TO_DATE(:val3, 'YYYY/MM/DD HH24:MI')";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Competition @GetAllCompetitionByDate()  Connected status: " + oracleConn.State +
                          $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    cmd.Parameters.Add("val2", minDate);
                    cmd.Parameters.Add("val3", maxDate);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Competition tempEquipment = new Competition();
                        tempEquipment.Id = (dataReader["id"].ToString());
                        tempEquipment.Szczebel = (dataReader["szczebel"].ToString());
                        tempEquipment.Kategoria = (dataReader["kategoria"].ToString());
                        tempEquipment.IloscUczestnikow = (dataReader["ilosc_uczestnikow"].ToString());
                        tempEquipment.ZajeteMiejsce = (dataReader["zajete_miejsce"].ToString());
                        tempEquipment.Data = (dataReader["data"].ToString());
                        if (!string.IsNullOrEmpty(tempEquipment.Data)) tempEquipment.Data =
                              Convert.ToDateTime(tempEquipment.Data).ToString("yyyy-MM-dd");
                        Li.Add(tempEquipment);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Competition @GetAllCompetitionByDate() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Competition @GetAllCompetitionByDate(): Success");
            return Li;
        }


        public static (List<CompetitionParticipants>, List<string>) GetCompetitionParticipants(string competitionId)
        {
            List<CompetitionParticipants> Li = new List<CompetitionParticipants>();
            List<string> IdentLi = new List<string>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = $"select * from osp_zawody_uczestnik WHERE zawody_id = :val1";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Competition @GetCompetitionParticipants()  Connected status: " + oracleConn.State +
                           $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", competitionId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("No data found");
                        oracleConn.Close();
                        return (Li , IdentLi);
                    }

                    while (dataReader.Read())
                    {
                        CompetitionParticipants tempEquipment = new CompetitionParticipants();
                        tempEquipment.Id = (dataReader["id"].ToString());
                        tempEquipment.UczestnikId = (dataReader["uzytkownik_id"].ToString());
                        tempEquipment.Rola = (dataReader["rola"].ToString());
                        Li.Add(tempEquipment);
                        IdentLi.Add(tempEquipment.UczestnikId);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Competition @GetCompetitionParticipants() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Competition @GetCompetitionParticipants(): Success");
            return (Li , IdentLi);
        }

        public static string InsertCompetition(Competition obj, List<CompetitionParticipants> listOfParticipants)
        {
            var success = false;
            var numberOfAttempts = 6;
            var returnedID = "";

            var sqlQuery = "INSERT INTO osp_zawody(jednostka_id, szczebel, kategoria, ilosc_uczestnikow, zajete_miejsce, data) " +
                "VALUES (:val1 , :val2, :val3, :val4, :val5, TO_DATE(:val6, 'YYYY/MM/DD') ) " +
                "RETURNING id INTO :returnInsertedRowId ";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Competition @InsertCompetition()  Connected status: " + oracleConn.State +
                          $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", obj.JednostkaId);
                    cmd.Parameters.Add("val2", obj.Szczebel);
                    cmd.Parameters.Add("val3", obj.Kategoria);
                    cmd.Parameters.Add("val4", obj.IloscUczestnikow);
                    cmd.Parameters.Add("val5", obj.ZajeteMiejsce);
                    cmd.Parameters.Add("val6", obj.Data);
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
                        Console.WriteLine("@Competition @InsertCompetition() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }


            InsertParticipants(returnedID, listOfParticipants);


            Console.WriteLine("@Competition @InsertCompetition(): Success");
            oracleConn.Close();
            return "(✓) Dodano wydarzenie";
        }


        public static string InsertParticipants(string zawody_id, List<CompetitionParticipants> listOfParticipants)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "INSERT INTO osp_zawody_uczestnik(zawody_id, uzytkownik_id, rola) VALUES " +
            "(:val1 , :val2, :val3 )";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Competition @InsertParticipants()  Connected status: " + oracleConn.State +
                          $" [Approach: {7 - numberOfAttempts}]");
                    foreach (var participant in listOfParticipants)
                    {
                        OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                        cmd.Parameters.Add("val1", zawody_id);
                        cmd.Parameters.Add("val2", participant.UczestnikId);
                        cmd.Parameters.Add("val3", participant.Rola);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@Competition @InsertParticipants() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@Competition @InsertParticipants(): Success");
            oracleConn.Close();
            return "(✓) Dodano uczestnika";
        }

















        public static string EditCompetition(Competition obj, List<CompetitionParticipants> listOfParticipants)
        {
            var success = false;
            var numberOfAttempts = 6;
            var returnedID = "";

            var sqlQuery = "UPDATE osp_zawody SET szczebel = :val1, " +
                "kategoria = :val2, ilosc_uczestnikow =  :val3, " +
                "zajete_miejsce = :val4,  " +
                "data = TO_TIMESTAMP(:val5 , 'YYYY-MM-DD HH24:MI:SS') " +
                "where id = :val6 RETURNING id INTO :returnInsertedRowId ";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Competition @EditCompetition()  Connected status: " + oracleConn.State +
                          $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", obj.Szczebel);
                    cmd.Parameters.Add("val2", obj.Kategoria);
                    cmd.Parameters.Add("val3", obj.IloscUczestnikow);
                    cmd.Parameters.Add("val4", obj.ZajeteMiejsce);
                    cmd.Parameters.Add("val5", obj.Data);
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
                        Console.WriteLine("@Competition @EditCompetition() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            
            RemoveCompetitionParticipants(returnedID);
            InsertParticipants(returnedID, listOfParticipants);


            Console.WriteLine("@Competition @EditCompetition(): Success");
            oracleConn.Close();
            return "(✓) Powodzenie";
        }

        public static string RemoveCompetitionParticipants(string id)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "DELETE FROM osp_zawody_uczestnik WHERE zawody_id = :val1";


            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Competition @RemoveCompetitionParticipants()  Connected status: " + oracleConn.State +
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
                        Console.WriteLine("@Competition @RemoveCompetitionParticipants() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@Competition @RemoveCompetitionParticipants(): Success");
            oracleConn.Close();
            return "(✓) Usunięto";
        }

        public static string RemoveCompetitionParticipantByMemberId(string id)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "DELETE FROM osp_zawody_uczestnik WHERE uzytkownik_id = :val1";


            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Competition @RemoveCompetitionParticipantByMemberId()  Connected status: " + oracleConn.State +
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
                        Console.WriteLine("@Competition @RemoveCompetitionParticipantByMemberId() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@Competition @RemoveCompetitionParticipantByMemberId(): Success");
            oracleConn.Close();
            return "(✓) Usunięto";
        }


        public static string RemoveCompetition(string id)
        {
            RemoveCompetitionParticipants(id);

            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "DELETE FROM osp_zawody WHERE id = :val1";


            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Competition @RemoveCompetition()  Connected status: " + oracleConn.State +
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
                        Console.WriteLine("@Competition @RemoveCompetition() Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@Competition @RemoveCompetition(): Success");
            oracleConn.Close();
            return "(✓) Usunięto";
        }

    }
}
