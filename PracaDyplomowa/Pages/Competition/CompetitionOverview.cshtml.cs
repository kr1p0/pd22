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
    public class CompetitionOverviewModel : PageModel
    {
        public Claim CurrentLoggedUnit { get; set; }

        public List<Models.Competition> ListOfCompetitions { get; set; } = new List<Models.Competition>();

        [BindProperty(SupportsGet = true)]
        public string passAlert { get; set; }

        [BindProperty]
        public string CompetitionCustomRadioFilter { get; set; }

        [BindProperty]
        public string CompetitionCustomRadioSort { get; set; }

        public void OnGet()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            ListOfCompetitions= Models.Competition.GetAllCompetitionData(CurrentLoggedUnit.Value);
            try
            {
                ListOfCompetitions = ListOfCompetitions.OrderByDescending(x => Convert.ToDateTime(x.Data)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("CompetitionOverviewModel Error[Cannot Convert ToDateTime]: "
                    + ex.Message);
            }




            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");

            string cookieValueFilter = Request.Cookies["FilterCompetitionOverview"];
            string cookieValueSort = Request.Cookies["SortCompetitionOverview"];


            if (cookieValueFilter == "miejsce1")
            {
                ListOfCompetitions = Models.Competition.GetAllCompetitionData(CurrentLoggedUnit.Value);
                ListOfCompetitions = ListOfCompetitions.FindAll(x => x.ZajeteMiejsce == "1");
            }

            else if (cookieValueFilter == "miejsce2")
            {
                ListOfCompetitions = Models.Competition.GetAllCompetitionData(CurrentLoggedUnit.Value);
                ListOfCompetitions = ListOfCompetitions.FindAll(x => x.ZajeteMiejsce == "2");
            }

            else if (cookieValueFilter == "miejsce3")
            {
                ListOfCompetitions = Models.Competition.GetAllCompetitionData(CurrentLoggedUnit.Value);
                ListOfCompetitions = ListOfCompetitions.FindAll(x => x.ZajeteMiejsce == "3");
            }

            else//(cookieValueFilter == "all")
            {
                ListOfCompetitions = ListOfCompetitions.OrderByDescending(x => Convert.ToDateTime(x.Data)).ToList();
            }


            if (ListOfCompetitions == null || ListOfCompetitions.Count == 0) return;


            if (cookieValueSort == "ZajeteMiejsce")
                ListOfCompetitions = ListOfCompetitions.OrderBy(x => x.ZajeteMiejsce).ToList();
            else //(cookieValueSort == "Data")
            {
                try
                {
                    ListOfCompetitions = ListOfCompetitions.OrderByDescending(x => Convert.ToDateTime(x.Data)).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("CompetitionOverviewModel Error[Cannot Convert ToDateTime]: "
                        + ex.Message);
                }
            }
        }




        public void OnPostSaveDisplayOptions()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            ListOfCompetitions = Models.Competition.GetAllCompetitionData(CurrentLoggedUnit.Value).
                OrderByDescending(x => Convert.ToDateTime(x.Data)).ToList();


            if (ListOfCompetitions.Count == 0 || ListOfCompetitions == null)
                return;
            
            if (CompetitionCustomRadioFilter == "miejsce1")
            {
                Response.Cookies.Append("FilterCompetitionOverview", "miejsce1");
                ListOfCompetitions = ListOfCompetitions.FindAll(x => x.ZajeteMiejsce == "1");
            }

            else if (CompetitionCustomRadioFilter == "miejsce2")
            {
                Response.Cookies.Append("FilterCompetitionOverview", "miejsce2");
                ListOfCompetitions = ListOfCompetitions.FindAll(x => x.ZajeteMiejsce == "2"); 
            }

            else if (CompetitionCustomRadioFilter == "miejsce3")
            {
                Response.Cookies.Append("FilterCompetitionOverview", "miejsce3");
                ListOfCompetitions = ListOfCompetitions.FindAll(x => x.ZajeteMiejsce == "3"); 
            }

            else if (CompetitionCustomRadioFilter == "all")
            {
                Response.Cookies.Append("FilterCompetitionOverview", "all");
                //ListOfCompetitions = Models.Competition.GetAllCompetitionData(CurrentLoggedUnit.Value);
            }


           

            if (CompetitionCustomRadioSort == "ZajeteMiejsce")
            {
                Response.Cookies.Append("SortCompetitionOverview", "ZajeteMiejsce");
                ListOfCompetitions = ListOfCompetitions.OrderBy(x => x.ZajeteMiejsce).ToList();
            }

            else// if (CompetitionCustomRadioSort == "Data")
            {
                Response.Cookies.Append("SortCompetitionOverview", "Data");
                try
                {
                    ListOfCompetitions = ListOfCompetitions.OrderByDescending(x => Convert.ToDateTime(x.Data)).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("CompetitionOverviewModel " +
                        "Error[Cannot Convert ToDateTime]: "
                        + ex.Message);
                }
            }
        }
    }
}
