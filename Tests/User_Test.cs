using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BarterNamespace
{
  public class UserTest : IDisposable
  {
    public UserTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=barter_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_GetPosts()
    {

      User testUser = new User("steve@aol.com","pic","Password","location");
      testUser.Save();

      UserPost testPost = new UserPost(testUser.GetId(), "Magic Johnson", new DateTime(2000, 1, 1));
      testPost.Save();
      //Act

      List<UserPost> result = testUser.GetPosts();


      List<UserPost> testList = new List<UserPost> {testPost};
      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_GetEmails()
    {

      User testUser = new User("steve@aol.com","pic","Password","location");
      testUser.Save();

      Email testEmail = new Email(testUser.GetId(), "Magic Johnson", new DateTime(2000, 1, 1), 1);
      testEmail.Save();
      //Act

      List<Email> result = testUser.GetEmails();

      List<Email> testList = new List<Email> {testEmail};
      //Assert
      Assert.Equal(testList, result);
    }


    [Fact]
    public void Dispose()
    {
      User.DeleteAll();
      UserPost.DeleteAll();
    }
  }
}
