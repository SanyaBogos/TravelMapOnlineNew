using System;
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
        public JsonResult GetTravelReport(Guid id)
        {
            try
            {
                var result = db.Posts.First(post => post.TravelId == id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new JsonErrorResponse("can't find travel report"), JsonRequestBehavior.AllowGet);
            }
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
        public void PostReport(string text, Guid travelId)
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
                TypeId = db.PostTypes.First(type => type.Name == "report").PostTypeId
            };
            travel.Posts.Add(post);
            db.UserProfiles.First(profile => profile.UserId == WebSecurity.CurrentUserId).Posts.Add(post);
            db.SaveChanges();
        }
    }
}