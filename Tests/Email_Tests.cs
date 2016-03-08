using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BarterNamespace
{
  public class EmailTest : IDisposable
  {
    public EmailTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=barter_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_EmptyAtFirst()
    {
      //Arrange, Act
      int result = Email.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_EqualOverrideTrueForSameDescription()
    {
      //Arrange, Act
      Email firstEmail = new Email(5, "email text", new DateTime(2000, 1, 1), 1);
      Email secondEmail = new Email(5, "email text", new DateTime(2000, 1, 1), 1);
      //Assert
      Assert.Equal(firstEmail, secondEmail);
    }

    [Fact]
    public void Test_Save()
    {
      //Arrange
      Email testEmail = new Email(5, "email text", new DateTime(2000, 1, 1), 1);
      testEmail.Save();
      //Act
      List<Email> result = Email.GetAll();

      Console.WriteLine(result.Count);



      List<Email> testList = new List<Email>{testEmail};
      Console.WriteLine(testList.Count);
      

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_SaveAssignsIdToObject()
    {
      //Arrange
      Email testEmail = new Email(5, "email text", new DateTime(2000, 1, 1), 1);
      testEmail.Save();
      //Act
      Email savedEmail = Email.GetAll()[0];
      int result = savedEmail.GetId();
      int testId = testEmail.GetId();
      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_FindFindsEmailInDatabase()
    {
      //Arrange
      Email testEmail = new Email(5, "email text", new DateTime(2000, 1, 1), 1);
      testEmail.Save();
      //Act
      Email result = Email.Find(testEmail.GetId());
      //Assert
      Assert.Equal(testEmail, result);
    }


    [Fact]
    public void Dispose()
    {
      Email.DeleteAll();

    }
  }
}
