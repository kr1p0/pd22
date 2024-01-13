using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PracaDyplomowa.Models
{
    public class Equipment : DbConnection
    {
        public  class EquipmentType
        {
            public static string Umundurowanie { get; set; } = "Umundurowanie";
            public static string Samochod { get; set; } = "Samochód";
            public static string Sprzet { get; set; } = "Sprzęt";
        }

        public Equipment()
        {
        }

        public Equipment(string jednostkaId, string itemId)
        {
            GetEquipmentData(jednostkaId, itemId);
        }

        public string Id { get; set; }
        public string JednostkaId { get; set; }
        public string Producent { get; set; }
        public string Model { get; set; }
        public string Sn { get; set; }
        public string Ilosc { get; set; }
        public string Wartosc { get; set; }
        public string AktualnoscBadan { get; set; }
        public string DataZakupu { get; set; }
        public string Typ { get; set; }
        public string PowiazanyEventId { get; set; } //termin badan
        public string ImgNazwa { get; set; }
        public string PrzypisanyDoId { get; set; }





        private void GetEquipmentData(string jednostkaId, string itemId)
        {
            var success = false;
            var numberOfAttempts = 6;
            string sqlsqlQuery = "select osp_inwentarz.* , osp_kalendarz.id " +
                "as kalendarz_id from osp_inwentarz " +
                "join osp_kalendarz on osp_inwentarz.id = osp_kalendarz.sprzet_id " +
                "Where osp_inwentarz.jednostka_id = :val1 AND osp_inwentarz.id = :val2 ";
            OracleConnection oracleConn = new OracleConnection(ConnString);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                   
                    oracleConn.Open();
                    Console.WriteLine("@GetEquipment @GetEquipmentData()  Connected status: " + oracleConn.State +
                       $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    cmd.Parameters.Add("val2", itemId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("No data found");
                        oracleConn.Close();
                        GetEquipmentDataAlternative(jednostkaId, itemId); //jesli brak terminu badan
                        return;
                    }

                    while (dataReader.Read())
                    {
                        Id = (dataReader["id"].ToString());
                        JednostkaId = (dataReader["jednostka_id"].ToString());
                        Producent = (dataReader["producent"].ToString());
                        Model = (dataReader["nazwa_modelu"].ToString());
                        Sn = (dataReader["sn"].ToString());
                        Ilosc = (dataReader["ilosc"].ToString());
                        Wartosc = (dataReader["wartosc"].ToString());
                        AktualnoscBadan = (dataReader["badania_koniec"].ToString());
                        if (!string.IsNullOrEmpty(AktualnoscBadan)) AktualnoscBadan =
                                Convert.ToDateTime(AktualnoscBadan).ToString("yyyy-MM-dd");
                        DataZakupu = (dataReader["data_zakupu"].ToString());
                        if (!string.IsNullOrEmpty(DataZakupu)) DataZakupu =
                               Convert.ToDateTime(DataZakupu).ToString("yyyy-MM-dd");
                        Typ = (dataReader["typ"].ToString());
                          PowiazanyEventId = (dataReader["kalendarz_id"].ToString());
                        ImgNazwa = (dataReader["img_nazwa"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Equipment @GetEquipmentData() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Equipment @GetEquipmentData(): Success");
        }


        private void GetEquipmentDataAlternative(string jednostkaId, string itemId)
        {
            var success = false;
            var numberOfAttempts = 6;
            string sqlsqlQuery = $"select * from  osp_inwentarz  Where jednostka_id = :val1 AND id = :val2 ";
            OracleConnection oracleConn = new OracleConnection(ConnString);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();
                    Console.WriteLine("@GetEquipment @GetEquipmentData()  Connected status: " + oracleConn.State +
                       $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    cmd.Parameters.Add("val2", itemId);
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
                        JednostkaId = (dataReader["jednostka_id"].ToString());
                        Producent = (dataReader["producent"].ToString());
                        Model = (dataReader["nazwa_modelu"].ToString());
                        Sn = (dataReader["sn"].ToString());
                        Ilosc = (dataReader["ilosc"].ToString());
                        Wartosc = (dataReader["wartosc"].ToString());
                        AktualnoscBadan = (dataReader["badania_koniec"].ToString());
                        if (!string.IsNullOrEmpty(AktualnoscBadan)) AktualnoscBadan =
                                Convert.ToDateTime(AktualnoscBadan).ToString("yyyy-MM-dd");
                        DataZakupu = (dataReader["data_zakupu"].ToString());
                        if (!string.IsNullOrEmpty(DataZakupu)) DataZakupu =
                               Convert.ToDateTime(DataZakupu).ToString("yyyy-MM-dd");
                        Typ = (dataReader["typ"].ToString());
                        ImgNazwa = (dataReader["img_nazwa"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Equipment @GetEquipmentData() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Equipment @GetEquipmentData(): Success");
        }


        public static List<Equipment> GetAllEquipmentData(string jednostkaId)
        {
            List<Equipment> Li = new List<Equipment>();
            var success = false;
            var numberOfAttempts = 6;
           
            string sqlsqlQuery = "select * from osp_inwentarz WHERE jednostka_id = :val1";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@GetEquipment @GetAllEquipmentData()  Connected status: " + oracleConn.State +
                       $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@GetEquipment @GetAllEquipmentData():  No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Equipment tempEquipment = new Equipment();
                        tempEquipment.Id = (dataReader["id"].ToString());
                        tempEquipment.JednostkaId = (dataReader["jednostka_id"].ToString());
                        tempEquipment.Producent = (dataReader["producent"].ToString());
                        tempEquipment.Model = (dataReader["nazwa_modelu"].ToString());
                        tempEquipment.Sn = (dataReader["sn"].ToString());
                        tempEquipment.Ilosc = (dataReader["ilosc"].ToString());
                        tempEquipment.Wartosc = (dataReader["wartosc"].ToString());
                        tempEquipment.AktualnoscBadan = (dataReader["badania_koniec"].ToString());
                        if (!string.IsNullOrEmpty(tempEquipment.AktualnoscBadan)) tempEquipment.AktualnoscBadan =
                                Convert.ToDateTime(tempEquipment.AktualnoscBadan).ToString("yyyy-MM-dd");
                        tempEquipment.DataZakupu = (dataReader["data_zakupu"].ToString());
                        if (!string.IsNullOrEmpty(tempEquipment.DataZakupu)) tempEquipment.DataZakupu =
                               Convert.ToDateTime(tempEquipment.DataZakupu).ToString("yyyy-MM-dd");
                        tempEquipment.Typ = (dataReader["typ"].ToString());
                        Li.Add(tempEquipment);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Equipment @GetAllEquipmentData() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Equipment @GetAllEquipmentData(): Success");
            return Li;
        }


        public static List<Equipment> GetAllEquipmentByFilter(string jednostkaId , string column , string value)
        {
            List<Equipment> Li = new List<Equipment>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = $"select * from osp_inwentarz WHERE jednostka_id = :val1 AND {column} = :val2";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@GetEquipment @GetAllEquipmentByFilter()  Connected status: " + oracleConn.State +
                      $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    cmd.Parameters.Add("val2", value);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@GetEquipment @GetAllEquipmentByFilter(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Equipment tempEquipment = new Equipment();
                        tempEquipment.Id = (dataReader["id"].ToString());
                        tempEquipment.JednostkaId = (dataReader["jednostka_id"].ToString());
                        tempEquipment.Producent = (dataReader["producent"].ToString());
                        tempEquipment.Model = (dataReader["nazwa_modelu"].ToString());
                        tempEquipment.Sn = (dataReader["sn"].ToString());
                        tempEquipment.Ilosc = (dataReader["ilosc"].ToString());
                        tempEquipment.Wartosc = (dataReader["wartosc"].ToString());
                        tempEquipment.AktualnoscBadan = (dataReader["badania_koniec"].ToString());
                        if (!string.IsNullOrEmpty(tempEquipment.AktualnoscBadan)) tempEquipment.AktualnoscBadan =
                               Convert.ToDateTime(tempEquipment.AktualnoscBadan).ToString("yyyy-MM-dd");
                        tempEquipment.DataZakupu = (dataReader["data_zakupu"].ToString());
                        if (!string.IsNullOrEmpty(tempEquipment.DataZakupu)) tempEquipment.DataZakupu =
                               Convert.ToDateTime(tempEquipment.DataZakupu).ToString("yyyy-MM-dd");
                        tempEquipment.Typ = (dataReader["typ"].ToString());
                        Li.Add(tempEquipment);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Equipment @GetAllEquipmentByFilter() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Equipment @GetAllEquipmentByFilter(): Success");
            return Li;
        }


        public static List<Equipment> GetAllEquipmentNotEmpty(string jednostkaId, string column)
        {
            List<Equipment> Li = new List<Equipment>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = $"select * from osp_inwentarz WHERE jednostka_id = :val1 ";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@GetEquipment @GetAllEquipmentNotEmpty()  Connected status: " + oracleConn.State +
                        $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", jednostkaId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@GetEquipment @GetAllEquipmentNotEmpty(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        if (string.IsNullOrEmpty(dataReader[column].ToString())) continue;  // NotEmpty
                        Equipment tempEquipment = new Equipment();
                        tempEquipment.Id = (dataReader["id"].ToString());
                        tempEquipment.JednostkaId = (dataReader["jednostka_id"].ToString());
                        tempEquipment.Producent = (dataReader["producent"].ToString());
                        tempEquipment.Model = (dataReader["nazwa_modelu"].ToString());
                        tempEquipment.Sn = (dataReader["sn"].ToString());
                        tempEquipment.Ilosc = (dataReader["ilosc"].ToString());
                        tempEquipment.Wartosc = (dataReader["wartosc"].ToString());
                        tempEquipment.AktualnoscBadan = (dataReader["badania_koniec"].ToString());
                        if (!string.IsNullOrEmpty(tempEquipment.AktualnoscBadan)) tempEquipment.AktualnoscBadan =
                              Convert.ToDateTime(tempEquipment.AktualnoscBadan).ToString("yyyy-MM-dd");
                        tempEquipment.DataZakupu = (dataReader["data_zakupu"].ToString());
                        if (!string.IsNullOrEmpty(tempEquipment.DataZakupu)) tempEquipment.DataZakupu =
                               Convert.ToDateTime(tempEquipment.DataZakupu).ToString("yyyy-MM-dd");
                        tempEquipment.Typ = (dataReader["typ"].ToString());
                        Li.Add(tempEquipment);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Equipment @GetAllEquipmentNotEmpty() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Equipment @GetAllEquipmentNotEmpty(): Success");
            return Li;
        }

        public static (string result , string insertedId) InsertEquipment(Equipment obj)
        {
            var success = false;
            var numberOfAttempts = 6;
            var returnedID = "";

            var sqlQuery = "INSERT INTO osp_inwentarz " +
                "(jednostka_id, producent, nazwa_modelu, sn, ilosc, " +
                "wartosc, badania_koniec, data_zakupu, typ, img_nazwa) " +
                "VALUES (:val0, :val1 , :val2, :val3 , :val4 , :val5, TO_DATE(:val6, 'YYYY/MM/DD'), " +
                "TO_DATE(:val7, 'YYYY/MM/DD'), :val8, :val9) RETURNING id INTO :returnInsertedRowId";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@GetEquipment @InsertEquipment()  Connected status: " + oracleConn.State +
                      $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val0", obj.JednostkaId);
                    cmd.Parameters.Add("val1", obj.Producent);
                    cmd.Parameters.Add("val2", obj.Model);
                    cmd.Parameters.Add("val3", obj.Sn);
                    cmd.Parameters.Add("val4", obj.Ilosc);
                    cmd.Parameters.Add("val5", obj.Wartosc);
                    cmd.Parameters.Add("val6", obj.AktualnoscBadan);
                    cmd.Parameters.Add("val7", obj.DataZakupu);
                    cmd.Parameters.Add("val8", obj.Typ);
                    cmd.Parameters.Add("val9", obj.ImgNazwa);
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
                        Console.WriteLine("@GetEquipment @InsertEquipment() Error: " + ex.Message);
                        oracleConn.Close();
                        return ("(🗙) Wystąpił błąd", null);
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }


          
            Console.WriteLine("@GetEquipment @InsertEquipment(): Success");
            oracleConn.Close();
            return ("(✓) Dodano pozycję", returnedID);
        }



        public static (string result, bool success) EditEquipment(Equipment obj)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "UPDATE osp_inwentarz SET " +
                "producent = :val1, nazwa_modelu = :val2, sn = :val3, ilosc = :val4, wartosc = :val5, " +
                "badania_koniec = TO_DATE(:val6, 'YYYY/MM/DD'), data_zakupu = TO_DATE(:val7, 'YYYY/MM/DD'), " +
                "typ = :val8, img_nazwa = :val9  WHERE id = :val10";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@GetEquipment @EditEquipment()  Connected status: " + oracleConn.State +
                      $" [Approach: {7 - numberOfAttempts}]");
                    OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", obj.Producent);
                    cmd.Parameters.Add("val2", obj.Model);
                    cmd.Parameters.Add("val3", obj.Sn);
                    cmd.Parameters.Add("val4", obj.Ilosc);
                    cmd.Parameters.Add("val5", obj.Wartosc);
                    cmd.Parameters.Add("val6", obj.AktualnoscBadan);
                    cmd.Parameters.Add("val7", obj.DataZakupu);
                    cmd.Parameters.Add("val8", obj.Typ);
                    cmd.Parameters.Add("val9", obj.ImgNazwa);
                    cmd.Parameters.Add("val10", obj.Id);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@GetEquipment @EditEquipment() Error: " + ex.Message);
                        oracleConn.Close();
                        return ("(🗙) Wystąpił błąd", false);
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }

            Console.WriteLine("@GetEquipment @EditEquipment(): Success");
            oracleConn.Close();
            return ("(✓) Powodzenie", true);
        }



        public static string DeleteEquipment(string id)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "DELETE FROM osp_inwentarz WHERE id = :val1";

            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@GetEquipment @DeleteEquipment()  Connected status: " + oracleConn.State +
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
                        Console.WriteLine("@GetEquipment @DeleteEquipment()  Error: " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@GetEquipment @DeleteEquipment(): Success");
            oracleConn.Close();
            return "(✓) Usunięto";
        }



        public static (List<Equipment> allData, List<String> idOnly) GetEquipmentAssignedToMember(string uzytkownikId)
        {
            List<Equipment> Li = new List<Equipment>();
            List<string> LiIdOnly = new List<string>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = "select osp_uzytkownik_przypisany_sprzet.sprzet_id, " +
                "osp_uzytkownik_przypisany_sprzet.ilosc , osp_inwentarz.producent, " +
                "osp_inwentarz.id,  osp_inwentarz.nazwa_modelu,  osp_inwentarz.sn " +
                "from osp_inwentarz join osp_uzytkownik_przypisany_sprzet " +
                "on osp_inwentarz.id = osp_uzytkownik_przypisany_sprzet.sprzet_id  " +
                "WHERE osp_uzytkownik_przypisany_sprzet.uzytkownik_id = :val1 ";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();

                    Console.WriteLine("@Equipment @GetEquipmentAssignedToMember()  Connected status: " + oracleConn.State +
                      $" [Approach: {7 - numberOfAttempts}]");

                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", uzytkownikId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Equipment @GetEquipmentAssignedToMember(): No data found");
                        oracleConn.Close();
                        return (Li ,LiIdOnly);
                    }

                    while (dataReader.Read())
                    {
                        Equipment tempEquipment = new Equipment();
                        tempEquipment.Id = (dataReader["id"].ToString());
                        tempEquipment.Producent = (dataReader["producent"].ToString());
                        tempEquipment.Model = (dataReader["nazwa_modelu"].ToString());
                        tempEquipment.Sn = (dataReader["sn"].ToString());
                          tempEquipment.Ilosc = (dataReader["ilosc"].ToString());
                        Li.Add(tempEquipment);
                        LiIdOnly.Add((dataReader["sprzet_id"].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Equipment @GetEquipmentAssignedToMember() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Equipment @GetEquipmentAssignedToMember():  Success");
            return (Li, LiIdOnly);
        }


        public static string  AssignEquipmentToMember(string uzytkownikId,  List<Equipment> listOfassignedEquipment)
        {
            RemoveAssignedEquipmentByMemberId(uzytkownikId);
            List<Equipment> Li = new List<Equipment>();
            List<string> LiIdOnly = new List<string>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlQuery = "insert into osp_uzytkownik_przypisany_sprzet (uzytkownik_id, sprzet_id, ilosc) values (:val1, :val2, :val3) ";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Equipment @AssignEquipmentToMember()  Connected status: " + oracleConn.State +
                      $" [Approach: {7 - numberOfAttempts}]");

                   
                    foreach (var item in listOfassignedEquipment)
                    {
                        OracleCommand cmd = new OracleCommand(sqlQuery, oracleConn);
                        cmd.Parameters.Add("val1", uzytkownikId);
                        cmd.Parameters.Add("val2", item.Id);
                        cmd.Parameters.Add("val3", item.Ilosc);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;
                    if (numberOfAttempts == 1)
                    {
                        Console.WriteLine("@Equipment @AssignEquipmentToMember() Error: " + ex.Message);
                        return "(🗙) Wystąpił błąd";
                    }
                        
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Equipment @AssignEquipmentToMember():  Success");
            return "Powodzenie";
        }

        public static string RemoveAssignedEquipmentByMemberId(string id)
        {
            var success = false;
            var numberOfAttempts = 6;

            var sqlQuery = "DELETE FROM  osp_uzytkownik_przypisany_sprzet WHERE uzytkownik_id = :val1";


            var oracleConn = new OracleConnection(ConnStringStatic);

            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;
                    oracleConn.Open();
                    Console.WriteLine("@Action @RemoveAssignedEquipmentByMemberId()  Connected status: " + oracleConn.State +
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
                        Console.WriteLine("@Action @RemoveAssignedEquipmentByMemberId(): " + ex.Message);
                        oracleConn.Close();
                        return "(🗙) Wystąpił błąd";
                    }
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            Console.WriteLine("@Action @RemoveAssignedEquipmentByMemberId(): Success");
            oracleConn.Close();
            return "(✓) Usunięto";
        }


        public static List<Models.User> AssignedMembersToEquipment(string sprzetId)
        {
            List<Models.User> Li = new List<Models.User>();
            var success = false;
            var numberOfAttempts = 6;

            string sqlsqlQuery = "select osp_uzytkownik.imie,  osp_uzytkownik.nazwisko,  " +
                "osp_uzytkownik.uzytkownik_id, osp_uzytkownik_przypisany_sprzet.ilosc " +
                "from osp_uzytkownik join osp_uzytkownik_przypisany_sprzet " +
                "on osp_uzytkownik.uzytkownik_id = osp_uzytkownik_przypisany_sprzet.uzytkownik_id  " +
                "WHERE osp_uzytkownik_przypisany_sprzet.sprzet_id = :val1 ";
            OracleConnection oracleConn = new OracleConnection(ConnStringStatic);
            while (!success && numberOfAttempts > 1)
            {
                try
                {
                    success = true;

                    oracleConn.Open();

                    Console.WriteLine("@Equipment @AssignedMembersToEquipment()  Connected status: " + oracleConn.State +
                      $" [Approach: {7 - numberOfAttempts}]");

                    OracleCommand cmd = new OracleCommand(sqlsqlQuery, oracleConn);
                    cmd.Parameters.Add("val1", sprzetId);
                    OracleDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows)
                    {
                        Console.WriteLine("@Equipment @AssignedMembersToEquipment(): No data found");
                        oracleConn.Close();
                        return Li;
                    }

                    while (dataReader.Read())
                    {
                        Models.User tempMember = new Models.User();
                        tempMember.Imie = (dataReader["imie"].ToString());
                        tempMember.Nazwisko = (dataReader["nazwisko"].ToString());
                        tempMember.IloscSprzetu = (dataReader["ilosc"].ToString());
                        tempMember.UzytkownikId = (dataReader["uzytkownik_id"].ToString());
                        Li.Add(tempMember);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    numberOfAttempts--;

                    if (numberOfAttempts == 1)
                        Console.WriteLine("@Equipment @AssignedMembersToEquipment() Error: " + ex.Message);
                    else
                        Thread.Sleep(10000 / numberOfAttempts);
                    oracleConn.Close();
                }
            }
            oracleConn.Close();
            Console.WriteLine("@Equipment @AssignedMembersToEquipment():  Success");
            return Li;
        }


    }

}
