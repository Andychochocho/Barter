using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BarterNamespace
{
  public class UserPost
  {
    private int _id;
    private int _user_id;
    private string _userpost;
    private DateTime _date_time;


    public UserPost(int user_id, string userpost, DateTime date, int Id = 0)
    {
      _id = Id;
      _user_id = user_id;
      _userpost = userpost;
      _date_time = date;
    }

    public override bool Equals(System.Object otherUserPost)
    {
        if (!(otherUserPost is UserPost))
        {
          return false;
        }
        else
        {
          UserPost newUserPost = (UserPost) otherUserPost;
          bool idEquality = this.GetId() == newUserPost.GetId();
          bool user_idEquality = this.GetUser_Id() == newUserPost.GetUser_Id();
          bool userpost_Equality = this.GetUserPost() == newUserPost.GetUserPost();
          return (idEquality && user_idEquality && userpost_Equality);
        }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetUserPost()
    {
      return _userpost;
    }
    public DateTime GetDate()
    {
      return _date_time;
    }
    public void SetDate(DateTime newDate)
    {
      _date_time = newDate;
    }
    public void SetUserPost(string newUserPost)
    {
      _userpost = newUserPost;
    }
    public int GetUser_Id()
    {
      return _user_id;
    }
    public void SetUser_Id(int newUser_Id)
    {
      _user_id = newUser_Id;
    }



    public static List<UserPost> GetAll()
    {
      List<UserPost> allUserPosts = new List<UserPost>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM posts;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int Id = rdr.GetInt32(0);
        int User_id = rdr.GetInt32(1);
        string UserPost = rdr.GetString(2);
        DateTime Date = rdr.GetDateTime(3);

        UserPost newUserPost = new UserPost(User_id, UserPost, Date, Id);
        allUserPosts.Add(newUserPost);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allUserPosts;
    }

    public static List<UserPost> SearchLocationPosts(string searchString)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      // List<User> locationUsers = new List<User> { };
      List<UserPost> locationUserPosts = new List<UserPost> { };

      SqlCommand cmd = new SqlCommand("SELECT barter_users.* FROM barter_users WHERE user_location = @SearchLocation", conn);

      SqlParameter searchLocationParameter = new SqlParameter();
      searchLocationParameter.ParameterName = "@SearchLocation";
      searchLocationParameter.Value = searchString;
      cmd.Parameters.Add(searchLocationParameter);

      rdr = cmd.ExecuteReader();

      int foundUserId = 0;
      string foundEmail = null;
      string foundPicture = null;
      string foundPassword = null;
      string foundLocation = null;
      string foundAbout = null;


      while (rdr.Read())
      {
          foundUserId = rdr.GetInt32(0);
          foundEmail = rdr.GetString(1);
          foundPicture = rdr.GetString(2);
          foundPassword = rdr.GetString(3);
          foundLocation = rdr.GetString(4);
          foundAbout = rdr.GetString(5);

        User newUser = new User(foundEmail, foundPicture, foundPassword, foundLocation, foundAbout, foundUserId);
        locationUserPosts.AddRange(newUser.GetPosts());
      }

      if (rdr != null)
      {
          rdr.Close();
      }
      if (conn != null)
      {
          conn.Close();
      }


      return locationUserPosts;
    }

    public string GetUserEmail()
    {
        SqlConnection conn = DB.Connection();
        SqlDataReader rdr = null;
        conn.Open();

        // string email = " "

        SqlCommand cmd = new SqlCommand("SELECT barter_users.* FROM barter_users WHERE id = @userPostId", conn);

        SqlParameter UserIdParameter = new SqlParameter();
        UserIdParameter.ParameterName = "@userPostId";
        UserIdParameter.Value = this.GetUser_Id();

        cmd.Parameters.Add(UserIdParameter);

        rdr = cmd.ExecuteReader();

        int postId = 0;
        string email = null;
        string pic = null;
        string password = null;
        string location = null;
        string about = null;


        while (rdr.Read())
        {
            postId = rdr.GetInt32(0);
            email = rdr.GetString(1);
            pic = rdr.GetString(2);
            password = rdr.GetString(3);
            location = rdr.GetString(4);
            about = rdr.GetString(5);


        }
        User newUser = new User(email, pic, password, location, about, postId);

        if (rdr != null)
        {
            rdr.Close();
        }
        if (conn != null)
        {
            conn.Close();
        }
        string userEmail = newUser.GetEmail();
        return userEmail;
    }

    public string GetUserPicture()
    {
        SqlConnection conn = DB.Connection();
        SqlDataReader rdr = null;
        conn.Open();

        // string email = " "

        SqlCommand cmd = new SqlCommand("SELECT barter_users.* FROM barter_users WHERE id = @userPostId", conn);

        SqlParameter UserIdParameter = new SqlParameter();
        UserIdParameter.ParameterName = "@userPostId";
        UserIdParameter.Value = this.GetUser_Id();

        cmd.Parameters.Add(UserIdParameter);

        rdr = cmd.ExecuteReader();

        int postId = 0;
        string email = null;
        string pic = null;
        string password = null;
        string location = null;
        string about = null;


        while (rdr.Read())
        {
            postId = rdr.GetInt32(0);
            email = rdr.GetString(1);
            pic = rdr.GetString(2);
            password = rdr.GetString(3);
            location = rdr.GetString(4);
            about = rdr.GetString(4);


        }
        User newUser = new User(email, pic, password, location, about, postId);

        if (rdr != null)
        {
            rdr.Close();
        }
        if (conn != null)
        {
            conn.Close();
        }
        string userPicture = newUser.GetPicture();
        return userPicture;
    }

    public string GetUserLocation()
    {
        SqlConnection conn = DB.Connection();
        SqlDataReader rdr = null;
        conn.Open();

        // string email = " "

        SqlCommand cmd = new SqlCommand("SELECT barter_users.* FROM barter_users WHERE id = @userPostId", conn);

        SqlParameter UserIdParameter = new SqlParameter();
        UserIdParameter.ParameterName = "@userPostId";
        UserIdParameter.Value = this.GetUser_Id();

        cmd.Parameters.Add(UserIdParameter);

        rdr = cmd.ExecuteReader();

        int postId = 0;
        string email = null;
        string pic = null;
        string password = null;
        string location = null;
        string about = null;


        while (rdr.Read())
        {
            postId = rdr.GetInt32(0);
            email = rdr.GetString(1);
            pic = rdr.GetString(2);
            password = rdr.GetString(3);
            location = rdr.GetString(4);
            about = rdr.GetString(4);


        }
        User newUser = new User(email, pic, password, location, about, postId);

        if (rdr != null)
        {
            rdr.Close();
        }
        if (conn != null)
        {
            conn.Close();
        }
        string userLocation = newUser.GetLocation();
        return userLocation;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO posts (post, barter_user_id, time_stamp) OUTPUT INSERTED.id VALUES (@UserPost, @UserId, @TimeStamp);", conn);

      SqlParameter userpost_nameParameter = new SqlParameter();
      userpost_nameParameter.ParameterName = "@UserPost";
      userpost_nameParameter.Value = this.GetUserPost();
      cmd.Parameters.Add(userpost_nameParameter);

      SqlParameter user_idParameter = new SqlParameter();
      user_idParameter.ParameterName = "@UserId";
      user_idParameter.Value = this.GetUser_Id();
      cmd.Parameters.Add(user_idParameter);

      SqlParameter date_timeParameter = new SqlParameter();
      date_timeParameter.ParameterName = "@TimeStamp";
      date_timeParameter.Value = this.GetDate();
      cmd.Parameters.Add(date_timeParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public void Update(string updatedUserPost)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE posts SET post = @updatedUserPost OUTPUT INSERTED.post WHERE id = @UserPostId;", conn);

      SqlParameter newUserPostParameter = new SqlParameter();
      newUserPostParameter.ParameterName = "@updatedUserPost";
      newUserPostParameter.Value = updatedUserPost;
      cmd.Parameters.Add(newUserPostParameter);


      SqlParameter UserPostIdParameter = new SqlParameter();
      UserPostIdParameter.ParameterName = "@UserPostId";
      UserPostIdParameter.Value = this.GetId();
      cmd.Parameters.Add(UserPostIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._userpost = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
    }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM posts;", conn);
      cmd.ExecuteNonQuery();
    }

    public static UserPost Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM posts WHERE id = @UserPostId;", conn);

      SqlParameter UserPostIdParameter = new SqlParameter();
      UserPostIdParameter.ParameterName = "@UserPostId";
      UserPostIdParameter.Value = id.ToString();
      cmd.Parameters.Add(UserPostIdParameter);
      rdr = cmd.ExecuteReader();

      int foundUserPostId = 0;
      string foundUserPost = null;
      int foundUserId= 0;
      DateTime foundDateTime = new DateTime(2016, 1, 1);

      while(rdr.Read())
      {
        foundUserPostId = rdr.GetInt32(0);
        foundUserId = rdr.GetInt32(1);
        foundUserPost = rdr.GetString(2);
        foundDateTime = rdr.GetDateTime(3);
      }
      UserPost newUserPost = new UserPost(foundUserId, foundUserPost, foundDateTime, foundUserPostId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return newUserPost;
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();


      SqlCommand cmd = new SqlCommand("DELETE from posts WHERE id = @UserPostId", conn);

      SqlParameter UserPostIdParameter = new SqlParameter();
      UserPostIdParameter.ParameterName = "@UserPostId";
      UserPostIdParameter.Value = this.GetId();

      cmd.Parameters.Add(UserPostIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
