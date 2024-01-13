using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PracaDyplomowa.Pages.Equipment
{
    [Authorize(Roles = Models.User.UserType.Admin)]
    public class EquipmentOverviewModel : PageModel
    {
        public Claim CurrentLoggedUnit { get; set; }

        public List<Models.Equipment> ListOfEquipment { get; set; } = new List<Models.Equipment>();

        public List<Models.Equipment> ListOfEquipmentAssignedToMember = new List<Models.Equipment>();
        public List<string> ListOfEquipmentIdAssignedToMember = new List<string>();

        [BindProperty(SupportsGet = true)]
        public string passAlert { get; set; }

        [BindProperty]
        public string EquipmentCustomRadioFilter { get; set; }
        [BindProperty]
        public string EquipmentCustomRadioSort { get; set; }

        [BindProperty(SupportsGet = true)]
        public string selectedUserId { get; set; } = null;


        public void OnGet()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            ListOfEquipment = Models.Equipment.GetAllEquipmentData(CurrentLoggedUnit.Value); //all
            if(!string.IsNullOrEmpty(selectedUserId))
                (ListOfEquipmentAssignedToMember, ListOfEquipmentIdAssignedToMember) 
                    = Models.Equipment.GetEquipmentAssignedToMember(selectedUserId);

            if (ListOfEquipment == null || ListOfEquipment.Count == 0) return;

            string cookieValueFilter = Request.Cookies["FilterEquipmentOverview"];
            string cookieValueSort = Request.Cookies["SortEquipmentOverview"];


            if (cookieValueFilter == "cars")
            {
                //ListOfEquipment = ListOfEquipment.FindAll(x => x.Typ == Models.Equipment.EquipmentType.Samochod);
                ListOfEquipment = Models.Equipment.GetAllEquipmentByFilter(CurrentLoggedUnit.Value,
                    "typ", Models.Equipment.EquipmentType.Samochod);
            }

            else if (cookieValueFilter == "tools")
            {
                //ListOfEquipment = ListOfEquipment.FindAll(x => x.Typ == Models.Equipment.EquipmentType.Sprzet);
                ListOfEquipment = Models.Equipment.GetAllEquipmentByFilter(CurrentLoggedUnit.Value,
                   "typ", Models.Equipment.EquipmentType.Sprzet);
            }

            else if (cookieValueFilter == "uniforms")
            {
                //ListOfEquipment = ListOfEquipment.FindAll(x => x.Typ == Models.Equipment.EquipmentType.Umundurowanie);
                ListOfEquipment = Models.Equipment.GetAllEquipmentByFilter(CurrentLoggedUnit.Value,
                   "typ", Models.Equipment.EquipmentType.Umundurowanie);
            }

            else if (cookieValueFilter == "inspection")
            {
                //ListOfEquipment = ListOfEquipment.FindAll(x => !string.IsNullOrEmpty(x.AktualnoscBadan));
                ListOfEquipment = Models.Equipment.GetAllEquipmentNotEmpty(CurrentLoggedUnit.Value,
                   "badania_koniec");
            }

            if (cookieValueSort == "date")
            {
                try
                {
                    ListOfEquipment = ListOfEquipment.OrderBy(x => string.IsNullOrEmpty(x.AktualnoscBadan) ?
                     new DateTime() : Convert.ToDateTime(x.AktualnoscBadan)).ToList(); //new DateTime() as empty date 
                }
                catch (Exception ex)
                {
                    Console.WriteLine("EquipmentOverviewModel Error[Cannot Convert ToDateTime]: "
                        + ex.Message);
                }
            }

            else if (cookieValueSort == "manufacturer")
                ListOfEquipment = ListOfEquipment.OrderBy(x => x.Producent).ToList();

            else
                ListOfEquipment = ListOfEquipment.OrderBy(x => x.Model).ToList();
       
        }

        public IActionResult OnPostSaveDisplayOptions()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            ListOfEquipment = Models.Equipment.GetAllEquipmentData(CurrentLoggedUnit.Value);


            if (ListOfEquipment.Count == 0 || ListOfEquipment == null)
                return Page();

            if (EquipmentCustomRadioFilter == "cars")
            {
                Response.Cookies.Append("FilterEquipmentOverview", "cars");
                //ListOfEquipment = ListOfEquipment.FindAll(x => x.Typ == Models.Equipment.EquipmentType.Samochod);
                ListOfEquipment = Models.Equipment.GetAllEquipmentByFilter(CurrentLoggedUnit.Value,
                    "typ", Models.Equipment.EquipmentType.Samochod);
            }

            else if (EquipmentCustomRadioFilter == "tools")
            {
                Response.Cookies.Append("FilterEquipmentOverview", "tools");
                //ListOfEquipment = ListOfEquipment.FindAll(x => x.Typ == Models.Equipment.EquipmentType.Sprzet);
                ListOfEquipment = Models.Equipment.GetAllEquipmentByFilter(CurrentLoggedUnit.Value,
                    "typ", Models.Equipment.EquipmentType.Sprzet);
            }

            else if (EquipmentCustomRadioFilter == "uniforms")
            {
                Response.Cookies.Append("FilterEquipmentOverview", "uniforms");
                //ListOfEquipment = ListOfEquipment.FindAll(x => x.Typ == Models.Equipment.EquipmentType.Umundurowanie);
                ListOfEquipment = Models.Equipment.GetAllEquipmentByFilter(CurrentLoggedUnit.Value,
                    "typ", Models.Equipment.EquipmentType.Umundurowanie);
            }

            else if (EquipmentCustomRadioFilter == "inspection")
            {
                Response.Cookies.Append("FilterEquipmentOverview", "inspection");
                //ListOfEquipment = ListOfEquipment.FindAll(x => !string.IsNullOrEmpty(x.AktualnoscBadan));
                ListOfEquipment = Models.Equipment.GetAllEquipmentNotEmpty(CurrentLoggedUnit.Value,
                  "badania_koniec");
            }

            else if (EquipmentCustomRadioFilter == "all")
                Response.Cookies.Append("FilterEquipmentOverview", "all");





            if (EquipmentCustomRadioSort == "manufacturer")
            {
                Response.Cookies.Append("SortEquipmentOverview", "manufacturer");
                ListOfEquipment = ListOfEquipment.OrderBy(x => x.Producent).ToList();
            }
            else if (EquipmentCustomRadioSort == "date")
            {
                Response.Cookies.Append("SortEquipmentOverview", "date");
                try
                {
                    ListOfEquipment = ListOfEquipment.OrderBy(x => string.IsNullOrEmpty(x.AktualnoscBadan) ?
                     new DateTime() : Convert.ToDateTime(x.AktualnoscBadan)).ToList(); //new DateTime() as empty date 
                }
                catch (Exception ex)
                {
                    Console.WriteLine("EquipmentOverviewModel " +
                        "Error[Cannot Convert ToDateTime]: " + ex.Message);
                }
            }
            else
                ListOfEquipment = ListOfEquipment.OrderBy(x => x.Model).ToList();

            if(!string.IsNullOrEmpty(selectedUserId))
                return RedirectToPage("/Equipment/EquipmentOverview", new { selectedUserId = selectedUserId });
            return Page();
        }

        public void OnPostAssignEquipmentAjax(List<Models.Equipment> listOfEquipment , string UzytkownikId)
        {
            Models.Equipment.AssignEquipmentToMember(UzytkownikId, listOfEquipment);
        }
    }
}
