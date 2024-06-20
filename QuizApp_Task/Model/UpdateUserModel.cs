namespace QuizApp_Task.Model
{
    public class UpdateUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public bool IsActive { get; set; }
        public List<Guid> RoleIds { get; set; }
    }
}
