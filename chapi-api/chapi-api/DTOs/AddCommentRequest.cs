using System.ComponentModel.DataAnnotations;

namespace DTOs;

/// <summary>
/// DTO for adding a comment to a task.
/// </summary>
public class AddCommentRequest
{
    /// <summary>
    /// The textual content of the comment.
    /// <example>This task is urgent and needs to be finished today.</example>
    /// </summary>
    [Required(ErrorMessage = "Comment content is required.")]
    public string Content { get; set; }
}
