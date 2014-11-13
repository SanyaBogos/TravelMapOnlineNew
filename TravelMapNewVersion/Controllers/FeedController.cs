using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DotNetOpenAuth.Messaging;
using HtmlAgilityPack;
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
					post.PostId,
					post.Title,
					Country = post.Travel.Country.Name,
					StartDate = post.Travel.StartDate == null ? string.Empty : dateToJs(post.Travel.StartDate.Value).ToString(),
					EndDate = post.Travel.EndDate == null ? string.Empty : dateToJs(post.Travel.EndDate.Value).ToString(),
					User = post.UserProfile.UserName == userProfile.UserName ? "You" : post.UserProfile.UserName,
					PostText = HtmlToPlainText(post.Text),
					Likes = post.Likes.Select(l => new { l.LikeId, l.PostId, l.UserId, l.UserProfile.UserName})
				});
			}

			var result = Json(postVms, JsonRequestBehavior.AllowGet);
			// todo: add friends reports to result

			return result;
		}

		[HttpPost]
		public JsonResult Like(Guid postId)
		{
			var userId = WebSecurity.CurrentUserId;

			var like = db.Likes.FirstOrDefault(l => l.PostId == postId && l.UserId == userId);
			if (like != null)
			{
				return Json(new JsonErrorResponse("post is already liked"), JsonRequestBehavior.AllowGet);
			}
			var newLikeId = Guid.NewGuid();
			db.Likes.Add(new Like {UserId = userId, PostId = postId, LikeId = newLikeId});
			var result = db.SaveChanges();
			if (result != 1)
			{
				return Json(new JsonErrorResponse("DB was not saved :("), JsonRequestBehavior.AllowGet);
			}

			// Get all OLD likes for current Post
			var likes = db.Posts.First(p => p.PostId == postId).Likes.Where(l => l.LikeId != newLikeId)
				.Select(lk => new {lk.LikeId, lk.PostId, lk.UserId, lk.UserProfile.UserName}).ToList();

			// Just added to DB like somehow can't see userProfile reference,
			// so it is added manually
			var userName = db.UserProfiles.First(u => u.UserId ==userId).UserName;
			likes.Add(new {LikeId = newLikeId, PostId = postId, UserId = userId, UserName = userName});

			return Json(likes, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult RemoveLike(Guid postId)
		{
			var userId = WebSecurity.CurrentUserId;

			var like = db.Likes.FirstOrDefault(l => l.PostId == postId && l.UserId == userId);
			if (like == null)
			{
				Response.StatusCode = (int) HttpStatusCode.BadRequest;
				return new JsonResult {Data = new JsonErrorResponse("like was not found"), JsonRequestBehavior = JsonRequestBehavior.AllowGet};
			}
			db.Likes.Remove(like);
			var result = db.SaveChanges();
			if (result != 1)
			{
				return Json(new JsonErrorResponse("DB was not saved :("), JsonRequestBehavior.AllowGet);
			}
			var likes = db.Posts.First(p => p.PostId == postId)
				.Likes.Select(l => new {l.LikeId, l.PostId, l.UserId, l.UserProfile.UserName});
			return Json(likes, JsonRequestBehavior.AllowGet);
		}


		private double dateToJs(DateTime date)
		{
			return date.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
		}

		private static string HtmlToPlainText(string htmlText)
		{
			var htmlDoc = new HtmlDocument();
			htmlDoc.LoadHtml(htmlText);

			var result = htmlDoc.DocumentNode.InnerText;
			//todo: remove &nbsp
			return result;
		}
	}
}