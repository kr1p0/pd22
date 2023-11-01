using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace PracaDyplomowa.Pages.Members
{
    [Authorize(Roles = Models.User.UserType.Admin)]
    public class CoursesModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string selectedUserId { get; set; }

        public List<Models.User> ListOfCourses { get; set; } = new List<Models.User>();

        public void OnGet()
        {
            var CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit").Value;
            ListOfCourses = Models.User.GetCourses(CurrentLoggedUnit, selectedUserId);
        }

        public IActionResult OnPostSaveCoursesAjax(List<Models.User> obj)
        {
            var CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit").Value;
            Models.User.RemoveCoursesByMemberId(obj[0].UzytkownikId);
            var result = Models.User.InsertCours(obj, CurrentLoggedUnit);
            return new JsonResult(result);
        }

        public void OnPostRemoveCourseAjax(string idToRemove)
        {
            Models.User.RemoveCoursesByCourseId(idToRemove);
        }
    }
}
