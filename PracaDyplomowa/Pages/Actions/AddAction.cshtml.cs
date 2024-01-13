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
    public class AddActionModel : PageModel
    {
        public Claim CurrentLoggedUnit { get; set; }

        public List<Models.User> ListOfUsers { get; set; } = new List<Models.User>();

        public void OnGet()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            ListOfUsers = Models.User.GetListOfAllUser(CurrentLoggedUnit.Value);



            /*
            var x = new Models.Action()
            {
                JednostkaId = "1",
                TypZdarzenia = "testowe",
                CzasZdarzenia = DateTime.Now.ToString(),
                CzasZakonczenia = DateTime.Now.ToString(),w
                IloscUczestnikow = "3"
            };

            Console.WriteLine(DateTime.Now.ToString());
            Models.Action.InsertAction(x);



            var x = new List<Models.Action.Participants>()
            {
                new Models.Action.Participants{Akcja_id = "1" , Uzytkownik_id = "1" },
                 new Models.Action.Participants{Akcja_id = "1" , Uzytkownik_id = "1" }
            };

            Models.Action.InsertParticipants(x);
            */
        }

        public IActionResult OnPostAddActionAjax(Models.Action obj , List<string> listOfParticipants)
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            obj.JednostkaId = CurrentLoggedUnit.Value;
            var result = Models.Action.InsertAction(obj, listOfParticipants);
            return new JsonResult(result);
        }
    }
}
