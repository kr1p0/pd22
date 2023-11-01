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
    public class EditFinancescshtmlModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string selectedFinancesId { get; set; }

        public Claim CurrentLoggedUnit { get; set; }

        [BindProperty]
        public Models.Finances EditFinances { get; set; } = new Models.Finances();

        public void OnGet()
        {
            EditFinances = new Models.Finances(selectedFinancesId);
        }

        public IActionResult OnPostEditFinances(string FinancesId)
        {
            EditFinances.Id = FinancesId;
            var result = Models.Finances.EditFinances(EditFinances);
            return RedirectToPage("/Finances/FinancesOverview", new { passAlert = result });
        }
    }
}
