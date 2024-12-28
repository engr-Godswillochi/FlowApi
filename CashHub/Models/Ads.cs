namespace CashHub.Models
{
    public class Ads
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; } = "pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    public enum TaskStatus
    {
        Pending,
        InProgress,
        Completed
    }
}
