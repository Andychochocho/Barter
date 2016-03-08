using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using System.Globalization;
using System.Security;
using System.Net;
using System.Web;


namespace BarterNamespace
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ =>
            {
                Dictionary<string, object> newDictionary = new Dictionary<string, object>();

                List<UserPost> AllPosts = UserPost.GetAll();
                List<User> AllUsers = User.GetAll();
                newDictionary.Add("posts", AllPosts);
                newDictionary.Add("users", AllUsers);

                return View["index.cshtml", newDictionary];
            };

            Post["/postForm/{id}"] = parameters =>
            {
                DateTime currentTime = DateTime.Now;
                UserPost userId = UserPost.Find(parameters.id);
                UserPost newPost = new UserPost(userId.GetId(), Request.Form["post"], currentTime);
                newPost.Save();

                Dictionary<string, object> newDictionary = new Dictionary<string, object>();
                List<UserPost> AllPosts = UserPost.GetAll();
                List<User> AllUsers = User.GetAll();
                newDictionary.Add("posts", AllPosts);
                newDictionary.Add("users", AllUsers);
                return View["index.cshtml", newDictionary];
            };


            Get["/newuser"] = _ =>
            {
                return View["newuser_form.cshtml"];
            };

            Post["/newuser"] = _ =>
            {
                User newUser = new User(Request.Form["email"], Request.Form["pic"], Request.Form["password"], Request.Form["location"]);
                newUser.Save();
                Dictionary<string, object> newDictionary = new Dictionary<string, object>();
                List<UserPost> AllPosts = UserPost.GetAll();
                List<User> AllUsers = User.GetAll();
                newDictionary.Add("posts", AllPosts);
                newDictionary.Add("users", AllUsers);
                return View["index.cshtml", newDictionary];
            };

            Get["/profile/{id}"] = parameters =>
            {
                User foundUser = User.Find(parameters.id);
                return View["profile.cshtml", foundUser];
            };

            Get["/login"] = _ =>
                {
                return View["login.cshtml"];
            };

            Post["/login"] = _ =>
            {
              
                    var newUser = User.MatchUser(Request.Form["user-email"], Request.Form["user-password"]);
                    if (newUser == true)
                    {
                        HttpCookie userCookie = new HttpCookie("user");
                        userCookie.Expires.AddDays(365);
                        HttpContext.Response.Cookies.Add(userCookie);

                        return RedirectToActionPermanent("Index");
                    }
                
                return View["index.cshtml"];
            };



        }
    }
}
