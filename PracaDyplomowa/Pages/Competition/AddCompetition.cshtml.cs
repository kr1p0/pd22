using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PracaDyplomowa.Pages.Competition
{
    [Authorize(Roles = Models.User.UserType.Admin)]
    public class AddCompetitionModel : PageModel
    {
        public Claim CurrentLoggedUnit { get; set; }

        public List<Models.User> ListOfUsers { get; set; } = new List<Models.User>();

        public void OnGet()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            ListOfUsers = Models.User.GetListOfAllUser(CurrentLoggedUnit.Value);
        }


        public IActionResult OnPostAddCompetitionAjax(Models.Competition obj , 
            List<Models.Competition.CompetitionParticipants> listOfParticipants)
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            obj.JednostkaId = CurrentLoggedUnit.Value;
            Console.WriteLine(obj.Data);
            Console.WriteLine(obj.IloscUczestnikow);
            Console.WriteLine(obj.Kategoria);
            Console.WriteLine(obj.Szczebel);
            Console.WriteLine(obj.ZajeteMiejsce);
            var result = Models.Competition.InsertCompetition(obj, listOfParticipants);
            return new JsonResult(result);
        }
    }
}
