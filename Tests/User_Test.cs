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


      Console.WriteLine(testUser.GetEmail());
      Console.WriteLine(testPost.GetUserPost());
      Console.WriteLine(result.Count);

      List<UserPost> testList = new List<UserPost> {testPost};
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
