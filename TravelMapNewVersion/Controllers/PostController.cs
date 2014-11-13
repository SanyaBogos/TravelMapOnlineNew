using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using nonintanon.Security;
using TravelMap.Models;

namespace TravelMap.Controllers
{
    public class PostController : Controller
    {
        private Entities db = new Entities();

        [HttpGet]
        public JsonResult GetPost(Guid id)
        {
            var post = db.Posts.First(post1 => post1.PostId == id);
            var jsonPost = new
            {
                postId = post.PostId,
                text = post.Text,
                time = post.Time.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds,
                travelId = post.TravelId,
                userId = post.UserId,
                title = post.Title
            };
            return Json(jsonPost, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPostFiles(Guid id)
        {
            try
            {
                var result = db.Posts.First(post => post.PostId == id).PostFiles.ToArray();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonErrorResponse("can't find post's files"), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetTravelReports(Guid id)
        {
            var result = db.Posts.Where(post => post.TravelId == id && post.PostType.PostTypeId == db.PostTypes.FirstOrDefault(type => type.Name == "report").PostTypeId);
            result = result.OrderBy(post => post.Time);
            var jsonResult = new List<dynamic>();
            foreach (var post in result)
            {
                jsonResult.Add(new
                {
                    postId = post.PostId,
                    text = post.Text,
                    time = post.Time.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds,
                    travelId = post.TravelId,
                    userId = post.UserId,
                    title = post.Title
                });
            }
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TravelReports(Guid travelId, string countryName = "")
        {
            ViewBag.CountryName = countryName;
            ViewBag.IsCurrentUser = (db.Travels.First(travel => travel.TravelId == travelId).UserId ==
                                     WebSecurity.CurrentUserId);
            return View(travelId);
        }

        [HttpGet]
        public JsonResult GetPostComments(Guid id)
        {
            try
            {
                var result = db.Posts.Where(post => post.ParentId == id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonErrorResponse("can't find comments"), JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public Guid PostReport(string text, Guid travelId, string title = "")
        {
            var travel = db.Travels.First(travel1 => travel1.TravelId == travelId);
            var post = new Post
            {
                PostType = db.PostTypes.First(type => type.Name == "report"),
                Travel = travel,
                Text = text,
                UserProfile = db.UserProfiles.First(profile => profile.UserId == WebSecurity.CurrentUserId),
                Time = DateTime.Now,
                ParentId = Guid.Empty,
                PostId = Guid.NewGuid(),
                UserId = WebSecurity.CurrentUserId,
                TravelId = travelId,
                TypeId = db.PostTypes.First(type => type.Name == "report").PostTypeId,
                Title = title
            };
            travel.Posts.Add(post);
            db.UserProfiles.First(profile => profile.UserId == WebSecurity.CurrentUserId).Posts.Add(post);
            db.SaveChanges();
            return post.PostId;
        }

        [HttpGet]
        public void DeleteReport(Guid reportId)
        {
            if (db.Posts.Count(post => post.PostId == reportId) == 0)
            {
                return;
            }
            db.Posts.Remove(db.Posts.First(post => post.PostId == reportId));
            db.SaveChanges();
        }

        [HttpPost]
        public void EditReport(Guid postId, string text, string title)
        {
            var post = db.Posts.First(pst => pst.PostId == postId);
            post.Text = text;
            post.Title = title;
            db.SaveChanges();
        }
    }
}