using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PracaDyplomowa.Models;
namespace PracaDyplomowa.Pages.Calendar
{
    [Authorize]
    public class CalendarViewModel : PageModel
    {
        [BindProperty]
        public CalendarEvent CalendarEvent { get; set; } = new CalendarEvent();
        [BindProperty]
        public bool PowiadomienieTelefon { get; set; }
        [BindProperty]
        public bool PowiadomienieEmail { get; set; }
        public CalendarEvent.AllCalendarEvent calendarEvents{ get; set; }
        public Claim CurrentLoggedUnit { get; set; }
        [BindProperty (SupportsGet =true)]
        public string passAlert { get; set; }
       
        public List<Models.User> ListOfUsers { get; set; } = new List<Models.User>();

        public void OnGet()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            calendarEvents = new CalendarEvent.AllCalendarEvent(CurrentLoggedUnit.Value);
        
            ListOfUsers = Models.User.GetListOfAllUser(CurrentLoggedUnit.Value);
        }

        public IActionResult OnGetSelectEventsByPartDate(string dateElement, string partDate)
        {
            
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            calendarEvents = new CalendarEvent.AllCalendarEvent(CurrentLoggedUnit.Value, dateElement, partDate);
            return new JsonResult(calendarEvents.Li);
        }

        public IActionResult OnGetSelectEventsByDate( string dateX)
        {

            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            calendarEvents = new CalendarEvent.AllCalendarEvent(CurrentLoggedUnit.Value, dateX);
            return new JsonResult(calendarEvents.Li.OrderBy(x => Convert.ToDateTime(x.CzasRozpoczecia)));
        }

        public ActionResult OnPostAddEventAjax(CalendarEvent obj , List<string> listOfRecipients)
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            obj.JednostkaId = CurrentLoggedUnit.Value;

          

            var result = CalendarEvent.insertEvent(obj , listOfRecipients);

          

            CalendarEvent = null;
            ModelState.Clear();
            return new JsonResult(result);
        }

        public ActionResult OnPostRemoveByIdAjax(string delNotiId)
        {
            if (!User.IsInRole(Models.User.UserType.Admin))
            {
                return new JsonResult("Brak uprawnieñ");
            }
            var result = Models.CalendarEvent.RemoveEvent(delNotiId);
            return new JsonResult(result);
            //OnGet();
        }

        public ActionResult OnGetNumberOfEventsToday()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            var result = Models.CalendarEvent.AllCalendarEvent.GetAllEvents4Today(CurrentLoggedUnit.Value);
       
            return new JsonResult(result);
            //OnGet();
        }
    }
}
