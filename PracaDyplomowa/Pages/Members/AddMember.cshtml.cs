using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PracaDyplomowa.Pages.Members
{
    [Authorize(Roles = Models.User.UserType.Admin)]
    public class AddMemberModel : PageModel
    {
        private readonly IWebHostEnvironment _rootEnv;

        public AddMemberModel(IWebHostEnvironment env)
        {
            _rootEnv = env; //for root path
        }

        [BindProperty]
        public Models.User UserInfo { get; set; } = new Models.User();

        [BindProperty]
        public IFormFile uploadedLogo { get; set; }

        public void OnGet()
        {

        }
        public IActionResult OnPostAddUser()
        {
            var CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            var CurrentLoggedUser = User.FindFirst(x => x.Type == "LoggedId"); //administrator
            var fileName = "";

            if (uploadedLogo != null)
            {
                string fileExtension = Path.GetExtension(uploadedLogo.FileName);
                string webRootPath = _rootEnv.WebRootPath;
                var FilePath = Path.Combine(webRootPath, "uploads/img/avatar");
                if (!Directory.Exists(FilePath))
                    Directory.CreateDirectory(FilePath);
                fileName = Guid.NewGuid() + fileExtension; //unique file name
                var filePath = Path.Combine(FilePath, fileName.ToString());
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    uploadedLogo.CopyTo(fs);
                }
            }

            UserInfo.Zatwierdzony = "t";
            UserInfo.Haslo = BCrypt.Net.BCrypt.HashPassword(UserInfo.Haslo);
            UserInfo.ImgNazwa = fileName;
            (var result, var insertedUserId) = Models.User.InsertUser(UserInfo, CurrentLoggedUnit.Value);

            if (!string.IsNullOrEmpty(UserInfo.WaznoscBadan) && !string.IsNullOrEmpty(insertedUserId))
            {
                Models.CalendarEvent obj = new Models.CalendarEvent();
                obj.JednostkaId = CurrentLoggedUnit.Value;
                obj.Tytul = "Termin badan";
                obj.Tresc = $"{UserInfo.Imie} {UserInfo.Nazwisko}  \nUplynela waznosc badan.";
                obj.DataRozpoczecia = Convert.ToDateTime(UserInfo.WaznoscBadan).ToString("yyyy-MM-dd");
                obj.CzasRozpoczecia = UserInfo.WaznoscBadan; //"yyyy-MM-dd HH:mm"
                obj.PowiadomienieEmail = "t";
                obj.PowiadomienieTelefon = "t";
                obj.TloHex = "#163975";
                obj.CzcionkaHex = "#fff";
                obj.UzytkownikId = insertedUserId;
                var LiOfRecipients = new List<string>() { new(CurrentLoggedUser.Value), new(insertedUserId) };
                Models.CalendarEvent.insertEvent(obj , LiOfRecipients);
            }
            return RedirectToPage("/Members/Membersoverview", new { passAlert = result });
        }
    }
}
