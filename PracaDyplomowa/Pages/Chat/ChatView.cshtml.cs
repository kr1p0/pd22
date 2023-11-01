using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PracaDyplomowa.Pages.Chat
{
    [Authorize]
    public class ChatViewModel : PageModel
    {
        //for display sender name lastname 
        public string CurrentLoggedLastName { get; set; }
        public string CurrentLoggedName { get; set; }
        public string CurrentLoggedUserId { get; set; }
        public string CurrentLoggedUnit { get; set; }

        public void OnGet()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit").Value;
            CurrentLoggedLastName = User.FindFirst(x => x.Type == "LastName").Value;
            CurrentLoggedUserId = User.FindFirst(x => x.Type == "LoggedId").Value;
            CurrentLoggedName = User.Identity.Name;
            Models.User.UpdateVisitDate(Models.User.LastVisit.Chat , CurrentLoggedUserId);
        }

        public IActionResult OnGetLoadMsgs(int numberOfRows=15)
        {
            var CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit").Value;
            var listOfMsgs = Models.Chat.GetAllChatMsgs(CurrentLoggedUnit , numberOfRows);
            listOfMsgs = listOfMsgs.OrderBy(x => Convert.ToDateTime(x.Data)).ToList();
            return new JsonResult(listOfMsgs);
        }

        public IActionResult OnPostSaveMsg(string msg)
        {
            var newMessage = new Models.Chat();
            newMessage.JednostkaId = User.FindFirst(x => x.Type == "Unit").Value;
            newMessage.UzytkownikId = User.FindFirst(x => x.Type == "LoggedId").Value;
            newMessage.Tresc = msg;
            Models.Chat.InsertMessage(newMessage);
            return new JsonResult("s");
        }

        public void OnPostAjaxUpdateLastVisitDate()
        {
            CurrentLoggedUserId = User.FindFirst(x => x.Type == "LoggedId").Value;
            Models.User.UpdateVisitDate(Models.User.LastVisit.Chat, CurrentLoggedUserId);
        }
    }
}
