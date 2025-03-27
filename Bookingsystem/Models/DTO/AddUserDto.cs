namespace Bookingsystem.Models.DTO
{
    public class AddUserDto
    {
        //The properties I want accept from the user
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Cellphone { get; set; }
    }
}
