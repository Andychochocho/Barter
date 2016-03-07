using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BarterNameSpace
{
  public class User
  {
      private int _id;
      private string _email;
      private string _picture;
      private string _password;
      private string _location;
    
    public User(string email, string picture, string password, string location, int id=0)
    {
        _id = id;
        _email = email;
        _picture = picture;
        _password = password;
        _location = location;
    }
    
    public override bool Equals(System.Object otherUser)
    {
        if(!(otherUser is User))
        {
            return false;
        }
        else
        {
            User newUser = (User) otherUser;
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
    
    public void Save()
    {
        SqlConnection conn = DB.Connection();
        SqlDataReader rdr;
        conn.Open();
        
        SqlCommand cmd = new SqlCommand ("INSERT INTO barter_users(email, pic, user_password, user_location) OUTPUT INSERTED.id VALUES(@Email, @Picture, @Password ,@Location);",conn);
        
        SqlParameter emailNameParameter = newSqlParameter();
        emailNameParameter.ParameterName = "@Email";
        emailNameParameter.Value= this.GetEmail();
        cmd.Parameters.Add(emailNameParameter);
        
        SqlParameter picNameParameter = newSqlParameter();
        picNameParameter.ParameterName = "@Picture";
        picNameParameter.Value= this.GetPicture();
        cmd.Parameters.Add(picNameParameter);
        
        SqlParameter user_passwordNameParameter = newSqlParameter();
        user_passwordNameParameter.ParameterName = "@Password";
        user_passwordNameParameter.Value= this.GetPassword();
        cmd.Parameters.Add(user_passwordNameParameter);
        
        SqlParameter user_locationNameParameter = newSqlParameter();
        user_locationNameParameter.ParameterName = "@Location";
        user_locationNameParameter.Value= this.GetLocation();
        cmd.Parameters.Add(user_locationNameParameter);                
        
        rdr = cmd.ExecuteReader();
        
        while(rdr.Read())
        {
            this._id = rdr.GetInt32(0);
        }
        if (rdr !- null)
        {
            rdr.Close();
        }
        if(conn != null)
        {
            conn.Close();
        }
    }
  }
}


