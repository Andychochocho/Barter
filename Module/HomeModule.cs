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
        return View["index.cshtml"];
      };

      Post["/postForm"] = _ => {
        DateTime currentTime = DateTime.Now;
        UserPost newPost = new UserPost(5, Request.Form["post"], currentTime);
        newPost.Save();
        List<UserPost> AllPosts = UserPost.GetAll();
        return View["index.cshtml", AllPosts];
      };





    }
  }
}
