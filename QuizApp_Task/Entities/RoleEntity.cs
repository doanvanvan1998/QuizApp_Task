using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizApp_Task.Entities
{
    public class RoleEntity : IdentityRole<Guid>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public ICollection<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();
    }
}
