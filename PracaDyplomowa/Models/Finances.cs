using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PracaDyplomowa.Models
{
    public class Finances :DbConnection
    {
        public class TransactionTypes
        {
            public static string Wplyw { get; } = "Wpływ";
            public static string Wydatek { get; } = "Wydatek";
            public static string KorektaPlus { get; } = "KorektaPlus";
            public static string KorektaMinus { get; } = "KorektaMinus";
        }

        public Finances() { }

        public Finances(string Kwota, string typTransakcji, string DataTransakcji)
        {
            this.Kwota = Kwota;
            this.TypTransakcji = typTransakcji;
            this.DataTransakcji = DataTransakcji;
        }

        public Finances(string id) 
        {
            GetFinances(id);
        }

        public string Id { get; set; }
        public string Dokument { get; set; }
        public string Opis { get; set; }
        public string Kwota { get; set; }
        public string TypTransakcji { get; set; }
        public string Odbiorca { get; set; }
        public string Zrodlo { get; set; }
        public string DataTransakcji { get; set; }
      


        public void GetFinances(string id)
        {
            var success = false;
            var numberOfAttempts = 6;
            string sqlsqlQuery = $"select * from osp_finanse_transakcje Where id = :val1";
            OracleConnection oracleConn = new OracleConnection(ConnString);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Finances @GetFinances()  Connected status: " + oracleConn.State +
                       $" [Approaches remaining: {numberOfAttempts}]");
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
                        Dokument = (dataReader["dokument"].ToString());
                        Opis = (dataReader["opis"].ToString());
                        Kwota = (dataReader["kwota"].ToString());
                        TypTransakcji = (dataReader["typ_transakcji"].ToString());
                        Odbiorca = (dataReader["odbiorca"].ToString());
                        Zrodlo = (dataReader["zrodlo"].ToString());
                        DataTransakcji = (dataReader["data_transakcji"].ToString());
                        if (!string.IsNullOrEmpty(DataTransakcji)) DataTransakcji =
                              Convert.ToDateTime(DataTransakcji).ToString("yyyy-MM-dd");
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Finances @GetFinances() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Finances @GetFinances(): Success");
        }



        public static List<Finances> GetAllFinances(string jednostkaId)
        {
            List<Finances> Li = new List<Finances>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = "select * from osp_finanse_transakcje WHERE jednostka_id = :val1 ";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();

                    Console.WriteLine("@Finances @GetAllFinances()  Connected status: " + oracleConn.State +
                      $" [Approaches remaining: {numberOfAttempts}]");

                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Finances @GetAllFinances(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Finances tempFinances = new Finances();
                        tempFinances.Id = (dataReader["id"].ToString());
                        tempFinances.Dokument = (dataReader["dokument"].ToString());
                        tempFinances.Opis = (dataReader["opis"].ToString());
                        tempFinances.Kwota = (dataReader["kwota"].ToString());
                        tempFinances.TypTransakcji = (dataReader["typ_transakcji"].ToString());
                        tempFinances.Odbiorca = (dataReader["odbiorca"].ToString());
                        tempFinances.Zrodlo = (dataReader["zrodlo"].ToString());
                        tempFinances.DataTransakcji = (dataReader["data_transakcji"].ToString());
                        if (!string.IsNullOrEmpty(tempFinances.DataTransakcji)) tempFinances.DataTransakcji =
                              Convert.ToDateTime(tempFinances.DataTransakcji).ToString("yyyy-MM-dd");
                        Li.Add(tempFinances);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Finances @GetAllFinances() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Finances @GetAllFinances():  Success");
            return Li;
        }


        public static List<Finances> GetAllFinancesByFilter(string jednostkaId , string column , string value)
        {
            List<Finances> Li = new List<Finances>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = "select * from osp_finanse_transakcje WHERE jednostka_id = :val1 " +
                $"AND {column} = :val2 ";

            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();

                    Console.WriteLine("@Finances @GetAllFinancesByFilter()  Connected status: " + oracleConn.State +
                      $" [Approaches remaining: {numberOfAttempts}]");

                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    cmd.Parameters.Add("val2", value);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Finances @GetAllFinancesByFilter(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Finances tempFinances = new Finances();
                        tempFinances.Id = (dataReader["id"].ToString());
                        tempFinances.Dokument = (dataReader["dokument"].ToString());
                        tempFinances.Opis = (dataReader["opis"].ToString());
                        tempFinances.Kwota = (dataReader["kwota"].ToString());
                        tempFinances.TypTransakcji = (dataReader["typ_transakcji"].ToString());
                        tempFinances.Odbiorca = (dataReader["odbiorca"].ToString());
                        tempFinances.Zrodlo = (dataReader["zrodlo"].ToString());
                        tempFinances.DataTransakcji = (dataReader["data_transakcji"].ToString());
                        if (!string.IsNullOrEmpty(tempFinances.DataTransakcji)) tempFinances.DataTransakcji =
                              Convert.ToDateTime(tempFinances.DataTransakcji).ToString("yyyy-MM-dd");
                        Li.Add(tempFinances);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Finances @GetAllFinancesByFilter() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Finances @GetAllFinancesByFilter():  Success");
            return Li;
        }


        public static List<Finances> GetAllFinancesByCorrection(string jednostkaId, string column, string value , string valueSec)
        {
            List<Finances> Li = new List<Finances>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = "select * from osp_finanse_transakcje WHERE jednostka_id = :val1 " +
                $"AND {column} = :val2 OR {column} = :val3";

            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();

                    Console.WriteLine("@Finances @GetAllFinancesByCorrection()  Connected status: " + oracleConn.State +
                      $" [Approaches remaining: {numberOfAttempts}]");

                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    cmd.Parameters.Add("val2", value);
                    cmd.Parameters.Add("val3", valueSec);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Finances @GetAllFinancesByCorrection(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Finances tempFinances = new Finances();
                        tempFinances.Id = (dataReader["id"].ToString());
                        tempFinances.Dokument = (dataReader["dokument"].ToString());
                        tempFinances.Opis = (dataReader["opis"].ToString());
                        tempFinances.Kwota = (dataReader["kwota"].ToString());
                        tempFinances.TypTransakcji = (dataReader["typ_transakcji"].ToString());
                        tempFinances.Odbiorca = (dataReader["odbiorca"].ToString());
                        tempFinances.Zrodlo = (dataReader["zrodlo"].ToString());
                        tempFinances.DataTransakcji = (dataReader["data_transakcji"].ToString());
                        if (!string.IsNullOrEmpty(tempFinances.DataTransakcji)) tempFinances.DataTransakcji =
                              Convert.ToDateTime(tempFinances.DataTransakcji).ToString("yyyy-MM-dd");
                        Li.Add(tempFinances);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Finances @GetAllFinancesByCorrection() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Finances @GetAllFinancesByCorrection():  Success");
            return Li;
        }



        public static List<Finances> GetFinancesByDate(string jednostkaId , string minDate ,string maxDate)
        {
            List<Finances> Li = new List<Finances>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = "select * from osp_finanse_transakcje WHERE jednostka_id = :val1 " +
                "AND data_transakcji >= TO_DATE(:val2, 'YYYY/MM/DD') " +
                "AND data_transakcji <= TO_DATE(:val3, 'YYYY/MM/DD HH24:MI') ";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();

                    Console.WriteLine("@Finances @GetAllFinances()  Connected status: " + oracleConn.State +
                      $" [Approaches remaining: {numberOfAttempts}]");

                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    cmd.Parameters.Add("val2", minDate);
                    cmd.Parameters.Add("val3", maxDate);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Finances @GetFinancesByDate(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Finances tempFinances = new Finances();
                        tempFinances.Id = (dataReader["id"].ToString());
                        tempFinances.Dokument = (dataReader["dokument"].ToString());
                        tempFinances.Opis = (dataReader["opis"].ToString());

                        tempFinances.Kwota = (dataReader["kwota"].ToString());
                        tempFinances.TypTransakcji = (dataReader["typ_transakcji"].ToString());

                        tempFinances.Odbiorca = (dataReader["odbiorca"].ToString());
                        tempFinances.Zrodlo = (dataReader["zrodlo"].ToString());

                        tempFinances.DataTransakcji = (dataReader["data_transakcji"].ToString());
                        if (!string.IsNullOrEmpty(tempFinances.DataTransakcji)) tempFinances.DataTransakcji =
                              Convert.ToDateTime(tempFinances.DataTransakcji).ToString("yyyy-MM-dd");
                        Li.Add(tempFinances);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Finances @GetFinancesByDate() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Finances @GetFinancesByDate():  Success");
            return Li;
        }




        public static int GetMoneySum(string jednostkaId)
        {
            List<Finances> Li = new List<Finances>();
            var success = false;
            var numberOfAttempts = 6;
            var sum = 0;
            string sqlsqlQuery = "select kwota, typ_transakcji from osp_finanse_transakcje WHERE jednostka_id = :val1";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();

                    Console.WriteLine("@Finances @GetMoneySum()  Connected status: " + oracleConn.State +
                      $" [Approaches remaining: {numberOfAttempts}]");

                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Finances @GetMoneySum(): No data found");
                        oracleConn.Close();
                        return 0;
                    }

                    while (dataReader.Read())
                    {
                        Finances tempFinances = new Finances();
                        tempFinances.Kwota = (dataReader["kwota"].ToString());
                        tempFinances.TypTransakcji = (dataReader["typ_transakcji"].ToString());
                        Li.Add(tempFinances);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Finances @GetMoneySum() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Finances @GetMoneySum():  Success");

            try
            {
                foreach (var el in Li)
                {
                    if (el.TypTransakcji == Finances.TransactionTypes.Wplyw 
                        || el.TypTransakcji == Finances.TransactionTypes.KorektaPlus)
                        sum += int.Parse(el.Kwota);
                    else if (el.TypTransakcji == Finances.TransactionTypes.Wydatek 
                        || el.TypTransakcji == Finances.TransactionTypes.KorektaMinus)
                        sum -= int.Parse(el.Kwota);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("@Finances @GetMoneySum() Error: " + ex);
                return 0;
            }

            return sum;
        }

        /*
        public static int GetCorrectionSum(string jednostkaId)
        {
            var success = false;
            var numberOfAttempts = 6;
            var sum = 0;
            string sqlsqlQuery = $"select kwota from osp_finanse Where jednostka_id = :val1";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Finances @GetCorrectionSum()  Connected status: " + oracleConn.State +
                       $" [Approaches remaining: {numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Action @GetCorrectionSum(): No data found");
                        return 0;
                    }

                    while (dataReader.Read())
                    {
                        sum = int.Parse(dataReader["kwota"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Finances @GetCorrectionSum() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Finances @GetCorrectionSum(): Success");
            return sum;
        }
        */

        public static string InsertFinances(Finances obj , string jednostkaId)
        {
            var success = false;
            var numberOfAttempts = 6;
            var currentSum = GetMoneySum(jednostkaId);

            if(obj.TypTransakcji == TransactionTypes.Wplyw 
                || obj.TypTransakcji == TransactionTypes.KorektaPlus)
                currentSum += int.Parse(obj.Kwota);
            else if (obj.TypTransakcji == TransactionTypes.Wydatek
                || obj.TypTransakcji == TransactionTypes.KorektaMinus)
                currentSum -= int.Parse(obj.Kwota);

            var sqlQuery = "INSERT INTO osp_finanse_transakcje" +
                "(jednostka_id, dokument, opis, kwota, typ_transakcji, odbiorca, zrodlo, data_transakcji, aktualna_suma ) " +
                "VALUES (:val1 , :val2, :val3 , :val4 , :val5, :val6, :val7, TO_DATE( :val8 , 'YYYY-MM-DD'), :val9 )";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();

                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    cmd.Parameters.Add("val2", obj.Dokument);
                    cmd.Parameters.Add("val3", obj.Opis);
                    cmd.Parameters.Add("val4", obj.Kwota);
                    cmd.Parameters.Add("val5", obj.TypTransakcji);
                    cmd.Parameters.Add("val6", obj.Odbiorca);
                    cmd.Parameters.Add("val7", obj.Zrodlo);
                    cmd.Parameters.Add("val8", obj.DataTransakcji);
                    cmd.Parameters.Add("val9", currentSum);

                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@---->Finances[InsertFinances]: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }


            Console.WriteLine("@---->Finances[InsertFinances]: Success");
            oracleConn.Close();
            return "(✓) Dodano pozycję";
        }


        /*
        public static string AddSumCorrection(string jednostkaId, string val)
        {
            DeleteSumCorrection(jednostkaId); //Tabela ogranicza się do jednej pozycji. 

            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "INSERT INTO osp_finanse (id, jednostka_id,  kwota) VALUES (1, :val1 , :val2)";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();

                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    cmd.Parameters.Add("val2", val);
                   

                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@---->Finances[AddSumCorrection]: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }


            Console.WriteLine("@---->Finances[AddSumCorrection]: Success");
            oracleConn.Close();
            return "(✓) Dodano pozycję";
        }

        public static string DeleteSumCorrection(string jednostkaId)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "DELETE FROM osp_finanse WHERE jednostka_id = :val1 ";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();

                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@---->Finances[DeleteSumCorrection]: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }

            Console.WriteLine("@---->Finances[DeleteSumCorrection]: Success");
            oracleConn.Close();
            return "(✓) Usunieto";
        }

        */


        public static string EditFinances(Finances obj)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "UPDATE osp_finanse_transakcje SET dokument = :val1, " +
                " opis = :val2, kwota =:val3 , typ_transakcji = :val4 , odbiorca = :val5 ," +
                " zrodlo = :val6 , data_transakcji = TO_DATE( :val7 , 'YYYY-MM-DD') where id = :val8 ";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();

                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", obj.Dokument);
                    cmd.Parameters.Add("val2", obj.Opis);
                    cmd.Parameters.Add("val3", obj.Kwota);
                    cmd.Parameters.Add("val4", obj.TypTransakcji);
                    cmd.Parameters.Add("val5", obj.Odbiorca);
                    cmd.Parameters.Add("val6", obj.Zrodlo);
                    cmd.Parameters.Add("val7", obj.DataTransakcji);
                    cmd.Parameters.Add("val8", obj.Id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@---->Finances[EditFinances]: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }

            Console.WriteLine("@---->Finances[EditFinances]: Success");
            oracleConn.Close();
            return "(✓) Powodzenie";
        }
    }

}
