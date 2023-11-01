using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace PracaDyplomowa.Pages.Restricted
{
    [Authorize]
    public class RestrictedMainModel : PageModel
    {
        private IHubContext<Hubs.NotificationHub> _hubContext;

        public RestrictedMainModel(IHubContext<Hubs.NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Models.User UserInfo { get; set; }
        public List<Models.Announcement> AnnouncementLi = new List<Models.Announcement>();
        public string NumberOfUnreadAnnouncements="0";
        public string NumberOfUnreadMsgs = "0";
        public string NumberOfEventsToday = "0";

        public void OnGet()
        {
            var CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            var CurrentLoggedUserId = User.FindFirst(x => x.Type == "LoggedId");
            UserInfo = new Models.User(CurrentLoggedUserId.Value , "uzytkownik_id");
            AnnouncementLi = Models.Announcement.Get2FirstAnnouncements(CurrentLoggedUnit.Value);

            NumberOfUnreadMsgs =  Models.Chat.GetNumberOfUnreadMsgs(CurrentLoggedUnit.Value , UserInfo.ChatWizyta);
            
            NumberOfUnreadAnnouncements = Models.Announcement.GetNumberOfUnreadAnnouncements(CurrentLoggedUnit.Value, UserInfo.OgloszeniaWizyta);

            NumberOfEventsToday = Models.CalendarEvent.AllCalendarEvent.GetAllEvents4Today(CurrentLoggedUnit.Value);
        }

        public IActionResult OnPostGoToUserData()
        {
            var CurrentLoggedUserId = User.FindFirst(x => x.Type == "LoggedId");
            return RedirectToPage("/Members/ViewMember");
        }

        public IActionResult OnPostGoToChat()
        {
            return RedirectToPage("/Chat/ChatView");
        }

        public IActionResult OnPostUpdateMemberStatus(bool dostepny)
        {
            var CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            var CurrentLoggedUserId = User.FindFirst(x => x.Type == "LoggedId");

            var status = dostepny ? Models.User.StandbyStatus.Dostepny : Models.User.StandbyStatus.Niedostepny;
            (var result , var success) = Models.User.updateStatus(status, CurrentLoggedUserId.Value);

            if(success)
                _hubContext.Clients.Group(CurrentLoggedUnit.Value).SendAsync("ReceiveMessage2", "addNot");
            return new JsonResult(result);
        }


        public IActionResult OnPostGoToAnnouncements()
        {
            return RedirectToPage("/Announcement/AnnouncementsOverview");
        }

        public IActionResult OnPostGoToCalendar()
        {
            return RedirectToPage("/Calendar/CalendarView");
        }

        public IActionResult OnPostGoToStatistics()
        {
            return RedirectToPage("/Statistics/StatisticsOverview");
        }
    }
}
