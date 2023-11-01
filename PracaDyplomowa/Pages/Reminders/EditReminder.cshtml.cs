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
    public class EditReminderModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string selectedReminderId { get; set; }
        [BindProperty]
        public Models.Reminder ReminderInfo { get; set; } = new Models.Reminder();
        public Claim CurrentLoggedUnit { get; set; }
        [BindProperty]
        public string checkBox1 { get; set; }
        [BindProperty]
        public string checkBox2 { get; set; }
        public List<Models.User> ListOfUsers { get; set; } = new List<Models.User>();
        public List<string> ListOfReminderRecipients = new List<string>();


        public void OnGet()
        {
            try
            {
                CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
                ReminderInfo = new Models.Reminder(selectedReminderId, CurrentLoggedUnit.Value);
                ReminderInfo.DataRozpoczecia = Convert.ToDateTime(ReminderInfo.DataRozpoczecia).Date.ToString("yyyy-MM-dd");
                ListOfUsers = Models.User.GetListOfAllUser(CurrentLoggedUnit.Value);
                ListOfReminderRecipients = Models.Reminder.getReminderRecipients(selectedReminderId).id;
            }
            catch(Exception ex)
            {
                Console.WriteLine("EditReminder Error:" + ex);
            }
            
        }

        public IActionResult OnPostUpdateReminderAjax(Models.Reminder obj , List<string> listOfRecipients)
        {
            var result = Models.Reminder.UpdateReminder(obj , listOfRecipients);
            return new JsonResult(result);
        }

        public IActionResult OnPostHideReminder(string selectedReminderId)
        {
            var Result = Models.Reminder.hideReminder(selectedReminderId);
            return RedirectToPage("/Reminders/RemindersOverview", new { passAlert = Result });
        }

        public IActionResult OnGetHideReminder(string selectedReminderId)
        {
            var Result = Models.Reminder.hideReminder(selectedReminderId);
            return RedirectToPage("/Reminders/RemindersOverview", new { passAlert = Result });
        }
    }
}
