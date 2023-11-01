using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PracaDyplomowa.Pages.Equipment
{
    [Authorize(Roles = Models.User.UserType.Admin)]
    public class AddEquipmentModel : PageModel
    {
        private readonly IWebHostEnvironment _rootEnv;

        public AddEquipmentModel(IWebHostEnvironment env)
        {
            _rootEnv = env; //for root path
        }

        [BindProperty]
        public IFormFile uploadedLogo { get; set; }

        public Claim CurrentLoggedUnit { get; set; }

        [BindProperty]
        public Models.Equipment EquipmentObj { get; set; } = new Models.Equipment();

        [BindProperty]
        public Models.Equipment.EquipmentType EqType { get; set; } = new Models.Equipment.EquipmentType();

        public void OnGet()
        {
        }

        public IActionResult OnPostAddEquipment()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            EquipmentObj.JednostkaId = CurrentLoggedUnit.Value;
            var CurrentLoggedUser = User.FindFirst(x => x.Type == "LoggedId"); //administrator
            var fileName = "";

            if (uploadedLogo != null)
            {
                string fileExtension = Path.GetExtension(uploadedLogo.FileName);
                string webRootPath = _rootEnv.WebRootPath;
                var FilePath = Path.Combine(webRootPath, "uploads/img/equipment");
                if (!Directory.Exists(FilePath))
                    Directory.CreateDirectory(FilePath);
                fileName = Guid.NewGuid() + fileExtension; //unique file name
                var filePath = Path.Combine(FilePath, fileName.ToString());
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    uploadedLogo.CopyTo(fs);
                }
            }

            EquipmentObj.ImgNazwa = fileName;

            (var result, var insertedId) = Models.Equipment.InsertEquipment(EquipmentObj);

            if (!string.IsNullOrEmpty(EquipmentObj.AktualnoscBadan) && !string.IsNullOrEmpty(insertedId))
            {
                Models.CalendarEvent obj = new Models.CalendarEvent();
                obj.JednostkaId = CurrentLoggedUnit.Value;
                obj.Tytul = "Termin przegladu";
                obj.Tresc = $"{EquipmentObj.Typ}: {EquipmentObj.Producent} \nModel: {EquipmentObj.Model} \nUplynela waznosc przegladu.";
                obj.DataRozpoczecia = EquipmentObj.AktualnoscBadan;
                obj.CzasRozpoczecia = EquipmentObj.AktualnoscBadan; //"yyyy-MM-dd HH:mm"
                obj.PowiadomienieEmail = "t";
                obj.PowiadomienieTelefon = "t";
                obj.TloHex = "#163975";
                obj.CzcionkaHex = "#fff";
                obj.SprzetId = insertedId;
                var LiOfRecipients = new List<string>() { new(CurrentLoggedUser.Value)};
                Models.CalendarEvent.insertEvent(obj, LiOfRecipients);
            }

            return RedirectToPage("/Equipment/EquipmentOverview", new { passAlert = result });
        }
    }
}
