using Microsoft.AspNetCore.Identity;
using QuizApp_Task.Until;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp_Task.Entities
{
    public class UserEntity : IdentityUser<Guid>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [NotMapped]
        public string DisplayName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        [Required]
        [DataType(DataType.Date)]
        [DateLessThanToday]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(500)]
        public string Avatar { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public ICollection<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();
        public ICollection<UserQuizEntity> UserQuizzes { get; set; } = new List<UserQuizEntity>();
    }
}
