namespace Models;

public class TaskComment
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Task Task { get; set; }
}
