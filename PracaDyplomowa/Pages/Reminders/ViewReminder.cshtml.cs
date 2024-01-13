using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PracaDyplomowa.Pages.Reminders
{
    [Authorize(Roles = Models.User.UserType.Admin)]
    public class ViewReminderModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string selectedReminderId { get; set; }
        public Models.Reminder ReminderInfo { get; set; } = new Models.Reminder();
        public Claim CurrentLoggedUnit { get; set; }
        public List<Models.User> ListOfUsers { get; set; } = new List<Models.User>();
        public List<string> ListOfReminderRecipients = new List<string>();

        public void OnGet()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            ReminderInfo = new Models.Reminder(selectedReminderId, CurrentLoggedUnit.Value);
            ListOfUsers = Models.User.GetListOfAllUser(CurrentLoggedUnit.Value);
            ListOfReminderRecipients = Models.Reminder.getReminderRecipients(selectedReminderId).id;

        }


    }
}
