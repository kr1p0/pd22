using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PracaDyplomowa.Pages.Announcement
{
    [Authorize]
    public class AnnouncementsOverviewModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string passAlert { get; set; }

        public List<Models.Announcement> ListOfAnnouncements { get; set; } = new List<Models.Announcement>();

        [BindProperty]
        public DateTime startDate { get; set; } = DateTime.Now;

        public void OnGet()
        {
            var CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            ListOfAnnouncements = Models.Announcement.GetAllAnnouncements(CurrentLoggedUnit.Value);
            ListOfAnnouncements = ListOfAnnouncements.OrderByDescending(x => Convert.ToDateTime(x.Data)).ToList();
            var CurrentLoggedUserId = User.FindFirst(x => x.Type == "LoggedId").Value;
            Models.User.UpdateVisitDate(Models.User.LastVisit.Ogloszenia, CurrentLoggedUserId);
        }

        public void OnPostAjaxDeleteAnnouncement(string idToRemove)
        {
            Models.Announcement.DeleteAnnouncement(idToRemove);
        }

        public void OnPostSaveDisplayOptions()
        {
            var CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            
            var minDate =Convert.ToDateTime(startDate).ToString("dd-MM-yyyy HH:mm:ss");
            ListOfAnnouncements = Models.Announcement.GetAnnouncementsByMinDate(CurrentLoggedUnit.Value, minDate);
            ListOfAnnouncements = ListOfAnnouncements.OrderByDescending(x => Convert.ToDateTime(x.Data)).ToList();
        }

    }
}
