using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PracaDyplomowa.Models;
using QRCoder;

namespace PracaDyplomowa.Pages.QRCode
{
    [Authorize(Roles = Models.User.UserType.Admin)]
    public class QRCodeGenerateModel : PageModel
    {
        [BindProperty (SupportsGet =true)]
        public string selectedEquipmentId { get; set; }

        [BindProperty]
        public List<Models.Equipment> EquipmentInfo { get; set; } = new List<Models.Equipment>();

        public Claim CurrentLoggedUnit { get; set; }

        public void OnGet()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            EquipmentInfo = Models.Equipment.GetAllEquipmentData(CurrentLoggedUnit.Value);
        }

       
        public IActionResult OnGetSelectAllEquipmentInfo()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            EquipmentInfo = Models.Equipment.GetAllEquipmentData(CurrentLoggedUnit.Value);
            return new JsonResult(EquipmentInfo);
        }
       
        public void OnPost()
        {
        }

    }
}
