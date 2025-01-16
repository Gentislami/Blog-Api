namespace Blog_Api.src.Dtos
{
    public class UserDto : IDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}