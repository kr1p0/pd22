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
    [Authorize]
    public class ViewActionModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string selectedActionId { get; set; }

        public Claim CurrentLoggedUnit { get; set; }

        public List<Models.User> ListOfUsers { get; set; } = new List<Models.User>();

        public List<string> ListOfActionParticipants = new List<string>();

        public Models.Action ActionObj { get; set; }

        public void OnGet()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            ListOfUsers = Models.User.GetListOfAllUser(CurrentLoggedUnit.Value);
            ListOfActionParticipants = Models.Action.GetParticipants(selectedActionId);
            ActionObj = new Models.Action();
            ActionObj.GetAction(selectedActionId);
        }
    }
}
