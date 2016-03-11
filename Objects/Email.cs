using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Linq;


namespace BarterNamespace
{
    public class Email
    {
        private int _id;
        private int _user_id;
        private string _email;
        private DateTime _date_time;
        private int _sender_id;


        public Email(int user_id, string email, DateTime date, int sender_id, int Id = 0)
        {
            _id = Id;
            _user_id = user_id;
            _email = email;
            _date_time = date;
            _sender_id = sender_id;
        }

        public override bool Equals(System.Object otherEmail)
        {
            if (!(otherEmail is Email))
            {
                return false;
            }
            else
            {
                Email newEmail = (Email)otherEmail;
                bool idEquality = this.GetId() == newEmail.GetId();
                bool user_idEquality = this.GetUser_Id() == newEmail.GetUser_Id();
                bool email_Equality = this.GetEmail() == newEmail.GetEmail();
                bool senderIdEquality = this.GetSender_Id() == newEmail.GetSender_Id();
                return (idEquality && user_idEquality && email_Equality && senderIdEquality);

            }
        }

        public int GetId()
        {
            return _id;
        }
        public string GetEmail()
        {
            return _email;
        }
        public DateTime GetDate()
        {
            return _date_time;
        }
        public void SetDate(DateTime newDate)
        {
            _date_time = newDate;
        }
        public void SetEmail(string newEmail)
        {
            _email = newEmail;
        }
        public int GetUser_Id()
        {
            return _user_id;
        }
        public void SetUser_Id(int newUser_Id)
        {
            _user_id = newUser_Id;
        }
        public int GetSender_Id()
        {
            return _sender_id;
        }
        public void SetSender_Id(int newSender_Id)
        {
            _sender_id = newSender_Id;
        }

        public static List<Email> GetAll(int id)
        {
          SqlConnection conn = DB.Connection();
          SqlDataReader rdr = null;
          conn.Open();

          List<Email> userEmails = new List<Email> { };

          SqlCommand cmd = new SqlCommand("SELECT emails.* FROM emails WHERE barter_user_id = @userId", conn);

          SqlParameter UserIdParameter = new SqlParameter();
          UserIdParameter.ParameterName = "@userId";
          UserIdParameter.Value = id;
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
          List<Email> SortedList = userEmails.OrderByDescending(o=>o._date_time).ToList();
          // Console.WriteLine()

          return SortedList;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            SqlDataReader rdr;
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO emails (email, barter_user_id, time_stamp, sender_id) OUTPUT INSERTED.id VALUES (@Email, @UserId, @TimeStamp, @SenderId);", conn);

            SqlParameter email_nameParameter = new SqlParameter();
            email_nameParameter.ParameterName = "@Email";
            email_nameParameter.Value = this.GetEmail();
            cmd.Parameters.Add(email_nameParameter);

            SqlParameter user_idParameter = new SqlParameter();
            user_idParameter.ParameterName = "@UserId";
            user_idParameter.Value = this.GetUser_Id();
            cmd.Parameters.Add(user_idParameter);

            SqlParameter date_timeParameter = new SqlParameter();
            date_timeParameter.ParameterName = "@TimeStamp";
            date_timeParameter.Value = this.GetDate();
            cmd.Parameters.Add(date_timeParameter);

            SqlParameter sender_idParameter = new SqlParameter();
            sender_idParameter.ParameterName = "@SenderId";
            sender_idParameter.Value = this.GetSender_Id();
            cmd.Parameters.Add(sender_idParameter);

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

        public string GetSenderEmail()
        {
            SqlConnection conn = DB.Connection();
            SqlDataReader rdr = null;
            conn.Open();

            // string email = " "

            SqlCommand cmd = new SqlCommand("SELECT barter_users.* FROM barter_users WHERE id = @userPostId", conn);

            SqlParameter UserIdParameter = new SqlParameter();
            UserIdParameter.ParameterName = "@userPostId";
            UserIdParameter.Value = this.GetSender_Id();

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


        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM emails;", conn);
            cmd.ExecuteNonQuery();
        }

        public static Email Find(int id)
        {
            SqlConnection conn = DB.Connection();
            SqlDataReader rdr = null;
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM emails WHERE id = @EmailId;", conn);

            SqlParameter EmailIdParameter = new SqlParameter();
            EmailIdParameter.ParameterName = "@EmailId";
            EmailIdParameter.Value = id.ToString();
            cmd.Parameters.Add(EmailIdParameter);
            rdr = cmd.ExecuteReader();

            int foundEmailId = 0;
            string foundEmail = null;
            int foundUserId = 0;
            DateTime foundDateTime = new DateTime(2016, 1, 1);
            int foundSenderId = 0;

            while (rdr.Read())
            {
                foundEmailId = rdr.GetInt32(0);
                foundUserId = rdr.GetInt32(1);
                foundEmail = rdr.GetString(2);
                foundDateTime = rdr.GetDateTime(3);
                foundSenderId = rdr.GetInt32(4);
            }
            Email newEmail = new Email(foundUserId, foundEmail, foundDateTime,  foundSenderId, foundEmailId);

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return newEmail;
        }

        public void Delete()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();


            SqlCommand cmd = new SqlCommand("DELETE from emails WHERE id = @EmailId", conn);

            SqlParameter EmailIdParameter = new SqlParameter();
            EmailIdParameter.ParameterName = "@EmailId";
            EmailIdParameter.Value = this.GetId();

            cmd.Parameters.Add(EmailIdParameter);
            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
            }
        }
    }
}
