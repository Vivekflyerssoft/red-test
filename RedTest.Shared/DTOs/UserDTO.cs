namespace RedTest.Shared.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Balance { get; set; }
        public bool IsVerified { get; set; }
    }
}
