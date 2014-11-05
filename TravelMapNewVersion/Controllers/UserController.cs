﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TravelMap.Models;
using nonintanon.Security;
using System.Threading.Tasks;

namespace TravelMap.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private Entities db = new Entities();

        // GET: UserProfile
        public ActionResult Index()
        {
            var userId = WebSecurity.CurrentUserId;
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(userId);
        }

        public JsonResult JIndex(Guid id)
        {
            // todo: check this configuration property in future
            // Property was set to enable Json serialization ignoring "unserializable" stuff
            db.Configuration.ProxyCreationEnabled = false;

            var userProfile = db.UserProfiles.Find(id);
            if (userProfile == null)
            {
                return new JsonResult();
            }

            var result = Json(userProfile, JsonRequestBehavior.AllowGet);
            return result;
            //(new { UserName = userProfile.UserName, Email = userProfile.Email }, JsonRequestBehavior.AllowGet);
        }

        public Guid GetCurrentUser()
        {
            return WebSecurity.CurrentUserId;
        }

        //
        public JsonResult SaveEmail(Guid id, string newEmail)
        {
            var userProfile = db.UserProfiles.Find(id);
            userProfile.Email = newEmail;
            var res = db.SaveChanges();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Save(Guid id, string firstname, string surname, string email, string phone)
        {
            var userProfile = db.UserProfiles.Find(id);
	        userProfile.FirstName = firstname;
            userProfile.Surname = surname;
            userProfile.Email = email;
            userProfile.Phone = phone;
            var res = db.SaveChanges();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveUserpic(object fd)
        {
            var userId = WebSecurity.CurrentUserId;
            var userProfile = db.UserProfiles.Find(userId);

            var photos = Request.Files;
            var photo = photos[0];
            var length = (int)photo.InputStream.Length;
            var bytePhoto = new byte[length];
            photo.InputStream.Read(bytePhoto, 0, length);

            userProfile.Photo = bytePhoto;
            var res = db.SaveChanges();
            return res == 1
                ? Json(bytePhoto)
                : Json("err");
        }

        // **********************************************************
        // ***** All stuff below is not used and autogenerated ******
        // **********************************************************
        // (except Dispose)

        // GET: UserProfiles/Details/5
		private ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = db.UserProfiles.Find(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            return View(userProfile);
        }

        // GET: UserProfiles/Edit/5
		private ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = db.UserProfiles.Find(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Followers, "UserId", "UserId", userProfile.UserId);
            return View(userProfile);
        }

        // POST: UserProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        private ActionResult Edit([Bind(Include = "UserId,UserName,Surname,BirthDate,Phone,Photo,Email")] UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Followers, "UserId", "UserId", userProfile.UserId);
            return View(userProfile);
        }

        // GET: UserProfiles/Delete/5
		private ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = db.UserProfiles.Find(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            return View(userProfile);
        }

        // POST: UserProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		private ActionResult DeleteConfirmed(Guid id)
        {
            UserProfile userProfile = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(userProfile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult GetUserProfile(Guid id)
        {
            try
            {
                var result = db.UserProfiles.First(profile => profile.UserId == id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonErrorResponse("can't find user by id"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetUserShortProfile(Guid id)
        {
            try
            {
                var result = db.UserProfiles.First(profile => profile.UserId == id);
                return Json(new { id = result.UserId, name = result.UserName, surname = result.Surname, photo = result.Photo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonErrorResponse("can't find user by id"), JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public JsonResult GetUserFollowers(Guid id)
        {
            try
            {
                var result = db.UserProfiles.First(profile => profile.UserId == id).Followers.ToArray();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonErrorResponse("can't find user's followers"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetUserPosts(Guid id)
        {
            try
            {
                var result = db.UserProfiles.First(profile => profile.UserId == id).Posts.ToArray();
                var serPosts = new List<dynamic>();
                foreach (var post in result)
                {
                    serPosts.Add(new Post
                    {
                        ParentId = post.ParentId,
                        Text = post.Text,
                        PostType = post.PostType,
                        UserId = post.UserId,
                        Time = post.Time,
                        PostFiles = post.PostFiles,
                        PostId = post.PostId,
                        TravelId = post.TravelId,
                        TypeId = post.TypeId
                    });
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonErrorResponse("can't find user's posts"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetUserVisitedCountries(Guid id)
        {
            var userTravels = db.Travels.Where(travel => travel.UserId == id);
            var result = userTravels.Select(userTravel => userTravel.Country).ToArray();
            var serializableResult = new List<dynamic>();
            foreach (var country in result)
            {
                serializableResult.Add(new
                {
                    id = country.CountryId,
                    name = country.Name,
                    title = country.Title
                });
            }
            return Json(serializableResult, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetUserTravels(Guid id, bool groupByCountry = false)
        {
            var result = db.Travels.Where(travel => travel.UserId == id).ToArray();
            IEnumerable<dynamic> jsonResult;
            if (!groupByCountry)
            {
                jsonResult = FormatTravelsWithoutGrouping(result);
            }
            else
            {
                jsonResult = FormatTravelsWithGrouping(result);
            }
            return Json(jsonResult.ToArray(), JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<dynamic> FormatTravelsWithoutGrouping(IEnumerable<Travel> travels)
        {
            var result = new List<dynamic>();
            foreach (var travel in travels)
            {
                result.Add(new
                {
                    travelId = travel.TravelId,
                    startDate = travel.StartDate,
                    endDate = travel.EndDate,
                    userId = travel.UserId,
                    country = travel.Country.Name,
                    countryId = travel.CountryId
                });
            }
            return result;
        }
        private IEnumerable<dynamic> FormatTravelsWithGrouping(IEnumerable<Travel> travels)
        {
            var grouped = FormatTravelsWithoutGrouping(travels).GroupBy(o => o.country);
            var result = new List<dynamic>();
            foreach (var group in grouped)
            {
                result.Add(new
                {
                    country = group.Key,
                    travels = group
                });
            }
            return result;
        }

        [HttpGet]
        public JsonResult GetTravelsForCountry(Guid countryId, Guid userId)
        {
            var travels = db.Travels.Where(travel => travel.CountryId == countryId && travel.UserId == userId);
            var serializableTravels = new List<dynamic>();
            foreach (var travel in travels)
            {
                serializableTravels.Add(new
                {
                    travelId = travel.TravelId,
                    startDate = travel.StartDate,
                    endDate = travel.EndDate,
                    userId = travel.UserId,
                    country = travel.Country.Name
                });
            }
            return Json(serializableTravels, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void SetFollower(Guid id)
        {
            var userId = WebSecurity.CurrentUserId;
            db.Followers.Add(new Follower { UserId = userId, FollowerId = id, UserFollowerId = Guid.NewGuid() });
            db.SaveChanges();
        }

        [HttpGet]
        public JsonResult GetFollowersForUser(Nullable<Guid> id)
        {
            try
            {
                List<Follower> followers;
                //var followers
                if (id == null)
                    followers = db.Followers.Where(u => u.UserId == WebSecurity.CurrentUserId).ToList();
                followers = db.Followers.Where(u => u.UserId == id).ToList();
                var jsonResult = new List<dynamic>();
                foreach (var follower in followers)
                {
                    jsonResult.Add(new Follower
                    {
                        UserFollowerId = follower.UserFollowerId,
                        UserId = follower.UserId,
                        FollowerId = follower.FollowerId
                    });
                }
                return Json(jsonResult.ToArray(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonErrorResponse("can't find user's followers"), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Travels()
        {
            return View(WebSecurity.CurrentUserId);
        }



        [HttpGet]
        public ActionResult PeopleSearch()
        {
            return View();
        }

        //public ActionResult PeopleSearch(string search)
        //{

        //    //List<UserProfile> searchedUsers;
        //    //if (!search.Contains(" "))
        //    var searchedUsers = db.UserProfiles.Where(u => u.Surname == search || u.UserName == search).ToList();
        //    return searchedUsers;
        //}

        [HttpPost]
        public JsonResult PeopleSearched(string searchUser)
        {
            List<dynamic> peopleDynamic = new List<dynamic>();
            if (searchUser.Contains(' '))
            {
                string[] parts = searchUser.Split(' ');
				// todo: убрать этот быдлокод и написать что-то нармальное :)
	            var part1 = parts[0];
	            var part2 = parts[1];
                var people = db.UserProfiles.Where(u => u.UserName.Contains(part1) &&
                    u.Surname.Contains(part2)).ToArray();
                foreach (var man in people)
                {
                    peopleDynamic.Add(new
                    {
                        Photo = man.Photo,
                        UserName = man.UserName,
                        Surname = man.UserName,
                        BirthDate = man.BirthDate != null ? man.BirthDate.Value.Millisecond : 0,
                        Email = man.Email
                    });
                }
                return Json(peopleDynamic.ToArray(), JsonRequestBehavior.AllowGet);
            }
            var barada = db.UserProfiles.Where(u => u.Surname.Contains(searchUser) ||
                u.UserName.Contains(searchUser)).ToList();
            //var xxx = barada.ToList();
            foreach (var man in barada)
            {
                peopleDynamic.Add(new
                {
                    Photo = Convert.ToBase64String(man.Photo),
                    UserName = man.UserName,
                    Surname = man.Surname,
					BirthDate = man.BirthDate != null ? man.BirthDate.Value.Millisecond : 0,
                    Email = man.Email
                });
            }
            var jsonResult = Json(peopleDynamic.ToArray(), JsonRequestBehavior.AllowGet); ;
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        //[HttpPost]
        //public async Task<PartialViewResult> PeopleSearched(string searchUser)
        //{
        //    if (searchUser.Contains(' '))
        //    {
        //        string[] parts = searchUser.Split(' ');
        //        var people = await Task.Run(() => (db.UserProfiles.Where(u => u.UserName.Contains(parts[0]) &&
        //            u.Surname.Contains(parts[1]))));
        //        return PartialView(people.ToList());
        //    }
        //    var barada = await Task.Run(() => (db.UserProfiles.Where(u => u.Surname.Contains(searchUser) ||
        //        u.UserName.Contains(searchUser))));
        //    var xxx = barada.ToList();
        //    return PartialView(xxx);
        //}





        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
