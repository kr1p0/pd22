using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PracaDyplomowa.Pages.Settings
{
    [Authorize]
    public class SettingsOverviewModel : PageModel
    {
        
        public void OnGet()
        {
        }
    }
}
