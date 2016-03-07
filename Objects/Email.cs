using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BarterNamespace
{
    public class Email
    {
        private int _id;
        private int _user_id;
        private string _email;
        private DateTime _date_time;


        public Email(int user_id, string email, DateTime date, int Id = 0)
        {
            _id = Id;
            _user_id = user_id;
            _email = email;
            _date_time = date;
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
                return (idEquality && user_idEquality && email_Equality);
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



        public static List<Email> GetAll()
        {
            List<Email> allEmails = new List<Email> { };

            SqlConnection conn = DB.Connection();
            SqlDataReader rdr = null;
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM emails;", conn);
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int Id = rdr.GetInt32(0);
                int User_id = rdr.GetInt32(1);
                string Email = rdr.GetString(2);
                DateTime Date = rdr.GetDateTime(3);

                Email newEmail = new Email(User_id, Email, Date, Id);
                allEmails.Add(newEmail);
            }

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }

            return allEmails;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            SqlDataReader rdr;
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO emails (email, barter_user_id, time_stamp) OUTPUT INSERTED.id VALUES (@Email, @UserId, @TimeStamp);", conn);

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

            while (rdr.Read())
            {
                foundEmailId = rdr.GetInt32(0);
                foundUserId = rdr.GetInt32(1);
                foundEmail = rdr.GetString(2);
                foundDateTime = rdr.GetDateTime(3);
            }
            Email newEmail = new Email(foundUserId, foundEmail, foundDateTime, foundEmailId);

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
