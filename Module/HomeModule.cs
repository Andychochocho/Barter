using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using System.Globalization;

namespace BarterNamespace
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        Dictionary<string, object> newDictionary = new Dictionary<string, object>();

        List<UserPost> AllPosts = UserPost.GetAll();
        List<User> AllUsers = User.GetAll();
        newDictionary.Add("posts", AllPosts);
        newDictionary.Add("users", AllUsers);

        return View["index.cshtml", newDictionary];
      };

      Post["/postForm"] = _ => {
        DateTime currentTime = DateTime.Now;
        UserPost newPost = new UserPost(5, Request.Form["post"], currentTime);
        newPost.Save();
        Dictionary<string, object> newDictionary = new Dictionary<string, object>();
        List<UserPost> AllPosts = UserPost.GetAll();
        List<User> AllUsers = User.GetAll();
        newDictionary.Add("posts", AllPosts);
        newDictionary.Add("users", AllUsers);
        return View["index.cshtml", newDictionary];
      };


      Get["/newuser"] = _ => {
        return View["newuser_form.cshtml"];
      };

      Post["/newuser"] = _ => {
        User newUser = new User(Request.Form["email"], Request.Form["pic"], Request.Form["password"], Request.Form["location"]);
        newUser.Save();
        Dictionary<string, object> newDictionary = new Dictionary<string, object>();
        List<UserPost> AllPosts = UserPost.GetAll();
        List<User> AllUsers = User.GetAll();
        newDictionary.Add("posts", AllPosts);
        newDictionary.Add("users", AllUsers);
        return View["index.cshtml", newDictionary];
      };

      Get["/profile/{id}"] = parameters => {
        User foundUser = User.Find(parameters.id);
        return View["profile.cshtml", foundUser];
      };





    }
  }
}
