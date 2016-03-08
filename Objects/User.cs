using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BarterNamespace
{
    public class User
    {
        private int _id;
        private string _email;
        private string _picture;
        private string _password;
        private string _location;

        public User(string email, string picture, string password, string location, int id = 0)
        {
            _id = id;
            _email = email;
            _picture = picture;
            _password = password;
            _location = location;
        }

        public override bool Equals(System.Object otherUser)
        {
            if (!(otherUser is User))
            {
                return false;
            }
            else
            {
                User newUser = (User)otherUser;
                bool idEquality = this.GetId() == newUser.GetId();
                bool emailEquality = this.GetEmail() == newUser.GetEmail();
                bool pictureEquality = this.GetPicture() == newUser.GetPicture();
                bool passwordEquality = this.GetPassword() == newUser.GetPassword();
                bool locationEquality = this.GetLocation() == newUser.GetLocation();
                return (idEquality && emailEquality && pictureEquality && passwordEquality && locationEquality);
            }
        }

        public string GetEmail()
        {
            return _email;
        }

        public string GetPicture()
        {
            return _picture;
        }

        public string GetPassword()
        {
            return _password;
        }

        public string GetLocation()
        {
            return _location;
        }

        public int GetId()
        {
            return _id;
        }

        public User MatchUser(string UserName, string Password)
        {
            var conn = DB.Connection();
            conn.Open();
            
            SqlCommand cmd = new SqlCommand("Select * from barter_users where email=@Email and user_password=@Password",conn);
            
            var userNameParam = new SqlParameter();
            userNameParam.ParameterName = "@Email";
            userNameParam.Value = UserName;
            cmd.Parameters.Add(userNameParam);
            
            var userPassParam = new SqlParameter();
            userPassParam.ParameterName = "@Password";
            userPassParam.Value = Password;
            cmd.Parameters.Add(userPassParam);
            
            
        }
        public static List<User> GetAll()
        {
            List<User> allUsers = new List<User> { };

            SqlConnection conn = DB.Connection();
            SqlDataReader rdr = null;
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM barter_users;", conn);
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int Id = rdr.GetInt32(0);
                string email = rdr.GetString(1);
                string picture = rdr.GetString(2);
                string password = rdr.GetString(3);
                string location = rdr.GetString(4);

                User newUser = new User(email, picture, password, location, Id);
                allUsers.Add(newUser);
            }

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }

            return allUsers;
        }
        public void Save()
        {
            SqlConnection conn = DB.Connection();
            SqlDataReader rdr;
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO barter_users(email, pic, user_password, user_location) OUTPUT INSERTED.id VALUES(@Email, @Picture, @Password ,@Location);", conn);

            SqlParameter emailNameParameter = new SqlParameter();
            emailNameParameter.ParameterName = "@Email";
            emailNameParameter.Value = this.GetEmail();
            cmd.Parameters.Add(emailNameParameter);

            SqlParameter picNameParameter = new SqlParameter();
            picNameParameter.ParameterName = "@Picture";
            picNameParameter.Value = this.GetPicture();
            cmd.Parameters.Add(picNameParameter);

            SqlParameter user_passwordNameParameter = new SqlParameter();
            user_passwordNameParameter.ParameterName = "@Password";
            user_passwordNameParameter.Value = this.GetPassword();
            cmd.Parameters.Add(user_passwordNameParameter);

            SqlParameter user_locationNameParameter = new SqlParameter();
            user_locationNameParameter.ParameterName = "@Location";
            user_locationNameParameter.Value = this.GetLocation();
            cmd.Parameters.Add(user_locationNameParameter);

            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                this._id = rdr.GetInt32(0);
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

        public List<UserPost> GetPosts()
        {
          SqlConnection conn = DB.Connection();
          SqlDataReader rdr = null;
          conn.Open();

          List<UserPost> userposts = new List<UserPost>{};

          SqlCommand cmd = new SqlCommand("SELECT posts.* FROM posts WHERE barter_user_id = @userId", conn);

          SqlParameter UserIdParameter = new SqlParameter();
          UserIdParameter.ParameterName = "@userId";
          UserIdParameter.Value = this.GetId();

          cmd.Parameters.Add(UserIdParameter);

          rdr = cmd.ExecuteReader();

          while(rdr.Read())
          {
            int postId = rdr.GetInt32(0);
            int userId = rdr.GetInt32(1);
            string post = rdr.GetString(2);
            DateTime timeStamp = rdr.GetDateTime(3);

            UserPost newUserPost = new UserPost(userId, post, timeStamp, postId);
            userposts.Add(newUserPost);
          }
          if (rdr != null)
          {
            rdr.Close();
          }
          if (conn != null)
          {
            conn.Close();
          }
          return userposts;
        }


        public static User Find(int id)
        {
            SqlConnection conn = DB.Connection();
            SqlDataReader rdr = null;
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM barter_users WHERE id = @UserId;", conn);

            SqlParameter UserIdParameter = new SqlParameter();
            UserIdParameter.ParameterName = "@UserId";
            UserIdParameter.Value = id.ToString();
            cmd.Parameters.Add(UserIdParameter);
            rdr = cmd.ExecuteReader();

            int foundUserId = 0;
            string foundEmail = null;
            string foundPicture = null;
            string foundPassword = null;
            string foundLocation = null;

            while (rdr.Read())
            {
                foundUserId = rdr.GetInt32(0);
                foundEmail = rdr.GetString(1);
                foundPicture = rdr.GetString(2);
                foundPassword = rdr.GetString(3);
                foundLocation = rdr.GetString(4);

            }
            User newUser = new User(foundEmail, foundPicture, foundPassword, foundLocation, foundUserId);

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return newUser;
        }
        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM barter_users;", conn);
            cmd.ExecuteNonQuery();

        }
    }
}
