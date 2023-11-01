using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PracaDyplomowa.Pages.Documents
{
    [Authorize]
    public class DocumentsOverviewModel : PageModel
    {

        public string CurrentLoggedLastName { get; set; }
        public string CurrentLoggedName { get; set; }

        public IActionResult OnGet()
        {
            return RedirectToPage("/Documents/ListOfActions");
        }
    }
}
