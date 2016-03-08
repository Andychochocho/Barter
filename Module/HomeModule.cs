using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using System.Globalization;
using System.Security;


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

        // User findUser = User.Find(userId.GetUser_Id());
        // newDictionary.Add("user", findUser);

        return View["index.cshtml", newDictionary];
      };

      Get["/about"] = _ => {
        return View["about.cshtml"];
      };

      Post["/postForm/{id}"] = parameters => {
        DateTime currentTime = DateTime.Now;
        UserPost userId = UserPost.Find(parameters.id);
        UserPost newPost = new UserPost(userId.GetId(), Request.Form["post"], currentTime);
        newPost.Save();

        Dictionary<string, object> newDictionary = new Dictionary<string, object>();
        List<UserPost> AllPosts = UserPost.GetAll();
        List<User> AllUsers = User.GetAll();

        // User findUser = User.Find(userId.GetUser_Id());
        // newDictionary.Add("user", findUser);

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

      Get["/login"] = _ => {
        return View["login.cshtml"];
      };

      Get["/email/{id}"] = parameters => {
        User foundUser = User.Find(parameters.id);
        return View["email_form.cshtml", foundUser];
      };

      Post["/email/{id}"] = parameters => {
        User foundUser = User.Find(parameters.id);
        DateTime TimeStamp = DateTime.Now;
        Email newEmail = new Email(foundUser.GetId(), Request.Form["email"], TimeStamp);
        newEmail.Save();
        Dictionary<string, object> newDictionary = new Dictionary<string, object>();
        List<UserPost> AllPosts = UserPost.GetAll();
        List<User> AllUsers = User.GetAll();
        newDictionary.Add("posts", AllPosts);
        newDictionary.Add("users", AllUsers);
        return View["index.cshtml", newDictionary];
      };

      // Post["/login"] = _ => {
      //
      //    LogOnModel logOnModel = new LogOnModel();
      //
      //    var existingCookie = Request.Cookies["userName"];
      //    if (existingCookie != null)
      //    {
      //       logOnModel.UserName = existingCookie.Value;
      //    }
      //   return View["index.cshtml"];
      // };



    }
  }
}
