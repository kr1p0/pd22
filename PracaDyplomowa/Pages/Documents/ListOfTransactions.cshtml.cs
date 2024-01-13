using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PracaDyplomowa.Pages.Documents
{
    [Authorize(Roles = Models.User.UserType.Admin)]
    public class ListOfTransactionsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
