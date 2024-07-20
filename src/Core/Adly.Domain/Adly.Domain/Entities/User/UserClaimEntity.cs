using Adly.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Adly.Domain.Entities.User;

public class UserClaimEntity:IdentityUserClaim<Guid>,IEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    
    public UserEntity User { get; set; }
}