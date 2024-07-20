using Adly.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Adly.Domain.Entities.User;

public class RoleEntity:IdentityRole<Guid>,IEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    
    public string DisplayName { get; private set; }

    public RoleEntity(string displayName,string name):base(name)
    {
        DisplayName = displayName;
    }
    
    public ICollection<UserRoleEntity> UserRoles { get; set; }
    public ICollection<RoleClaimEntity> RoleClaims { get; set; }
}