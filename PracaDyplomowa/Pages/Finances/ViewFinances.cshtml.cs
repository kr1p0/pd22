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
    [Authorize]
    public class ViewFinancesModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string selectedFinancesId { get; set; }

        public Claim CurrentLoggedUnit { get; set; }

        public Models.Finances ViewFinances { get; set; } = new Models.Finances();

        public void OnGet()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            ViewFinances = new Models.Finances(selectedFinancesId);
        }
    }
}
