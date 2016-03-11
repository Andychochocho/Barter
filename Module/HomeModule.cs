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
        User foundUser = new User("Patrick O'Whiskers", "http://56.media.tumblr.com/tumblr_lvy0v3pIo71r4fsmgo1_500.jpg", "password", "portland", "im a cool cat");
        newDictionary.Add("user", foundUser);
        newDictionary.Add("posts", AllPosts);
        newDictionary.Add("users", AllUsers);
        return View["index.cshtml", newDictionary];
      };

      Get["/location/{id}"] = parameters => {
        Dictionary<string, object> newDictionary = new Dictionary<string, object>();
        List<UserPost> AllPosts = UserPost.GetAll();
        User foundUser = User.Find(parameters.id);
        List<User> AllUsers = User.GetAll();
        newDictionary.Add("user", foundUser);
        newDictionary.Add("posts", AllPosts);
        newDictionary.Add("users", AllUsers);
        return View["index.cshtml", newDictionary];
      };

      Post["/location"] = parameters => {
        User foundUser = new User("Patrick O'Whiskers", "http://56.media.tumblr.com/tumblr_lvy0v3pIo71r4fsmgo1_500.jpg", "password", "portland", "im a cool cat");
        Dictionary<string, object> newDictionary = new Dictionary<string, object>();
        List<UserPost> LocationPosts = UserPost.SearchLocationPosts(Request.Form["location"]);
        List<User> AllUsers = User.GetAll();
        newDictionary.Add("user", foundUser);
        newDictionary.Add("posts", LocationPosts);
        newDictionary.Add("users", AllUsers);
        return View["index.cshtml", newDictionary];
      };


      Get["/about"] = _ => {
        return View["about.cshtml"];
      };

      Post["/postForm/{id}"] = parameters => {
        DateTime currentTime = DateTime.Now;
        User userId = User.Find(parameters.id);
        UserPost newPost = new UserPost(userId.GetId(), Request.Form["post"], currentTime);
        newPost.Save();

        Dictionary<string, object> newDictionary = new Dictionary<string, object>();
        List<UserPost> AllPosts = UserPost.GetAll();
        List<User> AllUsers = User.GetAll();


        User foundUser = new User("Patrick O'Whiskers", "http://56.media.tumblr.com/tumblr_lvy0v3pIo71r4fsmgo1_500.jpg", "password", "portland", "im a cool cat");
        newDictionary.Add("user", foundUser);
        newDictionary.Add("posts", AllPosts);
        newDictionary.Add("users", AllUsers);
        return View["index.cshtml", newDictionary];
      };


      Get["/newuser"] = _ => {
        return View["newuser_form.cshtml"];
      };

      Post["/newuser"] = _ => {
        User newUser = new User(Request.Form["email"], Request.Form["pic"], Request.Form["password"], Request.Form["location"], "about me");
        newUser.Save(Request.Form["email"]);
        Dictionary<string, object> newDictionary = new Dictionary<string, object>();
        List<UserPost> AllPosts = UserPost.GetAll();
        List<User> AllUsers = User.GetAll();
        User foundUser = new User("Patrick O'Whiskers", "http://56.media.tumblr.com/tumblr_lvy0v3pIo71r4fsmgo1_500.jpg", "password", "portland", "im a cool cat");
        newDictionary.Add("user", foundUser);
        newDictionary.Add("posts", AllPosts);
        newDictionary.Add("users", AllUsers);
        return View["login.cshtml", newDictionary];
      };

      Get["/profile/{id}"] = parameters => {
        User foundUser = User.Find(parameters.id);
        Console.WriteLine(foundUser.GetAboutMe());
        return View["profile.cshtml", foundUser];
      };

      Patch["/update/about_me/{id}"] = parameters => {
        User foundUser = User.Find(parameters.id);
        foundUser.Update(Request.Form["aboutMe"]);
        Console.WriteLine(foundUser.GetAboutMe());
        return View["profile.cshtml", foundUser];
      };

      Get["/login"] = _ => {

        return View["login.cshtml"];
      };

      Post["/login"] = _ =>{
        if (User.MatchUser(Request.Form["email"], Request.Form["password"]) == true)
        {
          User.LogIn(Request.Form["email"]);
          Dictionary<string, object> newDictionary = new Dictionary<string, object>();
          User foundUser = new User("Patrick O'Whiskers", "http://56.media.tumblr.com/tumblr_lvy0v3pIo71r4fsmgo1_500.jpg", "password", "portland", "im a cool cat");
          List<UserPost> AllPosts = UserPost.GetAll();
          List<User> AllUsers = User.GetAll();
          newDictionary.Add("user", foundUser);
          newDictionary.Add("posts", AllPosts);
          newDictionary.Add("users", AllUsers);
          
          return View["index.cshtml", newDictionary];
        }
        else {
            //write to view "Login failed, try again"
            return View["login.cshtml"];
        }

      };
      
      Get["/logout"] = _ =>{
        return View["logout.cshtml"];  
      };
      
      Post["/logout"] = _ => {
        User.LogOut(int.Parse(Request.Form["logout"]));

        return View["logout.cshtml"];
      };

      Get["/email/{id}"] = parameters => {
        User foundUser = User.Find(parameters.id);
        Dictionary<string, object> newDictionary = new Dictionary<string, object>();
        List<User> AllUsers = User.GetAll();
        newDictionary.Add("user", foundUser);
        newDictionary.Add("users", AllUsers);
        return View["email_form.cshtml", newDictionary];
      };

      Post["/email/{id}"] = parameters => {
      User foundUser = User.Find(parameters.id);
      DateTime TimeStamp = DateTime.Now;
      // User sendingUser = User.Find(Request.Form["userList"]);
      Email newEmail = new Email(foundUser.GetId(), Request.Form["email"], TimeStamp, Request.Form["sender"]);
      newEmail.Save();
      Dictionary<string, object> newDictionary = new Dictionary<string, object>();
      List<UserPost> AllPosts = UserPost.GetAll();
      List<User> AllUsers = User.GetAll();

      newDictionary.Add("user", foundUser);
      newDictionary.Add("posts", AllPosts);
      newDictionary.Add("users", AllUsers);
      return View["index.cshtml", newDictionary];
    };

    }
  }
}
