using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BarterNamespace
{
  public class PostTest : IDisposable
  {
    public PostTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=barter_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_EmptyAtFirst()
    {
      //Arrange, Act
      int result = Post.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_EqualOverrideTrueForSameDescription()
    {
      //Arrange, Act
      Post firstPost = new Post(5, "post text", new DateTime(2000, 1, 1));
      Post secondPost = new Post(5, "post text", new DateTime(2000, 1, 1));
      //Assert
      Assert.Equal(firstPost, secondPost);
    }

    [Fact]
    public void Test_Save()
    {
      //Arrange
      Post testPost = new Post(5, "post text", new DateTime(2000, 1, 1));
      testPost.Save();
      //Act
      List<Post> result = Post.GetAll();
      List<Post> testList = new List<Post>{testPost};
      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_SaveAssignsIdToObject()
    {
      //Arrange
      Post testPost = new Post(5, "post text", new DateTime(2000, 1, 1));
      testPost.Save();
      //Act
      Post savedPost = Post.GetAll()[0];
      int result = savedPost.GetId();
      int testId = testPost.GetId();
      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_FindFindsPostInDatabase()
    {
      //Arrange
      Post testPost = new Post(5, "post text", new DateTime(2000, 1, 1));
      testPost.Save();
      //Act
      Post result = Post.Find(testPost.GetId());
      //Assert
      Assert.Equal(testPost, result);
    }


    [Fact]
    public void Dispose()
    {
      Post.DeleteAll();
    }
  }
}
