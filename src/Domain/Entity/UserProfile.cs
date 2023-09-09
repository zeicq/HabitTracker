using Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entity;

public class UserProfile : EntityAuditData
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Habit> Habits { get; set; }
    public string UserId { get; set; } 
    public IdentityUser IdentityUser { get; set; }


}