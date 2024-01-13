using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PracaDyplomowa.Models;

namespace PracaDyplomowa.Pages.Notifications
{
    [Authorize(Roles = Models.User.UserType.Admin)]
    public class NotificationsOverviewModel : PageModel
    {
        [BindProperty]
        public string passAlert { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        public Claim CurrentLoggedUnit { get; set; }

        public List<Notification> ListOfNoti { get; set; } = new List<Notification>();
        public List<Notification> ListOfStatusUpdates { get; set; } = new List<Notification>();
        public List<Notification> ListOfOverdueUserDeadlines { get; set; } = new List<Notification>();
        public List<Notification> ListOfOverdueEquipmentDeadlines { get; set; } = new List<Notification>();

        //Display Options  // Binding by name
        [BindProperty]
        public string NotiCustomRadioFilter { get; set; }
        [BindProperty]
        public string NotiCustomRadioSort { get; set; }




        public void OnGet()
        {
             CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");

            string cookieValueFilter = Request.Cookies["FilterNotiOverview"];
            string cookieValueSort = Request.Cookies["SortNotiOverview"];


            if (cookieValueFilter == "status")
            {
                ListOfNoti = Notification.getUserStatusUpdates(CurrentLoggedUnit.Value);
            }

            else if (cookieValueFilter == "confirmation")
            {
                ListOfNoti = Notification.getUsersToApprove(CurrentLoggedUnit.Value);
            }

            else//(cookieValueFilter == "all")
            {
                ListOfNoti = Notification.getUsersToApprove(CurrentLoggedUnit.Value);
                ListOfStatusUpdates = Notification.getUserStatusUpdates(CurrentLoggedUnit.Value);
                ListOfOverdueUserDeadlines = Notification.GetOverdueUserDeadlines(CurrentLoggedUnit.Value);
                ListOfOverdueEquipmentDeadlines = Notification.GetOverdueEquipmentDeadlines(CurrentLoggedUnit.Value);
                ListOfNoti.AddRange(ListOfStatusUpdates);
                ListOfNoti.AddRange(ListOfOverdueUserDeadlines);
                ListOfNoti.AddRange(ListOfOverdueEquipmentDeadlines);
            }

            if (ListOfNoti == null || ListOfNoti.Count ==0) return;

            if (cookieValueSort == "name")
                ListOfNoti = ListOfNoti.OrderBy(x => x.Imie).ThenBy(x => x.Nazwisko).ToList();
            else if (cookieValueSort == "lastName")
                ListOfNoti = ListOfNoti.OrderBy(x => x.Nazwisko).ThenBy(x => x.Imie).ToList();
            else if (cookieValueSort == "email")
                ListOfNoti = ListOfNoti.OrderBy(x => x.Email).ToList();
            else //(cookieValueSort == "date")
                try
                {
                    ListOfNoti = ListOfNoti.OrderByDescending(x => Convert.ToDateTime(x.DataUtworzenia)).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("NotificationsOverviewModel Error[Cannot Convert ToDateTime]: "
                        + ex.Message);
                }
        }

        public void OnPostSaveDisplayOptions()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");

            if (NotiCustomRadioFilter == "status")
            {
                Response.Cookies.Append("FilterNotiOverview", "status");
                ListOfNoti = Notification.getUserStatusUpdates(CurrentLoggedUnit.Value);
            }

            else if (NotiCustomRadioFilter == "confirmation")
            {
                Response.Cookies.Append("FilterNotiOverview", "confirmation");
                ListOfNoti = Notification.getUsersToApprove(CurrentLoggedUnit.Value);
            }
            else if (NotiCustomRadioFilter == "all")
            {
                Response.Cookies.Append("FilterNotiOverview", "all");
                ListOfNoti = Notification.getUsersToApprove(CurrentLoggedUnit.Value);
                ListOfStatusUpdates = Notification.getUserStatusUpdates(CurrentLoggedUnit.Value);
                ListOfOverdueUserDeadlines = Notification.GetOverdueUserDeadlines(CurrentLoggedUnit.Value);
                ListOfOverdueEquipmentDeadlines = Notification.GetOverdueEquipmentDeadlines(CurrentLoggedUnit.Value);
                ListOfNoti.AddRange(ListOfStatusUpdates);
                ListOfNoti.AddRange(ListOfOverdueUserDeadlines);
                ListOfNoti.AddRange(ListOfOverdueEquipmentDeadlines);
            }


            if (ListOfNoti.Count == 0 || ListOfNoti == null)
                return;

            if (NotiCustomRadioSort == "name")
            {
                Response.Cookies.Append("SortNotiOverview", "name");
                ListOfNoti = ListOfNoti.OrderBy(x => x.Imie).ToList();
            }

            else if (NotiCustomRadioSort == "lastName")
            {
                Response.Cookies.Append("SortNotiOverview", "lastName");
                ListOfNoti = ListOfNoti.OrderBy(x => x.Nazwisko).ToList();
            }

            else if (NotiCustomRadioSort == "email")
            {
                Response.Cookies.Append("SortNotiOverview", "email");
                ListOfNoti = ListOfNoti.OrderBy(x => x.Email).ToList();
            }

            else if (NotiCustomRadioSort == "date")
            {
                Response.Cookies.Append("SortNotiOverview", "date");

                try
                {
                    ListOfNoti = ListOfNoti.OrderByDescending(x => Convert.ToDateTime(x.DataUtworzenia)).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("NotificationsOverviewModel Error[Cannot Convert ToDateTime]: "
                        + ex.Message);
                }
            }
        }

        public IActionResult OnGetLook4NotificationsAjax()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            ListOfNoti = Notification.getUsersToApprove(CurrentLoggedUnit.Value);
            ListOfStatusUpdates = Notification.getUserStatusUpdates(CurrentLoggedUnit.Value);
            ListOfOverdueUserDeadlines = Notification.GetOverdueUserDeadlines(CurrentLoggedUnit.Value);
            ListOfOverdueEquipmentDeadlines = Notification.GetOverdueEquipmentDeadlines(CurrentLoggedUnit.Value);
            ListOfNoti.AddRange(ListOfStatusUpdates);
            ListOfNoti.AddRange(ListOfOverdueUserDeadlines);
            ListOfNoti.AddRange(ListOfOverdueEquipmentDeadlines);
            return new JsonResult(ListOfNoti.Count);
        }



        public void OnPostApprove(string selectedMemberId)
        {
            passAlert = Notification.approveUser(selectedMemberId);
            OnGet();
        }


        public void OnPostRemoveNewUserStatus(string NotiId)
        {
            passAlert = Notification.RemoveNewUserStatus(NotiId);
            OnGet();
        }

    }
}
