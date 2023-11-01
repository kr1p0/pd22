using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PracaDyplomowa.Pages.Actions
{
    [Authorize(Roles = Models.User.UserType.Admin)]
    public class EditActionModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string selectedActionId { get; set; }

        public Claim CurrentLoggedUnit { get; set; }

        public List<Models.User> ListOfUsers { get; set; } = new List<Models.User>();

        public List<string> ListOfActionParticipants = new List<string>();

        [BindProperty]
        public Models.Action ActionObj { get; set; } = new Models.Action();


        public void OnGet()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            ListOfUsers = Models.User.GetListOfAllUser(CurrentLoggedUnit.Value);
            ListOfActionParticipants = Models.Action.GetParticipants(selectedActionId);
            
            ActionObj.GetAction(selectedActionId);
        }

        public IActionResult OnPostEditActionAjax(Models.Action obj, List<string> listOfParticipants)
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            obj.JednostkaId = CurrentLoggedUnit.Value;
            var result = Models.Action.EditAction(obj, listOfParticipants);
            return new JsonResult(result);
        }

        public IActionResult OnGetDeleteAction(string ActionId)
        {
            Console.WriteLine("jojojojojojojojojojojoj");
            var result = Models.Action.RemoveAction(ActionId);
            return RedirectToPage("/Actions/ActionOverview", new { passAlert = result });
        }
    }
}
