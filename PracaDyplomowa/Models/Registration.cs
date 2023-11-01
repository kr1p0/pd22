using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PracaDyplomowa.Models
{
    public class Registration : DbConnection
    {
        //INSERT INTO osp_uzytkownik(jednostka_id, typ_konta, imie, nazwisko, email, haslo, telefon, funkcja, data_wstapienia)
        //VALUES(1,'standardowy','Rafal','Kaminski','phpxd2@gmail.com','qwerty',458765239,'', TO_DATE('13/4/2005', 'DD/MM/YYYY') )
        public Registration()
        {
        }
        public Registration(string jednostka_id, string typ_konta, string imie, string nazwisko, string email, string haslo, char t_n)
        {
            (Result, Success) = insertData(jednostka_id, typ_konta, imie, nazwisko, email, haslo, t_n);
        }

        public string Result { get; set; }
        public bool Success { get; set; }


        private (string result, bool success) insertData(string jednostka_id, string typ_konta, string imie, string nazwisko, string email, string haslo, char t_n)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "INSERT INTO osp_uzytkownik(jednostka_id, typ_konta, imie, nazwisko, email, haslo, zatwierdzony) VALUES " +
          $"(:val1 , :val2 , :val3 ,  :val4 , :val5 , :val6, :val7 )";

            var oracleConn = new OracleConnection(ConnString);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    //await Task.Run(() => cmd.ExecuteNonQuery());
                    cmd.Parameters.Add("val1", jednostka_id);
                    cmd.Parameters.Add("val2", typ_konta);
                    cmd.Parameters.Add("val3", imie);
                    cmd.Parameters.Add("val4", nazwisko);
                    cmd.Parameters.Add("val5", email);
                    cmd.Parameters.Add("val6", haslo);
                    cmd.Parameters.Add("val7", t_n);




                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@---->insertData[Registration]: " + ex.Message);
                        oracleConn.Close();
                        return ("(🗙) Wystąpił błąd", false);
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@---->insertData[Registration]: Success");
            oracleConn.Close();
            return ("(✓) Dodano użytkownika", true);
        }
    }
}
