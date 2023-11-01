using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PracaDyplomowa.Pages.Finances
{
    [Authorize(Roles = Models.User.UserType.Admin)]
    public class FinancesOverviewModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string passAlert { get; set; }

        public Claim CurrentLoggedUnit { get; set; }

        public List<Models.Finances> FinancesList { get; set; } = new List <Models.Finances>();

        public string MoneySum { get; set; }

        [BindProperty]
        public string FinancesCustomRadioFilter { get; set; }

        [BindProperty]
        public string FinancesCustomRadioSort { get; set; }

        public void OnGet()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            MoneySum = Models.Finances.GetMoneySum(CurrentLoggedUnit.Value).ToString();



            string cookieValueFilter = Request.Cookies["FilterFinancesOverview"];
            string cookieValueSort = Request.Cookies["SortFinancesOverview"];


            if (cookieValueFilter == "Wplywy")
            {
                FinancesList = Models.Finances.GetAllFinancesByFilter(CurrentLoggedUnit.Value , 
                    "typ_transakcji", Models.Finances.TransactionTypes.Wplyw);
            }

            else if (cookieValueFilter == "Wydatki")
            {
                FinancesList = Models.Finances.GetAllFinancesByFilter(CurrentLoggedUnit.Value,
                    "typ_transakcji", Models.Finances.TransactionTypes.Wydatek);
            }

            else if (cookieValueFilter == "Korekty")
            {
                FinancesList = Models.Finances.GetAllFinancesByCorrection(CurrentLoggedUnit.Value,
                    "typ_transakcji", Models.Finances.TransactionTypes.KorektaPlus, 
                    Models.Finances.TransactionTypes.KorektaMinus);
            }

            else//if "all"
            {
                FinancesList = Models.Finances.GetAllFinances(CurrentLoggedUnit.Value);
            }


            if (FinancesList == null || FinancesList.Count == 0) return;


            if (cookieValueSort == "Kwota")
                FinancesList = FinancesList.OrderBy(x => double.Parse(x.Kwota)).ToList();
            else //== "Data"
            {
                try
                {
                    FinancesList = FinancesList.OrderByDescending(x => Convert.ToDateTime(x.DataTransakcji)).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("FinancesOverviewModel Error[Cannot Convert ToDateTime]: "
                        + ex.Message);
                }
            }

        }


        public void OnPostSaveDisplayOptions()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            FinancesList = Models.Finances.GetAllFinances(CurrentLoggedUnit.Value);



            if (FinancesList.Count == 0 || FinancesList == null)
                return;

            if (FinancesCustomRadioFilter == "Wplywy")
            {
                Response.Cookies.Append("FilterFinancesOverview", "Wplywy");
                FinancesList = Models.Finances.GetAllFinancesByFilter(CurrentLoggedUnit.Value,
                   "typ_transakcji", Models.Finances.TransactionTypes.Wplyw);
            }

            else if (FinancesCustomRadioFilter == "Wydatki")
            {
                Response.Cookies.Append("FilterFinancesOverview", "Wydatki");
                FinancesList = Models.Finances.GetAllFinancesByFilter(CurrentLoggedUnit.Value,
                   "typ_transakcji", Models.Finances.TransactionTypes.Wydatek);
            }

            else if (FinancesCustomRadioFilter == "Korekty")
            {
                Response.Cookies.Append("FilterFinancesOverview", "Korekty");
                FinancesList = Models.Finances.GetAllFinancesByCorrection(CurrentLoggedUnit.Value,
                    "typ_transakcji", Models.Finances.TransactionTypes.KorektaPlus,
                    Models.Finances.TransactionTypes.KorektaMinus);
            }

            else if (FinancesCustomRadioFilter == "all")
            {
                Response.Cookies.Append("FilterFinancesOverview", "all");
                //FinancesList = Models.Finances.GetAllFinances(CurrentLoggedUnit.Value);
            }



            if (FinancesCustomRadioSort == "Kwota")
            {
                Response.Cookies.Append("SortFinancesOverview", "Kwota");
                FinancesList = FinancesList.OrderBy(x => double.Parse(x.Kwota)).ToList();
            }

            else//if == "Data")
            {
                Response.Cookies.Append("SortFinancesOverview", "Data");
                try
                {
                    FinancesList = FinancesList.OrderByDescending(x => Convert.ToDateTime(x.DataTransakcji)).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("FinancesOverviewModel " +
                        "Error[Cannot Convert ToDateTime]: "
                        + ex.Message);
                }
            }
            MoneySum = Models.Finances.GetMoneySum(CurrentLoggedUnit.Value).ToString();
        }

        public IActionResult OnPostEditFinancesSum(string oldSum, string newSum)
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            var sum = int.Parse(newSum) - int.Parse(oldSum);
            var financesObj = new Models.Finances();
            financesObj.Dokument = "Korekta";
            financesObj.Opis = "";
            financesObj.Kwota = Math.Abs(sum).ToString();
            financesObj.TypTransakcji = sum < 0 ? Models.Finances.TransactionTypes.KorektaMinus
                : Models.Finances.TransactionTypes.KorektaPlus;
            financesObj.Odbiorca = "";
            financesObj.Zrodlo = "";
            financesObj.DataTransakcji = DateTime.Now.ToString("yyyy-MM-dd");
            var result = Models.Finances.InsertFinances(financesObj, CurrentLoggedUnit.Value);
            return new JsonResult(result);
        }
    }
}
