using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PracaDyplomowa.Models
{
    public class Unit : DbConnection
    {
        public Unit()
        { 
        }
        public Unit(string nazwaJednostki)
        {
            GetUnitData(nazwaJednostki);
        }
        public string Jednostka_id { get; set; }
        public string NazwaJednostki { get; set; }
    

        private void GetUnitData(string nazwaJednostki)
        {
            var success = false;
            var numberOfAttempts = 6;
           
            // Set TnsAdmin value to directory location of tnsnames.ora and sqlnet.ora files
            //OracleConfiguration.TnsAdmin = @"Wallet_pd";

            // Set WalletLocation value to directory location of the ADB wallet (i.e. cwallet.sso)
            //OracleConfiguration.WalletLocation = @"Wallet_pd";


            string sqlsqlQuery = $"select * from osp_jednostka Where nazwa_jednostki = :val1";
            OracleConnection oracleConn = new OracleConnection(ConnString);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    
                    oracleConn.Open();
                    Console.WriteLine("@Unit @GetUnitData()  Connected status: " + oracleConn.State +
                     $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", nazwaJednostki);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Unit @GetUnitData(): No data found");
                        oracleConn.Close();
                        return;
                    }

                    while (dataReader.Read())
                    {
                        Jednostka_id = (dataReader["jednostka_id"].ToString());
                        NazwaJednostki = (dataReader["nazwa_jednostki"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Unit @GetUnitData() Error:" + ex.Message);
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
