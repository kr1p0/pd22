using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PracaDyplomowa.Models
{
    public class Chat : DbConnection
    {
        public Chat() { }
        public string Id { get; set; }
        public string JednostkaId { get; set; }
        public string UzytkownikId { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Data { get; set; }
        public string Tresc { get; set; }


        public static List<Chat> GetAllChatMsgs(string jednostkaId, int numberOfRows)
        {
            List<Chat> Li = new List<Chat>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = "select osp_chat.tresc, osp_chat.data_utworzenia, " +
                "osp_uzytkownik.uzytkownik_id, osp_uzytkownik.imie , osp_uzytkownik.nazwisko from " +
                "osp_chat INNER JOIN osp_uzytkownik on osp_chat.uzytkownik_id = osp_uzytkownik.uzytkownik_id " +
                $" WHERE osp_chat.jednostka_id = :val1 ORDER BY data_utworzenia DESC FETCH FIRST :val2 ROWS ONLY ";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();

                    Console.WriteLine("@Chat @GetAllChatMsgs()  Connected status: " + oracleConn.State +
                           $" [Approach: {7 - numberOfAttempts}]");

                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    cmd.Parameters.Add("val2", numberOfRows);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Chat @GetAllChatMsgs(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Chat tempChat = new Chat();
                        tempChat.UzytkownikId = (dataReader["uzytkownik_id"].ToString());
                        tempChat.Imie = (dataReader["imie"].ToString());
                        tempChat.Nazwisko = (dataReader["nazwisko"].ToString());
                        tempChat.Tresc = (dataReader["tresc"].ToString());
                        tempChat.Data = (dataReader["data_utworzenia"].ToString());

                        Li.Add(tempChat);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Chat @GetAllChatMsgs() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Chat @GetAllChatMsgs():  Success");
            return Li;
        }


        public static void InsertMessage(Chat obj)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "INSERT INTO osp_chat(jednostka_id, uzytkownik_id, tresc) " +
                "VALUES (:val1 , :val2, :val3)";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Chat @InsertMessage()  Connected status: " + oracleConn.State +
                           $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", obj.JednostkaId);
                    cmd.Parameters.Add("val2", obj.UzytkownikId);
                    cmd.Parameters.Add("val3", obj.Tresc);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@Chat @InsertMessage() Error: " + ex.Message);
                        oracleConn.Close();
                        return;
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }

            Console.WriteLine("@Chat @InsertMessage(): Success");
            oracleConn.Close();
            return;
        }

        public static string GetNumberOfUnreadMsgs(string unitId, string lastVisitDate)
        {
            var success = false;
            var numberOfAttempts = 6;
            string count = "";
            string sqlsqlQuery = "Select COUNT(*) FROM osp_chat "+
                "Where jednostka_id = :val1 AND data_utworzenia > TO_TIMESTAMP(:val2, 'YYYY//MM/DD HH24:MI:SS,FF9') ";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();
                    Console.WriteLine("@User @GetNumberOfUnreadMsgs()  Connected status: " + oracleConn.State +
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
                        Console.WriteLine("@User @GetNumberOfUnreadMsgs() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@User @GetNumberOfUnreadMsgs(): Success");
            return count;
        }

    }
}
