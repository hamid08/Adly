using Adly.Domain.Common;
using Adly.Domain.Entities.Ad;
using Microsoft.AspNetCore.Identity;

namespace Adly.Domain.Entities.User;

public sealed class UserEntity:IdentityUser<Guid>,IEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string UserCode { get;private set; }

    private List<AdEntity> _ads=new();

    public IReadOnlyList<AdEntity> Ads => _ads.AsReadOnly();

    public UserEntity( string firstName, string lastName,string userName,string email):base(userName)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        UserCode = Guid.NewGuid().ToString("N")[0..7];
        Email = email;
    }
    
    public ICollection<UserRoleEntity> UserRoles { get; set; }
    public ICollection<UserClaimEntity> UserClaims { get; set; }
    public ICollection<UserLoginEntity> UserLogins { get; set; }
    public ICollection<UserTokenEntity> UserTokens { get; set; }


    public void AddUserAd(AdEntity entity)
    {
        _ads.Add(entity);
    }
}