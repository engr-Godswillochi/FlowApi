namespace CashHub.DTO
{
    public class SignupDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? ReferredBy { get; set; }
        public DateTime RegTime { get; set; }
    }
}
