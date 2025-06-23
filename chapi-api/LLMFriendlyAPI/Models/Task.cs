namespace LLMFriendlyAPI.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public ICollection<TaskComment> Comments { get; set; } = new List<TaskComment>();
        public ICollection<TaskStatusLog> StatusLogs { get; set; } = new List<TaskStatusLog>();
    }
}
