using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PracaDyplomowa.Pages.Announcement
{
    [Authorize(Roles = Models.User.UserType.Admin)]
    public class AddAnnouncementModel : PageModel
    {
        private readonly IWebHostEnvironment _rootEnv;

        public AddAnnouncementModel(IWebHostEnvironment env)
        {
            _rootEnv = env; //for root path
        }

        public List<Models.User> ListOfUsers { get; set; } = new List<Models.User>();

        public void OnGet()
        {
            var CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            ListOfUsers = Models.User.GetListOfAllUser(CurrentLoggedUnit.Value);
           
        }

        private string checkDirectory(string originalPath)
        {
            int i = 1;
            string fileExtension = Path.GetExtension(originalPath);
          
            string fullPath = Path.GetFullPath(originalPath);
            var fullPathWithNoExtension = fullPath.Substring(0, fullPath.LastIndexOf(fileExtension)); 
            
           
            
            string addToName = "";
            FileInfo dir0 = new FileInfo(originalPath);

            while (dir0.Exists)
            {
                i++;
                addToName = "(" + i + ")";
                string temp = fullPathWithNoExtension + addToName + fileExtension;
                dir0 = new FileInfo(temp);
            }
            var result = fullPathWithNoExtension + addToName + fileExtension;
            return result;
        }

        public IActionResult OnPostAddAnnouncement(Models.Announcement AnnouncementObj, List<string> odbiorcy , IFormFile uploadedFile=null)
        {
            var fileName = "";
            if (uploadedFile != null)
                fileName = uploadedFile.Name;
         
            if (uploadedFile != null)
            {
                string fileExtension = Path.GetExtension(uploadedFile.FileName);
                fileName = Path.GetFileNameWithoutExtension(uploadedFile.FileName); 
                string webRootPath = _rootEnv.WebRootPath;
                var FilePath = Path.Combine(webRootPath, "uploads/doc/announcement");
                if (!Directory.Exists(FilePath))
                    Directory.CreateDirectory(FilePath);
                //fileName = Guid.NewGuid() + fileExtension; //unique file name
                var dateT = "-" + DateTime.Now.ToString("dd-MM-yyyy");
                fileName += dateT+ fileExtension;
                var filePath = Path.Combine(FilePath, fileName.ToString());
                filePath = checkDirectory(filePath);
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    uploadedFile.CopyTo(fs);
                }
            }

            if (odbiorcy.Count>0)
                Models.Email.sendMail(odbiorcy, AnnouncementObj.Tytul, AnnouncementObj.Tresc , uploadedFile);


            var CurrentLoggedUnit = User.FindFirst(x => x.Type == "Unit");
            var lastName = User.FindFirst(x => x.Type == "LastName");
            AnnouncementObj.JednostkaId = CurrentLoggedUnit.Value;
            AnnouncementObj.Autor = User.Identity.Name +" "+ lastName.Value;
            AnnouncementObj.DokumentNazwa = fileName;

            var result = Models.Announcement.InsertAnnouncement(AnnouncementObj);
            return new JsonResult(result);
        }

        public IActionResult OnPostMyUploader(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                var fileName = "";
                
                string fileExtension = Path.GetExtension(uploadedFile.FileName);
                string webRootPath = _rootEnv.WebRootPath;
                var FilePath = Path.Combine(webRootPath, "uploads/img/equipment");
                if (!Directory.Exists(FilePath))
                    Directory.CreateDirectory(FilePath);
                fileName = Guid.NewGuid() + fileExtension; //unique file name
                var filePath = Path.Combine(FilePath, fileName.ToString());
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    uploadedFile.CopyTo(fs);
                }
             
                return new ObjectResult(new { status = "success" });

            }
            return new ObjectResult(new { status = "fail" });

        }



    }
}
