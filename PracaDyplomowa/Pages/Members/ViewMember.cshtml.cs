using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PracaDyplomowa.Models;

namespace PracaDyplomowa.Pages.Members
{
    [Authorize]
    public class ViewMemberModel : PageModel
    {
        //communication with js
        [BindProperty(SupportsGet = true)]
        public string passAlert { get; set; }

        [BindProperty(SupportsGet = true)]
        public string selectedMemberId { get; set; }

        public User UserInfo { get; set; } = new User();
        public Claim CurrentLoggedUnit { get; set; }

        public List<Models.Action> ListOfActions { get; set; } = new List<Models.Action>();

        public List<Models.Equipment> ListOfEquipmentAssignedToMember = new List<Models.Equipment>();

        public List<Models.User> ListOfCourses { get; set; } = new List<Models.User>();

        public void OnGet()
        {
            try
            {             
                if (User.IsInRole(Models.User.UserType.Standard))
                    selectedMemberId = User.FindFirst(x => x.Type == "LoggedId").Value;
               

                CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
                UserInfo = new User(selectedMemberId, CurrentLoggedUnit.Value, "uzytkownik_id");
                (int minutesSum, int iloscAkcji )= Models.User.getTimeWorked(selectedMemberId);

                UserInfo.CzasPracy = minutesSum / 60 + "h : " + minutesSum % 60 + "min";
                UserInfo.IloscAkcji = iloscAkcji.ToString();

                if (!string.IsNullOrEmpty(UserInfo.DataWstapienia))
                    UserInfo.DataWstapienia = Convert.ToDateTime(UserInfo.DataWstapienia).Date.ToString("yyyy-MM-dd");
                if (!string.IsNullOrEmpty(UserInfo.WaznoscBadan))
                    UserInfo.WaznoscBadan = Convert.ToDateTime(UserInfo.WaznoscBadan).Date.ToString("yyyy-MM-dd");

                ListOfActions = Models.Action.GetActionsbyMember(selectedMemberId);

                ListOfEquipmentAssignedToMember = Models.Equipment.GetEquipmentAssignedToMember(selectedMemberId).allData;

                ListOfCourses = Models.User.GetCourses(CurrentLoggedUnit.Value, selectedMemberId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("MembersView Error:" +ex.Message);
            }
            foreach (var x in ListOfActions)
            {
                Console.WriteLine(x.CzasZdarzenia);
                Console.WriteLine(x.CzasZakonczenia);
                Console.WriteLine("---------------------");
            }
        }
    }
}
