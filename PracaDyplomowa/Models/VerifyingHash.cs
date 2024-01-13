using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PracaDyplomowa.Models
{
    public class VerifyingHash :DbConnection
    {

        public VerifyingHash()
        { 
        }
        public string Id { get; set; }
        public string UzytkownikId { get; set; }
        public string HashString { get; set; }
        public string CreateData { get; set; }



        public string InsertHash(string uzytkownik_id , string hashString)
        {
            var sqlQuery = $"INSERT INTO OSP_TEMPHASH (uzytkownik_id, hash_string) " +
                $"VALUES( :val1 , :val2)";
            var success = false;
            var numberOfAttempts = 6;

            var oracleConn = new OracleConnection(ConnString);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", uzytkownik_id);
                    cmd.Parameters.Add("val2", hashString);

                    //await Task.Run(() => cmd.ExecuteNonQuery());
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@---->insertData[InsertHash]: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@---->insertData[InsertHash]: Success");
            oracleConn.Close();
            return "(✓) Powodzenie";
        }


        public void GetHash(string hashString) //newer than 10min
        {
            var success = false;
            var numberOfAttempts = 6;
            Console.WriteLine("--->@GetHash()");
            string sqlsqlQuery = $"select * from osp_temphash WHERE hash_string = :val1 " +
                $"AND data_utworzenia> CURRENT_TIMESTAMP - interval '10' minute";
            OracleConnection oracleConn = new OracleConnection(ConnString);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    Console.WriteLine("Open connection...");
                    oracleConn.Open();
                    Console.WriteLine("Connected status: " + oracleConn.State);
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", hashString);

                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("No data found");
                        return;
                    }

                    while (dataReader.Read())
                    {
                        Id = (dataReader["id"].ToString());
                        UzytkownikId = (dataReader["uzytkownik_id"].ToString());
                        HashString = (dataReader["hash_string"].ToString());
                        CreateData = (dataReader["data_utworzenia"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine(ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("Connection status: " + oracleConn.State);

        }
    }
}
