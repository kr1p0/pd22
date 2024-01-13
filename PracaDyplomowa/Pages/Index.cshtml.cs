using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaDyplomowa.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            if (User.IsInRole(Models.User.UserType.Admin))
                return RedirectToPage("/Members/MembersOverview");
            else
                return RedirectToPage("/Restricted/RestrictedMain");
        }

        //For Ajax
        public IActionResult OnGetFindCookie(string key)
        {
            /*
            string cookieValueFromReq = Request.Cookies[key];
            return new JsonResult(cookieValueFromReq);
            */
            return null;
        }

        public void OnPostSaveCookie(string key, string value, int? expireTime)
        {
            // string cookieValueFromReq = Request.Cookies["Key"];  
            /*
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = new DateTimeOffset(2038, 1, 1, 0, 0, 0, TimeSpan.FromHours(0));

            Response.Cookies.Append(key, value, option);
            */
        }
    }
}
