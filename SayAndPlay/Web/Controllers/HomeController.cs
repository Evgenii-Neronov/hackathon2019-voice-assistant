using System;
using System.Web;
using System.Web.Mvc;
using Lib.Models.History;
using Lib.Models.Settings;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Assistent()
        {
            return View();
        }

        public ActionResult Settings()
        {
            var userId = this.GetUserId();

            var userSettings = UserSettings.Load(this.GetUserId());

            var settingsModel = new SettingsModel()
            {
                Recognizer = userSettings.Recognizer,
                Synthesizer = userSettings.Synthesizer
            };

            return View(settingsModel);
        }

        public ActionResult Log()
        {
            var userId = this.GetUserId();

            var userHistory = UserHistory.Load(userId);
            
            return View(userHistory);
        }

        public ActionResult Help()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Settings(SettingsModel settings)
        {
            var userId = this.GetUserId();

            UserSettings.Save(userId, new UserSettings()
            {
                Recognizer = settings.Recognizer,
                Synthesizer = settings.Synthesizer
            });
            
            return View(settings);
        }

        private Guid GetUserId()
        {
            const string cookieKey = "Hackathon2019_PlayAndSay";

            var cookie = Request.Cookies[cookieKey];

            if (cookie == null)
            {
                cookie = new HttpCookie(cookieKey);

                cookie.Values["UserId"] = Guid.NewGuid().ToString();
            }

            cookie.Expires = DateTime.UtcNow.AddDays(30);
            Response.Cookies.Add(cookie);

            return Guid.Parse(cookie.Values["UserId"]);
        }

        [HttpPost]
        public ActionResult ClearLog()
        {
            var userId = this.GetUserId();

            UserHistory.Clear(userId);

            return RedirectToAction("Log");
        }
    }
}