using nonintanon.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TravelMap.Models;
using Microsoft.AspNet.Identity;

namespace TravelMap.Controllers
{
    [Authorize]
    public class MapController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index()
        {
            List<string> userCountries = new List<string>();
            var userId = WebSecurity.CurrentUserId;
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userMap = db.Travels.Where(n => n.UserId == userId);
            if (userMap == null)
            {
                return HttpNotFound();
            }
            foreach (var item in userMap)
            {
                userCountries.Add(db.Countries.Find(item.CountryId).Title);
            }
            return View(userCountries);
        }

        [HttpPost]
        public void SetTravel(string country, string start, string end)
        {
            var countryId = db.Countries.First(c => c.Name == country).CountryId;
            var userId = WebSecurity.CurrentUserId;

            Travel travel = new Travel
            {
                CountryId = countryId,
                UserId = userId,
                StartDate = Convert.ToDateTime(start),
                EndDate = Convert.ToDateTime(end),
                TravelId = Guid.NewGuid()                
            };
            db.Travels.Add(travel);
            db.SaveChanges();
        }
    }

}
