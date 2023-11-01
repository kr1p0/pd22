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
using Microsoft.AspNetCore.SignalR;
using PracaDyplomowa.Models;

namespace PracaDyplomowa.Pages.Members
{
    [Authorize]
    public class EditMemberModel : PageModel
    {
        private IHubContext<Hubs.NotificationHub> _hubContext;

        private readonly IWebHostEnvironment _rootEnv; 

        public EditMemberModel(IHubContext<Hubs.NotificationHub> hubContext , IWebHostEnvironment env)
        {
            _hubContext = hubContext;
            _rootEnv = env; //for root path
        }

      

     

        [BindProperty(SupportsGet = true)]
        public string selectedMemberId { get; set; }
        
        [BindProperty]
        public User UserInfo { get; set; } = new User();

        [BindProperty]
        public string Status4Compare { get; set; }

        public Claim CurrentLoggedUnit { get; set; }

        [BindProperty]
        public IFormFile uploadedLogo { get; set; }


        public void OnGet()
        {

            try
            {
                if (User.IsInRole(Models.User.UserType.Standard))
                    selectedMemberId = User.FindFirst(x => x.Type == "LoggedId").Value;

                CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
                UserInfo = new User(selectedMemberId, CurrentLoggedUnit.Value, "uzytkownik_id");
                if (!string.IsNullOrEmpty(UserInfo.DataWstapienia))
                    UserInfo.DataWstapienia = Convert.ToDateTime(UserInfo.DataWstapienia).Date.ToString("yyyy-MM-dd");
                if (!string.IsNullOrEmpty(UserInfo.WaznoscBadan))
                    UserInfo.WaznoscBadan = Convert.ToDateTime(UserInfo.WaznoscBadan).Date.ToString("yyyy-MM-dd");

                Status4Compare = UserInfo.Status;
            }
            catch(Exception ex)
            { 
                Console.WriteLine("EditMember Error:"+ ex.Message);
            }

          
        }

        public IActionResult OnPostUpdateUserData(string userId , string relatedEventId ,string previousStatus)
        {
            var CurrentLoggedUser = User.FindFirst(x => x.Type == "LoggedId"); //administrator
            var CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            var fileName = "";

            if (User.IsInRole(Models.User.UserType.Standard))
                userId = User.FindFirst(x => x.Type == "LoggedId").Value;

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

            UserInfo.ImgNazwa = fileName;

            (var result, var success) = Models.User.updateMainData(UserInfo, userId);

            if (success)
            {
                Models.CalendarEvent.RemoveEvent(relatedEventId);

                if (!string.IsNullOrEmpty(UserInfo.WaznoscBadan))
                {
                    Models.CalendarEvent obj = new Models.CalendarEvent();
                    obj.JednostkaId = CurrentLoggedUnit.Value;
                    obj.Tytul = "Termin badan";
                    obj.Tresc = $"{UserInfo.Imie} {UserInfo.Nazwisko} \nUplynela waznosc badan.";
                    obj.DataRozpoczecia = Convert.ToDateTime(UserInfo.WaznoscBadan).ToString("yyyy-MM-dd");
                    obj.CzasRozpoczecia = UserInfo.WaznoscBadan; //"yyyy-MM-dd HH:mm"
                    obj.PowiadomienieEmail = "t";
                    obj.PowiadomienieTelefon = "t";
                    obj.TloHex = "#163975";
                    obj.CzcionkaHex = "#fff";
                    obj.UzytkownikId = userId;
                    var LiOfRecipients = new List<string>() { new(CurrentLoggedUser.Value), new(userId) };
                    Models.CalendarEvent.insertEvent(obj, LiOfRecipients);
                }
                if (previousStatus!= UserInfo.Status)
                    _hubContext.Clients.Group(CurrentLoggedUnit.Value).SendAsync("ReceiveMessage2", "addNot");
            }




            if (User.IsInRole(Models.User.UserType.Standard))
                return RedirectToPage("/Members/ViewMember", new { passAlert = result });
            return RedirectToPage("/Members/MembersOverview", new { passAlert = result });
        }

        public IActionResult OnPostDeleteUser(string userId)
        {
            var result =  Models.User.DeleteUser(userId);
            if (User.IsInRole(Models.User.UserType.Standard))
                return RedirectToPage("/Account/Logout", new { passAlert = result });
            return RedirectToPage("/Members/MembersOverview", new { passAlert = result });
        }

        public IActionResult OnGetDeleteUser(string userId)
        {
            var result = Models.User.DeleteUser(userId);
            if (User.IsInRole(Models.User.UserType.Standard))
                return RedirectToPage("/Account/Logout", new { passAlert = result });
            return RedirectToPage("/Members/MembersOverview", new { passAlert = result });
        }
    }
}
