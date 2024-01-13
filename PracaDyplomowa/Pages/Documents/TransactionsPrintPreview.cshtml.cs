using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PracaDyplomowa.Pages.Documents
{
    public class TransactionsPrintPreviewModel : PageModel
    {
        public List<Models.Finances> ListOfFinances { get; set; } = new List<Models.Finances>();

        [BindProperty(SupportsGet = true)]
        public string Title { get; set; }

        [BindProperty(SupportsGet = true)]
        public string PreparedDateFrom { get; set; }

        [BindProperty(SupportsGet = true)]
        public string PreparedDateTo { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool inflow { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool outflow { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool adjustmentPlus { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool adjustmentMinus { get; set; }
        public void OnGet()
        {
            var CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            var minDate = string.IsNullOrEmpty(PreparedDateFrom) ? "1800-01-01" : PreparedDateFrom;
            var maxDate = string.IsNullOrEmpty(PreparedDateTo) ? DateTime.Now.ToString("yyyy-MM-dd") : PreparedDateTo;
            var allFinances = Models.Finances.GetFinancesByDate(CurrentLoggedUnit.Value, minDate, maxDate);
            ListOfFinances = allFinances.FindAll((x) =>  x.TypTransakcji == Models.Finances.TransactionTypes.Wplyw);
            ListOfFinances.AddRange(allFinances.FindAll((x) => x.TypTransakcji == Models.Finances.TransactionTypes.Wydatek));
            ListOfFinances.AddRange(allFinances.FindAll((x) => x.TypTransakcji == Models.Finances.TransactionTypes.KorektaPlus));
            ListOfFinances.AddRange(allFinances.FindAll((x) => x.TypTransakcji == Models.Finances.TransactionTypes.KorektaMinus));
            ListOfFinances = ListOfFinances.OrderByDescending(x => Convert.ToDateTime(x.DataTransakcji)).ToList();
        }
    }
}
