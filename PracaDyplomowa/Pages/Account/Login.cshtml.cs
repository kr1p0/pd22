using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using PracaDyplomowa.Models;

namespace PracaDyplomowa.Pages.Account
{
    public class LoginModel : PageModel
    {
        private IHubContext<Hubs.NotificationHub> _hubContext;

        public LoginModel(IHubContext<Hubs.NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        //Login
        [BindProperty]
        public string LoginInput { get; set; }
        [BindProperty]
        public string LoginPasswordInput { get; set; }

        //Registration
        [BindProperty]
        public string NameInput { get; set; }
        [BindProperty]
        public string LastnameInput { get; set; }
        [BindProperty]
        public string RegistrationPasswordInput { get; set; }
        [BindProperty]
        public string RegistrationEmailInput { get; set; }
        [BindProperty]
        public string FireProtectionUnitInput { get; set; }

        //Password Reminder
        [BindProperty]
        public string ForgotenPasswordEmail { get; set; }

        //communication with js
        [BindProperty(SupportsGet = true)]
        public string passAlert { get; set; }



        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Index");
            }
            return null;
        }
        public async Task<IActionResult> OnPostLogin(string returnurl)
        {

            var userInfo = new User(LoginInput);

            if (!ModelState.IsValid) return Page();

            if (!string.IsNullOrEmpty(userInfo.Email))
            {
                if (BCrypt.Net.BCrypt.Verify(LoginPasswordInput, userInfo.Haslo))
                {
                    if (userInfo.Zatwierdzony == "n")
                    {
                        passAlert = "🛈 Konto oczekuje weryfikacji administratora";
                        return Page();
                    }

                    var claims = new List<Claim>();
                    if(userInfo.TypKonta == Models.User.UserType.Admin)
                        claims.Add(new Claim(ClaimTypes.Role, Models.User.UserType.Admin));
                    else
                        claims.Add(new Claim(ClaimTypes.Role, Models.User.UserType.Standard));
                    claims.Add(new Claim(ClaimTypes.Name, userInfo.Imie));
                    claims.Add(new Claim(ClaimTypes.Email, userInfo.Email));
                    claims.Add(new Claim(type: "Unit", value: userInfo.Jednostka_id));
                    claims.Add(new Claim(type: "LastName", value: userInfo.Nazwisko));
                    claims.Add(new Claim(type: "LoggedId", value: userInfo.UzytkownikId));
                    claims.Add(new Claim(type: "UserType", value: userInfo.TypKonta));

                    var identity = new ClaimsIdentity(claims, "Ciastko");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync("Ciastko", claimsPrincipal);
                    return string.IsNullOrEmpty(returnurl) ? RedirectToPage("/Index") : Redirect(returnurl); //"/Index"
                    //The RedirectToPage() method is referring to a Razor cshtml "page" rather than a html page
                    //If you want to redirect to a url you can use Redirect()
                }
                else
                    passAlert = "(🗙) Nieprawidlowe haslo";
            }
            else
                passAlert = "(🗙) Nieprawidłowy login";
            return Page();

        }
        public async void OnPostRegistration()
        {
            var unitInfo = new Unit(FireProtectionUnitInput);
            if (string.IsNullOrEmpty(unitInfo.Jednostka_id))
            {
                passAlert = "(🗙) Jednostka nie widnieje w bazie";
                return;
            }

            var userInfo = new User(RegistrationEmailInput, unitInfo.Jednostka_id, "email");
            if (!string.IsNullOrEmpty(userInfo.Email))
            {
                passAlert = "(🗙) Email powiązany z innym kontem";
                return;
            }

            var hashedRegistrationPasswordInput = BCrypt.Net.BCrypt.HashPassword(RegistrationPasswordInput);
            var register = new Registration(unitInfo.Jednostka_id, Models.User.UserType.Standard, 
                NameInput, LastnameInput, RegistrationEmailInput, hashedRegistrationPasswordInput, 'n');

            passAlert = register.Result;

            if (register.Success)
                await _hubContext.Clients.Group(unitInfo.Jednostka_id).SendAsync("ReceiveMessage2", "addNot");

        }
        public void OnPostForgotPassword()
        {

            var userInfo = new User(ForgotenPasswordEmail);

            if (string.IsNullOrEmpty(userInfo.Email))
            {
                passAlert = "(🗙) Email nie widnieje w bazie";
                return;
            }

            var verifyingHash = CustomStr.GenerateRandomString(300);
            //var verifyingHash = BCrypt.Net.BCrypt.HashPassword(userInfo.Email + rndString);
            var obj = new VerifyingHash();
            obj.InsertHash(userInfo.UzytkownikId, verifyingHash);
            var currentHost = HttpContext.Request.Host;
            var link = $"https://{currentHost}/Account/ForgotPass?verifyingHash={verifyingHash}";
            string firstRow = $"<h1>Witaj, {userInfo.Imie} {userInfo.Nazwisko}. Prosiłeś o zmianę hasła.</h1><br>";
            string secondRow = "<h2>Aby potwierdzić tą czynność <a href= '" + link + "' > KLIK </a> </h2>";
            string divStyle = "background-color:#f6f6f8;border-radius:8px;align-content:center;" +
                "text-align:center;padding:10px;border:solid thin #e8e8ea";
            string mailbody = "<div style= '" + divStyle + "' >"+ firstRow + secondRow + "</div>";
            var emaiList = new List<string>();
            emaiList.Add(userInfo.Email);
            passAlert = Email.sendMail(emaiList, "Potwierdzenie prośby o reset hasła", mailbody);


            ForgotenPasswordEmail = null;
            ModelState.Clear();

        }
    }
}
