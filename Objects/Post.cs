using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BarterNamespace
{
  public class Post
  {
    private int _id;
    private int _user_id;
    private string _post;
    private DateTime _date_time;


    public Post(int user_id, string post, DateTime date, int Id = 0)
    {
      _id = Id;
      _user_id = user_id;
      _post = post;
      _date_time = date;
    }

    public override bool Equals(System.Object otherPost)
    {
        if (!(otherPost is Post))
        {
          return false;
        }
        else
        {
          Post newPost = (Post) otherPost;
          bool idEquality = this.GetId() == newPost.GetId();
          bool user_idEquality = this.GetUser_Id() == newPost.GetUser_Id();
          bool post_Equality = this.GetPost() == newPost.GetPost();
          return (idEquality && user_idEquality && post_Equality);
        }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetPost()
    {
      return _post;
    }
    public DateTime GetDate()
    {
      return _date_time;
    }
    public void SetDate(DateTime newDate)
    {
      _date_time = newDate;
    }
    public void SetPost(string newPost)
    {
      _post = newPost;
    }
    public int GetUser_Id()
    {
      return _user_id;
    }
    public void SetUser_Id(int newUser_Id)
    {
      _user_id = newUser_Id;
    }



    public static List<Post> GetAll()
    {
      List<Post> allPosts = new List<Post>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM posts;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int Id = rdr.GetInt32(0);
        int User_id = rdr.GetInt32(1);
        string Post = rdr.GetString(2);
        DateTime Date = rdr.GetDateTime(3);

        Post newPost = new Post(User_id, Post, Date, Id);
        allPosts.Add(newPost);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allPosts;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO posts (post, barter_user_id, time_stamp) OUTPUT INSERTED.id VALUES (@Post, @UserId, @TimeStamp);", conn);

      SqlParameter post_nameParameter = new SqlParameter();
      post_nameParameter.ParameterName = "@Post";
      post_nameParameter.Value = this.GetPost();
      cmd.Parameters.Add(post_nameParameter);

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

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM posts;", conn);
      cmd.ExecuteNonQuery();
    }

    public static Post Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM posts WHERE id = @PostId;", conn);

      SqlParameter PostIdParameter = new SqlParameter();
      PostIdParameter.ParameterName = "@PostId";
      PostIdParameter.Value = id.ToString();
      cmd.Parameters.Add(PostIdParameter);
      rdr = cmd.ExecuteReader();

      int foundPostId = 0;
      string foundPost = null;
      int foundUserId= 0;
      DateTime foundDateTime = new DateTime(2016, 1, 1);

      while(rdr.Read())
      {
        foundPostId = rdr.GetInt32(0);
        foundUserId = rdr.GetInt32(1);
        foundPost = rdr.GetString(2);
        foundDateTime = rdr.GetDateTime(3);
      }
      Post newPost = new Post(foundUserId, foundPost, foundDateTime, foundPostId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return newPost;
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();


      SqlCommand cmd = new SqlCommand("DELETE from posts WHERE id = @PostId", conn);

      SqlParameter PostIdParameter = new SqlParameter();
      PostIdParameter.ParameterName = "@PostId";
      PostIdParameter.Value = this.GetId();

      cmd.Parameters.Add(PostIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
