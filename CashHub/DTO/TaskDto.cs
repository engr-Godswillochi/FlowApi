namespace CashHub.DTO
{
    public class TaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public IFormFile TaskImage { get; set; }
    }
}
