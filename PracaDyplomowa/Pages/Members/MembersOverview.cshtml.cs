using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PracaDyplomowa.Models;

namespace PracaDyplomowa.Pages.Members
{
    [Authorize (Roles = Models.User.UserType.Admin)]
    public class MembersOverviewModel : PageModel
    {
      //  [BindProperty(SupportsGet =true)]
      //  public string Filter { get; set; }
        
        public Claim CurrentLoggedUnit { get; set; }

        public List<User> ListOfUsers { get; set; } = new List<User>();

        //communication with js
        [BindProperty(SupportsGet = true)]
        public string passAlert { get; set; }

        //Display Options  // Binding by name
        [BindProperty]
        public string MemberCustomRadioFilter { get; set; }
        [BindProperty]
        public string MemberCustomRadioSort { get; set; }

        public void OnGet()
        {
            //CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            //ListOfUsers = new List<User>(Models.User.GetListOfAllUser(CurrentLoggedUnit.Value).OrderBy(x => x.Imie).ThenBy(x => x.Nazwisko));

         
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");

            string cookieValueFilter = Request.Cookies["FilterMemberOverview"];
            string cookieValueSort = Request.Cookies["SortMemberOverview"];


            if (cookieValueFilter == "management")
            {
                ListOfUsers = Models.User.GetListOfAllAdministration(CurrentLoggedUnit.Value);
               // if (ListOfUsers == null) return;
               // ListOfUsers = ListOfUsers.OrderBy(x => x.Imie).ThenBy(x => x.Nazwisko)).ToList();
            }

            else//(ListOfUsers == "all")
            {
                ListOfUsers = Models.User.GetListOfAllUser(CurrentLoggedUnit.Value);
                //if (ListOfUsers.Count == 0) return;
                //ListOfUsers = ListOfUsers.OrderBy(x => x.DataUtworzenia).ToList();
            }


            if (cookieValueSort == "name")
                ListOfUsers = ListOfUsers.OrderBy(x => x.Imie).ToList();
            else if (cookieValueSort == "function")
                ListOfUsers = ListOfUsers.OrderBy(x => x.Funkcja).ToList();
            else if (cookieValueSort == "date")
                try
                {
                    ListOfUsers = ListOfUsers.OrderByDescending(x => string.IsNullOrEmpty(x.DataWstapienia) ?
                    new DateTime() : Convert.ToDateTime(x.DataWstapienia)).ToList(); //new DateTime() as empty date 
                }
                catch (Exception ex)
                {
                    Console.WriteLine("MembersOverviewModel Error[Cannot Convert ToDateTime]: "
                        + ex.Message);
                }
           
            else //(cookieValueSort == "lastName")
                ListOfUsers = ListOfUsers.OrderBy(x => x.Nazwisko).ToList();

        }

        public void OnPostSaveDisplayOptions()
        {
            
            CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");

            if (MemberCustomRadioFilter == "management")
            {
                Response.Cookies.Append("FilterMemberOverview", "management");
                ListOfUsers = Models.User.GetListOfAllAdministration(CurrentLoggedUnit.Value);
            }

            else if (MemberCustomRadioFilter == "all")
            {
                Response.Cookies.Append("FilterMemberOverview", "all");
                ListOfUsers = Models.User.GetListOfAllUser(CurrentLoggedUnit.Value);
            }


            if (ListOfUsers.Count == 0 || ListOfUsers == null)
                return;

            if (MemberCustomRadioSort == "name")
            {
                Response.Cookies.Append("SortMemberOverview", "name");
                ListOfUsers = ListOfUsers.OrderBy(x => x.Imie).ToList();
            }

            else if (MemberCustomRadioSort == "lastName")
            {
                Response.Cookies.Append("SortMemberOverview", "lastName");
                ListOfUsers = ListOfUsers.OrderBy(x => x.Nazwisko).ToList();
            }

            else if (MemberCustomRadioSort == "function")
            {
                Response.Cookies.Append("SortMemberOverview", "function");
                ListOfUsers = ListOfUsers.OrderBy(x => x.Funkcja).ToList();
            }

            else if (MemberCustomRadioSort == "date")
            {
                Response.Cookies.Append("SortMemberOverview", "date");
                //ListOfUsers = ListOfUsers.Where(w => w.DataWstapienia!="").ToList();
                try
                {
                    ListOfUsers = ListOfUsers.OrderByDescending(x => string.IsNullOrEmpty(x.DataWstapienia) ?
                     new DateTime() : Convert.ToDateTime(x.DataWstapienia)).ToList(); //new DateTime() as empty date 
                }
                catch(Exception ex)
                {
                    Console.WriteLine("MembersOverviewModel Error[Cannot Convert ToDateTime]: "
                        + ex.Message);
                }
            }
            
        }


        public void OnPostSearchUser(string searchData)
        {
            ListOfUsers = Models.User.SearchUser(CurrentLoggedUnit.Value, "searchData");
        }

    }
}
