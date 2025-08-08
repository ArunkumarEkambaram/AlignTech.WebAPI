namespace AlignTech.WebAPI.DataFirst.DTOs
{
    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterDto : UserDto
    {
        public string Name { get; set; } = string.Empty;
    }
}
