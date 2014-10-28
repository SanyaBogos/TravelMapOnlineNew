﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TravelMap.Models;
using nonintanon.Security;

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

        //
        public JsonResult SaveEmail(Guid id, string newEmail)
        {
            var userProfile = db.UserProfiles.Find(id);
            userProfile.Email = newEmail;
            var res = db.SaveChanges();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        // **********************************************************
        // ***** All stuff below is not used and autogenerated ******
        // **********************************************************
        // (except Dispose)

        // GET: UserProfiles/Details/5
        public ActionResult Details(Guid? id)
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
        public ActionResult Edit(Guid? id)
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
        public ActionResult Edit([Bind(Include = "UserId,UserName,Surname,BirthDate,Phone,Photo,Email")] UserProfile userProfile)
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
        public ActionResult Delete(Guid? id)
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
        public ActionResult DeleteConfirmed(Guid id)
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
            try
            {
                var userTravels = db.Travels.Where(travel => travel.UserId == id);
                var result = userTravels.Select(userTravel => userTravel.Country).ToArray();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonErrorResponse("can't find user's countries"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetUserTravels(Guid id)
        {
            try
            {
                var result = db.Travels.Where(travel => travel.UserId == id).ToArray();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonErrorResponse("can't find user's travels"), JsonRequestBehavior.AllowGet);
            }
        }


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
