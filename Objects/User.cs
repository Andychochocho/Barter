using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BarterNameSpace
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


        public static List<User> GetAll()
        {
            List<User> allUsers = new List<User> { };

            SqlConnection conn = DB.Connection();
            SqlDataReader rdr = null;
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Users;", conn);
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
    }
}