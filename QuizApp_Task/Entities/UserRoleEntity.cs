using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace QuizApp_Task.Entities
{
    public class UserRoleEntity : IdentityUserRole<Guid>
    {
        [JsonIgnore]
        public UserEntity User { get; set; }
      
        public RoleEntity Role { get; set; }
    }
}
