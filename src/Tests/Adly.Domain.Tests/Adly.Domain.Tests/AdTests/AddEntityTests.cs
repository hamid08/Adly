using Adly.Domain.Entities.Ad;
using FluentAssertions;

namespace Adly.Domain.Tests.AdTests;


public class AddEntityTests
{

    [Fact]
    public void Creating_Ads_With_Null_User_Should_Throw_Exception()
    {
        //Arrange
        var description = "Test description";
        var name = "Ad name";
        Guid? userId = null;
        Guid categoryId = Guid.NewGuid();
        Guid? locationId = Guid.NewGuid();
        
        //Act 
        Action act = () => AdEntity.Create(name, description, userId, categoryId,locationId);
        
        //assert

        act.Should().Throw<ArgumentException>();

    }

    [Fact]
    public void Creating_Ads_With_Null_Category_Should_Throw_Exception()
    {
        //Arrange
        var description = "Test description";
        var name = "Ad name";
        Guid? userId = Guid.NewGuid();
        Guid? categoryId =null ;
        Guid? locationId=Guid.NewGuid();
        
        //Act 
        Action act = () => AdEntity.Create(name, description, userId, categoryId,locationId);
        
        //assert

        act.Should().Throw<ArgumentException>();

    }
    
    [Fact]
    public void Creating_Ads_With_Empty_Category_Should_Throw_Exception()
    {
        //Arrange
        var description = "Test description";
        var name = "Ad name";
        Guid? userId = Guid.NewGuid();
        Guid? categoryId =Guid.Empty ;
        Guid? locationId=Guid.NewGuid();
        
        //Act 
        Action act = () => AdEntity.Create(name, description, userId, categoryId,locationId);
        
        //assert

        act.Should().Throw<ArgumentException>();

    }
    
    [Fact]
    public void Creating_Ads_With_Empty_User_Should_Throw_Exception()
    {
        //Arrange
        var description = "Test description";
        var name = "Ad name";
        Guid? userId = Guid.Empty;
        Guid? categoryId =Guid.NewGuid() ;
        Guid? locationId=Guid.NewGuid();
        
        //Act 
        Action act = () => AdEntity.Create(name, description, userId, categoryId,locationId);
        
        //assert

        act.Should().Throw<ArgumentException>();

    }

    [Fact]
    public void Two_Categories_With_SameId_Must_Be_Equal()
    {
        //Arrange
        var description = "Test description";
        var name = "Ad name";
        Guid? userId = Guid.NewGuid();
        Guid? categoryId =Guid.NewGuid() ;
        Guid? id = Guid.NewGuid();
        Guid? locationId=Guid.NewGuid();
        
        //Act
        var ad1 = AdEntity.Create(id, name, description, userId, categoryId,locationId);
        var ad2 = AdEntity.Create(id, name, description, userId, categoryId,locationId);

        ad1.Equals(ad2).Should().BeTrue();
    }
    
    [Fact]
    public void Creating_An_Ad_Should_Have_ChangeLog()
    {
        //Arrange
        var description = "Test description";
        var name = "Ad name";
        Guid? userId = Guid.NewGuid();
        Guid? categoryId =Guid.NewGuid() ;
        Guid? locationId=Guid.NewGuid();
        
        //Act 
       var ad= AdEntity.Create(name, description, userId, categoryId,locationId);
        
        //assert

        ad.ChangeLogs.Should().HaveCount(1);

    }
    
    [Fact]
    public void Changing_Ad_State_From_Approved_To_Other_States_Is_Not_Allowed()
    {
        //Arrange
        var description = "Test description";
        var name = "Ad name";
        Guid? userId = Guid.NewGuid();
        Guid? categoryId =Guid.NewGuid() ;
        Guid? locationId=Guid.NewGuid();
        
        //Act 
        var ad= AdEntity.Create(name, description, userId, categoryId,locationId);
        
        //assert

        ad.ChangeState(AdEntity.AdStates.Approved);

        var result = ad.ChangeState(AdEntity.AdStates.Rejected);

        result.IsSuccess.Should().BeFalse();

    }
    
    [Fact]
    public void Changing_Ad_State_Should_Log()
    {
        //Arrange
        var description = "Test description";
        var name = "Ad name";
        Guid? userId = Guid.NewGuid();
        Guid? categoryId =Guid.NewGuid() ;
        Guid? locationId=Guid.NewGuid();
        
        //Act 
        var ad= AdEntity.Create(name, description, userId, categoryId,locationId);

        var changeLogCount = ad.ChangeLogs.Count;
        
        //assert

        ad.ChangeState(AdEntity.AdStates.Approved);

       var changeLogCountAfterChangeState= ad.ChangeLogs.Count;

       changeLogCountAfterChangeState.Should().BeGreaterThan(changeLogCount);

    }

    [Fact]
    public void Ad_State_Should_Be_Pending_After_Edit()
    {
        //Arrange
        var description = "Test description";
        var name = "Ad name";
        Guid? userId = Guid.NewGuid();
        Guid? categoryId =Guid.NewGuid() ;
        Guid? locationId=Guid.NewGuid();
        
        //Act 
        var ad = AdEntity.Create(name, description, userId, categoryId, locationId);

        ad.ChangeState(AdEntity.AdStates.Approved);

        ad.Edit("New Title", "New Description", categoryId, locationId);

        ad.CurrentState.Should().Be(AdEntity.AdStates.Pending);
    }

    [Fact]
    public void Should_Create_Log_After_Editing_An_Ad()
    {
        //Arrange
        var description = "Test description";
        var name = "Ad name";
        Guid? userId = Guid.NewGuid();
        Guid? categoryId =Guid.NewGuid() ;
        Guid? locationId=Guid.NewGuid();
        
        //Act 
        var ad = AdEntity.Create(name, description, userId, categoryId, locationId);

        ad.ChangeState(AdEntity.AdStates.Approved);

        var currentChangeLogCount = ad.ChangeLogs.Count;
        
        ad.Edit("New Title", "New Description", categoryId, locationId);

        ad.ChangeLogs.Count.Should().BeGreaterThan(currentChangeLogCount);
    }
}