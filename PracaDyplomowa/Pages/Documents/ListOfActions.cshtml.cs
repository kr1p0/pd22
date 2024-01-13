using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace PracaDyplomowa.Pages.Documents
{
    [Authorize(Roles = Models.User.UserType.Admin)]
    public class ListOfActionsModel : PageModel
    {
        public List<Models.User> ListOfUsers { get; set; } = new List<Models.User>();
        public void OnGet()
        {
            var CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            ListOfUsers = Models.User.GetListOfAllUser(CurrentLoggedUnit.Value);
        }
    }
}
