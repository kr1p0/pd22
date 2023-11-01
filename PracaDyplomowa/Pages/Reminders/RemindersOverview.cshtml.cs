using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PracaDyplomowa.Models;

namespace PracaDyplomowa.Pages.Reminders
{
    [Authorize(Roles = Models.User.UserType.Admin)]
    public class RemindersOverviewModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Sort { get; set; }

        public List<Reminder> ReminderList { get; set; } = new List<Reminder>();

        public Claim CurrentLoggedUnit { get; set; }

        [BindProperty(SupportsGet = true)]
        public string passAlert { get; set; }

        //Display Options  // Binding by name
        [BindProperty]
        public string customRadioFilter { get; set; }
        [BindProperty]
        public string customRadioSort { get; set; }



        public void OnGet()
        {
            string cookieValueFilter = Request.Cookies["FilterReminderOverview"];
            string cookieValueSort = Request.Cookies["SortReminderOverview"];

            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");

            if (cookieValueFilter == "all")
            {
                ReminderList = Reminder.GetAllReminders(CurrentLoggedUnit.Value);
            }

            else//(cookieValueFilter == "current")
            {
                ReminderList = Reminder.GetAllCurrentReminders();
            }


            if (ReminderList.Count == 0 || ReminderList ==null) return;


            if (cookieValueSort == "title")
                ReminderList = ReminderList.OrderBy(x => x.Tytul).ToList();
            else if (cookieValueSort == "content")
                ReminderList = ReminderList.OrderBy(x => x.Tresc).ToList();
            else //(cookieValueSort == "date")
                try
                {
                    ReminderList =  ReminderList.OrderBy(x => Convert.ToDateTime(x.CzasRozpoczecia)).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("RemindersOverviewModel Error[Cannot Convert ToDateTime]: "
                        + ex.Message);
                }

        }

        public void OnPostSaveDisplayOptions()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");

            if (customRadioFilter == "current")
            {
                Response.Cookies.Append("FilterReminderOverview", "current");
                ReminderList = Reminder.GetAllCurrentReminders(CurrentLoggedUnit.Value);
            }
            else if (customRadioFilter == "all")
            {
                Response.Cookies.Append("FilterReminderOverview", "all");
                ReminderList = Reminder.GetAllReminders(CurrentLoggedUnit.Value);
            }

            if (ReminderList.Count == 0)
                return;

            if (ReminderList!=null && customRadioSort == "title")
            {
                Response.Cookies.Append("SortReminderOverview", "title");
                ReminderList = ReminderList.OrderBy(x => x.Tytul).ToList();
            }

            else if (ReminderList != null && customRadioSort == "content")
            {
                Response.Cookies.Append("SortReminderOverview", "content");
                ReminderList = ReminderList.OrderBy(x => x.Tresc).ToList();
            }

            else if (ReminderList != null && customRadioSort == "date")
            {
                Response.Cookies.Append("SortReminderOverview", "date");
                try
                {
                    ReminderList = ReminderList.OrderBy(x => Convert.ToDateTime(x.CzasRozpoczecia)).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("RemindersOverviewModel Error[Cannot Convert ToDateTime]: "
                        + ex.Message);
                }
            }
        }

        public void OnPostRemoveById(string delNotiId)
        {
            var result = Models.CalendarEvent.RemoveEvent(delNotiId);
            passAlert = result;
            OnGet();
        }

    }
}
