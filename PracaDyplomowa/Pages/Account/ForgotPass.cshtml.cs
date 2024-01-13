using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PracaDyplomowa.Models;

namespace PracaDyplomowa.Pages.Account
{
   
    public class ForgotPassModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public string verifyingHash { get; set; }

        [BindProperty]
        public string newPasswordInput { get; set; }

        public bool Confirmed { get; set; } = false;

        [BindProperty]
        public VerifyingHash hashStr { get; set; } = new VerifyingHash();

    

        public IActionResult OnGet()
        {
            hashStr.GetHash(verifyingHash);


            if (string.IsNullOrEmpty(hashStr.UzytkownikId))
            {
                Console.WriteLine("Nie znaleziono hash");
                return RedirectToPage("/Error");
                
            }
            else
            {
                Console.WriteLine("Znaleziono hash");
                Confirmed = true;
                return null;
            }
        }


        public IActionResult OnPostNewPass()
        {
            var hashedNewPasswordInput = BCrypt.Net.BCrypt.HashPassword(newPasswordInput);
            var result = Models.User.updatePassword(hashedNewPasswordInput, hashStr.UzytkownikId);

            Thread.Sleep(4100);
            return RedirectToPage("/Account/Login" ,new { passAlert  = result });
        }
    }
}
