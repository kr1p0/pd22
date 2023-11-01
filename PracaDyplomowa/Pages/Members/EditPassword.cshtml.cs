using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PracaDyplomowa.Models;

namespace PracaDyplomowa.Pages.Members
{
    [Authorize]
    public class EditPasswordModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string selectedMemberId { get; set; }
        public User UserInfo { get; set; } = new User();
        public Claim CurrentLoggedUnit { get; set; }
        [BindProperty]
        public string newPasswordInput { get; set; }

        public void OnGet()
        {
            if (User.IsInRole(Models.User.UserType.Standard))
                selectedMemberId = User.FindFirst(x => x.Type == "LoggedId").Value;
            

            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            UserInfo = new User(selectedMemberId, CurrentLoggedUnit.Value, "uzytkownik_id");
        }

        public IActionResult OnPostNewPass(string userId)
        {
            if (User.IsInRole(Models.User.UserType.Standard))
                selectedMemberId = User.FindFirst(x => x.Type == "LoggedId").Value;

            var hashedNewPasswordInput = BCrypt.Net.BCrypt.HashPassword(newPasswordInput);
            var result = Models.User.updatePassword(hashedNewPasswordInput, userId);

            if (User.IsInRole(Models.User.UserType.Standard))
                return RedirectToPage("/Members/ViewMember", new { passAlert = result });

            return RedirectToPage("/Members/MembersOverview", new { passAlert = result });
        }
    }
}
