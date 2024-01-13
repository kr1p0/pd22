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
    public class EditCompetitionModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string selectedCompetitionId { get; set; }

        public Claim CurrentLoggedUnit { get; set; }

        public List<Models.User> ListOfUsers { get; set; } = new List<Models.User>();

        public List<Models.Competition.CompetitionParticipants> ListOfCompetitionParticipants = 
            new List<Models.Competition.CompetitionParticipants>();

        public List<string> ListOfParticipantsId = new List<string>();

        public Models.Competition CompetitionObj { get; set; } = new Models.Competition();


        public void OnGet()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            ListOfUsers = Models.User.GetListOfAllUser(CurrentLoggedUnit.Value);
            (ListOfCompetitionParticipants , ListOfParticipantsId) = 
                Models.Competition.GetCompetitionParticipants(selectedCompetitionId);


            CompetitionObj.GetCompetitionData(selectedCompetitionId);
        }

        public IActionResult OnPostEditCompetitionAjax(Models.Competition obj, 
            List<Models.Competition.CompetitionParticipants> listOfParticipants)
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            obj.JednostkaId = CurrentLoggedUnit.Value;
            var result = Models.Competition.EditCompetition(obj, listOfParticipants);
            return new JsonResult(result);
        }

        public IActionResult OnGetDeleteCompetition(string CompetitionId)
        {
            var result = Models.Competition.RemoveCompetition(CompetitionId);
            return RedirectToPage("/Competition/CompetitionOverview", new { passAlert = result });
        }
    }
}
