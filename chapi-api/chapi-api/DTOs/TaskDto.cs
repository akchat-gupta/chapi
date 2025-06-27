using System.ComponentModel.DataAnnotations;

namespace DTOs;

/// <summary>
/// DTO representing a task's current state.
/// </summary>
public class TaskDto
{
    /// <summary>
    /// Unique identifier for the task.
    /// <example>101</example>
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Title of the task.
    /// <example>Fix login bug</example>
    /// </summary>
    [Required]
    public string Title { get; set; }

    /// <summary>
    /// Description of what the task involves.
    /// <example>Resolve the issue where login returns 500 error.</example>
    /// </summary>
    [Required]
    public string Description { get; set; }

    /// <summary>
    /// Date and time by which the task is due.
    /// <example>2025-07-01T12:00:00Z</example>
    /// </summary>
    [Required]
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Whether the task has been marked as completed.
    /// <example>true</example>
    /// </summary>
    [Required]
    public bool IsCompleted { get; set; }
}
