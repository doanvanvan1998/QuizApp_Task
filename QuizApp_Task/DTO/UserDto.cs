namespace QuizApp_Task.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Avatar { get; set; }
        public bool IsActive { get; set; }
        public ICollection<string> Roles { get; set; } // Thêm thuộc tính Roles để lưu danh sách vai trò của người dùng
    }
}
