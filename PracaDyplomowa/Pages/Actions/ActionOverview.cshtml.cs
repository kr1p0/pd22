using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace PracaDyplomowa.Pages.Actions
{
    [Authorize(Roles = Models.User.UserType.Admin)]
    public class ActionOverviewModel : PageModel
    {


        private IHubContext<Hubs.NotificationHub> _hubContext;

        public ActionOverviewModel(IHubContext<Hubs.NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }


        public Claim CurrentLoggedUnit { get; set; }

        public List<Models.Action> ListOfActions { get; set; } = new List<Models.Action>();

        [BindProperty(SupportsGet = true)]
        public string passAlert { get; set; }

        [BindProperty(SupportsGet = true)]
        public int selectedTablePage { get; set; }

        [BindProperty]
        public string ActionCustomRadioFilter { get; set; }
        [BindProperty]
        public string ActionCustomRadioSort { get; set; }
        [BindProperty]
        public string searchActionOverviewInput { get; set; }
        [BindProperty(SupportsGet = true)]
        public string selectedUserId { get; set; } = null;
        public List<string> ListOfActionIdAssignedToMember { get; set; } = new List<string>();


        public int tablePagesCount { get; set; }

        [BindProperty]
        public  int rowsOnPage { get;  } = 8;

        public async void OnGet(int selectedTablePage=1)
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");


            string cookieValueFilter = Request.Cookies["FilterActionOverview"];
            string cookieValueSort = Request.Cookies["SortActionOverview"];


            if (!string.IsNullOrEmpty(selectedUserId))
            {
                var actionList = Models.Action.GetActionsbyMember(selectedUserId);
                foreach(var action in actionList)
                    ListOfActionIdAssignedToMember.Add(action.Id);
            }




            if (cookieValueFilter == "pozar")
            {
                ListOfActions = Models.Action.GetActionsbyType(CurrentLoggedUnit.Value, Models.ActionType.Pozar);
            }

            else if (cookieValueFilter == "zdarzenieDrogowe")
            {
                ListOfActions = Models.Action.GetActionsbyType(CurrentLoggedUnit.Value , Models.ActionType.ZdarzenieDrogowe);
            }

            else if (cookieValueFilter == "miejsceZagrozenia")
            {
                ListOfActions = Models.Action.GetActionsbyType(CurrentLoggedUnit.Value, Models.ActionType.MiejsceZagrozenia);
            }

            else if (cookieValueFilter == "falszywyAlarm")
            {
                ListOfActions = Models.Action.GetActionsbyType(CurrentLoggedUnit.Value, Models.ActionType.FalszywyAlarm);
            }

            else//(cookieValueFilter == "all")
            {
                ListOfActions = Models.Action.GetAllActions(CurrentLoggedUnit.Value);
            }


            if (ListOfActions == null || ListOfActions.Count == 0) return;


            if (cookieValueSort == "typZdarzenia")
                ListOfActions = ListOfActions.OrderBy(x => x.TypZdarzenia).ToList();
            else //(cookieValueSort == "czasZdarzenia")
            { 
                try
                {
                    ListOfActions = ListOfActions.OrderByDescending(x => Convert.ToDateTime(x.CzasZdarzenia)).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ActionOverviewModel Error[Cannot Convert ToDateTime]: "
                        + ex.Message);
                }
            }

            if (string.IsNullOrEmpty(selectedUserId))
            {
                tablePagesCount = ListOfActions.Count % rowsOnPage == 0 ? ListOfActions.Count / rowsOnPage : (ListOfActions.Count / rowsOnPage) + 1;
                ListOfActions = prepareTablePage(ListOfActions, selectedTablePage, rowsOnPage);
            }
          
        }


        public void OnPostSaveDisplayOptions(int selectedTablePage=1)
        {
          
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");

            if (!string.IsNullOrEmpty(selectedUserId))
            {
                var actionList = Models.Action.GetActionsbyMember(selectedUserId);
                foreach (var action in actionList)
                    ListOfActionIdAssignedToMember.Add(action.Id);
            }

            if (ActionCustomRadioFilter == "pozar")
            {
                Response.Cookies.Append("FilterActionOverview", "pozar");
                ListOfActions = Models.Action.GetActionsbyType(CurrentLoggedUnit.Value, Models.ActionType.Pozar);
            }

            else if (ActionCustomRadioFilter == "zdarzenieDrogowe")
            {
                Response.Cookies.Append("FilterActionOverview", "zdarzenieDrogowe");
                ListOfActions = Models.Action.GetActionsbyType(CurrentLoggedUnit.Value, Models.ActionType.ZdarzenieDrogowe);
            }

            else if (ActionCustomRadioFilter == "miejsceZagrozenia")
            {
                Response.Cookies.Append("FilterActionOverview", "miejsceZagrozenia");
                ListOfActions = Models.Action.GetActionsbyType(CurrentLoggedUnit.Value, Models.ActionType.MiejsceZagrozenia);
            }

            else if (ActionCustomRadioFilter == "falszywyAlarm")
            {
                Response.Cookies.Append("FilterActionOverview", "falszywyAlarm");
                ListOfActions = Models.Action.GetActionsbyType(CurrentLoggedUnit.Value, Models.ActionType.FalszywyAlarm);
            }

            else if (ActionCustomRadioFilter == "all")
            {
                Response.Cookies.Append("FilterActionOverview", "all");
                ListOfActions = Models.Action.GetAllActions(CurrentLoggedUnit.Value);
            }


            if (ListOfActions.Count == 0 || ListOfActions == null)
                return;

            if (ActionCustomRadioSort == "typZdarzenia")
            {
                Response.Cookies.Append("SortActionOverview", "typZdarzenia");
                ListOfActions = ListOfActions.OrderBy(x => x.TypZdarzenia).ToList();
            }

            else// if (ActionCustomRadioSort == "czasZdarzenia")
            {
                Response.Cookies.Append("SortActionOverview", "czasZdarzenia");
                try
                {
                    
                    ListOfActions = ListOfActions.OrderByDescending(x => Convert.ToDateTime(x.CzasZdarzenia)).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ActionOverviewModel Error[Cannot Convert ToDateTime]: "
                        + ex.Message);
                }
            }
            if (string.IsNullOrEmpty(selectedUserId))
            {
                tablePagesCount = ListOfActions.Count % rowsOnPage == 0 ? ListOfActions.Count / rowsOnPage : (ListOfActions.Count / rowsOnPage) + 1;
                ListOfActions = prepareTablePage(ListOfActions, selectedTablePage, rowsOnPage);

            }

        }

       public void OnPostSearchSql()
        {
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            ListOfActions = Models.Action.SearchActions(CurrentLoggedUnit.Value, searchActionOverviewInput.ToLower());

            tablePagesCount = ListOfActions.Count % rowsOnPage == 0 ? ListOfActions.Count / rowsOnPage : (ListOfActions.Count / rowsOnPage) + 1;
            ListOfActions = prepareTablePage(ListOfActions, selectedTablePage, rowsOnPage);

        }

        public void OnPostAssignActionsAjax(List<string> listOfActionsId, string UzytkownikId)
        {
            Models.Action.AssignActionsToMember(UzytkownikId, listOfActionsId);
        }

        public List<Models.Action> prepareTablePage(List<Models.Action> ListOfActions, int currentPage , int rowsOnPage)
        {
            return ListOfActions.Skip((currentPage - 1)* rowsOnPage).Take(rowsOnPage).ToList();
        }
    }
}
