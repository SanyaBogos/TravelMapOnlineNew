using nonintanon.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TravelMap.Models;

namespace TravelMap.Controllers
{
    [Authorize]
    public class MapController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index()
        {
            var userCountries = new List<string>();
            var userId = WebSecurity.CurrentUserId;
            var userMap = db.Travels.Where(n => n.UserId == userId);
            foreach (var item in userMap)
            {
                userCountries.Add(db.Countries.Find(item.CountryId).Title);
            }
            return View(userCountries);
        }



        [HttpPost]
        public Guid SetTravel(string country, string start, string end, string color)
        {
            var countryId = db.Countries.First(c => c.Name == country).CountryId;
            var userId = WebSecurity.CurrentUserId;

            var travel = new Travel
            {
                CountryId = countryId,
                UserId = userId,
                //StartDate = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(long.Parse(start)),
                //EndDate = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(long.Parse(end)),
                TravelId = Guid.NewGuid(),
                CountryColor = color!=null ? color : ""
            };
            if (start != null) travel.StartDate = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(long.Parse(start));
            if (end != null) travel.EndDate = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(long.Parse(end));
            db.Travels.Add(travel);
            db.SaveChanges();
            return travel.TravelId;
        }

    }

}
