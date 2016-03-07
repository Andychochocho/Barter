using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BarterNamespace
{
  public class UserPostTest : IDisposable
  {
    public UserPostTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=barter_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_EmptyAtFirst()
    {
      //Arrange, Act
      int result = UserPost.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_EqualOverrideTrueForSameDescription()
    {
      //Arrange, Act
      UserPost firstUserPost = new UserPost(5, "userpost text", new DateTime(2000, 1, 1));
      UserPost secondUserPost = new UserPost(5, "userpost text", new DateTime(2000, 1, 1));
      //Assert
      Assert.Equal(firstUserPost, secondUserPost);
    }

    [Fact]
    public void Test_Save()
    {
      //Arrange
      UserPost testUserPost = new UserPost(5, "userpost text", new DateTime(2000, 1, 1));
      testUserPost.Save();
      //Act
      List<UserPost> result = UserPost.GetAll();
      List<UserPost> testList = new List<UserPost>{testUserPost};
      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_GetAll()
    {
      //Arrange
      UserPost testUserPost = new UserPost(5, "userpost text", new DateTime(2000, 1, 1));
      testUserPost.Save();
      UserPost testUserPost2 = new UserPost(5, "userpost text", new DateTime(2000, 1, 1));
      testUserPost2.Save();
      //Act
      List<UserPost> result = UserPost.GetAll();
      List<UserPost> testList = new List<UserPost>{testUserPost, testUserPost2};
      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_SaveAssignsIdToObject()
    {
      //Arrange
      UserPost testUserPost = new UserPost(5, "userpost text", new DateTime(2000, 1, 1));
      testUserPost.Save();
      //Act
      UserPost savedUserPost = UserPost.GetAll()[0];
      int result = savedUserPost.GetId();
      int testId = testUserPost.GetId();
      //Assert
      Assert.Equal(testId, result);
    }


    [Fact]
    public void Test_UpdateUserPost()
    {
      //Arrange
      UserPost testUserPost = new UserPost(5, "userpost text", new DateTime(2000, 1, 1));
      testUserPost.Save();
      //Arrang
      string newUserPost = "new userpost text";
      //Act
      testUserPost.Update(newUserPost);
      string result = testUserPost.GetUserPost();
      //Assert
      Assert.Equal(newUserPost, result);
    }
    [Fact]

    public void Test_Delete_DeletesUserPostFromDatabase()
    {
      //Arrange
      string name1 = "Soccer";
      UserPost testUserPost1 = new UserPost(5, name1, new DateTime(2000, 1, 1));
      testUserPost1.Save();
      string name2 = "Dancing";
      UserPost testUserPost2 = new UserPost(5, name2, new DateTime(2000, 1, 1));
      testUserPost2.Save();
      //Act
      testUserPost1.Delete();
      List<UserPost> resultUserPosts = UserPost.GetAll();
      List<UserPost> testUserPostList = new List<UserPost> {testUserPost2};
      //Assert
      Assert.Equal(testUserPostList, resultUserPosts);
    }

    [Fact]
    public void Test_FindFindsUserPostInDatabase()
    {
      //Arrange
      UserPost testUserPost = new UserPost(5, "userpost text", new DateTime(2000, 1, 1));
      testUserPost.Save();
      //Act
      UserPost result = UserPost.Find(testUserPost.GetId());
      //Assert
      Assert.Equal(testUserPost, result);
    }


    [Fact]
    public void Dispose()
    {
      UserPost.DeleteAll();
    }
  }
}
