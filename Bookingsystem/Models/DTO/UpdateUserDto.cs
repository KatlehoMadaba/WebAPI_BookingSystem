namespace Bookingsystem.Models.DTO
{
    public class UpdateUserDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Cellphone { get; set; } 
    }
}
