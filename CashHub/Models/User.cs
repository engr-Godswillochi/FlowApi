namespace CashHub.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Fullname { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ReferralCode { get; set; }
        public string? ReferredBy { get; set; }
        public int NumRef { get; set; } = 0;
        public int ReferalBal { get; set; } = 0;
        public int TaskBal { get; set; } = 1100;
        public string? Password { get; set; }
        public bool isUserVip { get; set; } = false;
        public string? ProfilePic { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? Twitter { get; set; }
        public string? Whatsapp { get; set; }
        public int[] CompletedTask { get; set; } = Array.Empty<int>();
        public int[] Referred { get; set; } = Array.Empty<int>();
        public DateTime RegTime { get; set; }
    }
}
