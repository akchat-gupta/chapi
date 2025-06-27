using System.ComponentModel.DataAnnotations;

namespace DTOs;

/// <summary>
/// DTO to update the status of a task.
/// </summary>
public class TaskStatusUpdateRequest
{
    /// <summary>
    /// The new status to apply to the task.
    /// <example>Completed</example>
    /// </summary>
    [Required(ErrorMessage = "NewStatus is required.")]
    public Enums.TaskStatus NewStatus { get; set; }

    /// <summary>
    /// Optional reason for status change.
    /// <example>Final review done. QA approved.</example>
    /// </summary>
    public string? Reason { get; set; }
}
