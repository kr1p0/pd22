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
    [Authorize]
    public class ViewEquipmentModel : PageModel
    {
        [BindProperty (SupportsGet =true)]
        public string selectedEquipmentId { get; set; }

        public Claim CurrentLoggedUnit { get; set; }

        [BindProperty]
        public Models.Equipment EquipmentObj { get; set; } = new Models.Equipment();

        public List<Models.User> MemebersAssignedToEquipment { get; set; } = new List<Models.User>();

        [BindProperty]
        public Models.Equipment.EquipmentType EqType { get; set; } = new Models.Equipment.EquipmentType();

        public void OnGet()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            EquipmentObj = new Models.Equipment(CurrentLoggedUnit.Value, selectedEquipmentId);
            MemebersAssignedToEquipment = Models.Equipment.AssignedMembersToEquipment(selectedEquipmentId);
        }

    }
}
