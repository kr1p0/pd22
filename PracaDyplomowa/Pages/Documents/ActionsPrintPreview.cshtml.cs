using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PracaDyplomowa.Pages.Documents
{
    public class ActionsPrintPreviewModel : PageModel
    {
        public List<Models.Action> ListOfActions { get; set; } = new List<Models.Action>();
        public List<Models.Action> CustomListOfActions { get; set; } = new List<Models.Action>();
        public List<Models.User> ListOfUsers { get; set; } = new List<Models.User>();

        [BindProperty (SupportsGet =true)]
        public string Title { get; set; }

        [BindProperty(SupportsGet = true)]
        public string PreparedDateFrom { get; set; }

        [BindProperty(SupportsGet = true)]
        public string PreparedDateTo { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedUserId { get; set; }

        public void OnGet()
        {
            var CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            var minDate = string.IsNullOrEmpty(PreparedDateFrom)? "1800-01-01" : PreparedDateFrom;
            var maxDate = string.IsNullOrEmpty(PreparedDateTo) ? DateTime.Now.ToString("yyyy-MM-dd") : PreparedDateTo;
            ListOfActions = Models.Action.GetActionsByDate(CurrentLoggedUnit.Value, minDate, maxDate + " 23:59", true);
            ListOfUsers = Models.User.GetListOfAllUser(CurrentLoggedUnit.Value);

            if(!string.IsNullOrEmpty(SelectedUserId))
                ListOfActions = ListOfActions.FindAll((x) => x.ListaIdUczestnikow.Contains(SelectedUserId));
            ListOfActions = ListOfActions.OrderByDescending(x => Convert.ToDateTime(x.CzasZakonczenia)).ToList();
        }
    }
}
