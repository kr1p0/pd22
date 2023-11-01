using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PracaDyplomowa.Pages.Account
{
    public class LogoutModel : PageModel
    {
        //communication with js
        [BindProperty(SupportsGet = true)]
        public string passAlert { get; set; }

        public async Task<IActionResult> OnGet()
        {
            await HttpContext.SignOutAsync("Ciastko");
            return RedirectToPage("/Account/Login", new { passAlert = passAlert});
        }
    }
}
