using System;
using System.Collections.Generic;
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
		
		//todo: create class to send all kinds of news: reports, new followers etc.
		 
		[HttpGet]
		public JsonResult GetPosts(Guid id)
		{
			var userProfile = db.UserProfiles.Find(id);
			if (userProfile == null)
			{
				return new JsonResult();
			}
			var posts = userProfile.Posts;

			var followers = userProfile.Followers.Select(f => f.FollowerId);
			var followersPosts = db.Posts.Where(p => followers.Contains(p.UserId));
			
			//var allPosts = db.Posts.Where(p => p.UserId != id);
			posts.AddRange(followersPosts);

			posts = posts.OrderByDescending(p => p.Time).ToList();

			var postVms = new List<object>();
			foreach (var post in posts)
			{
				postVms.Add(new
				{
					post.Title,
					Country = post.Travel.Country.Name,
					StartDate = post.Travel.StartDate == null ? string.Empty : dateToJs(post.Travel.StartDate.Value).ToString(),
					EndDate = post.Travel.EndDate == null ? string.Empty : dateToJs(post.Travel.EndDate.Value).ToString(),
					User = post.UserProfile.UserName == userProfile.UserName ? "You" : post.UserProfile.UserName,
					PostText = post.Text
				});
			}

			var result = Json(postVms, JsonRequestBehavior.AllowGet);
			// todo: add friends reports to result
			
			return result;
		}

		 //private class PostViewModel
		 //{
		 //	public string PostText { get; set; }
		 //	public string User { get; set; }
		 //	public double StartDate { get; set; }
		 //	public double EndDate { get; set; }
		 //	public string Title { get; set; }
		 //	public string Country { get; set; }
		 //}

		 private double dateToJs(DateTime date)
		 {
			 return date.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
		 }
    }
}