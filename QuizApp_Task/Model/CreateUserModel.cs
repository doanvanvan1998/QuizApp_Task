namespace QuizApp_Task.Model
{
    public class CreateUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Avatar { get; set; }
        public string Password { get; set; }
        public List<Guid> RoleIds { get; set; } 
    }
}
