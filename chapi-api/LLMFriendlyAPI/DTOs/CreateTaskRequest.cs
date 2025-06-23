using System.ComponentModel.DataAnnotations;

namespace LLMFriendlyAPI.DTOs
{
    /// <summary>
    /// DTO to create a new task with title, description, and due date.
    /// </summary>
    public class CreateTaskRequest
    {
        /// <summary>
        /// The short title of the task.
        /// <example>Submit report</example>
        /// </summary>
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        /// <summary>
        /// Detailed description of the task.
        /// <example>Submit the final Q2 report to the finance department</example>
        /// </summary>
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        /// <summary>
        /// The due date of the task.
        /// <example>2025-06-30T17:00:00Z</example>
        /// </summary>
        [Required(ErrorMessage = "DueDate is required.")]
        public DateTime DueDate { get; set; }
    }
}
