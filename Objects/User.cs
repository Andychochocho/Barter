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
  }
}