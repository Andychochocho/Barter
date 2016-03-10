using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Data;
using System.Linq;

namespace BarterNamespace
{
    public class User
    {
        private int _id;
        private string _email;
        private string _picture;
        private string _password;
        private string _location;
        private string _aboutMe;

        public User(string email, string picture, string password, string location, string AboutMe, int id = 0)
        {
            _id = id;
            _email = email;
            _picture = picture;
            _password = password;
            _location = location;
            _aboutMe = AboutMe;
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
                bool aboutMeEquality = this.GetAboutMe() == newUser.GetAboutMe();

                return (idEquality && emailEquality && pictureEquality && passwordEquality && locationEquality && aboutMeEquality);
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

        public string GetAboutMe()
        {
            return _aboutMe;
        }


        public static bool MatchUser(string UserName, string Password)
        {
            var conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("Select * from barter_users where email=@Email and user_password=@Password", conn);

            var userNameParam = new SqlParameter();
            userNameParam.ParameterName = "@Email";
            userNameParam.Value = UserName;
            cmd.Parameters.Add(userNameParam);

            var userPassParam = new SqlParameter();
            userPassParam.ParameterName = "@Password";
            userPassParam.Value = Password;
            cmd.Parameters.Add(userPassParam);

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            conn.Close();

            int count = ds.Tables[0].Rows.Count;

            if (count == 1)
            {
                Console.WriteLine(true);
                return true;
            }
            else
            {
                Console.WriteLine(false);
                return false;
            }
        }
        // TIM THIS IS THE FOREACH LOOP
        // public static void DetermineUser()
        // {
        //    var conn = DB.Connection();
        //    conn.Open();

        //    foreach(var user  in users)
        //    {
        //        if(users.MatchedUser == user)
        //        {
        //            return userinfo;
        //        }
        //        else
        //        {
        //            return 401;
        //        }
        //    }
        // }

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
                string aboutMe = rdr.GetString(5);



                User newUser = new User(email, picture, password, location, aboutMe, Id);
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

            SqlCommand cmd = new SqlCommand("INSERT INTO barter_users(email, pic, user_password, user_location, about_me) OUTPUT INSERTED.id VALUES(@Email, @Picture, @Password ,@Location, @about);", conn);

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

            SqlParameter userAbout = new SqlParameter();
            userAbout.ParameterName = "@about";
            userAbout.Value = "About Me!";
            cmd.Parameters.Add(userAbout);

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

        public string Update(string updatedAboutMe)
        {
          SqlConnection conn = DB.Connection();
          SqlDataReader rdr;
          conn.Open();

          SqlCommand cmd = new SqlCommand("UPDATE barter_users SET about_me = @updatedAboutMe OUTPUT INSERTED.about_me WHERE id = @UserProfileId;", conn);

          SqlParameter updateAboutMe = new SqlParameter();
          updateAboutMe.ParameterName = "@updatedAboutMe";
          updateAboutMe.Value = updatedAboutMe;
          cmd.Parameters.Add(updateAboutMe);


          SqlParameter UserPostIdParameter = new SqlParameter();
          UserPostIdParameter.ParameterName = "@UserProfileId";
          UserPostIdParameter.Value = this.GetId();
          cmd.Parameters.Add(UserPostIdParameter);
          rdr = cmd.ExecuteReader();

          while(rdr.Read())
          {
            this._aboutMe = rdr.GetString(0);
          }

          if (rdr != null)
          {
            rdr.Close();
          }

          if (conn != null)
          {
            conn.Close();
          }
          return updatedAboutMe;
        }

        public List<UserPost> GetPosts()
        {
            SqlConnection conn = DB.Connection();
            SqlDataReader rdr = null;
            conn.Open();

            List<UserPost> userposts = new List<UserPost> { };

            SqlCommand cmd = new SqlCommand("SELECT posts.* FROM posts WHERE barter_user_id = @userId", conn);

            SqlParameter UserIdParameter = new SqlParameter();
            UserIdParameter.ParameterName = "@userId";
            UserIdParameter.Value = this.GetId();

            cmd.Parameters.Add(UserIdParameter);

            rdr = cmd.ExecuteReader();

            while (rdr.Read())
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

        public List<Email> GetEmails()
        {
            SqlConnection conn = DB.Connection();
            SqlDataReader rdr = null;
            conn.Open();

            List<Email> userEmails = new List<Email> { };

            SqlCommand cmd = new SqlCommand("SELECT emails.* FROM emails WHERE barter_user_id = @userId", conn);

            SqlParameter UserIdParameter = new SqlParameter();
            UserIdParameter.ParameterName = "@userId";
            UserIdParameter.Value = this.GetId();
            cmd.Parameters.Add(UserIdParameter);

            rdr = cmd.ExecuteReader();

            int foundEmailId = 0;
            string foundEmail = null;
            int foundUserId = 0;
            DateTime foundDateTime = new DateTime(2016, 1, 1);
            int foundSenderId = 1;

            while (rdr.Read())
            {
                foundEmailId = rdr.GetInt32(0);
                foundUserId = rdr.GetInt32(1);
                foundEmail = rdr.GetString(2);
                foundDateTime = rdr.GetDateTime(3);
                foundSenderId = rdr.GetInt32(4);
                Email newEmail = new Email(foundUserId, foundEmail, foundDateTime, foundSenderId, foundEmailId);
                userEmails.Add(newEmail);

            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            // List<Email> SortedEmail = userEmails.OrderByDescending(o=>o.userEmails.GetDateTime()).ToList();
            return userEmails;
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
            string foundAbout = null;


            while (rdr.Read())
            {
                foundUserId = rdr.GetInt32(0);
                foundEmail = rdr.GetString(1);
                foundPicture = rdr.GetString(2);
                foundPassword = rdr.GetString(3);
                foundLocation = rdr.GetString(4);
                foundAbout = rdr.GetString(5);


            }
            User newUser = new User(foundEmail, foundPicture, foundPassword, foundLocation, foundAbout, foundUserId);

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
