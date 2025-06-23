namespace LLMFriendlyAPI.Models
{
    public class TaskStatusLog
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Models.Task Task { get; set; }
    }
}
