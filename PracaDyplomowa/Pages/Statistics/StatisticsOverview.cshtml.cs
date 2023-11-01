using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PracaDyplomowa.Pages.Statistics
{
    [Authorize]
    public class StatisticsOverviewModel : PageModel
    {
        public Claim CurrentLoggedUnit { get; set; }
        public void OnGet()
        {
            OnGetMembersActionCount();
        }

        //https://cezarywalenciuk.pl/blog/programing/group-by-w-linq-w-c
        public IActionResult OnGetPlaceInCompetitionAndCount(string maxDate=null , string minDate = "1800-01-01" )
        {
            if (string.IsNullOrEmpty(maxDate))
                maxDate = DateTime.Now.ToString("yyyy-MM-dd");
            //var x = Li.Count(x => x.ZajeteMiejsce == "4");
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            List<dynamic> resultList = new List<dynamic>();

            var Li = Models.Competition.GetAllCompetitionByDate(CurrentLoggedUnit.Value, minDate , maxDate+ " 23:59");


            var resultlamba = Li.GroupBy(x => int.Parse(x.ZajeteMiejsce)).OrderBy(x=>x.Key);
            foreach(var el in resultlamba)
                resultList.Add(new { zajeteMiejsce = el.Key.ToString(), powtarzalnoscMiejsca = el.Count() });
          
            return new JsonResult(resultList);
        }


        public IActionResult OnGetActionsYearsAndCount(bool yearOnly, bool limitedScope,  string maxDate=null, string minDate = "1800-01-01")
        {
            if (string.IsNullOrEmpty(maxDate))
                maxDate = DateTime.Now.ToString("yyyy-MM-dd");
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            List<dynamic> resultList = new List<dynamic>();

            var Li = Models.Action.GetActionsByDate(CurrentLoggedUnit.Value, minDate ,maxDate + " 23:59");

            //rozszerzenie o brakuj¹ce daty - na potrzeby wykresu 
            if (!limitedScope)
            {
                Li = Li.OrderBy(x => Convert.ToDateTime(x.CzasZdarzeniaPelny)).ToList();
                var startDate = Convert.ToDateTime(Li[0].CzasZdarzeniaPelny);
                var endDate = Convert.ToDateTime(Li[Li.Count - 1].CzasZdarzeniaPelny);
                for (var i = startDate; i < endDate; i = i.AddDays(1))
                {
                    if (!Li.Any(x => Convert.ToDateTime(x.CzasZdarzeniaPelny) == i))
                    {
                        Li.Add(new Models.Action(i.ToString("yyyy-MM-dd")));
                    }
                }
            }


            var resultLambda = yearOnly ? Li.GroupBy(x => Convert.ToDateTime(x.CzasZdarzeniaPelny).Year.ToString()).OrderBy(x => (x.Key))
             : Li.GroupBy(x => Convert.ToDateTime(x.CzasZdarzeniaPelny).ToString("MM/yyyy")).OrderBy(x => (Convert.ToDateTime(x.Key)));


            foreach (var el in resultLambda)
                resultList.Add(new { czasZdarzenia = el.Key.ToString(), iloscZdarzen = el.Where(x=>x.Id != null).Count() }); //where aby pomin¹c akcje puste, obejmuj¹ce tylko datê, na rzecz wykresu

            return new JsonResult(resultList);
        }


        public IActionResult OnGetMembersActionCount(string maxDate=null, string minDate = "1800-01-01")
        {
            if (string.IsNullOrEmpty(maxDate))
                maxDate = DateTime.Now.ToString("yyyy-MM-dd");


            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            var Li =  Models.User.getTimeWorkedAllMembers(CurrentLoggedUnit.Value, minDate ,maxDate + " 23:59");
            List<dynamic> resultList = new List<dynamic>();

            var resultLambda = Li.GroupBy(x => x.UzytkownikId);
            foreach (var el in resultLambda)
            {
                resultList.Add(new {
                        imie = el.First().Imie, nazwisko = el.First().Nazwisko, 
                        iloscAkcji = el.Count(), czasSuma = el.Sum(x => double.Parse(x.CzasPracy))
                    });
              
            }
            return new JsonResult(resultList);
        }


        public IActionResult OnGetTransactionsSumYears(bool yearOnly, bool limitedScope, string maxDate=null, string minDate = "1800-01-01")
        {
            if (string.IsNullOrEmpty(maxDate))
                maxDate = DateTime.Now.ToString("yyyy-MM-dd");

            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            List<dynamic> resultList = new List<dynamic>();

            var Li = Models.Finances.GetFinancesByDate(CurrentLoggedUnit.Value, minDate, maxDate + " 23:59");

            //rozszerzenie o brakuj¹ce daty - na potrzeby wykresu 
            if (!limitedScope)
            {
                try
                {
                    Li = Li.OrderBy(x => Convert.ToDateTime(x.DataTransakcji)).ToList();
                    var startDate = Convert.ToDateTime(Li[0].DataTransakcji);
                    var endDate = Convert.ToDateTime(Li[Li.Count - 1].DataTransakcji);
                    for (var i = startDate; i < endDate; i = i.AddDays(1))
                    {
                        if (!Li.Any(x => Convert.ToDateTime(x.DataTransakcji) == i))
                        {
                            Li.Add(new Models.Finances("0", Models.Finances.TransactionTypes.Wplyw, i.ToString("yyyy-MM-dd")));
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error converting DateTime @OnGetTransactionsSumYears: " + ex);
                }
            }

            foreach (var el in Li)
            {
                if (el.TypTransakcji == Models.Finances.TransactionTypes.Wydatek
                     || el.TypTransakcji == Models.Finances.TransactionTypes.KorektaMinus)
                    el.Kwota = (double.Parse(el.Kwota) * -1).ToString();
            }
            try
            {
                var resultLambda = yearOnly ? Li.GroupBy(x => Convert.ToDateTime(x.DataTransakcji).Year.ToString()).OrderBy(x => (x.Key))
                : Li.GroupBy(x => Convert.ToDateTime(x.DataTransakcji).ToString("MM/yyyy")).OrderBy(x => (Convert.ToDateTime(x.Key)));

                foreach (var el in resultLambda)
                    resultList.Add(new { rokTransakcji = el.Key.ToString(), kwotaTransakcji = el.Sum(x => double.Parse(x.Kwota)) });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error @OnGetTransactionsSumYears: " + ex);
            }
            return new JsonResult(resultList);
        }


        public IActionResult OnGetFinansesSumYears(bool yearOnly, bool limitedScope, string maxDate=null,  string minDate = "1800-01-01")
        {
            if (string.IsNullOrEmpty(maxDate))
                maxDate = DateTime.Now.ToString("yyyy-MM-dd");
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            List<dynamic> resultList = new List<dynamic>();
            double temp = 0;
            var LiSumOnStart = Models.Finances.GetFinancesByDate(CurrentLoggedUnit.Value, "1800-01-01", minDate + " 23:59");
            var Li = Models.Finances.GetFinancesByDate(CurrentLoggedUnit.Value, minDate , maxDate + " 23:59");

            //rozszerzenie o brakuj¹ce daty - na potrzeby wykresu 
            if (!limitedScope)
            {
                Li = Li.OrderBy(x => Convert.ToDateTime(x.DataTransakcji)).ToList();
                var startDate = Convert.ToDateTime(Li[0].DataTransakcji);
                var endDate = Convert.ToDateTime(Li[Li.Count - 1].DataTransakcji);
                for (var i = startDate; i < endDate; i = i.AddDays(1))
                {
                    if (!Li.Any(x => Convert.ToDateTime(x.DataTransakcji) == i))
                    {
                        Li.Add(new Models.Finances("0", Models.Finances.TransactionTypes.Wplyw, i.ToString("yyyy-MM-dd")));
                    }
                }
            }
         
            //starting sum(hidden on chart)
            foreach (var el in LiSumOnStart)
            {
                if (el.TypTransakcji == Models.Finances.TransactionTypes.Wydatek
                     || el.TypTransakcji == Models.Finances.TransactionTypes.KorektaMinus)
                    temp += (double.Parse(el.Kwota) * -1);
                else
                    temp += double.Parse(el.Kwota);
            }

            foreach (var el in Li)
            {
                if (el.TypTransakcji == Models.Finances.TransactionTypes.Wydatek
                     || el.TypTransakcji == Models.Finances.TransactionTypes.KorektaMinus)
                    el.Kwota = (double.Parse(el.Kwota) * -1).ToString();
            }


            var resultLambda = yearOnly ? Li.GroupBy(x => Convert.ToDateTime(x.DataTransakcji).Year.ToString()).OrderBy(x => (x.Key)) 
                : Li.GroupBy(x => Convert.ToDateTime(x.DataTransakcji).ToString("MM/yyyy")).OrderBy(x => (Convert.ToDateTime(x.Key)));

           
            foreach (var el in resultLambda)
            {
                resultList.Add(new { rok = el.Key.ToString(), suma = el.Sum(x => double.Parse(x.Kwota))+temp });
                temp += el.Sum(x => double.Parse(x.Kwota)); //for adding previous years;
            }


            return new JsonResult(resultList);
        }
    }
}
