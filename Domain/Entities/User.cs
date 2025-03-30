namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string Email { get; set; }
        public required string Cellphone { get; set; }
        public required string Username { get; set; }
        public required string HashedPassword { get; set; }
    }
}
