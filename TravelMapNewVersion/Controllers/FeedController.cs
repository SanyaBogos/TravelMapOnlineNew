using System;
using System.Linq;
using System.Web.Mvc;
using DotNetOpenAuth.Messaging;
using nonintanon.Security;
using TravelMap.Models;

namespace TravelMap.Controllers
{
	 [Authorize]
    public class FeedController : Controller
    {
		 private Entities db = new Entities();

        // GET: Feed
        public ActionResult Index()
        {
			var userId = WebSecurity.CurrentUserId;
			return View("~/Views/Feed.cshtml", userId);
        }

		public JsonResult JIndex(Guid id)
		{
			var userProfile = db.UserProfiles.Find(id);
			if (userProfile == null)
			{
				return new JsonResult();
			}
			var posts = userProfile.Posts;
			var fposts = userProfile.Followers.Select(f => f.UserProfile.Posts);
			foreach (var fpost in fposts)
			{
				posts.AddRange(fpost);
			}

			// todo: create class to send all kinds of news: reports, new followers etc.
			posts = posts.OrderByDescending(p => p.Time).ToList();
			
			var result = Json(posts, JsonRequestBehavior.AllowGet);
			// todo: add friends reports to result
			
			return result;
		}
    }
}