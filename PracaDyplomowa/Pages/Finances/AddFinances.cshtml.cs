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
    public class AddFinancesModel : PageModel
    {
        public Claim CurrentLoggedUnit { get; set; }

        [BindProperty]
        public Models.Finances NewFinances { get; set; } = new Models.Finances();

        public void OnGet()
        {
        }

        public IActionResult OnPostAddFinances()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            var result = Models.Finances.InsertFinances(NewFinances, CurrentLoggedUnit.Value);
            return RedirectToPage("/Finances/FinancesOverview", new { passAlert = result });
        }
    }
}
