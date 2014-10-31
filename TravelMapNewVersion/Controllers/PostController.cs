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
            try
            {
                var result = db.Posts.First(post => post.PostId == id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonErrorResponse("can't find post"), JsonRequestBehavior.AllowGet);
            }
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
            var jsonResult = new List<dynamic>();
            foreach (var post in result)
            {
                jsonResult.Add(new
                {
                    postId = post.PostId,
                    text = post.Text,
                    time = post.Time,
                    travelId = post.TravelId,
                    userId = post.UserId
                });
            }
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
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
        public void PostReport(string text, Guid travelId, string title="")
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
        }
    }
}