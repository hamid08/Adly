using Adly.Domain.Entities.Ad;
using Adly.Domain.Entities.User;
using FluentAssertions;

namespace Adly.Domain.Tests.UserTests;

public class UserEntityTests
{
    [Fact]
    public void Should_Add_Ad_ToUser()
    {
        var user = new UserEntity("Test", "Test", "TestUserName", "Test@test.com");

        var ad = AdEntity.Create(Guid.NewGuid(), "Test User Ad", "Test Description", user.Id, Guid.NewGuid(),
            Guid.NewGuid());
        
        user.AddUserAd(ad);

        user.Ads.Should().HaveCount(1);
    }
}